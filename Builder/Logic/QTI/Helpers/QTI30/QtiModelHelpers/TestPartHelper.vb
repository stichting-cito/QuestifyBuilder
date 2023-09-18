Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.PackageCreators.QTI_Base
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.Helpers.QTI30.QtiModelHelpers

    Public Class TestPartHelper

        Protected _testPart As GeneralTestPart

        Public Sub New(testPart As GeneralTestPart)
            _testPart = testPart
        End Sub

        Public Overridable Function CreateTestPart() As TestPartType
            Dim testPart = GetTestPart()
            testPart.qtitimelimits = GetTimeLimits()
            Return testPart
        End Function

        Private Function GetTestPart() As TestPartType
            Return New TestPartType With {
                .identifier = ChainHandlerHelper.GetIdentifierFromResourceId(_testPart.Identifier, PackageCreatorConstants.TypeOfResource.resource),
                .title = _testPart.Title
            }
        End Function

        Private Function GetTimeLimits() As TimeLimitsType
            If TypeOf _testPart Is QTITestPartBase Then
                Dim timeLimits = TimeLimitsHelper.GetTimeLimitsType(DirectCast(_testPart, QTITestPartBase).TimeLimits)
                If timeLimits IsNot Nothing Then
                    Return timeLimits
                End If
            End If
            Return Nothing
        End Function

        Public Sub AddTestPartToAssessmentTestType(ByRef assessmentTestType As AssessmentTestType, testPartType As TestPartType)
            If assessmentTestType.qtitestpart IsNot Nothing AndAlso assessmentTestType.qtitestpart.Count > 0 Then
                Dim testParts As List(Of TestPartType) = assessmentTestType.qtitestpart.ToList()
                testParts.Add(testPartType)
                assessmentTestType.qtitestpart = testParts.ToArray()
            Else
                assessmentTestType.qtitestpart = {testPartType}
            End If
        End Sub

        Protected Function ToObject(objectToConvert As Object, typeToConvertTo As Type) As Object
            Dim value As String = SerializeHelper.XmlSerializeToString(objectToConvert)
            Return SerializeHelper.XmlDeserializeFromString(value, typeToConvertTo)
        End Function

    End Class
End Namespace