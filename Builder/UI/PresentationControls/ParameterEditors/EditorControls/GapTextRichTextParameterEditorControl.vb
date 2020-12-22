Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Logic.Service.Exceptions

Public Class GapTextRichTextParameterEditorControl

    Private ReadOnly _resourceEntity As EntityClasses.ResourceEntity
    Private _gapTextRichTextParameter As GapTextRichTextParameter
    Private _required As Boolean
    Private _xhtmlParamControl As XHtmlParameterEditorControl2
    Private _xhtmlParamControlHeight As Integer = -1
    Private _controlIsLoaded As Boolean


    Public Sub New(ByVal parameterSetsEditor As ParameterSetsEditor, ByVal gapTextRichTextParameter As GapTextRichTextParameter)

        InitializeComponent()

        EditorParent = parameterSetsEditor
        Me.GapTextRichTextParameter = gapTextRichTextParameter
        Me.ResourceManager = parameterSetsEditor.ResourceManager
        _resourceEntity = parameterSetsEditor.ResourceEntity

        InitControls()

        Try
            Dim requiredSettingValue As String = _gapTextRichTextParameter.DesignerSettings.GetSettingValueByKey("required")
            If Not String.IsNullOrEmpty(requiredSettingValue) Then _required = Boolean.Parse(requiredSettingValue)

        Catch ex As Exception
            Throw New AppLogicException(String.Format("Error parsing designer settings for parameter '{0}'.", _gapTextRichTextParameter.Name))
        End Try

    End Sub



    Public Property GapTextRichTextParameter() As GapTextRichTextParameter
        Get
            Return _gapTextRichTextParameter
        End Get
        Set(ByVal value As GapTextRichTextParameter)
            _gapTextRichTextParameter = value
            ParameterBindingSource.DataSource = _gapTextRichTextParameter
        End Set
    End Property



    Public Overrides Function ValidateParameter() As String
        Dim result As String = String.Empty
        If Me.Enabled Then
            If String.IsNullOrEmpty(_gapTextRichTextParameter.Value) AndAlso _required Then
                result = MandatoryParameterMessage(_gapTextRichTextParameter.DesignerSettings.GetSettingValueByKey("label"), _gapTextRichTextParameter.DesignerSettings.GetSettingValueByKey("group"))
            End If
        End If
        Return result
    End Function

    Public Overrides Sub RemoveAllResources()
        If TableLayoutPanel1.Controls.OfType(Of XHtmlParameterEditorControl2).Any() Then
            Dim xHtmlParameterEditor = TableLayoutPanel1.Controls.OfType(Of XHtmlParameterEditorControl2).First()
            xHtmlParameterEditor.RemoveAllResources()
        End If
    End Sub



    Private Sub InitControls()

        TableLayoutPanel1.SuspendLayout()

        _xhtmlParamControl = New XHtmlParameterEditorControl2(EditorParent, GapTextRichTextParameter,
                                                             _resourceEntity, ResourceManager,
                                                           False,
                                                           EditorParent.ContextIdentifierForEditors)

        _xhtmlParamControl.Name = GapTextRichTextParameter.Name
        _xhtmlParamControl.ParentTabEnabledContainerControl = Me
        _xhtmlParamControl.Margin = New Padding(3, 3, 3, 3)
        _xhtmlParamControl.Dock = DockStyle.Fill
        _xhtmlParamControl.Width = 200

        TableLayoutPanel1.Controls.Add(_xhtmlParamControl, 0, 0)
        TableLayoutPanel1.SetColumnSpan(_xhtmlParamControl, 2)

        AddHandler _xhtmlParamControl.Resize, AddressOf HandleXhtmlEditorResize

        TableLayoutPanel1.ResumeLayout()

    End Sub

    Private Sub HandleXhtmlEditorResize(ByVal sender As Object, ByVal e As EventArgs)
        If Not _controlIsLoaded Then Exit Sub

        Dim xhtmlParamControlHeightDelta As Integer = _xhtmlParamControl.Height

        If _xhtmlParamControlHeight <> -1 Then xhtmlParamControlHeightDelta -= _xhtmlParamControlHeight
        Me.Height += xhtmlParamControlHeightDelta

        _xhtmlParamControlHeight = _xhtmlParamControl.Height
    End Sub


    Private Sub HotTextScoringParameterEditorControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _controlIsLoaded = True
    End Sub

End Class
