Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Logic.Service.Factories
Imports Questify.Builder.Logic.CustomInteractions
Imports Cito.Tester.Common
Imports System.IO
Imports System.Linq
Imports System.Windows
Imports Questify.Builder.Model.ContentModel.FactoryClasses

Public Class CustomInterActionDimensionsHelper

    Public Sub New(resourceEntity As ResourceEntity, resourceParameter As CustomInteractionResourceParameter)
        _resourceEntity = resourceEntity
        _resourceParameter = resourceParameter
    End Sub

    Public Sub New()
        _resourceParameter = New CustomInteractionResourceParameter()
    End Sub

    Public ReadOnly Property CiResourceParameter As CustomInteractionResourceParameter
        Get
            Return _resourceParameter
        End Get
    End Property

    Private ReadOnly _resourceParameter As CustomInteractionResourceParameter
    Private ReadOnly _resourceEntity As ResourceEntity
    Private _enableScorableCheckbox As Boolean

    Public Function GetCiPropertiesAndShouldEnableScoring(fileName As String) As Boolean
        Dim size As System.Drawing.Size = GetCiPropertiesAndSize(fileName)
        Return _enableScorableCheckbox
    End Function

    Public Function GetCiDimensions(fileName As String) As System.Drawing.Size
        Return GetCiPropertiesAndSize(fileName)
    End Function

    Public Function GetMetadata() As MetadataRoot
        Dim filename = SaveCustomInteractionResourceToTempLocation(_resourceParameter.Resource)
        Dim metadata As MetadataRoot = New CiPackageValidator(filename).GetMetaData()
        Return metadata
    End Function

    Private Function GetCiPropertiesAndSize(fileName As String) As System.Drawing.Size
        Dim metaData As MetadataRoot = Nothing
        If Not String.IsNullOrEmpty(fileName) AndAlso ValidateCustomInteractionResourceIsOfTypeCi(fileName, metaData) Then
            _resourceParameter.Scorable = CustomInteractionResourceIsScorable(metaData)
            _enableScorableCheckbox = False
            If metaData IsNot Nothing Then
                _resourceParameter.CommunicationType = CType([Enum].Parse(GetType(CustomInteractionResourceParameter.CustomInteractionCommunicationType), metaData.CommunicationType.ToString()), CustomInteractionResourceParameter.CustomInteractionCommunicationType)
                SetCustomInteractionResourceDimensions(metaData)
            End If
        Else
            _resourceParameter.Scorable = True
            _enableScorableCheckbox = True
        End If
        If metaData IsNot Nothing Then
            Return New System.Drawing.Size(metaData.Width, metaData.Height)
        Else
            Return Nothing
        End If
    End Function

    Public Function ValidateCustomInteractionResourceIsCiOrPci() As Boolean
        If _resourceParameter IsNot Nothing AndAlso Not String.IsNullOrEmpty(_resourceParameter.Value) Then
            Dim fileName As String = SaveCustomInteractionResourceToTempLocation(_resourceParameter.Value)
            If Not String.IsNullOrEmpty(fileName) Then
                Return ValidateCustomInteractionResourceIsCiOrPci(fileName)
            End If
        End If
        Return False
    End Function

    Private Function ValidateCustomInteractionResourceIsCiOrPci(fileName As String) As Boolean
        Dim packageValidator = New CiPackageValidator(fileName)
        Return packageValidator.FileIsCi(New List(Of String)) OrElse packageValidator.FileIsPci(New List(Of String))
    End Function

    Public Function ValidateCustomInteractionResourceIsOfTypeCi(fileName As String, ByRef metaData As MetadataRoot) As Boolean
        Dim packageValidator = New CiPackageValidator(fileName)
        Return packageValidator.FileIsCi(New List(Of String), metaData) OrElse packageValidator.FileIsPci(New List(Of String), metaData)
    End Function

    Public Function CustomInteractionResourceIsScorable(fileName As String, ByRef metaData As MetadataRoot) As Boolean
        Dim packageValidator = New CiPackageValidator(fileName)
        metaData = packageValidator.GetMetaData()
        If metaData IsNot Nothing Then
            Return CustomInteractionResourceIsScorable(metaData)
        End If
        Return True
    End Function

    Public Function CustomInteractionResourceIsScorable(metaData As MetadataRoot) As Boolean
        Return (metaData.CommunicationType = CommunicationType.Answer)
    End Function

    Public Sub SetCustomInteractionResourceDimensions(metaData As MetadataRoot)
        If metaData IsNot Nothing Then
            If metaData.Width > 0 AndAlso metaData.Height > 0 Then
                _resourceParameter.Width = metaData.Width
                _resourceParameter.Height = metaData.Height
            End If
        End If
    End Sub

    Private Function SaveCustomInteractionResourceToTempLocation(resourceName As String) As String
        Dim resource = ResourceFactory.Instance.GetResourceByNameWithOption(_resourceEntity.BankId, resourceName, New ResourceRequestDTO())
        Return SaveCustomInteractionResourceToTempLocation(resource)
    End Function

    Public Function SaveCustomInteractionResourceToTempLocation(resourceIdentifier As Guid) As String
        Dim resource = ResourceFactory.Instance.GetResourceByIdWithOption(resourceIdentifier, New GenericResourceEntityFactory(), New ResourceRequestDTO())
        Return SaveCustomInteractionResourceToTempLocation(resource)
    End Function

    Private Function SaveCustomInteractionResourceToTempLocation(resource As Byte()) As String
        Try
            If resource?.Length > 0 Then
                Dim fileName As String = TempStorageHelper.GetTempFilename()
                File.WriteAllBytes(fileName, resource)
                Return fileName
            End If
        Catch
        End Try

        Return String.Empty
    End Function

    Private Function SaveCustomInteractionResourceToTempLocation(resource As ResourceEntity) As String
        Dim fileName As String = TempStorageHelper.GetTempFilename()
        Try
            Dim resourceDataEntity As ResourceDataEntity
            If resource.ResourceData Is Nothing Then
                resourceDataEntity = ResourceFactory.Instance.GetResourceData(resource)
            Else
                resourceDataEntity = resource.ResourceData
            End If
            Debug.Assert(resourceDataEntity IsNot Nothing)
            File.WriteAllBytes(fileName, resourceDataEntity.BinData)
            Return fileName
        Catch ex As Exception
            MessageBox.Show($"An error occured while trying to save the resource file to location {fileName}: {ex.Message}")
            Return String.Empty
        End Try
    End Function

End Class
