Imports Enums
Imports Questify.Builder.Model.ContentModel.Interfaces

Namespace Questify.Builder.Model.ContentModel.EntityClasses
    Partial Public Class ItemLayoutTemplateResourceEntity
        Implements IVersionable

        Private _majorVersionLabel As String

        Public Overrides ReadOnly Property ResourceType() As String
            Get
                Return My.Resources.ResourceType_ItemLayoutTemplate
            End Get
        End Property


        Public ReadOnly Property ItemTypeString() As String
            Get
                Dim returnValue As String = String.Empty
                If Not String.IsNullOrEmpty(Me.ItemType) AndAlso [Enum].IsDefined(GetType(ItemTypeEnum), Me.ItemType) Then
                    returnValue = Cito.Tester.Common.ResourceEnumConverter.ConvertToString(DirectCast([Enum].Parse(GetType(ItemTypeEnum), Me.ItemType), ItemTypeEnum))
                Else
                    returnValue = Me.ItemType
                End If
                Return returnValue
            End Get
        End Property


        Public Property MajorVersionLabel As String Implements IVersionable.MajorVersionLabel
            Get
                Return _majorVersionLabel
            End Get
            Set(value As String)
                _majorVersionLabel = value
            End Set
        End Property

        Public Overrides Property Version() As String Implements IVersionable.Version
            Get
                Return MyBase.Version
            End Get
            Set(value As String)
                MyBase.Version = Value
            End Set
        End Property

        Public ReadOnly Property SaveObjectAsBinary As Boolean Implements IVersionable.SaveObjectAsBinary
            Get
                Return True
            End Get
        End Property

    End Class
End Namespace
