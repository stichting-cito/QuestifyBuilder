Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class AddCustomPropertyFormDialog

    Private _entity As CommonEntityBase
    Private _customBankPropertyEntity As CustomBankPropertyEntity
    Private _isDirty As Boolean

    Public Sub New()
        InitializeComponent()

    End Sub

    Public Sub New(entity As CommonEntityBase, customBankProperty As CustomBankPropertyEntity, Optional ByVal conceptTypes As List(Of KeyValuePair(Of ConceptTypeEntity, String)) = Nothing)
        Me.New()

        _entity = entity
        _customBankPropertyEntity = customBankProperty

        If conceptTypes IsNot Nothing Then
            BindComboBoxTypes(conceptTypes)
        Else
            ComboBoxTypes.Visible = False
            LabelType.Visible = False
        End If

        AddHandler ComboBoxTypes.SelectedIndexChanged, Sub()
                                                           _isDirty = True
                                                       End Sub

        AddHandler Me.FormClosing, AddressOf AddCustomPropertyFormDialog_FormClosing

        SetWindowTitle()
    End Sub

    Private Sub SetWindowTitle()
        If TypeOf _entity Is ListValueCustomBankPropertyEntity Then
            Me.Text = My.Resources.AddListValueWindowTitle
        ElseIf TypeOf _entity Is ConceptStructurePartCustomBankPropertyEntity Then
            Me.Text = My.Resources.AddConceptValueWindowTitle
        ElseIf TypeOf _entity Is TreeStructurePartCustomBankPropertyEntity Then
            Me.Text = My.Resources.AddTreeValueWindowTitle
        End If
    End Sub

    Private Sub AddCustomPropertyFormDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler TextBoxName.TextChanged, Sub()
                                                _isDirty = True
                                            End Sub
        AddHandler TextBoxTitle.TextChanged, Sub()
                                                 _isDirty = True
                                             End Sub
    End Sub

    Private Sub BindComboBoxTypes(ByVal conceptTypes As List(Of KeyValuePair(Of ConceptTypeEntity, String)))
        ComboBoxTypes.ValueMember = "Key"
        ComboBoxTypes.DisplayMember = "Value"

        ComboBoxTypes.DataSource = New BindingSource(conceptTypes, Nothing)

        If conceptTypes.Count > 0 Then
            ComboBoxTypes.SelectedIndex = 0
        End If
    End Sub

    Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click
        Save()
    End Sub

    Private Function Save() As Boolean
        If Not ValidateChildren() Then
            Return False
        End If

        RemoveHandler Me.FormClosing, AddressOf AddCustomPropertyFormDialog_FormClosing

        DialogResult = Windows.Forms.DialogResult.OK

        If TypeOf _entity Is ListValueCustomBankPropertyEntity Then
            With CType(_entity, ListValueCustomBankPropertyEntity)
                .Code = Guid.NewGuid()
                .Title = TextBoxTitle.Text
                .Name = TextBoxName.Text
            End With

            Return True
        ElseIf TypeOf _entity Is ConceptStructurePartCustomBankPropertyEntity Then
            With CType(_entity, ConceptStructurePartCustomBankPropertyEntity)
                .Code = Guid.NewGuid()
                .Title = TextBoxTitle.Text
                .Name = TextBoxName.Text
                .ConceptTypeId = CType(ComboBoxTypes.SelectedValue, ConceptTypeEntity).ConceptTypeId
            End With

            Return True
        ElseIf TypeOf _entity Is TreeStructurePartCustomBankPropertyEntity Then
            With CType(_entity, TreeStructurePartCustomBankPropertyEntity)
                .Code = Guid.NewGuid()
                .Title = TextBoxTitle.Text
                .Name = TextBoxName.Text
            End With

            Return True
        Else
            Throw New NotSupportedException("This type is not spupported.")
        End If
    End Function

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        Close()
    End Sub

    Private Sub AddCustomPropertyFormDialog_FormClosing(sender As Object, e As FormClosingEventArgs)
        e.Cancel = HandleClose()
    End Sub

    Private Function HandleClose() As Boolean
        DialogResult = Windows.Forms.DialogResult.Cancel

        If _isDirty Then
            Dim result As DialogResult = MessageBox.Show(My.Resources.Editor_Dialog_SaveChangesQuestionMessage, String.Empty, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If result = Windows.Forms.DialogResult.Yes Then
                Return Not Save()
            ElseIf result = Windows.Forms.DialogResult.Cancel Then
                Return True
            End If
        End If

        Return False
    End Function

    Private Function IsInputUnique(ByVal newName As String) As Boolean
        If TypeOf _entity Is TreeStructurePartCustomBankPropertyEntity Then
            Dim entity As TreeStructurePartCustomBankPropertyEntity = CType(_entity, TreeStructurePartCustomBankPropertyEntity)

            If TypeOf _customBankPropertyEntity Is TreeStructureCustomBankPropertyEntity Then
                Dim treeStructureCustomBankPropertyEntity As TreeStructureCustomBankPropertyEntity = CType(_customBankPropertyEntity, TreeStructureCustomBankPropertyEntity)

                For Each treeStructurePartCustomBankPropertyEntity As TreeStructurePartCustomBankPropertyEntity In treeStructureCustomBankPropertyEntity.TreeStructurePartCustomBankPropertyCollection
                    If treeStructurePartCustomBankPropertyEntity.Name = newName Then
                        Return False
                    ElseIf (treeStructurePartCustomBankPropertyEntity.Name = newName.Replace("_", ".")) Then
                        Return False
                    ElseIf (treeStructurePartCustomBankPropertyEntity.Name = newName.Replace(".", "_")) Then
                        Return False
                    End If
                Next
            End If
        ElseIf TypeOf _entity Is ConceptStructurePartCustomBankPropertyEntity Then
            Dim entity As ConceptStructurePartCustomBankPropertyEntity = CType(_entity, ConceptStructurePartCustomBankPropertyEntity)

            If TypeOf _customBankPropertyEntity Is ConceptStructureCustomBankPropertyEntity Then
                Dim conceptStructureCustomBankPropertyEntity As ConceptStructureCustomBankPropertyEntity = CType(_customBankPropertyEntity, ConceptStructureCustomBankPropertyEntity)

                For Each conceptStructurePartCustomBankPropertyEntity As ConceptStructurePartCustomBankPropertyEntity In conceptStructureCustomBankPropertyEntity.ConceptStructurePartCustomBankPropertyCollection
                    If conceptStructurePartCustomBankPropertyEntity.Name = newName Then
                        Return False
                    End If
                Next
            End If
        ElseIf TypeOf _entity Is ListValueCustomBankPropertyEntity Then
            Dim entity As ListValueCustomBankPropertyEntity = CType(_entity, ListValueCustomBankPropertyEntity)

            If TypeOf _customBankPropertyEntity Is ListCustomBankPropertyEntity Then
                Dim listCustomBankPropertyEntity As ListCustomBankPropertyEntity = CType(_customBankPropertyEntity, ListCustomBankPropertyEntity)

                For Each ListValueCustomBankPropertyEntity As ListValueCustomBankPropertyEntity In listCustomBankPropertyEntity.ListValueCustomBankPropertyCollection
                    If ListValueCustomBankPropertyEntity.Name = newName Then
                        Return False
                    End If
                Next
            End If
        End If

        Return True
    End Function

    Private Sub TextBoxName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBoxName.Validating
        CodeErrorProvider.Clear()

        If String.IsNullOrEmpty(TextBoxName.Text) Then
            CodeErrorProvider.SetError(TextBoxName, My.Resources.CodeFieldCannotBeEmpty)
            e.Cancel = True
        End If

        If Not IsInputUnique(TextBoxName.Text) Then
            CodeErrorProvider.SetError(TextBoxName, My.Resources.CodeFieldNotUnique)
            e.Cancel = True
        End If

        If e.Cancel Then
            TextBoxName.Focus()
        End If
    End Sub

    Private Sub TextBoxTitle_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBoxTitle.Validating
        TitleErrorProvider.Clear()

        If String.IsNullOrEmpty(TextBoxTitle.Text) Then
            TitleErrorProvider.SetError(TextBoxTitle, My.Resources.TitleFieldCannotBeEmpty)
            e.Cancel = True
        End If
    End Sub
End Class