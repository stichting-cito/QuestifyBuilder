Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Chain
Namespace TestConstruction.ChainHandlers.Validating
    Public Class SuggestDatasourceBindingException
        Inherits ChainHandlerException



        Sub New(ByVal bindingToDS As String,
        ByVal itemsOfDataSource As IEnumerable(Of Datasources.ResourceRef),
        ByVal targetSection As TestSection2)
            MyBase.New(String.Format(My.Resources.ExceptionMessages.SuggesDataSourceBindingMsg, bindingToDS))
            Me.SetBindingToDatasource = bindingToDS
            Me.ItemsOfDatasource = itemsOfDataSource
            Me.TargetSection = targetSection


        End Sub


        Sub New(bindingToDS As String,
    ByVal itemsOfDataSource As IEnumerable(Of Datasources.ResourceRef),
    ByVal targetSection As TestSection2,
    ByVal suggestedTargetSection As String)
            MyBase.New(String.Format(My.Resources.ExceptionMessages.SuggesDataSourceBindingNewSectionMsg, bindingToDS))
            Me.SetBindingToDatasource = bindingToDS
            Me.ItemsOfDatasource = itemsOfDataSource
            Me.TargetSection = targetSection
            Me.SuggestedTargetSection = suggestedTargetSection
        End Sub


        Public ReadOnly Property IsSuggestingNestedSection As Boolean
            Get
                Return Not String.IsNullOrEmpty(SuggestedTargetSection)
            End Get
        End Property

        Public Property SetBindingToDatasource As String
        Public Property ItemsOfDatasource As IEnumerable(Of Datasources.ResourceRef)
        Public Property ConflictingSection As TestSection2
        Public Property TargetSection As TestSection2
        Public Property SuggestedTargetSection As String


    End Class
End Namespace
