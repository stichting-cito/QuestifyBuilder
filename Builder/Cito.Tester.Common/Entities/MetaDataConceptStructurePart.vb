

Public Class MetaDataConceptStructurePart
    Inherits MetaDataConceptStructure


    Private _conceptTypeId As Integer



    Public Property ConceptTypeId() As Integer
        Get
            Return _conceptTypeId
        End Get
        Set(value As Integer)
            _conceptTypeId = value
        End Set
    End Property




    Public Sub New()
        MyBase.New()
    End Sub


End Class
