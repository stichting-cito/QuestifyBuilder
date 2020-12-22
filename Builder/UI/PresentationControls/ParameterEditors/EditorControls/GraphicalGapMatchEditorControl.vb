Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class GraphicalGapMatchEditorControl : Inherits ParameterCollectionEditorControl

    Public Sub New(ByVal parent As ParameterSetsEditor, ByVal scoreparameter As GraphGapMatchScoringParameter, ByVal itemResource As ResourceEntity,
                   ByVal resourceManager As ResourceManagerBase, ByVal hasLoadedOldItemLayoutTemplate As Boolean, contextIdentifier As Nullable(Of Integer), additionalParameters As IEnumerable(Of ParameterBase))
        MyBase.New(parent, scoreparameter, itemResource, resourceManager, hasLoadedOldItemLayoutTemplate, contextIdentifier, additionalParameters)

        InitializeComponent()


    End Sub

    Protected Overrides Sub InitParameters(ByVal parameterPanel As TableLayoutPanel, ByVal enableLabelNumbering As Boolean)
        MyBase.InitParameters(parameterPanel, enableLabelNumbering)

        Me.SuspendLayout()

        Dim param = DirectCast(collectionParameter, GraphGapMatchScoringParameter).Area
        Dim paramUIControl = factory.CreateControl(param, _parent)

        Dim rowIndex As Integer = parameterLayoutPanel.RowCount - 1

        parameterLayoutPanel.RowCount += 1
        parameterLayoutPanel.RowStyles.Insert(rowIndex, New RowStyle(SizeType.AutoSize))

        Dim description As String = param.DesignerSettings.GetSettingValueByKey("description")
        Dim label As String = param.DesignerSettings.GetSettingValueByKey("label")

        Dim paramLabel As New Label() With {.Margin = New Padding(0, 3, 3, 3)}

        If Not String.IsNullOrEmpty(label) Then ParameterEditorHelper.AddToolTip(label, description, paramLabel)

        Dim paramSetId As String = "1"
        paramLabel.Tag = paramSetId
        paramLabel.AutoSize = True
        paramLabel.TextAlign = ContentAlignment.MiddleLeft
        paramLabel.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Right

        paramUIControl = factory.CreateControl(param, EditorParent)
        paramUIControl.Name = param.Name
        paramUIControl.Margin = New Padding(3, 3, 15, 3)
        paramUIControl.Dock = DockStyle.Fill
        paramUIControl.Tag = paramSetId
        paramUIControl.ParentTabEnabledContainerControl = Me

        AddHandler paramUIControl.AddingResource, AddressOf ParameterEditorAddingResource
        AddHandler paramUIControl.RemovingResource, AddressOf ParameterEditorRemovingResource

        parameterLayoutPanel.Controls.Add(paramLabel, 0, rowIndex)
        parameterLayoutPanel.Controls.Add(paramUIControl, 1, rowIndex)
        ParameterEditors.Add(paramUIControl, param)

        Me.ResumeLayout()
    End Sub


End Class
