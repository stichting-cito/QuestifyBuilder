Imports Cito.Tester.Common

Public Class GeneralItemReferenceValidator
    Inherits EntityValidationBase(Of GeneralItemReference)


    Protected Overloads Overrides Function ValidateEntityFieldValue(entity As GeneralItemReference, fieldName As String, value As Object) As String
        Select Case fieldName
            Case "Identifier"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.IdentifierIsARequiredField

            Case "Title"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.TitleIsARequiredField

            Case "ItemFunctionalType"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.ItemFunctionalTypeIsARequiredField

            Case "SourceName"
                If Not ValidationHelper.IsNotEmpty(value) Then Return My.Resources.SourceNameIsARequiredField

        End Select

        Return String.Empty
    End Function

    Public Overrides ReadOnly Property FriendlyEntityName As String
        Get
            Return My.Resources.ItemReferenceViewBase_FriendlyEntityName
        End Get
    End Property
End Class
