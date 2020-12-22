Imports System.Xml.Serialization

Public Class AreaParameter
    Inherits CollectionParameter

    Private WithEvents _shapeList As New ShapeList

    <XmlArray("Shapes")>
    <XmlArrayItem("Rectangle", GetType(RectangleShape))>
    <XmlArrayItem("Circle", GetType(CircleShape))>
    <XmlArrayItem("Ellipse", GetType(EllipseShape))>
    <XmlArrayItem("Polygon", GetType(PolygonShape))>
    <XmlArrayItem("PointDownTriangle", GetType(PointDownTriangleShape))>
    <XmlArrayItem("PointUpTriangle", GetType(PointUpTriangleShape))>
    Public Property ShapeList As ShapeList
        Get
            Return _shapeList
        End Get
        Set
            If (value Is Nothing) Then Throw New ArgumentNullException(My.Resources.Error_ChoiceCollectionParameter_Value_SetValueNotAllowed)
            _shapeList = value

            AddHandler _shapeList.ListChanged, Sub() NotifyPropertyChanged("ShapeList")
        End Set
    End Property

End Class
