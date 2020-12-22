Imports System.Diagnostics.CodeAnalysis
Imports System.Xml

<SuppressMessage("Microsoft.Design", "CA1058:TypesShouldNotExtendCertainBaseTypes", MessageId:="System.Xml.XmlDocument")> <SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")> <SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")> _
Public Class XmlResource
    Inherits XmlDocument




    Public Function ListSourceNames() As ArrayList
        Dim list As New ArrayList
        Dim nodes As XmlNodeList = Me.SelectNodes("//*[@src]")
        For Each node As XmlNode In nodes
            list.Add(node.Attributes("id").Value)
        Next

        Return list
    End Function


End Class