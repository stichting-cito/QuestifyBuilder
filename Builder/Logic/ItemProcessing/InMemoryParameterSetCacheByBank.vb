Imports Cito.Tester.ContentModel

Namespace ItemProcessing

    <CLSCompliant(True)> _
    Public Class InMemoryParameterSetCacheByBank : Implements IITemSetupCacheHelper


        Private ReadOnly _cachedParameterSetCollection As Dictionary(Of String, ParameterSetCollection)
        Private ReadOnly _cachedIsTransformed As Dictionary(Of String, Boolean)


        Sub New(bankid As Integer)
            Me.BankId = bankid
            _cachedParameterSetCollection = New Dictionary(Of String, ParameterSetCollection)()
            _cachedIsTransformed = New Dictionary(Of String, Boolean)
        End Sub

        Public ReadOnly Property BankId As Integer

        Public Function GetCachedExtractedParameters(iltName As String) As ParameterSetCollection Implements IITemSetupCacheHelper.GetCachedExtractedParameters
            If (_cachedParameterSetCollection.ContainsKey(iltName)) Then
                Return _cachedParameterSetCollection(iltName)
            End If
            Return Nothing
        End Function

        Public Function GetCachedIsTransformed(iltName As String) As Boolean? Implements IITemSetupCacheHelper.GetCachedIsTransformed
            If (_cachedIsTransformed.ContainsKey(iltName)) Then
                Return _cachedIsTransformed(iltName)
            End If
            Return Nothing
        End Function


        Public Sub ReadyForCaching(iltName As String, extractedParameters As ParameterSetCollection, isTransformed As Boolean) Implements IITemSetupCacheHelper.ReadyForCaching
            If (Not _cachedParameterSetCollection.ContainsKey(iltName)) Then
                _cachedParameterSetCollection.Add(iltName, extractedParameters)
            End If
            If (Not _cachedIsTransformed.ContainsKey(iltName)) Then
                _cachedIsTransformed.Add(iltName, isTransformed)
            End If
        End Sub

    End Class
End Namespace
