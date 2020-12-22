
Imports System.Linq
Imports System.Text
Imports System.Threading
Imports System.Xml.Serialization
Imports Cito.Tester.Common

<Serializable, _
 XmlType([Namespace]:="http://Cito.Tester.Server/xml/serialization")> _
Public Class Response


    Private _applicationId As String
    Private _sessionId As String
    Private _id As String
    Private _findings As ResponseFindingCollection
    Private _active As Boolean
    Private _translatedScore As Double
    Private _rawScore As Integer
    Private _responseNumber As Long
    Private _navigatedToIndex As Integer
    Private _itemIndexInTest As Integer
    Private _responseProperties As New SerializableGenericDictionary(Of String, String)
    Private Shared _lastResponseNumber As Long



    <XmlAttribute("active")> _
    Public Property Active As Boolean
        Get
            Return _active
        End Get
        Set
            _active = value
        End Set
    End Property

    <XmlElement("ResponseProperties")> _
    Public Property ResponseProperties As SerializableGenericDictionary(Of String, String)
        Set
            _responseProperties = value
        End Set
        Get
            Return _responseProperties
        End Get
    End Property

    <XmlAttribute("sessionId")> _
    Public Property SessionId As String
        Get
            Return _sessionId
        End Get
        Set
            _sessionId = value
        End Set
    End Property

    Public Property ApplicationId As String
        Get
            Return _applicationId
        End Get
        Set
            _applicationId = value
        End Set
    End Property

    <XmlAttribute("id")> _
    Public Property Id As String
        Get
            Return _id
        End Get
        Set
            _id = value
        End Set
    End Property

    <XmlAttribute("responseNr")> _
    Public Property ResponseNumber As Long
        Get
            Return _responseNumber
        End Get
        Set
            _responseNumber = value
        End Set
    End Property

    <XmlArray("responseFindings"), _
XmlArrayItem("responseFinding", GetType(ResponseFinding))> _
    Public ReadOnly Property Findings As ResponseFindingCollection
        Get
            Return _findings
        End Get
    End Property

    <XmlAttribute("translatedScore")> _
    Public Property TranslatedScore As Double
        Get
            Return _translatedScore
        End Get
        Set
            _translatedScore = value
        End Set
    End Property

    <XmlAttribute("rawScore")> _
    Public Property RawScore As Integer
        Get
            Return _rawScore
        End Get
        Set
            _rawScore = value
        End Set
    End Property

    <XmlAttribute("navigatedToIndex")> _
    Public Property NavigatedToIndex As Integer
        Get
            Return _navigatedToIndex
        End Get
        Set
            _navigatedToIndex = value
        End Set
    End Property

    Public Property ItemIndexInTest As Integer
        Get
            Return _itemIndexInTest
        End Get
        Set
            _itemIndexInTest = value
        End Set
    End Property

    Public Function GetFindingOrMakeIt(id As String) As ResponseFinding
        Dim finding = Findings.FirstOrDefault(Function(f) f.Id = id)
        If finding Is Nothing Then
            finding = New ResponseFinding(id)
            Findings.Add(finding)
        End If
        Return finding
    End Function



    Private Shared Function GetNewResponseNumber() As Long
        Interlocked.Increment(_lastResponseNumber)
        Return _lastResponseNumber
    End Function

    Public Shared Sub SetLastResponseNumber(value As Long)
        _lastResponseNumber = value
    End Sub

    Public Shared Sub ResetResponseNumber()
        _lastResponseNumber = 0
    End Sub

    Public Function IsMissing() As Boolean
        If Me._findings.Count > 0 Then
            For Each finding As BaseFinding In _findings
                If finding.Facts.Count = 0 Then
                    Return True
                End If
            Next
        Else
            Return True
        End If

        Return False
    End Function

    Public Function IsPartiallyMissing() As Boolean
        Dim partAnswered As Boolean = False
        Dim partMissing As Boolean = False
        If Me._findings.Count > 1 Then
            For Each finding As BaseFinding In _findings
                If finding.Facts.Count = 0 Then
                    partMissing = True
                Else
                    partAnswered = True
                End If
            Next
        End If
        Return (partMissing AndAlso partAnswered)
    End Function

    Public Overrides Function ToString() As String
        Dim returnValueSb As New StringBuilder()
        For Each keyFinding As BaseFinding In Me.Findings
            For Each fact As ResponseFact In keyFinding.Facts
                If returnValueSb.Length > 0 Then returnValueSb.Append("&")
                Dim valueCounter As Integer = 0
                For Each value As ResponseValue In fact.Values
                    If valueCounter > 0 Then returnValueSb.Append("#")
                    returnValueSb.Append(value.Value.ToString())
                    valueCounter += 1
                Next
            Next
        Next
        Return returnValueSb.ToString()
    End Function

    Public Sub EnsureNewResponseNumber()
        If _responseNumber = Long.MinValue Then
            _responseNumber = GetNewResponseNumber()
        Else
            Throw New ContentModelException(
                $"A response that is assigned a number, already has another number (nr = {Me.ResponseNumber}, item id = '{Me.Id}'). Each response should be unique!")
        End If
    End Sub


    Public Sub New(applicationId As String, sessionId As String, id As String)
        Me.New()
        Me.ApplicationId = applicationId
        Me.SessionId = sessionId
        Me.Id = id
    End Sub

    Public Sub New()
        Me._findings = New ResponseFindingCollection
        Me._responseNumber = Long.MinValue
        Me._navigatedToIndex = Integer.MinValue
    End Sub


End Class
