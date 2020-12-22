Imports Cito.Tester.ContentModel

<TestClass>
Public Class StringComparisonValueTest : Inherits TypedComparisonValueBaseTest(Of String)

    Protected Overrides Function CreateTestObject() As TypedComparisonValue(Of String)
        Return New StringComparisonValue With {.Value = ""}
    End Function

    Protected Overrides Function GetSomeValue() As BaseValue
        Return New StringValue With {.Value = ""}
    End Function

    Protected Overrides ReadOnly Property ShouldSupport_None As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property ShouldSupport_Equivalent As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property ShouldSupport_NotEquals As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property ShouldSupport_Evaluate As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property ShouldSupport_Dependency As Boolean
        Get
            Return True
        End Get
    End Property

End Class
