Imports Enums
Imports FakeItEasy
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.ResourceProperties

Namespace FakeAppTemplate

    Public Class FakeBankServiceHandler

        Private ReadOnly _bankService As IBankService
        Private ReadOnly _collResources As EntityCollection


        Public Sub New(bankService As IBankService)
            _bankService = bankService
            _collResources = New EntityCollection
        End Sub

        Public Sub InitDefault()

            A.CallTo(Function() _bankService.GetResourcePropertyDefinitions(A(Of BankEntity).Ignored)
                     ).ReturnsLazily(Function(a) GetResourcePropertyDefinitions(a))
            A.CallTo(Function() _bankService.GetCustomBankPropertiesForBranch(A(Of BankEntity).Ignored, A(Of ResourceTypeEnum).Ignored)
                     ).ReturnsLazily(Function(a) GetCustomBankPropertiesForBranch(a))
            A.CallTo(Function() _bankService.GetCustomBankPropertiesForBranchById(A(Of Integer).Ignored, A(Of ResourceTypeEnum).Ignored)
                     ).ReturnsLazily(Function(a) GetCustomBankPropertiesForBranch(a))
            A.CallTo(Function() _bankService.GetCustomBankPropertiesForBranchById(A(Of Integer).Ignored, A(Of ResourceTypeEnum).Ignored)
                     ).ReturnsLazily(Function(a) GetCustomBankPropertiesForBranchById(a))
        End Sub

        Public Sub EnableSaveResources()

            A.CallTo(Function() _bankService.UpdateCustomProperty(A(Of CustomBankPropertyEntity).Ignored)
                     ).ReturnsLazily(Function(a) UpdateCustomProperty(a))

            A.CallTo(Function() _bankService.UpdateCustomProperties(A(Of EntityCollection).Ignored)
                     ).ReturnsLazily(Function(a) UpdateCustomProperties(a))
        End Sub

        Private Function UpdateCustomProperty(a As Core.IFakeObjectCall) As String
            Dim entry = a.Arguments.Get(Of CustomBankPropertyEntity)(0)
            _collResources.Add(entry)

            Return String.Empty
        End Function

        Private Function UpdateCustomProperties(a As Core.IFakeObjectCall) As String
            Dim entry = a.Arguments.Get(Of EntityCollection)(0)
            _collResources.AddRange(entry)

            Return String.Empty
        End Function

        Public Function GetResourcePropertyDefinitions(a As Core.IFakeObjectCall) As ResourcePropertyDefinitionCollection
            Dim result As New ResourcePropertyDefinitionCollection()
            Dim builder As New BankResourcePropertyBuilder(_bankService)

            result.AddRange(builder.AddStaticPropertyDefinitionsOfResource())
            result.AddRange(builder.AddDynamicPropertyDefinitionsOfResource(Nothing))

            Return result
        End Function

        Private Function GetCustomBankPropertiesForBranchById(a As Core.IFakeObjectCall) As EntityCollection
            Dim result As New EntityCollection()
            result.AddRange(From e In _collResources Where TypeOf e Is CustomBankPropertyEntity Select e)
            Return result
        End Function

        Private Function GetCustomBankPropertiesForBranch(a As Core.IFakeObjectCall) As EntityCollection
            Dim result As New EntityCollection()
            result.AddRange(From e In _collResources Where TypeOf e Is CustomBankPropertyEntity Select e)
            Return result
        End Function

    End Class
End NameSpace