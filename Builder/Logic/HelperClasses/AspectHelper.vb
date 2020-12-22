Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace HelperClasses

    Public Class AspectHelper

        Public Shared Function GetAspect(aspectIdentifier As String, bankId As Integer) As AspectResourceEntity
            If IsDefaultResourceAspect(aspectIdentifier) Then
                Return New AspectResourceEntity() With {.Name = AspectScoringParameter.DEFAULT_ASPECT_NAME, .Title = "Default aspect", .RawScore = 0}
            Else
                Dim aspect = ResourceFactory.Instance.GetResourceByNameWithOption(bankId, aspectIdentifier, New ResourceRequestDTO())
                If aspect IsNot Nothing AndAlso TypeOf aspect Is AspectResourceEntity Then
                    Return DirectCast(aspect, AspectResourceEntity)
                End If
            End If
            Return Nothing
        End Function

        Public Shared Function IsDefaultResourceAspect(aspectIdentifier As String) As Boolean
            Return aspectIdentifier.Equals(AspectScoringParameter.DEFAULT_ASPECT_NAME, StringComparison.InvariantCultureIgnoreCase)
        End Function

    End Class

End Namespace