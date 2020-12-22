Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class IntegerParameterEditorControl


    Private _integerParameter As Cito.Tester.ContentModel.IntegerParameter
    Private _rangeFrom As Nullable(Of Integer)
    Private _rangeTo As Nullable(Of Integer)



    Public Property IntegerParameter() As Cito.Tester.ContentModel.IntegerParameter
        Get
            Return _integerParameter
        End Get
        Set(ByVal value As Cito.Tester.ContentModel.IntegerParameter)
            _integerParameter = value
            ParameterBindingSource.DataSource = value
        End Set
    End Property




    Public Sub New(ByVal parent As ParameterSetsEditor, ByVal p As Cito.Tester.ContentModel.IntegerParameter)
        InitializeComponent()

        Me.EditorParent = parent
        Me.IntegerParameter = p

        Try
            Dim rangeFromSettingValue As String = _integerParameter.DesignerSettings.GetSettingValueByKey("rangeFrom")
            If Not String.IsNullOrEmpty(rangeFromSettingValue) Then _rangeFrom = Integer.Parse(rangeFromSettingValue)

            Dim rangeToSettingValue As String = _integerParameter.DesignerSettings.GetSettingValueByKey("rangeTo")
            If Not String.IsNullOrEmpty(rangeToSettingValue) Then _rangeTo = Integer.Parse(rangeToSettingValue)
        Catch ex As Exception
            Throw New AppLogicException(String.Format("Error parsing designer settings for parameter '{0}'.", _integerParameter.Name))
        End Try

    End Sub



    Public Overrides Function ResourceUsedInThisParameter(ByVal resource As ResourceEntity) As Boolean
        Return False
    End Function

    Public Overrides Function ValidateParameter() As String
        Dim result As String = String.Empty

        If _rangeFrom.HasValue AndAlso _rangeTo.HasValue AndAlso (_integerParameter.Value < _rangeFrom.Value OrElse _integerParameter.Value > _rangeTo.Value) Then
            result = String.Format(My.Resources.OnlyAValueWithinTheRangeOfTillIsAllowed, _rangeFrom.Value, _rangeTo.Value)
        ElseIf _rangeFrom.HasValue AndAlso _integerParameter.Value < _rangeFrom.Value Then
            result = String.Format(My.Resources.TheValueMustBeHigherThen, _rangeFrom.Value)
        ElseIf _rangeTo.HasValue AndAlso _integerParameter.Value > _rangeTo.Value Then
            result = String.Format(My.Resources.TheValueMustBeLowerThen, _rangeTo.Value)
        End If

        Return result
    End Function

    Public Overrides Sub RemoveAllResources()
    End Sub



    Private Sub InputTextBox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles InputTextBox.Validating
        If String.IsNullOrEmpty(Me.InputTextBox.Text) Then
            Me.InputTextBox.Text = "0"
        End If

        Dim dummy As Integer = 0
        If Not Integer.TryParse(Me.InputTextBox.Text, dummy) Then
            MessageBox.Show(My.Resources.OnlyNumericValueIsValid, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Me.InputTextBox.Text = "0"
        End If
    End Sub


End Class
