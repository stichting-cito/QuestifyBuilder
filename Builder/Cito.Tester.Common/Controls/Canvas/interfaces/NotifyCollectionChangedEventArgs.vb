Namespace Controls.Canvas
    Public Class NotifyCollectionChangedEventArgs
        Inherits EventArgs


        Private _action As NotifyCollectionChangedAction

        Private _newItems As IList

        Private _oldItems As IList

        Private _newStartingIndex As Integer = -1

        Private _oldStartingIndex As Integer = -1



        Public Sub New(action As NotifyCollectionChangedAction)
            If action <> NotifyCollectionChangedAction.Reset Then
                Throw New ArgumentException("WrongActionForCtor", "action")
            End If
            Me.InitializeAdd(action, Nothing, -1)
        End Sub

        Public Sub New(action As NotifyCollectionChangedAction, changedItem As Object)
            If action <> NotifyCollectionChangedAction.Add AndAlso action <> NotifyCollectionChangedAction.Remove AndAlso action <> NotifyCollectionChangedAction.Reset Then
                Throw New ArgumentException("MustBeResetAddOrRemoveActionForCtor", "action")
            End If
            If action <> NotifyCollectionChangedAction.Reset Then
                Me.InitializeAddOrRemove(action, New Object() {changedItem}, -1)
                Return
            End If
            If changedItem IsNot Nothing Then
                Throw New ArgumentException("ResetActionRequiresNullItem", "action")
            End If
            Me.InitializeAdd(action, Nothing, -1)
        End Sub

        Public Sub New(action As NotifyCollectionChangedAction, changedItem As Object, index As Integer)
            If action <> NotifyCollectionChangedAction.Add AndAlso action <> NotifyCollectionChangedAction.Remove AndAlso action <> NotifyCollectionChangedAction.Reset Then
                Throw New ArgumentException("MustBeResetAddOrRemoveActionForCtor", "action")
            End If
            If action <> NotifyCollectionChangedAction.Reset Then
                Me.InitializeAddOrRemove(action, New Object() {changedItem}, index)
                Return
            End If
            If changedItem IsNot Nothing Then
                Throw New ArgumentException("ResetActionRequiresNullItem", "action")
            End If
            If index <> -1 Then
                Throw New ArgumentException("ResetActionRequiresIndexMinus1", "action")
            End If
            Me.InitializeAdd(action, Nothing, -1)
        End Sub

        Public Sub New(action As NotifyCollectionChangedAction, changedItems As IList)
            If action <> NotifyCollectionChangedAction.Add AndAlso action <> NotifyCollectionChangedAction.Remove AndAlso action <> NotifyCollectionChangedAction.Reset Then
                Throw New ArgumentException("MustBeResetAddOrRemoveActionForCtor", "action")
            End If
            If action = NotifyCollectionChangedAction.Reset Then
                If changedItems IsNot Nothing Then
                    Throw New ArgumentException("ResetActionRequiresNullItem", "action")
                End If
                Me.InitializeAdd(action, Nothing, -1)
                Return
            Else
                If changedItems Is Nothing Then
                    Throw New ArgumentNullException("changedItems")
                End If
                Me.InitializeAddOrRemove(action, changedItems, -1)
                Return
            End If
        End Sub

        Public Sub New(action As NotifyCollectionChangedAction, changedItems As IList, startingIndex As Integer)
            If action <> NotifyCollectionChangedAction.Add AndAlso action <> NotifyCollectionChangedAction.Remove AndAlso action <> NotifyCollectionChangedAction.Reset Then
                Throw New ArgumentException("MustBeResetAddOrRemoveActionForCtor", "action")
            End If
            If action = NotifyCollectionChangedAction.Reset Then
                If changedItems IsNot Nothing Then
                    Throw New ArgumentException("ResetActionRequiresNullItem", "action")
                End If
                If startingIndex <> -1 Then
                    Throw New ArgumentException("ResetActionRequiresIndexMinus1", "action")
                End If
                Me.InitializeAdd(action, Nothing, -1)
                Return
            Else
                If changedItems Is Nothing Then
                    Throw New ArgumentNullException("changedItems")
                End If
                If startingIndex < -1 Then
                    Throw New ArgumentException("IndexCannotBeNegative", "startingIndex")
                End If
                Me.InitializeAddOrRemove(action, changedItems, startingIndex)
                Return
            End If
        End Sub

        Public Sub New(action As NotifyCollectionChangedAction, newItem As Object, oldItem As Object)
            If action <> NotifyCollectionChangedAction.Replace Then
                Throw New ArgumentException("WrongActionForCtor", "action")
            End If
            Me.InitializeMoveOrReplace(action, New Object() {newItem}, New Object() {oldItem}, -1, -1)
        End Sub

        Public Sub New(action As NotifyCollectionChangedAction, newItem As Object, oldItem As Object, index As Integer)
            If action <> NotifyCollectionChangedAction.Replace Then
                Throw New ArgumentException("WrongActionForCtor", "action")
            End If
            Me.InitializeMoveOrReplace(action, New Object() {newItem}, New Object() {oldItem}, index, index)
        End Sub

        Public Sub New(action As NotifyCollectionChangedAction, newItems As IList, oldItems As IList)
            If action <> NotifyCollectionChangedAction.Replace Then
                Throw New ArgumentException("WrongActionForCtor", "action")
            End If
            If newItems Is Nothing Then
                Throw New ArgumentNullException("newItems")
            End If
            If oldItems Is Nothing Then
                Throw New ArgumentNullException("oldItems")
            End If
            Me.InitializeMoveOrReplace(action, newItems, oldItems, -1, -1)
        End Sub

        Public Sub New(action As NotifyCollectionChangedAction, newItems As IList, oldItems As IList, startingIndex As Integer)
            If action <> NotifyCollectionChangedAction.Replace Then
                Throw New ArgumentException("WrongActionForCtor", "action")
            End If
            If newItems Is Nothing Then
                Throw New ArgumentNullException("newItems")
            End If
            If oldItems Is Nothing Then
                Throw New ArgumentNullException("oldItems")
            End If
            Me.InitializeMoveOrReplace(action, newItems, oldItems, startingIndex, startingIndex)
        End Sub

        Public Sub New(action As NotifyCollectionChangedAction, changedItem As Object, index As Integer, oldIndex As Integer)
            If action <> NotifyCollectionChangedAction.Move Then
                Throw New ArgumentException("WrongActionForCtor", "action")
            End If
            If index < 0 Then
                Throw New ArgumentException("IndexCannotBeNegative", "index")
            End If
            Dim _array() As Object = New Object() {changedItem}
            Me.InitializeMoveOrReplace(action, _array, _array, index, oldIndex)
        End Sub

        Public Sub New(action As NotifyCollectionChangedAction, changedItems As IList, index As Integer, oldIndex As Integer)
            If action <> NotifyCollectionChangedAction.Move Then
                Throw New ArgumentException("WrongActionForCtor", "action")
            End If
            If index < 0 Then
                Throw New ArgumentException("IndexCannotBeNegative", "index")
            End If
            Me.InitializeMoveOrReplace(action, changedItems, changedItems, index, oldIndex)
        End Sub

        Public ReadOnly Property Action() As NotifyCollectionChangedAction
            Get
                Return Me._action
            End Get
        End Property

        Public ReadOnly Property NewItems() As IList
            Get
                Return Me._newItems
            End Get
        End Property

        Public ReadOnly Property OldItems() As IList
            Get
                Return Me._oldItems
            End Get
        End Property

        Public ReadOnly Property NewStartingIndex() As Integer
            Get
                Return Me._newStartingIndex
            End Get
        End Property

        Public ReadOnly Property OldStartingIndex() As Integer
            Get
                Return Me._oldStartingIndex
            End Get
        End Property


        Private Sub InitializeAddOrRemove(action As NotifyCollectionChangedAction, changedItems As IList, startingIndex As Integer)
            If action = NotifyCollectionChangedAction.Add Then
                Me.InitializeAdd(action, changedItems, startingIndex)
                Return
            End If
            If action = NotifyCollectionChangedAction.Remove Then
                Me.InitializeRemove(action, changedItems, startingIndex)
            End If
        End Sub

        Private Sub InitializeAdd(action As NotifyCollectionChangedAction, newItems As IList, newStartingIndex As Integer)
            Me._action = action
            Me._newItems = (If((newItems Is Nothing), Nothing, ArrayList.[ReadOnly](newItems)))
            Me._newStartingIndex = newStartingIndex
        End Sub

        Private Sub InitializeRemove(action As NotifyCollectionChangedAction, oldItems As IList, oldStartingIndex As Integer)
            Me._action = action
            Me._oldItems = (If((oldItems Is Nothing), Nothing, ArrayList.[ReadOnly](oldItems)))
            Me._oldStartingIndex = oldStartingIndex
        End Sub

        Private Sub InitializeMoveOrReplace(action As NotifyCollectionChangedAction, newItems As IList, oldItems As IList, startingIndex As Integer, oldStartingIndex As Integer)
            Me.InitializeAdd(action, newItems, startingIndex)
            Me.InitializeRemove(action, oldItems, oldStartingIndex)
        End Sub

    End Class
End Namespace

