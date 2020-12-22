Imports System.ComponentModel
Imports System.Xml.Serialization
Imports System.Linq

<Serializable> _
<XmlRoot("Template")> _
Public MustInherit Class TemplateBase(Of templateTarget As TargetBase)
    Implements INotifyPropertyChanged


    Private _description As String
    Private _targets As New TargetCollection(Of templateTarget)
    Private _templateDefinitionVersion As Integer = 1
    Private _isTransformed As Boolean = False



    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged



    <XmlIgnore> _
    Public Property IsTransformed As Boolean
        Get
            Return _isTransformed
        End Get
        Set
            _isTransformed = value
        End Set
    End Property

    <XmlElement("Description")>
    Public Property Description As String
        Get
            Return _description
        End Get
        Set
            _description = Value
        End Set
    End Property

    <XmlArray("Targets")>
    <XmlArrayItem("Target")>
    Public ReadOnly Property Targets As TargetCollection(Of templateTarget)
        Get
            Return _targets
        End Get
    End Property


    <XmlAttribute("definitionVersion")> _
    Public Property TemplateDefinitionVersion As Integer
        Get
            Return _templateDefinitionVersion
        End Get
        Set
            _templateDefinitionVersion = value
        End Set
    End Property



    Public Function GetEnabledTargetNames() As String()
        Return GetEnabledTargets().Select(Function(target) target.Name).ToArray()
    End Function

    Public Function GetEnabledTargets() As TargetCollection(Of templateTarget)
        Dim enabledTargets = Me.Targets.Where(Function(target) target.Enabled)

        Dim result = New TargetCollection(Of templateTarget)()
        result.AddRange(enabledTargets)

        Return result
    End Function


    Public Function TargetExists(targetName As String) As Boolean
        Return Me.Targets.HasTarget(targetName)
    End Function

    Public Function GetEnabledTargetByName(targetName As String) As templateTarget
        Dim returnValue As templateTarget = Nothing
        If Me.Targets.HasTarget(targetName) AndAlso Me.Targets.Item(targetName).Enabled Then
            returnValue = Me.Targets.Item(targetName)
        End If

        Return returnValue
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


End Class