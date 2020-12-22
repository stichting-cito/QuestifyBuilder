Imports System.ComponentModel
Imports System.IO
Imports System.Xml.Serialization
Imports Cito.Tester.Common

<Serializable, _
XmlRoot("parameterSet")> _
Public Class ParameterCollection
    Implements INotifyPropertyChanged


    Private _id As String = String.Empty
    Private WithEvents _parameters As New ParameterList



    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged



    <XmlAttribute("id")> _
    Public Property Id As String
        Get
            Return _id
        End Get
        Set
            Me.SetValueWithChangeNotification("id", _id, value)
        End Set
    End Property

    <Obsolete("This parameter is for Backwards Compatibility only! Please use 'InnerParameters' instead.", False)> _
    Public ReadOnly Property Parameters As ParameterBase()
        Get
            Return _parameters.ToArray()
        End Get
    End Property


    <XmlElement("xhtmlparameter", GetType(XHtmlParameter))> _
    <XmlElement("choicecollection", GetType(ChoiceCollectionParameter))> _
    <XmlElement("integerparameter", GetType(IntegerParameter))> _
    <XmlElement("resourceparameter", GetType(ResourceParameter))> _
    <XmlElement("custominteractionresourceparameter", GetType(CustomInteractionResourceParameter))> _
    <XmlElement("xhtmlresourceparameter", GetType(XhtmlResourceParameter))> _
    <XmlElement("plaintextparameter", GetType(PlainTextParameter))> _
    <XmlElement("booleanparameter", GetType(BooleanParameter))> _
    <XmlElement("collectionparameter", GetType(CollectionParameter))> _
    <XmlElement("multichoicescoringparameter", GetType(MultiChoiceScoringParameter))> _
    <XmlElement("stringScoringParameter", GetType(StringScoringParameter))> _
    <XmlElement("geogebraScoringParameter", GetType(GeogebraScoringParameter))> _
    <XmlElement("integerScoringParameter", GetType(IntegerScoringParameter))> _
    <XmlElement("decimalScoringParameter", GetType(DecimalScoringParameter))> _
    <XmlElement("currencyScoringParameter", GetType(CurrencyScoringParameter))> _
    <XmlElement("dateScoringParameter", GetType(DateScoringParameter))> _
    <XmlElement("timeScoringParameter", GetType(TimeScoringParameter))> _
    <XmlElement("inlineChoiceScoringparameter", GetType(InlineChoiceScoringParameter))> _
    <XmlElement("mathScoringParameter", GetType(MathScoringParameter))> _
    <XmlElement("listedparameter", GetType(ListedParameter))> _
    <XmlElement("areaparameter", GetType(AreaParameter))> _
    <XmlElement("gapMatchScoringParameter", GetType(GapMatchScoringParameter))> _
    <XmlElement("gapMatchRichTextScoringParameter", GetType(GapMatchRichTextScoringParameter))> _
    <XmlElement("graphGapMatchScoringParameter", GetType(GraphGapMatchScoringParameter))> _
    <XmlElement("gapTextParameter", GetType(GapTextParameter))> _
    <XmlElement("gapTextRichTextParameter", GetType(GapTextRichTextParameter))> _
    <XmlElement("gapImageParameter", GetType(GapImageParameter))> _
    <XmlElement("orderScoringParameter", GetType(OrderScoringParameter))> _
    <XmlElement("hotspotScoringParameter", GetType(HotspotScoringParameter))> _
    <XmlElement("matrixScoringParameter", GetType(MatrixScoringParameter))> _
    <XmlElement("hotTextScoringParameter", GetType(HotTextScoringParameter))> _
    <XmlElement("hotTextCorrectionScoringParameter", GetType(HotTextCorrectionScoringParameter))> _
    <XmlElement("mathMLParameter", GetType(MathMLParameter))> _
    <XmlElement("mathCasDependencyScoringParameter", GetType(MathCasDependencyScoringParameter))> _
    <XmlElement("casEqualStepsScoringParameter", GetType(CasEqualStepsScoringParameter))> _
    <XmlElement("mathCasEqualScoringParameter", GetType(MathCasEqualScoringParameter))> _
    <XmlElement("mathCasEvaluateScoringParameter", GetType(MathCasEvaluateScoringParameter))> _
    <XmlElement("aspectScoringParameter", GetType(AspectScoringParameter))> _
    <XmlElement("selectPointScoringParameter", GetType(SelectPointScoringParameter))> _
    Public ReadOnly Property InnerParameters As ParameterList
        Get
            Return _parameters
        End Get
    End Property


    <XmlAttribute("isDynamicCollection")>
    Public Property IsDynamicCollection As Boolean

    <XmlIgnoreAttribute>
    Public ReadOnly Property IsDynamicCollectionSpecified As Boolean
        Get
            Return IsDynamicCollection
        End Get
    End Property



    Public Shared Sub CopyDesignerSettings(copyTo As ParameterCollection, copyFrom As ParameterCollection)
        For Each paramFrom As ParameterBase In copyFrom.InnerParameters
            Dim paramTo As ParameterBase = DirectCast(copyTo.GetParameterByName(paramFrom.Name, True), ParameterBase)
            If paramTo IsNot Nothing Then
                paramTo.DesignerSettings.Clear()
                paramTo.DesignerSettings.AddRange(paramFrom.DesignerSettings)

                If TypeOf paramFrom Is CollectionParameter Then
                    For index As Integer = 0 To DirectCast(paramFrom, CollectionParameter).Value.Count - 1
                        CopyDesignerSettings(DirectCast(paramTo, CollectionParameter).Value(index), DirectCast(paramFrom, CollectionParameter).Value(index))
                    Next
                End If
            End If
        Next
    End Sub

    Public Shared Sub CopyAttributeReferences(copyTo As ParameterCollection, copyFrom As ParameterCollection)
        For Each paramFrom As ParameterBase In copyFrom.InnerParameters
            Dim paramTo As ParameterBase = DirectCast(copyTo.GetParameterByName(paramFrom.Name, True), ParameterBase)
            If paramTo IsNot Nothing Then
                paramTo.AttributeReferences.Clear()
                paramTo.AttributeReferences.AddRange(paramFrom.AttributeReferences)

                If TypeOf paramFrom Is CollectionParameter Then
                    For index As Integer = 0 To DirectCast(paramFrom, CollectionParameter).Value.Count - 1
                        CopyAttributeReferences(DirectCast(paramTo, CollectionParameter).Value(index), DirectCast(paramFrom, CollectionParameter).Value(index))
                    Next
                End If
            End If
        Next
    End Sub

    Public Shared Function DeepClone(paramSet As ParameterCollection) As ParameterCollection
        Dim serializer As New XmlSerializer(GetType(ParameterCollection))
        Dim ms As New MemoryStream
        Dim resultCollection As ParameterCollection = Nothing

        Try
            serializer.Serialize(ms, paramSet)
            ms.Position = 0
            resultCollection = DirectCast(serializer.Deserialize(ms), ParameterCollection)
        Finally
            ms.Close()
        End Try

        CopyDesignerSettings(resultCollection, paramSet)
        CopyAttributeReferences(resultCollection, paramSet)

        Return resultCollection
    End Function

    Public Function GetParameterByName(name As String, throwError As Boolean) As Object
        For Each p As ParameterBase In _parameters
            If p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) Then
                Return p
            End If
        Next

        If throwError Then
            Throw New ContentModelException($"Error: Parametername '{name}' is not found in the collection")
        End If

        Return Nothing
    End Function

    Public Function TryGetParameterByName(Of T As ParameterBase)(name As String) As T
        Dim obj = GetParameterByName(name, False)
        Return TryCast(obj, T)
    End Function

    Public Function GetParameterByName(Of T As ParameterBase)(name As String) As T
        Dim obj = GetParameterByName(name, True)
        Return DirectCast(obj, T)
    End Function

    Public Function GetParameterByName(name As String) As Object
        Return GetParameterByName(name, True)
    End Function

    Friend Sub SetValueWithChangeNotification(Of T)(propertyName As String, ByRef oldValue As T, newValue As T)
        If oldValue Is Nothing OrElse Not oldValue.Equals(newValue) Then
            oldValue = newValue
            NotifyPropertyChanged(propertyName)
        End If
    End Sub

    Private Sub NotifyPropertyChanged(info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Private Sub _parameters_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles _parameters.PropertyChanged
        NotifyPropertyChanged(e.PropertyName)
    End Sub


End Class