
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace ContentModel

    Public Class BankHierarchyNode
        Public Property BankId As Integer
        Public Property BankName As String
        Public Property ParentBankNode As BankHierarchyNode
        Public Property ChildBankNodes As List(Of BankHierarchyNode)
        Public Property IsViewPointBank As Boolean

        Public Sub New(ByVal theBank As BankEntity)
            BankId = theBank.Id
            BankName = theBank.Name

            ChildBankNodes = New List(Of BankHierarchyNode)
        End Sub
    End Class

End Namespace

