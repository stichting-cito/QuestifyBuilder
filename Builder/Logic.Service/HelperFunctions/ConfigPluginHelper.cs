
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
        /// <summary>
		/// Ats the least one handler of type available.
		/// </summary>
		/// <param name="sectionName">Name of the section.</param>
		/// <param name="selectedEntities">The selected entities.</param><returns></returns>
		public static bool AtLeastOneHandlerAvailable(string sectionName, IList<ResourceDto> selectedEntities)
		{
			PluginConfiguration configurationElementCollection = (PluginConfiguration)ConfigurationManager.GetSection(sectionName);
			return IsThereAnySupportedHandler(selectedEntities, configurationElementCollection);
		}

		/// <summary>
		/// Gets the name of the list of plugin handlers by section.
		/// </summary>
		/// <param name="sectionName">Name of the section.</param><returns></returns>
		public static PluginHandlerCollection GetListOfPluginHandlersBySectionName(string sectionName)
		{
			PluginConfiguration configurationElementCollection = (PluginConfiguration)ConfigurationManager.GetSection(sectionName);
			if (configurationElementCollection != null) {
				return configurationElementCollection.Handlers;
			}

		    return null;
		}

		/// <summary>
		/// Gets the name of the list of plugin handlers by section.
		/// </summary>
		/// <param name="type">Name of the section.</param><returns></returns>
		public static CachedElement GetCacheSettingsByType(string type)
		{
			CacheConfiguration configurationElementCollection = (CacheConfiguration)ConfigurationManager.GetSection("cachedEntities");
			if (configurationElementCollection != null && configurationElementCollection.CachedEntities != null) {
				foreach (CachedElement c in configurationElementCollection.CachedEntities) {
					if (c.Type == type)
						return c;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the name of the item preview service by.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>ItemPreviewServiceElement.</returns>
		public static ItemPreviewServiceElement GetItemPreviewServiceByName(string name)
		{
			ItemPreviewServiceConfiguration configurationElementCollection = (ItemPreviewServiceConfiguration)ConfigurationManager.GetSection("itemPreviewServices");
			if (configurationElementCollection != null && configurationElementCollection.ItemPreviewServices != null) {
				foreach (ItemPreviewServiceElement c in configurationElementCollection.ItemPreviewServices) {
					if (c.Name == name)
						return c;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the name of the list of supported handlers by section.
		/// </summary>
		/// <param name="sectionName">Name of the section.</param>
		/// <param name="selectedEntities">The selected entities.</param>
		/// <param name="bankId">The bank entity.</param><returns></returns>
		public static Dictionary<string, IReportValidationBase> GetListOfSupportedHandlersBySectionName(string sectionName, IList<ResourceDto> selectedEntities, int bankId)
		{
			Dictionary<string, IReportValidationBase> returnValue = new Dictionary<string, IReportValidationBase>();
			try {
				PluginConfiguration configurationElementCollection = (PluginConfiguration)ConfigurationManager.GetSection(sectionName);
				foreach (PluginHandlerElement configHandler in configurationElementCollection.Handlers) {
					IReportValidationBase handler = (IReportValidationBase)Activator.CreateInstance(Type.GetType(configHandler.Type, true));
					bool shouldBeAdded = false;
					if ((handler) is IReportHandler && ((IReportHandler)handler).ShouldUseGridAsInput) {
						if (selectedEntities != null) {
							shouldBeAdded = true;
						}
					} else {
						handler.Collection = selectedEntities;
						handler.BankId = bankId;

						if (handler.IsDatasourceSupported()) {
							shouldBeAdded = true;
						}
					}

					if (handler is IReportHandlerWithConfig && configHandler is ReportHandlerElement) {
						((IReportHandlerWithConfig)handler).HandlerConfig = ((ReportHandlerElement)configHandler).HandlerConfig;
					}


					if (shouldBeAdded) {
						returnValue.Add(handler.Name, handler);
					}

				}
			} catch (Exception ex) {
				//if we dont find the report handler we dont do anything with the exception for now.
				//this way we can support reporthandler from the bankContext.
			}
			return returnValue;
		}


		/// <summary>
		/// Determines whether [is handler supported] [the specified selected entities].
		/// </summary>
		/// <param name="selectedEntities">The selected entities.</param>
		/// <param name="pluginCollection">The pluging collection.</param><returns>
		///   <c>true</c> if [is handler supported] [the specified selected entities]; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsThereAnySupportedHandler(IList<ResourceDto> selectedEntities, PluginConfiguration pluginCollection)
		{
			bool returnValue = false;
			foreach (PluginHandlerElement configHandler in pluginCollection.Handlers) {

				try {
					IReportValidationBase handler = (IReportValidationBase)Activator.CreateInstance(Type.GetType(configHandler.Type, true));
					if ((handler) is IReportHandler && ((IReportHandler)handler).ShouldUseGridAsInput) {
						returnValue = true;
						break;
					}
					handler.Collection = selectedEntities;
				    if (handler.IsDatasourceSupported())
				    {
				        returnValue = true;
				        break;
				    }
				} catch (Exception ex) {
					//we ignore the error
				}
			}
			return returnValue;
		}
	}
}
