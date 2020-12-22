Imports System.Activities
Imports System.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

Namespace CustomInteractions.Flow

    Public NotInheritable Class GetVariableValues
        Inherits CodeActivity
        Public Property ParameterSetCollection As InArgument(Of ParameterSetCollection)

        Public Property InlineParameterSetCollection As InArgument(Of ParameterSetCollection)

        Public Property MetaData As InArgument(Of MetadataRoot)

        Public Property parameterCollectionName As OutArgument(Of String)

        Public Property IsScorable As OutArgument(Of Boolean)

        Public Property ControllerId As OutArgument(Of String)

        Protected Overrides Sub Execute(context As CodeActivityContext)
            Dim parameters As ParameterSetCollection = context.GetValue(ParameterSetCollection)
            Dim inlineParameters As ParameterSetCollection = context.GetValue(InlineParameterSetCollection)

            Dim currentMetaData As MetadataRoot = context.GetValue(MetaData)
            If inlineParameters IsNot Nothing Then
                If inlineParameters.FlattenParameters().ToList().Any(Function(p) p.Name.Equals("controlId", StringComparison.OrdinalIgnoreCase)) Then
                    Dim controlId As ParameterBase = inlineParameters.FlattenParameters().ToList().First(Function(p) p.Name.Equals("controlId", StringComparison.OrdinalIgnoreCase))
                    If controlId IsNot Nothing Then
                        parameterCollectionName.[Set](context,
                                                      $"{IdentifierHelper.CI_ParameterCollectionPrefix}{controlId}")
                    End If
                Else
                    parameterCollectionName.[Set](context, IdentifierHelper.CI_ParameterCollectionName)
                End If

                If inlineParameters.FlattenParameters().ToList().Any(Function(p) p.Name.Equals("controllerId", StringComparison.OrdinalIgnoreCase)) Then
                    Dim cntrlId As ParameterBase = inlineParameters.FlattenParameters().ToList().First(Function(p) p.Name.Equals("controllerId", StringComparison.OrdinalIgnoreCase))
                    If cntrlId IsNot Nothing Then
                        controllerId.[Set](context, DirectCast(cntrlId, PlainTextParameter).Value)
                    End If
                End If

                Dim scorableTmp As Boolean = (currentMetaData IsNot Nothing AndAlso currentMetaData.Scoring IsNot Nothing AndAlso currentMetaData.Scoring.Count > 0)
                If scorableTmp Then
                    Dim scorablePrm As ParameterBase = inlineParameters.FlattenParameters().ToList().FirstOrDefault(Function(p) p.[GetType]() Is GetType(CustomInteractionResourceParameter))
                    If scorablePrm IsNot Nothing Then
                        scorableTmp = (scorableTmp AndAlso DirectCast(scorablePrm, CustomInteractionResourceParameter).Scorable)
                    End If
                End If
                isScorable.[Set](context, scorableTmp)
            ElseIf parameters IsNot Nothing Then
                Dim scorableTmp As Boolean = (currentMetaData IsNot Nothing AndAlso currentMetaData.Scoring IsNot Nothing AndAlso currentMetaData.Scoring.Count > 0)
                If scorableTmp Then
                    Dim scorablePrm As ParameterBase = parameters.FlattenParameters().ToList().FirstOrDefault(Function(p) p.[GetType]() Is GetType(CustomInteractionResourceParameter))
                    If scorablePrm IsNot Nothing Then
                        scorableTmp = (scorableTmp AndAlso DirectCast(scorablePrm, CustomInteractionResourceParameter).Scorable)
                    End If
                End If
                parameterCollectionName.[Set](context, IdentifierHelper.CI_ParameterCollectionName)
                isScorable.[Set](context, scorableTmp)
            Else
                parameterCollectionName.[Set](context, IdentifierHelper.CI_ParameterCollectionName)
                isScorable.[Set](context, False)
            End If
        End Sub
    End Class
End NameSpace