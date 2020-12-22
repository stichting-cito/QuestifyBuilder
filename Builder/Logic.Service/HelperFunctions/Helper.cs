using System;
using Questify.Builder.Security;
using AspectResourceDto = Questify.Builder.Logic.Service.Model.Entities.AspectResourceDto;
using AssessmentTestResourceDto = Questify.Builder.Logic.Service.Model.Entities.AssessmentTestResourceDto;
using ControlTemplateResourceDto = Questify.Builder.Logic.Service.Model.Entities.ControlTemplateResourceDto;
using DataSourceResourceDto = Questify.Builder.Logic.Service.Model.Entities.DataSourceResourceDto;
using GenericResourceDto = Questify.Builder.Logic.Service.Model.Entities.GenericResourceDto;
using ItemLayoutTemplateResourceDto = Questify.Builder.Logic.Service.Model.Entities.ItemLayoutTemplateResourceDto;
using ItemResourceDto = Questify.Builder.Logic.Service.Model.Entities.ItemResourceDto;
using ResourceDto = Questify.Builder.Logic.Service.Model.Entities.ResourceDto;
using TestPackageResourceDto = Questify.Builder.Logic.Service.Model.Entities.TestPackageResourceDto;

namespace Questify.Builder.Logic.Service.HelperFunctions
{
    public class Helper
    {
        public static TestBuilderPermissionTarget ContentModelObjectToPermissionTarget(Type genericType)
        {
            if (genericType == typeof(AssessmentTestResourceDto))
            {
                return TestBuilderPermissionTarget.TestEntity;
            }
            if (genericType == typeof(ControlTemplateResourceDto))
            {
                return TestBuilderPermissionTarget.ControlTemplateEntity;
            }
            if (genericType == typeof(GenericResourceDto))
            {
                return TestBuilderPermissionTarget.MediaEntity;
            }
            if (genericType == typeof(ItemResourceDto))
            {
                return TestBuilderPermissionTarget.ItemEntity;
            }
            if (genericType == typeof(ItemLayoutTemplateResourceDto))
            {
                return TestBuilderPermissionTarget.ItemLayoutTemplateEntity;
            }
            if (genericType == typeof(AspectResourceDto))
            {
                return TestBuilderPermissionTarget.AspectEntity;
            }
            if (genericType == typeof(DataSourceResourceDto))
            {
                return TestBuilderPermissionTarget.DataSourceEntity;
            }
            if (genericType == typeof(TestPackageResourceDto))
            {
                return TestBuilderPermissionTarget.TestPackageEntity;
            }
            return TestBuilderPermissionTarget.Any;
        }


        public static TestBuilderPermissionTarget ContentModelObjectToPermissionTarget(ResourceDto resourceObject)
        {
            if ((resourceObject) is AssessmentTestResourceDto)
            {
                return TestBuilderPermissionTarget.TestEntity;
            }
            if ((resourceObject) is ControlTemplateResourceDto)
            {
                return TestBuilderPermissionTarget.ControlTemplateEntity;
            }
            if ((resourceObject) is GenericResourceDto)
            {
                return TestBuilderPermissionTarget.MediaEntity;
            }
            if ((resourceObject) is ItemResourceDto)
            {
                return TestBuilderPermissionTarget.ItemEntity;
            }
            if ((resourceObject) is ItemLayoutTemplateResourceDto)
            {
                return TestBuilderPermissionTarget.ItemLayoutTemplateEntity;
            }
            if ((resourceObject) is AspectResourceDto)
            {
                return TestBuilderPermissionTarget.AspectEntity;
            }
            if ((resourceObject) is DataSourceResourceDto)
            {
                return TestBuilderPermissionTarget.DataSourceEntity;
            }
            if ((resourceObject) is TestPackageResourceDto)
            {
                return TestBuilderPermissionTarget.TestPackageEntity;
            }
            return TestBuilderPermissionTarget.Any;
        }
    }
}