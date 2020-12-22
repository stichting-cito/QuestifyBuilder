using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Questify.Builder.Logic;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.Model.ContentModel.Interfaces;
using Versioning;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.ViewModels
{

    public class History
        : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isHistoryCheckBoxEnabled;
        private bool _isRevertButtonEnabled;
        private bool _isRevertButtonVisible = true;
        private string _toolTipText = (string)Application.Current.FindResource("ResourcePropertyDialog.Tab.History.RevertButton.Enabled.ToolTip");

        public ResourceHistoryEntity ResourceHistoryEntity { get; set; }
        public List<MetaDataCompareResult> MetaDataCompareResults { get; set; }
        public bool IsChecked { get; set; }
        public string OldVersion { get; set; }
        public string NewVersion { get; set; }

        internal History(ResourceHistoryEntity mostRecentResourceHistoryEntity, ResourceHistoryEntity resourceHistoryEntity)
        {
            ResourceHistoryEntity = resourceHistoryEntity;
            MetaDataCompareResults = new List<MetaDataCompareResult>();
            _isHistoryCheckBoxEnabled = true;

            if (Equals(mostRecentResourceHistoryEntity, resourceHistoryEntity))
                _isRevertButtonVisible = _isRevertButtonEnabled = false;
            else
            {
                var errorMessage = default(string);

                _isRevertButtonEnabled = new VersionReverter(mostRecentResourceHistoryEntity, resourceHistoryEntity).CanRevert(ref errorMessage);

                var versionableResource = resourceHistoryEntity.Resource as IVersionable;
                if (versionableResource != null && !versionableResource.SaveObjectAsBinary)
                    ToolTipText = (string)Application.Current.FindResource("ResourcePropertyDialog.Tab.History.RevertButton.Disabled.ToolTipMedia");
                else if (_isRevertButtonEnabled)
                    ToolTipText = (string)Application.Current.FindResource("ResourcePropertyDialog.Tab.History.RevertButton.Enabled.ToolTip");
                else
                    ToolTipText = $"{(string)Application.Current.FindResource("ResourcePropertyDialog.Tab.History.RevertButton.Disabled.ToolTip")}\r\n{errorMessage}";
            }
        }

        public bool IsHistoryCheckBoxEnabled
        {
            get { return _isHistoryCheckBoxEnabled; }
            set
            {
                _isHistoryCheckBoxEnabled = value;
                NotifyPropertyChanged("IsHistoryCheckBoxEnabled");
            }
        }

        public bool IsRevertButtonEnabled
        {
            get { return _isRevertButtonEnabled; }
            set
            {
                _isRevertButtonEnabled = value;
                NotifyPropertyChanged("IsRevertButtonEnabled");
            }
        }

        public bool IsRevertButtonVisible
        {
            get { return _isRevertButtonVisible; }
            set
            {
                _isRevertButtonVisible = value;
                NotifyPropertyChanged("IsRevertButtonVisible");
            }
        }

        public string ToolTipText
        {
            get { return _toolTipText; }
            set
            {
                _toolTipText = value;
                NotifyPropertyChanged("ToolTipText");
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
