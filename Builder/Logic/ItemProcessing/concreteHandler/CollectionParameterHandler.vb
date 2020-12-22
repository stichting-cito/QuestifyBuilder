Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Versioning

Namespace ItemProcessing
    Friend Class CollectionParameterHandler
        Inherits ParameterHandler

        Friend Overrides Sub Merge(newParam As ParameterBase, currentParam As ParameterBase, warnErr As WarningsAndErrors)

            Dim newParamCollection As CollectionParameter = DirectCast(newParam, CollectionParameter)
            Dim dummySetNew As New ParameterSetCollection()
            Dim currentParamCollection As CollectionParameter = DirectCast(currentParam, CollectionParameter)
            Dim dummySetcurrent As New ParameterSetCollection()

            If newParamCollection.BluePrint IsNot Nothing Then
                dummySetNew.Add(newParamCollection.BluePrint)
            End If

            If currentParamCollection.BluePrint IsNot Nothing Then
                dummySetcurrent.Add(currentParamCollection.BluePrint)
            End If

            warnErr.SubWarningsAndErrors = ParameterHandler.Merge(dummySetNew, dummySetcurrent)

            If Not warnErr.Errors Then
                Dim maxLength As Integer
                If Not Integer.TryParse(newParamCollection.DesignerSettings.GetSettingValueByKey("maximumLength"), maxLength) Then
                    maxLength = Integer.MaxValue
                End If

                If maxLength < currentParamCollection.Value.Count Then
                    warnErr.WarningList.Add(String.Format(My.Resources.ItemProcessing.ValidateParametersCollectionParameterWillShrink, currentParam.Name, currentParamCollection.Value.Count, maxLength))
                End If
                AddParams(newParamCollection, currentParamCollection, maxLength)

                ParameterHandler.Merge(newParamCollection.Value, currentParamCollection.Value)
            End If
        End Sub

        Private Sub AddParams(newParamCollection As CollectionParameter, currentParamCollection As CollectionParameter, maxLength As Integer)

            For index As Integer = 0 To Math.Min(maxLength, currentParamCollection.Value.Count) - 1
                Dim newSub As ParameterCollection = ParameterCollection.DeepClone(newParamCollection.BluePrint)

                For Each param As ParameterBase In newSub.InnerParameters
                    Dim value As String = param.DesignerSettings.GetSettingValueByKey("defaultvalue")
                    If Not String.IsNullOrEmpty(value) AndAlso Not param.SetValue(value) Then
                        Throw New ItemTemplateException(
                            $"Parameter '{param.Name}' tries to set a default value which was not possible.")
                    End If
                Next

                newSub.Id = currentParamCollection.Value.Item(index).Id
                If Not newParamCollection.Value.Contains(newSub.Id) Then
                    newParamCollection.Value.Add(newSub)
                End If
            Next
        End Sub

        Friend Overloads Overrides Function Compare(newParam As ParameterBase, currentParam As ParameterBase) As IEnumerable(Of MetaDataCompareResult)
            Dim result As New List(Of MetaDataCompareResult)()

            If newParam Is Nothing Then
                result.Add(New MetaDataCompareResult(currentParam.Name, Nothing, HelperClasses.ParameterHelper.GetValue(currentParam), String.Empty, My.Resources.Category_AssessmentItem, Nothing))
            ElseIf (currentParam Is Nothing) Then
                result.Add(New MetaDataCompareResult(newParam.Name, Nothing, HelperClasses.ParameterHelper.GetValue(newParam), String.Empty, My.Resources.Category_AssessmentItem, Nothing))
            Else
                Dim newParamCollection As CollectionParameter = DirectCast(newParam, CollectionParameter)
                Dim dummySetNew As New ParameterSetCollection()
                Dim currentParamCollection As CollectionParameter = DirectCast(currentParam, CollectionParameter)
                Dim dummySetcurrent As New ParameterSetCollection()

                If newParamCollection.BluePrint IsNot Nothing Then
                    dummySetNew.Add(newParamCollection.BluePrint)
                End If

                If currentParamCollection.BluePrint IsNot Nothing Then
                    dummySetcurrent.Add(currentParamCollection.BluePrint)
                End If

                result.AddRange(ParameterHandler.Compare(dummySetNew, dummySetcurrent))

                Dim maxLength As Integer
                If Not Integer.TryParse(newParamCollection.DesignerSettings.GetSettingValueByKey("maximumLength"), maxLength) Then
                    maxLength = Integer.MaxValue
                End If

                If maxLength < currentParamCollection.Value.Count Then
                    result.Add(New MetaDataCompareResult(currentParam.Name, Nothing, currentParamCollection.Value.Count.ToString(), maxLength.ToString(), My.Resources.Category_AssessmentItem, Nothing))
                End If
                AddParams(newParamCollection, currentParamCollection, maxLength)
                result.AddRange(ParameterHandler.Compare(newParamCollection.Value, currentParamCollection.Value))
            End If

            Return result
        End Function
    End Class

End Namespace
