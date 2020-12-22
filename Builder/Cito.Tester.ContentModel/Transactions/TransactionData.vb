Imports System.Diagnostics.CodeAnalysis
Imports System.Threading
Imports System.Xml.Serialization
Imports Cito.Tester.Common

<Serializable, _
 XmlRoot("TransactionData", [Namespace]:="http://Cito.Tester.Server/xml/serialization", IsNullable:=True)> _
Public Class TransactionData

    Private _transactions As New List(Of TransactionBase)
    Private _transactionNumber As Long
    Private _sessionId As String
    Private Shared _lastTransactionNumber As Long


    <SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")>
    <XmlElement("AttributeTransaction", GetType(AttributeTransaction)),
XmlElement("DictionaryTransaction", GetType(DictionaryTransaction)),
XmlElement("MethodTransaction", GetType(MethodTransaction)),
XmlElement("ItemRefTransaction2", GetType(ItemRefTransaction2)),
XmlElement("RestartTestTransaction", GetType(RestartTestTransaction)),
XmlElement("SessionRestartedTransaction", GetType(SessionRestartedTransaction))>
    Public ReadOnly Property Transactions As List(Of TransactionBase)
        Get
            Return _transactions
        End Get
    End Property

    Public Property TransactionNumber As Long
        Get
            Return _transactionNumber
        End Get
        Set
            _transactionNumber = value
        End Set
    End Property

    Public Property SessionId As String
        Get
            Return _sessionId
        End Get
        Set
            _sessionId = value
        End Set
    End Property



    Public Sub New(sessionId As String)
        Me.New()
        _sessionId = sessionId
    End Sub

    Public Sub New()
        _transactionNumber = Long.MinValue
    End Sub




    Private Shared Function GetNewTransactionNumber() As Long
        Interlocked.Increment(_lastTransactionNumber)
        Return _lastTransactionNumber
    End Function

    Public Shared Sub SetLastTransactionNumber(value As Long)
        _lastTransactionNumber = value
    End Sub

    Public Shared Sub ResetTransactionNumber()
        _lastTransactionNumber = 0
    End Sub



    Public Sub AddAttributeTransaction(item As ItemReferenceViewBase, attribute As String)
        Me.Transactions.Add(New AttributeTransaction(item, attribute))
    End Sub

    Public Sub AddAttributeTransactionUsingIdentifier(item As ItemReferenceViewBase, attribute As String)
        Me.Transactions.Add(New AttributeTransaction(item, attribute, True))
    End Sub

    Public Sub AddAttributeTransaction(section As TestSectionViewBase, attribute As String)
        Me.Transactions.Add(New AttributeTransaction(section, attribute))
    End Sub

    Public Sub AddAttributeTransaction(part As TestPartViewBase, attribute As String)
        Me.Transactions.Add(New AttributeTransaction(part, attribute))
    End Sub

    Public Sub AddMethodTransaction(section As TestSectionViewBase, methodName As String)
        Me.Transactions.Add(New MethodTransaction(section, methodName))
    End Sub

    Public Sub AddDictionaryTransaction(component As TestComponentBase, attribute As String, key As String, value As String, action As DictionaryTransaction.DictionaryActionType)
        Me.Transactions.Add(New DictionaryTransaction(component, attribute, key, value, action))
    End Sub

    Public Sub AddItemRefTransaction(section As TestSectionViewBase, itemRef As ItemReferenceViewBase)
        Me.Transactions.Add(New ItemRefTransaction2(section, DirectCast(itemRef.TestComponentModel, ItemReference2)))
    End Sub

    Public Sub AddRange(trans As TransactionBase())
        Me.Transactions.AddRange(trans)
    End Sub



    Public Sub EnsureNewTransactionNumber()
        If _transactionNumber = Long.MinValue Then
            _transactionNumber = GetNewTransactionNumber()
        Else
            Throw New ContentModelException(
                $"A transaction that is assigned a number, already has another number (nr = {Me.TransactionNumber _
                                               }). Each transaction should be unique!")
        End If
    End Sub


End Class
