using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.UI.Publication.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cito.Tester.Common;
using Questify.Builder.Configuration;
using Questify.Builder.Plugins.QTI.Properties;
using Questify.Builder.Logic;
using Questify.Builder.Logic.QTI.Helpers;
using Questify.Builder.Logic.QTI.PackageCreators.QTI30;

namespace Questify.Builder.Plugins.QTI
{
    public class GenericTestPublicationHandler : IPublicationHandler
    {
        private string _errors;
        private string _warnings;
        private readonly Dictionary<string, string> _urls = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _exportedFiles = new Dictionary<string, string>();
        private SelectPublicationOptions _optionsControl;
        private ITimeStamp _timeStamper;

        public string FileExtension => ".zip";

        public string Errors => _errors;

        public string Warnings => _warnings;

        public string UserFriendlyName => Resources.PublishGenericTest;

        public bool CanHandleMultipleTests => true;

        public bool CanHandleSingleTests => true;

        public bool CanHandleMultipleTestPackages => false;

        public bool CanHandleSingleTestPackages => false;

        public bool CanHandleBanks => false;

        public List<string> SupportedViews => new List<string> { GenericTestModelPlugin.PLUGIN_NAME };

        public string ProgressMessage => Resources.PublishingGenericTestToSpecifiedLocation;

        public Dictionary<string, string> ExportedFiles => _exportedFiles;

        public Dictionary<string, string> Urls => _urls;

        public bool ShowTargetFileLocation => true;

        public bool ShowPublicationOptions => true;

        public bool ShowFileResultsAsUrl => true;

        public Dictionary<string, string> ConfigurationOptions { get; set; }

        public string PublicationPath
        {
            get => _optionsControl.PublicationPath;
            set
            {
            }
        }

        public IPublicationSelection PublicationSelection { get; set; }

        public UserControl PublicationOptionsControl
        {
            get
            {
                return _optionsControl ??
                       (_optionsControl = new SelectPublicationOptions(this.PublicationSelection, this));
            }
            set
            {
            }
        }

        public PluginHandlerConfigCollection HandlerConfig { get; set; }

        public string FilePath
        {
            get => _optionsControl.PackageName;
            set { }
        }

        public ITimeStamp TimeStamper
        {
            get { return _timeStamper ?? (_timeStamper = new TimeStamp()); }
            set => _timeStamper = value;
        }

        public event ProgressEventHandler Progress;
        public event StartProgressEventHandler StartProgress;

        public Dictionary<string, string> GetConfigurationOptions(int bankId, IList<string> testNames, IList<string> testPackageNames) => new Dictionary<string, string>();

        public bool IsValid() => _optionsControl.IsValid();

        public bool CheckItemsSupportedViews() => true;

        Dictionary<string, string> IPublicationHandler.ConfigurationOptions
        {
            get => null;
            set { }
        }

        PluginHandlerConfigCollection IPublicationHandler.HandlerConfig
        {
            get => null;
            set { }
        }

        public bool Publish(Dictionary<string, string> configurationOptions, int bankId, IList<string> testNames,
            IList<string> testPackageNames, string exportPath, bool isForPreview, string customName)
        {
            bool result = false;
            FileInfo fileSystemInfo = new FileInfo(exportPath);

            PackageCreator packageCreator = new PackageCreator(this.HandlerConfig);
            packageCreator.TimeStamper = TimeStamper;

            packageCreator.StartProgress += HandleStartProgress;
            packageCreator.Progress += HandleProgress;

            result = packageCreator.Create(null, bankId, testNames, testPackageNames, fileSystemInfo, true, string.Empty, isForPreview);

            _errors = GetErrorString(packageCreator.Errors.ToList());
            _warnings = GetErrorString(packageCreator.Warnings.ToList());

            packageCreator.StartProgress -= HandleStartProgress;
            packageCreator.Progress -= HandleProgress;

            if (!string.IsNullOrEmpty(_warnings) && _warnings.Length > 10000)
            {
                _warnings = $"{_warnings.Substring(0, 10000)}...";
            }
            _exportedFiles.Add(fileSystemInfo.Name, fileSystemInfo.DirectoryName);

            return result;
        }

        void HandleStartProgress(object s, StartEventArgs e)
        {
            StartProgress?.Invoke(s, e);
        }

        void HandleProgress(object s, ProgressEventArgs e)
        {
            Progress?.Invoke(s, e);
        }

        public bool PublishItem(FileInfo packageFile, bool isEncryptedPackage, global::Cito.Tester.ContentModel.AssessmentItem assessmentItem)
        {
            throw new NotImplementedException();
        }

        public string PublishItem(global::Cito.Tester.ContentModel.AssessmentItem assessmentItem, global::Cito.Tester.Common.ResourceManagerBase resourceManager)
        {
            throw new NotImplementedException();
        }

        private string GetErrorString(List<PublicationError> errorList)
        {
            if (errorList == null)
            {
                return string.Empty;
            }
            StringBuilder errorStringBuilder = new StringBuilder(string.Empty);
            errorList.ForEach(e =>
                {
                    errorStringBuilder.AppendFormat(Resources.ErrorMessage, e.EntityProcessed, e.ExceptionType, Environment.NewLine);
                    errorStringBuilder.AppendFormat("Error detail: {0}{1}", e.Message, Environment.NewLine);
                }
                );
            return errorStringBuilder.ToString();
        }
    }
}
