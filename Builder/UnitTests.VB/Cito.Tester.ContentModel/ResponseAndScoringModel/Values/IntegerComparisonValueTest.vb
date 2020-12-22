Imports Cito.Tester.ContentModel

<TestClass>
Public Class IntegerComparisonValueTest : Inherits TypedComparisonValueBaseTest(Of Integer)

    Protected Overrides Function CreateTestObject() As TypedComparisonValue(Of Integer)
        Return New IntegerComparisonValue
    End Function

    Protected Overrides Function GetSomeValue() As BaseValue
        Return New IntegerValue
    End Function

    Protected Overrides ReadOnly Property ShouldSupport_None As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property ShouldSupport_SmallerThan As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property ShouldSupport_SmallerThanEquals As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property ShouldSupport_GreaterThan As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property ShouldSupport_GreaterThanEquals As Boolean
        Get
            Return True
        End Get
    End Property

End Class
