Imports Cito.Tester.ContentModel
Imports Versioning

Namespace ItemProcessing

    Public MustInherit Class ParameterHandler

        Private Shared ReadOnly dict As Dictionary(Of Type, Func(Of ParameterHandler))

        Shared Sub New()
            dict = New Dictionary(Of Type, Func(Of ParameterHandler))
            dict.Add(GetType(ChoiceCollectionParameter), Function() New ChoiceCollectionParameterHandler)
            dict.Add(GetType(CollectionParameter), Function() New CollectionParameterHandler)
            dict.Add(GetType(AreaParameter), Function() New AreaParameterHandler())
            dict.Add(GetType(MultiChoiceScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(StringScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(IntegerScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(DecimalScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(CurrencyScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(DateScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(TimeScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(MathScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(InlineChoiceScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(GapMatchScoringParameter), Function() New GapMatchScoringParameterHandler())
            dict.Add(GetType(GraphGapMatchScoringParameter), Function() New GraphGapMatchScoringParameterHandler())
            dict.Add(GetType(GapTextParameter), Function() New GapChoiceParameterHandler())
            dict.Add(GetType(ResourceParameter), Function() New ResourceParameterHandler())
            dict.Add(GetType(CustomInteractionResourceParameter), Function() new CustomInteractionResourceParameterHandler())
            dict.Add(GetType(GapImageParameter), Function() New GapImageParameterHandler())
            dict.Add(GetType(OrderScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(HotspotScoringParameter), Function() New HotspotScoringParameterHandler())
            dict.Add(GetType(MatrixScoringParameter), Function() New MatrixScoringParameterHandler())
            dict.Add(GetType(HotTextScoringParameter), Function() New HotTextScoringParameterHandler())
            dict.Add(GetType(HotTextCorrectionScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(MathCasDependencyScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(MathCasEqualScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(CasEqualStepsScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(MathCasEvaluateScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(GapMatchRichTextScoringParameter), Function() New GapMatchRichTextScoringParameterHandler())
            dict.Add(GetType(GapTextRichTextParameter), Function() New GapChoiceParameterHandler())
            dict.Add(GetType(AspectScoringParameter), Function() New ScoringParameterHandler())
            dict.Add(GetType(SelectPointScoringParameter), Function() New HotspotScoringParameterHandler())

            Dim def As New DefaultParameterHandler()
            dict.Add(GetType(BooleanParameter), Function() def)
            dict.Add(GetType(IntegerParameter), Function() def)
            dict.Add(GetType(ListedParameter), Function() def)
            dict.Add(GetType(PlainTextParameter), Function() def)
            dict.Add(GetType(XHtmlParameter), Function() def)
            dict.Add(GetType(XhtmlResourceParameter), Function() def)
            dict.Add(GetType(MathMLParameter), Function() def)

        End Sub


        Friend Shared Function GetConcreteMerger(ByVal T As Type) As ParameterHandler
            Dim baseType As Type = GetType(ParameterBase)

            If baseType.IsAssignableFrom(T) Then

                If (dict.ContainsKey(T)) Then
                    Return dict(T)()
                End If
                Debug.Assert(False)
                Return New DefaultParameterHandler()
            Else
                Debug.Assert(False, "Type is not a ParameterBase type!")
                Throw New ArgumentException("parameterType")
            End If
        End Function


        Public Shared Function Merge(ByVal newParamSet As ParameterSetCollection,
                             ByVal currentParamSet As ParameterSetCollection) As WarningsAndErrors

            Dim ret As WarningsAndErrors = New WarningsAndErrors
            Dim handler As ParameterSetHandler = New ParameterSetHandler()

            handler.Merge(newParamSet, currentParamSet, ret)

            Return ret
        End Function

        Public Shared Function Compare(ByVal newParamSet As ParameterSetCollection, ByVal currentParamSet As ParameterSetCollection) As IEnumerable(Of MetaDataCompareResult)
            Dim handler As ParameterSetHandler = New ParameterSetHandler()

            Dim ret As List(Of MetaDataCompareResult) = New List(Of MetaDataCompareResult)()
            handler.Compare(newParamSet, currentParamSet, ret)

            Return ret
        End Function

        Friend MustOverride Sub Merge(ByVal newParam As ParameterBase, ByVal currentParam As ParameterBase, ByVal warnErr As WarningsAndErrors)

        Friend MustOverride Function Compare(ByVal newParam As ParameterBase, ByVal currentParam As ParameterBase) As IEnumerable(Of MetaDataCompareResult)

    End Class

End Namespace
