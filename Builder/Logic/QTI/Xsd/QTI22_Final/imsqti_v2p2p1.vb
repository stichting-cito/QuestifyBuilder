#Disable Warning
Imports System.Collections.Generic
Imports System.Linq

Namespace QTI.Xsd.QTI22_Final

    <System.Xml.Serialization.XmlIncludeAttribute(GetType(WeightType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(VariableMappingType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(VariableType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(TimeLimitsType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(TestVariablesType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(StyleSheetType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(RandomIntegerType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(RandomFloatType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(PrintedVariableType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(ParamType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(OutcomeMinMaxType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(NumberType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(MathConstantType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(MatchTableEntryType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(MapResponseType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(MapEntryType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(ItemSessionControlType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(InterpolationTableEntryType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(DefaultType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(CorrectType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(BaseSequenceXBaseEmptyType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(TextEntryInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(ImgType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(HRType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(HotspotChoiceType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(GapType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(EndAttemptInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(ColType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(BRType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(AssociableHotspotType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(AssessmentStimulusRefType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(AssessmentSectionRefType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(AreaMapEntryType)),
        System.Xml.Serialization.XmlRootAttribute("exitResponse", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class EmptyPrimitiveTypeType
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="OutcomeElse.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("outcomeElse", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class OutcomeElseType

        Private _items As List(Of Object)

        <System.Xml.Serialization.XmlElementAttribute("exitTest", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lookupOutcomeValue", GetType(LookupOutcomeValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeCondition", GetType(OutcomeConditionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeProcessingFragment", GetType(OutcomeProcessingFragmentType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("setOutcomeValue", GetType(SetValueType), Order:=0)>
        Public Property Items() As List(Of Object)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="LookupOutcomeValue.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("lookupOutcomeValue", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class LookupOutcomeValueType

        Private _item As Object

        Private _itemElementName As ItemChoiceType11

        Private _identifier As String

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType11
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Logic1toMany.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("and", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class Logic1toManyType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType10

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType10()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="AnyN.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("anyN", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class AnyNType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType11

        Private _min As String

        Private _max As String

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType11()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property min() As String
            Get
                Return Me._min
            End Get
            Set
                Me._min = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property max() As String
            Get
                Return Me._max
            End Get
            Set
                Me._max = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minSpecified() As Boolean
            Get
                Return (Equals(_min, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxSpecified() As Boolean
            Get
                Return (Equals(_max, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="BaseValue.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("baseValue", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class BaseValueType

        Private _baseType As BaseValueTypeBaseType

        Private _value As String

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property baseType() As BaseValueTypeBaseType
            Get
                Return Me._baseType
            End Get
            Set
                Me._baseType = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Value() As String
            Get
                Return Me._value
            End Get
            Set
                Me._value = Value
            End Set
        End Property

        Public ReadOnly Property baseTypeSpecified() As Boolean
            Get
                Return (Equals(_baseType, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ValueSpecified() As Boolean
            Get
                Return (Equals(_value, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseValueTypeBaseType

        [boolean]

        directedPair

        duration

        file

        float

        identifier

        [integer]

        pair

        point

        [string]

        uri
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="LogicSingle.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("containerSize", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class LogicSingleType

        Private _item As Object

        Private _itemElementName As ItemChoiceType6

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType6
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="LogicPair.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("contains", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class LogicPairType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType9

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType9()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Correct.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("correct", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class CorrectType
        Inherits EmptyPrimitiveTypeType

        Private _identifier As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="CustomOperator.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("customOperator", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class CustomOperatorType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType6

        Private _any As List(Of System.Xml.XmlElement)

        Private _class As String

        Private _definition As String

        Private _anyAttr As List(Of System.Xml.XmlAttribute)

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType6()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=2)>
        Public Property Any() As List(Of System.Xml.XmlElement)
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property [class]() As String
            Get
                Return Me._class
            End Get
            Set
                Me._class = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property definition() As String
            Get
                Return Me._definition
            End Get
            Set
                Me._definition = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As List(Of System.Xml.XmlAttribute)
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property AnySpecified() As Boolean
            Get
                Return (Equals(_any, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property classSpecified() As Boolean
            Get
                Return (Equals(_class, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property definitionSpecified() As Boolean
            Get
                Return (Equals(_definition, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property AnyAttrSpecified() As Boolean
            Get
                Return (Equals(_anyAttr, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Default.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("default", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class DefaultType
        Inherits EmptyPrimitiveTypeType

        Private _identifier As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Equal.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("equal", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class EqualType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType3

        Private _toleranceMode As EqualTypeToleranceMode

        Private _tolerance As List(Of String)

        Private _includeLowerBound As Boolean

        Private _includeUpperBound As Boolean

        Public Sub New()
            MyBase.New
            Me._toleranceMode = EqualTypeToleranceMode.exact
            Me._includeLowerBound = True
            Me._includeUpperBound = True
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType3()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property tolerance() As List(Of String)
            Get
                Return Me._tolerance
            End Get
            Set
                Me._tolerance = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(True)>
        Public Property includeLowerBound() As Boolean
            Get
                Return Me._includeLowerBound
            End Get
            Set
                Me._includeLowerBound = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(True)>
        Public Property includeUpperBound() As Boolean
            Get
                Return Me._includeUpperBound
            End Get
            Set
                Me._includeUpperBound = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property toleranceModeSpecified() As Boolean
            Get
                Return (Equals(_toleranceMode, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property toleranceSpecified() As Boolean
            Get
                Return (Equals(_tolerance, Nothing) <> True AndAlso _tolerance.Any())
            End Get
        End Property

        Public ReadOnly Property includeLowerBoundSpecified() As Boolean
            Get
                Return (Equals(_includeLowerBound, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property includeUpperBoundSpecified() As Boolean
            Get
                Return (Equals(_includeUpperBound, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="EqualRounded.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("equalRounded", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class EqualRoundedType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType4

        Private _roundingMode As EqualRoundedTypeRoundingMode

        Private _figures As String

        Public Sub New()
            MyBase.New
            Me._roundingMode = EqualRoundedTypeRoundingMode.significantFigures
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType4()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(EqualRoundedTypeRoundingMode.significantFigures)>
        Public Property roundingMode() As EqualRoundedTypeRoundingMode
            Get
                Return Me._roundingMode
            End Get
            Set
                Me._roundingMode = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property figures() As String
            Get
                Return Me._figures
            End Get
            Set
                Me._figures = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property roundingModeSpecified() As Boolean
            Get
                Return (Equals(_roundingMode, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property figuresSpecified() As Boolean
            Get
                Return (Equals(_figures, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="FieldValue.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("fieldValue", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class FieldValueType

        Private _item As Object

        Private _itemElementName As ItemChoiceType4

        Private _fieldIdentifier As String

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType4
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property fieldIdentifier() As String
            Get
                Return Me._fieldIdentifier
            End Get
            Set
                Me._fieldIdentifier = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property fieldIdentifierSpecified() As Boolean
            Get
                Return (Equals(_fieldIdentifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Index.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("index", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class IndexType

        Private _item As Object

        Private _itemElementName As ItemChoiceType5

        Private _n As String

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType5
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property n() As String
            Get
                Return Me._n
            End Get
            Set
                Me._n = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property nSpecified() As Boolean
            Get
                Return (Equals(_n, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Inside.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("inside", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class InsideType

        Private _item As Object

        Private _itemElementName As ItemChoiceType3

        Private _shape As InsideTypeShape

        Private _coords As String

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType3
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property shape() As InsideTypeShape
            Get
                Return Me._shape
            End Get
            Set
                Me._shape = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property coords() As String
            Get
                Return Me._coords
            End Get
            Set
                Me._coords = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property shapeSpecified() As Boolean
            Get
                Return (Equals(_shape, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property coordsSpecified() As Boolean
            Get
                Return (Equals(_coords, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="MapResponse.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("mapResponse", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class MapResponseType
        Inherits EmptyPrimitiveTypeType

        Private _identifier As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="MathConstant.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("mathConstant", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class MathConstantType
        Inherits EmptyPrimitiveTypeType

        Private _name As MathConstantTypeName

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property name() As MathConstantTypeName
            Get
                Return Me._name
            End Get
            Set
                Me._name = Value
            End Set
        End Property

        Public ReadOnly Property nameSpecified() As Boolean
            Get
                Return (Equals(_name, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum MathConstantTypeName

        pi

        e
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="MathOperator.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("mathOperator", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class MathOperatorType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType

        Private _name As MathOperatorTypeName

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property name() As MathOperatorTypeName
            Get
                Return Me._name
            End Get
            Set
                Me._name = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property nameSpecified() As Boolean
            Get
                Return (Equals(_name, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Logic0toMany.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("multiple", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class Logic0toManyType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType7

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType7()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Number.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("numberCorrect", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class NumberType
        Inherits EmptyPrimitiveTypeType

        Private _sectionIdentifier As String

        Private _includeCategory As List(Of String)

        Private _excludeCategory As List(Of String)

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property sectionIdentifier() As String
            Get
                Return Me._sectionIdentifier
            End Get
            Set
                Me._sectionIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property includeCategory() As List(Of String)
            Get
                Return Me._includeCategory
            End Get
            Set
                Me._includeCategory = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property excludeCategory() As List(Of String)
            Get
                Return Me._excludeCategory
            End Get
            Set
                Me._excludeCategory = Value
            End Set
        End Property

        Public ReadOnly Property sectionIdentifierSpecified() As Boolean
            Get
                Return (Equals(_sectionIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property includeCategorySpecified() As Boolean
            Get
                Return (Equals(_includeCategory, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property excludeCategorySpecified() As Boolean
            Get
                Return (Equals(_excludeCategory, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="OutcomeMinMax.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("outcomeMaximum", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class OutcomeMinMaxType
        Inherits EmptyPrimitiveTypeType

        Private _sectionIdentifier As String

        Private _includeCategory As List(Of String)

        Private _excludeCategory As List(Of String)

        Private _outcomeIdentifier As String

        Private _weightIdentifier As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property sectionIdentifier() As String
            Get
                Return Me._sectionIdentifier
            End Get
            Set
                Me._sectionIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property includeCategory() As List(Of String)
            Get
                Return Me._includeCategory
            End Get
            Set
                Me._includeCategory = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property excludeCategory() As List(Of String)
            Get
                Return Me._excludeCategory
            End Get
            Set
                Me._excludeCategory = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property outcomeIdentifier() As String
            Get
                Return Me._outcomeIdentifier
            End Get
            Set
                Me._outcomeIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property weightIdentifier() As String
            Get
                Return Me._weightIdentifier
            End Get
            Set
                Me._weightIdentifier = Value
            End Set
        End Property

        Public ReadOnly Property sectionIdentifierSpecified() As Boolean
            Get
                Return (Equals(_sectionIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property includeCategorySpecified() As Boolean
            Get
                Return (Equals(_includeCategory, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property excludeCategorySpecified() As Boolean
            Get
                Return (Equals(_excludeCategory, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property outcomeIdentifierSpecified() As Boolean
            Get
                Return (Equals(_outcomeIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property weightIdentifierSpecified() As Boolean
            Get
                Return (Equals(_weightIdentifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="PatternMatch.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("patternMatch", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class PatternMatchType

        Private _item As Object

        Private _itemElementName As ItemChoiceType2

        Private _pattern As String

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType2
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property pattern() As String
            Get
                Return Me._pattern
            End Get
            Set
                Me._pattern = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property patternSpecified() As Boolean
            Get
                Return (Equals(_pattern, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="RandomFloat.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("randomFloat", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class RandomFloatType
        Inherits EmptyPrimitiveTypeType

        Private _min As String

        Private _max As String

        Public Sub New()
            MyBase.New
            Me._min = "0"
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property min() As String
            Get
                Return Me._min
            End Get
            Set
                Me._min = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property max() As String
            Get
                Return Me._max
            End Get
            Set
                Me._max = Value
            End Set
        End Property

        Public ReadOnly Property minSpecified() As Boolean
            Get
                Return (Equals(_min, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxSpecified() As Boolean
            Get
                Return (Equals(_max, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="RandomInteger.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("randomInteger", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class RandomIntegerType
        Inherits EmptyPrimitiveTypeType

        Private _min As String

        Private _max As String

        Private _step As String

        Public Sub New()
            MyBase.New
            Me._min = "0"
            Me._step = "1"
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property min() As String
            Get
                Return Me._min
            End Get
            Set
                Me._min = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property max() As String
            Get
                Return Me._max
            End Get
            Set
                Me._max = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute("1")>
        Public Property [step]() As String
            Get
                Return Me._step
            End Get
            Set
                Me._step = Value
            End Set
        End Property

        Public ReadOnly Property minSpecified() As Boolean
            Get
                Return (Equals(_min, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxSpecified() As Boolean
            Get
                Return (Equals(_max, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stepSpecified() As Boolean
            Get
                Return (Equals(_step, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Repeat.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("repeat", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class RepeatType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType1

        Private _numberRepeats As String

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType1()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property numberRepeats() As String
            Get
                Return Me._numberRepeats
            End Get
            Set
                Me._numberRepeats = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property numberRepeatsSpecified() As Boolean
            Get
                Return (Equals(_numberRepeats, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="RoundTo.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("roundTo", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class RoundToType

        Private _item As Object

        Private _itemElementName As ItemChoiceType1

        Private _roundingMode As RoundToTypeRoundingMode

        Private _figures As String

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType1
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property roundingMode() As RoundToTypeRoundingMode
            Get
                Return Me._roundingMode
            End Get
            Set
                Me._roundingMode = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property figures() As String
            Get
                Return Me._figures
            End Get
            Set
                Me._figures = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property roundingModeSpecified() As Boolean
            Get
                Return (Equals(_roundingMode, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property figuresSpecified() As Boolean
            Get
                Return (Equals(_figures, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="StatsOperator.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("statsOperator", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class StatsOperatorType

        Private _item As Object

        Private _itemElementName As ItemChoiceType

        Private _name As StatsOperatorTypeName

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property name() As StatsOperatorTypeName
            Get
                Return Me._name
            End Get
            Set
                Me._name = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property nameSpecified() As Boolean
            Get
                Return (Equals(_name, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="StringMatch.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("stringMatch", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class StringMatchType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType2

        Private _caseSensitive As Boolean

        Private _substring As Boolean

        Public Sub New()
            MyBase.New
            Me._substring = False
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType2()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property caseSensitive() As Boolean
            Get
                Return Me._caseSensitive
            End Get
            Set
                Me._caseSensitive = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property substring() As Boolean
            Get
                Return Me._substring
            End Get
            Set
                Me._substring = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property caseSensitiveSpecified() As Boolean
            Get
                Return (Equals(_caseSensitive, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property substringSpecified() As Boolean
            Get
                Return (Equals(_substring, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Substring.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("substring", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class SubstringType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType5

        Private _caseSensitive As Boolean

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType5()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property caseSensitive() As Boolean
            Get
                Return Me._caseSensitive
            End Get
            Set
                Me._caseSensitive = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property caseSensitiveSpecified() As Boolean
            Get
                Return (Equals(_caseSensitive, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="NumericLogic1toMany.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("sum", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class NumericLogic1toManyType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType8

        <System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType8()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TestVariables.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("testVariables", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TestVariablesType
        Inherits EmptyPrimitiveTypeType

        Private _sectionIdentifier As String

        Private _includeCategory As List(Of String)

        Private _excludeCategory As List(Of String)

        Private _variableIdentifier As String

        Private _weightIdentifier As String

        Private _baseType As TestVariablesTypeBaseType

        Private baseTypeFieldSpecified As Boolean

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property sectionIdentifier() As String
            Get
                Return Me._sectionIdentifier
            End Get
            Set
                Me._sectionIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property includeCategory() As List(Of String)
            Get
                Return Me._includeCategory
            End Get
            Set
                Me._includeCategory = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property excludeCategory() As List(Of String)
            Get
                Return Me._excludeCategory
            End Get
            Set
                Me._excludeCategory = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property variableIdentifier() As String
            Get
                Return Me._variableIdentifier
            End Get
            Set
                Me._variableIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property weightIdentifier() As String
            Get
                Return Me._weightIdentifier
            End Get
            Set
                Me._weightIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property baseType() As TestVariablesTypeBaseType
            Get
                Return Me._baseType
            End Get
            Set
                Me._baseType = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property baseTypeSpecified() As Boolean
            Get
                Return Me.baseTypeFieldSpecified
            End Get
            Set
                Me.baseTypeFieldSpecified = Value
            End Set
        End Property

        Public ReadOnly Property sectionIdentifierSpecified() As Boolean
            Get
                Return (Equals(_sectionIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property includeCategorySpecified() As Boolean
            Get
                Return (Equals(_includeCategory, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property excludeCategorySpecified() As Boolean
            Get
                Return (Equals(_excludeCategory, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property variableIdentifierSpecified() As Boolean
            Get
                Return (Equals(_variableIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property weightIdentifierSpecified() As Boolean
            Get
                Return (Equals(_weightIdentifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum TestVariablesTypeBaseType

        [boolean]

        directedPair

        duration

        file

        float

        identifier

        [integer]

        pair

        point

        [string]

        uri
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Variable.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("variable", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class VariableType
        Inherits EmptyPrimitiveTypeType

        Private _identifier As String

        Private _weightIdentifier As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property weightIdentifier() As String
            Get
                Return Me._weightIdentifier
            End Get
            Set
                Me._weightIdentifier = Value
            End Set
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property weightIdentifierSpecified() As Boolean
            Get
                Return (Equals(_weightIdentifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType8

        baseValue

        containerSize

        correct

        customOperator

        [default]

        delete

        divide

        fieldValue

        gcd

        index

        integerDivide

        integerModulus

        integerToFloat

        lcm

        mapResponse

        mapResponsePoint

        mathConstant

        mathOperator

        max

        min

        multiple

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        ordered

        outcomeMaximum

        outcomeMinimum

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType5

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType2

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum StatsOperatorTypeName

        mean

        sampleVariance

        sampleSD

        popVariance

        popSD
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType1

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum RoundToTypeRoundingMode

        decimalPlaces

        significantFigures
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType1

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType2

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType7

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum MathOperatorTypeName

        sin

        cos

        tan

        sec

        csc

        cot

        asin

        acos

        atan

        atan2

        asec

        acsc

        acot

        sinh

        cosh

        tanh

        sech

        csch

        coth

        log

        ln

        exp

        abs

        signum

        floor

        ceil

        toDegrees

        toRadians
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType3

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum InsideTypeShape

        circle

        [default]

        ellipse

        poly

        rect
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType5

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType4

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType4

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum EqualRoundedTypeRoundingMode

        decimalPlaces

        significantFigures
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType3

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum EqualTypeToleranceMode

        absolute

        exact

        relative
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType6

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType9

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType6

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType11

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType10

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType11

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Object.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("object", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ObjectType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType35

        Private _text As List(Of String)

        Private _data As String

        Private _type As String

        Private _width As String

        Private _height As String

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("param", GetType(ParamType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType35()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property data() As String
            Get
                Return Me._data
            End Get
            Set
                Me._data = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property type() As String
            Get
                Return Me._type
            End Get
            Set
                Me._type = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property width() As String
            Get
                Return Me._width
            End Get
            Set
                Me._width = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property height() As String
            Get
                Return Me._height
            End Get
            Set
                Me._height = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property dataSpecified() As Boolean
            Get
                Return (Equals(_data, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property typeSpecified() As Boolean
            Get
                Return (Equals(_type, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property widthSpecified() As Boolean
            Get
                Return (Equals(_width, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property heightSpecified() As Boolean
            Get
                Return (Equals(_height, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="A.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("a", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class AType
        Inherits BaseSequenceXBaseType

        Private _items As List(Of Object)

        Private _text As List(Of String)

        Private _href As String

        Private _type As String

        <System.Xml.Serialization.XmlElementAttribute("customInteraction", GetType(CustomInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("endAttemptInteraction", GetType(EndAttemptInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gap", GetType(GapType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottext", GetType(HotTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inlineChoiceInteraction", GetType(InlineChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("textEntryInteraction", GetType(TextEntryInteractionType), Order:=0)>
        Public Property Items() As List(Of Object)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property href() As String
            Get
                Return Me._href
            End Get
            Set
                Me._href = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property type() As String
            Get
                Return Me._type
            End Get
            Set
                Me._type = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property hrefSpecified() As Boolean
            Get
                Return (Equals(_href, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property typeSpecified() As Boolean
            Get
                Return (Equals(_type, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="CustomInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("customInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class CustomInteractionType
        Inherits BaseSequenceFullType

        Private _any As List(Of System.Xml.XmlElement)

        Private _anyAttr1 As List(Of System.Xml.XmlAttribute)

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=0)>
        Public Property Any() As List(Of System.Xml.XmlElement)
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr1() As List(Of System.Xml.XmlAttribute)
            Get
                Return Me._anyAttr1
            End Get
            Set
                Me._anyAttr1 = Value
            End Set
        End Property

        Public ReadOnly Property AnySpecified() As Boolean
            Get
                Return (Equals(_any, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property AnyAttr1Specified() As Boolean
            Get
                Return (Equals(_anyAttr1, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.Xml.Serialization.XmlIncludeAttribute(GetType(GraphicOrderInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(InlineChoiceInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(GraphicGapMatchInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(CustomInteractionType)),
        System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="BaseSequenceFull.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Partial Public Class BaseSequenceFullType

        Private _id As String

        Private _class As List(Of String)

        Private _lang As String

        Private _label As String

        Private _responseIdentifier As String

        Private _base As String

        Private _dir As BaseSequenceFullTypeDir

        Private _role As BaseSequenceFullTypeRole

        Private roleFieldSpecified As Boolean

        Private _ariacontrols As String

        Private _ariadescribedby As String

        Private _ariaflowto As String

        Private _arialabel As String

        Private _arialabelledby As String

        Private _arialevel As String

        Private _arialive As BaseSequenceFullTypeArialive

        Private arialiveFieldSpecified As Boolean

        Private _ariaorientation As BaseSequenceFullTypeAriaorientation

        Private _ariaowns As String

        Private _anyAttr As List(Of System.Xml.XmlAttribute)

        Public Sub New()
            MyBase.New
            Me._dir = BaseSequenceFullTypeDir.[auto]
            Me._ariaorientation = BaseSequenceFullTypeAriaorientation.horizontal
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property id() As String
            Get
                Return Me._id
            End Get
            Set
                Me._id = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property [class]() As List(Of String)
            Get
                Return Me._class
            End Get
            Set
                Me._class = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property lang() As String
            Get
                Return Me._lang
            End Get
            Set
                Me._lang = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property label() As String
            Get
                Return Me._label
            End Get
            Set
                Me._label = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property responseIdentifier() As String
            Get
                Return Me._responseIdentifier
            End Get
            Set
                Me._responseIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property base() As String
            Get
                Return Me._base
            End Get
            Set
                Me._base = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(BaseSequenceFullTypeDir.[auto])>
        Public Property dir() As BaseSequenceFullTypeDir
            Get
                Return Me._dir
            End Get
            Set
                Me._dir = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property role() As BaseSequenceFullTypeRole
            Get
                Return Me._role
            End Get
            Set
                Me._role = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property roleSpecified() As Boolean
            Get
                Return Me.roleFieldSpecified
            End Get
            Set
                Me.roleFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-controls", DataType:="IDREFS")>
        Public Property ariacontrols() As String
            Get
                Return Me._ariacontrols
            End Get
            Set
                Me._ariacontrols = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-describedby", DataType:="IDREFS")>
        Public Property ariadescribedby() As String
            Get
                Return Me._ariadescribedby
            End Get
            Set
                Me._ariadescribedby = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-flowto", DataType:="IDREFS")>
        Public Property ariaflowto() As String
            Get
                Return Me._ariaflowto
            End Get
            Set
                Me._ariaflowto = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-label", DataType:="normalizedString")>
        Public Property arialabel() As String
            Get
                Return Me._arialabel
            End Get
            Set
                Me._arialabel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-labelledby", DataType:="IDREFS")>
        Public Property arialabelledby() As String
            Get
                Return Me._arialabelledby
            End Get
            Set
                Me._arialabelledby = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-level", DataType:="integer")>
        Public Property arialevel() As String
            Get
                Return Me._arialevel
            End Get
            Set
                Me._arialevel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-live")>
        Public Property arialive() As BaseSequenceFullTypeArialive
            Get
                Return Me._arialive
            End Get
            Set
                Me._arialive = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property arialiveSpecified() As Boolean
            Get
                Return Me.arialiveFieldSpecified
            End Get
            Set
                Me.arialiveFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-orientation"),
            System.ComponentModel.DefaultValueAttribute(BaseSequenceFullTypeAriaorientation.horizontal)>
        Public Property ariaorientation() As BaseSequenceFullTypeAriaorientation
            Get
                Return Me._ariaorientation
            End Get
            Set
                Me._ariaorientation = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-owns", DataType:="IDREFS")>
        Public Property ariaowns() As String
            Get
                Return Me._ariaowns
            End Get
            Set
                Me._ariaowns = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As List(Of System.Xml.XmlAttribute)
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property

        Public ReadOnly Property idSpecified() As Boolean
            Get
                Return (Equals(_id, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property classSpecified() As Boolean
            Get
                Return (Equals(_class, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property langSpecified() As Boolean
            Get
                Return (Equals(_lang, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property labelSpecified() As Boolean
            Get
                Return (Equals(_label, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property responseIdentifierSpecified() As Boolean
            Get
                Return (Equals(_responseIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property baseSpecified() As Boolean
            Get
                Return (Equals(_base, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property dirSpecified() As Boolean
            Get
                Return (Equals(_dir, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariacontrolsSpecified() As Boolean
            Get
                Return (Equals(_ariacontrols, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariadescribedbySpecified() As Boolean
            Get
                Return (Equals(_ariadescribedby, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaflowtoSpecified() As Boolean
            Get
                Return (Equals(_ariaflowto, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialabelSpecified() As Boolean
            Get
                Return (Equals(_arialabel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialabelledbySpecified() As Boolean
            Get
                Return (Equals(_arialabelledby, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialevelSpecified() As Boolean
            Get
                Return (Equals(_arialevel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaorientationSpecified() As Boolean
            Get
                Return (Equals(_ariaorientation, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaownsSpecified() As Boolean
            Get
                Return (Equals(_ariaowns, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property AnyAttrSpecified() As Boolean
            Get
                Return (Equals(_anyAttr, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceFullTypeDir

        ltr

        rtl

        [auto]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceFullTypeRole

        article

        columnheader

        definition

        directory

        document

        group

        heading

        img

        list

        listitem

        math

        note

        presentation

        region

        row

        rowgroup

        rowheader

        separator

        toolbar

        button

        checkbox

        gridcell

        link

        log

        [option]

        radio

        slider

        spinbutton

        status

        tab

        tabpanel

        textbox

        timer

        listbox

        radiogroup

        tablist

        complementary

        contentinfo
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceFullTypeArialive

        off

        polite

        assertive
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceFullTypeAriaorientation

        vertical

        horizontal
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="GraphicOrderInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("graphicOrderInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class GraphicOrderInteractionType
        Inherits BaseSequenceFullType

        Private _prompt As PromptType

        Private _object As ObjectType

        Private _hotspotChoice As List(Of HotspotChoiceType)

        Private _minChoices As String

        Private _maxChoices As String

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property prompt() As PromptType
            Get
                Return Me._prompt
            End Get
            Set
                Me._prompt = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)>
        Public Property [object]() As ObjectType
            Get
                Return Me._object
            End Get
            Set
                Me._object = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("hotspotChoice", Order:=2)>
        Public Property hotspotChoice() As List(Of HotspotChoiceType)
            Get
                Return Me._hotspotChoice
            End Get
            Set
                Me._hotspotChoice = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property minChoices() As String
            Get
                Return Me._minChoices
            End Get
            Set
                Me._minChoices = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property maxChoices() As String
            Get
                Return Me._maxChoices
            End Get
            Set
                Me._maxChoices = Value
            End Set
        End Property

        Public ReadOnly Property promptSpecified() As Boolean
            Get
                Return (Equals(_prompt, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property objectSpecified() As Boolean
            Get
                Return (Equals(_object, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property hotspotChoiceSpecified() As Boolean
            Get
                Return (Equals(_hotspotChoice, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minChoicesSpecified() As Boolean
            Get
                Return (Equals(_minChoices, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxChoicesSpecified() As Boolean
            Get
                Return (Equals(_maxChoices, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Prompt.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("prompt", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class PromptType
        Inherits BaseSequenceType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType26

        Private _text As List(Of String)

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType26()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="HTMLText.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("abbr", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class HTMLTextType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType37

        Private _text As List(Of String)

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customInteraction", GetType(CustomInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("endAttemptInteraction", GetType(EndAttemptInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gap", GetType(GapType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottext", GetType(HotTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inlineChoiceInteraction", GetType(InlineChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("textEntryInteraction", GetType(TextEntryInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType37()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="BDO.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("bdo", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class BDOType
        Inherits BaseSequenceType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType18

        Private _text As List(Of String)

        Private _title As String

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType18()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property title() As String
            Get
                Return Me._title
            End Get
            Set
                Me._title = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property titleSpecified() As Boolean
            Get
                Return (Equals(_title, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="BR.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("br", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class BRType
        Inherits BaseSequenceXBaseEmptyType
    End Class

    <System.Xml.Serialization.XmlIncludeAttribute(GetType(TextEntryInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(ImgType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(HRType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(HotspotChoiceType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(GapType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(EndAttemptInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(ColType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(BRType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(AssociableHotspotType)),
        System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="BaseSequenceXBaseEmpty.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Partial Public Class BaseSequenceXBaseEmptyType
        Inherits EmptyPrimitiveTypeType

        Private _id As String

        Private _class As List(Of String)

        Private _lang As String

        Private _label As String

        Private _base As String

        Private _dir As BaseSequenceXBaseEmptyTypeDir

        Private _role As BaseSequenceXBaseEmptyTypeRole

        Private roleFieldSpecified As Boolean

        Private _ariacontrols As String

        Private _ariadescribedby As String

        Private _ariaflowsto As String

        Private _arialabel As String

        Private _arialabelledby As String

        Private _arialevel As String

        Private _arialive As BaseSequenceXBaseEmptyTypeArialive

        Private arialiveFieldSpecified As Boolean

        Private _ariaorientation As BaseSequenceXBaseEmptyTypeAriaorientation

        Private _ariaowns As String

        Private _anyAttr As List(Of System.Xml.XmlAttribute)

        Public Sub New()
            MyBase.New
            Me._dir = BaseSequenceXBaseEmptyTypeDir.[auto]
            Me._ariaorientation = BaseSequenceXBaseEmptyTypeAriaorientation.horizontal
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property id() As String
            Get
                Return Me._id
            End Get
            Set
                Me._id = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property [class]() As List(Of String)
            Get
                Return Me._class
            End Get
            Set
                Me._class = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property lang() As String
            Get
                Return Me._lang
            End Get
            Set
                Me._lang = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property label() As String
            Get
                Return Me._label
            End Get
            Set
                Me._label = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property base() As String
            Get
                Return Me._base
            End Get
            Set
                Me._base = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(BaseSequenceXBaseEmptyTypeDir.[auto])>
        Public Property dir() As BaseSequenceXBaseEmptyTypeDir
            Get
                Return Me._dir
            End Get
            Set
                Me._dir = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property role() As BaseSequenceXBaseEmptyTypeRole
            Get
                Return Me._role
            End Get
            Set
                Me._role = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property roleSpecified() As Boolean
            Get
                Return Me.roleFieldSpecified
            End Get
            Set
                Me.roleFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-controls", DataType:="IDREFS")>
        Public Property ariacontrols() As String
            Get
                Return Me._ariacontrols
            End Get
            Set
                Me._ariacontrols = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-describedby", DataType:="IDREFS")>
        Public Property ariadescribedby() As String
            Get
                Return Me._ariadescribedby
            End Get
            Set
                Me._ariadescribedby = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-flowsto", DataType:="IDREFS")>
        Public Property ariaflowsto() As String
            Get
                Return Me._ariaflowsto
            End Get
            Set
                Me._ariaflowsto = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-label", DataType:="normalizedString")>
        Public Property arialabel() As String
            Get
                Return Me._arialabel
            End Get
            Set
                Me._arialabel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-labelledby", DataType:="IDREFS")>
        Public Property arialabelledby() As String
            Get
                Return Me._arialabelledby
            End Get
            Set
                Me._arialabelledby = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-level", DataType:="integer")>
        Public Property arialevel() As String
            Get
                Return Me._arialevel
            End Get
            Set
                Me._arialevel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-live")>
        Public Property arialive() As BaseSequenceXBaseEmptyTypeArialive
            Get
                Return Me._arialive
            End Get
            Set
                Me._arialive = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property arialiveSpecified() As Boolean
            Get
                Return Me.arialiveFieldSpecified
            End Get
            Set
                Me.arialiveFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-orientation"),
            System.ComponentModel.DefaultValueAttribute(BaseSequenceXBaseEmptyTypeAriaorientation.horizontal)>
        Public Property ariaorientation() As BaseSequenceXBaseEmptyTypeAriaorientation
            Get
                Return Me._ariaorientation
            End Get
            Set
                Me._ariaorientation = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-owns", DataType:="IDREFS")>
        Public Property ariaowns() As String
            Get
                Return Me._ariaowns
            End Get
            Set
                Me._ariaowns = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As List(Of System.Xml.XmlAttribute)
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property

        Public ReadOnly Property idSpecified() As Boolean
            Get
                Return (Equals(_id, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property classSpecified() As Boolean
            Get
                Return (Equals(_class, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property langSpecified() As Boolean
            Get
                Return (Equals(_lang, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property labelSpecified() As Boolean
            Get
                Return (Equals(_label, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property baseSpecified() As Boolean
            Get
                Return (Equals(_base, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property dirSpecified() As Boolean
            Get
                Return (Equals(_dir, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariacontrolsSpecified() As Boolean
            Get
                Return (Equals(_ariacontrols, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariadescribedbySpecified() As Boolean
            Get
                Return (Equals(_ariadescribedby, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaflowstoSpecified() As Boolean
            Get
                Return (Equals(_ariaflowsto, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialabelSpecified() As Boolean
            Get
                Return (Equals(_arialabel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialabelledbySpecified() As Boolean
            Get
                Return (Equals(_arialabelledby, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialevelSpecified() As Boolean
            Get
                Return (Equals(_arialevel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaorientationSpecified() As Boolean
            Get
                Return (Equals(_ariaorientation, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaownsSpecified() As Boolean
            Get
                Return (Equals(_ariaowns, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property AnyAttrSpecified() As Boolean
            Get
                Return (Equals(_anyAttr, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceXBaseEmptyTypeDir

        ltr

        rtl

        [auto]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceXBaseEmptyTypeRole

        article

        columnheader

        definition

        directory

        document

        group

        heading

        img

        list

        listitem

        math

        note

        presentation

        region

        row

        rowgroup

        rowheader

        separator

        toolbar

        button

        checkbox

        gridcell

        link

        log

        [option]

        radio

        slider

        spinbutton

        status

        tab

        tabpanel

        textbox

        timer

        listbox

        radiogroup

        tablist

        complementary

        contentinfo
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceXBaseEmptyTypeArialive

        off

        polite

        assertive
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceXBaseEmptyTypeAriaorientation

        vertical

        horizontal
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TextEntryInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("textEntryInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TextEntryInteractionType
        Inherits BaseSequenceXBaseEmptyType

        Private _responseIdentifier As String

        Private _base1 As Integer

        Private _stringIdentifier As String

        Private _expectedLength As String

        Private _patternMask As String

        Private _placeholderText As String

        Private _format As String

        Public Sub New()
            MyBase.New
            Me._base1 = 10
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="IDREF")>
        Public Property responseIdentifier() As String
            Get
                Return Me._responseIdentifier
            End Get
            Set
                Me._responseIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("base"),
            System.ComponentModel.DefaultValueAttribute(10)>
        Public Property base1() As Integer
            Get
                Return Me._base1
            End Get
            Set
                Me._base1 = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="IDREF")>
        Public Property stringIdentifier() As String
            Get
                Return Me._stringIdentifier
            End Get
            Set
                Me._stringIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property expectedLength() As String
            Get
                Return Me._expectedLength
            End Get
            Set
                Me._expectedLength = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property patternMask() As String
            Get
                Return Me._patternMask
            End Get
            Set
                Me._patternMask = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property placeholderText() As String
            Get
                Return Me._placeholderText
            End Get
            Set
                Me._placeholderText = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property format() As String
            Get
                Return Me._format
            End Get
            Set
                Me._format = Value
            End Set
        End Property

        Public ReadOnly Property responseIdentifierSpecified() As Boolean
            Get
                Return (Equals(_responseIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property base1Specified() As Boolean
            Get
                Return (Equals(_base1, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stringIdentifierSpecified() As Boolean
            Get
                Return (Equals(_stringIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property expectedLengthSpecified() As Boolean
            Get
                Return (Equals(_expectedLength, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property patternMaskSpecified() As Boolean
            Get
                Return (Equals(_patternMask, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property placeholderTextSpecified() As Boolean
            Get
                Return (Equals(_placeholderText, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property formatSpecified() As Boolean
            Get
                Return (Equals(_format, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Img.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("img", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ImgType
        Inherits BaseSequenceXBaseEmptyType

        Private _src As String

        Private _alt As String

        Private _longdesc As String

        Private _height As String

        Private _width As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property src() As String
            Get
                Return Me._src
            End Get
            Set
                Me._src = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property alt() As String
            Get
                Return Me._alt
            End Get
            Set
                Me._alt = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property longdesc() As String
            Get
                Return Me._longdesc
            End Get
            Set
                Me._longdesc = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property height() As String
            Get
                Return Me._height
            End Get
            Set
                Me._height = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property width() As String
            Get
                Return Me._width
            End Get
            Set
                Me._width = Value
            End Set
        End Property

        Public ReadOnly Property srcSpecified() As Boolean
            Get
                Return (Equals(_src, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property altSpecified() As Boolean
            Get
                Return (Equals(_alt, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property longdescSpecified() As Boolean
            Get
                Return (Equals(_longdesc, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property heightSpecified() As Boolean
            Get
                Return (Equals(_height, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property widthSpecified() As Boolean
            Get
                Return (Equals(_width, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="HR.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("hr", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class HRType
        Inherits BaseSequenceXBaseEmptyType
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="HotspotChoice.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("hotspotChoice", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class HotspotChoiceType
        Inherits BaseSequenceXBaseEmptyType

        Private _identifier As String

        Private _templateIdentifier As String

        Private _showHide As HotspotChoiceTypeShowHide

        Private _shape As HotspotChoiceTypeShape

        Private _coords As String

        Private _hotspotLabel As String

        Public Sub New()
            MyBase.New
            Me._showHide = HotspotChoiceTypeShowHide.show
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(HotspotChoiceTypeShowHide.show)>
        Public Property showHide() As HotspotChoiceTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property shape() As HotspotChoiceTypeShape
            Get
                Return Me._shape
            End Get
            Set
                Me._shape = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property coords() As String
            Get
                Return Me._coords
            End Get
            Set
                Me._coords = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property hotspotLabel() As String
            Get
                Return Me._hotspotLabel
            End Get
            Set
                Me._hotspotLabel = Value
            End Set
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property shapeSpecified() As Boolean
            Get
                Return (Equals(_shape, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property coordsSpecified() As Boolean
            Get
                Return (Equals(_coords, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property hotspotLabelSpecified() As Boolean
            Get
                Return (Equals(_hotspotLabel, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum HotspotChoiceTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum HotspotChoiceTypeShape

        circle

        [default]

        ellipse

        poly

        rect
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Gap.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("gap", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class GapType
        Inherits BaseSequenceXBaseEmptyType

        Private _identifier As String

        Private _templateIdentifier As String

        Private _showHide As GapTypeShowHide

        Private _matchGroup As List(Of String)

        Private _required As Boolean

        Public Sub New()
            MyBase.New
            Me._showHide = GapTypeShowHide.show
            Me._required = False
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(GapTypeShowHide.show)>
        Public Property showHide() As GapTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property matchGroup() As List(Of String)
            Get
                Return Me._matchGroup
            End Get
            Set
                Me._matchGroup = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property required() As Boolean
            Get
                Return Me._required
            End Get
            Set
                Me._required = Value
            End Set
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property matchGroupSpecified() As Boolean
            Get
                Return (Equals(_matchGroup, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property requiredSpecified() As Boolean
            Get
                Return (Equals(_required, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum GapTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="EndAttemptInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("endAttemptInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class EndAttemptInteractionType
        Inherits BaseSequenceXBaseEmptyType

        Private _responseIdentifier As String

        Private _title As String

        Private _countAttempt As Boolean

        Private countAttemptFieldSpecified As Boolean

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property responseIdentifier() As String
            Get
                Return Me._responseIdentifier
            End Get
            Set
                Me._responseIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property title() As String
            Get
                Return Me._title
            End Get
            Set
                Me._title = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property countAttempt() As Boolean
            Get
                Return Me._countAttempt
            End Get
            Set
                Me._countAttempt = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property countAttemptSpecified() As Boolean
            Get
                Return Me.countAttemptFieldSpecified
            End Get
            Set
                Me.countAttemptFieldSpecified = Value
            End Set
        End Property

        Public ReadOnly Property responseIdentifierSpecified() As Boolean
            Get
                Return (Equals(_responseIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property titleSpecified() As Boolean
            Get
                Return (Equals(_title, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Col.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("col", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ColType
        Inherits BaseSequenceXBaseEmptyType

        Private _span As Integer

        Private spanFieldSpecified As Boolean

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property span() As Integer
            Get
                Return Me._span
            End Get
            Set
                Me._span = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property spanSpecified() As Boolean
            Get
                Return Me.spanFieldSpecified
            End Get
            Set
                Me.spanFieldSpecified = Value
            End Set
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="AssociableHotspot.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("associableHotspot", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class AssociableHotspotType
        Inherits BaseSequenceXBaseEmptyType

        Private _identifier As String

        Private _templateIdentifier As String

        Private _showHide As AssociableHotspotTypeShowHide

        Private _matchGroup As List(Of String)

        Private _shape As AssociableHotspotTypeShape

        Private _coords As String

        Private _hotspotLabel As String

        Private _matchMax As String

        Private _matchMin As String

        Public Sub New()
            MyBase.New
            Me._showHide = AssociableHotspotTypeShowHide.show
            Me._matchMin = "0"
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(AssociableHotspotTypeShowHide.show)>
        Public Property showHide() As AssociableHotspotTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property matchGroup() As List(Of String)
            Get
                Return Me._matchGroup
            End Get
            Set
                Me._matchGroup = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property shape() As AssociableHotspotTypeShape
            Get
                Return Me._shape
            End Get
            Set
                Me._shape = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property coords() As String
            Get
                Return Me._coords
            End Get
            Set
                Me._coords = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property hotspotLabel() As String
            Get
                Return Me._hotspotLabel
            End Get
            Set
                Me._hotspotLabel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property matchMax() As String
            Get
                Return Me._matchMax
            End Get
            Set
                Me._matchMax = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property matchMin() As String
            Get
                Return Me._matchMin
            End Get
            Set
                Me._matchMin = Value
            End Set
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property matchGroupSpecified() As Boolean
            Get
                Return (Equals(_matchGroup, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property shapeSpecified() As Boolean
            Get
                Return (Equals(_shape, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property coordsSpecified() As Boolean
            Get
                Return (Equals(_coords, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property hotspotLabelSpecified() As Boolean
            Get
                Return (Equals(_hotspotLabel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property matchMaxSpecified() As Boolean
            Get
                Return (Equals(_matchMax, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property matchMinSpecified() As Boolean
            Get
                Return (Equals(_matchMin, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum AssociableHotspotTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum AssociableHotspotTypeShape

        circle

        [default]

        ellipse

        poly

        rect
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Q.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("q", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class QType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType17

        Private _text As List(Of String)

        Private _cite As String

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customInteraction", GetType(CustomInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("endAttemptInteraction", GetType(EndAttemptInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gap", GetType(GapType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottext", GetType(HotTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inlineChoiceInteraction", GetType(InlineChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("textEntryInteraction", GetType(TextEntryInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType17()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property cite() As String
            Get
                Return Me._cite
            End Get
            Set
                Me._cite = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property citeSpecified() As Boolean
            Get
                Return (Equals(_cite, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="FeedbackInline.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("feedbackInline", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class FeedbackInlineType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType38

        Private _text As List(Of String)

        Private _outcomeIdentifier As String

        Private _identifier As String

        Private _showHide As FeedbackInlineTypeShowHide

        Public Sub New()
            MyBase.New
            Me._showHide = FeedbackInlineTypeShowHide.show
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType38()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property outcomeIdentifier() As String
            Get
                Return Me._outcomeIdentifier
            End Get
            Set
                Me._outcomeIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(FeedbackInlineTypeShowHide.show)>
        Public Property showHide() As FeedbackInlineTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property outcomeIdentifierSpecified() As Boolean
            Get
                Return (Equals(_outcomeIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="PrintedVariable.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("printedVariable", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class PrintedVariableType
        Inherits EmptyPrimitiveTypeType

        Private _id As String

        Private _class As List(Of String)

        Private _lang As String

        Private _label As String

        Private _base As String

        Private _identifier As String

        Private _format As String

        Private _base1 As String

        Private _index As String

        Private _powerForm As Boolean

        Private _field As String

        Private _delimiter As String

        Private _mappingIndicator As String

        Public Sub New()
            MyBase.New
            Me._base1 = "10"
            Me._powerForm = False
            Me._delimiter = ";"
            Me._mappingIndicator = "="
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property id() As String
            Get
                Return Me._id
            End Get
            Set
                Me._id = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property [class]() As List(Of String)
            Get
                Return Me._class
            End Get
            Set
                Me._class = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property lang() As String
            Get
                Return Me._lang
            End Get
            Set
                Me._lang = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property label() As String
            Get
                Return Me._label
            End Get
            Set
                Me._label = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property base() As String
            Get
                Return Me._base
            End Get
            Set
                Me._base = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property format() As String
            Get
                Return Me._format
            End Get
            Set
                Me._format = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("base"),
            System.ComponentModel.DefaultValueAttribute("10")>
        Public Property base1() As String
            Get
                Return Me._base1
            End Get
            Set
                Me._base1 = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property index() As String
            Get
                Return Me._index
            End Get
            Set
                Me._index = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property powerForm() As Boolean
            Get
                Return Me._powerForm
            End Get
            Set
                Me._powerForm = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property field() As String
            Get
                Return Me._field
            End Get
            Set
                Me._field = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString"),
            System.ComponentModel.DefaultValueAttribute(";")>
        Public Property delimiter() As String
            Get
                Return Me._delimiter
            End Get
            Set
                Me._delimiter = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString"),
            System.ComponentModel.DefaultValueAttribute("=")>
        Public Property mappingIndicator() As String
            Get
                Return Me._mappingIndicator
            End Get
            Set
                Me._mappingIndicator = Value
            End Set
        End Property

        Public ReadOnly Property idSpecified() As Boolean
            Get
                Return (Equals(_id, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property classSpecified() As Boolean
            Get
                Return (Equals(_class, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property langSpecified() As Boolean
            Get
                Return (Equals(_lang, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property labelSpecified() As Boolean
            Get
                Return (Equals(_label, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property baseSpecified() As Boolean
            Get
                Return (Equals(_base, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property formatSpecified() As Boolean
            Get
                Return (Equals(_format, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property base1Specified() As Boolean
            Get
                Return (Equals(_base1, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property indexSpecified() As Boolean
            Get
                Return (Equals(_index, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property powerFormSpecified() As Boolean
            Get
                Return (Equals(_powerForm, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property fieldSpecified() As Boolean
            Get
                Return (Equals(_field, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property delimiterSpecified() As Boolean
            Get
                Return (Equals(_delimiter, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property mappingIndicatorSpecified() As Boolean
            Get
                Return (Equals(_mappingIndicator, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TemplateInline.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("templateInline", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TemplateInlineType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType36

        Private _text As List(Of String)

        Private _templateIdentifier As String

        Private _showHide As TemplateInlineTypeShowHide

        Private _identifier As String

        Public Sub New()
            MyBase.New
            Me._showHide = TemplateInlineTypeShowHide.show
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gap", GetType(GapType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottext", GetType(HotTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType36()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(TemplateInlineTypeShowHide.show)>
        Public Property showHide() As TemplateInlineTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="HotText.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("hottext", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class HotTextType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType39

        Private _text As List(Of String)

        Private _identifier As String

        Private _templateIdentifier As String

        Private _showHide As HotTextTypeShowHide

        Public Sub New()
            MyBase.New
            Me._showHide = HotTextTypeShowHide.show
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType39()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(HotTextTypeShowHide.show)>
        Public Property showHide() As HotTextTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType39

        a

        abbr

        acronym

        b

        bdo

        big

        br

        cite

        code

        dfn

        em

        feedbackInline

        i

        img

        kbd

        [object]

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        templateInline

        tt

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum HotTextTypeShowHide

        show

        hide
    End Enum

    <System.Xml.Serialization.XmlIncludeAttribute(GetType(RubricBlockTemplateInlineType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(RubricBlockTemplateBlockType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(RubricBlockType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(DTType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(TemplateBlockFeedbackBlockType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(DivType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(TableType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(BlockQuoteType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(OULType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(TemplateBlockType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(LabelType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(QType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(FeedbackBlockType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(DDType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(DLType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(ObjectType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(TemplateInlineType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(HTMLTextType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(FeedbackInlineType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(HotTextType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(AType)),
        System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="BaseSequenceXBase.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Partial Public Class BaseSequenceXBaseType

        Private _id As String

        Private _class As List(Of String)

        Private _lang As String

        Private _label As String

        Private _base As String

        Private _dir As BaseSequenceXBaseTypeDir

        Private _role As BaseSequenceXBaseTypeRole

        Private roleFieldSpecified As Boolean

        Private _ariacontrols As String

        Private _ariadescribedby As String

        Private _ariaflowto As String

        Private _arialabel As String

        Private _arialabelledby As String

        Private _arialevel As String

        Private _arialive As BaseSequenceXBaseTypeArialive

        Private arialiveFieldSpecified As Boolean

        Private _ariaorientation As BaseSequenceXBaseTypeAriaorientation

        Private _ariaowns As String

        Private _anyAttr As List(Of System.Xml.XmlAttribute)

        Public Sub New()
            MyBase.New
            Me._dir = BaseSequenceXBaseTypeDir.[auto]
            Me._ariaorientation = BaseSequenceXBaseTypeAriaorientation.horizontal
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property id() As String
            Get
                Return Me._id
            End Get
            Set
                Me._id = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property [class]() As List(Of String)
            Get
                Return Me._class
            End Get
            Set
                Me._class = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property lang() As String
            Get
                Return Me._lang
            End Get
            Set
                Me._lang = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property label() As String
            Get
                Return Me._label
            End Get
            Set
                Me._label = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property base() As String
            Get
                Return Me._base
            End Get
            Set
                Me._base = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(BaseSequenceXBaseTypeDir.[auto])>
        Public Property dir() As BaseSequenceXBaseTypeDir
            Get
                Return Me._dir
            End Get
            Set
                Me._dir = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property role() As BaseSequenceXBaseTypeRole
            Get
                Return Me._role
            End Get
            Set
                Me._role = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property roleSpecified() As Boolean
            Get
                Return Me.roleFieldSpecified
            End Get
            Set
                Me.roleFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-controls", DataType:="IDREFS")>
        Public Property ariacontrols() As String
            Get
                Return Me._ariacontrols
            End Get
            Set
                Me._ariacontrols = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-describedby", DataType:="IDREFS")>
        Public Property ariadescribedby() As String
            Get
                Return Me._ariadescribedby
            End Get
            Set
                Me._ariadescribedby = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-flowto", DataType:="IDREFS")>
        Public Property ariaflowto() As String
            Get
                Return Me._ariaflowto
            End Get
            Set
                Me._ariaflowto = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-label", DataType:="normalizedString")>
        Public Property arialabel() As String
            Get
                Return Me._arialabel
            End Get
            Set
                Me._arialabel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-labelledby", DataType:="IDREFS")>
        Public Property arialabelledby() As String
            Get
                Return Me._arialabelledby
            End Get
            Set
                Me._arialabelledby = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-level", DataType:="integer")>
        Public Property arialevel() As String
            Get
                Return Me._arialevel
            End Get
            Set
                Me._arialevel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-live")>
        Public Property arialive() As BaseSequenceXBaseTypeArialive
            Get
                Return Me._arialive
            End Get
            Set
                Me._arialive = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property arialiveSpecified() As Boolean
            Get
                Return Me.arialiveFieldSpecified
            End Get
            Set
                Me.arialiveFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-orientation"),
            System.ComponentModel.DefaultValueAttribute(BaseSequenceXBaseTypeAriaorientation.horizontal)>
        Public Property ariaorientation() As BaseSequenceXBaseTypeAriaorientation
            Get
                Return Me._ariaorientation
            End Get
            Set
                Me._ariaorientation = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-owns", DataType:="IDREFS")>
        Public Property ariaowns() As String
            Get
                Return Me._ariaowns
            End Get
            Set
                Me._ariaowns = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As List(Of System.Xml.XmlAttribute)
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property

        Public ReadOnly Property idSpecified() As Boolean
            Get
                Return (Equals(_id, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property classSpecified() As Boolean
            Get
                Return (Equals(_class, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property langSpecified() As Boolean
            Get
                Return (Equals(_lang, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property labelSpecified() As Boolean
            Get
                Return (Equals(_label, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property baseSpecified() As Boolean
            Get
                Return (Equals(_base, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property dirSpecified() As Boolean
            Get
                Return (Equals(_dir, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariacontrolsSpecified() As Boolean
            Get
                Return (Equals(_ariacontrols, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariadescribedbySpecified() As Boolean
            Get
                Return (Equals(_ariadescribedby, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaflowtoSpecified() As Boolean
            Get
                Return (Equals(_ariaflowto, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialabelSpecified() As Boolean
            Get
                Return (Equals(_arialabel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialabelledbySpecified() As Boolean
            Get
                Return (Equals(_arialabelledby, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialevelSpecified() As Boolean
            Get
                Return (Equals(_arialevel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaorientationSpecified() As Boolean
            Get
                Return (Equals(_ariaorientation, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaownsSpecified() As Boolean
            Get
                Return (Equals(_ariaowns, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property AnyAttrSpecified() As Boolean
            Get
                Return (Equals(_anyAttr, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceXBaseTypeDir

        ltr

        rtl

        [auto]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceXBaseTypeRole

        article

        columnheader

        definition

        directory

        document

        group

        heading

        img

        list

        listitem

        math

        note

        presentation

        region

        row

        rowgroup

        rowheader

        separator

        toolbar

        button

        checkbox

        gridcell

        link

        log

        [option]

        radio

        slider

        spinbutton

        status

        tab

        tabpanel

        textbox

        timer

        listbox

        radiogroup

        tablist

        complementary

        contentinfo
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceXBaseTypeArialive

        off

        polite

        assertive
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceXBaseTypeAriaorientation

        vertical

        horizontal
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="RubricBlockTemplateInline.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Partial Public Class RubricBlockTemplateInlineType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType41

        Private _text As List(Of String)

        Private _templateIdentifier As String

        Private _showHide As RubricBlockTemplateInlineTypeShowHide

        Private _identifier As String

        Public Sub New()
            MyBase.New
            Me._showHide = RubricBlockTemplateInlineTypeShowHide.show
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(RubricBlockTemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType41()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(RubricBlockTemplateInlineTypeShowHide.show)>
        Public Property showHide() As RubricBlockTemplateInlineTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType41

        a

        abbr

        acronym

        b

        bdo

        big

        br

        cite

        code

        dfn

        em

        i

        img

        kbd

        [object]

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        templateInline

        tt

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum RubricBlockTemplateInlineTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="RubricBlockTemplateBlock.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Partial Public Class RubricBlockTemplateBlockType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType40

        Private _stylesheet As List(Of StyleSheetType)

        Private _text As List(Of String)

        Private _templateIdentifier As String

        Private _showHide As RubricBlockTemplateBlockTypeShowHide

        Private _identifier As String

        Public Sub New()
            MyBase.New
            Me._showHide = RubricBlockTemplateBlockTypeShowHide.show
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(RubricBlockTemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType40()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("stylesheet", Order:=2)>
        Public Property stylesheet() As List(Of StyleSheetType)
            Get
                Return Me._stylesheet
            End Get
            Set
                Me._stylesheet = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(RubricBlockTemplateBlockTypeShowHide.show)>
        Public Property showHide() As RubricBlockTemplateBlockTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stylesheetSpecified() As Boolean
            Get
                Return (Equals(_stylesheet, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="BlockQuote.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("blockquote", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class BlockQuoteType
        Inherits BaseSequenceXBaseType

        Private _items As List(Of Object)

        Private _cite As String

        <System.Xml.Serialization.XmlElementAttribute("associateInteraction", GetType(AssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("choiceInteraction", GetType(ChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customInteraction", GetType(CustomInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("drawingInteraction", GetType(DrawingInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("extendedTextInteraction", GetType(ExtendedTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(FeedbackBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gapMatchInteraction", GetType(GapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicAssociateInteraction", GetType(GraphicAssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicGapMatchInteraction", GetType(GraphicGapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicOrderInteraction", GetType(GraphicOrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hotspotInteraction", GetType(HotspotInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottextInteraction", GetType(HotTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("infoControl", GetType(InfoControlType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("matchInteraction", GetType(MatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mediaInteraction", GetType(MediaInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("orderInteraction", GetType(OrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("positionObjectStage", GetType(PositionObjectStageType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("selectPointInteraction", GetType(SelectPointInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sliderInteraction", GetType(SliderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("uploadInteraction", GetType(UploadInteractionType), Order:=0)>
        Public Property Items() As List(Of Object)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property cite() As String
            Get
                Return Me._cite
            End Get
            Set
                Me._cite = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property citeSpecified() As Boolean
            Get
                Return (Equals(_cite, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="AssociateInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("associateInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class AssociateInteractionType
        Inherits BasePromptInteractionType

        Private _simpleAssociableChoice As List(Of SimpleAssociableChoiceType)

        Private _shuffle As Boolean

        Private _maxAssociations As String

        Private _minAssociations As String

        Public Sub New()
            MyBase.New
            Me._shuffle = False
            Me._maxAssociations = "1"
            Me._minAssociations = "0"
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("simpleAssociableChoice", Order:=0)>
        Public Property simpleAssociableChoice() As List(Of SimpleAssociableChoiceType)
            Get
                Return Me._simpleAssociableChoice
            End Get
            Set
                Me._simpleAssociableChoice = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property shuffle() As Boolean
            Get
                Return Me._shuffle
            End Get
            Set
                Me._shuffle = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("1")>
        Public Property maxAssociations() As String
            Get
                Return Me._maxAssociations
            End Get
            Set
                Me._maxAssociations = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property minAssociations() As String
            Get
                Return Me._minAssociations
            End Get
            Set
                Me._minAssociations = Value
            End Set
        End Property

        Public ReadOnly Property simpleAssociableChoiceSpecified() As Boolean
            Get
                Return (Equals(_simpleAssociableChoice, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property shuffleSpecified() As Boolean
            Get
                Return (Equals(_shuffle, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxAssociationsSpecified() As Boolean
            Get
                Return (Equals(_maxAssociations, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minAssociationsSpecified() As Boolean
            Get
                Return (Equals(_minAssociations, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="SimpleAssociableChoice.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("simpleAssociableChoice", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class SimpleAssociableChoiceType
        Inherits BaseSequenceType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType23

        Private _text As List(Of String)

        Private _identifier As String

        Private _fixed As Boolean

        Private fixedFieldSpecified As Boolean

        Private _templateIdentifier As String

        Private _showHide As SimpleAssociableChoiceTypeShowHide

        Private showHideFieldSpecified As Boolean

        Private _matchGroup As List(Of String)

        Private _matchMax As String

        Private _matchMin As String

        Public Sub New()
            MyBase.New
            Me._matchMin = "0"
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(FeedbackBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType23()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property fixed() As Boolean
            Get
                Return Me._fixed
            End Get
            Set
                Me._fixed = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property fixedSpecified() As Boolean
            Get
                Return Me.fixedFieldSpecified
            End Get
            Set
                Me.fixedFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property showHide() As SimpleAssociableChoiceTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property showHideSpecified() As Boolean
            Get
                Return Me.showHideFieldSpecified
            End Get
            Set
                Me.showHideFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property matchGroup() As List(Of String)
            Get
                Return Me._matchGroup
            End Get
            Set
                Me._matchGroup = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property matchMax() As String
            Get
                Return Me._matchMax
            End Get
            Set
                Me._matchMax = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property matchMin() As String
            Get
                Return Me._matchMin
            End Get
            Set
                Me._matchMin = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property matchGroupSpecified() As Boolean
            Get
                Return (Equals(_matchGroup, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property matchMaxSpecified() As Boolean
            Get
                Return (Equals(_matchMax, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property matchMinSpecified() As Boolean
            Get
                Return (Equals(_matchMin, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Div.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("div", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class DivType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType22

        Private _text As List(Of String)

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("associateInteraction", GetType(AssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("choiceInteraction", GetType(ChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customInteraction", GetType(CustomInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("drawingInteraction", GetType(DrawingInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("endAttemptInteraction", GetType(EndAttemptInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("extendedTextInteraction", GetType(ExtendedTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(FeedbackBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gapMatchInteraction", GetType(GapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicAssociateInteraction", GetType(GraphicAssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicGapMatchInteraction", GetType(GraphicGapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicOrderInteraction", GetType(GraphicOrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hotspotInteraction", GetType(HotspotInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottext", GetType(HotTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottextInteraction", GetType(HotTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inlineChoiceInteraction", GetType(InlineChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("matchInteraction", GetType(MatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mediaInteraction", GetType(MediaInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("orderInteraction", GetType(OrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("positionObjectStage", GetType(PositionObjectStageType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("selectPointInteraction", GetType(SelectPointInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sliderInteraction", GetType(SliderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("textEntryInteraction", GetType(TextEntryInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("uploadInteraction", GetType(UploadInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType22()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="ChoiceInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("choiceInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ChoiceInteractionType
        Inherits BasePromptInteractionType

        Private _simpleChoice As List(Of SimpleChoiceType)

        Private _shuffle As Boolean

        Private _maxChoices As String

        Private _minChoices As String

        Private _orientation As ChoiceInteractionTypeOrientation

        Public Sub New()
            MyBase.New
            Me._shuffle = False
            Me._maxChoices = "1"
            Me._minChoices = "0"
            Me._orientation = ChoiceInteractionTypeOrientation.vertical
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("simpleChoice", Order:=0)>
        Public Property simpleChoice() As List(Of SimpleChoiceType)
            Get
                Return Me._simpleChoice
            End Get
            Set
                Me._simpleChoice = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property shuffle() As Boolean
            Get
                Return Me._shuffle
            End Get
            Set
                Me._shuffle = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("1")>
        Public Property maxChoices() As String
            Get
                Return Me._maxChoices
            End Get
            Set
                Me._maxChoices = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property minChoices() As String
            Get
                Return Me._minChoices
            End Get
            Set
                Me._minChoices = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(ChoiceInteractionTypeOrientation.vertical)>
        Public Property orientation() As ChoiceInteractionTypeOrientation
            Get
                Return Me._orientation
            End Get
            Set
                Me._orientation = Value
            End Set
        End Property

        Public ReadOnly Property simpleChoiceSpecified() As Boolean
            Get
                Return (Equals(_simpleChoice, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property shuffleSpecified() As Boolean
            Get
                Return (Equals(_shuffle, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxChoicesSpecified() As Boolean
            Get
                Return (Equals(_maxChoices, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minChoicesSpecified() As Boolean
            Get
                Return (Equals(_minChoices, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property orientationSpecified() As Boolean
            Get
                Return (Equals(_orientation, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="SimpleChoice.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("simpleChoice", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class SimpleChoiceType
        Inherits BaseSequenceType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType20

        Private _text As List(Of String)

        Private _identifier As String

        Private _fixed As Boolean

        Private _templateIdentifier As String

        Private _showHide As SimpleChoiceTypeShowHide

        Public Sub New()
            MyBase.New
            Me._fixed = False
            Me._showHide = SimpleChoiceTypeShowHide.show
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(FeedbackBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType20()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property fixed() As Boolean
            Get
                Return Me._fixed
            End Get
            Set
                Me._fixed = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(SimpleChoiceTypeShowHide.show)>
        Public Property showHide() As SimpleChoiceTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property fixedSpecified() As Boolean
            Get
                Return (Equals(_fixed, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="DL.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("dl", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class DLType
        Inherits BaseSequenceXBaseType

        Private _items As List(Of BaseSequenceXBaseType)

        <System.Xml.Serialization.XmlElementAttribute("dd", GetType(DDType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dt", GetType(DTType), Order:=0)>
        Public Property Items() As List(Of BaseSequenceXBaseType)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="DD.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("dd", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class DDType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType33

        Private _text As List(Of String)

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("associateInteraction", GetType(AssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("choiceInteraction", GetType(ChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customInteraction", GetType(CustomInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("drawingInteraction", GetType(DrawingInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("endAttemptInteraction", GetType(EndAttemptInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("extendedTextInteraction", GetType(ExtendedTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(FeedbackBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gapMatchInteraction", GetType(GapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicAssociateInteraction", GetType(GraphicAssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicGapMatchInteraction", GetType(GraphicGapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicOrderInteraction", GetType(GraphicOrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hotspotInteraction", GetType(HotspotInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottext", GetType(HotTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottextInteraction", GetType(HotTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inlineChoiceInteraction", GetType(InlineChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("matchInteraction", GetType(MatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mediaInteraction", GetType(MediaInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("orderInteraction", GetType(OrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("selectPointInteraction", GetType(SelectPointInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sliderInteraction", GetType(SliderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("textEntryInteraction", GetType(TextEntryInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("uploadInteraction", GetType(UploadInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType33()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="DrawingInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("drawingInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class DrawingInteractionType
        Inherits BasePromptInteractionType

        Private _object As ObjectType

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property [object]() As ObjectType
            Get
                Return Me._object
            End Get
            Set
                Me._object = Value
            End Set
        End Property

        Public ReadOnly Property objectSpecified() As Boolean
            Get
                Return (Equals(_object, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.Xml.Serialization.XmlIncludeAttribute(GetType(AssociateInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(UploadInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(ExtendedTextInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(OrderInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(HotTextInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(MediaInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(ChoiceInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(SliderInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(GraphicAssociateInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(SelectPointInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(HotspotInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(MatchInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(GapMatchInteractionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(DrawingInteractionType)),
        System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="BasePromptInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Partial Public Class BasePromptInteractionType

        Private _prompt As PromptType

        Private _id As String

        Private _class As List(Of String)

        Private _lang As String

        Private _label As String

        Private _base As String

        Private _responseIdentifier As String

        Private _dir As BasePromptInteractionTypeDir

        Private _role As BasePromptInteractionTypeRole

        Private roleFieldSpecified As Boolean

        Private _ariacontrols As String

        Private _ariadescribedby As String

        Private _ariaflowsto As String

        Private _arialabel As String

        Private _arialabelledby As String

        Private _arialevel As String

        Private _arialive As BasePromptInteractionTypeArialive

        Private arialiveFieldSpecified As Boolean

        Private _ariaorientation As BasePromptInteractionTypeAriaorientation

        Private _ariaowns As String

        Private _anyAttr As List(Of System.Xml.XmlAttribute)

        Public Sub New()
            MyBase.New
            Me._dir = BasePromptInteractionTypeDir.[auto]
            Me._ariaorientation = BasePromptInteractionTypeAriaorientation.horizontal
        End Sub

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property prompt() As PromptType
            Get
                Return Me._prompt
            End Get
            Set
                Me._prompt = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property id() As String
            Get
                Return Me._id
            End Get
            Set
                Me._id = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property [class]() As List(Of String)
            Get
                Return Me._class
            End Get
            Set
                Me._class = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property lang() As String
            Get
                Return Me._lang
            End Get
            Set
                Me._lang = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property label() As String
            Get
                Return Me._label
            End Get
            Set
                Me._label = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property base() As String
            Get
                Return Me._base
            End Get
            Set
                Me._base = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property responseIdentifier() As String
            Get
                Return Me._responseIdentifier
            End Get
            Set
                Me._responseIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(BasePromptInteractionTypeDir.[auto])>
        Public Property dir() As BasePromptInteractionTypeDir
            Get
                Return Me._dir
            End Get
            Set
                Me._dir = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property role() As BasePromptInteractionTypeRole
            Get
                Return Me._role
            End Get
            Set
                Me._role = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property roleSpecified() As Boolean
            Get
                Return Me.roleFieldSpecified
            End Get
            Set
                Me.roleFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-controls", DataType:="IDREFS")>
        Public Property ariacontrols() As String
            Get
                Return Me._ariacontrols
            End Get
            Set
                Me._ariacontrols = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-describedby", DataType:="IDREFS")>
        Public Property ariadescribedby() As String
            Get
                Return Me._ariadescribedby
            End Get
            Set
                Me._ariadescribedby = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-flowsto", DataType:="IDREFS")>
        Public Property ariaflowsto() As String
            Get
                Return Me._ariaflowsto
            End Get
            Set
                Me._ariaflowsto = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-label", DataType:="normalizedString")>
        Public Property arialabel() As String
            Get
                Return Me._arialabel
            End Get
            Set
                Me._arialabel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-labelledby", DataType:="IDREFS")>
        Public Property arialabelledby() As String
            Get
                Return Me._arialabelledby
            End Get
            Set
                Me._arialabelledby = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-level", DataType:="integer")>
        Public Property arialevel() As String
            Get
                Return Me._arialevel
            End Get
            Set
                Me._arialevel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-live")>
        Public Property arialive() As BasePromptInteractionTypeArialive
            Get
                Return Me._arialive
            End Get
            Set
                Me._arialive = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property arialiveSpecified() As Boolean
            Get
                Return Me.arialiveFieldSpecified
            End Get
            Set
                Me.arialiveFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-orientation"),
            System.ComponentModel.DefaultValueAttribute(BasePromptInteractionTypeAriaorientation.horizontal)>
        Public Property ariaorientation() As BasePromptInteractionTypeAriaorientation
            Get
                Return Me._ariaorientation
            End Get
            Set
                Me._ariaorientation = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-owns", DataType:="IDREFS")>
        Public Property ariaowns() As String
            Get
                Return Me._ariaowns
            End Get
            Set
                Me._ariaowns = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As List(Of System.Xml.XmlAttribute)
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property

        Public ReadOnly Property promptSpecified() As Boolean
            Get
                Return (Equals(_prompt, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property idSpecified() As Boolean
            Get
                Return (Equals(_id, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property classSpecified() As Boolean
            Get
                Return (Equals(_class, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property langSpecified() As Boolean
            Get
                Return (Equals(_lang, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property labelSpecified() As Boolean
            Get
                Return (Equals(_label, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property baseSpecified() As Boolean
            Get
                Return (Equals(_base, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property responseIdentifierSpecified() As Boolean
            Get
                Return (Equals(_responseIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property dirSpecified() As Boolean
            Get
                Return (Equals(_dir, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariacontrolsSpecified() As Boolean
            Get
                Return (Equals(_ariacontrols, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariadescribedbySpecified() As Boolean
            Get
                Return (Equals(_ariadescribedby, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaflowstoSpecified() As Boolean
            Get
                Return (Equals(_ariaflowsto, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialabelSpecified() As Boolean
            Get
                Return (Equals(_arialabel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialabelledbySpecified() As Boolean
            Get
                Return (Equals(_arialabelledby, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialevelSpecified() As Boolean
            Get
                Return (Equals(_arialevel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaorientationSpecified() As Boolean
            Get
                Return (Equals(_ariaorientation, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaownsSpecified() As Boolean
            Get
                Return (Equals(_ariaowns, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property AnyAttrSpecified() As Boolean
            Get
                Return (Equals(_anyAttr, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BasePromptInteractionTypeDir

        ltr

        rtl

        [auto]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BasePromptInteractionTypeRole

        article

        columnheader

        definition

        directory

        document

        group

        heading

        img

        list

        listitem

        math

        note

        presentation

        region

        row

        rowgroup

        rowheader

        separator

        toolbar

        button

        checkbox

        gridcell

        link

        log

        [option]

        radio

        slider

        spinbutton

        status

        tab

        tabpanel

        textbox

        timer

        listbox

        radiogroup

        tablist

        complementary

        contentinfo
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BasePromptInteractionTypeArialive

        off

        polite

        assertive
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BasePromptInteractionTypeAriaorientation

        vertical

        horizontal
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="UploadInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("uploadInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class UploadInteractionType
        Inherits BasePromptInteractionType

        Private _type As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property type() As String
            Get
                Return Me._type
            End Get
            Set
                Me._type = Value
            End Set
        End Property

        Public ReadOnly Property typeSpecified() As Boolean
            Get
                Return (Equals(_type, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="ExtendedTextInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("extendedTextInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ExtendedTextInteractionType
        Inherits BasePromptInteractionType


        Private _stringIdentifier As String

        Private _expectedLength As String

        Private _patternMask As String

        Private _placeholderText As String

        Private _maxStrings As String

        Private _minStrings As String

        Private _expectedLines As String

        Private _format As ExtendedTextInteractionTypeFormat

        Public Sub New()
            MyBase.New
            Me._minStrings = "0"
            Me._format = ExtendedTextInteractionTypeFormat.plain
        End Sub


        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property stringIdentifier() As String
            Get
                Return Me._stringIdentifier
            End Get
            Set
                Me._stringIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property expectedLength() As String
            Get
                Return Me._expectedLength
            End Get
            Set
                Me._expectedLength = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property patternMask() As String
            Get
                Return Me._patternMask
            End Get
            Set
                Me._patternMask = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property placeholderText() As String
            Get
                Return Me._placeholderText
            End Get
            Set
                Me._placeholderText = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property maxStrings() As String
            Get
                Return Me._maxStrings
            End Get
            Set
                Me._maxStrings = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property minStrings() As String
            Get
                Return Me._minStrings
            End Get
            Set
                Me._minStrings = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property expectedLines() As String
            Get
                Return Me._expectedLines
            End Get
            Set
                Me._expectedLines = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(ExtendedTextInteractionTypeFormat.plain)>
        Public Property format() As ExtendedTextInteractionTypeFormat
            Get
                Return Me._format
            End Get
            Set
                Me._format = Value
            End Set
        End Property


        Public ReadOnly Property stringIdentifierSpecified() As Boolean
            Get
                Return (Equals(_stringIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property expectedLengthSpecified() As Boolean
            Get
                Return (Equals(_expectedLength, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property patternMaskSpecified() As Boolean
            Get
                Return (Equals(_patternMask, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property placeholderTextSpecified() As Boolean
            Get
                Return (Equals(_placeholderText, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxStringsSpecified() As Boolean
            Get
                Return (Equals(_maxStrings, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minStringsSpecified() As Boolean
            Get
                Return (Equals(_minStrings, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property expectedLinesSpecified() As Boolean
            Get
                Return (Equals(_expectedLines, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property formatSpecified() As Boolean
            Get
                Return (Equals(_format, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum ExtendedTextInteractionTypeFormat

        plain

        preformatted

        xhtml
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="OrderInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("orderInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class OrderInteractionType
        Inherits BasePromptInteractionType

        Private _simpleChoice As List(Of SimpleChoiceType)

        Private _shuffle As Boolean

        Private _minChoices As String

        Private _maxChoices As String

        Private _orientation As OrderInteractionTypeOrientation

        Private orientationFieldSpecified As Boolean

        Public Sub New()
            MyBase.New
            Me._shuffle = False
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("simpleChoice", Order:=0)>
        Public Property simpleChoice() As List(Of SimpleChoiceType)
            Get
                Return Me._simpleChoice
            End Get
            Set
                Me._simpleChoice = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property shuffle() As Boolean
            Get
                Return Me._shuffle
            End Get
            Set
                Me._shuffle = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property minChoices() As String
            Get
                Return Me._minChoices
            End Get
            Set
                Me._minChoices = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property maxChoices() As String
            Get
                Return Me._maxChoices
            End Get
            Set
                Me._maxChoices = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property orientation() As OrderInteractionTypeOrientation
            Get
                Return Me._orientation
            End Get
            Set
                Me._orientation = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property orientationSpecified() As Boolean
            Get
                Return Me.orientationFieldSpecified
            End Get
            Set
                Me.orientationFieldSpecified = Value
            End Set
        End Property

        Public ReadOnly Property simpleChoiceSpecified() As Boolean
            Get
                Return (Equals(_simpleChoice, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property shuffleSpecified() As Boolean
            Get
                Return (Equals(_shuffle, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minChoicesSpecified() As Boolean
            Get
                Return (Equals(_minChoices, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxChoicesSpecified() As Boolean
            Get
                Return (Equals(_maxChoices, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum OrderInteractionTypeOrientation

        horizontal

        vertical
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="HotTextInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("hottextInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class HotTextInteractionType
        Inherits BasePromptInteractionType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType21

        Private _maxChoices As String

        Private _minChoices As String

        Public Sub New()
            MyBase.New
            Me._maxChoices = "1"
            Me._minChoices = "0"
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(FeedbackBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType21()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("1")>
        Public Property maxChoices() As String
            Get
                Return Me._maxChoices
            End Get
            Set
                Me._maxChoices = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property minChoices() As String
            Get
                Return Me._minChoices
            End Get
            Set
                Me._minChoices = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxChoicesSpecified() As Boolean
            Get
                Return (Equals(_maxChoices, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minChoicesSpecified() As Boolean
            Get
                Return (Equals(_minChoices, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="FeedbackBlock.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("feedbackBlock", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class FeedbackBlockType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType32

        Private _stylesheet As List(Of StyleSheetType)

        Private _text As List(Of String)

        Private _outcomeIdentifier As String

        Private _identifier As String

        Private _showHide As FeedbackBlockTypeShowHide

        Public Sub New()
            MyBase.New
            Me._showHide = FeedbackBlockTypeShowHide.show
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("associateInteraction", GetType(AssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("choiceInteraction", GetType(ChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customInteraction", GetType(CustomInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("drawingInteraction", GetType(DrawingInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("extendedTextInteraction", GetType(ExtendedTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(FeedbackBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gapMatchInteraction", GetType(GapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicAssociateInteraction", GetType(GraphicAssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicGapMatchInteraction", GetType(GraphicGapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicOrderInteraction", GetType(GraphicOrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hotspotInteraction", GetType(HotspotInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottextInteraction", GetType(HotTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("infoControl", GetType(InfoControlType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("matchInteraction", GetType(MatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mediaInteraction", GetType(MediaInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("orderInteraction", GetType(OrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("positionObjectStage", GetType(PositionObjectStageType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("selectPointInteraction", GetType(SelectPointInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sliderInteraction", GetType(SliderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("uploadInteraction", GetType(UploadInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType32()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("stylesheet", Order:=2)>
        Public Property stylesheet() As List(Of StyleSheetType)
            Get
                Return Me._stylesheet
            End Get
            Set
                Me._stylesheet = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property outcomeIdentifier() As String
            Get
                Return Me._outcomeIdentifier
            End Get
            Set
                Me._outcomeIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(FeedbackBlockTypeShowHide.show)>
        Public Property showHide() As FeedbackBlockTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stylesheetSpecified() As Boolean
            Get
                Return (Equals(_stylesheet, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property outcomeIdentifierSpecified() As Boolean
            Get
                Return (Equals(_outcomeIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="GapMatchInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("gapMatchInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class GapMatchInteractionType
        Inherits BasePromptInteractionType

        Private _items As List(Of BaseSequenceType)

        Private _items1() As Object

        Private _items1ElementName() As Items1ChoiceType

        Private _shuffle As Boolean

        Private _minAssociations As String

        Private _maxAssociations As String

        Public Sub New()
            MyBase.New
            Me._shuffle = False
            Me._maxAssociations = "1"
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("gapImg", GetType(GapImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gapText", GetType(GapTextType), Order:=0)>
        Public Property Items() As List(Of BaseSequenceType)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(FeedbackBlockType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=1),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("Items1ElementName")>
        Public Property Items1() As Object()
            Get
                Return Me._items1
            End Get
            Set
                Me._items1 = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("Items1ElementName", Order:=2),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property Items1ElementName() As Items1ChoiceType()
            Get
                Return Me._items1ElementName
            End Get
            Set
                Me._items1ElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property shuffle() As Boolean
            Get
                Return Me._shuffle
            End Get
            Set
                Me._shuffle = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property minAssociations() As String
            Get
                Return Me._minAssociations
            End Get
            Set
                Me._minAssociations = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("1")>
        Public Property maxAssociations() As String
            Get
                Return Me._maxAssociations
            End Get
            Set
                Me._maxAssociations = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property Items1Specified() As Boolean
            Get
                Return (Equals(_items1, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property Items1ElementNameSpecified() As Boolean
            Get
                Return (Equals(_items1ElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property shuffleSpecified() As Boolean
            Get
                Return (Equals(_shuffle, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minAssociationsSpecified() As Boolean
            Get
                Return (Equals(_minAssociations, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxAssociationsSpecified() As Boolean
            Get
                Return (Equals(_maxAssociations, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="GapImg.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("gapImg", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class GapImgType
        Inherits BaseSequenceType

        Private _object As ObjectType

        Private _identifier As String

        Private _templateIdentifier As String

        Private _showHide As GapImgTypeShowHide

        Private _matchGroup As List(Of String)

        Private _matchMax As String

        Private _matchMin As String

        Private _objectLabel As String

        Private _top As String

        Private _left As String

        Public Sub New()
            MyBase.New
            Me._showHide = GapImgTypeShowHide.show
            Me._matchMin = "0"
        End Sub

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property [object]() As ObjectType
            Get
                Return Me._object
            End Get
            Set
                Me._object = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(GapImgTypeShowHide.show)>
        Public Property showHide() As GapImgTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property matchGroup() As List(Of String)
            Get
                Return Me._matchGroup
            End Get
            Set
                Me._matchGroup = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property matchMax() As String
            Get
                Return Me._matchMax
            End Get
            Set
                Me._matchMax = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property matchMin() As String
            Get
                Return Me._matchMin
            End Get
            Set
                Me._matchMin = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property objectLabel() As String
            Get
                Return Me._objectLabel
            End Get
            Set
                Me._objectLabel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property top() As String
            Get
                Return Me._top
            End Get
            Set
                Me._top = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property left() As String
            Get
                Return Me._left
            End Get
            Set
                Me._left = Value
            End Set
        End Property

        Public ReadOnly Property objectSpecified() As Boolean
            Get
                Return (Equals(_object, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property matchGroupSpecified() As Boolean
            Get
                Return (Equals(_matchGroup, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property matchMaxSpecified() As Boolean
            Get
                Return (Equals(_matchMax, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property matchMinSpecified() As Boolean
            Get
                Return (Equals(_matchMin, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property objectLabelSpecified() As Boolean
            Get
                Return (Equals(_objectLabel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property topSpecified() As Boolean
            Get
                Return (Equals(_top, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property leftSpecified() As Boolean
            Get
                Return (Equals(_left, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum GapImgTypeShowHide

        show

        hide
    End Enum

    <System.Xml.Serialization.XmlIncludeAttribute(GetType(StimulusBodyType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(SimpleChoiceType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(SimpleAssociableChoiceType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(TDHType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(TRType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(TablePartType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(PromptType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(LIType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(InfoControlType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(GapTextType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(GapImgType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(ColGroupType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(CaptionType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(InlineChoiceType)),
        System.Xml.Serialization.XmlIncludeAttribute(GetType(BDOType)),
        System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="BaseSequence.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Partial Public Class BaseSequenceType

        Private _id As String

        Private _class As List(Of String)

        Private _lang As String

        Private _label As String

        Private _dir As BaseSequenceTypeDir

        Private _role As BaseSequenceTypeRole

        Private roleFieldSpecified As Boolean

        Private _ariacontrols As String

        Private _ariadescribedby As String

        Private _ariaflowto As String

        Private _arialabel As String

        Private _arialabelledby As String

        Private _arialevel As String

        Private _arialive As BaseSequenceTypeArialive

        Private _ariaorientation As BaseSequenceTypeAriaorientation

        Private _ariaowns As String

        Private _anyAttr As List(Of System.Xml.XmlAttribute)

        Public Sub New()
            MyBase.New
            Me._dir = BaseSequenceTypeDir.[auto]
            Me._arialive = BaseSequenceTypeArialive.off
            Me._ariaorientation = BaseSequenceTypeAriaorientation.horizontal
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property id() As String
            Get
                Return Me._id
            End Get
            Set
                Me._id = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property [class]() As List(Of String)
            Get
                Return Me._class
            End Get
            Set
                Me._class = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property lang() As String
            Get
                Return Me._lang
            End Get
            Set
                Me._lang = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property label() As String
            Get
                Return Me._label
            End Get
            Set
                Me._label = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(BaseSequenceTypeDir.[auto])>
        Public Property dir() As BaseSequenceTypeDir
            Get
                Return Me._dir
            End Get
            Set
                Me._dir = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property role() As BaseSequenceTypeRole
            Get
                Return Me._role
            End Get
            Set
                Me._role = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property roleSpecified() As Boolean
            Get
                Return Me.roleFieldSpecified
            End Get
            Set
                Me.roleFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-controls", DataType:="IDREFS")>
        Public Property ariacontrols() As String
            Get
                Return Me._ariacontrols
            End Get
            Set
                Me._ariacontrols = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-describedby", DataType:="IDREFS")>
        Public Property ariadescribedby() As String
            Get
                Return Me._ariadescribedby
            End Get
            Set
                Me._ariadescribedby = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-flowto", DataType:="IDREFS")>
        Public Property ariaflowto() As String
            Get
                Return Me._ariaflowto
            End Get
            Set
                Me._ariaflowto = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-label", DataType:="normalizedString")>
        Public Property arialabel() As String
            Get
                Return Me._arialabel
            End Get
            Set
                Me._arialabel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-labelledby", DataType:="IDREFS")>
        Public Property arialabelledby() As String
            Get
                Return Me._arialabelledby
            End Get
            Set
                Me._arialabelledby = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-level", DataType:="integer")>
        Public Property arialevel() As String
            Get
                Return Me._arialevel
            End Get
            Set
                Me._arialevel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-live"),
            System.ComponentModel.DefaultValueAttribute(BaseSequenceTypeArialive.off)>
        Public Property arialive() As BaseSequenceTypeArialive
            Get
                Return Me._arialive
            End Get
            Set
                Me._arialive = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-orientation"),
            System.ComponentModel.DefaultValueAttribute(BaseSequenceTypeAriaorientation.horizontal)>
        Public Property ariaorientation() As BaseSequenceTypeAriaorientation
            Get
                Return Me._ariaorientation
            End Get
            Set
                Me._ariaorientation = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-owns", DataType:="IDREFS")>
        Public Property ariaowns() As String
            Get
                Return Me._ariaowns
            End Get
            Set
                Me._ariaowns = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As List(Of System.Xml.XmlAttribute)
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property

        Public ReadOnly Property idSpecified() As Boolean
            Get
                Return (Equals(_id, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property classSpecified() As Boolean
            Get
                Return (Equals(_class, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property langSpecified() As Boolean
            Get
                Return (Equals(_lang, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property labelSpecified() As Boolean
            Get
                Return (Equals(_label, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property dirSpecified() As Boolean
            Get
                Return (Equals(_dir, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariacontrolsSpecified() As Boolean
            Get
                Return (Equals(_ariacontrols, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariadescribedbySpecified() As Boolean
            Get
                Return (Equals(_ariadescribedby, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaflowtoSpecified() As Boolean
            Get
                Return (Equals(_ariaflowto, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialabelSpecified() As Boolean
            Get
                Return (Equals(_arialabel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialabelledbySpecified() As Boolean
            Get
                Return (Equals(_arialabelledby, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialevelSpecified() As Boolean
            Get
                Return (Equals(_arialevel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialiveSpecified() As Boolean
            Get
                Return (Equals(_arialive, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaorientationSpecified() As Boolean
            Get
                Return (Equals(_ariaorientation, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaownsSpecified() As Boolean
            Get
                Return (Equals(_ariaowns, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property AnyAttrSpecified() As Boolean
            Get
                Return (Equals(_anyAttr, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceTypeDir

        ltr

        rtl

        [auto]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceTypeRole

        article

        columnheader

        definition

        directory

        document

        group

        heading

        img

        list

        listitem

        math

        note

        presentation

        region

        row

        rowgroup

        rowheader

        separator

        toolbar

        button

        checkbox

        gridcell

        link

        log

        [option]

        radio

        slider

        spinbutton

        status

        tab

        tabpanel

        textbox

        timer

        listbox

        radiogroup

        tablist

        complementary

        contentinfo
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceTypeArialive

        off

        polite

        assertive
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceTypeAriaorientation

        vertical

        horizontal
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="StimulusBody.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("stimulusBody", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class StimulusBodyType
        Inherits BaseSequenceType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType31

        <System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("associateInteraction", GetType(AssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("choiceInteraction", GetType(ChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customInteraction", GetType(CustomInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("drawingInteraction", GetType(DrawingInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("extendedTextInteraction", GetType(ExtendedTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(FeedbackBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gapMatchInteraction", GetType(GapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicAssociateInteraction", GetType(GraphicAssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicGapMatchInteraction", GetType(GraphicGapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicOrderInteraction", GetType(GraphicOrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hotspotInteraction", GetType(HotspotInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottextInteraction", GetType(HotTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("infoControl", GetType(InfoControlType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("matchInteraction", GetType(MatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mediaInteraction", GetType(MediaInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("orderInteraction", GetType(OrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("positionObjectStage", GetType(PositionObjectStageType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("selectPointInteraction", GetType(SelectPointInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sliderInteraction", GetType(SliderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("uploadInteraction", GetType(UploadInteractionType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType31()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="GraphicAssociateInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("graphicAssociateInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class GraphicAssociateInteractionType
        Inherits BasePromptInteractionType

        Private _object As ObjectType

        Private _associableHotspot As List(Of AssociableHotspotType)

        Private _minAssociations As String

        Private _maxAssociations As String

        Public Sub New()
            MyBase.New
            Me._maxAssociations = "1"
        End Sub

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property [object]() As ObjectType
            Get
                Return Me._object
            End Get
            Set
                Me._object = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("associableHotspot", Order:=1)>
        Public Property associableHotspot() As List(Of AssociableHotspotType)
            Get
                Return Me._associableHotspot
            End Get
            Set
                Me._associableHotspot = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property minAssociations() As String
            Get
                Return Me._minAssociations
            End Get
            Set
                Me._minAssociations = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("1")>
        Public Property maxAssociations() As String
            Get
                Return Me._maxAssociations
            End Get
            Set
                Me._maxAssociations = Value
            End Set
        End Property

        Public ReadOnly Property objectSpecified() As Boolean
            Get
                Return (Equals(_object, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property associableHotspotSpecified() As Boolean
            Get
                Return (Equals(_associableHotspot, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minAssociationsSpecified() As Boolean
            Get
                Return (Equals(_minAssociations, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxAssociationsSpecified() As Boolean
            Get
                Return (Equals(_maxAssociations, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="GraphicGapMatchInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("graphicGapMatchInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class GraphicGapMatchInteractionType
        Inherits BaseSequenceFullType

        Private _prompt As PromptType

        Private _object As ObjectType

        Private _items As List(Of BaseSequenceType)

        Private _associableHotspot As List(Of AssociableHotspotType)

        Private _minAssociations As String

        Private _maxAssociations As String

        Public Sub New()
            MyBase.New
            Me._maxAssociations = "1"
        End Sub

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property prompt() As PromptType
            Get
                Return Me._prompt
            End Get
            Set
                Me._prompt = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)>
        Public Property [object]() As ObjectType
            Get
                Return Me._object
            End Get
            Set
                Me._object = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("gapImg", GetType(GapImgType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("gapText", GetType(GapTextType), Order:=2)>
        Public Property Items() As List(Of BaseSequenceType)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("associableHotspot", Order:=3)>
        Public Property associableHotspot() As List(Of AssociableHotspotType)
            Get
                Return Me._associableHotspot
            End Get
            Set
                Me._associableHotspot = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property minAssociations() As String
            Get
                Return Me._minAssociations
            End Get
            Set
                Me._minAssociations = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("1")>
        Public Property maxAssociations() As String
            Get
                Return Me._maxAssociations
            End Get
            Set
                Me._maxAssociations = Value
            End Set
        End Property

        Public ReadOnly Property promptSpecified() As Boolean
            Get
                Return (Equals(_prompt, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property objectSpecified() As Boolean
            Get
                Return (Equals(_object, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property associableHotspotSpecified() As Boolean
            Get
                Return (Equals(_associableHotspot, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minAssociationsSpecified() As Boolean
            Get
                Return (Equals(_minAssociations, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxAssociationsSpecified() As Boolean
            Get
                Return (Equals(_maxAssociations, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="GapText.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("gapText", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class GapTextType
        Inherits BaseSequenceType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType19

        Private _text As List(Of String)

        Private _identifier As String

        Private _templateIdentifier As String

        Private _showHide As GapTextTypeShowHide

        Private _matchGroup As List(Of String)

        Private _matchMax As String

        Private _matchMin As String

        Public Sub New()
            MyBase.New
            Me._showHide = GapTextTypeShowHide.show
            Me._matchMin = "0"
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType19()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(GapTextTypeShowHide.show)>
        Public Property showHide() As GapTextTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property matchGroup() As List(Of String)
            Get
                Return Me._matchGroup
            End Get
            Set
                Me._matchGroup = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property matchMax() As String
            Get
                Return Me._matchMax
            End Get
            Set
                Me._matchMax = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property matchMin() As String
            Get
                Return Me._matchMin
            End Get
            Set
                Me._matchMin = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property matchGroupSpecified() As Boolean
            Get
                Return (Equals(_matchGroup, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property matchMaxSpecified() As Boolean
            Get
                Return (Equals(_matchMax, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property matchMinSpecified() As Boolean
            Get
                Return (Equals(_matchMin, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType19

        a

        abbr

        acronym

        b

        bdo

        big

        br

        cite

        code

        dfn

        em

        feedbackInline

        i

        img

        kbd

        [object]

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        templateInline

        tt

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum GapTextTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="HotspotInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("hotspotInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class HotspotInteractionType
        Inherits BasePromptInteractionType

        Private _object As ObjectType

        Private _hotspotChoice As List(Of HotspotChoiceType)

        Private _minChoices As String

        Private _maxChoices As String

        Public Sub New()
            MyBase.New
            Me._minChoices = "0"
            Me._maxChoices = "1"
        End Sub

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property [object]() As ObjectType
            Get
                Return Me._object
            End Get
            Set
                Me._object = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("hotspotChoice", Order:=1)>
        Public Property hotspotChoice() As List(Of HotspotChoiceType)
            Get
                Return Me._hotspotChoice
            End Get
            Set
                Me._hotspotChoice = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property minChoices() As String
            Get
                Return Me._minChoices
            End Get
            Set
                Me._minChoices = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("1")>
        Public Property maxChoices() As String
            Get
                Return Me._maxChoices
            End Get
            Set
                Me._maxChoices = Value
            End Set
        End Property

        Public ReadOnly Property objectSpecified() As Boolean
            Get
                Return (Equals(_object, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property hotspotChoiceSpecified() As Boolean
            Get
                Return (Equals(_hotspotChoice, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minChoicesSpecified() As Boolean
            Get
                Return (Equals(_minChoices, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxChoicesSpecified() As Boolean
            Get
                Return (Equals(_maxChoices, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="InfoControl.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("infoControl", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class InfoControlType
        Inherits BaseSequenceType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType30

        Private _text As List(Of String)

        Private _title As String

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(FeedbackBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType30()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property title() As String
            Get
                Return Me._title
            End Get
            Set
                Me._title = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property titleSpecified() As Boolean
            Get
                Return (Equals(_title, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="OUL.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("ol", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class OULType
        Inherits BaseSequenceXBaseType

        Private _li As List(Of LIType)

        <System.Xml.Serialization.XmlElementAttribute("li", Order:=0)>
        Public Property li() As List(Of LIType)
            Get
                Return Me._li
            End Get
            Set
                Me._li = Value
            End Set
        End Property

        Public ReadOnly Property liSpecified() As Boolean
            Get
                Return (Equals(_li, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="LI.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("li", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class LIType
        Inherits BaseSequenceType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType27

        Private _text As List(Of String)

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("associateInteraction", GetType(AssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("choiceInteraction", GetType(ChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customInteraction", GetType(CustomInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("drawingInteraction", GetType(DrawingInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("endAttemptInteraction", GetType(EndAttemptInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("extendedTextInteraction", GetType(ExtendedTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(FeedbackBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gapMatchInteraction", GetType(GapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicAssociateInteraction", GetType(GraphicAssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicGapMatchInteraction", GetType(GraphicGapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicOrderInteraction", GetType(GraphicOrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hotspotInteraction", GetType(HotspotInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottext", GetType(HotTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottextInteraction", GetType(HotTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inlineChoiceInteraction", GetType(InlineChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("matchInteraction", GetType(MatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mediaInteraction", GetType(MediaInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("orderInteraction", GetType(OrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("selectPointInteraction", GetType(SelectPointInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sliderInteraction", GetType(SliderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("textEntryInteraction", GetType(TextEntryInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("uploadInteraction", GetType(UploadInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType27()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="InlineChoiceInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("inlineChoiceInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class InlineChoiceInteractionType
        Inherits BaseSequenceFullType

        Private _label1 As LabelType

        Private _inlineChoice As List(Of InlineChoiceType)

        Private _shuffle As Boolean

        Private _required As Boolean

        Public Sub New()
            MyBase.New
            Me._shuffle = False
            Me._required = False
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("label", Order:=0)>
        Public Property label1() As LabelType
            Get
                Return Me._label1
            End Get
            Set
                Me._label1 = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("inlineChoice", Order:=1)>
        Public Property inlineChoice() As List(Of InlineChoiceType)
            Get
                Return Me._inlineChoice
            End Get
            Set
                Me._inlineChoice = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property shuffle() As Boolean
            Get
                Return Me._shuffle
            End Get
            Set
                Me._shuffle = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property required() As Boolean
            Get
                Return Me._required
            End Get
            Set
                Me._required = Value
            End Set
        End Property

        Public ReadOnly Property label1Specified() As Boolean
            Get
                Return (Equals(_label1, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property inlineChoiceSpecified() As Boolean
            Get
                Return (Equals(_inlineChoice, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property shuffleSpecified() As Boolean
            Get
                Return (Equals(_shuffle, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property requiredSpecified() As Boolean
            Get
                Return (Equals(_required, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Label.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("label", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class LabelType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType15

        Private _text As List(Of String)

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType15()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType15

        a

        abbr

        acronym

        b

        bdo

        big

        br

        cite

        code

        dfn

        em

        feedbackInline

        i

        img

        kbd

        [object]

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        templateInline

        tt

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="InlineChoice.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("inlineChoice", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class InlineChoiceType
        Inherits BaseSequenceType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType16

        Private _text As List(Of String)

        Private _identifier As String

        Private _fixed As Boolean

        Private _templateIdentifier As String

        Private _showHide As InlineChoiceTypeShowHide

        Public Sub New()
            MyBase.New
            Me._fixed = False
            Me._showHide = InlineChoiceTypeShowHide.show
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType16()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property fixed() As Boolean
            Get
                Return Me._fixed
            End Get
            Set
                Me._fixed = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(InlineChoiceTypeShowHide.show)>
        Public Property showHide() As InlineChoiceTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property fixedSpecified() As Boolean
            Get
                Return (Equals(_fixed, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType16

        a

        abbr

        acronym

        b

        bdo

        big

        br

        cite

        code

        dfn

        em

        feedbackInline

        i

        img

        kbd

        [object]

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        templateInline

        tt

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum InlineChoiceTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="MatchInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("matchInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class MatchInteractionType
        Inherits BasePromptInteractionType

        Private _simpleMatchSet As List(Of SimpleMatchSetType)

        Private _shuffle As Boolean

        Private _maxAssociations As String

        Private _minAssociations As String

        Public Sub New()
            MyBase.New
            Me._shuffle = False
            Me._maxAssociations = "1"
            Me._minAssociations = "0"
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("simpleMatchSet", Order:=0)>
        Public Property simpleMatchSet() As List(Of SimpleMatchSetType)
            Get
                Return Me._simpleMatchSet
            End Get
            Set
                Me._simpleMatchSet = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property shuffle() As Boolean
            Get
                Return Me._shuffle
            End Get
            Set
                Me._shuffle = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("1")>
        Public Property maxAssociations() As String
            Get
                Return Me._maxAssociations
            End Get
            Set
                Me._maxAssociations = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property minAssociations() As String
            Get
                Return Me._minAssociations
            End Get
            Set
                Me._minAssociations = Value
            End Set
        End Property

        Public ReadOnly Property simpleMatchSetSpecified() As Boolean
            Get
                Return (Equals(_simpleMatchSet, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property shuffleSpecified() As Boolean
            Get
                Return (Equals(_shuffle, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxAssociationsSpecified() As Boolean
            Get
                Return (Equals(_maxAssociations, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minAssociationsSpecified() As Boolean
            Get
                Return (Equals(_minAssociations, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="SimpleMatchSet.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("simpleMatchSet", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class SimpleMatchSetType

        Private _simpleAssociableChoice As List(Of SimpleAssociableChoiceType)

        Private _id As String

        <System.Xml.Serialization.XmlElementAttribute("simpleAssociableChoice", Order:=0)>
        Public Property simpleAssociableChoice() As List(Of SimpleAssociableChoiceType)
            Get
                Return Me._simpleAssociableChoice
            End Get
            Set
                Me._simpleAssociableChoice = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property id() As String
            Get
                Return Me._id
            End Get
            Set
                Me._id = Value
            End Set
        End Property

        Public ReadOnly Property simpleAssociableChoiceSpecified() As Boolean
            Get
                Return (Equals(_simpleAssociableChoice, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property idSpecified() As Boolean
            Get
                Return (Equals(_id, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="MediaInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("mediaInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class MediaInteractionType
        Inherits BasePromptInteractionType

        Private _item As ObjectType

        Private _autostart As Boolean

        Private _minPlays As String

        Private _maxPlays As String

        Private _loop As Boolean

        Private _coords As String

        Public Sub New()
            MyBase.New
            Me._minPlays = "0"
            Me._maxPlays = "0"
            Me._loop = False
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("object", Order:=0)>
        Public Property Item() As ObjectType
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property autostart() As Boolean
            Get
                Return Me._autostart
            End Get
            Set
                Me._autostart = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property minPlays() As String
            Get
                Return Me._minPlays
            End Get
            Set
                Me._minPlays = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property maxPlays() As String
            Get
                Return Me._maxPlays
            End Get
            Set
                Me._maxPlays = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property [loop]() As Boolean
            Get
                Return Me._loop
            End Get
            Set
                Me._loop = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property coords() As String
            Get
                Return Me._coords
            End Get
            Set
                Me._coords = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property autostartSpecified() As Boolean
            Get
                Return (Equals(_autostart, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minPlaysSpecified() As Boolean
            Get
                Return (Equals(_minPlays, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxPlaysSpecified() As Boolean
            Get
                Return (Equals(_maxPlays, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property loopSpecified() As Boolean
            Get
                Return (Equals(_loop, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property coordsSpecified() As Boolean
            Get
                Return (Equals(_coords, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="SelectPointInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("selectPointInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class SelectPointInteractionType
        Inherits BasePromptInteractionType

        Private _object As ObjectType

        Private _minChoices As String

        Private _maxChoices As String

        Public Sub New()
            MyBase.New
            Me._minChoices = "0"
            Me._maxChoices = "0"
        End Sub

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property [object]() As ObjectType
            Get
                Return Me._object
            End Get
            Set
                Me._object = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property minChoices() As String
            Get
                Return Me._minChoices
            End Get
            Set
                Me._minChoices = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("0")>
        Public Property maxChoices() As String
            Get
                Return Me._maxChoices
            End Get
            Set
                Me._maxChoices = Value
            End Set
        End Property

        Public ReadOnly Property objectSpecified() As Boolean
            Get
                Return (Equals(_object, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minChoicesSpecified() As Boolean
            Get
                Return (Equals(_minChoices, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxChoicesSpecified() As Boolean
            Get
                Return (Equals(_maxChoices, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="SliderInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("sliderInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class SliderInteractionType
        Inherits BasePromptInteractionType

        Private _lowerBound As Double

        Private _upperBound As Double

        Private _step As Double

        Private _stepLabel As Boolean

        Private _orientation As SliderInteractionTypeOrientation

        Private orientationFieldSpecified As Boolean

        Private _reverse As Boolean

        Private reverseFieldSpecified As Boolean

        Public Sub New()
            MyBase.New
            Me._step = 1.0R
            Me._stepLabel = False
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property lowerBound() As Double
            Get
                Return Me._lowerBound
            End Get
            Set
                Me._lowerBound = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property upperBound() As Double
            Get
                Return Me._upperBound
            End Get
            Set
                Me._upperBound = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(1.0R)>
        Public Property [step]() As Double
            Get
                Return Me._step
            End Get
            Set
                Me._step = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property stepLabel() As Boolean
            Get
                Return Me._stepLabel
            End Get
            Set
                Me._stepLabel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property orientation() As SliderInteractionTypeOrientation
            Get
                Return Me._orientation
            End Get
            Set
                Me._orientation = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property orientationSpecified() As Boolean
            Get
                Return Me.orientationFieldSpecified
            End Get
            Set
                Me.orientationFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property reverse() As Boolean
            Get
                Return Me._reverse
            End Get
            Set
                Me._reverse = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property reverseSpecified() As Boolean
            Get
                Return Me.reverseFieldSpecified
            End Get
            Set
                Me.reverseFieldSpecified = Value
            End Set
        End Property

        Public ReadOnly Property lowerBoundSpecified() As Boolean
            Get
                Return (Equals(_lowerBound, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property upperBoundSpecified() As Boolean
            Get
                Return (Equals(_upperBound, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stepSpecified() As Boolean
            Get
                Return (Equals(_step, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stepLabelSpecified() As Boolean
            Get
                Return (Equals(_stepLabel, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum SliderInteractionTypeOrientation

        horizontal

        vertical
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Table.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("table", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TableType
        Inherits BaseSequenceXBaseType

        Private _caption As CaptionType

        Private _col As List(Of ColType)

        Private _colgroup As List(Of ColGroupType)

        Private _thead As TablePartType

        Private _tfoot As TablePartType

        Private _tbody As List(Of TablePartType)

        Private _summary As String

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property caption() As CaptionType
            Get
                Return Me._caption
            End Get
            Set
                Me._caption = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("col", Order:=1)>
        Public Property col() As List(Of ColType)
            Get
                Return Me._col
            End Get
            Set
                Me._col = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("colgroup", Order:=2)>
        Public Property colgroup() As List(Of ColGroupType)
            Get
                Return Me._colgroup
            End Get
            Set
                Me._colgroup = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=3)>
        Public Property thead() As TablePartType
            Get
                Return Me._thead
            End Get
            Set
                Me._thead = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=4)>
        Public Property tfoot() As TablePartType
            Get
                Return Me._tfoot
            End Get
            Set
                Me._tfoot = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("tbody", Order:=5)>
        Public Property tbody() As List(Of TablePartType)
            Get
                Return Me._tbody
            End Get
            Set
                Me._tbody = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property summary() As String
            Get
                Return Me._summary
            End Get
            Set
                Me._summary = Value
            End Set
        End Property

        Public ReadOnly Property captionSpecified() As Boolean
            Get
                Return (Equals(_caption, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property colSpecified() As Boolean
            Get
                Return (Equals(_col, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property colgroupSpecified() As Boolean
            Get
                Return (Equals(_colgroup, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property theadSpecified() As Boolean
            Get
                Return (Equals(_thead, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property tfootSpecified() As Boolean
            Get
                Return (Equals(_tfoot, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property tbodySpecified() As Boolean
            Get
                Return (Equals(_tbody, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property summarySpecified() As Boolean
            Get
                Return (Equals(_summary, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Caption.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("caption", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class CaptionType
        Inherits BaseSequenceType

        Private _items As List(Of Object)

        Private _text As List(Of String)

        <System.Xml.Serialization.XmlElementAttribute("customInteraction", GetType(CustomInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("endAttemptInteraction", GetType(EndAttemptInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gap", GetType(GapType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottext", GetType(HotTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inlineChoiceInteraction", GetType(InlineChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("textEntryInteraction", GetType(TextEntryInteractionType), Order:=0)>
        Public Property Items() As List(Of Object)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="ColGroup.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("colgroup", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ColGroupType
        Inherits BaseSequenceType

        Private _col As List(Of ColType)

        Private _span As Integer

        Private spanFieldSpecified As Boolean

        <System.Xml.Serialization.XmlElementAttribute("col", Order:=0)>
        Public Property col() As List(Of ColType)
            Get
                Return Me._col
            End Get
            Set
                Me._col = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property span() As Integer
            Get
                Return Me._span
            End Get
            Set
                Me._span = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property spanSpecified() As Boolean
            Get
                Return Me.spanFieldSpecified
            End Get
            Set
                Me.spanFieldSpecified = Value
            End Set
        End Property

        Public ReadOnly Property colSpecified() As Boolean
            Get
                Return (Equals(_col, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TablePart.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("tbody", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TablePartType
        Inherits BaseSequenceType

        Private _tr As List(Of TRType)

        <System.Xml.Serialization.XmlElementAttribute("tr", Order:=0)>
        Public Property tr() As List(Of TRType)
            Get
                Return Me._tr
            End Get
            Set
                Me._tr = Value
            End Set
        End Property

        Public ReadOnly Property trSpecified() As Boolean
            Get
                Return (Equals(_tr, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TR.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("tr", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TRType
        Inherits BaseSequenceType

        Private _items() As TDHType

        Private _itemsElementName() As ItemsChoiceType25

        <System.Xml.Serialization.XmlElementAttribute("td", GetType(TDHType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("th", GetType(TDHType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As TDHType()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType25()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TDH.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("td", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TDHType
        Inherits BaseSequenceType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType24

        Private _text As List(Of String)

        Private _headers As List(Of String)

        Private _scope As TDHTypeScope

        Private scopeFieldSpecified As Boolean

        Private _abbr As String

        Private _axis As String

        Private _rowspan As Integer

        Private rowspanFieldSpecified As Boolean

        Private _colspan As Integer

        Private colspanFieldSpecified As Boolean

        Private _align As TDHTypeAlign

        Private alignFieldSpecified As Boolean

        Private _valign As TDHTypeValign

        Private valignFieldSpecified As Boolean

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("associateInteraction", GetType(AssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("choiceInteraction", GetType(ChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customInteraction", GetType(CustomInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("drawingInteraction", GetType(DrawingInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("endAttemptInteraction", GetType(EndAttemptInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("extendedTextInteraction", GetType(ExtendedTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(FeedbackBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gapMatchInteraction", GetType(GapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicAssociateInteraction", GetType(GraphicAssociateInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicGapMatchInteraction", GetType(GraphicGapMatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("graphicOrderInteraction", GetType(GraphicOrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hotspotInteraction", GetType(HotspotInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottext", GetType(HotTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottextInteraction", GetType(HotTextInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inlineChoiceInteraction", GetType(InlineChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("matchInteraction", GetType(MatchInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mediaInteraction", GetType(MediaInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("orderInteraction", GetType(OrderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("selectPointInteraction", GetType(SelectPointInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sliderInteraction", GetType(SliderInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("textEntryInteraction", GetType(TextEntryInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("uploadInteraction", GetType(UploadInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType24()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property headers() As List(Of String)
            Get
                Return Me._headers
            End Get
            Set
                Me._headers = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property scope() As TDHTypeScope
            Get
                Return Me._scope
            End Get
            Set
                Me._scope = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property scopeSpecified() As Boolean
            Get
                Return Me.scopeFieldSpecified
            End Get
            Set
                Me.scopeFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property abbr() As String
            Get
                Return Me._abbr
            End Get
            Set
                Me._abbr = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property axis() As String
            Get
                Return Me._axis
            End Get
            Set
                Me._axis = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property rowspan() As Integer
            Get
                Return Me._rowspan
            End Get
            Set
                Me._rowspan = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property rowspanSpecified() As Boolean
            Get
                Return Me.rowspanFieldSpecified
            End Get
            Set
                Me.rowspanFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property colspan() As Integer
            Get
                Return Me._colspan
            End Get
            Set
                Me._colspan = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property colspanSpecified() As Boolean
            Get
                Return Me.colspanFieldSpecified
            End Get
            Set
                Me.colspanFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property align() As TDHTypeAlign
            Get
                Return Me._align
            End Get
            Set
                Me._align = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property alignSpecified() As Boolean
            Get
                Return Me.alignFieldSpecified
            End Get
            Set
                Me.alignFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property valign() As TDHTypeValign
            Get
                Return Me._valign
            End Get
            Set
                Me._valign = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property valignSpecified() As Boolean
            Get
                Return Me.valignFieldSpecified
            End Get
            Set
                Me.valignFieldSpecified = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property headersSpecified() As Boolean
            Get
                Return (Equals(_headers, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property abbrSpecified() As Boolean
            Get
                Return (Equals(_abbr, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property axisSpecified() As Boolean
            Get
                Return (Equals(_axis, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TemplateBlock.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("templateBlock", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TemplateBlockType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType29

        Private _stylesheet As List(Of StyleSheetType)

        Private _text As List(Of String)

        Private _templateIdentifier As String

        Private _showHide As TemplateBlockTypeShowHide

        Private _identifier As String

        Public Sub New()
            MyBase.New
            Me._showHide = TemplateBlockTypeShowHide.show
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(TemplateBlockFeedbackBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType29()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("stylesheet", Order:=2)>
        Public Property stylesheet() As List(Of StyleSheetType)
            Get
                Return Me._stylesheet
            End Get
            Set
                Me._stylesheet = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(TemplateBlockTypeShowHide.show)>
        Public Property showHide() As TemplateBlockTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stylesheetSpecified() As Boolean
            Get
                Return (Equals(_stylesheet, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TemplateBlockFeedbackBlock.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Partial Public Class TemplateBlockFeedbackBlockType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType28

        Private _stylesheet As List(Of StyleSheetType)

        Private _text As List(Of String)

        Private _outcomeIdentifier As String

        Private _showHide As TemplateBlockFeedbackBlockTypeShowHide

        Private _identifier As String

        Public Sub New()
            MyBase.New
            Me._showHide = TemplateBlockFeedbackBlockTypeShowHide.show
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackBlock", GetType(TemplateBlockFeedbackBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType28()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("stylesheet", Order:=2)>
        Public Property stylesheet() As List(Of StyleSheetType)
            Get
                Return Me._stylesheet
            End Get
            Set
                Me._stylesheet = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property outcomeIdentifier() As String
            Get
                Return Me._outcomeIdentifier
            End Get
            Set
                Me._outcomeIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(TemplateBlockFeedbackBlockTypeShowHide.show)>
        Public Property showHide() As TemplateBlockFeedbackBlockTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stylesheetSpecified() As Boolean
            Get
                Return (Equals(_stylesheet, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property outcomeIdentifierSpecified() As Boolean
            Get
                Return (Equals(_outcomeIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType28

        a

        abbr

        acronym

        address

        b

        bdo

        big

        blockquote

        br

        cite

        code

        dfn

        div

        dl

        em

        feedbackBlock

        feedbackInline

        h1

        h2

        h3

        h4

        h5

        h6

        hr

        i

        img

        kbd

        [object]

        ol

        p

        pre

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        templateInline

        tt

        ul

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="StyleSheet.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("stylesheet", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class StyleSheetType
        Inherits EmptyPrimitiveTypeType

        Private _href As String

        Private _type As String

        Private _media As String

        Private _title As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property href() As String
            Get
                Return Me._href
            End Get
            Set
                Me._href = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property type() As String
            Get
                Return Me._type
            End Get
            Set
                Me._type = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property media() As String
            Get
                Return Me._media
            End Get
            Set
                Me._media = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property title() As String
            Get
                Return Me._title
            End Get
            Set
                Me._title = Value
            End Set
        End Property

        Public ReadOnly Property hrefSpecified() As Boolean
            Get
                Return (Equals(_href, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property typeSpecified() As Boolean
            Get
                Return (Equals(_type, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property mediaSpecified() As Boolean
            Get
                Return (Equals(_media, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property titleSpecified() As Boolean
            Get
                Return (Equals(_title, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum TemplateBlockFeedbackBlockTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType29

        a

        abbr

        acronym

        address

        b

        bdo

        big

        blockquote

        br

        cite

        code

        dfn

        div

        dl

        em

        feedbackBlock

        feedbackInline

        h1

        h2

        h3

        h4

        h5

        h6

        hr

        i

        img

        kbd

        [object]

        ol

        p

        pre

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        templateInline

        tt

        ul

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum TemplateBlockTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType24

        a

        abbr

        acronym

        address

        associateInteraction

        b

        bdo

        big

        blockquote

        br

        choiceInteraction

        cite

        code

        customInteraction

        dfn

        div

        dl

        drawingInteraction

        em

        endAttemptInteraction

        extendedTextInteraction

        feedbackBlock

        feedbackInline

        gapMatchInteraction

        graphicAssociateInteraction

        graphicGapMatchInteraction

        graphicOrderInteraction

        h1

        h2

        h3

        h4

        h5

        h6

        hotspotInteraction

        hottext

        hottextInteraction

        hr

        i

        img

        inlineChoiceInteraction

        kbd

        matchInteraction

        mediaInteraction

        [object]

        ol

        orderInteraction

        p

        pre

        printedVariable

        q

        samp

        selectPointInteraction

        sliderInteraction

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        templateInline

        textEntryInteraction

        tt

        ul

        uploadInteraction

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum TDHTypeScope

        col

        colgroup

        row

        rowgroup
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum TDHTypeAlign

        left

        center

        right

        justify

        [char]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum TDHTypeValign

        bottom

        middle

        top

        baseline
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType25

        td

        th
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType27

        a

        abbr

        acronym

        address

        associateInteraction

        b

        bdo

        big

        blockquote

        br

        choiceInteraction

        cite

        code

        customInteraction

        dfn

        div

        dl

        drawingInteraction

        em

        endAttemptInteraction

        extendedTextInteraction

        feedbackBlock

        feedbackInline

        gapMatchInteraction

        graphicAssociateInteraction

        graphicGapMatchInteraction

        graphicOrderInteraction

        h1

        h2

        h3

        h4

        h5

        h6

        hotspotInteraction

        hottext

        hottextInteraction

        hr

        i

        img

        inlineChoiceInteraction

        kbd

        matchInteraction

        mediaInteraction

        [object]

        ol

        orderInteraction

        p

        pre

        printedVariable

        q

        samp

        selectPointInteraction

        sliderInteraction

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        templateInline

        textEntryInteraction

        tt

        ul

        uploadInteraction

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType30

        a

        abbr

        acronym

        address

        b

        bdo

        big

        blockquote

        br

        cite

        code

        dfn

        div

        dl

        em

        feedbackBlock

        feedbackInline

        h1

        h2

        h3

        h4

        h5

        h6

        hr

        i

        img

        kbd

        [object]

        ol

        p

        pre

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        templateInline

        tt

        ul

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="PositionObjectStage.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("positionObjectStage", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class PositionObjectStageType

        Private _object As ObjectType

        Private _positionObjectInteraction As List(Of PositionObjectInteractionType)

        Private _id As String

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property [object]() As ObjectType
            Get
                Return Me._object
            End Get
            Set
                Me._object = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("positionObjectInteraction", Order:=1)>
        Public Property positionObjectInteraction() As List(Of PositionObjectInteractionType)
            Get
                Return Me._positionObjectInteraction
            End Get
            Set
                Me._positionObjectInteraction = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property id() As String
            Get
                Return Me._id
            End Get
            Set
                Me._id = Value
            End Set
        End Property

        Public ReadOnly Property objectSpecified() As Boolean
            Get
                Return (Equals(_object, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property positionObjectInteractionSpecified() As Boolean
            Get
                Return (Equals(_positionObjectInteraction, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property idSpecified() As Boolean
            Get
                Return (Equals(_id, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="PositionObjectInteraction.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("positionObjectInteraction", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class PositionObjectInteractionType
        Inherits BaseSequenceRIdentType

        Private _object As ObjectType

        Private _centerPoint As List(Of Integer)

        Private _minChoices As String

        Private _maxChoices As String

        Public Sub New()
            MyBase.New
            Me._maxChoices = "1"
        End Sub

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property [object]() As ObjectType
            Get
                Return Me._object
            End Get
            Set
                Me._object = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property centerPoint() As List(Of Integer)
            Get
                Return Me._centerPoint
            End Get
            Set
                Me._centerPoint = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger")>
        Public Property minChoices() As String
            Get
                Return Me._minChoices
            End Get
            Set
                Me._minChoices = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="nonNegativeInteger"),
            System.ComponentModel.DefaultValueAttribute("1")>
        Public Property maxChoices() As String
            Get
                Return Me._maxChoices
            End Get
            Set
                Me._maxChoices = Value
            End Set
        End Property

        Public ReadOnly Property objectSpecified() As Boolean
            Get
                Return (Equals(_object, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property centerPointSpecified() As Boolean
            Get
                Return (Equals(_centerPoint, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property minChoicesSpecified() As Boolean
            Get
                Return (Equals(_minChoices, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property maxChoicesSpecified() As Boolean
            Get
                Return (Equals(_maxChoices, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.Xml.Serialization.XmlIncludeAttribute(GetType(PositionObjectInteractionType)),
        System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="BaseSequenceRIdent.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Partial Public Class BaseSequenceRIdentType

        Private _id As String

        Private _class As List(Of String)

        Private _lang As String

        Private _label As String

        Private _responseIdentifier As String

        Private _dir As BaseSequenceRIdentTypeDir

        Private _role As BaseSequenceRIdentTypeRole

        Private roleFieldSpecified As Boolean

        Private _ariacontrols As String

        Private _ariadescribedby As String

        Private _ariaflowto As String

        Private _arialabel As String

        Private _arialabelledby As String

        Private _arialevel As String

        Private _arialive As BaseSequenceRIdentTypeArialive

        Private arialiveFieldSpecified As Boolean

        Private _ariaorientation As BaseSequenceRIdentTypeAriaorientation

        Private _ariaowns As String

        Private _anyAttr As List(Of System.Xml.XmlAttribute)

        Public Sub New()
            MyBase.New
            Me._dir = BaseSequenceRIdentTypeDir.[auto]
            Me._ariaorientation = BaseSequenceRIdentTypeAriaorientation.horizontal
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property id() As String
            Get
                Return Me._id
            End Get
            Set
                Me._id = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property [class]() As List(Of String)
            Get
                Return Me._class
            End Get
            Set
                Me._class = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property lang() As String
            Get
                Return Me._lang
            End Get
            Set
                Me._lang = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property label() As String
            Get
                Return Me._label
            End Get
            Set
                Me._label = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property responseIdentifier() As String
            Get
                Return Me._responseIdentifier
            End Get
            Set
                Me._responseIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(BaseSequenceRIdentTypeDir.[auto])>
        Public Property dir() As BaseSequenceRIdentTypeDir
            Get
                Return Me._dir
            End Get
            Set
                Me._dir = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property role() As BaseSequenceRIdentTypeRole
            Get
                Return Me._role
            End Get
            Set
                Me._role = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property roleSpecified() As Boolean
            Get
                Return Me.roleFieldSpecified
            End Get
            Set
                Me.roleFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-controls", DataType:="IDREFS")>
        Public Property ariacontrols() As String
            Get
                Return Me._ariacontrols
            End Get
            Set
                Me._ariacontrols = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-describedby", DataType:="IDREFS")>
        Public Property ariadescribedby() As String
            Get
                Return Me._ariadescribedby
            End Get
            Set
                Me._ariadescribedby = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-flowto", DataType:="IDREFS")>
        Public Property ariaflowto() As String
            Get
                Return Me._ariaflowto
            End Get
            Set
                Me._ariaflowto = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-label", DataType:="normalizedString")>
        Public Property arialabel() As String
            Get
                Return Me._arialabel
            End Get
            Set
                Me._arialabel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-labelledby", DataType:="IDREFS")>
        Public Property arialabelledby() As String
            Get
                Return Me._arialabelledby
            End Get
            Set
                Me._arialabelledby = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-level", DataType:="integer")>
        Public Property arialevel() As String
            Get
                Return Me._arialevel
            End Get
            Set
                Me._arialevel = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-live")>
        Public Property arialive() As BaseSequenceRIdentTypeArialive
            Get
                Return Me._arialive
            End Get
            Set
                Me._arialive = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property arialiveSpecified() As Boolean
            Get
                Return Me.arialiveFieldSpecified
            End Get
            Set
                Me.arialiveFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-orientation"),
            System.ComponentModel.DefaultValueAttribute(BaseSequenceRIdentTypeAriaorientation.horizontal)>
        Public Property ariaorientation() As BaseSequenceRIdentTypeAriaorientation
            Get
                Return Me._ariaorientation
            End Get
            Set
                Me._ariaorientation = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("aria-owns", DataType:="IDREFS")>
        Public Property ariaowns() As String
            Get
                Return Me._ariaowns
            End Get
            Set
                Me._ariaowns = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As List(Of System.Xml.XmlAttribute)
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property

        Public ReadOnly Property idSpecified() As Boolean
            Get
                Return (Equals(_id, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property classSpecified() As Boolean
            Get
                Return (Equals(_class, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property langSpecified() As Boolean
            Get
                Return (Equals(_lang, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property labelSpecified() As Boolean
            Get
                Return (Equals(_label, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property responseIdentifierSpecified() As Boolean
            Get
                Return (Equals(_responseIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property dirSpecified() As Boolean
            Get
                Return (Equals(_dir, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariacontrolsSpecified() As Boolean
            Get
                Return (Equals(_ariacontrols, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariadescribedbySpecified() As Boolean
            Get
                Return (Equals(_ariadescribedby, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaflowtoSpecified() As Boolean
            Get
                Return (Equals(_ariaflowto, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialabelSpecified() As Boolean
            Get
                Return (Equals(_arialabel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialabelledbySpecified() As Boolean
            Get
                Return (Equals(_arialabelledby, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property arialevelSpecified() As Boolean
            Get
                Return (Equals(_arialevel, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaorientationSpecified() As Boolean
            Get
                Return (Equals(_ariaorientation, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ariaownsSpecified() As Boolean
            Get
                Return (Equals(_ariaowns, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property AnyAttrSpecified() As Boolean
            Get
                Return (Equals(_anyAttr, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceRIdentTypeDir

        ltr

        rtl

        [auto]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceRIdentTypeRole

        article

        columnheader

        definition

        directory

        document

        group

        heading

        img

        list

        listitem

        math

        note

        presentation

        region

        row

        rowgroup

        rowheader

        separator

        toolbar

        button

        checkbox

        gridcell

        link

        log

        [option]

        radio

        slider

        spinbutton

        status

        tab

        tabpanel

        textbox

        timer

        listbox

        radiogroup

        tablist

        complementary

        contentinfo
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceRIdentTypeArialive

        off

        polite

        assertive
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum BaseSequenceRIdentTypeAriaorientation

        vertical

        horizontal
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType31

        address

        associateInteraction

        blockquote

        choiceInteraction

        customInteraction

        div

        dl

        drawingInteraction

        extendedTextInteraction

        feedbackBlock

        gapMatchInteraction

        graphicAssociateInteraction

        graphicGapMatchInteraction

        graphicOrderInteraction

        h1

        h2

        h3

        h4

        h5

        h6

        hotspotInteraction

        hottextInteraction

        hr

        infoControl

        matchInteraction

        mediaInteraction

        [object]

        ol

        orderInteraction

        p

        positionObjectStage

        pre

        selectPointInteraction

        sliderInteraction

        table

        templateBlock

        ul

        uploadInteraction
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum Items1ChoiceType

        address

        blockquote

        div

        dl

        feedbackBlock

        h1

        h2

        h3

        h4

        h5

        h6

        hr

        [object]

        ol

        p

        pre

        table

        templateBlock

        ul
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType32

        a

        abbr

        acronym

        address

        associateInteraction

        b

        bdo

        big

        blockquote

        br

        choiceInteraction

        cite

        code

        customInteraction

        dfn

        div

        dl

        drawingInteraction

        em

        extendedTextInteraction

        feedbackBlock

        feedbackInline

        gapMatchInteraction

        graphicAssociateInteraction

        graphicGapMatchInteraction

        graphicOrderInteraction

        h1

        h2

        h3

        h4

        h5

        h6

        hotspotInteraction

        hottextInteraction

        hr

        i

        img

        infoControl

        kbd

        matchInteraction

        mediaInteraction

        [object]

        ol

        orderInteraction

        p

        positionObjectStage

        pre

        printedVariable

        q

        samp

        selectPointInteraction

        sliderInteraction

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        templateInline

        tt

        ul

        uploadInteraction

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum FeedbackBlockTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType21

        address

        blockquote

        div

        dl

        feedbackBlock

        h1

        h2

        h3

        h4

        h5

        h6

        hr

        [object]

        ol

        p

        pre

        table

        templateBlock

        ul
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType33

        a

        abbr

        acronym

        address

        associateInteraction

        b

        bdo

        big

        blockquote

        br

        choiceInteraction

        cite

        code

        customInteraction

        dfn

        div

        dl

        drawingInteraction

        em

        endAttemptInteraction

        extendedTextInteraction

        feedbackBlock

        feedbackInline

        gapMatchInteraction

        graphicAssociateInteraction

        graphicGapMatchInteraction

        graphicOrderInteraction

        h1

        h2

        h3

        h4

        h5

        h6

        hotspotInteraction

        hottext

        hottextInteraction

        hr

        i

        img

        inlineChoiceInteraction

        kbd

        matchInteraction

        mediaInteraction

        [object]

        ol

        orderInteraction

        p

        pre

        printedVariable

        q

        samp

        selectPointInteraction

        sliderInteraction

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        templateInline

        textEntryInteraction

        tt

        ul

        uploadInteraction

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="DT.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("dt", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class DTType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType34

        Private _text As List(Of String)

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customInteraction", GetType(CustomInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("endAttemptInteraction", GetType(EndAttemptInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("feedbackInline", GetType(FeedbackInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gap", GetType(GapType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottext", GetType(HotTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inlineChoiceInteraction", GetType(InlineChoiceInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("textEntryInteraction", GetType(TextEntryInteractionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType34()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType34

        a

        abbr

        acronym

        b

        bdo

        big

        br

        cite

        code

        customInteraction

        dfn

        em

        endAttemptInteraction

        feedbackInline

        gap

        hottext

        i

        img

        inlineChoiceInteraction

        kbd

        [object]

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        templateInline

        textEntryInteraction

        tt

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType20

        a

        abbr

        acronym

        address

        b

        bdo

        big

        blockquote

        br

        cite

        code

        dfn

        div

        dl

        em

        feedbackBlock

        feedbackInline

        h1

        h2

        h3

        h4

        h5

        h6

        hr

        i

        img

        kbd

        [object]

        ol

        p

        pre

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        templateInline

        tt

        ul

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum SimpleChoiceTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum ChoiceInteractionTypeOrientation

        horizontal

        vertical
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType22

        a

        abbr

        acronym

        address

        associateInteraction

        b

        bdo

        big

        blockquote

        br

        choiceInteraction

        cite

        code

        customInteraction

        dfn

        div

        dl

        drawingInteraction

        em

        endAttemptInteraction

        extendedTextInteraction

        feedbackBlock

        feedbackInline

        gapMatchInteraction

        graphicAssociateInteraction

        graphicGapMatchInteraction

        graphicOrderInteraction

        h1

        h2

        h3

        h4

        h5

        h6

        hotspotInteraction

        hottext

        hottextInteraction

        hr

        i

        img

        inlineChoiceInteraction

        kbd

        matchInteraction

        mediaInteraction

        [object]

        ol

        orderInteraction

        p

        positionObjectStage

        pre

        printedVariable

        q

        samp

        selectPointInteraction

        sliderInteraction

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        templateInline

        textEntryInteraction

        tt

        ul

        uploadInteraction

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType23

        a

        abbr

        acronym

        address

        b

        bdo

        big

        blockquote

        br

        cite

        code

        dfn

        div

        dl

        em

        feedbackBlock

        feedbackInline

        h1

        h2

        h3

        h4

        h5

        h6

        hr

        i

        img

        kbd

        [object]

        ol

        p

        pre

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        templateInline

        tt

        ul

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum SimpleAssociableChoiceTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType40

        a

        abbr

        acronym

        address

        b

        bdo

        big

        blockquote

        br

        cite

        code

        dfn

        div

        dl

        em

        h1

        h2

        h3

        h4

        h5

        h6

        hr

        i

        img

        kbd

        [object]

        ol

        p

        pre

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        tt

        ul

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum RubricBlockTemplateBlockTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="RubricBlock.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("rubricBlock", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class RubricBlockType
        Inherits BaseSequenceXBaseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType42

        Private _stylesheet As List(Of StyleSheetType)

        Private _text As List(Of String)

        Private _use As String

        Private _view As List(Of ViewType)

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(RubricBlockTemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(RubricBlockTemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType42()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("stylesheet", Order:=2)>
        Public Property stylesheet() As List(Of StyleSheetType)
            Get
                Return Me._stylesheet
            End Get
            Set
                Me._stylesheet = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property use() As String
            Get
                Return Me._use
            End Get
            Set
                Me._use = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property view() As List(Of ViewType)
            Get
                Return Me._view
            End Get
            Set
                Me._view = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stylesheetSpecified() As Boolean
            Get
                Return (Equals(_stylesheet, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property useSpecified() As Boolean
            Get
                Return (Equals(_use, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property viewSpecified() As Boolean
            Get
                Return (Equals(_view, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType42

        a

        abbr

        acronym

        address

        b

        bdo

        big

        blockquote

        br

        cite

        code

        dfn

        div

        dl

        em

        h1

        h2

        h3

        h4

        h5

        h6

        hr

        i

        img

        kbd

        [object]

        ol

        p

        pre

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        templateInline

        tt

        ul

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="View.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum ViewType

        author

        candidate

        proctor

        scorer

        testConstructor

        tutor
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType36

        a

        abbr

        acronym

        b

        bdo

        big

        br

        cite

        code

        dfn

        em

        feedbackInline

        gap

        hottext

        i

        img

        kbd

        [object]

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        templateInline

        tt

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum TemplateInlineTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType38

        a

        abbr

        acronym

        b

        bdo

        big

        br

        cite

        code

        dfn

        em

        i

        img

        kbd

        [object]

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        templateInline

        tt

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum FeedbackInlineTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType17

        a

        abbr

        acronym

        b

        bdo

        big

        br

        cite

        code

        customInteraction

        dfn

        em

        endAttemptInteraction

        feedbackInline

        gap

        hottext

        i

        img

        inlineChoiceInteraction

        kbd

        [object]

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        templateInline

        textEntryInteraction

        tt

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType18

        a

        abbr

        acronym

        b

        bdo

        big

        br

        cite

        code

        dfn

        em

        i

        img

        kbd

        [object]

        q

        samp

        small

        span

        strong

        [sub]

        sup

        tt

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType37

        a

        abbr

        acronym

        b

        bdo

        big

        br

        cite

        code

        customInteraction

        dfn

        em

        endAttemptInteraction

        feedbackInline

        gap

        hottext

        i

        img

        inlineChoiceInteraction

        kbd

        [object]

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        templateInline

        textEntryInteraction

        tt

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType26

        a

        abbr

        acronym

        address

        b

        bdo

        big

        blockquote

        br

        cite

        code

        dfn

        div

        dl

        em

        h1

        h2

        h3

        h4

        h5

        h6

        hr

        i

        img

        kbd

        [object]

        ol

        p

        pre

        q

        samp

        small

        span

        strong

        [sub]

        sup

        table

        tt

        ul

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Param.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("param", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ParamType
        Inherits EmptyPrimitiveTypeType

        Private _name As String

        Private _value As String

        Private _valuetype As ParamTypeValuetype

        Private _type As String

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property name() As String
            Get
                Return Me._name
            End Get
            Set
                Me._name = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property value() As String
            Get
                Return Me._value
            End Get
            Set
                Me._value = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property valuetype() As ParamTypeValuetype
            Get
                Return Me._valuetype
            End Get
            Set
                Me._valuetype = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property type() As String
            Get
                Return Me._type
            End Get
            Set
                Me._type = Value
            End Set
        End Property

        Public ReadOnly Property nameSpecified() As Boolean
            Get
                Return (Equals(_name, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property valueSpecified() As Boolean
            Get
                Return (Equals(_value, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property valuetypeSpecified() As Boolean
            Get
                Return (Equals(_valuetype, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property typeSpecified() As Boolean
            Get
                Return (Equals(_type, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum ParamTypeValuetype

        DATA

        REF
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType35

        a

        abbr

        acronym

        address

        b

        bdo

        big

        blockquote

        br

        cite

        code

        dfn

        div

        dl

        em

        h1

        h2

        h3

        h4

        h5

        h6

        hr

        i

        img

        kbd

        [object]

        ol

        p

        param

        pre

        q

        samp

        small

        span

        strong

        [sub]

        sup

        table

        tt

        ul

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="OutcomeCondition.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("outcomeCondition", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class OutcomeConditionType

        Private _outcomeIf As OutcomeIfType

        Private _outcomeElseIf As List(Of OutcomeIfType)

        Private _outcomeElse As OutcomeElseType

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property outcomeIf() As OutcomeIfType
            Get
                Return Me._outcomeIf
            End Get
            Set
                Me._outcomeIf = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("outcomeElseIf", Order:=1)>
        Public Property outcomeElseIf() As List(Of OutcomeIfType)
            Get
                Return Me._outcomeElseIf
            End Get
            Set
                Me._outcomeElseIf = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>
        Public Property outcomeElse() As OutcomeElseType
            Get
                Return Me._outcomeElse
            End Get
            Set
                Me._outcomeElse = Value
            End Set
        End Property

        Public ReadOnly Property outcomeIfSpecified() As Boolean
            Get
                Return (Equals(_outcomeIf, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property outcomeElseIfSpecified() As Boolean
            Get
                Return (Equals(_outcomeElseIf, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property outcomeElseSpecified() As Boolean
            Get
                Return (Equals(_outcomeElse, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="OutcomeIf.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("outcomeElseIf", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class OutcomeIfType

        Private _item As Object

        Private _itemElementName As ItemChoiceType14

        Private _items As List(Of Object)

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType14
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("exitTest", GetType(EmptyPrimitiveTypeType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("lookupOutcomeValue", GetType(LookupOutcomeValueType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("outcomeCondition", GetType(OutcomeConditionType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("outcomeProcessingFragment", GetType(OutcomeProcessingFragmentType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("setOutcomeValue", GetType(SetValueType), Order:=2)>
        Public Property Items() As List(Of Object)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType14

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="OutcomeProcessingFragment.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("outcomeProcessingFragment", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class OutcomeProcessingFragmentType

        Private _items As List(Of Object)

        <System.Xml.Serialization.XmlElementAttribute("exitTest", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lookupOutcomeValue", GetType(LookupOutcomeValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeCondition", GetType(OutcomeConditionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeProcessingFragment", GetType(OutcomeProcessingFragmentType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("setOutcomeValue", GetType(SetValueType), Order:=0)>
        Public Property Items() As List(Of Object)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="SetValue.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("setCorrectResponse", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class SetValueType

        Private _item As Object

        Private _itemElementName As ItemChoiceType7

        Private _identifier As String

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType7
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType7

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="OutcomeProcessing.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("outcomeProcessing", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class OutcomeProcessingType

        Private _items As List(Of Object)

        <System.Xml.Serialization.XmlElementAttribute("exitTest", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lookupOutcomeValue", GetType(LookupOutcomeValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeCondition", GetType(OutcomeConditionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeProcessingFragment", GetType(OutcomeProcessingFragmentType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("setOutcomeValue", GetType(SetValueType), Order:=0)>
        Public Property Items() As List(Of Object)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TestFeedback.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("testFeedback", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TestFeedbackType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType45

        Private _stylesheet As List(Of StyleSheetType)

        Private _text As List(Of String)

        Private _access As TestFeedbackTypeAccess

        Private _outcomeIdentifier As String

        Private _showHide As TestFeedbackTypeShowHide

        Private _identifier As String

        Private _title As String

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottext", GetType(HotTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType45()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("stylesheet", Order:=2)>
        Public Property stylesheet() As List(Of StyleSheetType)
            Get
                Return Me._stylesheet
            End Get
            Set
                Me._stylesheet = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property access() As TestFeedbackTypeAccess
            Get
                Return Me._access
            End Get
            Set
                Me._access = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property outcomeIdentifier() As String
            Get
                Return Me._outcomeIdentifier
            End Get
            Set
                Me._outcomeIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property showHide() As TestFeedbackTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property title() As String
            Get
                Return Me._title
            End Get
            Set
                Me._title = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stylesheetSpecified() As Boolean
            Get
                Return (Equals(_stylesheet, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property accessSpecified() As Boolean
            Get
                Return (Equals(_access, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property outcomeIdentifierSpecified() As Boolean
            Get
                Return (Equals(_outcomeIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property titleSpecified() As Boolean
            Get
                Return (Equals(_title, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType45

        a

        abbr

        acronym

        address

        b

        bdo

        big

        blockquote

        br

        cite

        code

        dfn

        div

        dl

        em

        h1

        h2

        h3

        h4

        h5

        h6

        hottext

        hr

        i

        img

        kbd

        [object]

        ol

        p

        pre

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        templateInline

        tt

        ul

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum TestFeedbackTypeAccess

        atEnd

        during
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum TestFeedbackTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TestPart.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("testPart", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TestPartType

        Private _preCondition As List(Of LogicSingleType)

        Private _branchRule As List(Of BranchRuleType)

        Private _itemSessionControl As ItemSessionControlType

        Private _timeLimits As TimeLimitsType

        Private _items As List(Of Object)

        Private _testFeedback As List(Of TestFeedbackType)

        Private _identifier As String

        Private _navigationMode As TestPartTypeNavigationMode

        Private _submissionMode As TestPartTypeSubmissionMode

        <System.Xml.Serialization.XmlElementAttribute("preCondition", Order:=0)>
        Public Property preCondition() As List(Of LogicSingleType)
            Get
                Return Me._preCondition
            End Get
            Set
                Me._preCondition = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("branchRule", Order:=1)>
        Public Property branchRule() As List(Of BranchRuleType)
            Get
                Return Me._branchRule
            End Get
            Set
                Me._branchRule = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>
        Public Property itemSessionControl() As ItemSessionControlType
            Get
                Return Me._itemSessionControl
            End Get
            Set
                Me._itemSessionControl = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=3)>
        Public Property timeLimits() As TimeLimitsType
            Get
                Return Me._timeLimits
            End Get
            Set
                Me._timeLimits = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("assessmentSection", GetType(AssessmentSectionType), Order:=4),
            System.Xml.Serialization.XmlElementAttribute("assessmentSectionRef", GetType(AssessmentSectionRefType), Order:=4)>
        Public Property Items() As List(Of Object)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("testFeedback", Order:=5)>
        Public Property testFeedback() As List(Of TestFeedbackType)
            Get
                Return Me._testFeedback
            End Get
            Set
                Me._testFeedback = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property navigationMode() As TestPartTypeNavigationMode
            Get
                Return Me._navigationMode
            End Get
            Set
                Me._navigationMode = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property submissionMode() As TestPartTypeSubmissionMode
            Get
                Return Me._submissionMode
            End Get
            Set
                Me._submissionMode = Value
            End Set
        End Property

        Public ReadOnly Property preConditionSpecified() As Boolean
            Get
                Return (Equals(_preCondition, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property branchRuleSpecified() As Boolean
            Get
                Return (Equals(_branchRule, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property itemSessionControlSpecified() As Boolean
            Get
                Return (Equals(_itemSessionControl, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property timeLimitsSpecified() As Boolean
            Get
                Return (Equals(_timeLimits, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property testFeedbackSpecified() As Boolean
            Get
                Return (Equals(_testFeedback, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property navigationModeSpecified() As Boolean
            Get
                Return (Equals(_navigationMode, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property submissionModeSpecified() As Boolean
            Get
                Return (Equals(_submissionMode, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="BranchRule.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("branchRule", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class BranchRuleType

        Private _item As Object

        Private _itemElementName As ItemChoiceType12

        Private _target As String

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType12
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property target() As String
            Get
                Return Me._target
            End Get
            Set
                Me._target = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property targetSpecified() As Boolean
            Get
                Return (Equals(_target, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType12

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="ItemSessionControl.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("itemSessionControl", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ItemSessionControlType
        Inherits EmptyPrimitiveTypeType

        Private _maxAttempts As Integer

        Private maxAttemptsFieldSpecified As Boolean

        Private _showFeedback As Boolean

        Private _allowReview As Boolean

        Private _showSolution As Boolean

        Private _allowComment As Boolean

        Private _allowSkipping As Boolean

        Private _validateResponses As Boolean

        Public Sub New()
            MyBase.New
            Me._showFeedback = False
            Me._allowReview = True
            Me._showSolution = False
            Me._allowComment = False
            Me._allowSkipping = True
            Me._validateResponses = False
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property maxAttempts() As Integer
            Get
                Return Me._maxAttempts
            End Get
            Set
                Me._maxAttempts = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property maxAttemptsSpecified() As Boolean
            Get
                Return Me.maxAttemptsFieldSpecified
            End Get
            Set
                Me.maxAttemptsFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property showFeedback() As Boolean
            Get
                Return Me._showFeedback
            End Get
            Set
                Me._showFeedback = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(True)>
        Public Property allowReview() As Boolean
            Get
                Return Me._allowReview
            End Get
            Set
                Me._allowReview = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property showSolution() As Boolean
            Get
                Return Me._showSolution
            End Get
            Set
                Me._showSolution = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property allowComment() As Boolean
            Get
                Return Me._allowComment
            End Get
            Set
                Me._allowComment = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(True)>
        Public Property allowSkipping() As Boolean
            Get
                Return Me._allowSkipping
            End Get
            Set
                Me._allowSkipping = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property validateResponses() As Boolean
            Get
                Return Me._validateResponses
            End Get
            Set
                Me._validateResponses = Value
            End Set
        End Property

        Public ReadOnly Property showFeedbackSpecified() As Boolean
            Get
                Return (Equals(_showFeedback, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property allowReviewSpecified() As Boolean
            Get
                Return (Equals(_allowReview, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showSolutionSpecified() As Boolean
            Get
                Return (Equals(_showSolution, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property allowCommentSpecified() As Boolean
            Get
                Return (Equals(_allowComment, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property allowSkippingSpecified() As Boolean
            Get
                Return (Equals(_allowSkipping, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property validateResponsesSpecified() As Boolean
            Get
                Return (Equals(_validateResponses, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TimeLimits.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("timeLimits", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TimeLimitsType
        Inherits EmptyPrimitiveTypeType

        Private _minTime As Double

        Private minTimeFieldSpecified As Boolean

        Private _maxTime As Double

        Private maxTimeFieldSpecified As Boolean

        Private _allowLateSubmission As Boolean

        Public Sub New()
            MyBase.New
            Me._allowLateSubmission = False
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property minTime() As Double
            Get
                Return Me._minTime
            End Get
            Set
                Me._minTime = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property minTimeSpecified() As Boolean
            Get
                Return Me.minTimeFieldSpecified
            End Get
            Set
                Me.minTimeFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property maxTime() As Double
            Get
                Return Me._maxTime
            End Get
            Set
                Me._maxTime = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property maxTimeSpecified() As Boolean
            Get
                Return Me.maxTimeFieldSpecified
            End Get
            Set
                Me.maxTimeFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property allowLateSubmission() As Boolean
            Get
                Return Me._allowLateSubmission
            End Get
            Set
                Me._allowLateSubmission = Value
            End Set
        End Property

        Public ReadOnly Property allowLateSubmissionSpecified() As Boolean
            Get
                Return (Equals(_allowLateSubmission, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="AssessmentSection.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("assessmentSection", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class AssessmentSectionType

        Private _preCondition As List(Of LogicSingleType)

        Private _branchRule As List(Of BranchRuleType)

        Private _itemSessionControl As ItemSessionControlType

        Private _timeLimits As TimeLimitsType

        Private _selection As SelectionType

        Private _ordering As OrderingType

        Private _rubricBlock As List(Of RubricBlockType)

        Private _items As List(Of Object)

        Private _identifier As String

        Private _required As Boolean

        Private _fixed As Boolean

        Private _title As String

        Private _visible As Boolean

        Private _keepTogether As Boolean

        Public Sub New()
            MyBase.New
            Me._required = False
            Me._fixed = False
            Me._keepTogether = True
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("preCondition", Order:=0)>
        Public Property preCondition() As List(Of LogicSingleType)
            Get
                Return Me._preCondition
            End Get
            Set
                Me._preCondition = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("branchRule", Order:=1)>
        Public Property branchRule() As List(Of BranchRuleType)
            Get
                Return Me._branchRule
            End Get
            Set
                Me._branchRule = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>
        Public Property itemSessionControl() As ItemSessionControlType
            Get
                Return Me._itemSessionControl
            End Get
            Set
                Me._itemSessionControl = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=3)>
        Public Property timeLimits() As TimeLimitsType
            Get
                Return Me._timeLimits
            End Get
            Set
                Me._timeLimits = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=4)>
        Public Property selection() As SelectionType
            Get
                Return Me._selection
            End Get
            Set
                Me._selection = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=5)>
        Public Property ordering() As OrderingType
            Get
                Return Me._ordering
            End Get
            Set
                Me._ordering = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("rubricBlock", Order:=6)>
        Public Property rubricBlock() As List(Of RubricBlockType)
            Get
                Return Me._rubricBlock
            End Get
            Set
                Me._rubricBlock = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("assessmentItemRef", GetType(AssessmentItemRefType), Order:=7),
            System.Xml.Serialization.XmlElementAttribute("assessmentSection", GetType(AssessmentSectionType), Order:=7),
            System.Xml.Serialization.XmlElementAttribute("assessmentSectionRef", GetType(AssessmentSectionRefType), Order:=7),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=7)>
        Public Property Items() As List(Of Object)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property required() As Boolean
            Get
                Return Me._required
            End Get
            Set
                Me._required = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property fixed() As Boolean
            Get
                Return Me._fixed
            End Get
            Set
                Me._fixed = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property title() As String
            Get
                Return Me._title
            End Get
            Set
                Me._title = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property visible() As Boolean
            Get
                Return Me._visible
            End Get
            Set
                Me._visible = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(True)>
        Public Property keepTogether() As Boolean
            Get
                Return Me._keepTogether
            End Get
            Set
                Me._keepTogether = Value
            End Set
        End Property

        Public ReadOnly Property preConditionSpecified() As Boolean
            Get
                Return (Equals(_preCondition, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property branchRuleSpecified() As Boolean
            Get
                Return (Equals(_branchRule, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property itemSessionControlSpecified() As Boolean
            Get
                Return (Equals(_itemSessionControl, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property timeLimitsSpecified() As Boolean
            Get
                Return (Equals(_timeLimits, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property selectionSpecified() As Boolean
            Get
                Return (Equals(_selection, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property orderingSpecified() As Boolean
            Get
                Return (Equals(_ordering, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property rubricBlockSpecified() As Boolean
            Get
                Return (Equals(_rubricBlock, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property requiredSpecified() As Boolean
            Get
                Return (Equals(_required, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property fixedSpecified() As Boolean
            Get
                Return (Equals(_fixed, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property titleSpecified() As Boolean
            Get
                Return (Equals(_title, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property visibleSpecified() As Boolean
            Get
                Return (Equals(_visible, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property keepTogetherSpecified() As Boolean
            Get
                Return (Equals(_keepTogether, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Selection.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("selection", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class SelectionType

        Private _any As List(Of System.Xml.XmlElement)

        Private _select As Integer

        Private _withReplacement As Boolean

        Private _anyAttr As List(Of System.Xml.XmlAttribute)

        Public Sub New()
            MyBase.New
            Me._withReplacement = False
        End Sub

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=0)>
        Public Property Any() As List(Of System.Xml.XmlElement)
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property [select]() As Integer
            Get
                Return Me._select
            End Get
            Set
                Me._select = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property withReplacement() As Boolean
            Get
                Return Me._withReplacement
            End Get
            Set
                Me._withReplacement = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As List(Of System.Xml.XmlAttribute)
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property

        Public ReadOnly Property AnySpecified() As Boolean
            Get
                Return (Equals(_any, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property selectSpecified() As Boolean
            Get
                Return (Equals(_select, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property withReplacementSpecified() As Boolean
            Get
                Return (Equals(_withReplacement, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property AnyAttrSpecified() As Boolean
            Get
                Return (Equals(_anyAttr, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Ordering.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("ordering", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class OrderingType

        Private _any As List(Of System.Xml.XmlElement)

        Private _shuffle As Boolean

        Private _anyAttr As List(Of System.Xml.XmlAttribute)

        Public Sub New()
            MyBase.New
            Me._shuffle = False
        End Sub

        <System.Xml.Serialization.XmlAnyElementAttribute(Order:=0)>
        Public Property Any() As List(Of System.Xml.XmlElement)
            Get
                Return Me._any
            End Get
            Set
                Me._any = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property shuffle() As Boolean
            Get
                Return Me._shuffle
            End Get
            Set
                Me._shuffle = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAnyAttributeAttribute()>
        Public Property AnyAttr() As List(Of System.Xml.XmlAttribute)
            Get
                Return Me._anyAttr
            End Get
            Set
                Me._anyAttr = Value
            End Set
        End Property

        Public ReadOnly Property AnySpecified() As Boolean
            Get
                Return (Equals(_any, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property shuffleSpecified() As Boolean
            Get
                Return (Equals(_shuffle, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property AnyAttrSpecified() As Boolean
            Get
                Return (Equals(_anyAttr, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="AssessmentItemRef.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("assessmentItemRef", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class AssessmentItemRefType

        Private _preCondition As List(Of LogicSingleType)

        Private _branchRule As List(Of BranchRuleType)

        Private _itemSessionControl As ItemSessionControlType

        Private _timeLimits As TimeLimitsType

        Private _variableMapping As List(Of VariableMappingType)

        Private _weight As List(Of WeightType)

        Private _templateDefault As List(Of TemplateDefaultType)

        Private _identifier As String

        Private _required As Boolean

        Private _fixed As Boolean

        Private _href As String

        Private _category As List(Of String)

        Public Sub New()
            MyBase.New
            Me._required = False
            Me._fixed = False
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("preCondition", Order:=0)>
        Public Property preCondition() As List(Of LogicSingleType)
            Get
                Return Me._preCondition
            End Get
            Set
                Me._preCondition = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("branchRule", Order:=1)>
        Public Property branchRule() As List(Of BranchRuleType)
            Get
                Return Me._branchRule
            End Get
            Set
                Me._branchRule = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>
        Public Property itemSessionControl() As ItemSessionControlType
            Get
                Return Me._itemSessionControl
            End Get
            Set
                Me._itemSessionControl = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=3)>
        Public Property timeLimits() As TimeLimitsType
            Get
                Return Me._timeLimits
            End Get
            Set
                Me._timeLimits = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("variableMapping", Order:=4)>
        Public Property variableMapping() As List(Of VariableMappingType)
            Get
                Return Me._variableMapping
            End Get
            Set
                Me._variableMapping = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("weight", Order:=5)>
        Public Property weight() As List(Of WeightType)
            Get
                Return Me._weight
            End Get
            Set
                Me._weight = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("templateDefault", Order:=6)>
        Public Property templateDefault() As List(Of TemplateDefaultType)
            Get
                Return Me._templateDefault
            End Get
            Set
                Me._templateDefault = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property required() As Boolean
            Get
                Return Me._required
            End Get
            Set
                Me._required = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property fixed() As Boolean
            Get
                Return Me._fixed
            End Get
            Set
                Me._fixed = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property href() As String
            Get
                Return Me._href
            End Get
            Set
                Me._href = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property category() As List(Of String)
            Get
                Return Me._category
            End Get
            Set
                Me._category = Value
            End Set
        End Property

        Public ReadOnly Property preConditionSpecified() As Boolean
            Get
                Return (Equals(_preCondition, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property branchRuleSpecified() As Boolean
            Get
                Return (Equals(_branchRule, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property itemSessionControlSpecified() As Boolean
            Get
                Return (Equals(_itemSessionControl, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property timeLimitsSpecified() As Boolean
            Get
                Return (Equals(_timeLimits, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property variableMappingSpecified() As Boolean
            Get
                Return (Equals(_variableMapping, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property weightSpecified() As Boolean
            Get
                Return (Equals(_weight, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateDefaultSpecified() As Boolean
            Get
                Return (Equals(_templateDefault, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property requiredSpecified() As Boolean
            Get
                Return (Equals(_required, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property fixedSpecified() As Boolean
            Get
                Return (Equals(_fixed, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property hrefSpecified() As Boolean
            Get
                Return (Equals(_href, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property categorySpecified() As Boolean
            Get
                Return (Equals(_category, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="VariableMapping.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("variableMapping", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class VariableMappingType
        Inherits EmptyPrimitiveTypeType

        Private _sourceIdentifier As String

        Private _targetIdentifier As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property sourceIdentifier() As String
            Get
                Return Me._sourceIdentifier
            End Get
            Set
                Me._sourceIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property targetIdentifier() As String
            Get
                Return Me._targetIdentifier
            End Get
            Set
                Me._targetIdentifier = Value
            End Set
        End Property

        Public ReadOnly Property sourceIdentifierSpecified() As Boolean
            Get
                Return (Equals(_sourceIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property targetIdentifierSpecified() As Boolean
            Get
                Return (Equals(_targetIdentifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Weight.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("weight", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class WeightType
        Inherits EmptyPrimitiveTypeType

        Private _identifier As String

        Private _value As Double

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property value() As Double
            Get
                Return Me._value
            End Get
            Set
                Me._value = Value
            End Set
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property valueSpecified() As Boolean
            Get
                Return (Equals(_value, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TemplateDefault.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("templateDefault", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TemplateDefaultType

        Private _item As Object

        Private _itemElementName As ItemChoiceType13

        Private _templateIdentifier As String

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType13
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property templateIdentifier() As String
            Get
                Return Me._templateIdentifier
            End Get
            Set
                Me._templateIdentifier = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateIdentifierSpecified() As Boolean
            Get
                Return (Equals(_templateIdentifier, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType13

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="AssessmentSectionRef.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("assessmentSectionRef", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class AssessmentSectionRefType
        Inherits EmptyPrimitiveTypeType

        Private _identifier As String

        Private _href As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property href() As String
            Get
                Return Me._href
            End Get
            Set
                Me._href = Value
            End Set
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property hrefSpecified() As Boolean
            Get
                Return (Equals(_href, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum TestPartTypeNavigationMode

        linear

        nonlinear
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum TestPartTypeSubmissionMode

        individual

        simultaneous
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="AssessmentTest.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("assessmentTest", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class AssessmentTestType

        Private _outcomeDeclaration As List(Of OutcomeDeclarationType)

        Private _timeLimits As TimeLimitsType

        Private _stylesheet As List(Of StyleSheetType)

        Private _testPart As List(Of TestPartType)

        Private _outcomeProcessing As OutcomeProcessingType

        Private _testFeedback As List(Of TestFeedbackType)

        Private _identifier As String

        Private _title As String

        Private _toolName As String

        Private _toolVersion As String

        <System.Xml.Serialization.XmlElementAttribute("outcomeDeclaration", Order:=0)>
        Public Property outcomeDeclaration() As List(Of OutcomeDeclarationType)
            Get
                Return Me._outcomeDeclaration
            End Get
            Set
                Me._outcomeDeclaration = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)>
        Public Property timeLimits() As TimeLimitsType
            Get
                Return Me._timeLimits
            End Get
            Set
                Me._timeLimits = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("stylesheet", Order:=2)>
        Public Property stylesheet() As List(Of StyleSheetType)
            Get
                Return Me._stylesheet
            End Get
            Set
                Me._stylesheet = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("testPart", Order:=3)>
        Public Property testPart() As List(Of TestPartType)
            Get
                Return Me._testPart
            End Get
            Set
                Me._testPart = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=4)>
        Public Property outcomeProcessing() As OutcomeProcessingType
            Get
                Return Me._outcomeProcessing
            End Get
            Set
                Me._outcomeProcessing = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("testFeedback", Order:=5)>
        Public Property testFeedback() As List(Of TestFeedbackType)
            Get
                Return Me._testFeedback
            End Get
            Set
                Me._testFeedback = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property title() As String
            Get
                Return Me._title
            End Get
            Set
                Me._title = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property toolName() As String
            Get
                Return Me._toolName
            End Get
            Set
                Me._toolName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property toolVersion() As String
            Get
                Return Me._toolVersion
            End Get
            Set
                Me._toolVersion = Value
            End Set
        End Property

        Public ReadOnly Property outcomeDeclarationSpecified() As Boolean
            Get
                Return (Equals(_outcomeDeclaration, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property timeLimitsSpecified() As Boolean
            Get
                Return (Equals(_timeLimits, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stylesheetSpecified() As Boolean
            Get
                Return (Equals(_stylesheet, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property testPartSpecified() As Boolean
            Get
                Return (Equals(_testPart, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property outcomeProcessingSpecified() As Boolean
            Get
                Return (Equals(_outcomeProcessing, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property testFeedbackSpecified() As Boolean
            Get
                Return (Equals(_testFeedback, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property titleSpecified() As Boolean
            Get
                Return (Equals(_title, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property toolNameSpecified() As Boolean
            Get
                Return (Equals(_toolName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property toolVersionSpecified() As Boolean
            Get
                Return (Equals(_toolVersion, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="OutcomeDeclaration.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("outcomeDeclaration", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class OutcomeDeclarationType

        Private _defaultValue As DefaultValueType

        Private _item As Object

        Private _identifier As String

        Private _cardinality As OutcomeDeclarationTypeCardinality

        Private _baseType As OutcomeDeclarationTypeBaseType

        Private baseTypeFieldSpecified As Boolean

        Private _view As List(Of ViewType)

        Private _interpretation As String

        Private _longInterpretation As String

        Private _normalMaximum As Double

        Private normalMaximumFieldSpecified As Boolean

        Private _normalMinimum As Double

        Private normalMinimumFieldSpecified As Boolean

        Private _masteryValue As Double

        Private masteryValueFieldSpecified As Boolean

        Private _externalScored As OutcomeDeclarationTypeExternalScored

        Private externalScoredFieldSpecified As Boolean

        Private _variableIdentifierRef As String

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property defaultValue() As DefaultValueType
            Get
                Return Me._defaultValue
            End Get
            Set
                Me._defaultValue = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("interpolationTable", GetType(InterpolationTableType), Order:=1),
            System.Xml.Serialization.XmlElementAttribute("matchTable", GetType(MatchTableType), Order:=1)>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property cardinality() As OutcomeDeclarationTypeCardinality
            Get
                Return Me._cardinality
            End Get
            Set
                Me._cardinality = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property baseType() As OutcomeDeclarationTypeBaseType
            Get
                Return Me._baseType
            End Get
            Set
                Me._baseType = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property baseTypeSpecified() As Boolean
            Get
                Return Me.baseTypeFieldSpecified
            End Get
            Set
                Me.baseTypeFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property view() As List(Of ViewType)
            Get
                Return Me._view
            End Get
            Set
                Me._view = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property interpretation() As String
            Get
                Return Me._interpretation
            End Get
            Set
                Me._interpretation = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property longInterpretation() As String
            Get
                Return Me._longInterpretation
            End Get
            Set
                Me._longInterpretation = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property normalMaximum() As Double
            Get
                Return Me._normalMaximum
            End Get
            Set
                Me._normalMaximum = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property normalMaximumSpecified() As Boolean
            Get
                Return Me.normalMaximumFieldSpecified
            End Get
            Set
                Me.normalMaximumFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property normalMinimum() As Double
            Get
                Return Me._normalMinimum
            End Get
            Set
                Me._normalMinimum = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property normalMinimumSpecified() As Boolean
            Get
                Return Me.normalMinimumFieldSpecified
            End Get
            Set
                Me.normalMinimumFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property masteryValue() As Double
            Get
                Return Me._masteryValue
            End Get
            Set
                Me._masteryValue = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property masteryValueSpecified() As Boolean
            Get
                Return Me.masteryValueFieldSpecified
            End Get
            Set
                Me.masteryValueFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property externalScored() As OutcomeDeclarationTypeExternalScored
            Get
                Return Me._externalScored
            End Get
            Set
                Me._externalScored = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property externalScoredSpecified() As Boolean
            Get
                Return Me.externalScoredFieldSpecified
            End Get
            Set
                Me.externalScoredFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="IDREF")>
        Public Property variableIdentifierRef() As String
            Get
                Return Me._variableIdentifierRef
            End Get
            Set
                Me._variableIdentifierRef = Value
            End Set
        End Property

        Public ReadOnly Property defaultValueSpecified() As Boolean
            Get
                Return (Equals(_defaultValue, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property cardinalitySpecified() As Boolean
            Get
                Return (Equals(_cardinality, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property viewSpecified() As Boolean
            Get
                Return (Equals(_view, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property interpretationSpecified() As Boolean
            Get
                Return (Equals(_interpretation, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property longInterpretationSpecified() As Boolean
            Get
                Return (Equals(_longInterpretation, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property variableIdentifierRefSpecified() As Boolean
            Get
                Return (Equals(_variableIdentifierRef, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="DefaultValue.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("defaultValue", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class DefaultValueType

        Private _value As List(Of ValueType)

        Private _interpretation As String

        <System.Xml.Serialization.XmlElementAttribute("value", Order:=0)>
        Public Property value() As List(Of ValueType)
            Get
                Return Me._value
            End Get
            Set
                Me._value = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property interpretation() As String
            Get
                Return Me._interpretation
            End Get
            Set
                Me._interpretation = Value
            End Set
        End Property

        Public ReadOnly Property valueSpecified() As Boolean
            Get
                Return (Equals(_value, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property interpretationSpecified() As Boolean
            Get
                Return (Equals(_interpretation, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Value.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("value", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ValueType

        Private _fieldIdentifier As String

        Private _baseType As ValueTypeBaseType

        Private baseTypeFieldSpecified As Boolean

        Private _value As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property fieldIdentifier() As String
            Get
                Return Me._fieldIdentifier
            End Get
            Set
                Me._fieldIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property baseType() As ValueTypeBaseType
            Get
                Return Me._baseType
            End Get
            Set
                Me._baseType = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property baseTypeSpecified() As Boolean
            Get
                Return Me.baseTypeFieldSpecified
            End Get
            Set
                Me.baseTypeFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute(DataType:="normalizedString")>
        Public Property Value() As String
            Get
                Return Me._value
            End Get
            Set
                Me._value = Value
            End Set
        End Property

        Public ReadOnly Property fieldIdentifierSpecified() As Boolean
            Get
                Return (Equals(_fieldIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ValueSpecified() As Boolean
            Get
                Return (Equals(_value, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum ValueTypeBaseType

        [boolean]

        directedPair

        duration

        file

        float

        identifier

        [integer]

        pair

        point

        [string]

        uri
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="InterpolationTable.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("interpolationTable", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class InterpolationTableType

        Private _interpolationTableEntry As List(Of InterpolationTableEntryType)

        Private _defaultValue As String

        <System.Xml.Serialization.XmlElementAttribute("interpolationTableEntry", Order:=0)>
        Public Property interpolationTableEntry() As List(Of InterpolationTableEntryType)
            Get
                Return Me._interpolationTableEntry
            End Get
            Set
                Me._interpolationTableEntry = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property defaultValue() As String
            Get
                Return Me._defaultValue
            End Get
            Set
                Me._defaultValue = Value
            End Set
        End Property

        Public ReadOnly Property interpolationTableEntrySpecified() As Boolean
            Get
                Return (Equals(_interpolationTableEntry, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property defaultValueSpecified() As Boolean
            Get
                Return (Equals(_defaultValue, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="InterpolationTableEntry.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("interpolationTableEntry", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class InterpolationTableEntryType
        Inherits EmptyPrimitiveTypeType

        Private _sourceValue As Double

        Private _includeBoundary As Boolean

        Private _targetValue As String

        Public Sub New()
            MyBase.New
            Me._includeBoundary = True
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property sourceValue() As Double
            Get
                Return Me._sourceValue
            End Get
            Set
                Me._sourceValue = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(True)>
        Public Property includeBoundary() As Boolean
            Get
                Return Me._includeBoundary
            End Get
            Set
                Me._includeBoundary = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property targetValue() As String
            Get
                Return Me._targetValue
            End Get
            Set
                Me._targetValue = Value
            End Set
        End Property

        Public ReadOnly Property sourceValueSpecified() As Boolean
            Get
                Return (Equals(_sourceValue, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property includeBoundarySpecified() As Boolean
            Get
                Return (Equals(_includeBoundary, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property targetValueSpecified() As Boolean
            Get
                Return (Equals(_targetValue, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="MatchTable.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("matchTable", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class MatchTableType

        Private _matchTableEntry As List(Of MatchTableEntryType)

        Private _defaultValue As String

        <System.Xml.Serialization.XmlElementAttribute("matchTableEntry", Order:=0)>
        Public Property matchTableEntry() As List(Of MatchTableEntryType)
            Get
                Return Me._matchTableEntry
            End Get
            Set
                Me._matchTableEntry = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property defaultValue() As String
            Get
                Return Me._defaultValue
            End Get
            Set
                Me._defaultValue = Value
            End Set
        End Property

        Public ReadOnly Property matchTableEntrySpecified() As Boolean
            Get
                Return (Equals(_matchTableEntry, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property defaultValueSpecified() As Boolean
            Get
                Return (Equals(_defaultValue, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="MatchTableEntry.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("matchTableEntry", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class MatchTableEntryType
        Inherits EmptyPrimitiveTypeType

        Private _sourceValue As Integer

        Private _targetValue As MatchTableEntryTypeTargetValue

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property sourceValue() As Integer
            Get
                Return Me._sourceValue
            End Get
            Set
                Me._sourceValue = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property targetValue() As MatchTableEntryTypeTargetValue
            Get
                Return Me._targetValue
            End Get
            Set
                Me._targetValue = Value
            End Set
        End Property

        Public ReadOnly Property sourceValueSpecified() As Boolean
            Get
                Return (Equals(_sourceValue, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property targetValueSpecified() As Boolean
            Get
                Return (Equals(_targetValue, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum MatchTableEntryTypeTargetValue

        [boolean]

        directedPair

        duration

        file

        float

        identifier

        [integer]

        pair

        point

        [string]

        uri
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum OutcomeDeclarationTypeCardinality

        multiple

        ordered

        record

        [single]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum OutcomeDeclarationTypeBaseType

        [boolean]

        directedPair

        duration

        file

        float

        identifier

        [integer]

        pair

        point

        [string]

        uri
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum OutcomeDeclarationTypeExternalScored

        externalMachine

        human
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="AssessmentStimulus.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("assessmentStimulus", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class AssessmentStimulusType

        Private _stylesheet As List(Of StyleSheetType)

        Private _stimulusBody As StimulusBodyType

        Private _object As ObjectType

        Private _identifier As String

        Private _title As String

        Private _label As String

        Private _lang As String

        Private _toolName As String

        Private _toolVersion As String

        <System.Xml.Serialization.XmlElementAttribute("stylesheet", Order:=0)>
        Public Property stylesheet() As List(Of StyleSheetType)
            Get
                Return Me._stylesheet
            End Get
            Set
                Me._stylesheet = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)>
        Public Property stimulusBody() As StimulusBodyType
            Get
                Return Me._stimulusBody
            End Get
            Set
                Me._stimulusBody = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>
        Public Property [object]() As ObjectType
            Get
                Return Me._object
            End Get
            Set
                Me._object = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property title() As String
            Get
                Return Me._title
            End Get
            Set
                Me._title = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property label() As String
            Get
                Return Me._label
            End Get
            Set
                Me._label = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property lang() As String
            Get
                Return Me._lang
            End Get
            Set
                Me._lang = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property toolName() As String
            Get
                Return Me._toolName
            End Get
            Set
                Me._toolName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property toolVersion() As String
            Get
                Return Me._toolVersion
            End Get
            Set
                Me._toolVersion = Value
            End Set
        End Property

        Public ReadOnly Property stylesheetSpecified() As Boolean
            Get
                Return (Equals(_stylesheet, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stimulusBodySpecified() As Boolean
            Get
                Return (Equals(_stimulusBody, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property objectSpecified() As Boolean
            Get
                Return (Equals(_object, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property titleSpecified() As Boolean
            Get
                Return (Equals(_title, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property labelSpecified() As Boolean
            Get
                Return (Equals(_label, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property langSpecified() As Boolean
            Get
                Return (Equals(_lang, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property toolNameSpecified() As Boolean
            Get
                Return (Equals(_toolName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property toolVersionSpecified() As Boolean
            Get
                Return (Equals(_toolVersion, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="ModalFeedback.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("modalFeedback", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ModalFeedbackType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType44

        Private _stylesheet As List(Of StyleSheetType)

        Private _text As List(Of String)

        Private _outcomeIdentifier As String

        Private _showHide As ModalFeedbackTypeShowHide

        Private _identifier As String

        Private _title As String

        <System.Xml.Serialization.XmlElementAttribute("a", GetType(AType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("abbr", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("acronym", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("address", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("b", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("bdo", GetType(BDOType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("big", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("blockquote", GetType(BlockQuoteType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("br", GetType(BRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("cite", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("code", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dfn", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("div", GetType(DivType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("dl", GetType(DLType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("em", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h1", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h2", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h3", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h4", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h5", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("h6", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hottext", GetType(HotTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("hr", GetType(HRType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("i", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("img", GetType(ImgType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("kbd", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ol", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("p", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("pre", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("printedVariable", GetType(PrintedVariableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("q", GetType(QType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("samp", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("small", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("span", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("strong", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sub", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sup", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("table", GetType(TableType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateBlock", GetType(TemplateBlockType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateInline", GetType(TemplateInlineType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("tt", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ul", GetType(OULType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("var", GetType(HTMLTextType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType44()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("stylesheet", Order:=2)>
        Public Property stylesheet() As List(Of StyleSheetType)
            Get
                Return Me._stylesheet
            End Get
            Set
                Me._stylesheet = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlTextAttribute()>
        Public Property Text() As List(Of String)
            Get
                Return Me._text
            End Get
            Set
                Me._text = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property outcomeIdentifier() As String
            Get
                Return Me._outcomeIdentifier
            End Get
            Set
                Me._outcomeIdentifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property showHide() As ModalFeedbackTypeShowHide
            Get
                Return Me._showHide
            End Get
            Set
                Me._showHide = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property title() As String
            Get
                Return Me._title
            End Get
            Set
                Me._title = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stylesheetSpecified() As Boolean
            Get
                Return (Equals(_stylesheet, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property TextSpecified() As Boolean
            Get
                Return (Equals(_text, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property outcomeIdentifierSpecified() As Boolean
            Get
                Return (Equals(_outcomeIdentifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property showHideSpecified() As Boolean
            Get
                Return (Equals(_showHide, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property titleSpecified() As Boolean
            Get
                Return (Equals(_title, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType44

        a

        abbr

        acronym

        address

        b

        bdo

        big

        blockquote

        br

        cite

        code

        dfn

        div

        dl

        em

        h1

        h2

        h3

        h4

        h5

        h6

        hottext

        hr

        i

        img

        kbd

        [object]

        ol

        p

        pre

        printedVariable

        q

        samp

        small

        span

        strong

        [sub]

        sup

        table

        templateBlock

        templateInline

        tt

        ul

        var
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum ModalFeedbackTypeShowHide

        show

        hide
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="ResponseElse.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("responseElse", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ResponseElseType

        Private _items As List(Of Object)

        <System.Xml.Serialization.XmlElementAttribute("exitResponse", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lookupOutcomeValue", GetType(LookupOutcomeValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("responseCondition", GetType(ResponseConditionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("responseProcessingFragment", GetType(ResponseProcessingFragmentType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("setOutcomeValue", GetType(SetValueType), Order:=0)>
        Public Property Items() As List(Of Object)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="ResponseCondition.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("responseCondition", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ResponseConditionType

        Private _responseIf As ResponseIfType

        Private _responseElseIf As List(Of ResponseIfType)

        Private _responseElse As ResponseElseType

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property responseIf() As ResponseIfType
            Get
                Return Me._responseIf
            End Get
            Set
                Me._responseIf = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("responseElseIf", Order:=1)>
        Public Property responseElseIf() As List(Of ResponseIfType)
            Get
                Return Me._responseElseIf
            End Get
            Set
                Me._responseElseIf = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>
        Public Property responseElse() As ResponseElseType
            Get
                Return Me._responseElse
            End Get
            Set
                Me._responseElse = Value
            End Set
        End Property

        Public ReadOnly Property responseIfSpecified() As Boolean
            Get
                Return (Equals(_responseIf, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property responseElseIfSpecified() As Boolean
            Get
                Return (Equals(_responseElseIf, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property responseElseSpecified() As Boolean
            Get
                Return (Equals(_responseElse, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="ResponseIf.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("responseElseIf", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ResponseIfType

        Private _item As Object

        Private _itemElementName As ItemChoiceType10

        Private _items As List(Of Object)

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType10
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("exitResponse", GetType(EmptyPrimitiveTypeType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("lookupOutcomeValue", GetType(LookupOutcomeValueType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("responseCondition", GetType(ResponseConditionType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("responseProcessingFragment", GetType(ResponseProcessingFragmentType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("setOutcomeValue", GetType(SetValueType), Order:=2)>
        Public Property Items() As List(Of Object)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType10

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="ResponseProcessingFragment.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("responseProcessingFragment", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ResponseProcessingFragmentType

        Private _items As List(Of Object)

        <System.Xml.Serialization.XmlElementAttribute("exitResponse", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lookupOutcomeValue", GetType(LookupOutcomeValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("responseCondition", GetType(ResponseConditionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("responseProcessingFragment", GetType(ResponseProcessingFragmentType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("setOutcomeValue", GetType(SetValueType), Order:=0)>
        Public Property Items() As List(Of Object)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="ResponseProcessing.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("responseProcessing", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ResponseProcessingType

        Private _items As List(Of Object)

        Private _template As String

        Private _templateLocation As String

        <System.Xml.Serialization.XmlElementAttribute("exitResponse", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lookupOutcomeValue", GetType(LookupOutcomeValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("object", GetType(ObjectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("responseCondition", GetType(ResponseConditionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("responseProcessingFragment", GetType(ResponseProcessingFragmentType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("setOutcomeValue", GetType(SetValueType), Order:=0)>
        Public Property Items() As List(Of Object)
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property template() As String
            Get
                Return Me._template
            End Get
            Set
                Me._template = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property templateLocation() As String
            Get
                Return Me._templateLocation
            End Get
            Set
                Me._templateLocation = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateSpecified() As Boolean
            Get
                Return (Equals(_template, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateLocationSpecified() As Boolean
            Get
                Return (Equals(_templateLocation, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="ItemBody.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("itemBody", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ItemBodyType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType43

        Private _id As String

        Private _class As List(Of String)

        Private _lang As String

        Private _label As String

        Private _dir As ItemBodyTypeDir

        Public Sub New()
            MyBase.New
            Me._dir = ItemBodyTypeDir.[auto]
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType43()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property id() As String
            Get
                Return Me._id
            End Get
            Set
                Me._id = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property [class]() As List(Of String)
            Get
                Return Me._class
            End Get
            Set
                Me._class = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property lang() As String
            Get
                Return Me._lang
            End Get
            Set
                Me._lang = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property label() As String
            Get
                Return Me._label
            End Get
            Set
                Me._label = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(ItemBodyTypeDir.[auto])>
        Public Property dir() As ItemBodyTypeDir
            Get
                Return Me._dir
            End Get
            Set
                Me._dir = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property idSpecified() As Boolean
            Get
                Return (Equals(_id, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property classSpecified() As Boolean
            Get
                Return (Equals(_class, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property langSpecified() As Boolean
            Get
                Return (Equals(_lang, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property labelSpecified() As Boolean
            Get
                Return (Equals(_label, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property dirSpecified() As Boolean
            Get
                Return (Equals(_dir, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType43

        address

        associateInteraction

        blockquote

        choiceInteraction

        customInteraction

        div

        dl

        drawingInteraction

        extendedTextInteraction

        feedbackBlock

        gapMatchInteraction

        graphicAssociateInteraction

        graphicGapMatchInteraction

        graphicOrderInteraction

        h1

        h2

        h3

        h4

        h5

        h6

        hotspotInteraction

        hottextInteraction

        hr

        infoControl

        matchInteraction

        mediaInteraction

        [object]

        ol

        orderInteraction

        p

        positionObjectStage

        pre

        rubricBlock

        selectPointInteraction

        sliderInteraction

        table

        templateBlock

        ul

        uploadInteraction
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum ItemBodyTypeDir

        ltr

        rtl

        [auto]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TemplateElse.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("templateElse", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TemplateElseType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType13

        <System.Xml.Serialization.XmlElementAttribute("exitTemplate", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("setCorrectResponse", GetType(SetValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("setDefaultValue", GetType(SetValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("setTemplateValue", GetType(SetValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateCondition", GetType(TemplateConditionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateConstraint", GetType(TemplateConstraintType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType13()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TemplateCondition.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("templateCondition", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TemplateConditionType

        Private _templateIf As TemplateIfType

        Private _templateElseIf As List(Of TemplateIfType)

        Private _templateElse As TemplateElseType

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property templateIf() As TemplateIfType
            Get
                Return Me._templateIf
            End Get
            Set
                Me._templateIf = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("templateElseIf", Order:=1)>
        Public Property templateElseIf() As List(Of TemplateIfType)
            Get
                Return Me._templateElseIf
            End Get
            Set
                Me._templateElseIf = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>
        Public Property templateElse() As TemplateElseType
            Get
                Return Me._templateElse
            End Get
            Set
                Me._templateElse = Value
            End Set
        End Property

        Public ReadOnly Property templateIfSpecified() As Boolean
            Get
                Return (Equals(_templateIf, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateElseIfSpecified() As Boolean
            Get
                Return (Equals(_templateElseIf, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateElseSpecified() As Boolean
            Get
                Return (Equals(_templateElse, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TemplateIf.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("templateElseIf", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TemplateIfType

        Private _item As Object

        Private _itemElementName As ItemChoiceType8

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType12

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType8
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("exitTemplate", GetType(EmptyPrimitiveTypeType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("setCorrectResponse", GetType(SetValueType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("setDefaultValue", GetType(SetValueType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("setTemplateValue", GetType(SetValueType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("templateCondition", GetType(TemplateConditionType), Order:=2),
            System.Xml.Serialization.XmlElementAttribute("templateConstraint", GetType(TemplateConstraintType), Order:=2),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=3),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType12()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType8

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TemplateConstraint.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("templateConstraint", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TemplateConstraintType

        Private _item As Object

        Private _itemElementName As ItemChoiceType9

        <System.Xml.Serialization.XmlElementAttribute("and", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("anyN", GetType(AnyNType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("baseValue", GetType(BaseValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("containerSize", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("contains", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("correct", GetType(CorrectType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("customOperator", GetType(CustomOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("default", GetType(DefaultType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("delete", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("divide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationGTE", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("durationLT", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equal", GetType(EqualType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("equalRounded", GetType(EqualRoundedType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("fieldValue", GetType(FieldValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gcd", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("gte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("index", GetType(IndexType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("inside", GetType(InsideType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerDivide", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerModulus", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("integerToFloat", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("isNull", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lcm", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lt", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("lte", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponse", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mapResponsePoint", GetType(MapResponseType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("match", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathConstant", GetType(MathConstantType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("mathOperator", GetType(MathOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("max", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("member", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("min", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("multiple", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("not", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("null", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberCorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberIncorrect", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberPresented", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberResponded", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("numberSelected", GetType(NumberType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("or", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("ordered", GetType(Logic0toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMaximum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("outcomeMinimum", GetType(OutcomeMinMaxType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("patternMatch", GetType(PatternMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("power", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("product", GetType(Logic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("random", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomFloat", GetType(RandomFloatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("randomInteger", GetType(RandomIntegerType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("repeat", GetType(RepeatType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("round", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("roundTo", GetType(RoundToType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("statsOperator", GetType(StatsOperatorType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("stringMatch", GetType(StringMatchType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("substring", GetType(SubstringType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("subtract", GetType(LogicPairType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("sum", GetType(NumericLogic1toManyType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("testVariables", GetType(TestVariablesType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("truncate", GetType(LogicSingleType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("variable", GetType(VariableType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")>
        Public Property Item() As Object
            Get
                Return Me._item
            End Get
            Set
                Me._item = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemElementName() As ItemChoiceType9
            Get
                Return Me._itemElementName
            End Get
            Set
                Me._itemElementName = Value
            End Set
        End Property

        Public ReadOnly Property ItemSpecified() As Boolean
            Get
                Return (Equals(_item, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemElementName, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemChoiceType9

        [and]

        anyN

        baseValue

        containerSize

        contains

        correct

        customOperator

        [default]

        delete

        divide

        durationGTE

        durationLT

        equal

        equalRounded

        fieldValue

        gcd

        gt

        gte

        index

        inside

        integerDivide

        integerModulus

        integerToFloat

        isNull

        lcm

        lt

        lte

        mapResponse

        mapResponsePoint

        match

        mathConstant

        mathOperator

        max

        member

        min

        multiple

        [not]

        null

        numberCorrect

        numberIncorrect

        numberPresented

        numberResponded

        numberSelected

        [or]

        ordered

        outcomeMaximum

        outcomeMinimum

        patternMatch

        power

        product

        random

        randomFloat

        randomInteger

        repeat

        round

        roundTo

        statsOperator

        stringMatch

        substring

        subtract

        sum

        testVariables

        truncate

        variable
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType12

        exitTemplate

        setCorrectResponse

        setDefaultValue

        setTemplateValue

        templateCondition

        templateConstraint
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType13

        exitTemplate

        setCorrectResponse

        setDefaultValue

        setTemplateValue

        templateCondition

        templateConstraint
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TemplateProcessing.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("templateProcessing", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TemplateProcessingType

        Private _items() As Object

        Private _itemsElementName() As ItemsChoiceType14

        <System.Xml.Serialization.XmlElementAttribute("exitTemplate", GetType(EmptyPrimitiveTypeType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("setCorrectResponse", GetType(SetValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("setDefaultValue", GetType(SetValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("setTemplateValue", GetType(SetValueType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateCondition", GetType(TemplateConditionType), Order:=0),
            System.Xml.Serialization.XmlElementAttribute("templateConstraint", GetType(TemplateConstraintType), Order:=0),
            System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")>
        Public Property Items() As Object()
            Get
                Return Me._items
            End Get
            Set
                Me._items = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("ItemsElementName", Order:=1),
            System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property ItemsElementName() As ItemsChoiceType14()
            Get
                Return Me._itemsElementName
            End Get
            Set
                Me._itemsElementName = Value
            End Set
        End Property

        Public ReadOnly Property ItemsSpecified() As Boolean
            Get
                Return (Equals(_items, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property ItemsElementNameSpecified() As Boolean
            Get
                Return (Equals(_itemsElementName, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IncludeInSchema:=False)>
    Public Enum ItemsChoiceType14

        exitTemplate

        setCorrectResponse

        setDefaultValue

        setTemplateValue

        templateCondition

        templateConstraint
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="TemplateDeclaration.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("templateDeclaration", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class TemplateDeclarationType

        Private _defaultValue As DefaultValueType

        Private _identifier As String

        Private _cardinality As TemplateDeclarationTypeCardinality

        Private _baseType As TemplateDeclarationTypeBaseType

        Private baseTypeFieldSpecified As Boolean

        Private _paramVariable As Boolean

        Private _mathVariable As Boolean

        Public Sub New()
            MyBase.New
            Me._paramVariable = False
            Me._mathVariable = False
        End Sub

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property defaultValue() As DefaultValueType
            Get
                Return Me._defaultValue
            End Get
            Set
                Me._defaultValue = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="NCName")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property cardinality() As TemplateDeclarationTypeCardinality
            Get
                Return Me._cardinality
            End Get
            Set
                Me._cardinality = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property baseType() As TemplateDeclarationTypeBaseType
            Get
                Return Me._baseType
            End Get
            Set
                Me._baseType = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property baseTypeSpecified() As Boolean
            Get
                Return Me.baseTypeFieldSpecified
            End Get
            Set
                Me.baseTypeFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property paramVariable() As Boolean
            Get
                Return Me._paramVariable
            End Get
            Set
                Me._paramVariable = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property mathVariable() As Boolean
            Get
                Return Me._mathVariable
            End Get
            Set
                Me._mathVariable = Value
            End Set
        End Property

        Public ReadOnly Property defaultValueSpecified() As Boolean
            Get
                Return (Equals(_defaultValue, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property cardinalitySpecified() As Boolean
            Get
                Return (Equals(_cardinality, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property paramVariableSpecified() As Boolean
            Get
                Return (Equals(_paramVariable, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property mathVariableSpecified() As Boolean
            Get
                Return (Equals(_mathVariable, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum TemplateDeclarationTypeCardinality

        multiple

        ordered

        record

        [single]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum TemplateDeclarationTypeBaseType

        [boolean]

        directedPair

        duration

        file

        float

        identifier

        [integer]

        pair

        point

        [string]

        uri
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="Mapping.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("mapping", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class MappingType

        Private _mapEntry As List(Of MapEntryType)

        Private _lowerBound As Double

        Private lowerBoundFieldSpecified As Boolean

        Private _upperBound As Double

        Private upperBoundFieldSpecified As Boolean

        Private _defaultValue As Double

        Public Sub New()
            MyBase.New
            Me._defaultValue = 0R
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("mapEntry", Order:=0)>
        Public Property mapEntry() As List(Of MapEntryType)
            Get
                Return Me._mapEntry
            End Get
            Set
                Me._mapEntry = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property lowerBound() As Double
            Get
                Return Me._lowerBound
            End Get
            Set
                Me._lowerBound = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property lowerBoundSpecified() As Boolean
            Get
                Return Me.lowerBoundFieldSpecified
            End Get
            Set
                Me.lowerBoundFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property upperBound() As Double
            Get
                Return Me._upperBound
            End Get
            Set
                Me._upperBound = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property upperBoundSpecified() As Boolean
            Get
                Return Me.upperBoundFieldSpecified
            End Get
            Set
                Me.upperBoundFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(0R)>
        Public Property defaultValue() As Double
            Get
                Return Me._defaultValue
            End Get
            Set
                Me._defaultValue = Value
            End Set
        End Property

        Public ReadOnly Property mapEntrySpecified() As Boolean
            Get
                Return (Equals(_mapEntry, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property defaultValueSpecified() As Boolean
            Get
                Return (Equals(_defaultValue, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="MapEntry.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("mapEntry", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class MapEntryType
        Inherits EmptyPrimitiveTypeType

        Private _mapKey As String

        Private _mappedValue As Double

        Private _caseSensitive As Boolean

        Public Sub New()
            MyBase.New
            Me._caseSensitive = False
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property mapKey() As String
            Get
                Return Me._mapKey
            End Get
            Set
                Me._mapKey = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property mappedValue() As Double
            Get
                Return Me._mappedValue
            End Get
            Set
                Me._mappedValue = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property caseSensitive() As Boolean
            Get
                Return Me._caseSensitive
            End Get
            Set
                Me._caseSensitive = Value
            End Set
        End Property

        Public ReadOnly Property mapKeySpecified() As Boolean
            Get
                Return (Equals(_mapKey, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property mappedValueSpecified() As Boolean
            Get
                Return (Equals(_mappedValue, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property caseSensitiveSpecified() As Boolean
            Get
                Return (Equals(_caseSensitive, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="CorrectResponse.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("correctResponse", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class CorrectResponseType

        Private _value As List(Of ValueType)

        Private _interpretation As String

        <System.Xml.Serialization.XmlElementAttribute("value", Order:=0)>
        Public Property value() As List(Of ValueType)
            Get
                Return Me._value
            End Get
            Set
                Me._value = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property interpretation() As String
            Get
                Return Me._interpretation
            End Get
            Set
                Me._interpretation = Value
            End Set
        End Property

        Public ReadOnly Property valueSpecified() As Boolean
            Get
                Return (Equals(_value, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property interpretationSpecified() As Boolean
            Get
                Return (Equals(_interpretation, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="ResponseDeclaration.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("responseDeclaration", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class ResponseDeclarationType

        Private _defaultValue As DefaultValueType

        Private _correctResponse As CorrectResponseType

        Private _mapping As MappingType

        Private _areaMapping As AreaMappingType

        Private _identifier As String

        Private _cardinality As ResponseDeclarationTypeCardinality

        Private _baseType As ResponseDeclarationTypeBaseType

        Private baseTypeFieldSpecified As Boolean

        <System.Xml.Serialization.XmlElementAttribute(Order:=0)>
        Public Property defaultValue() As DefaultValueType
            Get
                Return Me._defaultValue
            End Get
            Set
                Me._defaultValue = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=1)>
        Public Property correctResponse() As CorrectResponseType
            Get
                Return Me._correctResponse
            End Get
            Set
                Me._correctResponse = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=2)>
        Public Property mapping() As MappingType
            Get
                Return Me._mapping
            End Get
            Set
                Me._mapping = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=3)>
        Public Property areaMapping() As AreaMappingType
            Get
                Return Me._areaMapping
            End Get
            Set
                Me._areaMapping = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="ID")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property cardinality() As ResponseDeclarationTypeCardinality
            Get
                Return Me._cardinality
            End Get
            Set
                Me._cardinality = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property baseType() As ResponseDeclarationTypeBaseType
            Get
                Return Me._baseType
            End Get
            Set
                Me._baseType = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property baseTypeSpecified() As Boolean
            Get
                Return Me.baseTypeFieldSpecified
            End Get
            Set
                Me.baseTypeFieldSpecified = Value
            End Set
        End Property

        Public ReadOnly Property defaultValueSpecified() As Boolean
            Get
                Return (Equals(_defaultValue, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property correctResponseSpecified() As Boolean
            Get
                Return (Equals(_correctResponse, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property mappingSpecified() As Boolean
            Get
                Return (Equals(_mapping, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property areaMappingSpecified() As Boolean
            Get
                Return (Equals(_areaMapping, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property cardinalitySpecified() As Boolean
            Get
                Return (Equals(_cardinality, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="AreaMapping.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("areaMapping", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class AreaMappingType

        Private _areaMapEntry As List(Of AreaMapEntryType)

        Private _lowerBound As Double

        Private lowerBoundFieldSpecified As Boolean

        Private _upperBound As Double

        Private upperBoundFieldSpecified As Boolean

        Private _defaultValue As Double

        Public Sub New()
            MyBase.New
            Me._defaultValue = 0R
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("areaMapEntry", Order:=0)>
        Public Property areaMapEntry() As List(Of AreaMapEntryType)
            Get
                Return Me._areaMapEntry
            End Get
            Set
                Me._areaMapEntry = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property lowerBound() As Double
            Get
                Return Me._lowerBound
            End Get
            Set
                Me._lowerBound = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property lowerBoundSpecified() As Boolean
            Get
                Return Me.lowerBoundFieldSpecified
            End Get
            Set
                Me.lowerBoundFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property upperBound() As Double
            Get
                Return Me._upperBound
            End Get
            Set
                Me._upperBound = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlIgnoreAttribute()>
        Public Property upperBoundSpecified() As Boolean
            Get
                Return Me.upperBoundFieldSpecified
            End Get
            Set
                Me.upperBoundFieldSpecified = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(0R)>
        Public Property defaultValue() As Double
            Get
                Return Me._defaultValue
            End Get
            Set
                Me._defaultValue = Value
            End Set
        End Property

        Public ReadOnly Property areaMapEntrySpecified() As Boolean
            Get
                Return (Equals(_areaMapEntry, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property defaultValueSpecified() As Boolean
            Get
                Return (Equals(_defaultValue, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="AreaMapEntry.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("areaMapEntry", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class AreaMapEntryType
        Inherits EmptyPrimitiveTypeType

        Private _shape As AreaMapEntryTypeShape

        Private _coords As String

        Private _mappedValue As Double

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property shape() As AreaMapEntryTypeShape
            Get
                Return Me._shape
            End Get
            Set
                Me._shape = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property coords() As String
            Get
                Return Me._coords
            End Get
            Set
                Me._coords = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property mappedValue() As Double
            Get
                Return Me._mappedValue
            End Get
            Set
                Me._mappedValue = Value
            End Set
        End Property

        Public ReadOnly Property shapeSpecified() As Boolean
            Get
                Return (Equals(_shape, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property coordsSpecified() As Boolean
            Get
                Return (Equals(_coords, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property mappedValueSpecified() As Boolean
            Get
                Return (Equals(_mappedValue, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum AreaMapEntryTypeShape

        circle

        [default]

        ellipse

        poly

        rect
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum ResponseDeclarationTypeCardinality

        multiple

        ordered

        record

        [single]
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2")>
    Public Enum ResponseDeclarationTypeBaseType

        [boolean]

        directedPair

        duration

        file

        float

        identifier

        [integer]

        pair

        point

        [string]

        uri
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="AssessmentItem.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("assessmentItem", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class AssessmentItemType

        Private _responseDeclaration As List(Of ResponseDeclarationType)

        Private _outcomeDeclaration As List(Of OutcomeDeclarationType)

        Private _templateDeclaration As List(Of TemplateDeclarationType)

        Private _templateProcessing As TemplateProcessingType

        Private _assessmentStimulusRef As List(Of AssessmentStimulusRefType)

        Private _stylesheet As List(Of StyleSheetType)

        Private _itemBody As ItemBodyType

        Private _responseProcessing As ResponseProcessingType

        Private _modalFeedback As List(Of ModalFeedbackType)

        Private _object As ObjectType

        Private _identifier As String

        Private _title As String

        Private _label As String

        Private _lang As String

        Private _toolName As String

        Private _toolVersion As String

        Private _adaptive As Boolean

        Private _timeDependent As Boolean

        Public Sub New()
            MyBase.New
            Me._adaptive = False
        End Sub

        <System.Xml.Serialization.XmlElementAttribute("responseDeclaration", Order:=0)>
        Public Property responseDeclaration() As List(Of ResponseDeclarationType)
            Get
                Return Me._responseDeclaration
            End Get
            Set
                Me._responseDeclaration = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("outcomeDeclaration", Order:=1)>
        Public Property outcomeDeclaration() As List(Of OutcomeDeclarationType)
            Get
                Return Me._outcomeDeclaration
            End Get
            Set
                Me._outcomeDeclaration = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("templateDeclaration", Order:=2)>
        Public Property templateDeclaration() As List(Of TemplateDeclarationType)
            Get
                Return Me._templateDeclaration
            End Get
            Set
                Me._templateDeclaration = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=3)>
        Public Property templateProcessing() As TemplateProcessingType
            Get
                Return Me._templateProcessing
            End Get
            Set
                Me._templateProcessing = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("assessmentStimulusRef", Order:=4)>
        Public Property assessmentStimulusRef() As List(Of AssessmentStimulusRefType)
            Get
                Return Me._assessmentStimulusRef
            End Get
            Set
                Me._assessmentStimulusRef = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("stylesheet", Order:=5)>
        Public Property stylesheet() As List(Of StyleSheetType)
            Get
                Return Me._stylesheet
            End Get
            Set
                Me._stylesheet = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=6)>
        Public Property itemBody() As ItemBodyType
            Get
                Return Me._itemBody
            End Get
            Set
                Me._itemBody = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=7)>
        Public Property responseProcessing() As ResponseProcessingType
            Get
                Return Me._responseProcessing
            End Get
            Set
                Me._responseProcessing = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute("modalFeedback", Order:=8)>
        Public Property modalFeedback() As List(Of ModalFeedbackType)
            Get
                Return Me._modalFeedback
            End Get
            Set
                Me._modalFeedback = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlElementAttribute(Order:=9)>
        Public Property [object]() As ObjectType
            Get
                Return Me._object
            End Get
            Set
                Me._object = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property title() As String
            Get
                Return Me._title
            End Get
            Set
                Me._title = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property label() As String
            Get
                Return Me._label
            End Get
            Set
                Me._label = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Qualified, [Namespace]:="http://www.w3.org/XML/1998/namespace")>
        Public Property lang() As String
            Get
                Return Me._lang
            End Get
            Set
                Me._lang = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property toolName() As String
            Get
                Return Me._toolName
            End Get
            Set
                Me._toolName = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property toolVersion() As String
            Get
                Return Me._toolVersion
            End Get
            Set
                Me._toolVersion = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(),
            System.ComponentModel.DefaultValueAttribute(False)>
        Public Property adaptive() As Boolean
            Get
                Return Me._adaptive
            End Get
            Set
                Me._adaptive = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute()>
        Public Property timeDependent() As Boolean
            Get
                Return Me._timeDependent
            End Get
            Set
                Me._timeDependent = Value
            End Set
        End Property

        Public ReadOnly Property responseDeclarationSpecified() As Boolean
            Get
                Return (Equals(_responseDeclaration, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property outcomeDeclarationSpecified() As Boolean
            Get
                Return (Equals(_outcomeDeclaration, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateDeclarationSpecified() As Boolean
            Get
                Return (Equals(_templateDeclaration, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property templateProcessingSpecified() As Boolean
            Get
                Return (Equals(_templateProcessing, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property assessmentStimulusRefSpecified() As Boolean
            Get
                Return (Equals(_assessmentStimulusRef, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property stylesheetSpecified() As Boolean
            Get
                Return (Equals(_stylesheet, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property itemBodySpecified() As Boolean
            Get
                Return (Equals(_itemBody, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property responseProcessingSpecified() As Boolean
            Get
                Return (Equals(_responseProcessing, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property modalFeedbackSpecified() As Boolean
            Get
                Return (Equals(_modalFeedback, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property objectSpecified() As Boolean
            Get
                Return (Equals(_object, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property titleSpecified() As Boolean
            Get
                Return (Equals(_title, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property labelSpecified() As Boolean
            Get
                Return (Equals(_label, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property langSpecified() As Boolean
            Get
                Return (Equals(_lang, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property toolNameSpecified() As Boolean
            Get
                Return (Equals(_toolName, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property toolVersionSpecified() As Boolean
            Get
                Return (Equals(_toolVersion, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property adaptiveSpecified() As Boolean
            Get
                Return (Equals(_adaptive, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property timeDependentSpecified() As Boolean
            Get
                Return (Equals(_timeDependent, Nothing) <> True)
            End Get
        End Property
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0"),
        System.SerializableAttribute(),
        System.Diagnostics.DebuggerStepThroughAttribute(),
        System.ComponentModel.DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(TypeName:="AssessmentStimulusRef.Type", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2"),
        System.Xml.Serialization.XmlRootAttribute("assessmentStimulusRef", [Namespace]:="http://www.imsglobal.org/xsd/imsqti_v2p2", IsNullable:=False)>
    Partial Public Class AssessmentStimulusRefType
        Inherits EmptyPrimitiveTypeType

        Private _identifier As String

        Private _href As String

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="normalizedString")>
        Public Property identifier() As String
            Get
                Return Me._identifier
            End Get
            Set
                Me._identifier = Value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="anyURI")>
        Public Property href() As String
            Get
                Return Me._href
            End Get
            Set
                Me._href = Value
            End Set
        End Property

        Public ReadOnly Property identifierSpecified() As Boolean
            Get
                Return (Equals(_identifier, Nothing) <> True)
            End Get
        End Property

        Public ReadOnly Property hrefSpecified() As Boolean
            Get
                Return (Equals(_href, Nothing) <> True)
            End Get
        End Property
    End Class
#Enable Warning
End NameSpace