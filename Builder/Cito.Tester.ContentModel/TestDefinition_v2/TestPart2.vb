Imports System.Diagnostics.CodeAnalysis
Imports System.Xml.Serialization


<Serializable, _
 XmlRoot(ElementName:="TestPart2")> _
Public Class TestPart2
    Inherits AssessmentTestNode

    Private _sections As TestSectionCollection2

    <SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")> _
    Public Sub New()
        MyBase.New()

        Me._sections = New TestSectionCollection2(Me)
    End Sub

    <XmlArray("TestSections"), _
    XmlArrayItem("Section", GetType(TestSection2))> _
    Public ReadOnly Property Sections As TestSectionCollection2
        Get
            Return Me._sections
        End Get
    End Property

    <XmlIgnore> _
    Public Overrides ReadOnly Property MaxScore As Double
        Get
            Dim cummulativeMaxScore As Double = 0
            For Each section As TestSection2 In Me.Sections
                cummulativeMaxScore += section.MaxScore
            Next

            Return cummulativeMaxScore
        End Get
    End Property

    Public Overrides ReadOnly Property IsPickable As Boolean
        Get
            For Each comp As TestComponent2 In Me.Sections
                If comp.IsPickable Then Return True
            Next
        End Get
    End Property

    Public Function FindFirstItemReferenceInTestPart() As ItemReference2
        Dim result As ItemReference2 = Nothing
        For Each section As TestSection2 In Me.Sections
            result = section.FindFirstItemReferenceInSection
            If result IsNot Nothing Then Return result
        Next
        Return result
    End Function

    Public Function GetAllItemReferencesInTestPart(includeChildren As Boolean) As List(Of ItemReference2)
        Dim returnValue As New List(Of ItemReference2)

        For Each comp As TestSection2 In Me.Sections
            returnValue.AddRange(comp.GetAllItemReferencesInSection(includeChildren))
        Next

        Return returnValue
    End Function

    Public Function FindNodeByIdentifier(identifier As String) As AssessmentTestNode
        Dim result As AssessmentTestNode = Nothing

        If Me.Identifier = identifier Then
            result = Me
        Else
            For Each component As TestSection2 In Me.Sections
                result = DirectCast(component, TestSection2).FindNodeByIdentifier(identifier)
                If result IsNot Nothing Then
                    Return result
                End If
            Next
        End If

        Return result
    End Function

End Class
