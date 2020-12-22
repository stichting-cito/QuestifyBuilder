Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.Common
Namespace Common.Filtering
    Public Class ModifyRequestHandler(Of T, R As {New, ChainHandlerRequest(Of T)})
        Inherits ChainHandlerBase(Of R)

        Private ReadOnly _comparer As IEqualityComparer(Of T)
        Private ReadOnly _items As IEnumerable(Of T)
        Private ReadOnly _requestType As FilterRequestTypeEnum

        Public Sub New(ByVal requestType As FilterRequestTypeEnum, ByVal items As IEnumerable(Of T), ByVal comparer As IEqualityComparer(Of T))
            _items = items
            _requestType = requestType
            _comparer = comparer
        End Sub

        Private Function ProcessRequest_AddItems(ByVal requestData As R) As ChainHandlerResult
            Dim itemsToAdd As IEnumerable(Of T) = SetOperations.Difference(requestData.Items, Me._items, Me._comparer)

            For Each ref As T In itemsToAdd
                requestData.Items.Add(ref)
            Next

            Return ChainHandlerResult.RequestHandled
        End Function

        Private Function ProcessRequest_RemoveItems(ByVal requestData As R) As ChainHandlerResult
            Dim itemsToRemove As IEnumerable(Of T) = SetOperations.Intersect(requestData.Items, Me._items, Me._comparer)

            For Each ref As T In itemsToRemove
                If Not requestData.Items.Remove(ref) Then
                    Debug.Assert(False, "Tried to remove an item that is not part of the collection.")
                    Throw New ApplicationException("BUGBUG")
                End If
            Next

            Return ChainHandlerResult.RequestHandled
        End Function

        Public Overrides Function ProcessRequest(ByVal requestData As R) As ChainHandlerResult
            Dim result As ChainHandlerResult

            Select Case _requestType
                Case FilterRequestTypeEnum.Add
                    result = ProcessRequest_AddItems(requestData)

                Case FilterRequestTypeEnum.Remove
                    result = ProcessRequest_RemoveItems(requestData)

                Case Else
                    result = ChainHandlerResult.RequestNotHandled
            End Select

            Return result
        End Function

    End Class

End Namespace