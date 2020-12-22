Imports Enums
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.UnitTests.Framework.FakeAppTemplate
Imports Questify.Builder.UnitTests.Framework.Faketory.interface

Namespace Faketory.impl

    Public Class AddTreePropertyKeyword
        Implements IAddTreePropertyKeyword


        Private _treeCustomBankProperty As TreeStructureCustomBankPropertyEntity
        Private _currentNode As TreeStructurePartCustomBankPropertyEntity

        Public Sub New(tree As TreeStructureCustomBankPropertyEntity, node As TreeStructurePartCustomBankPropertyEntity)
            _treeCustomBankProperty = tree
            _currentNode = node
        End Sub

        Public Sub New(code As Guid, name As String)
            _treeCustomBankProperty = New TreeStructureCustomBankPropertyEntity()
            _treeCustomBankProperty.Code = code
            _treeCustomBankProperty.Name = name
            _treeCustomBankProperty.Title = name
            _treeCustomBankProperty.ApplicableToMask = ResourceTypeEnum.AllResources

            FakeDal.FakeServices.FakeBankService.UpdateCustomProperty(_treeCustomBankProperty)
        End Sub

        Public Function Node(name As String, title As String) As IAddTreePropertyKeyword Implements IAddTreePropertyKeyword.Node
            If (_currentNode Is Nothing) Then
                Throw New InvalidOperationException("No root node created first")
            End If
            Dim childnode As New TreeStructurePartCustomBankPropertyEntity()
            childnode.Code = Guid.NewGuid
            childnode.Name = name
            childnode.Title = title
            childnode.TreeStructurePartCustomBankPropertyId = Guid.NewGuid

            Dim childStructure As New ChildTreeStructurePartCustomBankPropertyEntity
            childStructure.ChildTreeStructurePartCustomBankPropertyId = childnode.TreeStructurePartCustomBankPropertyId
            childStructure.TreeStructurePartCustomBankProperty = childnode
            _currentNode.ChildTreeStructurePartCustomBankPropertyCollection.Add(childStructure)
            _treeCustomBankProperty.TreeStructurePartCustomBankPropertyCollection.Add(childnode)
            Return Me
        End Function

        Public Function Root(name As String, title As String) As IAddTreePropertyKeyword Implements IAddTreePropertyKeyword.Root
            Dim node As New TreeStructurePartCustomBankPropertyEntity()
            node.Code = Guid.NewGuid
            node.Name = name
            node.Title = title
            node.TreeStructurePartCustomBankPropertyId = Guid.NewGuid
            _treeCustomBankProperty.TreeStructurePartCustomBankPropertyCollection.Add(node)
            Return New AddTreePropertyKeyword(_treeCustomBankProperty, node)
        End Function
    End Class
End NameSpace