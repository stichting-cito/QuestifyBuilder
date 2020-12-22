
Option Strict On
Option Explicit On

Imports System

Namespace My.Resources

    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"), _
Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), _
Global.Microsoft.VisualBasic.HideModuleNameAttribute()> _
    Friend Module Resources

        Private resourceMan As Global.System.Resources.ResourceManager

        Private resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Questify.Builder.UnitTests.VB.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property

        Friend ReadOnly Property _2_2_4() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("_2_2_4", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property Concept() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("Concept", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property ConceptAttribute() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("ConceptAttribute", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property ConceptEmpty() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("ConceptEmpty", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property EachInteractionHasScoringDefinition_Choice() As String
            Get
                Return ResourceManager.GetString("EachInteractionHasScoringDefinition_Choice", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property EachInteractionHasScoringDefinition_Date() As String
            Get
                Return ResourceManager.GetString("EachInteractionHasScoringDefinition_Date", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property EachInteractionHasScoringDefinition_Decimal() As String
            Get
                Return ResourceManager.GetString("EachInteractionHasScoringDefinition_Decimal", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property EachInteractionHasScoringDefinition_InlineChoice() As String
            Get
                Return ResourceManager.GetString("EachInteractionHasScoringDefinition_InlineChoice", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property EachInteractionHasScoringDefinition_InlineGap() As String
            Get
                Return ResourceManager.GetString("EachInteractionHasScoringDefinition_InlineGap", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property EachInteractionHasScoringDefinition_Integer() As String
            Get
                Return ResourceManager.GetString("EachInteractionHasScoringDefinition_Integer", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property EachInteractionHasScoringDefinition_Math() As String
            Get
                Return ResourceManager.GetString("EachInteractionHasScoringDefinition_Math", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property EachInteractionHasScoringDefinition_String() As String
            Get
                Return ResourceManager.GetString("EachInteractionHasScoringDefinition_String", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property EachInteractionHasScoringDefinition_Time() As String
            Get
                Return ResourceManager.GetString("EachInteractionHasScoringDefinition_Time", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property FreeValue() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("FreeValue", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property GroupConsistsOfMultipleDifferentInteractions_Choice() As String
            Get
                Return ResourceManager.GetString("GroupConsistsOfMultipleDifferentInteractions_Choice", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GroupConsistsOfMultipleDifferentInteractions_ChoiceInline() As String
            Get
                Return ResourceManager.GetString("GroupConsistsOfMultipleDifferentInteractions_ChoiceInline", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property GroupsWithSameInteractionsAreEqual() As String
            Get
                Return ResourceManager.GetString("GroupsWithSameInteractionsAreEqual", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property InlineChoice_EmptyFacts() As String
            Get
                Return ResourceManager.GetString("InlineChoice_EmptyFacts", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property InlineChoice_MultipleAnswer() As String
            Get
                Return ResourceManager.GetString("InlineChoice_MultipleAnswer", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property InlineChoice_SingleAnswer() As String
            Get
                Return ResourceManager.GetString("InlineChoice_SingleAnswer", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property IntegerGrouping() As String
            Get
                Return ResourceManager.GetString("IntegerGrouping", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property InteractionOfGroupCannotExistOutsideAGroup() As String
            Get
                Return ResourceManager.GetString("InteractionOfGroupCannotExistOutsideAGroup", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property MRGroupedRemovedAlternative() As String
            Get
                Return ResourceManager.GetString("MRGroupedRemovedAlternative", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property MultiListValue() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("MultiListValue", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property NoAndOnSameInteraction() As String
            Get
                Return ResourceManager.GetString("NoAndOnSameInteraction", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property NoOrBetweenSingleInteractions() As String
            Get
                Return ResourceManager.GetString("NoOrBetweenSingleInteractions", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OtherGroupWithSameInteractionsExists_Wrong() As String
            Get
                Return ResourceManager.GetString("OtherGroupWithSameInteractionsExists_Wrong", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property OtherGroupWithSameInteractionsExists_Zero() As String
            Get
                Return ResourceManager.GetString("OtherGroupWithSameInteractionsExists_Zero", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property sampleimage() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("sampleimage", resourceCulture)
                Return CType(obj, System.Drawing.Bitmap)
            End Get
        End Property

        Friend ReadOnly Property samplemp4() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("samplemp4", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property samplesvg() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("samplesvg", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property SingleListValue() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("SingleListValue", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property SingleListValueDoubleValue() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("SingleListValueDoubleValue", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property Test_Doc() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("Test_Doc", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property Tree() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("Tree", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property TreeEmpty() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("TreeEmpty", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property TreeMulti() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("TreeMulti", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property TreeMulti2() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("TreeMulti2", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property UnitTestAbsolutePackage() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("UnitTestAbsolutePackage", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property UnitTestPackageFile() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("UnitTestPackageFile", resourceCulture)
                Return CType(obj, Byte())
            End Get
        End Property

        Friend ReadOnly Property Valid_MulitpleInteractionsWithAndInOrGroups() As String
            Get
                Return ResourceManager.GetString("Valid_MulitpleInteractionsWithAndInOrGroups", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Valid_MulitpleInteractionsWithAndInOrGroups_PlusSingleInteraction() As String
            Get
                Return ResourceManager.GetString("Valid_MulitpleInteractionsWithAndInOrGroups_PlusSingleInteraction", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Valid_MulitpleInteractionWithAnd() As String
            Get
                Return ResourceManager.GetString("Valid_MulitpleInteractionWithAnd", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Valid_MulitpleResponsesWithOr() As String
            Get
                Return ResourceManager.GetString("Valid_MulitpleResponsesWithOr", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Valid_SingleInteraction() As String
            Get
                Return ResourceManager.GetString("Valid_SingleInteraction", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Valid_SingleInteractionWithOr() As String
            Get
                Return ResourceManager.GetString("Valid_SingleInteractionWithOr", resourceCulture)
            End Get
        End Property

        Friend ReadOnly Property Valid_TwoSetsOfIndependentGroups() As String
            Get
                Return ResourceManager.GetString("Valid_TwoSetsOfIndependentGroups", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
