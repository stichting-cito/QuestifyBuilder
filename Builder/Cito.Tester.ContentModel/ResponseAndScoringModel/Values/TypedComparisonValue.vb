Imports System.Xml.Serialization

<Serializable>
Public MustInherit Class TypedComparisonValue(Of T)
    Inherits BaseValue


    Enum ComparisonType
        None
        GreaterThan
        SmallerThan
        GreaterThanEquals
        SmallerThanEquals
        Equivalent
        Evaluate
        NotEquals
        Dependency
        NoValue
        EqualsSoft
        EqualsStrict
        EqualEquation
    End Enum

    Private _value As T
    Private _comparisonType As ComparisonType = ComparisonType.None



    Protected Sub New()
        MyBase.New()
    End Sub



    Public Overrides ReadOnly Property IsRange As Boolean
        Get
            Return False
        End Get
    End Property

    <XmlElement("typedComparisonValue")>
    Public Property Value As T
        Get
            Return _value
        End Get
        Set
            _value = value
        End Set
    End Property

    <XmlElement("comparisonType")>
    Public Property TypeOfComparison As String
        Get
            Return _comparisonType.ToString()
        End Get
        Set
            _comparisonType = GetComparisonType(value)
        End Set
    End Property



    Public Overrides Function ToString() As String
        Return String.Concat(comparisonPrefix(_comparisonType), _value.ToString)
    End Function

    Public Function GetComparisonType(comparisonType As String) As ComparisonType
        Dim localComparisonType As ComparisonType
        Dim type = localComparisonType.GetType()

        Return DirectCast([Enum].Parse(type, comparisonType, True), ComparisonType)
    End Function

    Public Function ComparisonPrefix(comparisonType As String) As String
        Return comparisonPrefix(GetComparisonType(comparisonType))
    End Function

    Public Function ComparisonPrefix(comparisonType As ComparisonType) As String
        Select Case comparisonType
            Case ComparisonType.None,
                 ComparisonType.EqualsSoft,
                 ComparisonType.EqualsStrict,
                 ComparisonType.EqualEquation,
                 ComparisonType.Evaluate,
                 ComparisonType.Dependency
                Return String.Empty
            Case ComparisonType.SmallerThan
                Return "<"
            Case ComparisonType.SmallerThanEquals
                Return "<="
            Case ComparisonType.GreaterThan
                Return ">"
            Case ComparisonType.GreaterThanEquals
                Return ">="
            Case ComparisonType.Equivalent
                Return "⇔"
            Case ComparisonType.NotEquals
                Return "<>"
            Case ComparisonType.NoValue
                Return "Ø"
        End Select
        Debug.Assert(False, "undefined what to return here")
        Return String.Empty
    End Function


End Class