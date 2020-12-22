Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.RelationClasses
    Public Class UserRelations
        Public Sub New()
        End Sub

        Public Overridable Function GetAllRelations() As List(Of IEntityRelation)
            Dim toReturn As List(Of IEntityRelation) = New List(Of IEntityRelation)()
            toReturn.Add(Me.BankEntityUsingCreatedBy)
            toReturn.Add(Me.BankEntityUsingModifiedBy)
            toReturn.Add(Me.CustomBankPropertyEntityUsingCreatedBy)
            toReturn.Add(Me.CustomBankPropertyEntityUsingModifiedBy)
            toReturn.Add(Me.ResourceEntityUsingCreatedBy)
            toReturn.Add(Me.ResourceEntityUsingModifiedBy)
            toReturn.Add(Me.RoleEntityUsingCreatedBy)
            toReturn.Add(Me.RoleEntityUsingModifiedBy)
            toReturn.Add(Me.UserEntityUsingCreatedBy)
            toReturn.Add(Me.UserEntityUsingModifiedBy)
            toReturn.Add(Me.UserApplicationRoleEntityUsingUserId)
            toReturn.Add(Me.UserBankRoleEntityUsingUserId)
            toReturn.Add(Me.UserEntityUsingIdCreatedBy)
            toReturn.Add(Me.UserEntityUsingIdModifiedBy)
            Return toReturn
        End Function


        Public Overridable ReadOnly Property BankEntityUsingCreatedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "", True)
                relation.AddEntityFieldPair(UserFields.Id, BankFields.CreatedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property BankEntityUsingModifiedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "", True)
                relation.AddEntityFieldPair(UserFields.Id, BankFields.ModifiedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("BankEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property CustomBankPropertyEntityUsingCreatedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "CustomBankPropertyCollection_", True)
                relation.AddEntityFieldPair(UserFields.Id, CustomBankPropertyFields.CreatedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property CustomBankPropertyEntityUsingModifiedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "CustomBankPropertyCollection", True)
                relation.AddEntityFieldPair(UserFields.Id, CustomBankPropertyFields.ModifiedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("CustomBankPropertyEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ResourceEntityUsingCreatedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "", True)
                relation.AddEntityFieldPair(UserFields.Id, ResourceFields.CreatedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property ResourceEntityUsingModifiedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "", True)
                relation.AddEntityFieldPair(UserFields.Id, ResourceFields.ModifiedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("ResourceEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property RoleEntityUsingCreatedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "", True)
                relation.AddEntityFieldPair(UserFields.Id, RoleFields.CreatedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RoleEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property RoleEntityUsingModifiedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "", True)
                relation.AddEntityFieldPair(UserFields.Id, RoleFields.ModifiedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("RoleEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserEntityUsingCreatedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "", True)
                relation.AddEntityFieldPair(UserFields.Id, UserFields.CreatedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserEntityUsingModifiedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "", True)
                relation.AddEntityFieldPair(UserFields.Id, UserFields.ModifiedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserApplicationRoleEntityUsingUserId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "UserApplicationRoleCollection", True)
                relation.AddEntityFieldPair(UserFields.Id, UserApplicationRoleFields.UserId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserApplicationRoleEntity", False)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserBankRoleEntityUsingUserId() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.OneToMany, "UserBankRoleCollection", True)
                relation.AddEntityFieldPair(UserFields.Id, UserBankRoleFields.UserId)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserBankRoleEntity", False)
                Return relation
            End Get
        End Property


        Public Overridable ReadOnly Property UserEntityUsingIdCreatedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "CreatedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, UserFields.CreatedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable ReadOnly Property UserEntityUsingIdModifiedBy() As IEntityRelation
            Get
                Dim relation As IEntityRelation = New EntityRelation(SD.LLBLGen.Pro.ORMSupportClasses.RelationType.ManyToOne, "ModifiedByUser", False)
                relation.AddEntityFieldPair(UserFields.Id, UserFields.ModifiedBy)
                relation.InheritanceInfoPkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", False)
                relation.InheritanceInfoFkSideEntity = InheritanceInfoProviderSingleton.GetInstance().GetInheritanceInfo("UserEntity", True)
                Return relation
            End Get
        End Property
        Public Overridable Function GetSubTypeRelation(subTypeEntityName As String) As IEntityRelation
            Return Nothing
        End Function
        Public Overridable Function GetSuperTypeRelation() As IEntityRelation
            Return Nothing
        End Function


    End Class

    Friend Class StaticUserRelations
        Friend Shared ReadOnly BankEntityUsingCreatedByStatic As IEntityRelation = New UserRelations().BankEntityUsingCreatedBy
        Friend Shared ReadOnly BankEntityUsingModifiedByStatic As IEntityRelation = New UserRelations().BankEntityUsingModifiedBy
        Friend Shared ReadOnly CustomBankPropertyEntityUsingCreatedByStatic As IEntityRelation = New UserRelations().CustomBankPropertyEntityUsingCreatedBy
        Friend Shared ReadOnly CustomBankPropertyEntityUsingModifiedByStatic As IEntityRelation = New UserRelations().CustomBankPropertyEntityUsingModifiedBy
        Friend Shared ReadOnly ResourceEntityUsingCreatedByStatic As IEntityRelation = New UserRelations().ResourceEntityUsingCreatedBy
        Friend Shared ReadOnly ResourceEntityUsingModifiedByStatic As IEntityRelation = New UserRelations().ResourceEntityUsingModifiedBy
        Friend Shared ReadOnly RoleEntityUsingCreatedByStatic As IEntityRelation = New UserRelations().RoleEntityUsingCreatedBy
        Friend Shared ReadOnly RoleEntityUsingModifiedByStatic As IEntityRelation = New UserRelations().RoleEntityUsingModifiedBy
        Friend Shared ReadOnly UserEntityUsingCreatedByStatic As IEntityRelation = New UserRelations().UserEntityUsingCreatedBy
        Friend Shared ReadOnly UserEntityUsingModifiedByStatic As IEntityRelation = New UserRelations().UserEntityUsingModifiedBy
        Friend Shared ReadOnly UserApplicationRoleEntityUsingUserIdStatic As IEntityRelation = New UserRelations().UserApplicationRoleEntityUsingUserId
        Friend Shared ReadOnly UserBankRoleEntityUsingUserIdStatic As IEntityRelation = New UserRelations().UserBankRoleEntityUsingUserId
        Friend Shared ReadOnly UserEntityUsingIdCreatedByStatic As IEntityRelation = New UserRelations().UserEntityUsingIdCreatedBy
        Friend Shared ReadOnly UserEntityUsingIdModifiedByStatic As IEntityRelation = New UserRelations().UserEntityUsingIdModifiedBy

        Shared Sub New()
        End Sub
    End Class
End Namespace
