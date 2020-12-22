Imports Enums
Imports SD.LLBLGen.Pro.ORMSupportClasses
Imports Questify.Builder.Model.ContentModel.Interfaces

Namespace Questify.Builder.Model.ContentModel.EntityClasses
    Partial Public Class ItemResourceEntity
        Implements IVersionable

        Private _majorVersionLabel As String
        Private _itemLayoutTemplateUsed As ItemLayoutTemplateResourceEntity = Nothing

        Public Overrides ReadOnly Property ResourceType() As String
            Get
                Return "Item"
            End Get
        End Property


        Public ReadOnly Property ItemLayoutTemplateUsedName() As String
            Get
                Dim itemLayoutTemplateResourceEntity As ItemLayoutTemplateResourceEntity = GetItemLayoutTemplateFromDependencies()
                If itemLayoutTemplateResourceEntity IsNot Nothing Then
                    Return _itemLayoutTemplateUsed.Name
                End If
                Return String.Empty
            End Get
        End Property

        Public Overrides Property Version() As String Implements IVersionable.Version
            Get
                Return MyBase.Version
            End Get
            Set(value As String)
                MyBase.Version = value
            End Set
        End Property

        Public Sub ReloadItemLayoutTemplateUsed()
            _itemLayoutTemplateUsed = Nothing
            GetItemLayoutTemplateFromDependencies()
        End Sub


        Private Function GetItemLayoutTemplateFromDependencies() As ItemLayoutTemplateResourceEntity
            If _itemLayoutTemplateUsed Is Nothing Then
                For Each resource As DependentResourceEntity In Me.DependentResourceCollection
                    If resource.DependentResource IsNot Nothing AndAlso
                            TypeOf resource.DependentResource Is ItemLayoutTemplateResourceEntity AndAlso
                            Not DirectCast(resource.DependentResource, ItemLayoutTemplateResourceEntity).ItemType.Equals([Enum].GetName(GetType(ItemTypeEnum), ItemTypeEnum.Error), StringComparison.InvariantCultureIgnoreCase) AndAlso
                            Not DirectCast(resource.DependentResource, ItemLayoutTemplateResourceEntity).ItemType.Equals([Enum].GetName(GetType(ItemTypeEnum), ItemTypeEnum.Inline), StringComparison.InvariantCultureIgnoreCase) Then
                        _itemLayoutTemplateUsed = DirectCast(resource.DependentResource, ItemLayoutTemplateResourceEntity)
                        Exit For
                    End If
                Next
            End If
            Return _itemLayoutTemplateUsed
        End Function

        Public ReadOnly Property ItemTypeFromItemLayoutTemplateString() As String
            Get
                Dim returnValue As String = String.Empty
                Dim itemLayoutTemplateResourceEntity As ItemLayoutTemplateResourceEntity = GetItemLayoutTemplateFromDependencies()
                If itemLayoutTemplateResourceEntity IsNot Nothing Then
                    If [Enum].IsDefined(GetType(ItemTypeEnum), itemLayoutTemplateResourceEntity.ItemType) Then
                        returnValue = Cito.Tester.Common.ResourceEnumConverter.ConvertToString(DirectCast([Enum].Parse(GetType(ItemTypeEnum), itemLayoutTemplateResourceEntity.ItemType), ItemTypeEnum))
                    Else
                        returnValue = itemLayoutTemplateResourceEntity.ItemType
                    End If
                End If

                Return returnValue
            End Get
        End Property

        Public ReadOnly Property ItemTypeFromItemLayoutTemplate() As ItemTypeEnum
            Get
                Dim returnValue As ItemTypeEnum = Nothing
                Dim itemLayoutTemplateResourceEntity As ItemLayoutTemplateResourceEntity = GetItemLayoutTemplateFromDependencies()
                If itemLayoutTemplateResourceEntity IsNot Nothing Then
                    If [Enum].IsDefined(GetType(ItemTypeEnum), itemLayoutTemplateResourceEntity.ItemType) Then
                        returnValue = DirectCast([Enum].Parse(GetType(ItemTypeEnum), itemLayoutTemplateResourceEntity.ItemType), ItemTypeEnum)
                    End If
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

        Public ReadOnly Property SaveObjectAsBinary As Boolean Implements IVersionable.SaveObjectAsBinary
            Get
                Return True
            End Get
        End Property

        Protected Overrides Sub PreProcessValueToSet(fieldToSet As IFieldInfo, ByRef valueToSet As Object)
            Select Case fieldToSet.FieldIndex
                Case ItemResourceFieldIndex.KeyValues

                    LimitStringLength(500, valueToSet)

                Case Else
            End Select

            MyBase.PreProcessValueToSet(fieldToSet, valueToSet)
        End Sub

        Private Sub LimitStringLength(maxLength As Integer, ByRef valueToSet As Object)
            If (valueToSet IsNot Nothing) Then

                Dim str As String = valueToSet.ToString()

                Dim maxLengtOfSubString = Math.Min(str.Length, maxLength)

                valueToSet = str.Substring(0, maxLengtOfSubString)
            End If

        End Sub

    End Class
End Namespace
