Imports System.ComponentModel
Imports System.Xml.Serialization

<DebuggerDisplay("{Name} = '{Value}'")>
Public MustInherit Class Parameter(Of T)
    Inherits ParameterBase

    Private _innerText As String

    <XmlIgnore> _
    Protected ReadOnly Property InnerText As String
        Get
            If String.IsNullOrEmpty(_innerText) Then
                _innerText = ParameterHelper.CreateInnerText(Nodes)
            End If

            Return _innerText
        End Get
    End Property

    <XmlIgnore> _
    Public MustOverride Property Value As T

    Public Overrides Function EqualsByValue(param As ParameterBase) As Boolean
        Return param.GetType().Equals(Me.GetType()) AndAlso DirectCast(Me.Value, Object).Equals(DirectCast(DirectCast(param, Parameter(Of T)).Value, Object))
    End Function

    Protected Sub New()
        AddHandler PropertyChanged, AddressOf OnPropertyChangedHandler
    End Sub

    Private Sub OnPropertyChangedHandler(sender As Object, e As PropertyChangedEventArgs)
        If e.PropertyName = "Nodes" Then
            _innerText = String.Empty
        End If
    End Sub

End Class