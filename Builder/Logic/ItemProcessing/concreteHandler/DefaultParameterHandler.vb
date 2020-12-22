Imports Cito.Tester.ContentModel
Imports Versioning

Namespace ItemProcessing
    Friend Class DefaultParameterHandler
        Inherits ParameterHandler

        Friend Overrides Sub Merge(newParam As ParameterBase, currentParam As ParameterBase, warnErr As WarningsAndErrors)
            newParam.Nodes = currentParam.Nodes
        End Sub

        Friend Overloads Overrides Function Compare(newParam As ParameterBase, currentParam As ParameterBase) As IEnumerable(Of MetaDataCompareResult)
            Dim result As New List(Of MetaDataCompareResult)()
            Dim currentParamValue As String = HelperClasses.ParameterHelper.GetValue(currentParam)
            Dim newParamValue As String = HelperClasses.ParameterHelper.GetValue(newParam)

            If currentParam IsNot Nothing AndAlso newParam IsNot Nothing Then
                If currentParamValue <> newParamValue Then
                    Dim designerSetting As DesignerSetting = currentParam.DesignerSettings.GetDesignerSettingByKey("label")

                    If designerSetting IsNot Nothing Then
                        result.Add(New MetaDataCompareResult(currentParam.Name, designerSetting.Value, currentParamValue, newParamValue, My.Resources.Category_AssessmentItem, Nothing))
                    Else
                        result.Add(New MetaDataCompareResult(currentParam.Name, Nothing, currentParamValue, newParamValue, My.Resources.Category_AssessmentItem, Nothing))
                    End If
                End If
            ElseIf currentParam Is Nothing Then
                Dim designerSetting As DesignerSetting = newParam.DesignerSettings.GetDesignerSettingByKey("label")

                If designerSetting IsNot Nothing Then
                    result.Add(New MetaDataCompareResult(newParam.Name, designerSetting.Value, String.Empty, newParamValue, My.Resources.Category_AssessmentItem, Nothing))
                Else
                    result.Add(New MetaDataCompareResult(newParam.Name, Nothing, String.Empty, newParamValue, My.Resources.Category_AssessmentItem, Nothing))
                End If
            ElseIf newParam Is Nothing Then
                Dim designerSetting As DesignerSetting = currentParam.DesignerSettings.GetDesignerSettingByKey("label")

                If designerSetting IsNot Nothing Then
                    result.Add(New MetaDataCompareResult(currentParam.Name, designerSetting.Value, currentParamValue, String.Empty, My.Resources.Category_AssessmentItem, Nothing))
                Else
                    result.Add(New MetaDataCompareResult(currentParam.Name, Nothing, currentParamValue, String.Empty, My.Resources.Category_AssessmentItem, Nothing))
                End If
            End If
            Return result
        End Function

    End Class
End Namespace

