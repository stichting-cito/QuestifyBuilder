Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestConstruction.ChainHandlers.Processing
    Public Class RemoveFromAssessmentTestHandler
        Inherits ChainHandlerBase(Of TestConstructionRequest)

        Private ReadOnly _assessmentTest As AssessmentTest2

        Public Sub New(ByVal test As AssessmentTest2)
            If test Is Nothing Then
                Throw New ArgumentNullException("test")
            End If

            _assessmentTest = test
        End Sub


        Public Overrides Function ProcessRequest(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            Dim result As ChainHandlerResult

            If requestData.RequestType = TestConstructionRequest.RequestTypeEnum.Remove AndAlso requestData.Items.Count > 0 Then
                result = ExecuteRequest(requestData)
            Else
                result = ChainHandlerResult.RequestNotHandled
            End If

            Return result
        End Function

        Private Function ExecuteRequest(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            For Each res As Datasources.ResourceRef In requestData.Items
                Dim testSection As TestSection2
                Dim itemref As ItemReference2 = _assessmentTest.GetItemReferenceByName(res.Identifier)

                Trace.Assert(itemref IsNot Nothing)

                If itemref IsNot Nothing Then
                    testSection = CType(itemref.Parent, TestSection2)
                    Trace.Assert(testSection IsNot Nothing)

                    If testSection IsNot Nothing Then
                        testSection.Components.Remove(itemref)
                    End If
                End If
            Next

            Return ChainHandlerResult.RequestHandled
        End Function

    End Class

End Namespace


