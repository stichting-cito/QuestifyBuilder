Imports System.Drawing.Imaging
Imports System.Globalization
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports System.IO
Imports System.Linq
Imports System.Xml.Linq
Imports Questify.Builder.Logic.ContentModel
Imports System.Threading.Tasks
Imports Cito.Tester.Common
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Logic.Service.Factories

Public Class AreaParameterEditorControl


    Private ReadOnly _areaParameter As AreaParameter
    Private ReadOnly _prmSetsEditor As ParameterSetsEditor
    Private ReadOnly _required As Boolean = False
    Private _sizeChanging As Boolean = False
    Private ReadOnly _resourceParameterEditorControl As ResourceParameterEditorControl
    Private ReadOnly _freeFormResourceParameterEditorControl As ResourceParameterEditorControl



    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)
    Public Event EditResource As EventHandler(Of ResourceNameEventArgs)



    Public Sub New(parent As ParameterSetsEditor, parameters As AreaParameter, ByVal itemResource As ResourceEntity)
        MyBase.New(parent, parameters, itemResource)
        InitializeComponent()
        _areaParameter = parameters
        Dim linkedResourceParameterName = parameters.DesignerSettings.GetSettingValueByKey("linkedresourceparametername")
        Dim freeformresourceparametername = parameters.DesignerSettings.GetSettingValueByKey("freeformresourceparametername")
        _prmSetsEditor = parent
        Try
            Dim subsetIds As String = parameters.DesignerSettings.GetSettingValueByKey("subsetidentifiers")
            If Not String.IsNullOrEmpty(subsetIds) Then
                subsetIdentifiers = DirectCast([Enum].Parse(GetType(SubSetIdentifierGeneration), subsetIds), SubSetIdentifierGeneration)
            Else
                subsetIdentifiers = SubSetIdentifierGeneration.Numeric
            End If
            _required = parameters.IsRequired
            If Not String.IsNullOrEmpty(parameters.DesignerSettings.GetSettingValueByKey("toolbarvisible")) Then
                Boolean.TryParse(parameters.DesignerSettings.GetSettingValueByKey("toolbarvisible"), AreaEditor1.AreaToolstrip.Visible)
            End If
            If Not String.IsNullOrEmpty(parameters.DesignerSettings.GetSettingValueByKey("maxnrofshapestocreate")) Then
                Integer.TryParse(parameters.DesignerSettings.GetSettingValueByKey("maxnrofshapestocreate"), AreaEditor1.MaxNrOfShapesToCreate)
            End If
            If String.IsNullOrEmpty(linkedResourceParameterName) OrElse Not parameters.SetValue(linkedResourceParameterName) Then
                Throw New ItemTemplateException(String.Format("Failed to link parameter '{0}' to a (XHTML)ResourceParameter. DesignerSetting '{1}' is mandatory!", parameters.Name, "linkedresourceparametername"))
            End If
        Catch ex As Exception
            Throw New AppLogicException(String.Format(My.Resources.ErrorParsingDesignerSettingsForParameter, parameters.Name), ex)
        End Try

        collectionParameter = parameters
        resourceEntity = itemResource

        InitParameters(parameterLayoutPanel, False)
        Me.AreaEditor1.ShapeList = DirectCast(collectionParameter, AreaParameter).ShapeList
        InitParameterCollection()

        Dim panelControls = parameterLayoutPanel.Controls
        _resourceParameterEditorControl = FindControl(Of ResourceParameterEditorControl)(panelControls, linkedResourceParameterName)
        If Not String.IsNullOrEmpty(freeformresourceparametername) Then
            _freeFormResourceParameterEditorControl = FindControl(Of ResourceParameterEditorControl)(panelControls, freeformresourceparametername)
            _freeFormResourceParameterEditorControl.IsSVGFreeFormResource = True
            Me.AreaEditor1.Enabled = String.IsNullOrEmpty(_freeFormResourceParameterEditorControl.ResourceParameter.Value)
        End If

        If _resourceParameterEditorControl Is Nothing Then Throw New ArgumentException(String.Format("Failed to find ResourceParameterEditorControl with name '{0}'", linkedResourceParameterName))
        _resourceParameterEditorControl.ForceShowDimensions = True
    End Sub



    Public Overrides Function ValidateParameter() As String
        Dim result As String = MyBase.ValidateParameter
        If Me.Enabled AndAlso _required Then
            If Me.AreaEditor1.ShapeList Is Nothing OrElse Me.AreaEditor1.ShapeList.Count = 0 Then
                Dim label = _areaParameter.DesignerSettings.GetSettingValueByKey("label")
                If String.IsNullOrEmpty(label) Then label = _areaParameter.DesignerSettings.GetSettingValueByKey("group")
                If String.IsNullOrEmpty(label) Then label = _areaParameter.Name
                If Not String.IsNullOrEmpty(result) Then result += vbNewLine
                result += String.Format(My.Resources.MandatoryEmptyShapelistMessage, label)
            End If
        End If
        Return result
    End Function


    Private Sub OnShapeRemoved(ByVal sender As Object, ByVal e As EventArgs)
        For Each finding In _prmSetsEditor.Solution.Findings.ToList()
            _prmSetsEditor.Solution.Findings.Remove(finding)
        Next
        If Not (Me.EditorParent Is Nothing) Then Me.EditorParent.ValidateThisEditor(Me)
    End Sub

    Private Sub OnShapeAdded(ByVal sender As Object, ByVal e As EventArgs)
        If Not (Me.EditorParent Is Nothing) Then Me.EditorParent.ValidateThisEditor(Me)
    End Sub

    Private Sub OnSvgRemoved(sender As Object, e As ResourceNameEventArgs)
        ClearFreeForms()
    End Sub

    Protected Overridable Sub OnResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(sender, e)
    End Sub

    Protected Overridable Sub OnEditResource(ByVal sender As Object, ByVal e As ResourceNameEventArgs)
        RaiseEvent EditResource(Me, e)
    End Sub

    Private Sub InitParameterCollection()
        Dim pCol As New ParameterSetCollection()
        Dim paramSet As ParameterCollection = ParameterCollection.DeepClone(collectionParameter.BluePrint)
        Dim doAdd As Boolean = False
        Dim index = 1
        For Each param As ParameterBase In paramSet.InnerParameters
            paramSet.Id = GetSubSetIdentifier(index)
            If collectionParameter.Value.GetParameter(param.Name, paramSet.Id) Is Nothing Then
                doAdd = True
                Dim value As String = param.DesignerSettings.GetSettingValueByKey("defaultvalue")
                If Not String.IsNullOrEmpty(value) AndAlso Not param.SetValue(value) Then
                    Throw New ItemTemplateException(String.Format("Parameter '{0}' tries to set a default value which was not possible.", param.Name))
                End If
                If param.IsVisible Then
                    CreateParameterRow(parameterLayoutPanel, param, paramSet.Id, False)
                End If
            End If
        Next

        If doAdd Then
            Dim paramsetToRemove = pCol.FirstOrDefault(Function(x) x.Id.Equals(paramSet.Id))
            If paramsetToRemove IsNot Nothing Then
                pCol.Remove(paramsetToRemove)
            End If
            pCol.Add(paramSet)
        End If

        collectionParameter.Value.AddRange(pCol)
    End Sub

    Private Sub ResourceChanged(o As Object, e As ResourceNameEventArgs)
        _sizeChanging = True
        Dim editorControl As ResourceParameterEditorControl = (DirectCast(o, ResourceParameterEditorControl))
        If editorControl IsNot Nothing AndAlso editorControl.IsSVGFreeFormResource Then
            SetFreeForms()
        ElseIf editorControl.HeightValue.HasValue AndAlso editorControl.WidthValue.HasValue Then
            SetBackgroundImage()
        End If
        OnAddingResource(New ResourceNameEventArgs(e.ResourceName))
        _sizeChanging = False
    End Sub

    Private _sizeChanged As Integer = 0
    Private Async Sub ImageSizeChanged(o As Object, e As EventArgs)
        Dim editorControl = (DirectCast(o, ResourceParameterEditorControl))
        If Not _sizeChanging AndAlso editorControl.HeightValue.HasValue AndAlso editorControl.WidthValue.HasValue Then
            _sizeChanged += 1
            Await SetBackgroundImageWithDelay()
            If _sizeChanged = 1 Then
                SetBackgroundImage()
            End If
            _sizeChanged -= 1
        End If
    End Sub

    Private Async Function SetBackgroundImageWithDelay() As Task
        Await Task.Delay(1000)
    End Function

    Private Sub LinkToResourceEditorControl()
        If _resourceParameterEditorControl.WidthValue.HasValue AndAlso _resourceParameterEditorControl.HeightValue.HasValue Then
            SetBackgroundImage()
        End If
        If _freeFormResourceParameterEditorControl IsNot Nothing Then
            AddHandler _freeFormResourceParameterEditorControl.AddingResource, AddressOf ResourceChanged
            AddHandler _freeFormResourceParameterEditorControl.RemovingResource, AddressOf OnSvgRemoved
        End If
        If _resourceParameterEditorControl IsNot Nothing Then
            AddHandler _resourceParameterEditorControl.AddingResource, AddressOf ResourceChanged
            AddHandler _resourceParameterEditorControl.ImageSizeChanged, AddressOf ImageSizeChanged
        End If
    End Sub

    Private Sub SetBackgroundImage()
        Dim pictureName = _resourceParameterEditorControl.ResourceParameter.Value
        If Not String.IsNullOrEmpty(pictureName) Then
            Dim r = TryCast(ResourceFactory.Instance().GetResourceByNameWithOption(MyBase.EditorParent.ResourceEntity.BankId, pictureName, New ResourceRequestDTO()), GenericResourceEntity)
            If (r IsNot Nothing) Then
                If r.ResourceData Is Nothing Then
                    r.ResourceData = ResourceFactory.Instance.GetResourceData(r)
                End If
                Try
                    Using binDataStream = New MemoryStream(r.ResourceData.BinData)
                        Using image As Image = Image.FromStream(binDataStream)
                            Using targetStream As New MemoryStream()
                                If (_resourceParameterEditorControl.ResourceParameter.EditSize AndAlso
    _resourceParameterEditorControl.WidthValue.HasValue AndAlso
    _resourceParameterEditorControl.HeightValue.HasValue AndAlso
    _resourceParameterEditorControl.ResourceParameter.Width > 0 AndAlso
    _resourceParameterEditorControl.ResourceParameter.Height > 0 AndAlso
    (_resourceParameterEditorControl.ResourceParameter.Width <> image.Width OrElse _resourceParameterEditorControl.ResourceParameter.Height <> image.Height)) Then
                                    Using resizedImage = New Bitmap(image, New Size(_resourceParameterEditorControl.ResourceParameter.Width, _resourceParameterEditorControl.ResourceParameter.Height))
                                        resizedImage.Save(targetStream, ImageFormat.Gif)
                                    End Using
                                Else
                                    image.Save(targetStream, ImageFormat.Gif)
                                End If
                                AreaEditor1.SetEditImage(Image.FromStream(targetStream, True))
                            End Using

                        End Using
                    End Using
                    Return
                Catch Ex As Exception
                End Try
                Throw New ArgumentException("Error setting background. Possibly due to an unsupported image format.")
            End If
        End If
    End Sub


    Private Sub SetFreeForms()
        Dim svgName = _freeFormResourceParameterEditorControl.ResourceParameter.Value
        If Not String.IsNullOrEmpty(svgName) Then
            Dim svgXml = String.Empty
            Dim r = ResourceFactory.Instance().GetResourceByNameWithOption(MyBase.EditorParent.ResourceEntity.BankId, svgName, New ResourceRequestDTO())
            If (r IsNot Nothing AndAlso TypeOf r Is GenericResourceEntity) Then
                If r.ResourceData Is Nothing Then
                    r.ResourceData = ResourceFactory.Instance.GetResourceData(r)
                End If

                Using binDataStream = New MemoryStream(r.ResourceData.BinData)
                    Dim reader = New StreamReader(binDataStream)
                    svgXml = reader.ReadToEnd()
                End Using
            End If

            If Not String.IsNullOrEmpty(svgXml) Then
                Dim svgElement = XDocument.Parse(svgXml).Root

                Dim shapeList = New ShapeList
                For Each shape In SvgToShapes(svgElement)
                    shapeList.Add(shape)
                Next
                Me.AreaEditor1.SetExternalShapeList(shapeList)
                Me.AreaEditor1.Enabled = False

            End If
        End If
    End Sub

    Private Sub ClearFreeForms()
        Me.AreaEditor1.DeleteAll(False)
        Me.AreaEditor1.Enabled = True
    End Sub

    Private Function FindControl(Of T As Control)(tlControls As TableLayoutControlCollection, ByVal controlName As String) As T
        If Not String.IsNullOrEmpty(controlName) Then
            For Each ctrl As Control In tlControls
                If TypeOf ctrl Is T AndAlso ctrl.Name = controlName Then
                    Return DirectCast(ctrl, T)
                End If
            Next
        End If
        Return Nothing
    End Function

    Private Function SvgToShapes(svgXml As XElement) As List(Of Shape)
        Dim polygons As List(Of Shape) = GetPolygons(svgXml)
        Dim circles As List(Of Shape) = GetCircles(svgXml)
        Dim rectangles As List(Of Shape) = GetRectangles(svgXml)

        Dim shapes As List(Of Shape) = polygons
        shapes.AddRange(circles)
        shapes.AddRange(rectangles)

        Return shapes
    End Function

    Private Function GetPolygons(svg As XElement) As List(Of Shape)
        Dim i As Integer = 1
        Dim svgPolygons As IEnumerable(Of XElement) = svg.Descendants("{http://www.w3.org/2000/svg}polygon")
        Dim polygons As List(Of Shape) = New List(Of Shape)()
        Dim coords As List(Of Point)
        Dim coordinate As String()
        For Each svgPolygon As XElement In svgPolygons
            coords = New List(Of Point)()
            Dim points As String() = svgPolygon.Attribute("points").Value.Trim.Split(" "c)
            For Each point In points
                If point <> String.Empty Then
                    coordinate = point.Split(","c)
                    coords.Add(New Point(GetIntValue(coordinate.First), GetIntValue(coordinate.Last)))
                End If
            Next
            If Not coords.First.Equals(coords.Last) Then coords.Add(coords.First)
            If PolygonIsTriangle(coords) Then
                Dim triangle As New PointUpTriangleShape() With {
                    .BaseLeft = coords(0),
                    .Top = coords(1),
                    .BaseRight = coords(2),
                    .Identifier = "triangle" + i.ToString
                }
                polygons.Add(triangle)
            Else
                Dim polygon As New PolygonShape() With {
                    .Coords = coords,
                    .Identifier = "polygon" + i.ToString
                }
                polygons.Add(polygon)
            End If
            i = i + 1
        Next
        Return polygons
    End Function

    Private Shared Function PolygonIsTriangle(points As IReadOnlyList(Of Point)) As Boolean
        Return points.Count = 4 AndAlso points(0).Y = points(2).Y AndAlso points.First().Equals(points.Last())
    End Function

    Private Function GetCircles(svg As XElement) As List(Of Shape)
        Dim i As Integer = 1
        Dim svgCircles As IEnumerable(Of XElement) = svg.Descendants("{http://www.w3.org/2000/svg}circle")
        Dim circles As List(Of Shape) = New List(Of Shape)()
        Dim cx As Integer, cy As Integer, r As Integer
        For Each svgCircle As XElement In svgCircles
            cx = GetIntValue(svgCircle.Attribute("cx").Value)
            cy = GetIntValue(svgCircle.Attribute("cy").Value)
            r = GetIntValue(svgCircle.Attribute("r").Value)
            Dim circle As CircleShape = New CircleShape()
            circle.AnchorPoint = New Point(cx, cy)
            circle.Radius = r
            circle.Identifier = "circle" + i.ToString()
            circles.Add(circle)
            i = i + 1
        Next
        Return circles
    End Function

    Private Function GetRectangles(svg As XElement) As List(Of Shape)
        Dim i As Integer = 1
        Dim svgRectangles As IEnumerable(Of XElement) = svg.Descendants("{http://www.w3.org/2000/svg}rect")
        Dim rectangles As List(Of Shape) = New List(Of Shape)
        Dim x As Integer, y As Integer, width As Integer, height As Integer
        For Each svgRectangle As XElement In svgRectangles
            x = GetIntValue(svgRectangle.Attribute("x").Value)
            y = GetIntValue(svgRectangle.Attribute("y").Value)
            width = GetIntValue(svgRectangle.Attribute("width").Value)
            height = GetIntValue(svgRectangle.Attribute("height").Value)
            Dim rectangle As RectangleShape = New RectangleShape()
            rectangle.Identifier = "rectangle" + i.ToString()
            rectangle.TopLeft = New Point(x, y)
            rectangle.BottomRight = New Point(x + width, y + height)
            rectangles.Add(rectangle)
        Next
        Return rectangles
    End Function

    Private Function GetIntValue(value As String) As Integer
        Return CInt(value.Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
    End Function

    Private Sub AreaParameterEditorControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        LinkToResourceEditorControl()
        AddHandler Me.AreaEditor1.ShapeRemoved, AddressOf OnShapeRemoved
        AddHandler Me.AreaEditor1.ShapeAdded, AddressOf OnShapeAdded
    End Sub
End Class
