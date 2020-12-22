using System;
using Cinch;
using Cito.Tester.Common;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.Interfaces;
using Questify.Builder.UI.Wpf.Presentation.GenericControls.ConsumerInterfaces;
using Questify.Builder.UI.Wpf.Presentation.Services;

namespace Questify.Builder.UI.Wpf.Presentation.GenericDialogs.ResourcePropertyDialog.ViewModels
{
    internal interface IResourcePropertyDialogViewModel : IMetadataViewConsumer
    {
        SimpleCommand<object, EventToCommandArgs> WindowClosing { get; }
        SimpleCommand<object, object> Ok { get; }
        SimpleCommand<object, object> Cancel { get; }
        SimpleCommand<object, object> Apply { get; }

        DataWrapper<Guid> PropertyId { get; }
        DataWrapper<string> WindowTitle { get; set; }
        DataWrapper<int> SelectedTab { get; }
        DataWrapper<IPropertyEntity> PropertyEntity { get; }
        DataWrapper<ResourceManagerBase> ResourceManager { get; }

        IResourcePropertyDialogObjectFactory ResourcePropertyDialogObjectFactory { get; }
        IResourcePropertyDialogService ResourcePropertyDialogService { get; }

        string PathToNewResource { get; set; }
        bool IdentifierAndCodeFieldDiffer { get; set; }
        Type PropertyType { get; }
    }
}
