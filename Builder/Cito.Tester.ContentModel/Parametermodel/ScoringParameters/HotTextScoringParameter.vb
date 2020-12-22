Imports System.Linq
Imports System.Xml.Serialization


Public Class HotTextScoringParameter : Inherits MultiChoiceScoringParameter

    Public Const ContentLabel = "contentLabel"

    Private _isCorrectionVariant As Boolean
    Private _hotTextText As New XHtmlParameter
    Private _previousHotTextTextValue As String


    <XmlIgnore> _
    Public Overrides ReadOnly Property IsSingleChoice As Boolean
        Get
            Return False
        End Get
    End Property

    <XmlAttribute("isCorrectionVariant")>
    Public Property IsCorrectionVariant As Boolean
        Get
            Return _isCorrectionVariant
        End Get
        Set
            _isCorrectionVariant = value
        End Set
    End Property

    <XmlIgnore>
    Public Property HotTextText As XHtmlParameter
        Get
            Return _hotTextText
        End Get
        Set(v As XHtmlParameter)
            _hotTextText = v
        End Set
    End Property

    <XmlElement("subparameterset", GetType(ParameterCollection))> _
    Public Overrides Property Value As ParameterSetCollection
        Get
            If HotTextTextHasChanged() Then
                If MyBase.Value IsNot Nothing Then
                    MyBase.Value.Clear()
                End If
                If _hotTextText IsNot Nothing Then
                    Dim textIdentifierAndLabel As Dictionary(Of String, String) = GetTextIdentifiersFromXhtml()

                    For Each dictEntry In textIdentifierAndLabel
                        Dim paramSet = New ParameterCollection() With {.Id = dictEntry.Key}
                        paramSet.InnerParameters.Add(New PlainTextParameter() With {.Name = ContentLabel, .Value = dictEntry.Value})

                        If MyBase.Value Is Nothing Then
                            MyBase.Value = New ParameterSetCollection()
                        End If
                        MyBase.Value.Add(paramSet)
                    Next
                End If
            End If

            Return MyBase.Value
        End Get
        Set
            MyBase.Value = value
        End Set
    End Property

    <XmlIgnore> _
    Public Overrides ReadOnly Property ShouldSuffix As Boolean
        Get
            Return False
        End Get
    End Property



    Public Overrides Function GetParametersWithDesignerSettings() As IEnumerable(Of ParameterBase)
        Return New ParameterBase() {}
    End Function


    Public Overrides Function GetLabelFor(scoreKey As String) As String
        If Value IsNot Nothing Then
            Dim subPar = Value.FirstOrDefault(Function(x) x.Id = scoreKey)
            If subPar IsNot Nothing Then
                Dim controlLabelPar = subPar.TryGetParameterByName(Of PlainTextParameter)(ContentLabel)
                If controlLabelPar IsNot Nothing Then
                    Return controlLabelPar.Value
                End If
            End If
        End If

        Return String.Empty
    End Function



    Private Function GetTextIdentifiersFromXhtml() As Dictionary(Of String, String)
        Dim firstLabelOccurenceToKeyDict = New Dictionary(Of String, String)
        Dim textIdentifierDict = New Dictionary(Of String, String)

        For Each inlineElement As KeyValuePair(Of String, InlineElement) In _hotTextText.GetInlineElements()

            For Each parameterCollection As ParameterCollection In inlineElement.Value.Parameters

                Dim controlTypeListedParam As ListedParameter = parameterCollection.TryGetParameterByName(Of ListedParameter)("controlType")

                If controlTypeListedParam IsNot Nothing AndAlso controlTypeListedParam.Value = "hottext" Then
                    Dim key As String = inlineElement.Value.Identifier
                    Dim controlLabelParameter As PlainTextParameter = DirectCast(parameterCollection.GetParameterByName("controlLabel"), PlainTextParameter)
                    Debug.Assert(controlLabelParameter IsNot Nothing, "controlLabel parameter must be present!")

                    Dim text As String = controlLabelParameter.Value.TrimEnd()

                    Dim identicalTexts = textIdentifierDict.Values.Where(Function(x)
                                                                             If x IsNot Nothing AndAlso x.StartsWith(text) Then
                                                                                 If x.Equals(text) Then
                                                                                     Return True
                                                                                 Else
                                                                                     Dim indexOfLastOpeningParentheses As Integer = x.LastIndexOf("("c)
                                                                                     If indexOfLastOpeningParentheses > -1 Then
                                                                                         Dim xWithoutCountSuffix As String = x.Substring(0, indexOfLastOpeningParentheses).TrimEnd()

                                                                                         Return xWithoutCountSuffix.Equals(text)
                                                                                     End If
                                                                                 End If
                                                                             End If

                                                                             Return False
                                                                         End Function)

                    If identicalTexts.Count > 0 Then
                        If Not identicalTexts(0).EndsWith(")") Then
                            Dim textWithSuffix As String = $"{identicalTexts(0)} (1)"
                            textIdentifierDict(firstLabelOccurenceToKeyDict(text)) = textWithSuffix
                        End If

                        text = $"{text} ({(identicalTexts.Count + 1)})"
                    Else
                        firstLabelOccurenceToKeyDict.Add(text, key)
                    End If

                    textIdentifierDict.Add(key, text)
                End If
            Next
        Next

        Return textIdentifierDict
    End Function

    Private Function HotTextTextHasChanged() As Boolean
        Dim areEqual As Boolean = False

        If HotTextText Is Nothing OrElse HotTextText.Value Is Nothing Then
            areEqual = String.IsNullOrEmpty(_previousHotTextTextValue)
            _previousHotTextTextValue = Nothing
        Else
            areEqual = HotTextText.Value.Equals(_previousHotTextTextValue)

            If Not areEqual Then
                _previousHotTextTextValue = HotTextText.Value
            End If
        End If

        Return Not areEqual
    End Function


End Class
