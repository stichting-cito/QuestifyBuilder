Imports System.Linq
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories

Namespace ContentModel

    Public Class BankHierarchy

        Dim ReadOnly _rootBankNode As BankHierarchyNode

        Public Sub New(ByVal viewPointBankId As Integer)
            Dim viewPointBank As BankEntity = BankFactory.Instance.GetBank(viewPointBankId)
            Dim viewPointBankNode As BankHierarchyNode = Nothing
            Dim runningBank As BankEntity = viewPointBank


            Dim previousBankNode As BankHierarchyNode = Nothing

            While runningBank IsNot Nothing
                Dim newBankNode As New BankHierarchyNode(runningBank)

                If previousBankNode Is Nothing Then
                    newBankNode.IsViewPointBank = True
                    viewPointBankNode = newBankNode
                Else
                    newBankNode.ChildBankNodes.Add(previousBankNode)
                    previousBankNode.ParentBankNode = newBankNode
                End If

                previousBankNode = newBankNode
                _rootBankNode = newBankNode

                If runningBank.ParentBankId IsNot Nothing Then
                    runningBank = BankFactory.Instance.GetBank(CInt(runningBank.ParentBankId))
                Else
                    runningBank = Nothing
                End If
            End While

            If viewPointBankNode IsNot Nothing Then
                Dim bankhierarchy As EntityCollection = BankFactory.Instance.GetBankHierarchy()
                Dim rootBankResult = bankhierarchy.Where(Function(x) DirectCast(x, BankEntity).Id = _rootBankNode.BankId)
                If rootBankResult.Count > 0 Then
                    Dim rootBank As BankEntity = DirectCast(rootBankResult(0), BankEntity)

                    viewPointBank = LookupBankInHierarchy(rootBank, viewPointBank.Id)
                    AddChildBankNodes(viewPointBankNode, viewPointBank)
                End If

                SortChildBanksAlphabetically(_rootBankNode)
            End If
        End Sub

        Public ReadOnly Property Root As BankHierarchyNode
            Get
                Return _rootBankNode
            End Get
        End Property

        Public Function BankInFirstParameterIsDescendantOfBankInSecondParameter(ByVal bankOneId As Integer, ByVal bankTwoId As Integer) As Boolean
            Dim bankOneIsDescendantOfTwo As Boolean = False

            Dim bankTwoNode As BankHierarchyNode = LookupBankTreeNodeViaBankId(bankTwoId, _rootBankNode)

            Debug.Assert(bankTwoNode IsNot Nothing, "BankTreeNode for bankTwoId should not be nothing!")

            If bankTwoNode IsNot Nothing AndAlso bankTwoNode.BankId <> bankOneId Then
                Dim bankOneNode As BankHierarchyNode = LookupBankTreeNodeViaBankId(bankOneId, bankTwoNode)

                bankOneIsDescendantOfTwo = (bankOneNode IsNot Nothing)
            End If

            Return bankOneIsDescendantOfTwo
        End Function

        Private Function LookupBankTreeNodeViaBankId(ByVal bankId As Integer, ByVal currentNode As BankHierarchyNode) As BankHierarchyNode
            Dim foundNode As BankHierarchyNode = Nothing

            If currentNode IsNot Nothing Then
                If currentNode.BankId = bankId Then
                    foundNode = currentNode
                Else
                    For Each childNode As BankHierarchyNode In currentNode.ChildBankNodes
                        foundNode = LookupBankTreeNodeViaBankId(bankId, childNode)
                        If foundNode IsNot Nothing Then
                            Exit For
                        End If
                    Next
                End If
            End If

            Return foundNode
        End Function

        Private Sub AddChildBankNodes(ByVal banktreeNode As BankHierarchyNode, ByVal parentBank As BankEntity)

            For Each childBank As BankEntity In parentBank.BankCollection
                Dim newChildNode As New BankHierarchyNode(childBank) With {.ParentBankNode = banktreeNode}

                banktreeNode.ChildBankNodes.Add(newChildNode)

                AddChildBankNodes(newChildNode, childBank)
            Next

        End Sub

        Private Function LookupBankInHierarchy(ByVal parentbank As BankEntity, ByVal childBankId As Integer) As BankEntity
            Dim childBank As BankEntity = Nothing

            If parentbank.Id = childBankId Then
                childBank = parentbank
            Else
                For Each bank As BankEntity In parentbank.BankCollection
                    If bank.Id = childBankId Then
                        childBank = bank
                        Exit For
                    Else
                        childBank = LookupBankInHierarchy(bank, childBankId)
                        If childBank IsNot Nothing Then
                            Exit For
                        End If
                    End If
                Next
            End If

            Return childBank
        End Function

        Private Sub SortChildBanksAlphabetically(ByVal bankNode As BankHierarchyNode)
            bankNode.ChildBankNodes.Sort(Function(x, y) x.BankName.CompareTo(y.BankName))

            bankNode.ChildBankNodes.ForEach(Sub(x) SortChildBanksAlphabetically(x))
        End Sub

    End Class

End Namespace