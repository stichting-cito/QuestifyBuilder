Imports System.Xml


Public Class InteractionControlInfo
    Inherits ControlInfoBase



    Public Sub New(xmlControlInfo As XmlNode)
        MyBase.New(xmlControlInfo)
    End Sub




    Public Property ControllerId As String
        Get
            Return Me.Setting("controller")
        End Get
        Set
            Me.Setting("controller") = value
        End Set
    End Property


End Class