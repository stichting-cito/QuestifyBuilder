
Imports System.Xml.Serialization
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel.Scoring.Validator
Imports Questify.Builder.UnitTests.Framework

<TestClass()>
Public Class ScoringValidatorTests

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub OrBetweenSingleInteractions_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.NoOrBetweenSingleInteractions)
        Dim validator As New NoOrBetweenSingleInteractions()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub AndOnSameInteraction_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.NoAndOnSameInteraction)
        Dim validator As New NoAndOnSameInteraction()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub ScoringParameterChoiceHasNoScoringDefinition_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.EachInteractionHasScoringDefinition_Choice)
        Dim validator As New EachInteractionHasScoringDefinition()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub ScoringParameterStringHasNoScoringDefinition_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.EachInteractionHasScoringDefinition_String)
        Dim validator As New EachInteractionHasScoringDefinition()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub ScoringParameterIntegerHasNoScoringDefinition_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.EachInteractionHasScoringDefinition_Integer)
        Dim validator As New EachInteractionHasScoringDefinition()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub ScoringParameterDecimalHasNoScoringDefinition_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.EachInteractionHasScoringDefinition_Decimal)
        Dim validator As New EachInteractionHasScoringDefinition()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub ScoringParameterDateHasNoScoringDefinition_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.EachInteractionHasScoringDefinition_Date)
        Dim validator As New EachInteractionHasScoringDefinition()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub ScoringParameterTimeHasNoScoringDefinition_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.EachInteractionHasScoringDefinition_Time)
        Dim validator As New EachInteractionHasScoringDefinition()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub ScoringParameterMathHasNoScoringDefinition_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.EachInteractionHasScoringDefinition_Math)
        Dim validator As New EachInteractionHasScoringDefinition()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub GroupConsistsOfMultipleSameInteractionsChoice_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.GroupConsistsOfMultipleDifferentInteractions_Choice)
        Dim validator As New GroupConsistsOfMultipleDifferentInteractions()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub GroupConsistsOfMultipleSameInteractionsChoiceInline_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.GroupConsistsOfMultipleDifferentInteractions_ChoiceInline)
        Dim validator As New GroupConsistsOfMultipleDifferentInteractions()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub OtherGroupWithSameInteractionsDoesNotExists_Zero_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.OtherGroupWithSameInteractionsExists_Zero)
        Dim validator As New OtherGroupWithSameInteractionsExists()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub OtherGroupWithSameInteractionsDoesNotExists_Wrong_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.OtherGroupWithSameInteractionsExists_Wrong)
        Dim validator As New OtherGroupWithSameInteractionsExists()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub InteractionOfGroupExistOutsideAGroup_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.InteractionOfGroupCannotExistOutsideAGroup)
        Dim validator As New InteractionOfGroupCannotExistOutsideAGroup()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Invalid"), TestCategory("Scoring")>
    Public Sub GroupsWithSameInteractionsDiffer_IsNotValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.GroupsWithSameInteractionsAreEqual)
        Dim validator As New GroupsWithSameInteractionsAreEqual()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.ProcessValidationRule(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNotNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Valid"), TestCategory("Scoring")>
    Public Sub SingleInteraction_IsValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.Valid_SingleInteraction)
        Dim validator As New ScoringValidator()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.Validate(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Valid"), TestCategory("Scoring")>
    Public Sub SingleInteractionWithOr_IsValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.Valid_SingleInteractionWithOr)
        Dim validator As New ScoringValidator()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.Validate(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Valid"), TestCategory("Scoring")>
    Public Sub MultipleInteractionsWithAnd_IsValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.Valid_MulitpleInteractionWithAnd)
        Dim validator As New ScoringValidator()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.Validate(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Valid"), TestCategory("Scoring")>
    Public Sub MultipleInteractionsWithAndInOrGroup_IsValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.Valid_MulitpleInteractionsWithAndInOrGroups)
        Dim validator As New ScoringValidator()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.Validate(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Valid"), TestCategory("Scoring")>
    Public Sub MultipleInteractionsWithAndInOrGroup_PlusSingleInteraction_IsValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.Valid_MulitpleInteractionsWithAndInOrGroups_PlusSingleInteraction)
        Dim validator As New ScoringValidator()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.Validate(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Valid"), TestCategory("Scoring")>
    Public Sub MultipleRepsonsesWithOr_IsValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.Valid_MulitpleResponsesWithOr)
        Dim validator As New ScoringValidator()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.Validate(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNull(exception)
    End Sub

    <TestMethod(), TestCategory("ScoringValidation_Valid"), TestCategory("Scoring")>
    Public Sub TwoSetsOfIndependentGroups_IsValid()
        'Arrange
        Dim xmlSerializer As New XmlSerializer(GetType(AssessmentItem))
        Dim item = xmlSerializer.Deserialize(Of AssessmentItem)(My.Resources.Valid_TwoSetsOfIndependentGroups)
        Dim validator As New ScoringValidator()
        Dim exception As Exception = Nothing

        'Act
        Try
            validator.Validate(item)
        Catch ex As Exception
            exception = ex
        End Try

        'Assert
        Assert.IsNull(exception)
    End Sub

End Class
