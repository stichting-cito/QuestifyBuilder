Imports System.ComponentModel
Imports System.Xml.Serialization

<Serializable> _
<XmlRoot(ElementName:="itemSessionControl")> _
Public Class ItemSessionControl
    Implements INotifyPropertyChanged



    Private _maxAttempts As Integer
    Private _showFeedback As Boolean = False
    Private _allowReview As Boolean = True
    Private _showSolution As Boolean = False
    Private _allowComment As Boolean = False
    Private _allowSkipping As Boolean = True
    Private _validateResponses As Boolean = False



    <XmlAttribute("maxAttempts")> _
    Public Property MaxAttempts As Integer
        Get
            Return _maxAttempts
        End Get
        Set
            _maxAttempts = value
            OnPropertyChanged(New PropertyChangedEventArgs("MaxAttempts"))
        End Set
    End Property

    <XmlIgnore> _
    Public Property MaxAttemptsSpecified As Boolean

    <XmlAttribute("showFeedback")> _
    Public Property ShowFeedback As Boolean
        Get
            Return _showFeedback
        End Get
        Set
            _showFeedback = value
            OnPropertyChanged(New PropertyChangedEventArgs("ShowFeedback"))
        End Set
    End Property

    <XmlIgnore> _
    Public Property ShowFeedbackSpecified As Boolean


    <XmlAttribute("allowReview")> _
    Public Property AllowReview As Boolean
        Get
            Return _allowReview
        End Get
        Set
            _allowReview = value
            OnPropertyChanged(New PropertyChangedEventArgs("AllowReview"))
        End Set
    End Property

    <XmlIgnore> _
    Public Property AllowReviewSpecified As Boolean

    <XmlAttribute("showSolution")> _
    Public Property ShowSolution As Boolean
        Get
            Return _showSolution
        End Get
        Set
            _showSolution = value
            OnPropertyChanged(New PropertyChangedEventArgs("ShowSolution"))
        End Set
    End Property

    <XmlIgnore> _
    Public Property ShowSolutionSpecified As Boolean

    <XmlAttribute("allowComment")> _
    Public Property AllowComment As Boolean
        Get
            Return _allowComment
        End Get
        Set
            _allowComment = value
            OnPropertyChanged(New PropertyChangedEventArgs("AllowComment"))
        End Set
    End Property

    <XmlIgnore> _
    Public Property AllowCommentSpecified As Boolean

    <XmlAttribute("allowSkipping")> _
    Public Property AllowSkipping As Boolean
        Get
            Return _allowSkipping
        End Get
        Set
            _allowSkipping = value
            OnPropertyChanged(New PropertyChangedEventArgs("AllowSkipping"))
        End Set
    End Property

    <XmlIgnore> _
    Public Property AllowSkippingSpecified As Boolean

    <XmlAttribute("validateResponses")> _
    Public Property ValidateResponses As Boolean
        Get
            Return _validateResponses
        End Get
        Set
            _validateResponses = value
            OnPropertyChanged(New PropertyChangedEventArgs("ValidateResponses"))
        End Set
    End Property





    Protected Overridable Sub OnPropertyChanged(e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub



    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged


    Public Sub New()
    End Sub

End Class

