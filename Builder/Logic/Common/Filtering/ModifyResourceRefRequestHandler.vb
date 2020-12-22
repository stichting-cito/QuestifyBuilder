Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel.Datasources
Namespace Common.Filtering
    Public Class ModifyResourceRefRequestHandler(Of R As {New, ChainHandlerRequest(Of ResourceRef)})
        Inherits ModifyRequestHandler(Of ResourceRef, R)

        Public Sub New(ByVal requestType As FilterRequestTypeEnum, ByVal items As IEnumerable(Of ResourceRef))
            MyBase.New(requestType, items, New ResourceRefIdentityEqualityComparer)
        End Sub

        Public Sub New(ByVal requestType As FilterRequestTypeEnum, ByVal items As IEnumerable(Of String))
            MyBase.New(requestType, New List(Of String)(items).ConvertAll(Function(id) New ResourceRef(id)),
                       New ResourceRefIdentityEqualityComparer)
        End Sub

    End Class

End Namespace
