Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
#If Not CF Then
Imports System.Runtime.Serialization
#End If
Imports System.Xml.Serialization
Imports System.Xml
Imports System.Xml.Schema
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.HelperClasses
    <Serializable()> _
    Public Class EntityCollection
        Inherits EntityCollectionNonGeneric

        Public Sub New()
        End Sub

        Public Sub New(entityFactoryToUse As IEntityFactory2)
            MyBase.New(entityFactoryToUse)
        End Sub

        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
        End Sub





    End Class

    <Serializable()> _
    Public Class EntityCollection(Of TEntity As {EntityBase2, IEntity2})
        Inherits EntityCollectionBase2(Of TEntity)

        Public Sub New()
            MyBase.New(CType(Nothing, IEntityFactory2))
        End Sub

        Public Sub New(entityFactoryToUse As IEntityFactory2)
            MyBase.New(entityFactoryToUse)
        End Sub

        Public Sub New(initialContents As IList(Of TEntity))
            MyBase.New(initialContents)
        End Sub

        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
        End Sub





    End Class
End Namespace
