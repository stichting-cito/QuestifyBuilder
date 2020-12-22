Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization

Imports Cito.Tester.Common

Public Class XhtmlResourceParameter
    Inherits ResourceParameter


    Private Const ReferNode As String = "referto"
    Private Const ResourceNode As String = "resource"
    Private Const ValueTemplate As String = "<{0}>{1}</{0}><{2}>{3}</{2}>"



    Private _activeRefId As String
    Private _references As New XhtmlReferenceList
    Private _TekstResource As String



    Public Overrides ReadOnly Property EditSizeSpecified As Boolean
        Get
            Return False
        End Get
    End Property

    <XmlIgnore>
    Public Property ActiveReference As XhtmlReference
        Get
            Return Me.References.GetReferenceById(Me.ExtractData(ReferNode))
        End Get
        Set
            If value Is Nothing Then
                _activeRefId = Nothing
            Else
                Dim reference As XhtmlReference = Me.References.GetReferenceById(value.ID)
                If reference Is Nothing Then
                    Throw New ArgumentException("Given reference is not found in the reference collection.", "value")
                Else
                    _activeRefId = reference.ID
                End If
            End If

            MyBase.Value = String.Format(ValueTemplate, ReferNode, _activeRefId, ResourceNode, Me.ExtractData(ResourceNode))
            NotifyPropertyChanged("ActiveReference")
        End Set
    End Property

    <XmlIgnore>
    Public ReadOnly Property Content As String
        Get
            Return _TekstResource
        End Get
    End Property

    <XmlIgnore>
    Public ReadOnly Property References As XhtmlReferenceList
        Get
            Return _references
        End Get
    End Property

    <XmlIgnore>
    Public Overrides Property Value As String
        Get
            Return Me.ExtractData(ResourceNode)
        End Get
        Set
            Dim referTo As String = Me.ExtractData(ReferNode)
            MyBase.Value = String.Concat(IIf(String.IsNullOrEmpty(Trim(referTo)), $"<{ReferNode}/>", String.Format("<{0}>{1}</{0}>", ReferNode, referTo)), IIf(String.IsNullOrEmpty(Trim(value)),
                                                                                                                                                               $"<{ _
                                                                                                                                                                  ResourceNode _
                                                                                                                                                                  }/>", String.Format("<{0}>{1}</{0}>", ResourceNode, value)))
        End Set
    End Property



    Private Function ExtractData(node As String) As String
        Dim data As String = MyBase.Value
        If Not String.IsNullOrEmpty(data) Then
            Dim doc As New XHtmlDocument
            Dim rootElement As XmlElement = doc.CreateElement("root")
            rootElement.InnerXml = MyBase.Value
            doc.AppendChild(rootElement)
            data = doc.DocumentElement.SelectSingleNode(node).InnerXml
            Dim trimmedData As String = Trim(data)
            If String.IsNullOrEmpty(trimmedData) OrElse (trimmedData.Length = 1 AndAlso Char.IsWhiteSpace(trimmedData, 0)) Then data = String.Empty
        End If
        Return data
    End Function



    Public Overrides Function ToString() As String
        Return Me.Value
    End Function

    Public Overrides Sub RefreshResource()
        MyBase.RefreshResource()
        If Me.Resource IsNot Nothing AndAlso Me.Resource.Length > 0 Then
            _TekstResource = Encoding.UTF8.GetString(Me.Resource)
            If Not String.IsNullOrEmpty(_TekstResource) Then
                Dim doc As New XHtmlDocument()

                Dim rootElement As XmlElement = doc.CreateElement("root")
                rootElement.InnerXml = _TekstResource
                doc.AppendChild(rootElement)

                _references = XhtmlReferenceFactory.ParseXhtmlReference(doc.InnerXml)
            End If

            Dim active As XhtmlReference = Me.References.GetReferenceById(_activeRefId)
            If active Is Nothing Then
                _activeRefId = Nothing
            End If
        Else
            _TekstResource = Nothing
            _references.Clear()
            _activeRefId = Nothing
        End If
    End Sub


    Public Overrides Sub PreFetchResource()
        RefreshResource()
    End Sub

End Class