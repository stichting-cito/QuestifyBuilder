Imports System.Xml
Imports System.ComponentModel
Imports System.Text
Imports System.Xml.Serialization

Public Class ChoiceCollectionParameter
    Inherits Parameter(Of SimpleChoiceCollection)

    Private WithEvents _choices As SimpleChoiceCollection

    <XmlElement("simpleChoice", GetType(SimpleChoice))> _
    Public ReadOnly Property Choices As SimpleChoiceCollection
        Get
            Return _choices
        End Get
    End Property

    <XmlIgnore> _
    Overrides Property Value As SimpleChoiceCollection
        Get
            Return _choices
        End Get
        Set
            Throw New ArgumentException(My.Resources.Error_ChoiceCollectionParameter_Value_SetValueNotAllowed)
        End Set
    End Property

    Public Sub New()
        _choices = New SimpleChoiceCollection()
        AddHandler _choices.ListChanged, AddressOf Choices_ListChanged
    End Sub

    Public Overrides Function SetValue(value As String) As Boolean
        Return True
    End Function

    Public Overrides Function ToString() As String
        Dim sb As new StringBuilder()
        For Each choice As SimpleChoice In _choices
            Dim choiceValue As String = String.Empty
            For Each xmlNode As XmlNode In choice.Nodes
                choiceValue += xmlNode.InnerText
            Next
            If Not String.IsNullOrEmpty(sb.ToString()) Then sb.Append("#")
            sb.Append(choiceValue)
        Next
        Return sb.ToString()
    End Function

    Private Sub Choices_ListChanged(sender As Object, e As ListChangedEventArgs)
        NotifyPropertyChanged("Value")
    End Sub

End Class