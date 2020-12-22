Imports Questify.Builder.Logic.Annotations
Imports System.ComponentModel
Imports System.Linq
Imports System.Runtime.CompilerServices

Namespace ItemEditor
    <DebuggerDisplay("{Name}    visible:{IsVisible}    hasVisibleParameters:{HasVisibleParameters}")>
    Friend Class Group
        Implements IGroup
        Private _isVisible As Boolean = True
        Private ReadOnly _ownedParameter As New List(Of IParameterEvaluator)()
        Private ReadOnly _groupConditionalEnabled As List(Of IGroupConditionalEnabled)

        Public Sub New(<NotNull> nameParam As String)
            Name = nameParam
            HasVisibleParameters = False
            _groupConditionalEnabled = New List(Of IGroupConditionalEnabled)()
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Property Name As String Implements IGroup.Name

        Public Property HasVisibleParameters As Boolean Implements IGroup.HasVisibleParameters


        Public Property IsVisible() As Boolean Implements IGroup.IsVisible
            Get
                Return _isVisible
            End Get
            Set
                If _isVisible <> Value Then
                    _isVisible = Value
                    OnPropertyChanged()
                End If
            End Set
        End Property

        Public ReadOnly Property Parameters() As IEnumerable(Of IParameterEvaluator) Implements IGroup.Parameters
            Get
                Return _ownedParameter
            End Get
        End Property

        <NotifyPropertyChangedInvocator>
        Protected Overridable Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Public Sub AddParameter(parameter As IParameterEvaluator)
            _ownedParameter.Add(parameter)

            If (parameter.IsVisible)
                HasVisibleParameters = True
            End If
        End Sub

        Friend Sub AddInfluencers(influences As List(Of IGroupInfluence))

            For Each influence As IGroupInfluence In influences
                RegisterConditionEnabled(influence)
            Next
            EvaluateVisibility()
        End Sub
        Private Sub RegisterConditionEnabled(obj As IGroupInfluence)
            Dim groupEnabled As IGroupConditionalEnabled = TryCast(obj, IGroupConditionalEnabled)

            If groupEnabled IsNot Nothing Then
                AddHandler groupEnabled.NeedsReEvaluation, Sub() EvaluateVisibility()
                _groupConditionalEnabled.Add(groupEnabled)
            End If
        End Sub

        Private Sub EvaluateVisibility()
            IsVisible = _groupConditionalEnabled.All(Function(condition) condition.IsEnabled())
        End Sub
    End Class
End Namespace
