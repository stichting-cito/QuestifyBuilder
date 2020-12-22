Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Exceptions
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class ListedParameterEditorControl
    Private _listedParameter As Cito.Tester.ContentModel.ListedParameter
    Private _required As Boolean

    Public Property ListedParameter() As Cito.Tester.ContentModel.ListedParameter
        Get
            Return _listedParameter
        End Get
        Set(ByVal value As Cito.Tester.ContentModel.ListedParameter)
            _listedParameter = value
            ParameterBindingSource.DataSource = value
            If Me.ParameterComboBox.DataBindings.Count = 0 Then
                Me.ParameterComboBox.DataBindings.Add(New Binding("SelectedValue", Me.ParameterBindingSource, "Value", True, DataSourceUpdateMode.OnPropertyChanged))
            End If
        End Set
    End Property


    Public Sub New(ByVal parent As ParameterSetsEditor, ByVal p As Cito.Tester.ContentModel.ListedParameter)

        InitializeComponent()

        Me.EditorParent = parent
        Me.ListedParameter = p

        Try
            Dim requiredSettingValue As String = _listedParameter.DesignerSettings.GetSettingValueByKey("required")
            If Not String.IsNullOrEmpty(requiredSettingValue) Then _required = Boolean.Parse(requiredSettingValue)

            Dim listValues As List(Of ListValue) = _listedParameter.DesignerSettings.GetListValuesByKey("list")
            Me.ListValuesBindingSource.DataSource = listValues
            Me.ParameterComboBox.DataSource = Me.ListValuesBindingSource.DataSource
            Me.ParameterComboBox.ValueMember = "Key"
            Me.ParameterComboBox.DisplayMember = "DisplayValue"
            ParameterComboBox.DisableScrollWheel()


        Catch ex As Exception
            Throw New AppLogicException(String.Format("Error parsing designer settings for parameter '{0}'.", _listedParameter.Name))
        End Try

    End Sub






    Public Overrides Function ResourceUsedInThisParameter(ByVal resource As ResourceEntity) As Boolean
        Return False
    End Function

    Public Overrides Function ValidateParameter() As String
        Dim result As String = String.Empty

        If String.IsNullOrEmpty(_listedParameter.Value) AndAlso _required AndAlso Me.Enabled Then
            result = MandatoryParameterMessage(_listedParameter.DesignerSettings.GetSettingValueByKey("label"), _listedParameter.DesignerSettings.GetSettingValueByKey("group"))
        End If

        Return result
    End Function

    Public Overrides Sub RemoveAllResources()
    End Sub



End Class
