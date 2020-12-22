Imports Cito.Tester.Common

Public Class DictionaryTransaction
    Inherits TransactionBase


    Private _action As DictionaryActionType
    Private _attribute As String
    Private _key As String
    Private _value As String



    Public Sub New(component As TestComponentBase, attribute As String, key As String, value As String, action As DictionaryActionType)
        MyBase.New(component)
        If component IsNot Nothing Then
            If Not String.IsNullOrEmpty(attribute) AndAlso Not String.IsNullOrEmpty(key) AndAlso Not String.IsNullOrEmpty(value) Then
                Me.Id = component.Identifier
                _attribute = attribute
                _key = key
                _value = value
                _action = action
            Else
                Throw New TestViewerException(My.Resources.Error_DictionaryTransaction_ParametersNotSet)
            End If
        Else
            Throw New TestViewerException(My.Resources.Error_DictionaryTransaction_ParametersNotSet)
        End If
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub



    Public Property Action As DictionaryActionType
        Get
            Return _action
        End Get
        Set
            _action = value
        End Set
    End Property

    Public Property Attribute As String
        Get
            Return _attribute
        End Get
        Set
            _attribute = value
        End Set
    End Property

    Public Property Key As String
        Get
            Return _key
        End Get
        Set
            _key = value
        End Set
    End Property

    Public Property Value As String
        Get
            Return _value
        End Get
        Set
            _value = value
        End Set
    End Property



    Public Enum DictionaryActionType
        Add
        Change
        Remove
    End Enum


End Class