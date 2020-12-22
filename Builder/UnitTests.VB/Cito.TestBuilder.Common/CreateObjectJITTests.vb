
Imports Questify.Builder.Logic.Service.HelperFunctions

<TestClass>
Public Class CreateObjectJITTests

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateUninitialized_CurrentValueIsNull()
        Dim cJit = CreateUninitialized()

        Assert.IsNull(cJit.CurrentValue)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateUninitialized_IsNotEnsured()
        Dim cJit = CreateUninitialized()

        Assert.IsFalse(cJit.ValueIsEnsured)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateUninitialized_EnsuredValueIsNotNull()
        Dim cJit = CreateUninitialized()

        Assert.IsNull(cJit.CurrentValue)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateUninitialized_CallEnsuredWillSetCurrentValue()
        Dim cJit = CreateUninitialized()

        cJit.Ensure()

        Assert.IsNotNull(cJit.CurrentValue)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateUninitialized_CallEnsuredWillSetCurrentValueAndSetsIsEnsured()
        Dim cJit = CreateUninitialized()

        cJit.Ensure()

        Assert.IsTrue(cJit.ValueIsEnsured)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateUninitialized_GettingEnsuredValueWillReturnActualObject()
        Dim cJit = CreateUninitialized()

        Assert.IsNotNull(cJit.GetEnsuredValue)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateInitialized_GettingEnsuredValueWillReturnActualObject()
        Dim cJit = CreateInitialized()
        Dim current = cJit.CurrentValue

        Assert.AreSame(current, cJit.GetEnsuredValue)
    End Sub


    Public Function CreateUninitialized() As CreateObjectJIT(Of MyTest)
        Return New CreateObjectJIT(Of MyTest)(Function() New MyTest())
    End Function

    Public Function CreateInitialized() As CreateObjectJIT(Of MyTest)
        Return New CreateObjectJIT(Of MyTest)(New MyTest(), Function() New MyTest())
    End Function


    Class MyTest

    End Class

End Class