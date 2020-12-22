Imports System
#If Not CF Then
Imports System.Runtime.Serialization
#End If
Imports System.Collections.Generic
Imports Questify.Builder.Model.ContentModel.FactoryClasses

Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.HelperClasses
    <Serializable()> _
    Public Class ResultsetFields
        Inherits EntityFields2
        Implements ISerializable

        Public Sub New(amountFields As Integer)
            MyBase.New(amountFields, InheritanceInfoProviderSingleton.GetInstance(), Nothing)
        End Sub

        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info.GetInt32("_amountFields"), InheritanceInfoProviderSingleton.GetInstance(), Nothing)
            Dim fields As List(Of IEntityField2) = CType(info.GetValue("_fields", GetType(List(Of IEntityField2))), List(Of IEntityField2))
            For i As Integer = 0 To fields.Count - 1
                Me(i) = fields(i)
            Next i
        End Sub

        Public Overridable Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
            info.AddValue("_amountFields", Me.Count)
            Dim fields As New List(Of IEntityField2)(Me.Count)
            For i As Integer = 0 To Me.Count - 1
                fields.Add(Me(i))
            Next i
            info.AddValue("_fields", fields, GetType(List(Of IEntityField2)))
        End Sub


    End Class
End Namespace
