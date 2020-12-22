Imports System.Text.RegularExpressions
Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring

    Public Class PreProcessingHelper

        Public Shared Function UnCollapseStrToList(input As String) As List(Of String)
            Dim types = new List(Of String)

            Try
                Dim pattern As New Regex("\((?<types>.+?)\)")

                For Each match As Match In pattern.Matches(input)
                    types.AddRange(match.Groups("types").Value.Split(","c))
                Next
            Catch m As Exception
                Debug.Assert(False, m.Message)
            End Try

            Return types
        End Function

        Public Shared Function CreateUsablePreprocessorRules(types As List(Of String)) As List(Of IResponseKeyValuePreprocessor)
            Dim preProcessorRules = New List(Of IResponseKeyValuePreprocessor)

            types.ForEach(Sub(t)
                              Dim ppr = ResponseKeyValuePreProcessorFactory.Create(t)
                              If ppr IsNot Nothing AndAlso Not preProcessorRules.Contains(ppr) Then
                                  preProcessorRules.Add(ppr)
                              End If
                          End Sub)

            Return preProcessorRules
        End Function

        Public Shared Function GetPreprocessorRule(intendedType As String) As IResponseKeyValuePreprocessor
            Return ResponseKeyValuePreProcessorFactory.Create(intendedType)
        End Function

    End Class

End Namespace