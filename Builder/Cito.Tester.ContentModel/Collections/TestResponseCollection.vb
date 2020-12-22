Imports System.Diagnostics.CodeAnalysis
Imports System.Runtime.Serialization
Imports Cito.Tester.Common


<SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix"), _
    SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix"), _
    Serializable> _
Public Class TestResponseCollection
    Inherits Dictionary(Of String, Response)


    Public Function GetResponseByIdentifier(id As String) As Response
        Dim r As Response = Nothing

        If Me.ContainsKey(id) Then
            r = Me(id)
        End If

        Return r
    End Function

    Public Function AddFromGenericResponseCollection(collection As IList(Of Response)) As Long
        Dim lastResponseNumber As Long = 0

        For Each r As Response In collection
            Me.Add(r.Id, r)
            If r.ResponseNumber > lastResponseNumber Then
                lastResponseNumber = r.ResponseNumber
            End If
        Next

        Return lastResponseNumber
    End Function

    <SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
    Public Sub AddOrReplaceThisResponse(r As Response)
        ReflectionHelper.CheckIsNotNothing(r, "response")
        If Me.ContainsKey(r.Id) Then
            Me.Remove(r.Id)
        End If

        Me.Add(r.Id, r)
    End Sub

    Public Function GetLastResponse() As Response
        Dim lastResponseNr As Long = 0
        Dim lastResponse As Response = Nothing

        For Each r As Response In Me.Values
            If r.ResponseNumber > lastResponseNr Then
                lastResponseNr = r.ResponseNumber
                lastResponse = r
            End If
        Next

        Return lastResponse
    End Function



    Public Sub New()
        MyBase.New()
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub


End Class
