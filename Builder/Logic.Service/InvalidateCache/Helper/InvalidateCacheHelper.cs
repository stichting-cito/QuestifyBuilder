using System;
using System.Collections.Generic;
using System.Linq;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.Logic.Service.InvalidateCache.Helper
{
    public class InvalidateCacheHelper
    {
        public static void ClearCacheForBank(int bankId)
        {
            DtoFactory.Aspect.BankChanged(bankId);
            DtoFactory.Bank.EntityChanged(bankId);
            DtoFactory.ControlTemplate.BankChanged(bankId);
            DtoFactory.CustomBankProperty.BankChanged(bankId);
            DtoFactory.CustomBankResourceProperty.BankChanged(bankId);
            DtoFactory.Datasource.BankChanged(bankId);
            DtoFactory.DatasourceTemplate.BankChanged(bankId);
            DtoFactory.Generic.BankChanged(bankId);
            DtoFactory.Item.BankChanged(bankId);
            DtoFactory.ItemLayoutTemplate.BankChanged(bankId);
            DtoFactory.Test.BankChanged(bankId);
            DtoFactory.TestPackage.BankChanged(bankId);
            DtoFactory.TestTemplate.BankChanged(bankId);
        }

        public static void ClearCustomProperties(IList<Guid> customPropertyIds)
        {
            if (customPropertyIds == null || !customPropertyIds.Any())
            {
                return;
            }

            DtoFactory.CustomBankProperty.EntitiesChanged(customPropertyIds);
            DtoFactory.CustomBankResourceProperty.EntitiesChanged(customPropertyIds);
        }

        public static void ClearCacheForResourcesOfAnyType(IList<Guid> resourceIds)
        {
            if (resourceIds == null || !resourceIds.Any())
            {
                return;
            }
            DtoFactory.Aspect.EntitiesChanged(resourceIds);
            DtoFactory.ControlTemplate.EntitiesChanged(resourceIds);
            DtoFactory.Datasource.EntitiesChanged(resourceIds);
            DtoFactory.DatasourceTemplate.EntitiesChanged(resourceIds);
            DtoFactory.Generic.EntitiesChanged(resourceIds);
            DtoFactory.Item.EntitiesChanged(resourceIds);
            DtoFactory.ItemLayoutTemplate.EntitiesChanged(resourceIds);
            DtoFactory.Test.EntitiesChanged(resourceIds);
            DtoFactory.TestTemplate.EntitiesChanged(resourceIds);
            DtoFactory.TestPackage.EntitiesChanged(resourceIds);
        }

        public static void ClearCacheForResourcesOfAnyType(IList<Tuple<Guid, Type>> resourceIdsAndTypes)
        {
            if (resourceIdsAndTypes == null || !resourceIdsAndTypes.Any())
            {
                return;
            }
            if (resourceIdsAndTypes.Any(ri => ri.Item2 == null))
            {
                ClearCacheForResourcesOfAnyType(resourceIdsAndTypes.Select(r => r.Item1).ToList());
            }

            ClearAspect(resourceIdsAndTypes);
            ClearControlTemplate(resourceIdsAndTypes);
            ClearDataSource(resourceIdsAndTypes);
            ClearGeneric(resourceIdsAndTypes);
            ClearItem(resourceIdsAndTypes);
            ClearItemLayoutTemplate(resourceIdsAndTypes);
            ClearAssessmentTest(resourceIdsAndTypes);
            ClearTestPackage(resourceIdsAndTypes);
        }

        private static void ClearTestPackage(IList<Tuple<Guid, Type>> resourceIdsAndTypes)
        {
            if (resourceIdsAndTypes.Any(rt => rt.Item2 == typeof(TestPackageResourceEntity)))
            {
                DtoFactory.TestPackage.EntitiesChanged(resourceIdsAndTypes.Where(rt => rt.Item2 == typeof(TestPackageResourceEntity)).Select(rt => rt.Item1));
            }
        }

        private static void ClearAssessmentTest(IList<Tuple<Guid, Type>> resourceIdsAndTypes)
        {
            if (resourceIdsAndTypes.Any(rt => rt.Item2 == typeof(AssessmentTestResourceEntity)))
            {
                DtoFactory.Test.EntitiesChanged(resourceIdsAndTypes.Where(rt => rt.Item2 == typeof(AssessmentTestResourceEntity)).Select(rt => rt.Item1));
                DtoFactory.TestTemplate.EntitiesChanged(resourceIdsAndTypes.Where(rt => rt.Item2 == typeof(AssessmentTestResourceEntity)).Select(rt => rt.Item1));
            }
        }

        private static void ClearItem(IList<Tuple<Guid, Type>> resourceIdsAndTypes)
        {
            if (resourceIdsAndTypes.Any(rt => rt.Item2 == typeof(ItemResourceEntity)))
            {
                DtoFactory.Item.EntitiesChanged(resourceIdsAndTypes.Where(rt => rt.Item2 == typeof(ItemResourceEntity)).Select(rt => rt.Item1));
            }
        }

        private static void ClearItemLayoutTemplate(IList<Tuple<Guid, Type>> resourceIdsAndTypes)
        {
            if (resourceIdsAndTypes.Any(rt => rt.Item2 == typeof(ItemLayoutTemplateResourceEntity)))
            {
                DtoFactory.ItemLayoutTemplate.EntitiesChanged(resourceIdsAndTypes.Where(rt => rt.Item2 == typeof(ItemLayoutTemplateResourceEntity)).Select(rt => rt.Item1));
            }
        }

        private static void ClearGeneric(IList<Tuple<Guid, Type>> resourceIdsAndTypes)
        {
            if (resourceIdsAndTypes.Any(rt => rt.Item2 == typeof(GenericResourceEntity)))
            {
                DtoFactory.Generic.EntitiesChanged(resourceIdsAndTypes.Where(rt => rt.Item2 == typeof(GenericResourceEntity)).Select(rt => rt.Item1));
            }
        }

        private static void ClearDataSource(IList<Tuple<Guid, Type>> resourceIdsAndTypes)
        {
            if (resourceIdsAndTypes.Any(rt => rt.Item2 == typeof(DataSourceResourceEntity)))
            {
                DtoFactory.Datasource.EntitiesChanged(resourceIdsAndTypes.Where(rt => rt.Item2 == typeof(DataSourceResourceEntity)).Select(rt => rt.Item1));
                DtoFactory.DatasourceTemplate.EntitiesChanged(resourceIdsAndTypes.Where(rt => rt.Item2 == typeof(DataSourceResourceEntity)).Select(rt => rt.Item1));
            }
        }

        private static void ClearControlTemplate(IList<Tuple<Guid, Type>> resourceIdsAndTypes)
        {
            if (resourceIdsAndTypes.Any(rt => rt.Item2 == typeof(ControlTemplateResourceEntity)))
            {
                DtoFactory.ControlTemplate.EntitiesChanged(resourceIdsAndTypes.Where(rt => rt.Item2 == typeof(ControlTemplateResourceEntity)).Select(rt => rt.Item1));
            }
        }

        private static void ClearAspect(IList<Tuple<Guid, Type>> resourceIdsAndTypes)
        {
            if (resourceIdsAndTypes.Any(rt => rt.Item2 == typeof(AspectResourceEntity)))
            {
                DtoFactory.Aspect.EntitiesChanged(resourceIdsAndTypes.Where(rt => rt.Item2 == typeof(AspectResourceEntity)).Select(rt => rt.Item1));
            }
        }
    }
}