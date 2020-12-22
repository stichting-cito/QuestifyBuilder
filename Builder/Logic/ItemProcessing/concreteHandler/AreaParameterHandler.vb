Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Versioning

Namespace ItemProcessing

    Friend Class AreaParameterHandler
        Inherits CollectionParameterHandler


        Friend Overrides Sub Merge(newParam As ParameterBase, currentParam As ParameterBase, warnErr As WarningsAndErrors)
            Dim newAreaPrm As AreaParameter = DirectCast(newParam, AreaParameter)
            Dim currAreaPrm As AreaParameter = DirectCast(currentParam, AreaParameter)

            MyBase.Merge(newAreaPrm, currAreaPrm, warnErr)

            newAreaPrm.ShapeList.Clear()
            For Each e As Shape In currAreaPrm.ShapeList
                If (e IsNot Nothing) Then newAreaPrm.ShapeList.Add(e)
            Next

        End Sub

        Friend Overrides Function Compare(newParam As ParameterBase, currentParam As ParameterBase) As IEnumerable(Of MetaDataCompareResult)
            Dim result As New List(Of MetaDataCompareResult)()
            Dim newAreaPrm As AreaParameter = DirectCast(newParam, AreaParameter)
            Dim currAreaPrm As AreaParameter = DirectCast(currentParam, AreaParameter)

            result.AddRange(MyBase.Compare(newParam, currentParam))

            If newParam Is Nothing Then
                result.Add(New MetaDataCompareResult(currentParam.Name, Nothing, HelperClasses.ParameterHelper.GetValue(currentParam), String.Empty, My.Resources.Category_AssessmentItem, Nothing))
            ElseIf currentParam Is Nothing Then
                result.Add(New MetaDataCompareResult(newParam.Name, Nothing, String.Empty, HelperClasses.ParameterHelper.GetValue(newParam), My.Resources.Category_AssessmentItem, Nothing))
            Else
                For Each shape As Shape In currAreaPrm.ShapeList
                    If newAreaPrm.ShapeList.FirstOrDefault(Function(i) i.Identifier = shape.Identifier) Is Nothing Then
                        result.Add(New MetaDataCompareResult(newParam.Name, Nothing, String.Empty, shape.Identifier, My.Resources.Category_AssessmentItem, Nothing))
                    End If
                Next
            End If

            Return result
        End Function
    End Class

End Namespace
