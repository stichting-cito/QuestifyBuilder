using System;
using System.ComponentModel.Composition;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    [Export(typeof(ICurrentItemEditorContext))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    class CurrentItemEditorContext : ICurrentItemEditorContext
    {
        Mediator mediator;
        bool _isActive;
        string _title;
        int _bank;
        Guid _itemLayoutTemplateId;


        [ImportingConstructor]
        public CurrentItemEditorContext()
        {
            mediator = Mediator.Instance;
            mediator.Register(this);
        }



        public bool IsActive
        {
            get { return _isActive; }
        }

        public string Title
        {
            get { return _title; }
        }

        public int BankIdentifier
        {
            get { return _bank; }
        }

        public Guid ItemLayoutTemplateId
        {
            get { return _itemLayoutTemplateId; }
        }



        [MediatorMessageSink(MediatorMessages.ItemEditor.IsActive)]
        internal void ItemEditorActivation(bool isActive)
        {
            _isActive = isActive;
        }

        [MediatorMessageSink(MediatorMessages.ItemEditor.Title)]
        internal void TitleIsSet(string title)
        {
            _title = title;
        }


        [MediatorMessageSink(MediatorMessages.ItemEditor.Bank)]
        internal void ItemEditorActivation(int bank)
        {
            _bank = bank;
        }

        [MediatorMessageSink(MediatorMessages.ItemEditor.ItemLayoutTemplate)]
        internal void LayoutTemplateActivation(Guid itemLayoutTemplateId)
        {
            _itemLayoutTemplateId = itemLayoutTemplateId;
        }

    }
}
