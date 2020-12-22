Imports Cito.Tester.Common.Controls.Canvas.Factory

Namespace Controls.Canvas.ShapeConstructor
    Friend MustInherit Class TwoPointConstructorbase(Of T As IShape)
        Implements IShapeConstructor(Of T)

        Private _p1 As Point?
        Private _p2 As Point?
        Private _shape As T
        Private _drawing As IDrawableItem

        Public Sub New(shapeFactory As IDrawableShapeFactory)
            _drawing = shapeFactory.CreateShape(Of T)()
            _shape = DirectCast(_drawing, T)
        End Sub

        Friend ReadOnly Property p1 As Point?
            Get
                Return _p1
            End Get
        End Property

        Friend ReadOnly Property p2 As Point?
            Get
                Return _p2
            End Get
        End Property


        Public ReadOnly Property CanHandleInfinitivePoints As Boolean Implements IShapeConstructor(Of T).CanHandleInfinitivePoints
            Get
                Return False
            End Get
        End Property

        Public Function CommitLastPoint(p As Point) As Boolean Implements IShapeConstructor(Of T).CommitLastPoint
            Return False
        End Function

        Public Function CommitPoint(p As Point) As Boolean Implements IShapeConstructor(Of T).CommitPoint
            If (Not _p1.HasValue) Then
                _p1 = p
            ElseIf (Not _p2.HasValue) Then
                _p2 = p
            Else
                Throw New InvalidOperationException()
            End If
            SetShape(Nothing)
            Return _p2.HasValue
        End Function

        Public ReadOnly Property Shape As T Implements IShapeConstructor(Of T).Shape
            Get
                Return _shape
            End Get
        End Property

        Public ReadOnly Property Drawing As IDrawableItem Implements IShapeConstructor(Of T).Drawing
            Get
                Return _drawing
            End Get
        End Property

        Public Sub SuggestPoint(p As Point) Implements IShapeConstructor(Of T).SuggestPoint
            SetShape(p)
        End Sub

        Friend MustOverride Sub SetShape(pSuggested As Point?)
    End Class
End Namespace