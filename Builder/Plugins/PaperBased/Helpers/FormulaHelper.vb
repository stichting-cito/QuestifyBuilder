Imports System.Globalization
Imports System.Threading

Public Class FormulaHelper

    Private ReadOnly _cultureInfo As CultureInfo
    Private ReadOnly _properties As New Dictionary(Of String, String)()

    Public Sub New(Optional ByVal cultureInfo As CultureInfo = Nothing)
        If cultureInfo Is Nothing Then
            _cultureInfo = Thread.CurrentThread.CurrentUICulture
        Else
            _cultureInfo = cultureInfo
        End If

        If IsNumberDecimalSeparatorComma() Then
            _properties("grammar") = "grammar_comma_content.txt"
        End If
    End Sub

    Private ReadOnly Property IsNumberDecimalSeparatorComma() As Boolean
        Get
            If _cultureInfo Is Nothing Then
                Return Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator = ","
            Else
                Return _cultureInfo.NumberFormat.NumberDecimalSeparator = ","
            End If
        End Get
    End Property

End Class