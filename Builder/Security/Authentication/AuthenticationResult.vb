Imports System.Xml.Serialization

<XmlRoot("AuthenticationResult", [Namespace]:="http://Cito.TestBuilder.Security/xml/serialization", IsNullable:=True), _
 Serializable()> _
Public Class AuthenticationResult
    public Property QuestifyBuilderIdentity() As TestBuilderIdentity
    Public Property AuthenticationActionState() As Integer

    Public Sub New
        AuthenticationActionState = Security.AuthenticationActionState.Successful
    End Sub

    Public Sub New(authenticationActionState1 As AuthenticationActionState)
        MyBase.New()
        AuthenticationActionState = authenticationActionState1
    End Sub

    Public Sub New(identity As TestBuilderIdentity, authenticationActionState1 As AuthenticationActionState)
        QuestifyBuilderIdentity = identity
        AuthenticationActionState = authenticationActionState1
    End Sub
End Class