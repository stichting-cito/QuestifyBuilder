Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class ChoicesParameterEditorControl


    Private _choicesParameter As Cito.Tester.ContentModel.ChoiceCollectionParameter
    Private _minimumAlternatives As Integer
    Private _maximumAlternatives As Integer



    Public Property ChoicesParameter() As Cito.Tester.ContentModel.ChoiceCollectionParameter
        Get
            Return _choicesParameter
        End Get
        Set(ByVal value As Cito.Tester.ContentModel.ChoiceCollectionParameter)
            _choicesParameter = value
            ChoicesBindingSource.DataMember = "Choices"

            ParameterBindingSource.DataSource = _choicesParameter
        End Set
    End Property




    Public Sub New(ByVal parent As ParameterSetsEditor, ByVal choicesParameter As Cito.Tester.ContentModel.ChoiceCollectionParameter)
        InitializeComponent()

        If choicesParameter Is Nothing Then
            Throw New ArgumentNullException("choicesParameter")
        End If

        If parent Is Nothing Then
            Throw New ArgumentNullException("parent")
        End If

        Try
            Dim minimumAlternativesSettingValue As String = choicesParameter.DesignerSettings.GetSettingValueByKey("minimumAlternatives")
            If Not String.IsNullOrEmpty(minimumAlternativesSettingValue) Then _minimumAlternatives = Integer.Parse(minimumAlternativesSettingValue) Else _minimumAlternatives = 2

            Dim maximumAlternativesSettingValue As String = choicesParameter.DesignerSettings.GetSettingValueByKey("maximumAlternatives")
            If Not String.IsNullOrEmpty(maximumAlternativesSettingValue) Then _maximumAlternatives = Integer.Parse(maximumAlternativesSettingValue) Else _maximumAlternatives = 8
        Catch ex As Exception
            Throw New AppLogicException(String.Format("Error parsing designer settings for parameter '{0}'.", choicesParameter.Name))
        End Try

        For altNr As Integer = _minimumAlternatives To _maximumAlternatives
            NrOfAlternativesComboBox.Items.Add(altNr)
        Next

        Me.EditorParent = parent
        Me.ChoicesParameter = choicesParameter
        NrOfAlternativesComboBox.DisableScrollWheel()

    End Sub



    Public Overrides Function ResourceUsedInThisParameter(ByVal resource As ResourceEntity) As Boolean
        Return False
    End Function

    Public Overrides Function ValidateParameter() As String
        Dim result As String = String.Empty

        For Each choice As SimpleChoice In _choicesParameter.Choices
            If String.IsNullOrEmpty(choice.Value) Then
                result = My.Resources.ChoicesParameterEditorControl_ValidateParameter_NotAllValuesFilled
            End If
        Next

        Return result
    End Function

    Public Overrides Sub RemoveAllResources()
    End Sub




    Private Sub ParameterBindingSource_DataSourceChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ParameterBindingSource.DataSourceChanged
        If _choicesParameter IsNot Nothing Then
            If _choicesParameter.Choices.Count > 0 Then
                NrOfAlternativesComboBox.SelectedItem = _choicesParameter.Choices.Count
            Else
                NrOfAlternativesComboBox.SelectedItem = _minimumAlternatives
            End If
        End If
    End Sub

    Private Sub NrOfAlternativesComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NrOfAlternativesComboBox.SelectedIndexChanged
        Dim currentNrOfAlternatives As Integer = _choicesParameter.Choices.Count
        Dim selectedNrOfAlternatives As Integer = Int32.Parse(NrOfAlternativesComboBox.SelectedItem.ToString())

        If currentNrOfAlternatives < selectedNrOfAlternatives Then
            For index As Integer = currentNrOfAlternatives + 1 To selectedNrOfAlternatives
                Dim newChoice As New Cito.Tester.ContentModel.SimpleChoice()
                newChoice.Identifier = Convert.ToChar(64 + index).ToString()
                _choicesParameter.Choices.Add(newChoice)
            Next
        ElseIf currentNrOfAlternatives > selectedNrOfAlternatives Then
            Dim result As DialogResult = MessageBox.Show(My.Resources.ChoicesParameterEditorControl_NrOfAlternativesComboBox_SelectedIndexChanged_ValuesLostWarning, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                For index As Integer = currentNrOfAlternatives - 1 To selectedNrOfAlternatives Step -1
                    _choicesParameter.Choices.RemoveAt(_choicesParameter.Choices.Count - 1)
                Next
            Else
                NrOfAlternativesComboBox.SelectedItem = currentNrOfAlternatives
            End If
        End If

        If Not (Me.EditorParent Is Nothing) Then Me.EditorParent.ValidateThisEditor(Me)
    End Sub

    Private Sub ChoicesBindingSource_CurrentItemChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChoicesBindingSource.CurrentItemChanged
        If Not (Me.EditorParent Is Nothing) Then Me.EditorParent.ValidateThisEditor(Me)
    End Sub


End Class
