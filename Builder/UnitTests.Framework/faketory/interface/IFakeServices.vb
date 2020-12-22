Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Security

Namespace Faketory.interface

    Public Interface IFakeServices

        Property FakeBankService() As IBankService
        Property FakeResourceService() As IResourceService
        Property FakeSecurityService As ISecurityService
        Property FakePermissionService As IPermissionService

        Sub SetupFakeServices()
        Sub CleanFakeServices()

    End Interface
End NameSpace