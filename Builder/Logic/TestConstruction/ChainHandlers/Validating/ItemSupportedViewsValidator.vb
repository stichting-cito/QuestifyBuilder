Imports System.Linq
Imports Cito.Tester.ContentModel
Imports Microsoft.Ajax.Utilities
Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace TestConstruction.ChainHandlers.Validating

    Public Class ItemSupportedViewsValidator


        Private ReadOnly _cachedSupportedViewsOfItemLayoutTemplates As New Dictionary(Of String, String())
        Private ReadOnly _resourceNeededHandler As EventHandler(Of ResourceNeededEventArgs)


        Public Sub New(resourceNeededHandler As EventHandler(Of ResourceNeededEventArgs))
            _resourceNeededHandler = resourceNeededHandler
        End Sub

        Public Function ContainsItemSupportedViews(ByVal itemEntity As ItemResourceEntity, ByVal mustSupportViewTypes As List(Of String)) As Boolean
            Return ContainsItemLayoutTemplateSupportedViews(itemEntity.ItemLayoutTemplateUsedName, mustSupportViewTypes)
        End Function

        Public Function ContainsItemLayoutTemplateSupportedViews(ByVal itemLayoutTemplateName As String, ByVal mustSupportViewTypes As List(Of String)) As Boolean
            Dim viewTypeLeftovers As New List(Of String)(mustSupportViewTypes.Select(Function(s) s.ToUpper()).ToList())
            Dim templateTargets As String() = Nothing
            If Not mustSupportViewTypes.Count = 0 Then

                If Not _cachedSupportedViewsOfItemLayoutTemplates.ContainsKey(itemLayoutTemplateName) Then
                    Dim adapter As New ItemLayoutAdapter(itemLayoutTemplateName, Nothing, _resourceNeededHandler)
                    templateTargets = adapter.Template.GetEnabledTargetNames()

                    _cachedSupportedViewsOfItemLayoutTemplates.Add(itemLayoutTemplateName, templateTargets)
                Else
                    templateTargets = _cachedSupportedViewsOfItemLayoutTemplates(itemLayoutTemplateName)
                End If

                templateTargets.ForEach(Sub(t)
                                            If viewTypeLeftovers.Contains(t.ToUpper()) Then
                                                viewTypeLeftovers.Remove(t.ToUpper())
                                            End If
                                        End Sub)

                If viewTypeLeftovers.Contains(GenericTestModelPlugin.PLUGIN_NAME.ToUpper()) Then
                    viewTypeLeftovers.Remove(GenericTestModelPlugin.PLUGIN_NAME.ToUpper())
                End If
            End If
            Return (viewTypeLeftovers.Count = 0)
        End Function


    End Class
End Namespace