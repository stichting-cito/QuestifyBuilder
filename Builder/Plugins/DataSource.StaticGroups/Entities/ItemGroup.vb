Imports System.ComponentModel
Imports System.Xml.Serialization

Namespace Entities

    <Serializable()>
    Public Class ItemGroup
        Inherits StaticGroupEntry




        Public Sub New(ByVal resourceIdentifier As String)
            MyBase.New(resourceIdentifier)
            Items = New BindingList(Of ItemReference)()
        End Sub

        Public Sub New()
            MyBase.New()
            Items = New BindingList(Of ItemReference)()
        End Sub


        <XmlArray("items")>
        Public Property Items As BindingList(Of ItemReference)


    End Class

End Namespace

