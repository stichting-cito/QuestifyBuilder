Imports System.Xml.Serialization

<XmlRoot("ItemExclusionList", [Namespace]:="http://Cito.Tester.Common/xml/serialization", IsNullable:=True), _
Serializable()> _
Public Class ItemExclusionList


    Private _itemInfo As New List(Of ItemInfo)



    Public Property ItemInfo() As List(Of ItemInfo)
        Get
            Return _itemInfo
        End Get
        Set(value As List(Of ItemInfo))
            _itemInfo = value
        End Set
    End Property


End Class