Namespace Logging


    Public Enum TraceCategory
        General
        [Error]
        StateManager
        ResourceManager
        ItemPicking
        ItemViewer
        RenderItem
        TestViewer
        ScoreModel
        TestResuming
        TestTakerInteraction
        Navigation
        Performance
    End Enum


    Public NotInheritable Class Log

        Private Sub New()

        End Sub


        Friend Shared Function EscapeString(text As String) As String
            Dim escapedText As String = text
            If String.IsNullOrEmpty(text) Then
                Return String.Empty
            End If

            If text.Contains("{") Then
                escapedText = escapedText.Replace("{", "{{")
            End If

            If text.Contains("}") Then
                escapedText = escapedText.Replace("}", "}}")
            End If


            Return escapedText
        End Function


        Public Shared Sub Indent()
            Trace.Indent()
        End Sub


        Public Shared Sub Unindent()
            Trace.Unindent()
        End Sub


        Public Shared Sub TraceInformation(category As TraceCategory, message As String)
            message = EscapeString(message)

            Trace.TraceInformation(message, category.ToString(), TraceEventType.Information.ToString())
        End Sub


        Public Shared Sub TraceInformation(category As TraceCategory, message As String, ByVal ParamArray args As Object())
            Dim formattedMessage As String = String.Format(message, args)
            formattedMessage = EscapeString(formattedMessage)
            Trace.TraceInformation(formattedMessage, category.ToString(), TraceEventType.Information.ToString())
        End Sub


        Public Shared Sub TraceError(category As TraceCategory, message As String)
            message = EscapeString(message)

            Trace.TraceError(message, category.ToString(), TraceEventType.Error.ToString())
        End Sub


        Public Shared Sub TraceError(category As TraceCategory, message As String, ByVal ParamArray args As Object())

            Dim formattedMessage As String = String.Format(message, args)
            formattedMessage = EscapeString(formattedMessage)

            Trace.TraceError(formattedMessage, category.ToString(), TraceEventType.Error.ToString())
        End Sub


        Public Shared Sub TraceWarning(category As TraceCategory, message As String)
            message = EscapeString(message)

            Trace.TraceWarning(message, category.ToString(), TraceEventType.Warning.ToString())
        End Sub


        Public Shared Sub TraceWarning(category As TraceCategory, message As String, ByVal ParamArray args As Object())
            Dim formattedMessage As String = String.Format(message, args)
            formattedMessage = EscapeString(formattedMessage)

            Trace.TraceWarning(formattedMessage, category.ToString(), TraceEventType.Warning.ToString())
        End Sub


        Public Shared Sub TraceInformation(category As TraceCategory, value As Object)
            Trace.TraceInformation(value.ToString(), category.ToString(), TraceEventType.Information.ToString())
        End Sub


        Public Shared Sub TraceError(category As TraceCategory, value As Object)
            Trace.TraceError(value.ToString(), category.ToString(), TraceEventType.Error.ToString())
        End Sub


        Public Shared Sub TraceWarning(category As TraceCategory, value As Object)
            Trace.TraceWarning(value.ToString(), category.ToString(), TraceEventType.Warning.ToString())
        End Sub


        Public Shared Function GetListenerByType(listenerType As Type) As TraceListener
            For Each listener As TraceListener In Trace.Listeners
                If listener.GetType.Equals(listenerType) Then
                    Return listener
                End If
            Next

            Return Nothing
        End Function


        Public Shared Sub Clear()
            For Each listener As TraceListener In Trace.Listeners
                If TypeOf listener Is ILogListener Then
                    DirectCast(listener, ILogListener).Clear()
                End If
            Next
            Trace.IndentLevel = 0
        End Sub

    End Class

End Namespace
