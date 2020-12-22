using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web.Hosting;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Questify.Builder.Configuration;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ResourceManager;
using Questify.Builder.Logic.Service.Classes;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Logic.Service.Logging;
using Questify.Builder.Model.ContentModel.EntityClasses;
using LogHelper = Questify.Builder.Logic.Service.Logging.LogHelper;

namespace Questify.Builder.Services.PublicationService.ItemPreview
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ItemPreviewService : IItemPreviewService
    {

        public PublicationResult PreviewItemByCode(string itemHandlerType, string target, int bankId, string itemCode, bool isDebug, List<PublicationProperty> publicationProperties)
        {
            var itemResource = (ItemResourceEntity)ResourceFactory.Instance.GetResourceByNameWithOption(bankId, itemCode, new ResourceRequestDTO());
            return PreviewItem(itemHandlerType, target, bankId, itemResource.GetAssessmentItem(), itemCode, isDebug, publicationProperties);
        }

        public PublicationResult PreviewItemByAssessmentItem(string itemHandlerType, string target, int bankId, byte[] assessmentItemByteArray, bool isDebug, List<PublicationProperty> publicationProperties)
        {
            var assessmentItem = (AssessmentItem)SerializeHelper.XmlDeserializeFromByteArray(assessmentItemByteArray, typeof(AssessmentItem), true);
            return PreviewItem(itemHandlerType, target, bankId, assessmentItem, String.Empty, isDebug, publicationProperties);
        }

        private PublicationResult PreviewItem(string itemHandlerType, string target, int bankId, AssessmentItem assessmentItem, string itemCode, bool isDebug, List<PublicationProperty> publicationProperties)
        {
            var result = new PublicationResult();
            var config = GetItemPreviewHandlersConfiguration();
            if (config == null || config.ItemPreviewServices == null)
            {
                return result;
            }

            var handler = config.ItemPreviewServices.Cast<ItemPreviewServiceElement>().FirstOrDefault(h => h.Name == itemHandlerType);
            if (handler == null)
            {
                return result;
            }

            using (var resourceManager = new DataBaseResourceManager(bankId))
            {
                var exportPackageName = string.Format("{0}-{1}_{2}.zip", bankId, itemCode, Guid.NewGuid());
                var exportPath = HostingEnvironment.MapPath("~/Publicaties/" + exportPackageName);

                var properties = new Dictionary<string, string>()
                {
                    { "ItemHandlerType", itemHandlerType },
                    { "Target", target },
                    { "BankId", bankId.ToString() },
                    { "ItemCode", itemCode },
                    { "PublicationProperties", $"{{{string.Join(", ", publicationProperties.Select(pp => $"{pp.Key} : {pp.Value}"))}}}" }
                };
                LogHelper.TrackEvent(EventsToTrack.ItemPreview, properties);

                var itemPreviewHandler = (IStartItemPreview)Activator.CreateInstance(Type.GetType(handler.Type, true));
                result = itemPreviewHandler.DoPreviewFromServer(target, assessmentItem, resourceManager, exportPath, handler.Url, publicationProperties);

                if (isDebug)
                {
                    var requestUri = OperationContext.Current.RequestContext.RequestMessage.Headers.To.AbsoluteUri;
                    var baseUrl = requestUri.Substring(0, requestUri.LastIndexOf('/') + 1);
                    baseUrl = baseUrl.Replace("/ItemPreview/", "/");
                    result.DebugFileLocation = baseUrl + "Publicaties/" + exportPackageName;
                    return result;
                }

                try
                {
                    if (exportPath != null)
                    {
                        var f = new FileInfo(exportPath);
                        f.Delete();
                    }
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        protected virtual ItemPreviewServiceConfiguration GetItemPreviewHandlersConfiguration()
        {
            return ConfigurationManager.GetSection("itemPreviewServices") as ItemPreviewServiceConfiguration;
        }
    }
}
