
Imports System.Linq
Imports System.Reflection
Imports Cito.Tester.ContentModel

<TestClass()>
Public MustInherit Class TypedComparisonValueBaseTest(Of T)

    Const PropertyMarker As String = "ShouldSupport_"

    <TestMethod> <TestCategory("ContentModel")>
    Public Sub AllComparisonTypesAreTested()

        Dim names = getComparisonValues()
        Dim properties = GetProperties().Where(Function(propertyInfo) propertyInfo.Name.StartsWith(PropertyMarker)).Select(Function(propertyInfo) propertyInfo.Name).ToList()

        For Each name As String In names
            Dim exists = properties.Any(Function(propname) propname = PropertyMarker + name)
            Assert.IsTrue(exists)
        Next
    End Sub

    <TestMethod> <TestCategory("ContentModel")>
    Public Sub TestSupport()
        Dim testTee As TypedComparisonValue(Of T) = CreateTestObject()
        Dim testValue As BaseValue = GetSomeValue()

        For Each comparison As String In getComparisonValues()
            Dim isSupported = True

            Try
                testTee.TypeOfComparison = comparison
                testTee.IsMatch(testValue)
            Catch ex As NotSupportedException
                isSupported = False
            Catch ex As Exception
                Assert.Fail("Failed for reasons unknown")
            End Try

            Assert.AreEqual(isSupported, IsComparisonSupported(comparison))
        Next
    End Sub

    Private Function IsComparisonSupported(comparison As String) As Boolean
        Dim p = GetProperties().Single(Function(propertyInfo) propertyInfo.Name = PropertyMarker + comparison)

        Dim result = DirectCast(p.GetMethod.Invoke(Me, Nothing), Boolean)

        Return result
    End Function

    Protected MustOverride Function GetSomeValue() As BaseValue

    Protected MustOverride Function CreateTestObject() As TypedComparisonValue(Of T)


    Private Function GetProperties() As PropertyInfo()
        Return Me.GetType().GetProperties(BindingFlags.Instance Or BindingFlags.NonPublic)
    End Function

    Private Function getComparisonValues() As IEnumerable(Of String)
        Dim enums As TypedComparisonValue(Of Integer).ComparisonType
        Return [Enum].GetNames(enums.GetType())
    End Function

    Protected Overridable ReadOnly Property ShouldSupport_None As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property ShouldSupport_GreaterThan As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property ShouldSupport_SmallerThan As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property ShouldSupport_GreaterThanEquals As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property ShouldSupport_SmallerThanEquals As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property ShouldSupport_Equivalent As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property ShouldSupport_NotEquals As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property ShouldSupport_Evaluate As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property ShouldSupport_Dependency As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property ShouldSupport_NoValue As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property ShouldSupport_EqualEquation As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property ShouldSupport_EqualsSoft As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property ShouldSupport_EqualsStrict As Boolean
        Get
            Return False
        End Get
    End Property

End Class
