Imports System.Xml.Serialization


<XmlRoot("TestState", [Namespace]:="http://Cito.Tester.Server/xml/serialization", IsNullable:=True), _
 Serializable> _
Public Class TestState
    Private ReadOnly _transactions As TransactionDataCollection
    Private ReadOnly _responses As ResponseCollection

    Public ReadOnly Property Transactions As TransactionDataCollection
        Get
            Return _transactions
        End Get
    End Property

    Public ReadOnly Property Responses As ResponseCollection
        Get
            Return _responses
        End Get
    End Property

    Public Property Errors As String()

    Public Sub New(t As TransactionDataCollection, r As ResponseCollection)
        _transactions = t
        _responses = r
    End Sub

    Public Sub New()
        Me.New(New TransactionDataCollection(), New ResponseCollection)
    End Sub
End Class
