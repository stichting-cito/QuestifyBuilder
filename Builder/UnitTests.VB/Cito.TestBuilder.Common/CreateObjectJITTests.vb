
Imports Questify.Builder.Logic.Service.HelperFunctions

<TestClass>
Public Class CreateObjectJITTests

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateUninitialized_CurrentValueIsNull()                                 
        'Act
        Dim cJit = CreateUninitialized()
        
        'Assert
        Assert.IsNull(cJit.CurrentValue)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateUninitialized_IsNotEnsured()                                  
        'Act
        Dim cJit = CreateUninitialized()
        
        'Assert
        Assert.IsFalse(cJit.ValueIsEnsured)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateUninitialized_EnsuredValueIsNotNull()
        'Act
        Dim cJit = CreateUninitialized()
        
        'Assert
        Assert.IsNull(cJit.CurrentValue)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateUninitialized_CallEnsuredWillSetCurrentValue()
        'Arrange
        Dim cJit = CreateUninitialized()
        
        'Act
        cJit.Ensure()
        
        'Assert
        Assert.IsNotNull(cJit.CurrentValue)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateUninitialized_CallEnsuredWillSetCurrentValueAndSetsIsEnsured()
        'Arrange
        Dim cJit = CreateUninitialized()
        
        'Act
        cJit.Ensure()
        
        'Assert
        Assert.IsTrue(cJit.ValueIsEnsured)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateUninitialized_GettingEnsuredValueWillReturnActualObject()
        'Act
        Dim cJit = CreateUninitialized()
        
        'Assert
        Assert.IsNotNull(cJit.GetEnsuredValue)
    End Sub

    <TestMethod(), TestCategory("HelperMethods")>
    Public Sub CreateInitialized_GettingEnsuredValueWillReturnActualObject()
        'Act
        Dim cJit = CreateInitialized()
        Dim current = cJit.CurrentValue
        
        'Assert
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