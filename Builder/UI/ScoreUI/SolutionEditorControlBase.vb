Imports System.ComponentModel
Imports System.Diagnostics.CodeAnalysis
Imports System.Xml

Public Class SolutionEditorControlBase


    Private _controllerInfo As XmlNode
    Private _controlInfo As XmlNodeList



    <SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId:="System.Xml.XmlNode")> _
    Public Property ControllerInfo() As XmlNode
        Get
            Return _controllerInfo
        End Get
        Set(value As XmlNode)
            _controllerInfo = value

            If CanInitEditor() Then
                Me.InitEditor()
            End If
        End Set
    End Property

    Public Property ControlInfo() As XmlNodeList
        Get
            Return _controlInfo
        End Get
        Set(value As XmlNodeList)
            _controlInfo = value

            If CanInitEditor() Then
                Me.InitEditor()
            End If
        End Set
    End Property



    Protected Overridable Function CanInitEditor() As Boolean
        Return Me.ControllerInfo IsNot Nothing AndAlso Me.ControlInfo IsNot Nothing
    End Function


    <Description("This event will be raised when data is changed on this control"), Category("KeyFindingEditorControlBase events")> _
    Public Event DataChanged As EventHandler(Of EventArgs)

    <SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")> _
    Public Overridable Sub BuildSolution()
        Throw New NotImplementedException("BuildSolution is not implemented")
    End Sub


    <SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")> _
    Public Overridable Function GetRawScore() As Integer
        Throw New NotImplementedException("GetRawScore is not implemented!")
    End Function

    <SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")> _
    Protected Overridable Sub InitEditor()
        Throw New NotImplementedException("InitEditor is not implemented InitEditor")
    End Sub

    Protected Sub OnDataChanged(e As EventArgs)
        RaiseEvent DataChanged(Me, e)
    End Sub

End Class
