Namespace QTI.Helpers.QTI_Base

    Public Class ResourceNameHelper

        Public Shared Function GetQtiCompliantResourceName(ByVal resourceName As String, Optional removeIllegalCharacters As Boolean = False) As String
            Dim result As String = resourceName
            If result IsNot Nothing Then
                If removeIllegalCharacters Then result = RemoveIllegalCharactersFromResourcename(result)
                Return result.Replace(Chr(32), "_"c)
            End If

            Return Nothing
        End Function

        Public Shared Function RemoveIllegalCharactersFromResourcename(resourceName As String) As String
            Dim result As String = resourceName
            result = result.Replace("%25", String.Empty)
            result = result.Replace("%20", String.Empty)
            Dim illegalChars As String = "<>""#%{}|\/^~[]';?:@=&^$+()!,`’*"
            If resourcename.IndexOfAny(illegalChars.ToCharArray()) > -1 Then
                For Each x As Char In illegalChars.ToCharArray()
                    result = result.Replace(x, String.Empty)
                Next
            End If
            Return result
        End Function

        Public Shared Function ResourcenameWithIllegalCharacters(resourceName As String) As String
            Dim result As String = resourceName
            Dim illegalChars As String = "<>""#{}|^~[]';?:@=&^$+()!,`’*"
            If resourcename.IndexOfAny(illegalChars.ToCharArray()) > -1 Then
                For Each x As Char In illegalChars.ToCharArray()
                    result = result.Replace(x, $"\{x}")
                Next
            End If
            Return result
        End Function

    End Class
End NameSpace