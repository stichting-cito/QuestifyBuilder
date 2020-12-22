
Imports System.Xml.Linq
Imports System.Xml.Serialization
Imports System.IO

Public Class ItemProcessingBase

    Private Shared testContextInstance As TestContext
    Private serializeCache As New Dictionary(Of Type, System.Xml.Serialization.XmlSerializer)

    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

    <ClassInitialize()>
    Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
        testContextInstance = testContext
    End Sub

    Public Function GetSerializer(ByVal type As Type) As System.Xml.Serialization.XmlSerializer
        If serializeCache.ContainsKey(type) Then
            Return serializeCache(type)
        End If
        Dim ret As New System.Xml.Serialization.XmlSerializer(type)
        serializeCache.Add(type, ret)
        Return ret
    End Function

    Public Function Data(Of T)(ByVal column As String) As T
        Return DirectCast(TestContext.DataRow(column), T)
    End Function

    Public Function Data(ByVal column As String) As String
        Return Data(Of String)(column)
    End Function

    Public Function GetInt(ByVal column As String) As Integer
        Return Convert.ToInt32(TestContext.DataRow(column))
    End Function

    Protected Function Deserialize(Of T)(input As XElement) As T
        Dim ret As T
        Dim s = New XmlSerializer(GetType(T))

        Using m As New StringReader(input.ToString())
            ret = DirectCast(s.Deserialize(m), T)
        End Using

        Return ret
    End Function
End Class
