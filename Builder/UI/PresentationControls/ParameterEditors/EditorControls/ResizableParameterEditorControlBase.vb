Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public MustInherit Class ResizableParameterEditorControlBase(Of TParameter As ResizableParameter)
    Inherits ParameterEditorControlBase

    Private _enableEditDimensions As Boolean = True

    Protected _sizeChanging As Boolean = False
    Protected _required As Boolean
    Protected _filter As String
    Protected _resourceEntity As ResourceEntity
    Protected _contextIdentifier As Nullable(Of Integer)
    Protected _parameter As TParameter

    Public Event ImageSizeChanged As EventHandler(Of EventArgs)

    Protected Sub OnImageSizeChanged(sender As Object, args As EventArgs)
        RaiseEvent ImageSizeChanged(sender, args)
    End Sub

    Private Event _ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)

    Public Custom Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)
        AddHandler(value As EventHandler(Of ResourceNeededEventArgs))
            AddHandler _ResourceNeeded, value
            Debug.Assert(_parameter IsNot Nothing)
            RefreshResourceData()

        End AddHandler

        RemoveHandler(value As EventHandler(Of ResourceNeededEventArgs))
            RemoveHandler _ResourceNeeded, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As ResourceNeededEventArgs)
            RaiseEvent _ResourceNeeded(sender, e)
        End RaiseEvent
    End Event

    Protected Overridable Sub OnResourceNeeded(ByVal sender As Object, ByVal e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(sender, e)
    End Sub

    Public Event EditResource As EventHandler(Of ResourceNameEventArgs)

    Protected Overridable Sub OnEditResource(ByVal e As ResourceNameEventArgs)
        RaiseEvent EditResource(Me, e)
    End Sub

    Public Property WidthValue As Integer? = Nothing
    Public Property HeightValue As Integer? = Nothing
    Public Property ForceShowDimensions As Boolean = True
    Public Property EnableEditDimensions As Boolean
        Get
            Return _enableEditDimensions
        End Get
        Set
            _enableEditDimensions = Value
            DimensionEditing(Value)
        End Set
    End Property

    Protected MustOverride Sub DimensionEditing(enable As Boolean)

    Protected Overridable Sub RefreshResourceData()
        Dim prm As ResourceParameter = TryCast(_parameter, ResourceParameter)
        If (prm IsNot Nothing) Then
            prm.RefreshResource()
        End If
    End Sub

    Protected Function GetTableLayout(c As Control) As TableLayoutPanel
        While c.Parent IsNot Nothing
            Dim tableLayoutPanel = TryCast(c.Parent, TableLayoutPanel)
            If (tableLayoutPanel IsNot Nothing) Then
                Return tableLayoutPanel
            End If
        End While
        Return Nothing
    End Function

    Protected Sub HideParameterControls(c As Control)
        If c IsNot Nothing Then
            Dim t = GetTableLayout(c)
            If t IsNot Nothing Then
                Dim row = t.GetRow(c)
                t.RowStyles(row).SizeType = SizeType.Absolute
                t.RowStyles(row).Height = 0
            End If
        End If
    End Sub

    Protected Sub Initialize(parameterSetsEditor As ParameterSetsEditor, resourceMngr As ResourceManagerBase, resourceEntity As ResourceEntity)
        EditorParent = parameterSetsEditor
        ResourceManager = resourceMngr
        _resourceEntity = resourceEntity

        Try
            Dim requiredSettingValue As String = _parameter.DesignerSettings.GetSettingValueByKey("required")
            If Not String.IsNullOrEmpty(requiredSettingValue) Then _required = Boolean.Parse(requiredSettingValue)

            Dim filterSettingValue As String = _parameter.DesignerSettings.GetSettingValueByKey("filter")
            If Not String.IsNullOrEmpty(filterSettingValue) Then _filter = filterSettingValue Else _filter = String.Empty
        Catch ex As Exception
            Throw New AppLogicException($"Error parsing designer settings for parameter '{_parameter.Name}'.")
        End Try
    End Sub

    Protected Function IsSizeChanged(heightTbValue As Decimal, widthTbValue As Decimal) As Boolean
        Return Not (Not HeightValue.HasValue OrElse Not WidthValue.HasValue _
                    OrElse (HeightValue.Value = heightTbValue AndAlso WidthValue.Value = widthTbValue))
    End Function

    Protected Sub HandleWidthTextBoxChanged(keepAspectRatio As Boolean, ByRef heightTextBox As NumericUpDown, ByRef widthTextBox As NumericUpDown)
        If _sizeChanging Then Return

        _sizeChanging = True
        If keepAspectRatio Then
            heightTextBox.Value = GetHeightFromWidth(CInt(widthTextBox.Value))
        End If
        RaiseEvent ImageSizeChanged(Me, New EventArgs)
        _parameter.EditSize = IsSizeChanged(heightTextBox.Value, widthTextBox.Value)
        _parameter.Width = CInt(widthTextBox.Value)
        _parameter.Height = CInt(heightTextBox.Value)
        _sizeChanging = False
    End Sub

    Protected Sub HandleHeightTextBoxChanged(keepAspectRatio As Boolean, ByRef heightTextBox As NumericUpDown, ByRef widthTextBox As NumericUpDown)
        If _sizeChanging Then Return

        _sizeChanging = True
        If keepAspectRatio Then
            widthTextBox.Value = GetWidthFromHeight(CInt(heightTextBox.Value))
        End If
        RaiseEvent ImageSizeChanged(Me, New EventArgs)
        _parameter.EditSize = IsSizeChanged(heightTextBox.Value, widthTextBox.Value)
        _parameter.Width = CInt(widthTextBox.Value)
        _parameter.Height = CInt(heightTextBox.Value)
        _sizeChanging = False
    End Sub

    Protected Function GetHeightFromWidth(w As Integer) As Integer
        Dim newHeightValue = 0
        If WidthValue.HasValue AndAlso HeightValue.HasValue AndAlso w <> 0 Then
            newHeightValue = GetNewDimension(New Size(WidthValue.Value, HeightValue.Value), w, HeightValue.Value > WidthValue.Value)
        End If
        Return newHeightValue
    End Function

    Protected Function GetWidthFromHeight(h As Integer) As Integer
        Dim newWidthValue = 0
        If WidthValue.HasValue AndAlso HeightValue.HasValue AndAlso h <> 0 Then
            newWidthValue = GetNewDimension(New Size(WidthValue.Value, HeightValue.Value), h, WidthValue.Value > HeightValue.Value)
        End If
        Return newWidthValue
    End Function

    Protected Function GetNewDimension(orgSize As Size, newDimensionValue As Integer, multiply As Boolean) As Integer
        Dim aspect As Double = CDbl(orgSize.Width) / CDbl(orgSize.Height)
        If CDbl(orgSize.Height) > CDbl(orgSize.Width) Then
            aspect = CDbl(orgSize.Height) / CDbl(orgSize.Width)
        End If
        If aspect > 0 Then
            If multiply Then
                Return Convert.ToInt32(newDimensionValue * aspect)
            Else
                Return Convert.ToInt32(newDimensionValue / aspect)
            End If
        End If
    End Function

    Public Sub SetDimensionVisibility(ByRef dimensionsPanel As Panel, heightEnabled As Integer, heightDisabled As Integer)
        Dim enable = ForceShowDimensions
        dimensionsPanel.Visible = enable
        If enable Then
            Height = heightEnabled
        Else
            Height = heightDisabled
        End If
    End Sub

    Protected Sub SetDimensionValues(heightTb As NumericUpDown, widthTb As NumericUpDown)
        If Not _parameter.Width = 0 AndAlso Not _parameter.Height = 0 Then
            _sizeChanging = True
            widthTb.Value = _parameter.Width
            heightTb.Value = _parameter.Height
            _sizeChanging = False
        ElseIf WidthValue.HasValue AndAlso HeightValue.HasValue Then
            _sizeChanging = True
            widthTb.Value = WidthValue.Value
            heightTb.Value = HeightValue.Value
            _sizeChanging = False
        End If
    End Sub
End Class
