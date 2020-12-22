

Public Class BinaryResource

    Private _name As String
    Private _type As String
    Private _resourceObject As Object
    Private _dependentResources As DependentResourceCollection

    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property

    Public ReadOnly Property Type() As String
        Get
            Return _type
        End Get
    End Property

    Public ReadOnly Property ResourceObject() As Object
        Get
            Return _resourceObject
        End Get
    End Property

    Public ReadOnly Property DependentResources() As DependentResourceCollection
        Get
            Return _dependentResources
        End Get
    End Property

    Protected Sub New()
        Me._dependentResources = New DependentResourceCollection
    End Sub

    Public Sub New(obj As Object)
        Me.New()
        _resourceObject = obj
    End Sub

    Public Sub New(name As String, type As String, obj As Object, dependentResources As DependentResourceCollection)
        Me.New(obj)
        Me._name = name
        Me._type = type

        If dependentResources IsNot Nothing Then
            Me._dependentResources.AddRange(dependentResources)
        End If
    End Sub

End Class