
#If CF Then
Imports System.Data.SqlServerCe
#Else
#End If

Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.DatabaseSpecific

    Partial Public Class DataAccessAdapter

        Protected Overrides Sub OnBeforeEntitySave(ByVal entitySaved As IEntity2, ByVal insertAction As Boolean)
            MyBase.OnBeforeEntitySave(entitySaved, insertAction)

            DataAccessAdapterExecuter.PerformModificationsOnEntity(entitySaved)

            insertAction = True
        End Sub

    End Class
End Namespace