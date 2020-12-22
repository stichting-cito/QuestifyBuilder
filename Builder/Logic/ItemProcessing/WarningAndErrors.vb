Namespace ItemProcessing

    Public Class WarningsAndErrors
        Private ReadOnly _errorList As New List(Of String)
        Private ReadOnly _warningList As New List(Of String)
        Private _subWarningsAndErrors As WarningsAndErrors

        Public ReadOnly Property Errors() As Boolean
            Get
                Return _errorList.Count > 0 OrElse (Me.SubWarningsAndErrors IsNot Nothing AndAlso Me.SubWarningsAndErrors.Errors)
            End Get
        End Property

        Public ReadOnly Property Warnings() As Boolean
            Get
                Return _warningList.Count > 0 OrElse (Me.SubWarningsAndErrors IsNot Nothing AndAlso Me.SubWarningsAndErrors.Warnings)
            End Get
        End Property

        Public ReadOnly Property ErrorList() As List(Of String)
            Get
                Return _errorList
            End Get
        End Property

        Public ReadOnly Property WarningList() As List(Of String)
            Get
                Return _warningList
            End Get
        End Property

        Public Property SubWarningsAndErrors() As WarningsAndErrors
            Get
                Return _subWarningsAndErrors
            End Get
            Set(ByVal value As WarningsAndErrors)
                _subWarningsAndErrors = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return GetMessage(String.Empty)
        End Function

        Public Sub Merge(ByVal warnerrors As WarningsAndErrors)
            Me.ErrorList.AddRange(warnerrors.ErrorList)
            Me.WarningList.AddRange(warnerrors.WarningList)
        End Sub

        Private Function GetMessage(ByVal indent As String) As String
            Dim message As New System.Text.StringBuilder()

            If Me.Errors Then
                For Each err As String In Me.ErrorList
                    message.AppendLine(indent + err)
                Next

                If Me.SubWarningsAndErrors IsNot Nothing AndAlso Me.SubWarningsAndErrors.Errors Then
                    indent += vbTab
                    message.AppendLine(indent + My.Resources.ItemProcessing.ErrorsFoundInCollectionparameter)
                    message.Append(Me.SubWarningsAndErrors.GetMessage(indent))
                End If
            ElseIf Me.Warnings Then
                For Each warn As String In Me.WarningList
                    message.AppendLine(indent + warn)
                Next

                If Me.SubWarningsAndErrors IsNot Nothing AndAlso Me.SubWarningsAndErrors.Warnings Then
                    indent += vbTab
                    message.AppendLine(indent + My.Resources.ItemProcessing.WarningsFoundInCollectionparameter)
                    message.Append(Me.SubWarningsAndErrors.GetMessage(indent))
                End If
            End If

            Return message.ToString()
        End Function

    End Class

End Namespace
