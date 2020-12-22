Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Logic.Service.Exceptions


Public Class HotTextScoringParameterEditorControl


    Private ReadOnly _resourceEntity As EntityClasses.ResourceEntity
    Private _hotTextScoringParameter As HotTextScoringParameter
    Private ReadOnly _required As Boolean
    Private _xhtmlParamControl As XHtmlParameterEditorControl2
    Private _xhtmlParamControlHeight As Integer = -1
    Private _controlIsLoaded As Boolean




    Public Sub New(ByVal parent As ParameterSetsEditor, ByVal hotTextParameter As HotTextScoringParameter, ByVal resourceEntity As EntityClasses.ResourceEntity, ByVal resourceManager As ResourceManagerBase)

        InitializeComponent()

        Me.AddAttributeReferenceDrivenChangeHandlers(hotTextParameter, parent.ParameterSets)

        EditorParent = parent
        Me.HotTextScoringParameter = hotTextParameter
        Me.ResourceManager = resourceManager

        _resourceEntity = resourceEntity

        InitControls()

        Try
            Dim requiredSettingValue As String = _hotTextScoringParameter.DesignerSettings.GetSettingValueByKey("required")
            If Not String.IsNullOrEmpty(requiredSettingValue) Then _required = Boolean.Parse(requiredSettingValue)

        Catch ex As Exception
            Throw New AppLogicException(String.Format("Error parsing designer settings for parameter '{0}'.", _hotTextScoringParameter.Name))
        End Try
    End Sub




    Public Property HotTextScoringParameter() As HotTextScoringParameter
        Get
            Return _hotTextScoringParameter
        End Get
        Set(ByVal value As HotTextScoringParameter)
            _hotTextScoringParameter = value
            ParameterBindingSource.DataSource = _hotTextScoringParameter
        End Set
    End Property



    Public Overrides Function ResourceUsedInThisParameter(ByVal resource As EntityClasses.ResourceEntity) As Boolean
        Return False
    End Function

    Public Overrides Function ValidateParameter() As String
        Dim result As String = String.Empty
        If Enabled Then
            If _hotTextScoringParameter.Value.Count = 0 AndAlso _required Then
                result = MandatoryParameterMessage(_hotTextScoringParameter.DesignerSettings.GetSettingValueByKey("label"), _hotTextScoringParameter.DesignerSettings.GetSettingValueByKey("group"))
            End If
        End If
        Return result
    End Function

    Public Overrides Sub RemoveAllResources()
    End Sub



    Private Sub InitControls()

        MainTableLayoutPanel.SuspendLayout()

        Dim label As String = HotTextScoringParameter.HotTextText.DesignerSettings.GetSettingValueByKey("label")
        LabelHotText.Text = label

        _xhtmlParamControl = New XHtmlParameterEditorControl2(EditorParent, _hotTextScoringParameter.HotTextText, _resourceEntity, ResourceManager, False, EditorParent.ContextIdentifierForEditors)

        _xhtmlParamControl.Name = HotTextScoringParameter.Name
        _xhtmlParamControl.ParentTabEnabledContainerControl = Me

        _xhtmlParamControl.Margin = New Padding(3, 3, 3, 15)

        AddHandler _xhtmlParamControl.Resize, AddressOf HandleXhtmlEditorResize

        _xhtmlParamControl.Dock = DockStyle.Fill
        _xhtmlParamControl.Width = 200

        MainTableLayoutPanel.Controls.Add(_xhtmlParamControl, 0, 1)
        MainTableLayoutPanel.SetColumnSpan(_xhtmlParamControl, 2)

        MainTableLayoutPanel.ResumeLayout()

    End Sub

    Private Sub HandleXhtmlEditorResize(ByVal sender As Object, ByVal e As EventArgs)
        If Not _controlIsLoaded Then Exit Sub

        Dim xhtmlParamControlHeightDelta As Integer = _xhtmlParamControl.Height

        If _xhtmlParamControlHeight <> -1 Then
            xhtmlParamControlHeightDelta -= _xhtmlParamControlHeight

            Me.Height += xhtmlParamControlHeightDelta
        End If

        _xhtmlParamControlHeight = _xhtmlParamControl.Height
    End Sub


    Private Sub HotTextScoringParameterEditorControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _controlIsLoaded = True
    End Sub
End Class
