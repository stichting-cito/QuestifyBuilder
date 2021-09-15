Imports System.Linq
Imports System.Runtime.CompilerServices
Imports Cito.Tester.ContentModel

Namespace ContentModel

    Public Module AspectExtensions

        <Extension>
        Public Function AspectScoreIsTranslated(aspect As Aspect) As Boolean
            If Not aspect.AspectScoreTranslationTableSpecified Then
                Return False
            End If
            Return aspect.AspectScoreTranslationTable.Any(Function(i) Not Convert.ToDouble(i.RawScore).Equals(i.TranslatedScore))
        End Function

    End Module

End Namespace
