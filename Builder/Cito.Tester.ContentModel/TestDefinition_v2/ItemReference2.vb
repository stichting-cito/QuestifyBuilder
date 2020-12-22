Imports System.Diagnostics.CodeAnalysis
Imports System.IO
Imports System.Xml.Serialization
Imports Cito.Tester.Common


<Serializable, _
 XmlRoot(ElementName:="ItemReference2")> _
Public Class ItemReference2
    Inherits TestComponent2


    Private _active As Boolean = True
    Private _firstItemInSection As Boolean
    Private _itemFunctionalType As ItemFunctionalType
    Private _itemScoreTranslationTable As ItemScoreTranslationTable
    Private _weight As Double = 1.0
    Private _lastItemInTestPart As Boolean
    Private _sourceName As String
    Private _isAnchorItem As Boolean




    <SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")> _
    Public Sub New()
        MyBase.New()

        Me._itemScoreTranslationTable = New ItemScoreTranslationTable

        Me.SourceName = String.Empty
    End Sub


    Public Sub New(item As AssessmentItem)
        Me.New()
        If item IsNot Nothing Then
            Me._sourceName = item.Identifier
        Else
            Throw New ContentModelException(My.Resources.Error_TestReference_Constructor_ParametersNotSet1)
        End If
    End Sub



    <XmlAttribute("Active")> _
    Public Property Active As Boolean
        Get
            Return _active
        End Get
        Set
            _active = value
        End Set
    End Property

    <XmlIgnore> _
    Public Property FirstItemInSection As Boolean
        Get
            Return _firstItemInSection
        End Get
        Set
            _firstItemInSection = value
        End Set
    End Property



    <XmlIgnore> _
    Public Overrides ReadOnly Property IsPickable As Boolean
        Get
            If Me.State = ComponentState.Pickable Then
                Return True
            End If
        End Get
    End Property

    <XmlAttribute("ItemFunctionalType")> _
    Public Property ItemFunctionalType As ItemFunctionalType
        Get
            Return _itemFunctionalType
        End Get
        Set
            _itemFunctionalType = value
        End Set
    End Property

    <XmlArray("ItemScoreTranslationTable"), _
XmlArrayItem("ItemScoreTranslationTableEntry", GetType(ItemScoreTranslationTableEntry))> _
    Public ReadOnly Property ItemScoreTranslationTable As ItemScoreTranslationTable
        Get
            Return _itemScoreTranslationTable
        End Get
    End Property

    <XmlIgnore> _
    Public Property LastItemInTestPart As Boolean
        Get
            Return _lastItemInTestPart
        End Get
        Set
            _lastItemInTestPart = value
        End Set
    End Property

    <XmlIgnore> _
    Public Overrides ReadOnly Property MaxScore As Double
        Get
            Dim referencedAssessmentItem As AssessmentItem = GetAssessmentItem(Me.SourceName)

            Dim isttToUse As ItemScoreTranslationTable = Nothing

            If referencedAssessmentItem IsNot Nothing AndAlso referencedAssessmentItem.Solution IsNot Nothing AndAlso referencedAssessmentItem.Solution.ItemScoreTranslationTable.Count > 0 Then
                isttToUse = referencedAssessmentItem.Solution.ItemScoreTranslationTable
            ElseIf Me.ItemScoreTranslationTable IsNot Nothing AndAlso Me.ItemScoreTranslationTable.Count > 0 Then
                isttToUse = Me.ItemScoreTranslationTable
            End If

            If isttToUse IsNot Nothing Then
                Return (isttToUse.MaxTranslatedScore * Me.Weight)
            ElseIf referencedAssessmentItem IsNot Nothing AndAlso referencedAssessmentItem.Solution IsNot Nothing Then
                Return (referencedAssessmentItem.Solution.MaxSolutionRawScore * Me.Weight)
            End If

            Return 0
        End Get
    End Property

    Public Property Weight As Double
        Get
            Return _weight
        End Get

        Set
            _weight = value
        End Set
    End Property

    <XmlAttribute("Src")> _
    Public Property SourceName As String
        Get
            Return Me._sourceName
        End Get
        Set
            Me._sourceName = value
        End Set
    End Property

    <XmlAttribute("IsAnchorItem")> _
    Public Property IsAnchorItem As Boolean
        Get
            Return _isAnchorItem
        End Get
        Set
            _isAnchorItem = value
        End Set
    End Property



    Private Function GetAssessmentItem(itemCode As String) As AssessmentItem
        Dim referencedAssessmentItem As AssessmentItem = Nothing
        Dim assessmentItemBytes() As Byte = DirectCast(TestSessionContext.GetResourceObject(Me.SourceName, AddressOf StreamConverters.ConvertStreamToByteArray), Byte())

        Using assessmentItemStream = New MemoryStream(assessmentItemBytes)
            Using assessmentItemStreamResource As New StreamResource(assessmentItemStream)
                referencedAssessmentItem = DirectCast(StreamConverters.ConvertStreamToTypedInstance(Me.SourceName, assessmentItemStreamResource, GetType(AssessmentItem)), AssessmentItem)
            End Using
        End Using

        Return referencedAssessmentItem
    End Function


End Class