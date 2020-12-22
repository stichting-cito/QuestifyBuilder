Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses

Public Class ScoreEditorControlBase


    Private _resourceManager As ResourceManagerBase
    Private _assessmentItem As Cito.Tester.ContentModel.AssessmentItem
    Private _itemResource As ItemResourceEntity



    Public Property ResourceManager() As ResourceManagerBase
        Get
            Return _resourceManager
        End Get
        Set(ByVal value As ResourceManagerBase)
            _resourceManager = value
        End Set
    End Property

    Public Property AssessmentItem() As Cito.Tester.ContentModel.AssessmentItem
        Get
            Return _assessmentItem
        End Get
        Set(ByVal value As Cito.Tester.ContentModel.AssessmentItem)
            _assessmentItem = value
            AssessmentItemBindingSource.DataSource = _assessmentItem
        End Set
    End Property

    Public Property ItemResource() As ItemResourceEntity
        Get
            Return _itemResource
        End Get
        Set(ByVal value As ItemResourceEntity)
            _itemResource = value
        End Set
    End Property

    Public Overridable Function IsResourceUsedInItem(ByVal resource As ResourceEntity) As Boolean
        Return False
    End Function



    Public Overridable Sub UpdateScoreEditor()
        Throw New NotImplementedException("This function is not implemented!")
    End Sub



    Public Sub New()
        InitializeComponent()

    End Sub



End Class
