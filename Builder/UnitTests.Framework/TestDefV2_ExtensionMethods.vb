Imports System.Runtime.CompilerServices
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Public Module TestDefV2_ExtensionMethods

    <Extension()>
    Public Function ConvertTo(Of T As {New, AssessmentTestViewBase})(test As AssessmentTest2) As T
        If test Is Nothing Then Throw New ArgumentException("test")
        Return AssessmentTestv2Factory.CreateView(Of T)(test)
    End Function

End Module
