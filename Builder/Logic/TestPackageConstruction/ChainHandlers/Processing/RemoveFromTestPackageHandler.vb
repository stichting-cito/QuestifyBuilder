Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.TestPackageConstruction.Requests
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestPackageConstruction.ChainHandlers.Processing
    Public Class RemoveFromTestPackageHandler
        Inherits ChainHandlerBase(Of TestPackageConstructionRequest)

        Private ReadOnly _testPackage As TestPackage

        Public Sub New(ByVal testPackage As TestPackage)
            If testPackage Is Nothing Then
                Throw New ArgumentNullException("testPackage")
            End If

            _testPackage = testPackage
        End Sub


        Public Overrides Function ProcessRequest(ByVal requestData As TestPackageConstructionRequest) As ChainHandlerResult
            Dim result As ChainHandlerResult

            If requestData.RequestType = TestConstructionRequest.RequestTypeEnum.Remove AndAlso requestData.Tests.Count > 0 Then
                result = ExecuteRequest(requestData)
            Else
                result = ChainHandlerResult.RequestNotHandled
            End If

            Return result
        End Function


        Private Function ExecuteRequest(ByVal requestData As TestPackageConstructionRequest) As ChainHandlerResult
            For Each res As Datasources.ResourceRef In requestData.Tests
                Dim testSet As TestSet
                Dim testref As TestReference = _testPackage.GetTestReferenceByName(res.Identifier)

                Trace.Assert(testref IsNot Nothing)

                If testref IsNot Nothing Then
                    testSet = CType(testref.Parent, TestSet)
                    Trace.Assert(testSet IsNot Nothing)

                    If testSet IsNot Nothing Then
                        testSet.Components.Remove(testref)
                    End If
                End If
            Next

            Return ChainHandlerResult.RequestHandled
        End Function

    End Class
End Namespace