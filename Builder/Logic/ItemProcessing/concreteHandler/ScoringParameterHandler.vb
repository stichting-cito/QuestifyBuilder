Imports Cito.Tester.ContentModel
Imports Versioning

Namespace ItemProcessing

    Friend Class ScoringParameterHandler
        Inherits CollectionParameterHandler

        Friend Overrides Sub Merge(newParam As ParameterBase, currentParam As ParameterBase, warnErr As WarningsAndErrors)
            Dim newAreaPrm As ScoringParameter = DirectCast(newParam, ScoringParameter)
            Dim currAreaPrm As ScoringParameter = DirectCast(currentParam, ScoringParameter)

            MyBase.Merge(newAreaPrm, currAreaPrm, warnErr)

        End Sub

        Friend Overloads Overrides Function Compare(newParam As ParameterBase, currentParam As ParameterBase) As IEnumerable(Of MetaDataCompareResult)
            Return MyBase.Compare(newParam, currentParam)
        End Function
    End Class
End Namespace