Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Versioning

Namespace ItemProcessing

    Friend Class ParameterCollectionHandler

        Public Sub Merge(ByVal newParameters As ParameterCollection, ByVal currentParameters As ParameterCollection, ByVal warnErr As WarningsAndErrors)
            For Each currentParam As ParameterBase In currentParameters.InnerParameters
                Dim newParam As ParameterBase = DirectCast(newParameters.GetParameterByName(currentParam.Name, False), ParameterBase)

                If newParam Is Nothing Then

                    warnErr.WarningList.Add(String.Format(My.Resources.ItemProcessing.ValidateParametersParameterWillBeDeleted, currentParam.Name))

                ElseIf currentParam.GetType() IsNot newParam.GetType() Then
                    If currentParam.Nodes IsNot Nothing AndAlso currentParam.Nodes.Length > 0 Then
                        warnErr.ErrorList.Add(String.Format(My.Resources.ItemProcessing.ValidateParametersParameterHasADifferentType, currentParam.Name))
                    End If
                Else
                    ParameterHandler.GetConcreteMerger(newParam.GetType()).Merge(newParam, currentParam, warnErr)
                End If
            Next
        End Sub

        Public Function Compare(ByVal newParameters As ParameterCollection, ByVal currentParameters As ParameterCollection) As IEnumerable(Of MetaDataCompareResult)
            Dim result As New List(Of MetaDataCompareResult)()

            For Each currentParam As ParameterBase In currentParameters.InnerParameters
                Dim newParam As ParameterBase = DirectCast(newParameters.GetParameterByName(currentParam.Name, False), ParameterBase)

                If newParam Is Nothing Then
                    result.AddRange(ParameterHandler.GetConcreteMerger(currentParam.GetType()).Compare(newParam, currentParam))
                ElseIf currentParam Is Nothing Then
                    result.AddRange(ParameterHandler.GetConcreteMerger(newParam.GetType()).Compare(newParam, currentParam))
                ElseIf currentParam Is Nothing AndAlso newParam Is Nothing Then
                    Throw New ArgumentException("Unable to determine type. This is needed to get the right merger.")
                ElseIf currentParam.GetType() IsNot newParam.GetType() Then
                    If currentParam.Nodes IsNot Nothing AndAlso currentParam.Nodes.Length > 0 Then
                        result.Add(New MetaDataCompareResult(currentParam.Name, Nothing,
             $"Type mismatch: '{currentParam.GetType().Name}'",
             $"Type mismatch: '{newParam.GetType().Name}'", Nothing, Nothing))
                    End If
                Else
                    result.AddRange(ParameterHandler.GetConcreteMerger(currentParam.GetType()).Compare(newParam, currentParam))
                End If
            Next

            For Each newParam As ParameterBase In newParameters.InnerParameters.Where(Function(prm) Not currentParameters.InnerParameters.Any(Function(newPrm) newPrm.Name.Equals(prm.Name)))
                result.AddRange(ParameterHandler.GetConcreteMerger(newParam.GetType()).Compare(newParam, Nothing))
            Next

            Return result
        End Function

    End Class

End Namespace

