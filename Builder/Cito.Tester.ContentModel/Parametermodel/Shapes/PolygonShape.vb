Imports System.Drawing
Imports System.Xml.Serialization
Imports System.Linq

Public Class PolygonShape
    Inherits Shape

    Private _coords As List(Of Point)
    Private _anchorPoint As Point
    Private _height As Integer
    Private _width As Integer
    Private _boxPropertiesSet As Boolean


    <XmlArray("coords")>
    Public Property Coords As List(Of Point)
        Get
            Return _coords
        End Get
        Set
            _coords = value
            _boxPropertiesSet = False
            SetBoxProperties()
        End Set
    End Property

    Public ReadOnly Property AnchorPoint As Point
        Get
            SetBoxProperties()
            Return _anchorPoint
        End Get
    End Property

    Public ReadOnly Property Width As Integer
        Get
            SetBoxProperties()
            Return _width
        End Get
    End Property

    Public ReadOnly Property Height As Integer
        Get
            SetBoxProperties()
            Return _height
        End Get
    End Property

    Public Sub New()
        _coords = New List(Of Point)
        _anchorPoint = New Point()
    End Sub

    Private Sub SetBoxProperties()
        If Not _boxPropertiesSet Then
            Dim minX As Integer = _coords.Min(Function(p) p.X)
            Dim minY As Integer = _coords.Min(Function(p) p.Y)
            Dim maxX As Integer = _coords.Max(Function(p) p.X)
            Dim maxY As Integer = _coords.Max(Function(p) p.Y)
            _anchorPoint = New Point(minX, minY)
            _width = maxX - minX
            _height = maxY - minY
            _boxPropertiesSet = True
        End If
    End Sub


End Class

