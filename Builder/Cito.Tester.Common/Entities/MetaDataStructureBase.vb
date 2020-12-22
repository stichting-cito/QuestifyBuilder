Public MustInherit Class MetaDataStructureBase
    Inherits MetaData


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(name As String, value As String, type As enumMetaDataType)
        MyBase.New(name, value, type)
    End Sub



    Dim _structureParts As New MetaDataCollection
    Dim _code As Guid
    Dim _visualOrder As Integer



    Public ReadOnly Property StructureParts() As MetaDataCollection
        Get
            Return _structureParts
        End Get
    End Property

    Public Property Code() As Guid
        Get
            Return _code
        End Get
        Set(value As Guid)
            _code = value
        End Set
    End Property

    Public Property VisualOrder As Integer
        Get
            Return _visualOrder
        End Get
        Set(value As Integer)
            _visualOrder = value
        End Set
    End Property




    Private Function ContainsValue(valueName As String) As Boolean
        Dim result As Boolean = False

        For Each md As MetaData In Me.StructureParts
            If md.Name.Equals(valueName, StringComparison.InvariantCultureIgnoreCase) Then
                result = True
                Exit For
            End If
        Next

        Return result
    End Function


End Class
