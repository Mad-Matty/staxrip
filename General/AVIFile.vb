Imports System.Runtime.InteropServices
Imports System.Reflection
Imports System.Text

Public Class AVIFile
    Implements IDisposable

    Private AviFile As IntPtr
    Private FrameObject As IntPtr
    Private AviStream As IntPtr
    Private StreamInfo As _AVISTREAMINFO
    Private Sourcefile As String

    Sub New(path As String)
        Try
            AVIFileInit()
            Sourcefile = path

            Const OF_SHARE_DENY_WRITE = 32

            If AVIFileOpen(AviFile, path, OF_SHARE_DENY_WRITE, IntPtr.Zero) <> 0 Then
                Throw New Exception("AVIFileOpen failed")
            End If

            If AVIFileGetStream(AviFile, AviStream, mmioStringToFOURCC("vids", 0), 0) <> 0 Then 'FourCC for vids
                Throw New Exception("AVIFileGetStream failed")
            End If

            FrameCountValue = AVIStreamLength(AviStream)

            If FrameCountValue = 240 Then
                Dim o = Marshal.GetObjectForIUnknown(AviFile)

                If Not o Is Nothing Then
                    Dim i = CType(o, IAvisynthClipInfo)

                    If Not i Is Nothing Then
                        Dim ptr As IntPtr

                        If i.GetError(ptr) = 0 Then
                            ErrorMessageValue = Marshal.PtrToStringAnsi(ptr)
                        End If

                        Marshal.ReleaseComObject(i)
                    End If
                End If
            Else
                ErrorMessageValue = Nothing
            End If

            StreamInfo = New _AVISTREAMINFO()

            If AVIStreamInfo(AviStream, StreamInfo, Marshal.SizeOf(StreamInfo)) <> 0 Then
                Throw New Exception("AVIStreamInfo failed")
            End If
        Catch ex As Exception
            Dispose()
            LogAVS()
            Throw ex
        End Try
    End Sub

    Private FrameCountValue As Integer

    ReadOnly Property FrameCount() As Integer
        Get
            Return FrameCountValue
        End Get
    End Property

    ReadOnly Property FrameRate() As Double
        Get
            Return StreamInfo.dwRate / StreamInfo.dwScale
        End Get
    End Property

    ReadOnly Property FrameSize() As Size
        Get
            Return New Size(CInt(StreamInfo.rcFrame.Right), CInt(StreamInfo.rcFrame.Bottom))
        End Get
    End Property

    Private ErrorMessageValue As String

    ReadOnly Property ErrorMessage() As String
        Get
            Return ErrorMessageValue
        End Get
    End Property

    Private PositionValue As Integer

    Property Position() As Integer
        Get
            Return PositionValue
        End Get
        Set(value As Integer)
            If value < 0 Then
                PositionValue = 0
            ElseIf value > FrameCount - 1 Then
                PositionValue = FrameCount - 1
            Else
                PositionValue = value
            End If
        End Set
    End Property

    Sub LogAVS()
        Log.WriteHeader("AviSynth script failed to load")
        Log.WriteLine(File.ReadAllText(Sourcefile, Encoding.Default) + CrLf)

        If Directory.Exists(Paths.PluginsDir) Then
            Log.WriteLine("AviSynth Plugins: " + Directory.GetFiles(Paths.PluginsDir, "*.dll").ToArray.Join(", ").Replace(Paths.PluginsDir, "").Replace(".dll", ""))
        Else
            Log.WriteLine("AviSynth plugin directory is missing!")
        End If
    End Sub

    Function GetBitmap() As Bitmap
        If FrameObject = IntPtr.Zero Then
            Const AVIGETFRAMEF_BESTDISPLAYFMT = 1
            FrameObject = AVIStreamGetFrameOpen(AviStream, AVIGETFRAMEF_BESTDISPLAYFMT)
            If FrameObject = IntPtr.Zero Then FrameObject = AVIStreamGetFrameOpen(AviStream, 0)
        End If

        If FrameObject <> IntPtr.Zero Then
            Dim ptr = AVIStreamGetFrame(FrameObject, Position)
            If ptr <> IntPtr.Zero Then Return GetBMPFromDib(ptr)
        End If
    End Function

    Private Function GetBMPFromDib(pDIB As IntPtr) As Bitmap
        Dim pPix As New IntPtr(pDIB.ToInt64 + Marshal.SizeOf(GetType(BITMAPINFOHEADER)))
        Dim mi = GetType(Bitmap).GetMethod("FromGDIplus", BindingFlags.Static Or BindingFlags.NonPublic)
        Dim pBmp = IntPtr.Zero
        GdipCreateBitmapFromGdiDib(pDIB, pPix, pBmp)
        Return DirectCast(mi.Invoke(Nothing, {pBmp}), Bitmap)
    End Function

    <Guid("E6D6B708-124D-11D4-86F3-DB80AFD98778"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Private Interface IAvisynthClipInfo
        Function GetError(ByRef msg As IntPtr) As Integer
        Function GetParity(value As Integer) As Byte
        Function IsFieldBased() As Byte
    End Interface

    <DllImport("gdiplus.dll")>
    Private Shared Function GdipCreateBitmapFromGdiDib(bminfo As IntPtr, pixdat As IntPtr, ByRef image As IntPtr) As Integer
    End Function

    <DllImport("avifil32.dll")>
    Private Shared Sub AVIFileInit()
    End Sub

    <DllImport("avifil32.dll")>
    Private Shared Function AVIFileOpen(ByRef ppfile As IntPtr, szFile As String, uMode As Integer, pclsidHandler As IntPtr) As Integer
    End Function

    <DllImport("avifil32.dll")>
    Private Shared Function AVIFileGetStream(pfile As IntPtr, <Out()> ByRef ppavi As IntPtr, fccType As UInteger, lParam As Integer) As Integer
    End Function

    <DllImport("avifil32.dll")>
    Private Shared Function AVIStreamLength(pavi As IntPtr) As Integer
    End Function

    <DllImport("avifil32.dll", CharSet:=CharSet.Unicode)>
    Private Shared Function AVIStreamInfo(pAVIStream As IntPtr, ByRef psi As _AVISTREAMINFO, lSize As Integer) As Integer
    End Function

    <DllImport("avifil32.dll")>
    Private Shared Function AVIStreamGetFrameOpen(pAVIStream As IntPtr, lpbiWanted As Integer) As IntPtr
    End Function

    <DllImport("avifil32.dll")>
    Private Shared Function AVIStreamGetFrame(pGetFrameObj As IntPtr, lPos As Integer) As IntPtr
    End Function

    <DllImport("avifil32.dll")>
    Private Shared Function AVIStreamGetFrameClose(pGetFrameObj As IntPtr) As Integer
    End Function

    <DllImport("avifil32.dll")>
    Private Shared Function AVIStreamRelease(aviStream As IntPtr) As Integer
    End Function

    <DllImport("avifil32.dll")>
    Private Shared Function AVIFileRelease(pfile As IntPtr) As Integer
    End Function

    <DllImport("avifil32.dll")>
    Private Shared Sub AVIFileExit()
    End Sub

    <DllImport("winmm.dll")>
    Private Shared Function mmioStringToFOURCC(sz As String, uFlags As Integer) As UInteger
    End Function

    Private Structure BITMAPINFOHEADER
        Public biSize As UInt32
        Public biWidth As Int32
        Public biHeight As Int32
        Public biPlanes As Int16
        Public biBitCount As Int16
        Public biCompression As UInt32
        Public biSizeImage As UInt32
        Public biXPelsPerMeter As Int32
        Public biYPelsPerMeter As Int32
        Public biClrUsed As UInt32
        Public biClrImportant As UInt32
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Private Structure _AVISTREAMINFO
        Public fccType As UInt32
        Public fccHandler As UInt32
        Public dwFlags As UInt32
        Public dwCaps As UInt32
        Public wPriority As UInt16
        Public wLanguage As UInt16
        Public dwScale As UInt32
        Public dwRate As UInt32
        Public dwStart As UInt32
        Public dwLength As UInt32
        Public dwInitialFrames As UInt32
        Public dwSuggestedBufferSize As UInt32
        Public dwQuality As UInt32
        Public dwSampleSize As UInt32
        Public rcFrame As Native.RECT
        Public dwEditCount As UInt32
        Public dwFormatChangeCount As UInt32
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=64)>
        Public szName As String
    End Structure

#Region "IDisposable Support"

    Private WasDisposed As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not WasDisposed Then
            If FrameObject <> IntPtr.Zero Then AVIStreamGetFrameClose(FrameObject)
            If AviStream <> IntPtr.Zero Then AVIStreamRelease(AviStream)
            If AviFile <> IntPtr.Zero Then AVIFileRelease(AviFile)
            AVIFileExit()
            WasDisposed = True
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class