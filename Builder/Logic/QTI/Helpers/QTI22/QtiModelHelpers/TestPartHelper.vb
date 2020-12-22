Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Helpers.QTI22.QtiModelHelpers

    Public Class TestPartHelper

        Protected _testPart As GeneralTestPart

        Public Sub New(testPart As GeneralTestPart)
            _testPart = testPart
        End Sub

        Public Overridable Function CreateTestPart() As TestPartType
            Dim testPart As New TestPartType
            testPart.identifier = ChainHandlerHelper.GetIdentifierFromResourceId(_testPart.Identifier, PackageCreatorConstants.TypeOfResource.resource)
            If TypeOf _testPart Is QTITestPartBase Then
                Dim timeLimits = TimeLimitsHelper.GetTimeLimitsType(DirectCast(_testPart, QTITestPartBase).TimeLimits)
                If timeLimits IsNot Nothing Then
                    testPart.timeLimits = timeLimits
                End If
            End If

            Return testPart
        End Function

        Public Sub AddTestPartToAssessmentTestType(ByRef assessmentTestType As AssessmentTestType, testPartType As TestPartType)
            If assessmentTestType.testPart IsNot Nothing AndAlso assessmentTestType.testPart.Count > 0 Then
                Dim testParts As List(Of TestPartType) = assessmentTestType.testPart.ToList
                testParts.Add(testPartType)
                assessmentTestType.testPart = testParts
            Else
                Dim testParts As New List(Of TestPartType)
                testParts.Add(testPartType)
                assessmentTestType.testPart = testParts
            End If
        End Sub

        Protected Function ToObject(objectToConvert As Object, typeToConvertTo As Type) As Object
            Dim value As String = SerializeHelper.XmlSerializeToString(objectToConvert)
            Return SerializeHelper.XmlDeserializeFromString(value, typeToConvertTo)
        End Function

    End Class
End Namespace