Imports System.Drawing
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Public Class QbSettings

    Public Property Language As String
    Public Property ImportLastFilename As String
    Public Property CesTestPreviewerLocation As String
    Public Property ItemEditorFullScreen As Boolean
    Public Property ItemEditorSize As Size
    Public Property ItemEditorPosition As Point
    Public Property Base As String
    Public Property UserBankSettings As String
    Public Property GridColumnOrderSettings As String
    Public Property UserWizardSettings As String
    Public Property ItemEditorGroups As String
    Public Property ItemEditorLeftColumnWidth As Integer
    Public Property ItemEditorRightColumnWidth As Integer

    Public Property Reports_WordReport As String
    Public Property Reports_ExcelReport As String

    Public Property Publication_PackageExportLocation As String
    Public Property Publication_TestServerTimeToLive As String
    Public Property Publication_TestServerPackageName As String
    Public Property Publication_WordExportLocation As String
    Public Property Publication_CesExportLocation As String
    Public Property Publication_CesitemPreviewResolution As Integer

    Public Property UI_itemPreviewResolution As Integer
    Public Property UI_formulaEditorFont As Font
    Public Property UI_itemPreviewTarget As String
    Public Property UI_itemPreviewTheme As String
    Public Property UI_itemPreviewThemeConfig As String

    Public Property TestBuilderClient_SelectedBankId As Integer
    Public Property TestBuilderClient_ExportLocation As String
    Public Property TestBuilderClient_ImportLocation As String
    Public Property TestBuilderClient_ItemEditorWindowState As Integer
    Public Property TestBuilderClient_ItemEditorBounds As Rectangle = New Rectangle(10, 10, 800, 600)
    Public Property TestBuilderClient_TestEditorBounds As Rectangle = New Rectangle(10, 10, 800, 600)
    Public Property TestBuilderClient_PublishLocation As String
    Public Property TestBuilderClient_TestPackageEditorWindowState As Integer
    Public Property TestBuilderClient_TestPackageEditorBounds As Rectangle = New Rectangle(10, 10, 800, 600)
    Public Property TestBuilderClient_SelectedServer As String
    Public Property TestBuilderClient_SelectedTabKey As String
    Public Property TestBuilderClient_TestEditorWindowState As Integer

    Public Property IsQATSaveAsVisible As Boolean
    Public Property IsQATSaveAndCloseVisible As Boolean

End Class

Public Class SizeJsonConverter
    Inherits JsonConverter

    Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
        Dim size As Size = CType(value, Size)
        Dim jo As JObject = New JObject()
        jo.Add("Width", size.Width)
        jo.Add("Height", size.Height)
        jo.WriteTo(writer)
    End Sub

    Public Overrides Function CanConvert(objectType As Type) As Boolean
        Return (objectType = GetType(Size))
    End Function

    Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
        Dim jo As JObject = JObject.Load(reader)
        Return New Size(CInt(jo("Width")), CInt(jo("Height")))
    End Function
End Class

Public Class PointJsonConverter
    Inherits JsonConverter

    Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
        Dim point As Point = CType(value, Point)
        Dim jo As JObject = New JObject()
        jo.Add("X", point.X)
        jo.Add("Y", point.Y)
        jo.WriteTo(writer)
    End Sub

    Public Overrides Function CanConvert(objectType As Type) As Boolean
        Return (objectType = GetType(Point))
    End Function

    Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
        Dim jo As JObject = JObject.Load(reader)
        Return New Point(CInt(jo("X")), CInt(jo("Y")))
    End Function
End Class

Public Class RectangleJsonConverter
    Inherits JsonConverter

    Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
        Dim rect As Rectangle = CType(value, Rectangle)
        Dim jo As JObject = New JObject()
        jo.Add("X", rect.X)
        jo.Add("Y", rect.Y)
        jo.Add("Width", rect.Width)
        jo.Add("Height", rect.Height)
        jo.WriteTo(writer)
    End Sub

    Public Overrides Function CanConvert(objectType As Type) As Boolean
        Return (objectType = GetType(Rectangle))
    End Function

    Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
        Dim jo As JObject = JObject.Load(reader)
        Return New Rectangle(CInt(jo("X")), CInt(jo("Y")), CInt(jo("Width")), CInt(jo("Height")))
    End Function
End Class