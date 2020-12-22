Imports Cito.Tester.Common
Imports Questify.Builder.Logic.ResourceManager

Public Class DbResourceManagerFactory
    Implements IResourceManagerFactory

    Private ReadOnly _bankId As Integer

    Public Sub New(bankId As Integer)
        _bankId = bankId
    End Sub

    Public Function Create() As IResourceManagerWrapper Implements IResourceManagerFactory.Create
        Return New DatabaseResourceManagerWrapper(_bankId, True)
    End Function
End Class
