Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.Chain

Namespace TestConstruction.ChainHandlers.Validating
    Public Class ItemDatasourceUsedElsewhereException
        Inherits ChainHandlerException

        Public Property ConflictingDatasource As String
        Public Property ConflictingItems As IEnumerable(Of ResourceRef)
        Public Property ConflictiongSection As TestSection2
        Public Property TargetSection As TestSection2

        Sub New(ds As String, conflictingItems As IEnumerable(Of ResourceRef),
        conflictSection As TestSection2,
        ByVal targetSection As TestSection2)
            MyBase.New(String.Format(My.Resources.ExceptionMessages.DataSourceUsedSomewhereElseMsg, conflictingItems.Count, ds, conflictSection.Title))
            Me.ConflictingDatasource = ds
            Me.ConflictingItems = conflictingItems
            Me.ConflictiongSection = conflictSection
            Me.TargetSection = targetSection
        End Sub

    End Class
End Namespace
