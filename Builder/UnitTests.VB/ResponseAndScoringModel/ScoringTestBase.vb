
Imports System.Diagnostics
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports System.Xml.Serialization
Imports System.IO
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel.ResponseAndScoringModel.Solution.ConcreteScoring

<TestClass>
Public MustInherit Class ScoringTestBase

    Private _Initializer As New Dictionary(Of Type, Action(Of Attribute))()
    Private _scoringMethod As String

    <TestInitialize()>
    Public Sub TestInitialize()
        RunInitializers()
    End Sub

    <TestCleanup()>
    Public Sub MyClassCleanup()
        ScoringFactory.ResetScoreMethodToDefault()
    End Sub

    Public Sub New()
        'A construct to override the scoring strategy
        AddAttributteInitializer(Of ScoringMethodAttribute)(AddressOf DealWithScoring)
    End Sub

    Private Sub DealWithScoring(a As Attribute)
        'A construct to override the scoring strategy
        Dim scoreOverride = DirectCast(a, ScoringMethodAttribute)
        _scoringMethod = scoreOverride.Method
        ScoringFactory.OverrideScoreMethod(scoreOverride.Method)
    End Sub

    Private Sub AddAttributteInitializer(Of T As Attribute)(init As Action(Of Attribute))
        _Initializer.Add(GetType(T), init)
    End Sub

    Protected Function ToSolution(data As XElement) As Solution
        Return DirectCast(SerializeHelper.XmlDeserializeFromString(data.ToString(), GetType(Solution)), Solution)
    End Function

    Protected Function ToResponse(data As XElement) As Response
        Return DirectCast(SerializeHelper.XmlDeserializeFromString(data.ToString(), GetType(Response)), Response)
    End Function

    Protected Function CreateResponseValueNoDomain(Of T)(value As T) As ResponseValue
        Dim ret = New ResponseValue()
        ret.Value = CreateBaseValue(value)
        Return ret
    End Function

    Protected Function CreateResponseValue(Of T)(value As T, domain As String) As ResponseValue
        Return New ResponseValue(domain, CreateBaseValue(value))
    End Function
    
    protected Function GetResponse(of T)(valuesToCreate As List(Of T)) As Response
        return GetResponse(valuesToCreate, "mc")
    End Function

    protected Function GetResponse(of T)(valuesToCreate As List(Of T), findingId As string) As Response
        dim valuesToCreateTuple = New List(Of Tuple(Of T, string))
        Dim i As Integer = 1

        valuesToCreate.ForEach(Sub(v)
            valuesToCreateTuple.Add(New Tuple(of T, String)(v , AlphabeticIdentifierHelper.GetAlphabeticIdentifier(i)))
            i +=1
        End Sub)

        return GetResponse(valuesToCreateTuple, findingId)
    End Function

    protected Function GetResponse(of T)(valuesToCreate As List(Of Tuple(Of T, String)), findingId As string) As Response
        Dim r As New Response()
        Dim rF As New ResponseFinding(id:=findingId)
        Dim respFact As New ResponseFact()
        
        valuesToCreate.ForEach(Sub(v)
            respFact.Values.Add(CreateResponseValue(v.Item1, v.Item2))
        End Sub)
        rF.Facts.Add(respFact) : r.Findings.Add(rF)
        Return r
    End Function

    Protected Function GetScoreSolution(element As XElement, response As XElement) As Integer
        Dim solution = toSolution(element)
        Dim r = toResponse(response)
        Write("Response", "Arrange", r) 'Write for debugging
        Return solution.ScoreSolution(r)
    End Function


    Protected Sub Write(Of T)(typeName As String, stateName As String, obj As T)
        Dim a As New XmlSerializer(GetType(T))
        Debug.WriteLine(String.Empty)
        Debug.WriteLine($"Write{typeName} for State [{stateName}]")
        Using stream = New StringWriter()
            a.Serialize(stream, obj)

            Debug.WriteLine(stream.ToString())
            'Console.WriteLine(stream.ToString())
        End Using
    End Sub

    Private Function CreateBaseValue(Of T)(value As T) As BaseValue
        Dim ret As BaseValue = Nothing
        WhenObject(value,
                                IsType(Of String)(Sub(e As String) ret = New StringValue(e)),
                                IsType(Of Integer)(Sub(e As Integer) ret = New IntegerValue(e)),
                                IsType(Of Decimal)(Sub(e As Decimal) ret = New DecimalValue(e)),
                                IsType(Of Byte())(Sub(e As Byte()) ret = New BinaryValue(e)),
                                IsType(Of BaseValue)(Sub(e) ret = e),
                                Otherwise(Sub() Throw New ArgumentException(GetType(T).ToString())))

        If (ret Is Nothing) Then Throw New Exception("Should NOT occur")

        Return ret
    End Function


    Private Sub RunInitializers()
        'Per test method, DOES take inherited attributtes
        Dim meth = Me.GetType().GetMethod(TestContext.TestName)
        For Each a As Object In meth.GetCustomAttributes(True)
            Dim doInit4Att As Action(Of Attribute) = Nothing
            If (_Initializer.TryGetValue(a.GetType(), doInit4Att)) Then
                doInit4Att(TryCast(a, Attribute))
            End If
        Next
        
        'Per test class, Does NOT take inherited attributtes
        Dim clss = Me.GetType()
        For Each a As Object In clss.GetCustomAttributes(False)
            Dim doInit4Att As Action(Of Attribute) = Nothing
            If (_Initializer.TryGetValue(a.GetType(), doInit4Att)) Then
                doInit4Att(TryCast(a, Attribute))
            End If
        Next
    End Sub

    Public Property TestContext As TestContext

    Protected ReadOnly Property ScoringMethod() As String
        Get
            Return _scoringMethod
        End Get
    End Property

End Class
