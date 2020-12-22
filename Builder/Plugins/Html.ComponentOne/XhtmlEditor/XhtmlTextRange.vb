Imports System.Xml
Imports C1.Win.C1Editor
Imports Questify.Builder.Logic

Public Class XhtmlTextRange
    Implements ITextRange

    Protected ReadOnly _innerSelection As Selection
    Protected ReadOnly _innerEditor As C1Editor

    Public Sub New(editor As C1Editor)
        _innerSelection = editor.Selection
        _innerEditor = editor
    End Sub

    Public ReadOnly Property Node As XmlNode Implements ITextRange.Node
        Get
            If _innerSelection Is Nothing Then
                Return Nothing
            End If
            Return _innerSelection.Node
        End Get
    End Property
    Public Property Text As String Implements ITextRange.Text
        Get
            If _innerSelection Is Nothing Then
                Return String.Empty
            End If
            Return _innerSelection.Text
        End Get
        Set(value As String)
            Try
                _innerSelection.Text = value
            Catch ex As ArgumentOutOfRangeException
            End Try
        End Set
    End Property

    Public ReadOnly Property [End] As XmlNode Implements ITextRange.[End]
        Get
            If _innerSelection Is Nothing Then
                Return Nothing
            End If
            Return _innerSelection.End.Node
        End Get
    End Property

    Public ReadOnly Property Start As XmlNode Implements ITextRange.Start
        Get
            If _innerSelection Is Nothing Then
                Return Nothing
            End If
            Return _innerSelection.Start.Node
        End Get

    End Property

    Public Property XmlText As String Implements ITextRange.XmlText
        Get
            If _innerSelection Is Nothing Then
                Return Nothing
            End If
            Dim result As String
            Try
                result = _innerSelection.XmlText
            Catch ex As Exception
                result = String.Empty
            End Try
            Return result
        End Get
        Set
            If _innerSelection Is Nothing Then
                Return
            End If
            Try
                _innerSelection.XmlText = Value
            Catch ex As ArgumentOutOfRangeException
            End Try

        End Set
    End Property


    Public Sub SetXmlElement(element As XmlElement) Implements ITextRange.SetXmlElement
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.SetXmlElement(element)
    End Sub

    Public Sub RemoveStyle(propertyName As String, propertyValue As String) Implements ITextRange.RemoveStyle
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.RemoveStyle(propertyName, propertyValue)
    End Sub

    Public Sub RemoveTag(tagName As String) Implements ITextRange.RemoveTag
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.RemoveTag(tagName)
    End Sub

    Public Sub ApplyStyle(propertyName As String, propertyValue As String) Implements ITextRange.ApplyStyle
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.ApplyStyle(propertyName, propertyValue)
    End Sub

    Public Sub RemoveClass(className As String) Implements ITextRange.RemoveClass
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.RemoveClass(className)
    End Sub

    Public Sub ApplyClass(className As String) Implements ITextRange.ApplyClass
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.ApplyClass(className)
    End Sub

    Public Sub ApplyClass(className As String, type As StyleType) Implements ITextRange.ApplyClass
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.ApplyClass(className, type)
    End Sub

    Public Sub ApplyTag(tagName As String) Implements ITextRange.ApplyTag
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.ApplyTag(tagName)
    End Sub

    Public Sub ClearFormatting() Implements ITextRange.ClearFormatting
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.ClearFormatting()
    End Sub

    Public Sub MoveTo(node As XmlNode) Implements ITextRange.MoveTo
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.MoveTo(node)
    End Sub

    Public Sub Move(offset As Integer, length As Integer) Implements ITextRange.Move
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.Move(offset, length)
    End Sub

    Public Sub [Select]() Implements ITextRange.[Select]
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.Select()
    End Sub

    Public Sub Normalize() Implements ITextRange.Normalize
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.Normalize()
    End Sub

    Public Sub Trim() Implements ITextRange.Trim
        If _innerSelection Is Nothing Then
            Return
        End If

        _innerSelection.Trim()
    End Sub

    Public Function IsTagApplied(tagName As String) As Boolean Implements ITextRange.IsTagApplied
        If _innerSelection Is Nothing Then
            Return Nothing
        End If

        Return _innerSelection.IsTagApplied(tagName)
    End Function

    Public Function IsClassApplied(className As String) As Boolean Implements ITextRange.IsClassApplied
        If _innerSelection Is Nothing Then
            Return Nothing
        End If

        Return _innerSelection.IsClassApplied(className)
    End Function

    Public Function IsStyleApplied(propertyName As String) As Boolean Implements ITextRange.IsStyleApplied
        If _innerSelection Is Nothing Then
            Return Nothing
        End If

        Return _innerSelection.IsStyleApplied(propertyName)
    End Function

    Public Function GetStyleValue(propertyName As String) As String Implements ITextRange.GetStyleValue
        If _innerSelection Is Nothing Then
            Return Nothing
        End If

        Return _innerSelection.GetStyleValue(propertyName)
    End Function

End Class
