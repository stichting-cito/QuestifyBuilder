Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Cloning

Public Class TestPackageResourceCloner
    Inherits BaseResourceCloner

    Sub New(item As ResourceEntity)
        MyBase.New(item)
    End Sub

    Public Overrides Sub DoSpecificClone(originalResource As ResourceEntity)
    End Sub
End Class
