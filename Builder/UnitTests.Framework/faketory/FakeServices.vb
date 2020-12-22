Imports FakeItEasy
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Security
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

Namespace Faketory

    Public Class FakeServices
        Implements IFakeServices


        Public Property FakeBankService As IBankService Implements IFakeServices.FakeBankService


        Public Property FakeResourceService As IResourceService Implements IFakeServices.FakeResourceService


        Public Property FakeSecurityService As ISecurityService Implements IFakeServices.FakeSecurityService


        Public Property FakePermissionService As IPermissionService Implements IFakeServices.FakePermissionService




        Public Sub SetupFakeServices() Implements IFakeServices.SetupFakeServices
            FakeBankService = A.Fake(Of IBankService)()
            BankFactory.Instantiate(FakeBankService)

            FakeResourceService = A.Fake(Of IResourceService)()
            ResourceFactory.Instantiate(FakeResourceService)

            FakeSecurityService = A.Fake(Of ISecurityService)()
            SecurityFactory.Instantiate(FakeSecurityService)

            FakePermissionService = A.Fake(Of IPermissionService)()
            PermissionFactory.Instantiate(FakePermissionService)
        End Sub

        Public Sub CleanFakeServices() Implements IFakeServices.CleanFakeServices
            BankFactory.Destroy()
            ResourceFactory.Destroy()
            SecurityFactory.Destroy()
            PermissionFactory.Destroy()
        End Sub

    End Class
End NameSpace