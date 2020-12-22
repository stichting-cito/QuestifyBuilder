Imports System.Xml

Public Class InteractionControlInfoCollection
    Inherits List(Of InteractionControlInfo)


    Private _internalControlInfoList As XmlNodeList




    Sub New(xmlControlInfoList As XmlNodeList)
        _internalControlInfoList = xmlControlInfoList
        For Each node As XmlNode In _internalControlInfoList
            Dim iControlInfo As New InteractionControlInfo(node)
            Trace.Assert(Not Me.Contains(iControlInfo.Id), String.Format("Duplicate 'Interaction Control' with id [{1}] (related Interaction Controller id=[{0}]).", iControlInfo.ControllerId, iControlInfo.Id))
            Me.Add(iControlInfo)
        Next
    End Sub




    Public Overloads ReadOnly Property Item(id As String) As InteractionControlInfo
        Get
            For Each iControlInfo As InteractionControlInfo In Me
                If iControlInfo.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase) Then
                    Return iControlInfo
                End If
            Next

            Return Nothing
        End Get
    End Property




    Public Overloads Function Contains(id As String) As Boolean
        Return Me.item(id) IsNot Nothing
    End Function



    Public Function GetXmlControlInfoList() As XmlNodeList
        Return _internalControlInfoList
    End Function


End Class