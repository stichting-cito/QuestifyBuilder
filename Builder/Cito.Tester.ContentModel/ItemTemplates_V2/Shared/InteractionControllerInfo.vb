Imports System.Xml

Public Class InteractionControllerInfo
    Inherits ControlInfoBase


    Private _interactionControls As InteractionControlInfoCollection



    Public Sub New(xmlControlInfo As XmlNode, xmlControlInfoList As XmlNodeList)
        MyBase.New(xmlControlInfo)
        Me._interactionControls = New InteractionControlInfoCollection(xmlControlInfoList)
    End Sub




    Public Property Designersrc As String
        Get
            Return Me.Setting("designersrc")
        End Get
        Set
            Me.Setting("designersrc") = value
        End Set
    End Property


    Public Property Designertype As String
        Get
            Return Me.Setting("designertype")
        End Get
        Set
            Me.Setting("designertype") = value
        End Set
    End Property


    Public ReadOnly Property InteractionControls As InteractionControlInfoCollection
        Get
            Return _interactionControls
        End Get
    End Property


End Class