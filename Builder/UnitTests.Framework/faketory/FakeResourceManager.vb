Imports Cito.Tester.Common
Imports FakeItEasy
Imports Questify.Builder.Logic.ResourceManager

Namespace Faketory

#If Not CONTENTMODEL Then

#End If
    Public Class FakeResourceManager

#If Not CONTENTMODEL Then
        Public Shared Function Make() As DataBaseResourceManager
            Dim dbMan As DataBaseResourceManager = A.Fake(Of DataBaseResourceManager)(Function(x) New DataBaseResourceManager(1))
            Return dbMan
        End Function
#End If

        Public Shared Function MakeResourceManagerBase() As ResourceManagerBase
            Return A.Fake(Of ResourceManagerBase)()
        End Function

    End Class
End Namespace