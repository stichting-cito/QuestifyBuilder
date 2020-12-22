Imports System.Reflection
Imports Cito.Tester.Common

Public Class AttributeTransaction
    Inherits TransactionBase


    Private _attributeName As String
    Private _value As Object



    Public Sub New(item As ItemReferenceViewBase, attribute As String)
        Me.New(item, attribute, False)
    End Sub

    Public Sub New(item As ItemReferenceViewBase, attribute As String, useIdentifier As Boolean)
        MyBase.New(item)
        If item IsNot Nothing Then
            If Not String.IsNullOrEmpty(attribute) Then
                If useIdentifier Then
                    Me.Id = $"identifier:{item.Identifier}"
                Else
                    Me.Id = item.SourceName
                End If

                Me.AttributeName = attribute

                Dim propertyInfo As PropertyInfo = item.GetType.GetProperty(attribute, BindingFlags.GetProperty Or BindingFlags.Instance Or BindingFlags.Public)
                Me.Value = propertyInfo.GetValue(item, Nothing)
            Else
                Throw New TestViewerException(My.Resources.Error_AttributeTransaction_ParametersNotSet)
            End If
        Else
            Throw New TestViewerException(My.Resources.Error_AttributeTransaction_ParametersNotSet)
        End If
    End Sub

    Public Sub New(section As TestSectionViewBase, attribute As String)
        MyBase.New(section)
        If section IsNot Nothing Then
            If Not String.IsNullOrEmpty(attribute) Then
                Me.Id = section.Identifier
                Me.AttributeName = attribute

                Dim propertyInfo As PropertyInfo = section.GetType.GetProperty(attribute, BindingFlags.GetProperty Or BindingFlags.Instance Or BindingFlags.Public)
                Me.Value = propertyInfo.GetValue(section, Nothing)
            Else
                Throw New TestViewerException(My.Resources.Error_AttributeTransaction_ParametersNotSet)
            End If
        Else
            Throw New TestViewerException(My.Resources.Error_AttributeTransaction_ParametersNotSet)
        End If
    End Sub

    Public Sub New(part As TestPartViewBase, attribute As String)
        MyBase.New(part)
        If part IsNot Nothing Then
            If Not String.IsNullOrEmpty(attribute) Then
                Me.Id = part.Identifier
                Me.AttributeName = attribute

                Dim propertyInfo As PropertyInfo = part.GetType.GetProperty(attribute, BindingFlags.GetProperty Or BindingFlags.Instance Or BindingFlags.Public)
                Me.Value = propertyInfo.GetValue(part, Nothing)
            Else
                Throw New TestViewerException(My.Resources.Error_AttributeTransaction_ParametersNotSet)
            End If
        Else
            Throw New TestViewerException(My.Resources.Error_AttributeTransaction_ParametersNotSet)
        End If
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub



    Public Property AttributeName As String
        Get
            Return _attributeName
        End Get
        Set
            _attributeName = value
        End Set
    End Property

    Public Property Value As Object
        Get
            Return _value
        End Get
        Set
            _value = value
        End Set
    End Property


End Class