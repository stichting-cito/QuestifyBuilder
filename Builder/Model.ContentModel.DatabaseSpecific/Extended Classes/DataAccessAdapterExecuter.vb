Imports Questify.Builder.Security
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports System.Configuration
Imports Extended_Classes

Namespace Questify.Builder.Model.ContentModel.DatabaseSpecific

    Public NotInheritable Class DataAccessAdapterExecuter

        Public Shared Sub PerformModificationsOnEntity(ByVal entitySaved As IEntity2)
            If entitySaved.Fields.Count > 0 AndAlso String.Compare(entitySaved.Fields(0).ContainingObjectName, "ResourceHistoryEntity", True) <> 0 Then
                If entitySaved.IsDirty Then
                    Dim userId As Integer = 0

                    If TypeOf System.Threading.Thread.CurrentPrincipal Is TestBuilderPrincipal Then
                        Dim principal As TestBuilderPrincipal = CType(System.Threading.Thread.CurrentPrincipal, TestBuilderPrincipal)
                        Dim identity As TestBuilderIdentity = CType(principal.Identity, TestBuilderIdentity)
                        userId = identity.UserId
                    Else
                        userId = 1
                    End If

                    If entitySaved.GetFieldByName("ModifiedBy") IsNot Nothing Then entitySaved.SetNewFieldValue("ModifiedBy", userId)
                    If entitySaved.GetFieldByName("ModifiedDate") IsNot Nothing Then entitySaved.SetNewFieldValue("ModifiedDate", DateTime.Now())

                    If entitySaved.IsNew Then
                        Dim nameFileValue As IEntityFieldCore = entitySaved.GetFieldByName("Name")
                        If nameFileValue IsNot Nothing AndAlso nameFileValue.CurrentValue IsNot Nothing Then entitySaved.SetNewFieldValue("Name", nameFileValue.CurrentValue.ToString().Trim())

                        If entitySaved.GetFieldByName("CreatedBy") IsNot Nothing Then entitySaved.SetNewFieldValue("CreatedBy", userId)
                        If entitySaved.GetFieldByName("CreationDate") IsNot Nothing Then entitySaved.SetNewFieldValue("CreationDate", DateTime.Now())
                    End If

                    Dim currentMajorVersion As Integer = 0
                    Dim currentMinorVersion As Integer = 0

                    If entitySaved.Fields("Version") IsNot Nothing AndAlso entitySaved.Fields("Version").CurrentValue IsNot Nothing Then
                        If VersionHelper.TryParseVersion(entitySaved.Fields("Version").CurrentValue.ToString(), currentMajorVersion, currentMinorVersion) Then
                            If Not entitySaved.IsNew Then currentMinorVersion += 1
                        ElseIf Integer.TryParse(entitySaved.Fields("Version").CurrentValue.ToString(), currentMajorVersion) Then
                            currentMinorVersion = 0
                        ElseIf Not entitySaved.GetType().ToString().Equals("Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity", StringComparison.OrdinalIgnoreCase) AndAlso String.IsNullOrEmpty(entitySaved.Fields("Version").CurrentValue.ToString()) Then
                            currentMinorVersion = 1
                        End If
                    Else
                        Dim doUseItemId As Boolean
                        Boolean.TryParse(ConfigurationManager.AppSettings("UseItemId"), doUseItemId)
                        If entitySaved.GetType().ToString().Equals("Questify.Builder.Model.ContentModel.EntityClasses.ItemResourceEntity", StringComparison.OrdinalIgnoreCase) AndAlso doUseItemId Then
                        Else
                            currentMinorVersion += 1
                        End If
                    End If

                    entitySaved.SetNewFieldValue("Version", VersionHelper.CreateVersionString(currentMajorVersion, currentMinorVersion))
                End If
            End If
        End Sub

    End Class

End Namespace