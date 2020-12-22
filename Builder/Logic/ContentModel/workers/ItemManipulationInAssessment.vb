Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.Service.Factories

Namespace ContentModel

    Friend Class ItemManipulationInAssessment
        Private ReadOnly _assessmentTest As AssessmentTestResourceEntity

        Sub New(AssessmentTest As AssessmentTestResourceEntity)
            _assessmentTest = AssessmentTest
        End Sub

        Public Function ContainsItemCode(itmCode As String) As Boolean
            Dim resourceData = _assessmentTest.ResourceData
            Dim factoryInfo As ReturnedAssessmentTestModelInfo = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(resourceData.BinData, True)
            Dim itemRef = factoryInfo.AssessmentTestv2.GetItemReferenceByName(itmCode)
            Return itemRef IsNot Nothing
        End Function



        Public Function Rename(currentItemCode As String, newItemCode As String) As Boolean
            If (_assessmentTest.ResourceData Is Nothing) Then _assessmentTest.ResourceData = ResourceFactory.Instance.GetResourceData(_assessmentTest)
            Dim resourceData = _assessmentTest.ResourceData
            Dim factoryInfo As ReturnedAssessmentTestModelInfo = AssessmentTestv2Factory.ReturnAssessmentTestv2ModelFromByteArray(resourceData.BinData, True)
            Dim itemRef = factoryInfo.AssessmentTestv2.GetItemReferenceByName(currentItemCode)

            If itemRef IsNot Nothing Then
                itemRef.Title = newItemCode
                itemRef.SourceName = newItemCode
            Else
                Return False
            End If

            _assessmentTest.ResourceData.BinData = SerializeHelper.XmlSerializeToByteArray(factoryInfo.AssessmentTestv2)
            ResourceFactory.Instance.UpdateAssessmentTestResource(_assessmentTest)

            Return True
        End Function

    End Class

End Namespace

