Imports System.Linq
Imports System.Xml
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel
Imports Questify.Builder.Logic.QTI.Converters.Declaration.QTI22
Imports Questify.Builder.Logic.QTI.Converters.Processing.QTI22
Imports Questify.Builder.Logic.QTI.Converters.ScoringConverter.QTI22
Imports Questify.Builder.Logic.QTI.Helpers.QTI_Base
Imports Questify.Builder.Logic.QTI.Interfaces
Imports Questify.Builder.Logic.QTI.Interfaces.QTI22

Namespace QTI.Facade.Factories.QTI22

    Public Class ResponseTypeFactory

        Private ReadOnly _factIdsPerScoringParameter As New Dictionary(Of String, ScoringParameter)
        Private ReadOnly _factIdPerBaseFact As New Dictionary(Of BaseFact, String)
        Private _solutionIdHelper As SolutionIdentifierHelper
        Private ReadOnly _responseIndexPerIdentifier As Dictionary(Of String, Double)
        Private ReadOnly _conceptIndexPerIdentifier As New Dictionary(Of String, Integer)
        Private ReadOnly _owner As QTI22CombinedScoringConverter
        Private ReadOnly _responseIdentifierAttributeList As XmlNodeList
        Private ReadOnly _finding As KeyFinding
        Private ReadOnly _controlType As CombinedScoringHelper.EnumControlType = CombinedScoringHelper.EnumControlType.Unknown
        Private Const ciControllerId As String = "CI_SP"


        Public Sub New(scoringParameters As HashSet(Of ScoringParameter), finding As KeyFinding, responseIdentifierAttributeList As XmlNodeList)
            Me.New(scoringParameters, finding, responseIdentifierAttributeList, Nothing)
        End Sub

        Public Sub New(scoringParameters As HashSet(Of ScoringParameter), finding As KeyFinding, responseIdentifierAttributeList As XmlNodeList, owner As QTI22CombinedScoringConverter)
            If scoringParameters IsNot Nothing AndAlso scoringParameters.Count > 0 AndAlso _factIdsPerScoringParameter.Count = 0 Then
                _factIdsPerScoringParameter = CombinedScoringHelper.GetFactIdsFromScoringParameters(scoringParameters)
                _responseIdentifierAttributeList = responseIdentifierAttributeList
                _finding = finding
                _owner = owner
                _responseIndexPerIdentifier = CombinedScoringHelper.DetermineResponseIndexPerIdentifier(_responseIdentifierAttributeList)
                FillResponseIndex()
                GetFactIdentifiers(finding)
            End If
        End Sub

        Public Sub New(solution As Solution, finding As KeyFinding, responseIdentifierAttributeList As XmlNodeList)
            Me.New(solution, finding, responseIdentifierAttributeList, Nothing)
        End Sub

        Public Sub New(solution As Solution, finding As KeyFinding, responseIdentifierAttributeList As XmlNodeList, owner As QTI22CombinedScoringConverter)
            _controlType = CombinedScoringHelper.DetermineControlType(solution, responseIdentifierAttributeList, finding)
            _responseIdentifierAttributeList = responseIdentifierAttributeList
            _finding = finding
            _owner = owner
            _responseIndexPerIdentifier = CombinedScoringHelper.DetermineResponseIndexPerIdentifier(_responseIdentifierAttributeList)
            FillResponseIndex()
            GetFactIdentifiers(finding)
        End Sub


        Public ReadOnly Property CollectionOfResponseIndexPerIdentifier() As Dictionary(Of String, Double)
            Get
                Return _responseIndexPerIdentifier
            End Get
        End Property

        Public ReadOnly Property CollectionOfConceptIndexPerIdentifier() As Dictionary(Of String, Integer)
            Get
                If _conceptIndexPerIdentifier.Count = 0 Then FillConceptIndex()
                Return _conceptIndexPerIdentifier
            End Get
        End Property

        Public Property IdHelper As SolutionIdentifierHelper
            Get
                If _solutionIdHelper Is Nothing Then _solutionIdHelper = New SolutionIdentifierHelper
                Return _solutionIdHelper
            End Get
            Set(value As SolutionIdentifierHelper)
                _solutionIdHelper = value
            End Set
        End Property

        Public Function CreateResponseDeclarationTypeByFact(ByVal fact As KeyFact) As IResponseDeclaration
            Dim ret As IResponseDeclaration = Nothing

            If fact.Id IsNot Nothing AndAlso Not String.IsNullOrEmpty(fact.Id) Then
                Dim factId As String = IdHelper.GetStrippedId(fact.Id)
                If _factIdsPerScoringParameter.ContainsKey(factId) AndAlso _responseIndexPerIdentifier.ContainsKey(GetIdFromScoringParameter(_factIdsPerScoringParameter(factId))) Then
                    Return GetResponseDeclarationTypeByScoringParameter(_factIdsPerScoringParameter(factId))
                ElseIf Not _controlType = CombinedScoringHelper.EnumControlType.Unknown AndAlso _responseIndexPerIdentifier.ContainsKey(factId) Then
                    Return GetResponseDeclarationTypeByControlType(fact)
                End If
            End If
            If Not _controlType = CombinedScoringHelper.EnumControlType.Unknown AndAlso fact.Values.Count > 0 AndAlso _responseIndexPerIdentifier.ContainsKey(fact.Values(0).Domain) Then
                Return GetResponseDeclarationTypeByControlType(fact)
            Else
                Debug.Assert(False, "Unable to retrieve type of responsedeclaration. Should not occur !")
            End If

            Return ret
        End Function

        Public Function CreateConceptResponseProcessingTypeByFact(ByVal fact As KeyFact) As IResponseProcessing
            Dim ret As IResponseProcessing = Nothing
            Dim factId As String = IdHelper.GetStrippedId(fact.Id)

            If _factIdsPerScoringParameter.ContainsKey(factId) AndAlso _responseIndexPerIdentifier.ContainsKey(GetIdFromScoringParameter(_factIdsPerScoringParameter(factId))) Then
                Dim scoringPrm As ScoringParameter = _factIdsPerScoringParameter(factId)
                If ScoringPrmBelongsToCustomInteraction(scoringPrm) Then
                    Return GetConceptResponseProcessingTypeByScoringParameter(_factIdsPerScoringParameter(factId), GetResponseIndexByIdentifier(GetIdFromScoringParameter(_factIdsPerScoringParameter(factId))), GetIndexForCustomInteractionScoringParameters(scoringPrm.ControllerId))
                Else
                    Return GetConceptResponseProcessingTypeByScoringParameter(_factIdsPerScoringParameter(factId), GetResponseIndexByIdentifier(GetIdFromScoringParameter(_factIdsPerScoringParameter(factId))))
                End If
            Else
                Debug.Assert(False, "Unable to retrieve type of concept responseprocessing. Should not occur !")
            End If

            Return ret
        End Function

        Public Function CreateResponseProcessingTypeByFact(ByVal fact As KeyFact) As IResponseProcessing
            Dim ret As IResponseProcessing = Nothing
            Dim scoringPrm As ScoringParameter = Nothing
            If fact.Id IsNot Nothing AndAlso Not String.IsNullOrEmpty(fact.Id) Then
                Dim factId As String = IdHelper.GetStrippedId(fact.Id)
                If _factIdsPerScoringParameter.ContainsKey(factId) AndAlso _responseIndexPerIdentifier.ContainsKey(GetIdFromScoringParameter(_factIdsPerScoringParameter(factId))) Then
                    scoringPrm = _factIdsPerScoringParameter(factId)
                    If ScoringPrmBelongsToCustomInteraction(scoringPrm) Then
                        Return GetResponseProcessingTypeByScoringParameter(scoringPrm, GetResponseIndexByIdentifier(GetIdFromScoringParameter(scoringPrm)), GetIndexForCustomInteractionScoringParameters(scoringPrm.ControllerId))
                    Else
                        Return GetResponseProcessingTypeByScoringParameter(scoringPrm, GetResponseIndexByIdentifier(GetIdFromScoringParameter(scoringPrm)))
                    End If
                ElseIf Not _controlType = CombinedScoringHelper.EnumControlType.Unknown AndAlso _responseIndexPerIdentifier.ContainsKey(factId) Then
                    Return GetResponseProcessingTypeByControlType(fact, GetResponseIndexByIdentifier(factId), scoringPrm)
                End If
            End If
            If Not _controlType = CombinedScoringHelper.EnumControlType.Unknown AndAlso fact.Values.Count > 0 AndAlso _responseIndexPerIdentifier.ContainsKey(fact.Values(0).Domain) Then
                Return GetResponseProcessingTypeByControlType(fact, GetResponseIndexByIdentifier(fact.Values(0).Domain), scoringPrm)
            Else
                Debug.Assert(False, "Unable to retrieve type of responseprocessing. Should not occur !")
            End If
            Return ret
        End Function

        Public Function GetResponseIndexByIdentifier(identifier As String) As Integer
            Dim id As String = IdHelper.GetStrippedId(identifier)
            If _responseIndexPerIdentifier.ContainsKey(id) Then
                Return CInt(Math.Floor(_responseIndexPerIdentifier(id)))
            Else
                Return 0
            End If
        End Function

        Public Function GetConceptIndexByIdentifier(identifier As String) As Integer
            If _conceptIndexPerIdentifier.Count = 0 Then FillConceptIndex()
            Dim id As String = IdHelper.GetStrippedId(identifier)
            If _conceptIndexPerIdentifier.ContainsKey(id) Then
                Return _conceptIndexPerIdentifier(id)
            Else
                Return 0
            End If
        End Function

        Public Function GetOrderIndexByIdentifier(identifier As String) As Double
            Dim id As String = IdHelper.GetStrippedId(identifier)
            If _responseIndexPerIdentifier.ContainsKey(id) Then
                Return _responseIndexPerIdentifier(id)
            Else
                Return 0
            End If
        End Function

        Public Function GetOrderIndexByKeyValue(fact As BaseFact) As Integer
            Dim factId As String = IdHelper.GetStrippedId(fact.Id)
            If Not String.IsNullOrEmpty(factId) AndAlso _factIdsPerScoringParameter.ContainsKey(factId) Then
                Dim scoringPrm As ScoringParameter = _factIdsPerScoringParameter(factId)
                If TypeOf scoringPrm Is GraphGapMatchScoringParameter AndAlso DirectCast(scoringPrm, GraphGapMatchScoringParameter).IsCategorizationVariant Then
                    If TypeOf DirectCast(fact.Values(0), KeyValue).Values(0) Is NoValue Then
                        Return AscW(DirectCast(DirectCast(fact.Values(0), KeyValue).Values(0), NoValue).ToString) - 64
                    Else
                        Return AscW(DirectCast(DirectCast(fact.Values(0), KeyValue).Values(0), StringValue).Value) - 64
                    End If
                ElseIf TypeOf scoringPrm Is OrderScoringParameter Then
                    Return Asc(fact.Id.Substring(0, 1)) - 64
                End If
            End If
            Return 0
        End Function

        Private Sub GetFactIdentifiers(finding As KeyFinding)
            If finding IsNot Nothing Then
                GetFactIdentifiers(finding.Facts)
                For Each factSet In finding.KeyFactsets
                    GetFactIdentifiers(factSet.Facts)
                Next
            End If
        End Sub

        Private Sub GetFactIdentifiers(facts As List(Of BaseFact))
            For Each fact In facts
                If Not _factIdPerBaseFact.ContainsKey(fact) Then
                    Dim result As String = String.Empty
                    Dim factId As String = IdentifierHelper.GetStrippedId(fact.Id)
                    If Not String.IsNullOrEmpty(factId) AndAlso _factIdsPerScoringParameter.ContainsKey(factId) Then
                        Dim scoringPrm = _factIdsPerScoringParameter(factId)
                        If scoringPrm.GetType Is GetType(MultiChoiceScoringParameter) AndAlso Not DirectCast(scoringPrm, MultiChoiceScoringParameter).IsSingleValue Then
                            result = IdHelper.GetStrippedId(fact.Values(0).Domain)
                        End If
                    End If

                    If String.IsNullOrEmpty(result) AndAlso fact.Values IsNot Nothing AndAlso fact.Values.Count > 0 Then
                        result = IdHelper.GetStrippedId(fact.Values(0).Domain, True)
                        If _factIdsPerScoringParameter.Any(Function(fid) GetIdFromScoringParameter(fid.Value).Equals(result)) Then
                            Select Case _factIdsPerScoringParameter.First(Function(fid) GetIdFromScoringParameter(fid.Value).Equals(result)).Value.GetType
                                Case GetType(CasEqualStepsScoringParameter), GetType(MathCasEqualScoringParameter)
                                    result = IdHelper.GetStrippedId(fact.Id)
                            End Select
                        End If
                    End If
                    _factIdPerBaseFact.Add(fact, result)
                End If
            Next
        End Sub

        Public Function GetFactIdentifier(fact As BaseFact) As String
            Return If(_factIdPerBaseFact.ContainsKey(fact), _factIdPerBaseFact(fact), String.Empty)
        End Function

        Private Function GetResponseDeclarationTypeByScoringParameter(scoringPrm As ScoringParameter) As IResponseDeclaration
            Dim responseDeclarationType As IResponseDeclaration = Nothing
            Select Case scoringPrm.GetType
                Case GetType(MultiChoiceScoringParameter), GetType(HotspotScoringParameter)
                    responseDeclarationType = New ResponseDeclarationMultipleResponse()
                Case GetType(MatrixScoringParameter)
                    responseDeclarationType = New ResponseDeclarationMatrix()
                Case GetType(IntegerScoringParameter), GetType(StringScoringParameter), GetType(DateScoringParameter), GetType(TimeScoringParameter), GetType(CurrencyScoringParameter), GetType(DecimalScoringParameter), GetType(HotTextCorrectionScoringParameter), GetType(MathScoringParameter), GetType(GeogebraScoringParameter)
                    responseDeclarationType = New ResponseDeclarationInput(CombinedScoringHelper.GetGapTypeByScoringParameter(scoringPrm))
                Case GetType(GapMatchScoringParameter), GetType(GapMatchRichTextScoringParameter)
                    responseDeclarationType = New ResponseDeclarationGapMatch()
                Case GetType(GraphGapMatchScoringParameter)
                    responseDeclarationType = New ResponseDeclarationGraphicGapMatch(DirectCast(scoringPrm, GraphGapMatchScoringParameter).IsCategorizationVariant)
                Case GetType(InlineChoiceScoringParameter)
                    responseDeclarationType = New ResponseDeclarationChoice()
                Case GetType(HotTextScoringParameter)
                    responseDeclarationType = New ResponseDeclarationHottext()
                Case GetType(OrderScoringParameter)
                    responseDeclarationType = New ResponseDeclarationOrder()
                Case GetType(MathCasEqualScoringParameter), GetType(CasEqualStepsScoringParameter), GetType(MathCasEvaluateScoringParameter), GetType(MathCasDependencyScoringParameter)
                    responseDeclarationType = New ResponseDeclarationCas()
                Case GetType(SelectPointScoringParameter)
                    responseDeclarationType = New ResponseDeclarationSelectPoint()
            End Select
            Return responseDeclarationType
        End Function

        Private Function GetResponseDeclarationTypeByControlType(fact As KeyFact) As IResponseDeclaration
            Dim responseDeclarationType As IResponseDeclaration = Nothing
            Select Case _controlType
                Case CombinedScoringHelper.EnumControlType.MultipleChoice
                    responseDeclarationType = New ResponseDeclarationMultipleResponse()
                Case CombinedScoringHelper.EnumControlType.Choice
                    responseDeclarationType = New ResponseDeclarationChoice()
                Case CombinedScoringHelper.EnumControlType.Input
                    Dim responseIdentifierAttribute As XmlNode = GetResponseIdentifierForValue(fact)
                    Dim factGapType As CombinedScoringHelper.EnumGapType = CombinedScoringHelper.GetGapTypeByKeyValue(responseIdentifierAttribute, CType(fact.Values(0), KeyValue))
                    responseDeclarationType = New ResponseDeclarationInput(factGapType)
                Case CombinedScoringHelper.EnumControlType.Gapmatch
                    responseDeclarationType = New ResponseDeclarationGapMatch()
            End Select
            Return responseDeclarationType
        End Function

        Private Function GetConceptResponseProcessingTypeByScoringParameter(scoringPrm As ScoringParameter, responseIndex As Integer, Optional responseSubIndex As Integer = 0) As IResponseProcessing
            Dim ret As IResponseProcessing = Nothing
            Select Case scoringPrm.GetType
                Case GetType(DateScoringParameter),
                     GetType(IntegerScoringParameter),
                     GetType(StringScoringParameter),
                     GetType(TimeScoringParameter),
                     GetType(HotTextCorrectionScoringParameter),
                     GetType(CasEqualStepsScoringParameter),
                     GetType(GeogebraScoringParameter)

                    If scoringPrm.SupportCasScoring AndAlso CombinedScoringHelper.IsDependencyGap(scoringPrm, _finding, _factIdsPerScoringParameter) Then
                        ret = New ConceptResponseProcessingInputFormula(responseIndex, CombinedScoringHelper.FormulaItemType.EvaluateDependency, _owner.GetResponseProcessingCustomOperators, GetDependentVariablesForFormulaGap(), responseSubIndex)
                    ElseIf TypeOf scoringPrm Is GeogebraScoringParameter Then
                        ret = New ConceptResponseProcessingGeogebra(responseIndex, CType(_owner.GetResponseProcessingCustomOperators, ResponseProcessingCustomOperators), scoringPrm, responseSubIndex)
                    Else
                        ret = New ConceptResponseProcessingInput(responseIndex, CType(_owner.GetResponseProcessingCustomOperators, ResponseProcessingCustomOperators), scoringPrm, responseSubIndex)
                    End If
                Case GetType(CurrencyScoringParameter), GetType(DecimalScoringParameter)
                    If scoringPrm.SupportCasScoring AndAlso CombinedScoringHelper.IsDependencyGap(scoringPrm, _finding, _factIdsPerScoringParameter) Then
                        ret = New ConceptResponseProcessingInputFormula(responseIndex, CombinedScoringHelper.FormulaItemType.EvaluateDependency, _owner.GetResponseProcessingCustomOperators, GetDependentVariablesForFormulaGap(), responseSubIndex)
                    ElseIf _responseIndexPerIdentifier.ContainsKey(GetIdFromScoringParameter(scoringPrm)) Then
                        ret = New ConceptResponseProcessingInput(responseIndex, CType(_owner.GetResponseProcessingCustomOperators, ResponseProcessingCustomOperators), scoringPrm, CombinedScoringHelper.DetermineDecimalSeparatorForGap(CType(_responseIdentifierAttributeList(GetResponseIndexByIdentifier(GetIdFromScoringParameter(scoringPrm)) - 1), XmlAttribute)), responseSubIndex)
                    Else
                        Debug.Assert(False, "GetConceptResponseProcessingTypeByScoringParameter: unable to determine decimal separator. Should not occur !")
                    End If
                Case GetType(MathScoringParameter),
                    GetType(MathCasDependencyScoringParameter),
                    GetType(MathCasEqualScoringParameter),
                    GetType(MathCasEvaluateScoringParameter)

                    Dim formulaItemType As CombinedScoringHelper.FormulaItemType = CombinedScoringHelper.GetFormulaItemType(_finding, scoringPrm)
                    If formulaItemType = CombinedScoringHelper.FormulaItemType.EvaluateDependency Then
                        ret = New ConceptResponseProcessingInputFormula(responseIndex, formulaItemType, _owner.GetResponseProcessingCustomOperators, GetDependentVariablesForFormulaGap(), responseSubIndex)
                    Else
                        ret = New ConceptResponseProcessingInputFormula(responseIndex, formulaItemType, _owner.GetResponseProcessingCustomOperators, CombinedScoringHelper.IsMultiLineFormulaGap(CType(_responseIdentifierAttributeList(GetResponseIndexByIdentifier(GetIdFromScoringParameter(scoringPrm)) - 1), XmlAttribute)), responseSubIndex)
                    End If
                Case GetType(GapMatchScoringParameter), GetType(GapMatchRichTextScoringParameter)
                    ret = New ConceptResponseProcessingGapMatch(responseIndex, scoringPrm, responseSubIndex)
                Case GetType(GraphGapMatchScoringParameter)
                    ret = New ConceptResponseProcessingGraphicGapMatch(responseIndex, scoringPrm, responseSubIndex)
                Case GetType(HotspotScoringParameter), GetType(HotTextScoringParameter), GetType(MultiChoiceScoringParameter)
                    ret = New ConceptResponseProcessingMultipleResponse(responseIndex, responseSubIndex)
                Case GetType(InlineChoiceScoringParameter)
                    ret = New ConceptResponseProcessingChoice(responseIndex, responseSubIndex)
                Case GetType(MatrixScoringParameter)
                    ret = New ConceptResponseProcessingMatrix(responseIndex, responseSubIndex)
                Case GetType(OrderScoringParameter)
                    ret = New ConceptResponseProcessingOrder(responseIndex, responseSubIndex)
                Case GetType(SelectPointScoringParameter)
                    ret = New ConceptResponseProcessingSelectPoint(responseIndex, responseSubIndex)
            End Select
            Return ret
        End Function

        Private Function GetResponseProcessingTypeByScoringParameter(scoringPrm As ScoringParameter, responseIndex As Integer, Optional responseSubIndex As Integer = 0) As IResponseProcessing
            Dim responseProcessing As IResponseProcessing = Nothing
            Select Case scoringPrm.GetType
                Case GetType(CurrencyScoringParameter),
                     GetType(DateScoringParameter),
                     GetType(DecimalScoringParameter),
                     GetType(IntegerScoringParameter),
                     GetType(StringScoringParameter),
                     GetType(TimeScoringParameter),
                     GetType(HotTextCorrectionScoringParameter),
                     GetType(CasEqualStepsScoringParameter),
                     GetType(GeogebraScoringParameter)

                    Dim gapType = CombinedScoringHelper.GetGapTypeByScoringParameter(scoringPrm)
                    If scoringPrm.SupportCasScoring AndAlso CombinedScoringHelper.IsDependencyGap(scoringPrm, _finding, _factIdsPerScoringParameter) Then
                        responseProcessing = New ResponseProcessingInputFormula(responseIndex, CombinedScoringHelper.FormulaItemType.EvaluateDependency, _owner.GetResponseProcessingCustomOperators, GetDependentVariablesForFormulaGap(), responseSubIndex)
                    ElseIf (gapType = CombinedScoringHelper.EnumGapType.DecimalGap OrElse gapType = CombinedScoringHelper.EnumGapType.CurrencyGap) AndAlso _responseIndexPerIdentifier.ContainsKey(GetIdFromScoringParameter(scoringPrm)) Then
                        responseProcessing = New ResponseProcessingInput(responseIndex, gapType, CombinedScoringHelper.DetermineDecimalSeparatorForGap(CType(_responseIdentifierAttributeList(GetResponseIndexByIdentifier(GetIdFromScoringParameter(scoringPrm)) - 1), XmlAttribute)), _owner.GetResponseProcessingCustomOperators, responseSubIndex)
                    ElseIf TypeOf scoringPrm Is GeogebraScoringParameter Then
                        responseProcessing = New ResponseProcessingGeogebra(responseIndex, gapType, _owner.GetResponseProcessingCustomOperators, responseSubIndex)
                    Else
                        responseProcessing = New ResponseProcessingInput(responseIndex, gapType, _owner.GetResponseProcessingCustomOperators, responseSubIndex)
                    End If

                Case GetType(MathScoringParameter),
                     GetType(MathCasDependencyScoringParameter),
                     GetType(MathCasEqualScoringParameter),
                     GetType(MathCasEvaluateScoringParameter)

                    Dim formulaItemType As CombinedScoringHelper.FormulaItemType = CombinedScoringHelper.GetFormulaItemType(_finding, scoringPrm)

                    If formulaItemType = CombinedScoringHelper.FormulaItemType.EvaluateDependency Then
                        responseProcessing = New ResponseProcessingInputFormula(responseIndex, formulaItemType, _owner.GetResponseProcessingCustomOperators, GetDependentVariablesForFormulaGap(), responseSubIndex)
                    Else
                        responseProcessing = New ResponseProcessingInputFormula(responseIndex, formulaItemType, _owner.GetResponseProcessingCustomOperators, CombinedScoringHelper.IsMultiLineFormulaGap(CType(_responseIdentifierAttributeList(GetResponseIndexByIdentifier(GetIdFromScoringParameter(scoringPrm)) - 1), XmlAttribute)), responseSubIndex)
                    End If

                Case GetType(GapMatchScoringParameter),
                     GetType(GapMatchRichTextScoringParameter)
                    responseProcessing = New ResponseProcessingGapMatch(responseIndex, scoringPrm, responseSubIndex)

                Case GetType(GraphGapMatchScoringParameter)
                    responseProcessing = New ResponseProcessingGraphicGapMatch(responseIndex, scoringPrm, responseSubIndex)

                Case GetType(HotspotScoringParameter),
                        GetType(HotTextScoringParameter),
                        GetType(MultiChoiceScoringParameter)
                    responseProcessing = New ResponseProcessingMultipleResponse(responseIndex, responseSubIndex)

                Case GetType(InlineChoiceScoringParameter)
                    responseProcessing = New ResponseProcessingChoice(responseIndex, responseSubIndex)

                Case GetType(MatrixScoringParameter)
                    responseProcessing = New ResponseProcessingMatrix(responseIndex, responseSubIndex)

                Case GetType(OrderScoringParameter)
                    responseProcessing = New ResponseProcessingOrder(responseIndex, responseSubIndex)

                Case GetType(SelectPointScoringParameter)
                    responseProcessing = New ResponseProcessingSelectPoint(responseIndex, responseSubIndex)

            End Select
            Return responseProcessing
        End Function

        Private Function GetDependentVariablesForFormulaGap() As List(Of Tuple(Of String, Integer, Boolean))
            Dim result As New List(Of Tuple(Of String, Integer, Boolean))
            Dim idx As Integer = 1
            For Each scoringPrm As ScoringParameter In _factIdsPerScoringParameter.Values.Where(Function(sp) TypeOf sp Is IntegerScoringParameter OrElse TypeOf sp Is DecimalScoringParameter).OrderBy(Function(mcdsp) GetResponseIndexByIdentifier(GetIdFromScoringParameter(mcdsp)))
                Dim responseIndex As Integer = GetResponseIndexByIdentifier(GetIdFromScoringParameter(scoringPrm))
                Debug.Assert(responseIndex > 0, "GetDependentVariablesForFormulaGap: responseIndex = 0. Should not occur !")
                result.Add(New Tuple(Of String, Integer, Boolean)(scoringPrm.Label, responseIndex, TypeOf scoringPrm Is DecimalScoringParameter))
            Next

            For Each scoringPrm As ScoringParameter In _factIdsPerScoringParameter.Values.Where(Function(sp) TypeOf sp Is MathCasDependencyScoringParameter).OrderBy(Function(mcdsp) GetResponseIndexByIdentifier(GetIdFromScoringParameter(mcdsp)))
                Dim responseIndex As Integer = GetResponseIndexByIdentifier(GetIdFromScoringParameter(scoringPrm))
                Debug.Assert(responseIndex > 0, "GetDependentVariablesForFormulaGap: responseIndex = 0. Should not occur !")
                result.Add(New Tuple(Of String, Integer, Boolean)(ChrW(idx + 64).ToString.ToLower, responseIndex, True))
                idx += 1
            Next
            Return result
        End Function

        Private Function GetResponseProcessingTypeByControlType(fact As KeyFact, responseId As Integer, scoringParameter As ScoringParameter) As IResponseProcessing
            Dim ret As IResponseProcessing = Nothing
            Select Case _controlType
                Case CombinedScoringHelper.EnumControlType.Choice
                    ret = New ResponseProcessingChoice(responseId)
                Case CombinedScoringHelper.EnumControlType.Gapmatch
                    ret = New ResponseProcessingGapMatch(responseId, scoringParameter)
                Case CombinedScoringHelper.EnumControlType.Input
                    Dim responseIdentifierAttribute As XmlNode = GetResponseIdentifierForValue(fact)
                    Dim factGapType As CombinedScoringHelper.EnumGapType = CombinedScoringHelper.GetGapTypeByKeyValue(responseIdentifierAttribute, CType(fact.Values(0), KeyValue))
                    If factGapType = CombinedScoringHelper.EnumGapType.FormulaGap Then
                        Dim formulaItemType As CombinedScoringHelper.FormulaItemType = CombinedScoringHelper.GetFormulaItemTypeByFinding(_finding)
                        Debug.Assert(formulaItemType <> CombinedScoringHelper.FormulaItemType.EvaluateDependency, "Formula-item without scoringparameters, has formulaItemType 'EvaluateDependency'. Should not occur !")
                        ret = New ResponseProcessingInputFormula(responseId, formulaItemType, CType(_owner.GetResponseProcessingCustomOperators, ResponseProcessingCustomOperators), CombinedScoringHelper.IsMultiLineFormulaGap(CType(responseIdentifierAttribute, XmlAttribute)))
                    ElseIf factGapType = CombinedScoringHelper.EnumGapType.DecimalGap OrElse factGapType = CombinedScoringHelper.EnumGapType.CurrencyGap Then
                        ret = New ResponseProcessingInput(responseId, factGapType, CombinedScoringHelper.DetermineDecimalSeparatorForGap(CType(responseIdentifierAttribute, XmlAttribute)), CType(_owner.GetResponseProcessingCustomOperators, ResponseProcessingCustomOperators))
                    Else
                        ret = New ResponseProcessingInput(responseId, factGapType, CType(_owner.GetResponseProcessingCustomOperators, ResponseProcessingCustomOperators))
                    End If
                Case CombinedScoringHelper.EnumControlType.MultipleChoice
                    ret = New ResponseProcessingMultipleResponse(responseId)
            End Select
            Return ret
        End Function

        Private Function GetIdFromScoringParameter(scoringPrm As ScoringParameter) As String
            If Not String.IsNullOrEmpty(scoringPrm.InlineId) Then
                Return scoringPrm.InlineId
            ElseIf Not String.IsNullOrEmpty(scoringPrm.FindingOverride) Then
                Return scoringPrm.FindingOverride
            Else
                Return scoringPrm.ControllerId
            End If
        End Function

        Private Function GetResponseIdentifierForValue(fact As KeyFact) As XmlNode
            If fact IsNot Nothing AndAlso fact.Values.Count > 0 Then
                Dim idx As Integer = GetResponseIndexByIdentifier(fact.Values(0).Domain)
                If idx > 0 Then Return _responseIdentifierAttributeList(idx - 1)
            End If
            For Each responseIdentifierAttribute As XmlNode In _responseIdentifierAttributeList
                If (_finding IsNot Nothing AndAlso responseIdentifierAttribute.Value = _finding.Id) OrElse responseIdentifierAttribute.Value = fact.Values(0).Domain Then
                    Return responseIdentifierAttribute
                End If
            Next
            Throw New ArgumentException()
        End Function

        Private Function GetOrderIndexForScoringParameter(scoringPrm As ScoringParameter, domainOrderRetrievalType As CombinedScoringHelper.DomainOrderRetrievalType) As Dictionary(Of String, Integer)
            Return CombinedScoringHelper.GetDomainOrderFromScoringParameter(scoringPrm, domainOrderRetrievalType)
        End Function

        Private Sub FillResponseIndex()
            For Each kvp As KeyValuePair(Of String, Double) In _responseIndexPerIdentifier.OrderBy(Function(id) id.Value)
                For Each scoringPrm As ScoringParameter In GetScoringParamFromFactIdsPerScoringParameterById(kvp.Key)
                    Dim ciScoringPrmIndex As Integer = 0
                    If scoringPrm.ControllerId IsNot Nothing Then ciScoringPrmIndex = GetIndexForCustomInteractionScoringParameters(scoringPrm.ControllerId)
                    If ciScoringPrmIndex > 0 Then
                        If TypeOf scoringPrm Is DecimalScoringParameter Then
                            Dim orderIndexPerScoringPrm As Dictionary(Of String, Integer) = GetOrderIndexForScoringParameter(scoringPrm, CombinedScoringHelper.DomainOrderRetrievalType.FactId)
                            For Each oiKvp As KeyValuePair(Of String, Integer) In orderIndexPerScoringPrm.OrderBy(Function(oiKvp2) oiKvp2.Value)
                                If Not _responseIndexPerIdentifier.ContainsKey(oiKvp.Key) Then _responseIndexPerIdentifier.Add(oiKvp.Key, CDbl(kvp.Value + (ciScoringPrmIndex / 100) + (oiKvp.Value / 10000)))
                            Next
                        End If
                        If Not _responseIndexPerIdentifier.ContainsKey(scoringPrm.ControllerId) Then
                            _responseIndexPerIdentifier.Add(scoringPrm.ControllerId, CDbl(kvp.Value + (ciScoringPrmIndex / 100)))
                        End If
                    End If

                    If scoringPrm.Value.Count > 1 AndAlso ((TypeOf scoringPrm Is MultiChoiceScoringParameter AndAlso Not scoringPrm.IsSingleValue) OrElse ((TypeOf scoringPrm Is ChoiceScoringParameter AndAlso Not TypeOf scoringPrm Is MultiChoiceScoringParameter) OrElse TypeOf scoringPrm Is GapMatchScoringParameter OrElse TypeOf scoringPrm Is GraphGapMatchScoringParameter OrElse TypeOf scoringPrm Is MatrixScoringParameter) AndAlso Not scoringPrm.IsSingleChoice) Then
                        Dim orderIndexPerScoringPrm As Dictionary(Of String, Integer) = GetOrderIndexForScoringParameter(scoringPrm, CombinedScoringHelper.DomainOrderRetrievalType.All)
                        For Each oiKvp As KeyValuePair(Of String, Integer) In orderIndexPerScoringPrm.OrderBy(Function(oiKvp2) oiKvp2.Value)
                            If Not _responseIndexPerIdentifier.ContainsKey(oiKvp.Key) Then _responseIndexPerIdentifier.Add(oiKvp.Key, CDbl(kvp.Value + (ciScoringPrmIndex / 100) + (oiKvp.Value / 10000)))
                        Next
                    ElseIf TypeOf scoringPrm Is CasEqualStepsScoringParameter OrElse TypeOf scoringPrm Is MathCasEqualScoringParameter OrElse TypeOf scoringPrm Is MathCasEvaluateScoringParameter Then
                        Dim manipulator = scoringPrm.GetScoreManipulator(New Solution())
                        Dim subsetIdentifier As String = manipulator.GetFactIdForKey(scoringPrm.Value(0).Id)
                        If Not _responseIndexPerIdentifier.ContainsKey(subsetIdentifier) Then _responseIndexPerIdentifier.Add(subsetIdentifier, CDbl(kvp.Value + (ciScoringPrmIndex / 100) + (GetIndexForCasEqualScoringParameter(scoringPrm.Value(0).Id) / 10000)))
                    End If
                Next
            Next
        End Sub

        Private Sub FillConceptIndex()
            Dim idx As Integer = 1
            For Each kvp As KeyValuePair(Of String, Double) In _responseIndexPerIdentifier.Where(Function(resp) resp.Value = Math.Floor(resp.Value)).OrderBy(Function(id) id.Value)
                For Each scoringPrm As ScoringParameter In GetScoringParamFromFactIdsPerScoringParameterById(kvp.Key)
                    Dim ciScoringPrmIndex As Integer = 0
                    If scoringPrm.ControllerId IsNot Nothing Then ciScoringPrmIndex = GetIndexForCustomInteractionScoringParameters(scoringPrm.ControllerId)
                    If ciScoringPrmIndex > 0 Then
                        If TypeOf scoringPrm Is DecimalScoringParameter Then
                            Dim orderIndexPerScoringPrm As Dictionary(Of String, Integer) = GetOrderIndexForScoringParameter(scoringPrm, CombinedScoringHelper.DomainOrderRetrievalType.FactId)
                            For Each oiKvp As KeyValuePair(Of String, Integer) In orderIndexPerScoringPrm.OrderBy(Function(oiKvp2) oiKvp2.Value)
                                If Not _conceptIndexPerIdentifier.ContainsKey(oiKvp.Key) Then _conceptIndexPerIdentifier.Add(oiKvp.Key, idx)
                                If Not _conceptIndexPerIdentifier.ContainsKey(scoringPrm.ControllerId) Then _conceptIndexPerIdentifier.Add(scoringPrm.ControllerId, idx)
                                idx += 1
                            Next
                        ElseIf Not _conceptIndexPerIdentifier.ContainsKey(scoringPrm.ControllerId) Then
                            _conceptIndexPerIdentifier.Add(scoringPrm.ControllerId, idx)
                            idx += 1
                        End If
                    End If
                    If TypeOf scoringPrm Is ChoiceScoringParameter OrElse TypeOf scoringPrm Is GapMatchScoringParameter OrElse TypeOf scoringPrm Is GraphGapMatchScoringParameter OrElse TypeOf scoringPrm Is MatrixScoringParameter Then
                        If ((TypeOf scoringPrm Is MultiChoiceScoringParameter AndAlso Not scoringPrm.IsSingleValue) OrElse Not TypeOf scoringPrm Is MultiChoiceScoringParameter) AndAlso Not scoringPrm.IsSingleChoice AndAlso scoringPrm.Value.Count > 1 Then
                            Dim orderIndexPerScoringPrm As Dictionary(Of String, Integer)
                            If TypeOf scoringPrm Is GapMatchScoringParameter Then
                                orderIndexPerScoringPrm = GetOrderIndexForScoringParameter(scoringPrm, CombinedScoringHelper.DomainOrderRetrievalType.CollectionId)
                            ElseIf TypeOf scoringPrm Is GraphGapMatchScoringParameter Then
                                orderIndexPerScoringPrm = GetOrderIndexForScoringParameter(scoringPrm, CombinedScoringHelper.DomainOrderRetrievalType.FactId)
                            Else
                                orderIndexPerScoringPrm = GetOrderIndexForScoringParameter(scoringPrm, CombinedScoringHelper.DomainOrderRetrievalType.Domain)
                            End If
                            For Each oiKvp As KeyValuePair(Of String, Integer) In orderIndexPerScoringPrm.OrderBy(Function(oiKvp2) oiKvp2.Value)
                                _conceptIndexPerIdentifier.Add(oiKvp.Key, idx)
                                idx += 1
                            Next
                        ElseIf ciScoringPrmIndex = 0 AndAlso Not _conceptIndexPerIdentifier.ContainsKey(kvp.Key) Then
                            _conceptIndexPerIdentifier.Add(kvp.Key, idx)
                            idx += 1
                        End If
                    ElseIf TypeOf scoringPrm Is CasEqualStepsScoringParameter OrElse TypeOf scoringPrm Is MathCasEqualScoringParameter OrElse TypeOf scoringPrm Is MathCasEvaluateScoringParameter Then
                        Dim manipulator = scoringPrm.GetScoreManipulator(New Solution())
                        Dim subsetIdentifier As String = scoringPrm.Value(0).Id
                        _conceptIndexPerIdentifier.Add(manipulator.GetFactIdForKey(subsetIdentifier), idx)
                        idx += 1
                    ElseIf ciScoringPrmIndex = 0 AndAlso Not _conceptIndexPerIdentifier.ContainsKey(kvp.Key) Then
                        _conceptIndexPerIdentifier.Add(kvp.Key, idx)
                        idx += 1
                    End If
                Next
            Next
        End Sub

        Private Function GetIndexForCasEqualScoringParameter(subsetIdentifier As String) As Integer
            Select Case subsetIdentifier.ToLower
                Case "first"
                    Return 1
                Case "second"
                    Return 2
                Case "last"
                    Return 3
            End Select
            Return 0
        End Function

        Private Function GetIndexForCustomInteractionScoringParameters(controllerId As String) As Integer
            If controllerId.StartsWith(ciControllerId) Then
                Dim index As Integer = 0
                If Integer.TryParse(controllerId.Substring(ciControllerId.Length), index) Then
                    Return index + 2
                ElseIf controllerId.StartsWith(String.Concat(ciControllerId, "_"), StringComparison.OrdinalIgnoreCase) AndAlso controllerId.Substring(String.Concat(ciControllerId, "_").Length).IndexOf("_") > 0 Then
                    Dim controllerGuid As String = controllerId.Substring(String.Concat(ciControllerId, "_").Length, controllerId.Substring(String.Concat(ciControllerId, "_").Length).IndexOf("_"))
                    If Not String.IsNullOrEmpty(controllerGuid) AndAlso CombinedScoringHelper.CheckIdentifierIsGuid(controllerGuid) Then
                        If Integer.TryParse(controllerId.Substring(controllerId.LastIndexOf("_") + 1), index) Then
                            Return index + 2
                        End If
                    End If
                End If
            End If
            Return 0
        End Function

        Public Function GetScoringParamFromFactIdsPerScoringParameterById(identifier As String) As List(Of ScoringParameter)
            Dim result As New List(Of ScoringParameter)
            Dim scoringPrms As New List(Of ScoringParameter)
            _factIdsPerScoringParameter.Values.ToList.ForEach(Sub(fid)
                                                                  Dim id As String = GetIdFromScoringParameter(fid)
                                                                  If Not String.IsNullOrEmpty(id) Then
                                                                      If id.Equals(identifier, StringComparison.OrdinalIgnoreCase) Then
                                                                          scoringPrms.Add(fid)
                                                                      ElseIf CombinedScoringHelper.GetGuidPartOfIdentifier(id).Equals(identifier, StringComparison.OrdinalIgnoreCase) Then
                                                                          scoringPrms.Add(fid)
                                                                      End If
                                                                  End If
                                                              End Sub)
            If scoringPrms.Any() Then
                If ScoringPrmBelongsToCustomInteraction(scoringPrms(0)) Then
                    For Each scPrm As ScoringParameter In scoringPrms.OrderBy(Function(s) GetIndexForCustomInteractionScoringParameters(s.ControllerId))
                        result.Add(scPrm)
                    Next
                Else
                    Select Case scoringPrms(0).GetType
                        Case GetType(CasEqualStepsScoringParameter), GetType(MathCasEqualScoringParameter), GetType(MathCasEvaluateScoringParameter)
                            For Each scPrm As ScoringParameter In scoringPrms.OrderBy(Function(s) GetIndexForCasEqualScoringParameter(s.Value(0).Id))
                                result.Add(scPrm)
                            Next
                        Case Else
                            result.Add(scoringPrms(0))
                    End Select
                End If
            End If
            Return result
        End Function

        Private Function ScoringPrmBelongsToCustomInteraction(scoringPrm As ScoringParameter) As Boolean
            Return (scoringPrm IsNot Nothing AndAlso Not TypeOf scoringPrm Is GeogebraScoringParameter AndAlso scoringPrm.ControllerId IsNot Nothing AndAlso scoringPrm.ControllerId.StartsWith(ciControllerId))
        End Function

    End Class
End Namespace