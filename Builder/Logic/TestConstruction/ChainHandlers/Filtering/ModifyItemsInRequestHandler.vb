Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel.Datasources
Imports Questify.Builder.Logic.TestConstruction.Requests
Imports Questify.Builder.Logic.Chain

Namespace TestConstruction.ChainHandlers.Filtering

    Public Class ModifyItemsInRequestHandler
        Inherits ChainHandlerBase(Of TestConstructionRequest)


        Private ReadOnly _comparer As IEqualityComparer(Of ResourceRef) = New ResourceRefIdentityEqualityComparer
        Private ReadOnly _items As IEnumerable(Of ResourceRef)
        Private ReadOnly _requestType As RequestTypeEnum



        Public Sub New(ByVal requestType As RequestTypeEnum, ByVal items As IEnumerable(Of ResourceRef))
            _items = items
            _requestType = requestType
        End Sub

        Public Sub New(ByVal name As String, ByVal requestType As RequestTypeEnum, ByVal items As IEnumerable(Of ResourceRef))
            Me.New(requestType, items)
            Me.Name = name
        End Sub



        Public ReadOnly Property RequestType() As RequestTypeEnum
            Get
                Return _requestType
            End Get
        End Property




        Private Function ProcessRequest_AddItems(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            Dim itemsToAdd As IEnumerable(Of ResourceRef) = SetOperations.Difference(_items, requestData.Items, _comparer)

            For Each ref As ResourceRef In itemsToAdd
                requestData.Items.Add(ref)
            Next

            Return ChainHandlerResult.RequestHandled
        End Function


        Private Function ProcessRequest_RemoveItems(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            Dim itemsToRemove As IEnumerable(Of ResourceRef) = SetOperations.Intersect(requestData.Items, _items, _comparer)

            For Each ref As ResourceRef In itemsToRemove
                If Not requestData.Items.Remove(ref) Then
                    Throw New ApplicationException("BUGBUG")
                End If
            Next

            Return ChainHandlerResult.RequestHandled
        End Function




        Public Overrides Function ProcessRequest(ByVal requestData As TestConstructionRequest) As ChainHandlerResult
            Dim result As ChainHandlerResult

            Select Case Me.RequestType
                Case RequestTypeEnum.Add
                    result = ProcessRequest_AddItems(requestData)

                Case RequestTypeEnum.Remove
                    result = ProcessRequest_RemoveItems(requestData)

                Case Else
                    result = ChainHandlerResult.RequestNotHandled
            End Select

            Return result
        End Function



        Public Enum RequestTypeEnum
            Add
            Remove
        End Enum


    End Class
End Namespace