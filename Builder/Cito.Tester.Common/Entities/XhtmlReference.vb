Imports System.ComponentModel

Public Enum XhtmlReferenceType
    None
    Element
    ReferTo
    Symbol
    Highlight
End Enum

Public Class XhtmlReference
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged


    Private _description As String
    Private _id As String
    Private _type As XhtmlReferenceType
    Private _value As String



    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
            OnPropertyChanged(New PropertyChangedEventArgs("Description"))
        End Set
    End Property

    Public Property ID() As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
            OnPropertyChanged(New PropertyChangedEventArgs("ID"))
        End Set
    End Property

    Public Property Type() As XhtmlReferenceType
        Get
            Return _type
        End Get
        Set(value As XhtmlReferenceType)
            _type = value
            OnPropertyChanged(New PropertyChangedEventArgs("Type"))
        End Set
    End Property


    Public Property Value() As String
        Get
            Return _value
        End Get
        Set(value As String)
            _value = value
            OnPropertyChanged(New PropertyChangedEventArgs("Value"))
        End Set
    End Property


    Public Sub New()
    End Sub

    Public Sub New(ID As String, type As XhtmlReferenceType, description As String, value As String)
        Me.ID = ID
        Me.Type = type
        Me.Description = description
        Me.Value = value
    End Sub

    Protected Overridable Sub OnPropertyChanged(e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub

End Class