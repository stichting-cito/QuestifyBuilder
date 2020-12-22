Imports System.Xml.Serialization

<XmlRoot("ItemInTestFilter")> _
Public Class ItemInTestFilterPredicate
    Inherits ItemFilterPredicate


    Private _assessmentTest As Guid = Guid.Empty
    Private _assessmentTestName As String



    Public Sub New()
    End Sub



    <XmlAttribute("assessmentTest")> _
    Public Property AssessmentTest() As Guid
        Get
            Return _assessmentTest
        End Get
        Set(ByVal value As Guid)
            _assessmentTest = value
        End Set
    End Property

    <XmlAttribute("assessmentTestName")> _
    Public Property AssessmentTestName() As String
        Get
            Return _assessmentTestName
        End Get
        Set(ByVal value As String)
            _assessmentTestName = value
        End Set
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Return My.Resources.ItemInTestFilterPredicateName
        End Get
    End Property

    Public Overrides ReadOnly Property NameLocalized() As String
        Get
            Return My.Resources.ItemInTestFilterPredicateNameLocalized
        End Get
    End Property



    Public Overrides Function ToString() As String
        Dim assessmentTest As String = Me.AssessmentTestName
        Return String.Format("{0} : {1}", Me.NameLocalized, assessmentTest)
    End Function


End Class