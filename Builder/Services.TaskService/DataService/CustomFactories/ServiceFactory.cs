using Questify.Builder.Logic.Service.Cache;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.LlblGen.Proxy.Services;

namespace Questify.Builder.Services.TasksService.DataService.CustomFactories
{
    public class ServiceFactory
    {
        private static bool _initialized = false;


        public static void Init()
        {
            var itemResourceService = new CacheItemResourceDtoService(new LlblGenItemResourceDtoServiceAdapter(MakeService<ItemResourceDto, ItemResourceEntity>()));
            var dataSourceResourceService = new CacheDataSourceResourceDtoService(new LlblGenDatasourceResourceDtoServiceAdapter(MakeService<DataSourceResourceDto, DataSourceResourceEntity>()));
            var testResourceService = new CacheTestResourceDtoService(new LlblGenAssessmentTestResourceDtoServiceAdapter(MakeService<AssessmentTestResourceDto, AssessmentTestResourceEntity>()));
            var testPackageResourceService = new CacheTestPackageResourceDtoService(new LlblGenTestPackageResourceDtoServiceAdapter(MakeService<TestPackageResourceDto, TestPackageResourceEntity>()));
            var aspectResourceService = new CacheAspectResourceDtoService(new LlblGenAspectResourceDtoServiceAdapter(MakeService<AspectResourceDto, AspectResourceEntity>()));
            var genericResourceService = new CacheGenericResourceDtoService(new LlblGenGenericResourceDtoServiceAdapter(MakeService<GenericResourceDto, GenericResourceEntity>()));
            var customBankPropertyResourceService = new CacheCustomBankPropertyResourceDtoService(new LlblGenCustomBankPropertyResourceDtoService());
            var customBankPropertyService = new CacheCustomBankPropertyDtoService(new LlblGenCustomBankPropertyDtoService());
            var itemLayoutTemplateResourcePropertyService = new CacheItemLayoutResourceDtoService(new LlblGenItemLayoutTemplateResourceDtoServiceAdapter(MakeService<ItemLayoutTemplateResourceDto, ItemLayoutTemplateResourceEntity>()));
            var datasourceTemplateResourceService = new CacheDataSourceTemplateResourceDtoService(new LlblGenDatasourceTemplateResourceDtoServiceAdapter(MakeService<DataSourceResourceDto, DataSourceResourceEntity>()));
            var testTemplateResourceService = new CacheTestTemplateResourceDtoService(new LlblGenAssessmentTestTemplateResourceDtoServiceAdapter(MakeService<AssessmentTestResourceDto, AssessmentTestResourceEntity>()));
            var controlTemplateResourceService = new CacheControlTemplateResourceDtoService(new LlblGenControlTemplateResourceDtoServiceAdapter(MakeService<ControlTemplateResourceDto, ControlTemplateResourceEntity>()));
            var bankService = new CacheBankDtoService(new LlblGenBankDtoService());
            var cacheService = new CacheDtoService();

            DtoFactory.Instantiate(itemResourceService, dataSourceResourceService, testResourceService, testPackageResourceService,
                                    aspectResourceService, genericResourceService, customBankPropertyResourceService,
                                    customBankPropertyService, itemLayoutTemplateResourcePropertyService,
                                    datasourceTemplateResourceService, testTemplateResourceService, controlTemplateResourceService, bankService, cacheService);
            _initialized = true;
        }

        private static LlblGenResourceDtoService<TResourceDto, TResource> MakeService<TResourceDto, TResource>()
              where TResource : ResourceEntity
            where TResourceDto : ResourceDto
        {
            return new LlblGenResourceDtoService<TResourceDto, TResource>();
        }




        public static IItemResourceDtoRepository GetItemService()
        {
            EnsureServices();
            return DtoFactory.Item;
        }

        public static IDataSourceResourceDtoRepository GetDataSourceService()
        {
            EnsureServices();
            return DtoFactory.Datasource;
        }

        public static ITestResourceDtoRepository GetTestService()
        {
            EnsureServices();
            return DtoFactory.Test;
        }

        public static ITestPackageResourceDtoRepository GetTestPackageService()
        {
            EnsureServices();
            return DtoFactory.TestPackage;
        }

        public static IAspectResourceDtoRepository GetAspectService()
        {
            EnsureServices();
            return DtoFactory.Aspect;
        }

        public static IGenericResourceDtoRepository GetGenericService()
        {
            EnsureServices();
            return DtoFactory.Generic;
        }

        public static ICustomBankPropertyResourceDtoRepository GetCustomBankResourceService()
        {
            EnsureServices();
            return DtoFactory.CustomBankResourceProperty;
        }

        public static ICustomBankPropertyDtoRepository GetCustomBankPropertyService()
        {
            EnsureServices();
            return DtoFactory.CustomBankProperty;
        }

        public static IItemlayoutTemplateResourceDtoRepository GetItemLayoutTemplateService()
        {
            EnsureServices();
            return DtoFactory.ItemLayoutTemplate;
        }

        public static IDataSourceTemplateResourceDtoRepository GetDataSourceTemplateService()
        {
            EnsureServices();
            return DtoFactory.DatasourceTemplate;
        }

        public static ITestTemplateResourceDtoRepository GetTestTemplateService()
        {
            EnsureServices();
            return DtoFactory.TestTemplate;
        }

        public static IControlTemplateResourceDtoRepository GetControlTemplateService()
        {
            EnsureServices();
            return DtoFactory.ControlTemplate;
        }

        public static IBankDtoRepository GetBankService()
        {
            EnsureServices();
            return DtoFactory.Bank;
        }

        public static ICacheService GetCacheService()
        {
            EnsureServices();
            return DtoFactory.CacheService;
        }

        private static void EnsureServices()
        {
            if ((!_initialized))
                Init();
        }
    }
}