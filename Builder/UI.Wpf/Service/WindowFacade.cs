using System;
using System.Collections.Generic;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UI.Wpf.Service.Interfaces;

namespace Questify.Builder.UI.Wpf.Service
{
    public class WindowFacade : IWindowFacade
    {
        private readonly IItemEditorService _ItemEditorService;
        private readonly ISourceTextEditorService _SourceTextEditorService;
        private readonly IResourcePropertyDialogService _ResourcePropertyDialogService;
        private readonly IAnnouncementService _announcementService;

        private readonly IResourceMoverWizardService _ResourceMoverWizardService;
        private readonly IMajorVersionDialogService _MajorVersionDialogService;

        public WindowFacade()
        {
            _ItemEditorService = ViewModelRepository.Instance.Resolver.Container.GetExport<IItemEditorService>().Value;
            _SourceTextEditorService = ViewModelRepository.Instance.Resolver.Container.GetExport<ISourceTextEditorService>().Value;
            _ResourcePropertyDialogService = ViewModelRepository.Instance.Resolver.Container.GetExport<IResourcePropertyDialogService>().Value;
            _announcementService = ViewModelRepository.Instance.Resolver.Container.GetExport<IAnnouncementService>().Value;

            _ResourceMoverWizardService = ViewModelRepository.Instance.Resolver.Container.GetExport<IResourceMoverWizardService>().Value;
            _MajorVersionDialogService = ViewModelRepository.Instance.Resolver.Container.GetExport<IMajorVersionDialogService>().Value;
        }



        public List<Guid> ItemsToBeOpen
        {
            get { return _ItemEditorService.ItemsToBeOpen; }
        }

        public void OpenSecondItemEditorById(Guid id, bool canMoveBackward, bool canMoveForward, Guid itemToCloseId)
        {
            _ItemEditorService.Show(id, canMoveBackward, canMoveForward, true, itemToCloseId);
        }

        public void OpenSecondItemEditorById(Guid id, bool canMoveBackward, bool canMoveForward)
        {
            _ItemEditorService.Show(id, canMoveBackward, canMoveForward, true);
        }

        public void OpenItemEditorById(Guid id, bool canMoveBackward, bool canMoveForward, Guid itemToCloseId)
        {
            _ItemEditorService.Show(id, canMoveBackward, canMoveForward, false, itemToCloseId);
        }

        public void OpenItemEditorById(Guid id, bool canMoveBackward, bool canMoveForward, bool canChangeCode)
        {
            _ItemEditorService.Show(id, canMoveBackward, canMoveForward, false, canChangeCode);
        }

        public void CreateNewItem(Guid itemLayoutTemplateId, int bankId, bool canMoveBackward, bool canMoveForward, Guid previousItemId)
        {
            _ItemEditorService.Make_NewItem(itemLayoutTemplateId, bankId, canMoveBackward, canMoveForward);
        }

        public bool FocusItem(Guid itemEntityId)
        {
            return _ItemEditorService.FocusItem(itemEntityId);
        }




        public void OpenSourceTextEditorById(Guid id)
        {
            _SourceTextEditorService.Show(id);
        }

        public void OpenSourceTextEditorDialogById(Guid id)
        {
            _SourceTextEditorService.ShowDialog(id);
        }


        public void CreateNewSourceText(int bankId, bool makeSourceTextTemplate)
        {
            _SourceTextEditorService.Make_NewSourceTextTemplate(bankId, makeSourceTextTemplate);
        }



        public bool OpenResourcePropertyDialog(Guid id, Type type, int tabIndex = 0)
        {
            return _ResourcePropertyDialogService.Show(id, type, tabIndex);
        }




        public void OpenAnnouncementDialog()
        {
            _announcementService.Show();
        }



        public bool OpenMajorVersionDialog(Questify.Builder.Model.ContentModel.EntityClasses.ResourceEntity entity)
        {
            return _MajorVersionDialogService.Show(entity);
        }



        public bool? ShowResourceMoverWizard(int sourceBankId, Guid[] resourcesToMoveIds)
        {
            return _ResourceMoverWizardService.ShowDialog(sourceBankId, resourcesToMoveIds);
        }

    }
}
