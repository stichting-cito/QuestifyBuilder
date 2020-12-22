
using System;
using System.Collections.Generic;
using System.Configuration;
using Questify.Builder.Configuration;
using Questify.Builder.Logic.Service.Interfaces;
using ResourceDto = Questify.Builder.Logic.Service.Model.Entities.ResourceDto;

namespace Questify.Builder.Logic.Service.HelperFunctions
{
    public class ConfigPluginHelper
    {
        public static bool AtLeastOneHandlerAvailable(string sectionName, IList<ResourceDto> selectedEntities)
        {
            PluginConfiguration configurationElementCollection = (PluginConfiguration)ConfigurationManager.GetSection(sectionName);
            return IsThereAnySupportedHandler(selectedEntities, configurationElementCollection);
        }

        public static PluginHandlerCollection GetListOfPluginHandlersBySectionName(string sectionName)
        {
            PluginConfiguration configurationElementCollection = (PluginConfiguration)ConfigurationManager.GetSection(sectionName);
            if (configurationElementCollection != null)
            {
                return configurationElementCollection.Handlers;
            }

            return null;
        }

        public static CachedElement GetCacheSettingsByType(string type)
        {
            CacheConfiguration configurationElementCollection = (CacheConfiguration)ConfigurationManager.GetSection("cachedEntities");
            if (configurationElementCollection != null && configurationElementCollection.CachedEntities != null)
            {
                foreach (CachedElement c in configurationElementCollection.CachedEntities)
                {
                    if (c.Type == type)
                        return c;
                }
            }
            return null;
        }

        public static ItemPreviewServiceElement GetItemPreviewServiceByName(string name)
        {
            ItemPreviewServiceConfiguration configurationElementCollection = (ItemPreviewServiceConfiguration)ConfigurationManager.GetSection("itemPreviewServices");
            if (configurationElementCollection != null && configurationElementCollection.ItemPreviewServices != null)
            {
                foreach (ItemPreviewServiceElement c in configurationElementCollection.ItemPreviewServices)
                {
                    if (c.Name == name)
                        return c;
                }
            }
            return null;
        }

        public static Dictionary<string, IReportValidationBase> GetListOfSupportedHandlersBySectionName(string sectionName, IList<ResourceDto> selectedEntities, int bankId)
        {
            Dictionary<string, IReportValidationBase> returnValue = new Dictionary<string, IReportValidationBase>();
            try
            {
                PluginConfiguration configurationElementCollection = (PluginConfiguration)ConfigurationManager.GetSection(sectionName);
                foreach (PluginHandlerElement configHandler in configurationElementCollection.Handlers)
                {
                    IReportValidationBase handler = (IReportValidationBase)Activator.CreateInstance(Type.GetType(configHandler.Type, true));
                    bool shouldBeAdded = false;
                    if ((handler) is IReportHandler && ((IReportHandler)handler).ShouldUseGridAsInput)
                    {
                        if (selectedEntities != null)
                        {
                            shouldBeAdded = true;
                        }
                    }
                    else
                    {
                        handler.Collection = selectedEntities;
                        handler.BankId = bankId;

                        if (handler.IsDatasourceSupported())
                        {
                            shouldBeAdded = true;
                        }
                    }

                    if (handler is IReportHandlerWithConfig && configHandler is ReportHandlerElement)
                    {
                        ((IReportHandlerWithConfig)handler).HandlerConfig = ((ReportHandlerElement)configHandler).HandlerConfig;
                    }


                    if (shouldBeAdded)
                    {
                        returnValue.Add(handler.Name, handler);
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return returnValue;
        }


        private static bool IsThereAnySupportedHandler(IList<ResourceDto> selectedEntities, PluginConfiguration pluginCollection)
        {
            bool returnValue = false;
            foreach (PluginHandlerElement configHandler in pluginCollection.Handlers)
            {

                try
                {
                    IReportValidationBase handler = (IReportValidationBase)Activator.CreateInstance(Type.GetType(configHandler.Type, true));
                    if ((handler) is IReportHandler && ((IReportHandler)handler).ShouldUseGridAsInput)
                    {
                        returnValue = true;
                        break;
                    }
                    handler.Collection = selectedEntities;
                    if (handler.IsDatasourceSupported())
                    {
                        returnValue = true;
                        break;
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return returnValue;
        }
    }
}
