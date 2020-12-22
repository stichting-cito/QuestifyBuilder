Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

Namespace Dialogs.BusinessLogic
    Friend Class TableStyleFactory

        Public Shared Function GetDefaultTableStyleStrategy(presenter As BorderAndShadingPresenter) As baseTableStyleStrategy
            Return New CustomTableStyleStrategy(presenter)
        End Function

        Public Shared Function NonBorders(presenter As BorderAndShadingPresenter) As baseTableStyleStrategy
            Return New NoneTableStyleStrategy(presenter)
        End Function

        Public Shared Function Outline(presenter As BorderAndShadingPresenter) As baseTableStyleStrategy
            Return New BoxTableStyleStrategy(presenter)
        End Function

        Shared Function All(presenter As BorderAndShadingPresenter) As baseTableStyleStrategy
            Return New AllStyleStrategy(presenter)
        End Function

        Shared Function Grid(presenter As BorderAndShadingPresenter) As baseTableStyleStrategy
            Return New GridTableStyleStrategy(presenter)
        End Function

        Shared Function GetFactoryBasedOnStyle(model As TableStyleDto, presenter As BorderAndShadingPresenter) As baseTableStyleStrategy
            If model.BoxAndInnerSameStyle() Then
                Return If(model.LeftVertical = LineStyle.None OrElse model.LeftVertical = LineStyle.Hidden, NonBorders(presenter), All(presenter))
            End If
            Dim innerStyle = model.GetInnserstyle()
            Dim innerWidth = model.GetInnserstyleWidth()
            If model.BoxIsSameStyle() AndAlso model.HasInnser() AndAlso
                model.InnerSameStyle() AndAlso innerStyle.Value = LineStyle.Solid AndAlso innerWidth = 1 Then Return Grid(presenter)
            If model.BoxIsSameStyle() AndAlso model.HasInnser() AndAlso
               model.InnerSameStyle() AndAlso (innerStyle.Value = LineStyle.None OrElse innerStyle.Value = LineStyle.Hidden) Then Return Outline(presenter)

            If Not model.BoxAndInnerSameStyle() Then Return GetDefaultTableStyleStrategy(presenter)

            Debug.Assert(False, "You should not reach this place, all the cases should have been handled")
            Return GetDefaultTableStyleStrategy(presenter)
        End Function

    End Class
End Namespace