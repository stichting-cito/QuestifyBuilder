using System;
using System.Collections.Generic;
using Cito.Tester.Common;
using Cito.Tester.ContentModel;
using Questify.Builder.Configuration;

namespace Questify.Builder.Logic.Service.Interfaces
{
    public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);
    public delegate void StartProgressEventHandler(object sender, StartEventArgs e);

    public interface IPublicationHandler
    {
        string FileExtension { get; }

        string Errors { get; }

        string Warnings { get; }

        string UserFriendlyName { get; }

        bool CanHandleMultipleTests { get; }

        bool CanHandleSingleTests { get; }

        bool CanHandleMultipleTestPackages { get; }

        bool CanHandleSingleTestPackages { get; }

        bool CanHandleBanks { get; }

        List<String> SupportedViews { get; }

        string ProgressMessage { get; }

        Dictionary<string, string> ExportedFiles { get; }

        Dictionary<string, string> Urls { get; }

        bool ShowTargetFileLocation { get; }

        bool ShowPublicationOptions { get; }

        bool ShowFileResultsAsUrl { get; }

        Dictionary<string, string> ConfigurationOptions { get; set; }

        string PublicationPath { get; set; }

        IPublicationSelection PublicationSelection { get; set; }

        System.Windows.Forms.UserControl PublicationOptionsControl { get; set; }

        Dictionary<string, string> GetConfigurationOptions(int bankId, IList<string> testNames, IList<string> testPackageNames);

        bool Publish(Dictionary<string, string> configurationOptions, int bankId, IList<string> testNames, IList<string> testPackageNames, string exportPath, bool isForPreview, string customName);

        bool PublishItem(System.IO.FileInfo packageFile, bool isEncryptedPackage, AssessmentItem assessmentItem);

        string PublishItem(AssessmentItem assessmentItem, ResourceManagerBase resourceManager);

        PluginHandlerConfigCollection HandlerConfig { get; set; }

        string FilePath { get; set; }

        event ProgressEventHandler Progress;
        event StartProgressEventHandler StartProgress;
        bool IsValid();

        bool CheckItemsSupportedViews();
    }


    public class PublicationHandlerProgressEventArgs : EventArgs
    {

        private string _statusMessage;

        private int _value;
        public string StatusMessage
        {
            get { return _statusMessage; }
        }

        public int Value
        {
            get { return _value; }
        }

        public PublicationHandlerProgressEventArgs(string sMessage, int value)
        {
            _statusMessage = sMessage;
            _value = value;
        }
    }

}