Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class BooleanParameterEditorControl


    Private _booleanParameter As Cito.Tester.ContentModel.BooleanParameter



    Public Property BooleanParameter() As Cito.Tester.ContentModel.BooleanParameter
        Get
            Return _booleanParameter
        End Get
        Set(ByVal value As Cito.Tester.ContentModel.BooleanParameter)
            _booleanParameter = value
            ParameterBindingSource.DataSource = value
        End Set
    End Property




    Public Sub New(ByVal parent As ParameterSetsEditor, ByVal p As Cito.Tester.ContentModel.BooleanParameter)
        InitializeComponent()

        Me.EditorParent = parent
        Me.BooleanParameter = p
    End Sub

    Public Overrides Function ResourceUsedInThisParameter(ByVal resource As ResourceEntity) As Boolean
        Return False
    End Function

    Public Overrides Function ValidateParameter() As String
        Dim result As String = String.Empty
        Return result
    End Function

    Public Overrides Sub RemoveAllResources()
    End Sub

    Public ReadOnly Property GetDesignerSettingValue(designerSettingKey As String) As String
        Get
            Return If(BooleanParameter.DesignerSettings.GetSettingValueByKey(designerSettingKey), String.Empty)
        End Get
    End Property



End Class
