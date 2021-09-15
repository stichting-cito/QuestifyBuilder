
Imports System.Drawing
Imports System.Text
Imports Cito.Tester.Common
Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Questify.Builder.Model.ContentModel.HelperClasses
Imports Questify.Builder.UnitTests.Framework.Faketory
Imports Questify.Builder.UnitTests.Framework.Faketory.impl
Imports Questify.Builder.UnitTests.Framework.Faketory.interface
Imports Questify.Builder.UnitTests.Framework.My.Resources

Namespace FakeAppTemplate

    ''' <summary>
    ''' Acts as a fake data access layer for test projects.
    ''' This module is NOT bank aware!
    ''' </summary>
    Public Module FakeDal

#Region "fields"
        Private _idGen As New SequentialGuid
        Private _fakeServices As IFakeServices
        Private _addAll As IAddAll
        Private _resourceServiceHandler As FakeResourceServiceHandler
        Private _bankServiceHandler As FakeBankServiceHandler

        Private _fakeDtoRepository As IFakeDtoRepository
        Dim _genericResourceDtoRepository As FakeGenericResourceDtoRepositoryHandler
#End Region

#Region "Public Api"

        ''' <summary>
        ''' Initializes services.
        ''' </summary>
        Public Sub Init()
            _addAll = Nothing
            _fakeServices = FakeFactory.FakeServices
            _fakeServices.SetupFakeServices() 'Initialize fakes
            'Default handler for ResourceService
            _resourceServiceHandler = New FakeResourceServiceHandler(_fakeServices.FakeResourceService)
            _resourceServiceHandler.InitDefault()
            'Default handler for BankService
            _bankServiceHandler = New FakeBankServiceHandler(_fakeServices.FakeBankService)
            _bankServiceHandler.InitDefault()
        End Sub

        Public Sub InitDtoService()
            If (_resourceServiceHandler Is Nothing) Then Throw New Exception("Call Init First")
            _fakeDtoRepository = FakeFactory.FakeDtoRepository
            _fakeDtoRepository.SetupFakeServices() 'initialize fakes
            'Default handler for GenericResourceDtoRepository
            _genericResourceDtoRepository = New FakeGenericResourceDtoRepositoryHandler(_fakeDtoRepository.FakeGenericResourceDtoRepository)
        End Sub

        ''' <summary>
        ''' De-init this module.
        ''' </summary>
        Public Sub Deinit()
            _addAll = Nothing
            If (_fakeServices IsNot Nothing) Then _fakeServices.CleanFakeServices()
            _fakeServices = Nothing

            If (_fakeDtoRepository IsNot Nothing) Then _fakeDtoRepository.CleanFakeServices()
            _fakeDtoRepository = Nothing
        End Sub

        ''' <summary>
        ''' Gets the fake services.
        ''' </summary>
        Public ReadOnly Property FakeServices As IFakeServices
            Get
                Return _fakeServices
            End Get
        End Property

        ''' <summary>
        ''' Gets the faked resources
        ''' </summary>
        Public ReadOnly Property Resources As IEnumerable(Of ResourceEntity)
            Get
                Return _resourceServiceHandler.ResourceCollection.Select(Function(e) TryCast(e, ResourceEntity))
            End Get
        End Property

        ''' <summary>
        ''' Access point for Fluent interface for adding objects to the dal.
        ''' </summary>
        Public ReadOnly Property Add As IAddAll
            Get
                If _addAll Is Nothing Then
                    _addAll = New AddNoParent()
                    CanSaveResources() 'Setup stuff so we can save objects.
                End If

                Return _addAll
            End Get
        End Property

        ''' <summary>
        ''' Creates an aspect.
        ''' </summary>
        ''' <param name="title">The title.</param>
        ''' <param name="score">The score.</param>
        ''' <returns>the id of the object</returns>
        Public Function MakeAspect(title As String, score As Integer) As Guid
            Dim id = NextId()
            Dim ret = New AspectResourceEntity(id) With {.Title = title, .Name = title, .RawScore = score}
            _resourceServiceHandler.ResourceCollection.Add(ret)
        End Function

        Public Function MakeGenericResource(name As String) As Guid
            Dim id As Guid = NextId()
            _resourceServiceHandler.ResourceCollection.Add(New GenericResourceEntity(id) With {.Name = name, .CreationDate = DateTime.Now, .ModifiedDate = DateTime.Now, .ResourceData = New ResourceDataEntity() With {.Resource = New ResourceEntity()}})
            Return id
        End Function

        Public Function AddInlineElement(iltName As String, iltData As XElement, ctrlName As String, ctrlData As XElement) As String
            Dim id As Guid = NextId()
            _resourceServiceHandler.ResourceCollection.Add(New ItemLayoutTemplateResourceEntity(id) With {.Name = iltName,
                                                              .ResourceData = New ResourceDataEntity(Guid.NewGuid) With
                                                              {
                                                              .BinData = New System.Text.UTF8Encoding().GetBytes(iltData.ToString())
                                                              }
                                                              })

            Dim id2 As Guid = NextId()
            _resourceServiceHandler.ResourceCollection.Add(New ControlTemplateResourceEntity(id) With {.Name = ctrlName,
                                                              .ResourceData = New ResourceDataEntity(Guid.NewGuid) With
                                                              {
                                                              .BinData = New System.Text.UTF8Encoding().GetBytes(ctrlData.ToString())
                                                              }
                                                              })
            Return $"{id}  |  {id2}"
        End Function

        Public Function MakeTextResource(name As String, txt As String) As Guid
            Dim id As Guid = NextId()
            Dim ret As New GenericResourceEntity(id)
            ret.Name = name
            ret.MediaType = "text/plain"
            ret.Description = name
            ret.ResourceData = New ResourceDataEntity(Guid.NewGuid) With {
                .BinData = New System.Text.UTF8Encoding().GetBytes(txt)}
            _resourceServiceHandler.ResourceCollection.Add(ret)
            Return id
        End Function

        Public Function MakeImage(name As String, image As Image) As Guid
            Dim id As Guid = NextId()
            Dim ret As New GenericResourceEntity(id)
            ret.Name = name
            ret.Dimensions = $"{image.Width} x {image.Height}"
            ret.MediaType = "image"
            ret.Description = name
            ret.ResourceData = New ResourceDataEntity(Guid.NewGuid) With {
                .BinData = DirectCast(New System.Drawing.ImageConverter().ConvertTo(image, GetType(Byte())), Byte())}
            _resourceServiceHandler.ResourceCollection.Add(ret)
            ret.Bank = New BankEntity()
            Return id
        End Function

        Public Sub AddInline()
            AddInlineElement("InlineImageLayoutTemplate", FakedResources.ImageLayoutTemplate,
                             "InlineImage", FakedResources.ImageControlTemplate)

            MakeGenericResource("InlineImageParameterSet")
        End Sub

        Public Sub AddText_A_Tale_Of_Two_Cities()
            MakeTextResource("A Tale of two Cities", FakeStaticResources.a_tale_of_two_cities)
        End Sub

        Public Sub AddTransparentPix()
            MakeImage("TransparentPix.png", FakeStaticResources.transparentPix)
        End Sub

        Public Sub AddFile(name As String, mediatype As String, data As Byte(), Optional title As String = "")
            AddGenericResource(name, mediatype, data, title)
        End Sub

        Private Function AddGenericStringResource(name As String, mediatype As String, data As String, Optional title As String = "") As Guid
            Return AddGenericResource(name, mediatype, Encoding.UTF8.GetBytes(data), title)
        End Function

        Private Function AddGenericResource(name As String, mediatype As String, data As Byte(), Optional title As String = "") As Guid
            Dim id As Guid = NextId()
            Dim ret As New GenericResourceEntity(id)
            ret.Name = name
            ret.Title = title
            ret.MediaType = mediatype
            ret.Description = name
            ret.ResourceData = New ResourceDataEntity(Guid.NewGuid) With {
                .BinData = data}
            _resourceServiceHandler.ResourceCollection.Add(ret)
            Return id
        End Function

        Public Function AddStyleSheet(name As String, data As String) As Guid
            Return AddGenericStringResource(name, name, "text/css", data)
        End Function

        Public Function AddSourceText(name As String, data As String) As Guid
            Return AddGenericStringResource(name, name, "application/xhtml+xml", data)
        End Function

        Public Sub CanSaveResources()
            If (_fakeServices Is Nothing) Then
                'Hello Developer,.. so you have come here and gotten an error
                'If you are writing unit tests then you probably need to add these lines (vb.net sytax) to your unit test.

                '<TestInitialize()>
                'Public Sub Init()
                '    FakeDal.Init()
                'End Sub

                '<TestCleanup()>
                'Public Sub DeInit()
                '    FakeDal.Deinit()
                'End Sub

                'That's it! DO NOT FORGET THE DEINIT.
                Debug.Assert(False, "FakeDal Service Init / Deinit probably missing")
            End If
            _resourceServiceHandler.EnableSaveResources()
            _bankServiceHandler.EnableSaveResources()
        End Sub

        Public Function GetFakeResourceManager() As ResourceManagerBase
            Dim resourceCollection = _resourceServiceHandler.ResourceCollection
            Return New SimpleFakedResourceManager(resourceCollection)
        End Function

        Public Function GetFakeExportReadyResourceManager() As ResourceManagerBase
            Dim resourceCollection = _resourceServiceHandler.ResourceCollection
            Return New ExportReadyResourceManager(resourceCollection)
        End Function

#End Region

        Friend Function NextId() As Guid
            _idGen.Inc()
            Return _idGen.CurrentGuid
        End Function

        Private Function ToCollection(entityCollection As EntityCollection(Of DependentResourceEntity)) As IEnumerable(Of DependentResource)
            Return From e In entityCollection Select New DependentResource(e.DependentResource.Name)
        End Function

    End Module
End Namespace