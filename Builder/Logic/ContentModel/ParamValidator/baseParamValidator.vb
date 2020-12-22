Imports Cito.Tester.ContentModel

Namespace ContentModel.ParamValidator
    Friend MustInherit Class baseParamValidator : Implements IParamValidator


        Private Shared ReadOnly dict As Dictionary(Of Type, Func(Of ParameterBase, IParamValidator))


        Shared Sub New()

            Dim def As Func(Of ParameterBase, IParamValidator) = Function(p) New DefaultParamValidator(p)
            dict = New Dictionary(Of Type, Func(Of ParameterBase, IParamValidator))
            dict.Add(GetType(PlainTextParameter), Function(p) New PlainTextParameterValidator(p))
            dict.Add(GetType(XHtmlParameter), Function(p) New XHtmlParameterValidator(p))
            dict.Add(GetType(ResourceParameter), Function(p) New ResourceParameterValidator(p))
            dict.Add(GetType(CustomInteractionResourceParameter), Function(p) New ResourceParameterValidator(p))
            dict.Add(GetType(IntegerParameter), Function(p) New IntegerParameterValidator(p))
            dict.Add(GetType(CollectionParameter), Function(p) New CollectionParameterValidator(p))
            dict.Add(GetType(AreaParameter), Function(p) New AreaParameterValidator(p))
            dict.Add(GetType(GapTextParameter), Function(p) New PlainTextParameterValidator(p))
            dict.Add(GetType(GapImageParameter), Function(p) New GapImageParameterValidator(p))
            dict.Add(GetType(GapMatchScoringParameter), Function(p) New GapMatchScoringParameterValidator(p))
            dict.Add(GetType(GraphGapMatchScoringParameter), Function(p) New GraphGapMatchScoringParameterValidator(p))
            dict.Add(GetType(HotspotScoringParameter), Function(p) New HotspotScoringParameterValidator(p))
            dict.Add(GetType(MatrixScoringParameter), Function(p) New MatrixScoringParameterValidator(p))
            dict.Add(GetType(MultiChoiceScoringParameter), Function(p) New CollectionParameterValidator(p))
            dict.Add(GetType(OrderScoringParameter), Function(p) New CollectionParameterValidator(p))
            dict.Add(GetType(GapMatchRichTextScoringParameter), Function(p) New GapMatchScoringParameterValidator(p))
            dict.Add(GetType(GapTextRichTextParameter), Function(p) New XHtmlParameterValidator(p))
            dict.Add(GetType(AspectScoringParameter), Function(p) New CollectionParameterValidator(p))
            dict.Add(GetType(SelectPointScoringParameter), Function(p) New HotspotScoringParameterValidator(p))


            dict.Add(GetType(ChoiceCollectionParameter), def)
            dict.Add(GetType(BooleanParameter), def)
            dict.Add(GetType(ListedParameter), def)
            dict.Add(GetType(XhtmlResourceParameter), def)

            dict.Add(GetType(StringScoringParameter), def)
            dict.Add(GetType(IntegerScoringParameter), def)
            dict.Add(GetType(DecimalScoringParameter), def)
            dict.Add(GetType(CurrencyScoringParameter), def)
            dict.Add(GetType(TimeScoringParameter), def)
            dict.Add(GetType(DateScoringParameter), def)

            dict.Add(GetType(GeogebraScoringParameter), def)
            dict.Add(GetType(HotTextScoringParameter), def)
            dict.Add(GetType(HotTextCorrectionScoringParameter), def)
            dict.Add(GetType(MathScoringParameter), def)
        End Sub


        Friend Shared Function GetValidator(ByVal p As ParameterBase) As IParamValidator

            Dim ret As Func(Of ParameterBase, IParamValidator) = Nothing
            If (dict.TryGetValue(p.GetType(), ret)) Then
                Return ret.Invoke(p)
            Else
                Debug.Assert(False, $"No validator present for [{p.GetType()}]")
                Return New DefaultParamValidator(p)
            End If
        End Function



        Private ReadOnly _param As ParameterBase
        Private ReadOnly _KnownDesignersettings As List(Of String)


        Sub New(param As ParameterBase)
            ValueBag = New Dictionary(Of String, String)
            _param = param
            _KnownDesignersettings = New List(Of String)
            SetKnownDesignerSettings(_KnownDesignersettings)
        End Sub

        Protected Overridable Sub SetKnownDesignerSettings(ByRef knownParameters As List(Of String))
            knownParameters.Add("label")
            knownParameters.Add("description")
            knownParameters.Add("group")
        End Sub

        Protected ReadOnly Property Parameter As ParameterBase
            Get
                Return _param
            End Get
        End Property


        Public MustOverride Function GetError() As IEnumerable(Of String) Implements IParamValidator.GetError

        Public Function isValid() As Boolean Implements IParamValidator.isValid

            If Not _param.GetDesignerValue("visible", True) Then
                Return True
            End If

            If (_param.GetDesignerValue("conditionalEnabled", False)) Then

                Dim ThisParamIsEnabled = IsConditionalyEnabled()

                If (Not ThisParamIsEnabled) Then
                    Return True
                End If

            End If

            Return DoCheckIsValid()
        End Function

        Public Property ValueBag As IDictionary(Of String, String) Implements IParamValidator.ValueBag


        Public MustOverride Function DoCheckIsValid() As Boolean

        Public ReadOnly Property ValidDesignerSettings As IEnumerable(Of String) Implements IParamValidator.ValidDesignerSettings
            Get
                Return _KnownDesignersettings
            End Get
        End Property



        Private Function IsConditionalyEnabled() As Boolean

            Dim key = _param.GetDesignerValue("conditionalEnabledSwitchParameter", String.Empty)
            Dim value = _param.GetDesignerValue("conditionalEnabledWhenValue", String.Empty)

            If (String.IsNullOrEmpty(key)) Then Throw New ArgumentException("conditionalEnabledSwitchParameter was nothing")
            If (String.IsNullOrEmpty(value)) Then Throw New ArgumentException("conditionalEnabledWhenValue was nothing")

            Dim dictVal As String = String.Empty

            If (ValueBag.TryGetValue(key, dictVal)) Then
                Return ParameterHasConditionalValue(dictVal, value)
            Else
                Throw New KeyNotFoundException(
    $"Key ['{key}'] not found while validating parameter, order of parameters")
            End If
        End Function

        Private Function ParameterHasConditionalValue(parameterValue As String, conditionalValue As String) As Boolean
            Dim returnValue As Boolean = True
            Const CONDITIONAL_OR As Char = "|"c
            Const CONDITIONAL_NOT As Char = "!"c
            Const CONDITIONAL_EMPTY As String = "(EMPTY)"

            If conditionalValue.Contains(CONDITIONAL_OR) Then
                For Each value As String In conditionalValue.Split(CONDITIONAL_OR)
                    returnValue = ParameterHasConditionalValue(parameterValue, value)
                    If returnValue = True Then Exit For
                Next
            ElseIf conditionalValue.StartsWith(CONDITIONAL_NOT) AndAlso conditionalValue.Contains(CONDITIONAL_EMPTY) Then
                returnValue = Not String.IsNullOrEmpty(parameterValue)
            ElseIf conditionalValue.StartsWith(CONDITIONAL_NOT) Then
                returnValue = Not parameterValue.Equals(conditionalValue.Substring(1), StringComparison.InvariantCultureIgnoreCase)
            ElseIf conditionalValue.Contains(CONDITIONAL_EMPTY) Then
                returnValue = String.IsNullOrEmpty(parameterValue)
            Else
                returnValue = parameterValue.Equals(conditionalValue, StringComparison.InvariantCultureIgnoreCase)
            End If
            Return returnValue
        End Function


    End Class
End Namespace