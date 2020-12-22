Imports Cito.Tester.ContentModel

Namespace HelperClasses

    Public Class ParameterHelper

        Public Shared Function GetValue(ByVal param As ParameterBase) As String
            If param Is Nothing Then
                Return String.Empty
            End If

            If param.Nodes IsNot Nothing Then
                If param.Nodes.Length = 1 Then
                    Return param.Nodes(0).OuterXml.Trim()
                Else
                    Return param.ToString().Trim()
                End If
            End If

            Return String.Empty
        End Function

    End Class
End Namespace