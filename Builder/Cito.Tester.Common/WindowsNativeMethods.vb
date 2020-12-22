Imports System.Diagnostics.CodeAnalysis
Imports System.Runtime.InteropServices

Public NotInheritable Class WindowsNativeMethods


    Private Sub New()
    End Sub


    <SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId:="1#"), _
        DllImport("urlmon.dll", CharSet:=CharSet.Auto)> _
    Public Shared Function FindMimeFromData( _
        pBC As IntPtr, _
        <MarshalAs(UnmanagedType.LPWStr)> _
        pwzUrl As String, _
        <MarshalAs(UnmanagedType.LPArray, ArraySubType:=UnmanagedType.I1, SizeParamIndex:=3)> _
        pBuffer As Byte(), _
        cbSize As Integer, _
        <MarshalAs(UnmanagedType.LPWStr)> _
        pwzMimeProposed As String, _
        dwMimeFlags As Integer, _
        ByRef ppwzMimeOut As IntPtr, _
        dwReserved As Integer) As Integer
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function GetForegroundWindow() As IntPtr
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function SetForegroundWindow(hWnd As IntPtr) As Integer
    End Function


    <SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId:="Member")> <SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId:="Member")> Public Const ENUM_CURRENT_SETTINGS As Integer = -1
    <SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId:="Member")> <SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId:="Member")> Public Const CDS_UPDATEREGISTRY As Integer = 1
    <SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId:="Member")> <SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId:="Member")> Public Const CDS_TEST As Integer = 2
    <SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId:="Member")> <SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId:="Member")> Public Const DISP_CHANGE_SUCCESSFUL As Integer = 0
    <SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId:="Member")> <SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId:="Member")> Public Const DISP_CHANGE_RESTART As Integer = 1
    <SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", MessageId:="Member")> <SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId:="Member")> Public Const DISP_CHANGE_FAILED As Integer = -1

    <SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")> <SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")> <SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased")> <StructLayout(LayoutKind.Sequential)> _
    Public Structure DEVMODE1

        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> Public dmDeviceName As String
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmSpecVersion As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmDriverVersion As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmSize As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmDriverExtra As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmFields As Integer

        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmOrientation As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmPaperSize As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmPaperLength As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmPaperWidth As Short

        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmScale As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmCopies As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmDefaultSource As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmPrintQuality As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmColor As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmDuplex As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmYResolution As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmTTOption As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmCollate As Short

        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> Public dmFormName As String
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmLogPixels As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmBitsPerPel As Short
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmPelsWidth As Integer
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmPelsHeight As Integer

        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmDisplayFlags As Integer
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmDisplayFrequency As Integer

        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmICMMethod As Integer
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmICMIntent As Integer
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmMediaType As Integer
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmDitherType As Integer
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmReserved1 As Integer
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmReserved2 As Integer

        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmPanningWidth As Integer
        <SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")> Public dmPanningHeight As Integer
    End Structure

    <SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId:="2#")> <DllImport("user32.dll")> _
    Public Shared Function EnumDisplaySettings(deviceName As String, modeNum As Integer, ByRef devMode As DEVMODE1) As Integer
    End Function

    <SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId:="0#")> <DllImport("user32.dll")> _
    Public Shared Function ChangeDisplaySettings(ByRef devMode As DEVMODE1, flags As Integer) As Integer
    End Function


End Class

