Imports FakeItEasy
Imports Questify.Builder.Security

Namespace FakeAppTemplate

    Public Class FakeSecurityServiceHandler

        Private _security As ISecurityService

        Public Sub New(security As ISecurityService)
            _security = security
        End Sub

        Public Sub InitDefault()

            A.CallTo(Function() _security.Authenticate(A(Of String).Ignored, A(Of String).Ignored, A(Of String).Ignored)).
                ReturnsLazily(Function(arg) DoAuthenticate(arg))

        End Sub

        Private Function DoAuthenticate(a As Core.IFakeObjectCall) As AuthenticationResult
            Dim ret As AuthenticationResult = Nothing

            Return ret
        End Function

    End Class
End NameSpace