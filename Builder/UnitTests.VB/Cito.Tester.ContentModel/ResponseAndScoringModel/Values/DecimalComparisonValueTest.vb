Imports Cito.Tester.ContentModel

<TestClass>
Public Class DecimalComparisonValueTest : Inherits TypedComparisonValueBaseTest(Of Decimal)

    Protected Overrides Function CreateTestObject() As TypedComparisonValue(Of Decimal)
        Return New DecimalComparisonValue
    End Function

    Protected Overrides Function GetSomeValue() As BaseValue
        Return New DecimalValue
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
