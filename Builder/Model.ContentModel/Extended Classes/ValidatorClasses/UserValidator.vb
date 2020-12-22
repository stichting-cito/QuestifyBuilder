Imports Enums
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.Security
Imports SD.LLBLGen.Pro.ORMSupportClasses

Namespace Questify.Builder.Model.ContentModel.ValidatorClasses

    Partial Public Class UserValidator

        Public Overrides Sub ValidateEntityBeforeSave(ByVal involvedEntity As SD.LLBLGen.Pro.ORMSupportClasses.IEntityCore)
            Dim user As EntityClasses.UserEntity = DirectCast(involvedEntity, EntityClasses.UserEntity)

            If user.IsNew OrElse user.Fields(UserFieldIndex.UserName).IsChanged Then
                Dim results As EntityCollection = GetUniqueUsernameValidationResults(user, user.UserName)
                If results.Count > 0 Then
                    Throw New ORMEntityValidationException(String.Format(My.Resources.User_Not_Unique, user.UserName), involvedEntity)
                End If
            End If
            If Not String.IsNullOrEmpty(user.Password) Then
                If (user.AuthenticationType = AuthenticationType.Default.ToString()) Then
                    If Not (user.Password.StartsWith(PasswordHashing.Prefix))
                        user.Password = PasswordHashing.CreateHash(user.Password)
                    End If
                End If
            End If
            If TypeOf My.User.CurrentPrincipal Is TestBuilderPrincipal Then
                Dim curUserId = DirectCast(My.User.CurrentPrincipal.Identity(), TestBuilderIdentity).UserId
                If user.Fields(UserFieldIndex.Password).IsChanged AndAlso Not user.Id.Equals(curUserId) Then
                    user.ChangePassword = True
                End If
            End If
        End Sub

        Public Overrides Function ValidateFieldValue(ByVal involvedEntity As IEntityCore, ByVal fieldIndex As Integer, ByVal value As Object) As Boolean
            Dim toReturn As Boolean = True
            Select Case CType(fieldIndex, UserFieldIndex)
                Case UserFieldIndex.Password
                    If String.IsNullOrEmpty(CType(value, String)) Then
                        involvedEntity.SetEntityFieldError("Password", My.Resources.PasswordIsRequired, False)
                        involvedEntity.SetEntityError(My.Resources.PasswordIsRequired)
                        toReturn = False
                    Else
                        involvedEntity.SetEntityError(String.Empty)
                    End If
                Case UserFieldIndex.UserName
                    If String.IsNullOrEmpty(CType(value, String)) Then
                        involvedEntity.SetEntityFieldError("UserName", My.Resources.UsernameIsRequired, False)
                        involvedEntity.SetEntityError(My.Resources.UsernameIsRequired)
                        toReturn = False
                    ElseIf ValidateUnigueUsername(involvedEntity, value) Then
                        involvedEntity.SetEntityFieldError("UserName", String.Format(My.Resources.User_Not_Unique, value), False)
                        involvedEntity.SetEntityError(My.Resources.User_Not_Unique)
                        toReturn = False
                    Else
                        involvedEntity.SetEntityError(String.Empty)
                    End If
                Case UserFieldIndex.FullName
                    If String.IsNullOrEmpty(CType(value, String)) Then
                        involvedEntity.SetEntityFieldError("FullName", My.Resources.CodeIsRequired, False)
                        involvedEntity.SetEntityError(My.Resources.CodeIsRequired)
                        toReturn = False
                    Else
                        involvedEntity.SetEntityError(String.Empty)
                    End If
            End Select

            Return toReturn
        End Function

        Private Function ValidateUnigueUsername(ByVal involvedEntity As IEntityCore, ByVal value As Object) As Boolean
            Dim user As UserEntity = DirectCast(involvedEntity, UserEntity)
            Dim results As EntityCollection = GetUniqueUsernameValidationResults(user, value.ToString())
            If results.Count > 0 Then
                Dim resultEntity = CType(results(0), CommonEntityBase)
                If resultEntity.ObjectId <> user.ObjectId Then
                    Return True
                End If
            End If

            Return False
        End Function

        Private Function GetUniqueUsernameValidationResults(ByVal user As UserEntity, ByVal username As String) As EntityCollection
            Dim adapter As New DatabaseSpecific.DataAccessAdapter()
            adapter.StartTransaction(IsolationLevel.ReadUncommitted, Guid.NewGuid.ToString().Substring(0, 32))

            Dim filter As IRelationPredicateBucket = New RelationPredicateBucket()
            With filter
                .PredicateExpression.AddWithAnd(HelperClasses.UserFields.UserName = username)
            End With

            Dim results As New HelperClasses.EntityCollection(New FactoryClasses.UserEntityFactory)

            Try
                adapter.FetchEntityCollection(results, filter)
                adapter.Commit()
            Catch ex As Exception
                Throw
            Finally
                If adapter IsNot Nothing Then
                    adapter.Dispose()
                End If
            End Try
            Return results
        End Function
    End Class
End Namespace
