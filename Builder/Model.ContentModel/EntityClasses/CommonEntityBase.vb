Imports System
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Model.ContentModel.FactoryClasses
Imports Questify.Builder.Model.ContentModel.RelationClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses
#If Not CF Then
Imports System.Runtime.Serialization
#End If


Namespace Questify.Builder.Model.ContentModel.EntityClasses
    <Serializable()> _
    Public MustInherit Class CommonEntityBase
        Inherits EntityBase2





        Protected Sub New()
            MyBase.New()
        End Sub

        Protected Sub New(name As String)
            MyBase.New(name)
        End Sub

        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)


        End Sub

        Protected Overrides Function GetInheritanceInfoProvider() As IInheritanceInfoProvider
            Return InheritanceInfoProviderSingleton.GetInstance()
        End Function

        Protected Overrides Function CreateTypeDefaultValueProvider() As ITypeDefaultValue
            Return New TypeDefaultValue()
        End Function





    End Class
End Namespace
