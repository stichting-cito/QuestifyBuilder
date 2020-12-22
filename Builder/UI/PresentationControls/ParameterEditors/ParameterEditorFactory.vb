Imports Cito.Tester.ContentModel
Imports Questify.Builder.UI.PresentationControls.ParameterEditors.EditorControls.ConcreteFactory

Public Class ParameterEditorFactory
    Implements IDisposable


    Public Sub New()
        _factory = New Dictionary(Of Type, IParameterEditorControlFactory)()
        _factory.Add(GetType(AreaParameter), New AreaPrmFactory())
        _factory.Add(GetType(BooleanParameter), New BooleanPrmFactory())
        _factory.Add(GetType(ChoiceCollectionParameter), New ChoiceCollectionPrmFactory())
        _factory.Add(GetType(CollectionParameter), New CollectionPrmFactory())
        _factory.Add(GetType(IntegerParameter), New IntegerPrmFactory())
        _factory.Add(GetType(ListedParameter), New ListedPrmFactory())
        _factory.Add(GetType(PlainTextParameter), New PlainTextPrmFactory())
        _factory.Add(GetType(ResourceParameter), New ResourcePrmFactory())
        _factory.Add(GetType(CustomInteractionResourceParameter), New CustomInteractionResourcePrmFactory())
        _factory.Add(GetType(XHtmlParameter), New XHtmlPrmFactory())
        _factory.Add(GetType(XhtmlResourceParameter), New XHtmlResourcePrmFactory())
        _factory.Add(GetType(StringScoringParameter), New ScoringParameterEditorFactory(Of StringScoringParameter)())
        _factory.Add(GetType(IntegerScoringParameter), New ScoringParameterEditorFactory(Of IntegerScoringParameter)())
        _factory.Add(GetType(DecimalScoringParameter), New ScoringParameterEditorFactory(Of DecimalScoringParameter)())
        _factory.Add(GetType(CurrencyScoringParameter), New ScoringParameterEditorFactory(Of CurrencyScoringParameter)())
        _factory.Add(GetType(DateScoringParameter), New ScoringParameterEditorFactory(Of DateScoringParameter)())
        _factory.Add(GetType(TimeScoringParameter), New ScoringParameterEditorFactory(Of TimeScoringParameter)())
        _factory.Add(GetType(MathScoringParameter), New ScoringParameterEditorFactory(Of MathScoringParameter)())
        _factory.Add(GetType(MultiChoiceScoringParameter), New ScoringParameterEditorFactory(Of MultiChoiceScoringParameter)())
        _factory.Add(GetType(InlineChoiceScoringParameter), New ScoringParameterEditorFactory(Of InlineChoiceScoringParameter)())
        _factory.Add(GetType(GapMatchScoringParameter), New ScoringParameterMultiEditorFactory(Of GapMatchScoringParameter)())
        _factory.Add(GetType(GraphGapMatchScoringParameter), New ScoringParameterMultiEditorFactory(Of GraphGapMatchScoringParameter)())
        _factory.Add(GetType(GapTextParameter), New GapTextPrmFactory())
        _factory.Add(GetType(OrderScoringParameter), New ScoringParameterEditorFactory(Of OrderScoringParameter))
        _factory.Add(GetType(GapImageParameter), New GapImagePrmFactory())
        _factory.Add(GetType(HotspotScoringParameter), New ScoringParameterMultiEditorFactory(Of HotspotScoringParameter))
        _factory.Add(GetType(MatrixScoringParameter), New ScoringParameterMultiEditorFactory(Of MatrixScoringParameter))
        _factory.Add(GetType(HotTextScoringParameter), New HotTextScoringParameterEditorFactory())
        _factory.Add(GetType(MathMLParameter), New MathMLParameterEditorFactory())
        _factory.Add(GetType(HotTextCorrectionScoringParameter), New CreateNoEditor())
        _factory.Add(GetType(MathCasDependencyScoringParameter), New ScoringParameterMultiEditorFactory(Of MathCasDependencyScoringParameter))
        _factory.Add(GetType(CasEqualStepsScoringParameter), New ScoringParameterEditorFactory(Of CasEqualStepsScoringParameter)())
        _factory.Add(GetType(MathCasEqualScoringParameter), New ScoringParameterEditorFactory(Of MathCasEqualScoringParameter)())
        _factory.Add(GetType(MathCasEvaluateScoringParameter), New ScoringParameterEditorFactory(Of MathCasEvaluateScoringParameter)())
        _factory.Add(GetType(GapMatchRichTextScoringParameter), New ScoringParameterMultiEditorFactory(Of GapMatchRichTextScoringParameter)())
        _factory.Add(GetType(GapTextRichTextParameter), New GapTextRichTextParameterEditorFactory())
        _factory.Add(GetType(AspectScoringParameter), New ScoringParameterEditorFactory(of AspectScoringParameter))
        _factory.Add(GetType(SelectPointScoringParameter), New ScoringParameterMultiEditorFactory(Of SelectPointScoringParameter))

    End Sub


    private _factory As Dictionary(Of Type, IParameterEditorControlFactory)

    Public Function CreateControl(parameter As ParameterBase, editor As ParameterSetsEditor) As ParameterEditorControlBase
        Dim factory As IParameterEditorControlFactory = Nothing
        _factory.TryGetValue(parameter.GetType(), factory)

        If factory IsNot Nothing Then
            Dim createdControl = factory.Construct(parameter, editor)
            Dim codedUiTag = (parameter.Name + "#" + parameter.GetType().Name).ToLower()
            createdControl.Name = codedUiTag
            createdControl.AccessibleName = codedUiTag
            createdControl.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Right
            Return createdControl
        End If

        Debug.Assert(False, "No factory present for requested parameter")
        Return Nothing
    End Function


    Private Class CreateNoEditor : Implements IParameterEditorControlFactory

        Public Function Construct(prm As ParameterBase, editor As ParameterSetsEditor) As ParameterEditorControlBase Implements IParameterEditorControlFactory.Construct
            Return Nothing
        End Function

        Public ReadOnly Property CreatedType As Type Implements IParameterEditorControlFactory.CreatedType
            Get
                Return Nothing
            End Get
        End Property
    End Class

    Public Sub Dispose() Implements IDisposable.Dispose
        _factory.Clear()
        _factory = Nothing
    End Sub
End Class