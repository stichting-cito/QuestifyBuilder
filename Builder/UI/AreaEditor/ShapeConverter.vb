Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common.Controls.Canvas.Factory
Imports Cito.Tester.Common.Controls.Canvas

Friend Class ShapeConverter

    Private _shapeFactory As IDrawableShapeFactory

    Public Sub New(ByVal shapeFactory As IDrawableShapeFactory)
        _shapeFactory = shapeFactory
    End Sub

    Public Function ToDrawing(shape As Shape, Optional relabel As Boolean = False) As IDrawableItem
        Dim ret As IDrawableItem = Nothing
        WhenObject(shape,
                      IsType(Of CircleShape)(Sub()
                                                 Dim s As CircleShape = DirectCast(shape, CircleShape)
                                                 If relabel Then
                                                     ret = _shapeFactory.CreateShape(Of ICircle)()
                                                 ElseIf s.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.Label) Then
                                                     ret = _shapeFactory.CreateShape(Of ICircle)(s.Identifier, s.Label)
                                                 Else
                                                     ret = _shapeFactory.CreateShape(Of ICircle)(s.Identifier)
                                                 End If
                                                 Dim t As ICircle = DirectCast(ret, ICircle)
                                                 t.AnchorPoint = s.AnchorPoint
                                                 t.Radius = s.Radius
                                             End Sub),
                      IsType(Of EllipseShape)(Sub()
                                                  Dim s As EllipseShape = DirectCast(shape, EllipseShape)
                                                  If relabel Then
                                                      ret = _shapeFactory.CreateShape(Of IEllipse)()
                                                  ElseIf s.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.Label) Then
                                                      ret = _shapeFactory.CreateShape(Of IEllipse)(s.Identifier, s.Label)
                                                  Else
                                                      ret = _shapeFactory.CreateShape(Of IEllipse)(s.Identifier)
                                                  End If
                                                  Dim t As IEllipse = DirectCast(ret, IEllipse)
                                                  t.AnchorPoint = s.AnchorPoint
                                                  t.VRadius = s.VRadius
                                                  t.HRadius = s.HRadius
                                              End Sub),
                      IsType(Of RectangleShape)(Sub()
                                                    Dim s As RectangleShape = DirectCast(shape, RectangleShape)
                                                    If relabel Then
                                                        ret = _shapeFactory.CreateShape(Of IRectangle)()
                                                    ElseIf s.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.Label) Then
                                                        ret = _shapeFactory.CreateShape(Of IRectangle)(s.Identifier, s.Label)
                                                    Else
                                                        ret = _shapeFactory.CreateShape(Of IRectangle)(s.Identifier)
                                                    End If
                                                    Dim t As IRectangle = DirectCast(ret, IRectangle)
                                                    t.Left = s.TopLeft.X
                                                    t.Top = s.TopLeft.Y
                                                    t.Right = s.BottomRight.X
                                                    t.Bottom = s.BottomRight.Y
                                                End Sub),
                   IsType(Of PointUpTriangleShape)(Sub()
                                                       Dim s As PointUpTriangleShape = DirectCast(shape, PointUpTriangleShape)
                                                       If relabel Then
                                                           ret = _shapeFactory.CreateShape(Of IPointUpTriangle)()
                                                       ElseIf s.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.Label) Then
                                                           ret = _shapeFactory.CreateShape(Of IPointUpTriangle)(s.Identifier, s.Label)
                                                       Else
                                                           ret = _shapeFactory.CreateShape(Of IPointUpTriangle)(s.Identifier)
                                                       End If
                                                       Dim t As IPointUpTriangle = DirectCast(ret, IPointUpTriangle)
                                                       t.Left = s.BaseLeft.X
                                                       t.Right = s.BaseRight.X
                                                       t.Top = s.Top.Y
                                                       t.Bottom = s.BaseLeft.Y
                                                   End Sub),
                   IsType(Of PointDownTriangleShape)(Sub()
                                                         Dim s As PointDownTriangleShape = DirectCast(shape, PointDownTriangleShape)
                                                         If relabel Then
                                                             ret = _shapeFactory.CreateShape(Of IPointDownTriangle)()
                                                         ElseIf s.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.Label) Then
                                                             ret = _shapeFactory.CreateShape(Of IPointDownTriangle)(s.Identifier, s.Label)
                                                         Else
                                                             ret = _shapeFactory.CreateShape(Of IPointDownTriangle)(s.Identifier)
                                                         End If
                                                         Dim t As IPointDownTriangle = DirectCast(ret, IPointDownTriangle)
                                                         t.Left = s.BaseLeft.X
                                                         t.Right = s.BaseRight.X
                                                         t.Top = s.Top.Y
                                                         t.Bottom = s.BaseLeft.Y
                                                     End Sub),
                   IsType(Of PolygonShape)(Sub()
                                               Dim s As PolygonShape = DirectCast(shape, PolygonShape)
                                               If relabel Then
                                                   ret = _shapeFactory.CreateShape(Of IPolygon)()
                                               ElseIf s.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.Label) Then
                                                   ret = _shapeFactory.CreateShape(Of IPolygon)(s.Identifier, s.Label)
                                               Else
                                                   ret = _shapeFactory.CreateShape(Of IPolygon)(s.Identifier)
                                               End If
                                               Dim t As IPolygon = DirectCast(ret, IPolygon)
                                               t.Coordinates = s.Coords
                                           End Sub),
                    Otherwise(Sub() Debug.Assert(False)))
        Debug.Assert(ret IsNot Nothing)
        Return ret
    End Function

    Public Function ToShape(drawing As IDrawableItem) As Shape
        Dim ret As Shape = Nothing
        WhenObject(DirectCast(drawing, IShape),
                IsType(Of ICircle)(Sub()
                                       Dim s As ICircle = DirectCast(drawing, ICircle)
                                       Dim t As CircleShape = New CircleShape()
                                       t.AnchorPoint = s.AnchorPoint
                                       t.Radius = s.Radius

                                       t.Identifier = drawing.ID
                                       If s.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.Label) Then t.Label = s.Label

                                       ret = t
                                   End Sub),
                IsType(Of IEllipse)(Sub()
                                        Dim s As IEllipse = DirectCast(drawing, IEllipse)
                                        Dim t As EllipseShape = New EllipseShape()
                                        t.AnchorPoint = s.AnchorPoint
                                        t.VRadius = s.VRadius
                                        t.HRadius = s.HRadius

                                        t.Identifier = drawing.ID
                                        If s.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.Label) Then t.Label = s.Label

                                        ret = t
                                    End Sub),
                IsType(Of IRectangle)(Sub()
                                          Dim s As IRectangle = DirectCast(drawing, IRectangle)
                                          Dim t As RectangleShape = New RectangleShape()
                                          t.TopLeft = New Point(s.Left, s.Top)
                                          t.BottomRight = New Point(s.Right, s.Bottom)

                                          t.Identifier = drawing.ID
                                          If s.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.Label) Then t.Label = s.Label

                                          ret = t
                                      End Sub),
                IsType(Of IPointUpTriangle)(Sub()
                                                Dim s As IPointUpTriangle = DirectCast(drawing, IPointUpTriangle)
                                                Dim t As PointUpTriangleShape = New PointUpTriangleShape()
                                                t.BaseLeft = New Point(s.Left, s.Bottom)
                                                t.BaseRight = New Point(s.Right, s.Bottom)
                                                t.Top = New Point(s.Left + ((s.Right - s.Left) / 2), s.Top)
                                                t.Identifier = drawing.ID
                                                If s.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.Label) Then t.Label = s.Label

                                                ret = t
                                            End Sub),
                IsType(Of IPointDownTriangle)(Sub()
                                                  Dim s As IPointDownTriangle = DirectCast(drawing, IPointDownTriangle)
                                                  Dim t As PointDownTriangleShape = New PointDownTriangleShape()
                                                  t.BaseLeft = New Point(s.Left, s.Bottom)
                                                  t.BaseRight = New Point(s.Right, s.Bottom)
                                                  t.Top = New Point(s.Left + ((s.Right - s.Left) / 2), s.Top)
                                                  t.Identifier = drawing.ID
                                                  If s.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.Label) Then t.Label = s.Label

                                                  ret = t
                                              End Sub),
                IsType(Of IPolygon)(Sub()
                                        Dim s As IPolygon = DirectCast(drawing, IPolygon)
                                        Dim t As PolygonShape = New PolygonShape()
                                        t.Coords = s.Coordinates

                                        t.Identifier = drawing.ID
                                        If s.Label IsNot Nothing AndAlso Not String.IsNullOrEmpty(s.Label) Then t.Label = s.Label

                                        ret = t
                                    End Sub),
        Otherwise(Sub() Debug.Assert(False)))

        Debug.Assert(ret IsNot Nothing)
        Return ret
    End Function



End Class
