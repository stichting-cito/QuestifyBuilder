Imports Questify.Builder.Logic
Imports Questify.Builder.Logic.PluginExtensibility.Html.Handlers.Logic

Namespace Dialogs.BusinessLogic

    Public Class BorderAndShadingPresenter

        Private _model As TableStyleDto
        Private ReadOnly _view As IBordersAndShadingView

        Private _topChecked As Boolean = True
        Private _midHorizontalChecked As Boolean = True
        Private _bottomChecked As Boolean = True
        Private _leftChecked As Boolean = True
        Private _midVerticalChecked As Boolean = True
        Private _rightChecked As Boolean = True

        Private _currentLineStyle As LineStyle = LineStyle.Solid
        Private _currentLineWidth As Integer = 1

        Private _styleStrategy As baseTableStyleStrategy
        Private _IsWorking As Boolean

        Public Sub New(view As IBordersAndShadingView, model As TableStyleDto)
            _view = view
            _model = model
            DetermineTableStrategy()
        End Sub


        Public Property Style As TableStyleDto
            Get
                Return _model
            End Get
            Set(value As TableStyleDto)
                _model = value
            End Set
        End Property

        Public Property TopChecked() As Boolean
            Get
                Return _topChecked
            End Get
            Set(ByVal value As Boolean)
                If (value <> _topChecked) Then
                    _topChecked = value
                    Update()
                End If
            End Set
        End Property


        Public Property MidHorizontalChecked() As Boolean
            Get
                Return _midHorizontalChecked
            End Get
            Set(ByVal value As Boolean)
                If (value <> _midHorizontalChecked) Then
                    _midHorizontalChecked = value
                    Update()
                End If
            End Set
        End Property


        Public Property BottomChecked() As Boolean
            Get
                Return _bottomChecked
            End Get
            Set(ByVal value As Boolean)
                If (value <> _bottomChecked) Then
                    _bottomChecked = value
                    Update()
                End If
            End Set
        End Property

        Public Property LeftChecked() As Boolean
            Get
                Return _leftChecked
            End Get
            Set(ByVal value As Boolean)
                If (value <> _leftChecked) Then
                    _leftChecked = value
                    Update()
                End If
            End Set
        End Property

        Public Property MidVerticalChecked() As Boolean
            Get
                Return _midVerticalChecked
            End Get
            Set(ByVal value As Boolean)
                If (value <> _midVerticalChecked) Then
                    _midVerticalChecked = value
                    Update()
                End If
            End Set
        End Property


        Public Property RightChecked() As Boolean
            Get
                Return _rightChecked
            End Get
            Set(ByVal value As Boolean)
                If (value <> _rightChecked) Then
                    _rightChecked = value
                    Update()
                End If
            End Set
        End Property

        Friend ReadOnly Property CurrentTableStyleStrategy As String
            Get
                Return _styleStrategy.StrategyName
            End Get
        End Property

        Property CurrentLineStyle As LineStyle
            Get
                Return _currentLineStyle
            End Get
            Set(value As LineStyle)
                _currentLineStyle = value
                Update()
            End Set
        End Property

        Public Property CurrentLineWidth As Integer
            Get
                Return _currentLineWidth
            End Get
            Set(value As Integer)
                _currentLineWidth = value
                Update()
            End Set
        End Property

        Property SelectedBackgroundColor As Color?
            Get
                Return _model.BackColor
            End Get
            Set(value As Color?)
                _model.BackColor = value
                Update()
            End Set
        End Property

        Property SelectedLineColor As Color?
            Get
                Return _model.LineColor
            End Get
            Set(value As Color?)
                _model.LineColor = value
                Update()
            End Set
        End Property

        Public Sub SetBorderStrategy(name As String)
            Dim toSet As baseTableStyleStrategy = Nothing
            Select Case name
                Case "none"
                    toSet = TableStyleFactory.NonBorders(Me)
                Case "box"
                    toSet = TableStyleFactory.Outline(Me)
                Case "all"
                    toSet = TableStyleFactory.All(Me)
                Case "grid"
                    toSet = TableStyleFactory.Grid(Me)
                Case "custom"
                    toSet = TableStyleFactory.GetDefaultTableStyleStrategy(Me)
                Case Else

            End Select

            Debug.Assert(toSet IsNot Nothing)
            _IsWorking = True
            _styleStrategy = toSet
            _styleStrategy.InitiateState()
            toSet.CalculateNewStyle()
            _IsWorking = False
            Update()
        End Sub

        Public Sub Update()
            If Not _IsWorking Then

                If Not _styleStrategy.CanHandleState() Then
                    _styleStrategy = TableStyleFactory.GetDefaultTableStyleStrategy(Me)
                End If
                _styleStrategy.CalculateNewStyle()
                _styleStrategy = TableStyleFactory.GetFactoryBasedOnStyle(_model, Me)

                _view.SetStrategyDisplay(_styleStrategy.StrategyName)
                _view.InvalidateExample()

            End If
        End Sub

        Private Sub DetermineTableStrategy()
            _styleStrategy = TableStyleFactory.GetFactoryBasedOnStyle(_model, Me)
            _styleStrategy.GetDefaultSettings(_currentLineStyle, _currentLineWidth)
            _IsWorking = True
            _styleStrategy.InitiateState()
            _IsWorking = False
        End Sub

    End Class

End Namespace
