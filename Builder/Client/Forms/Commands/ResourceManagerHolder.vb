
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.ResourceManager
Imports Cito.Tester.ContentModel

Public Class ResourceManagerHolder

    Private ReadOnly _bankId As Integer
    Private ReadOnly _resourceManager As DataBaseResourceManager


    Public Sub New(ByVal bank As BankEntity)
        _bankId = bank.Id
        _resourceManager = New DataBaseResourceManager(_bankId)
    End Sub



    Public ReadOnly Property ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)
        Get
            Return New EventHandler(Of ResourceNeededEventArgs)(AddressOf ResourcesNeeded)
        End Get
    End Property

    Public ReadOnly Property Bankid As Integer
        Get
            Return _bankId
        End Get
    End Property


    Sub ResourcesNeeded(sender As Object, e As ResourceNeededEventArgs)
        _resourceManager.HandleResourceNeeded(e, New ResourceRequestDTO())
    End Sub

End Class
