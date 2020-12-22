Imports System.Collections.Generic

Namespace CustomClasses

    Public Class CachingWithOptionsConverter

        Enum RequestOptions
            withDependencies = 1
            withReferences = 2
            withCustomProperties = 3
            withUserInfo = 4
            withState = 5
            withHiddenResources = 6
        End Enum

        Public Shared Function WithOptionsToArray(allOptions As Boolean) As List(Of Int16)
            Return WithOptionsToArray(allOptions, allOptions, allOptions, allOptions, allOptions, allOptions)
        End Function

        Public Shared Function WithOptionsToArray(withDependencies As Boolean, withCustomProperties As Boolean) As List(Of Int16)
            Return WithOptionsToArray(withDependencies, False, withCustomProperties, False, False, False)
        End Function

        Public Shared Function WithOptionsToArray(withDependencies As Boolean, withReferences As Boolean, withCustomProperties As Boolean, withUserInfo As Boolean, withState As Boolean, withHiddenResources As Boolean) As List(Of Int16)
            Dim result As New List(Of Int16)

            If withDependencies Then
                result.Add(CShort(RequestOptions.withDependencies))
            End If
            If withReferences Then
                result.Add(CShort(RequestOptions.withReferences))
            End If
            If withCustomProperties Then
                result.Add(CShort(RequestOptions.withCustomProperties))
            End If
            If withUserInfo Then
                result.Add(CShort(RequestOptions.withUserInfo))
            End If
            If withState Then
                result.Add(CShort(RequestOptions.withState))
            End If
            If withHiddenResources Then
                result.Add(CShort(RequestOptions.withHiddenResources))
            End If

            Return result
        End Function

    End Class

End Namespace