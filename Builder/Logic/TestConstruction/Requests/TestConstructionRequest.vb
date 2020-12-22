Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Cito.Tester.ContentModel.Datasources

Namespace TestConstruction.Requests
    Public Class TestConstructionRequest
        Inherits ChainHandlerRequest(Of Datasources.ResourceRef)


        Private ReadOnly _comparer As IEqualityComparer(Of ResourceRef) = New ResourceRefIdentityEqualityComparer
        Private ReadOnly _itemContext As IEnumerable(Of Datasources.ResourceRef)
        Private ReadOnly _requestType As TestConstructionRequest.RequestTypeEnum
        Private ReadOnly _overriddenTarget As IDictionary(Of ResourceRef, TestSection2)



        Public Sub New(ByVal requestType As RequestTypeEnum, ByVal items As IEnumerable(Of ResourceRef), ByVal itemContext As IEnumerable(Of ResourceRef))
            MyBase.New(items)
            _itemContext = itemContext
            _requestType = requestType
            _overriddenTarget = New Dictionary(Of ResourceRef, TestSection2)()
        End Sub



        Public ReadOnly Property ItemContext() As IEnumerable(Of Datasources.ResourceRef)
            Get
                Return _itemContext
            End Get
        End Property


        Public ReadOnly Property RequestType() As TestConstructionRequest.RequestTypeEnum
            Get
                Return _requestType
            End Get
        End Property




        Public Function GetPurposedItemContext() As IEnumerable(Of ResourceRef)
            Dim purposedItemContext As IEnumerable(Of ResourceRef)

            Select Case Me.RequestType
                Case RequestTypeEnum.Add
                    purposedItemContext = SetOperations.Union(Me.ItemContext, Me.Items, _comparer)

                Case RequestTypeEnum.Remove
                    purposedItemContext = SetOperations.Difference(Items, ItemContext, _comparer)

                Case Else
                    Throw New NotSupportedException
            End Select

            Return purposedItemContext
        End Function



        Friend ReadOnly Property OverridenTarget As IDictionary(Of ResourceRef, TestSection2)
            Get
                Return _overriddenTarget
            End Get
        End Property



        Public Enum RequestTypeEnum
            Add
            Remove
        End Enum


    End Class

End Namespace