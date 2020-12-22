Imports System.Xml.Serialization

<Serializable> _
Public Class ControlTemplateTarget
    Inherits TargetBase


    Private _parameterSet As New ParameterCollection
    Private _template As CDATA



    Public Sub New(name As String, template As String)
        MyBase.New(name)
        Me.Template = New CDATA(template)
    End Sub

    Public Sub New(name As String, description As String, template As String)
        MyBase.New(name, description)
        Me.Template = New CDATA(template)
    End Sub


    Public Sub New()
        MyBase.New()
    End Sub



    <XmlElement("ParameterSet")> _
    Public Property ParameterSet As ParameterCollection
        Get
            Return Me._parameterSet
        End Get
        Set
            Me._parameterSet = value
        End Set
    End Property


End Class

