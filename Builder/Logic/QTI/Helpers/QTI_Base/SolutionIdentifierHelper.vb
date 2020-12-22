Namespace QTI.Helpers.QTI_Base

    Public Class SolutionIdentifierHelper

        Private ReadOnly _strippedSolutionIds As Dictionary(Of String, String)

        Public Sub New()
            _strippedSolutionIds = New Dictionary(Of String, String)
        End Sub

        Public Function GetStrippedId(id As String, Optional stripPrefixForGapsToo As Boolean = False) As String
            If String.IsNullOrEmpty(id) Then Return id
            Dim result As String = String.Empty

            If _strippedSolutionIds.ContainsKey(id) Then
                result = _strippedSolutionIds(id)
            Else
                result = IdentifierHelper.GetStrippedId(id)
                _strippedSolutionIds.Add(id, result)
            End If
            If stripPrefixForGapsToo AndAlso result.Contains("-I") Then result = result.Substring(result.IndexOf("-I") + 1)

            Return result
        End Function

    End Class

End NameSpace