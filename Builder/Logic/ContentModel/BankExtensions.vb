Imports System.Runtime.CompilerServices
Imports Questify.Builder.Logic.Common
Imports Questify.Builder.Logic.Service.Interfaces
Imports Questify.Builder.Logic.Service.Factories

Namespace ContentModel

    Public Module BankExtensions

        <Extension>
        Public Function IsResourceNameTaken(bankId As Integer, newResourceName As String) As Boolean
            If (String.IsNullOrEmpty(newResourceName)) Then Throw New ArgumentNullException(nameof(newResourceName))
            Return ResourceFactory.Instance.ResourceExists(bankId, newResourceName, True)
        End Function

    End Module

End Namespace

