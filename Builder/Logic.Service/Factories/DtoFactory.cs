using Questify.Builder.Logic.Service.Exceptions;
using Questify.Builder.Logic.Service.Interfaces;

namespace Questify.Builder.Logic.Service.Factories
{
    public class DtoFactory
    {

        private static IItemResourceDtoRepository _itemServiceInstance;
        private static IDataSourceResourceDtoRepository _datasourceServiceInstance;
        private static ITestResourceDtoRepository _testServiceInstance;
        private static ITestPackageResourceDtoRepository _testPackageInstance;
        private static IAspectResourceDtoRepository _aspectInstance;
        private static IGenericResourceDtoRepository _genericInstance;
        private static ICustomBankPropertyResourceDtoRepository _customBankResourceInstance;
        private static ICustomBankPropertyDtoRepository _customBankInstance;
        private static IItemlayoutTemplateResourceDtoRepository _itemLayoutServiceInstance;
        private static IDataSourceTemplateResourceDtoRepository _datasourceTemplateServiceInstance;
        private static ITestTemplateResourceDtoRepository _testTemplateServiceInstance;
        private static IControlTemplateResourceDtoRepository _controlTemplateResourceDtoRepository;
        private static IBankDtoRepository _bankServiceInstance;
        private static ICacheService _cacheServiceInstance;
        private static bool _isInstantiated = false;


        private DtoFactory()
        {
        }



        public static bool IsInstantiated { get { return _isInstantiated; } }
        public static IItemResourceDtoRepository Item
        {
            get
            {
                return ValidateInstance(_itemServiceInstance);
            }
        }
        public static IDataSourceResourceDtoRepository Datasource
        {
            get
            {
                return ValidateInstance(_datasourceServiceInstance);
            }
        }
        public static ITestResourceDtoRepository Test
        {
            get
            {
                return ValidateInstance(_testServiceInstance);

            }
        }
        public static ITestPackageResourceDtoRepository TestPackage
        {
            get
            {
                return ValidateInstance(_testPackageInstance);
            }
        }
        public static IAspectResourceDtoRepository Aspect
        {
            get
            {
                return ValidateInstance(_aspectInstance);
            }
        }
        public static IGenericResourceDtoRepository Generic
        {
            get
            {
                return ValidateInstance(_genericInstance);
            }
        }

        public static ICustomBankPropertyResourceDtoRepository CustomBankResourceProperty
        {
            get
            {
                return ValidateInstance(_customBankResourceInstance);
            }
        }

        public static ICustomBankPropertyDtoRepository CustomBankProperty
        {
            get
            {
                return ValidateInstance(_customBankInstance);
            }
        }
        public static IItemlayoutTemplateResourceDtoRepository ItemLayoutTemplate
        {
            get
            {
                return ValidateInstance(_itemLayoutServiceInstance);
            }
        }

        public static IDataSourceTemplateResourceDtoRepository DatasourceTemplate
        {
            get
            {
                return ValidateInstance(_datasourceTemplateServiceInstance);
            }
        }
        public static ITestTemplateResourceDtoRepository TestTemplate
        {
            get
            {
                return ValidateInstance(_testTemplateServiceInstance);
            }
        }
        public static IControlTemplateResourceDtoRepository ControlTemplate
        {
            get
            {
                return ValidateInstance(_controlTemplateResourceDtoRepository);
            }
        }
        public static IBankDtoRepository Bank
        {
            get
            {
                return ValidateInstance(_bankServiceInstance);
            }
        }

        public static ICacheService CacheService
        {
            get
            {
                return ValidateInstance(_cacheServiceInstance);
            }
        }

        private static TInstance ValidateInstance<TInstance>(TInstance value) where TInstance : class
        {
            if (value == null)
            {
                throw new ServiceException("Not instantiated");
            }

            return value;
        }




        public static void Instantiate(
            IItemResourceDtoRepository itemServiceInstance,
            IDataSourceResourceDtoRepository datasourceServiceInstance,
            ITestResourceDtoRepository testServiceInstance,
            ITestPackageResourceDtoRepository testPackageInstance,
            IAspectResourceDtoRepository aspectInstance,
            IGenericResourceDtoRepository genericInstance,
            ICustomBankPropertyResourceDtoRepository customBankResourceInstance,
            ICustomBankPropertyDtoRepository customBankInstance,
            IItemlayoutTemplateResourceDtoRepository itemLayoutServiceInstance,
            IDataSourceTemplateResourceDtoRepository datasourceTemplateServiceInstance,
            ITestTemplateResourceDtoRepository testTemplateServiceInstance,
            IControlTemplateResourceDtoRepository controlTemplateResourceDtoRepository,
            IBankDtoRepository bankServiceInstance,
            ICacheService cacheServiceInstance
            )
        {
            _itemServiceInstance = itemServiceInstance;
            _datasourceServiceInstance = datasourceServiceInstance;
            _testServiceInstance = testServiceInstance;
            _testPackageInstance = testPackageInstance;
            _aspectInstance = aspectInstance;
            _genericInstance = genericInstance;
            _customBankResourceInstance = customBankResourceInstance;
            _customBankInstance = customBankInstance;
            _itemLayoutServiceInstance = itemLayoutServiceInstance;
            _datasourceTemplateServiceInstance = datasourceTemplateServiceInstance;
            _testTemplateServiceInstance = testTemplateServiceInstance;
            _controlTemplateResourceDtoRepository = controlTemplateResourceDtoRepository;
            _bankServiceInstance = bankServiceInstance;
            _cacheServiceInstance = cacheServiceInstance;
            _isInstantiated = true;
        }

        public static void Destroy()
        {
            _itemServiceInstance = null;
            _datasourceServiceInstance = null;
            _testServiceInstance = null;
            _testPackageInstance = null;
            _aspectInstance = null;
            _genericInstance = null;
            _customBankResourceInstance = null;
            _customBankInstance = null;
            _itemLayoutServiceInstance = null;
            _datasourceTemplateServiceInstance = null;
            _testTemplateServiceInstance = null;
            _controlTemplateResourceDtoRepository = null;
            _bankServiceInstance = null;
            _cacheServiceInstance = null;
        }

    }
}







