Imports Questify.Builder.Model.ContentModel
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

Public Class XhtmlResourceParameterEditorControl
    Public Sub New(ByVal parent As ParameterSetsEditor, ByVal resourceParameter As Cito.Tester.ContentModel.ResourceParameter, ByVal resourceEntity As EntityClasses.ResourceEntity, resourceManager As ResourceManagerBase)
        MyBase.New(parent, resourceParameter, resourceEntity, resourceManager)

        InitializeComponent()

        ReferenceComboBox.DisableScrollWheel()

        AddHandler ParameterBindingSource.CurrentItemChanged, AddressOf ParameterBindingSource_CurrentItemChanged
    End Sub

    Protected Overrides Sub RefreshResourceData()
        MyBase.RefreshResourceData()
        Me.BindReferences()
    End Sub

    Private Sub ParameterBindingSource_CurrentItemChanged(ByVal sender As Object, ByVal e As EventArgs)
        ResourceParameter.RefreshResource()
        Me.BindReferences()
    End Sub

    Private Sub BindReferences()
        Dim references As New XhtmlReferenceList()
        references.Add(New XhtmlReference("none", XhtmlReferenceType.Element, My.Resources.EmptyReference, Nothing))

        If Me.ResourceParameter IsNot Nothing AndAlso Me.ResourceParameter.Resource IsNot Nothing Then
            references.AddRange(DirectCast(Me.ResourceParameter, XhtmlResourceParameter).References)
        End If

        XhtmlReferenceBindingSource.DataSource = references

        Me.TableLayoutPanel2.SuspendLayout()
        If references.Count > 1 Then
            Dim active As XhtmlReference = DirectCast(Me.ResourceParameter, XhtmlResourceParameter).ActiveReference
            If active Is Nothing Then
                ReferenceComboBox.SelectedIndex = 0
            Else
                ReferenceComboBox.SelectedValue = active.ID
            End If

            SetActiveReference()
        End If

        TableLayoutPanel2.Location = New Point(0, 0)
        ReferenceComboBox.Visible = False
        ReferenceLabel.Visible = False
        TableLayoutPanel2.Visible = False

        Me.TableLayoutPanel2.ResumeLayout()
    End Sub

    Private Sub ReferenceComboBox_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReferenceComboBox.SelectionChangeCommitted
        ParameterBindingSource.RaiseListChangedEvents = False
        SetActiveReference()
        ParameterBindingSource.RaiseListChangedEvents = True
    End Sub

    Private Sub SetActiveReference()
        Dim selected As XhtmlReference = DirectCast(ReferenceComboBox.SelectedItem, XhtmlReference)
        If selected.ID = "none" Then
            selected = Nothing
        End If
        DirectCast(Me.ResourceParameter, XhtmlResourceParameter).ActiveReference = selected
    End Sub

    Private Sub XhtmlResourceParameterEditorControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        ForceShowDimensions = False
        MyBase.InitControls()
    End Sub
End Class
