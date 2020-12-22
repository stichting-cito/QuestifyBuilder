using Questify.Builder.Model.ContentModel.Interfaces;

namespace Questify.Builder.UI.Wpf.Presentation.GenericControls.ConsumerInterfaces
{
    interface IMetadataViewConsumer
    {
        IPropertyEntity ResourceToEditMetadataFor { get; }

        bool EditNameAllowed { get; }
        bool EditTitleAllowed { get; }
        bool OpenResourcePropertyDialogButtonVisible { get; }
    }
}
