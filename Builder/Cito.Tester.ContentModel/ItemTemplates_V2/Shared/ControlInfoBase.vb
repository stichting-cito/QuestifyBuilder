Imports System.Xml

Public Class ControlInfoBase


    Private _internalControlInfo As XmlNode




    Public Sub New(XmlControlInfo As XmlNode)
        _internalControlInfo = XmlControlInfo
    End Sub




    Public Property Id As String
        Get
            Return Me.Setting("id")
        End Get
        Set
            Me.Setting("id") = value
        End Set
    End Property


    Public Property Setting(name As String) As String
        Get
            Dim attribute As XmlAttribute = _internalControlInfo.Attributes(name)

            If attribute IsNot Nothing Then
                Return attribute.Value
            Else
                Throw New ArgumentException($"Error getting settings for [{name}].", "Setting")
            End If
        End Get

        Set
            Dim attribute As XmlAttribute = _internalControlInfo.Attributes(name)

            If attribute IsNot Nothing Then
                attribute.Value = value
            Else
                Throw New ArgumentException($"Error setting settings for [{name}].", "Setting")
            End If
        End Set
    End Property


    Public Property Src As String
        Get
            Return Me.Setting("src")
        End Get
        Set
            Me.Setting("src") = value
        End Set
    End Property


    Public Property Type As String
        Get
            Return Me.Setting("type")
        End Get
        Set
            Me.Setting("type") = value
        End Set
    End Property





    Public Function GetXmlControlInfo() As XmlNode
        Return _internalControlInfo
    End Function


End Class