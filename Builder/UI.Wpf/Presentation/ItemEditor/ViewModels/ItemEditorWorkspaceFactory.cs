using System;
using System.ComponentModel;
using Cinch;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels
{
    static class ItemEditorWorkspaceFactory
    {
        public static DataWrapper<WorkspaceData> Create(ItemEditorViewModel itemEditorViewModel
                                                , PropertyChangedEventArgs notificationArgs
                                                , Func<IItemEditorViewModel, WorkspaceData> dataValueFunc)
        {
            return new DataWrapper<WorkspaceData>(itemEditorViewModel, notificationArgs)
            {
                DataValue = dataValueFunc(itemEditorViewModel)
            };
        }

        public static WorkspaceData CreatePresentation(IItemEditorViewModel itemEditorViewModel)
        {
            return DoCreate(Constants.PresentationWorkSpace, itemEditorViewModel);
        }

        public static WorkspaceData CreateMetaData(IItemEditorViewModel itemEditorViewModel)
        {
            return DoCreate(Constants.MetaDataWorkSpace, itemEditorViewModel);
        }

        public static WorkspaceData CreateTreeStructure(IItemEditorViewModel itemEditorViewModel)
        {
            return DoCreate(Constants.TreeStructureWorkSpace, itemEditorViewModel);
        }

        public static WorkspaceData CreateConceptStructure(IItemEditorViewModel itemEditorViewModel)
        {
            return DoCreate(Constants.ConceptStructureWorkSpace, itemEditorViewModel);
        }

        public static WorkspaceData CreateScore(IItemEditorViewModel itemEditorViewModel)
        {
            return DoCreate(Constants.ScoringWorkSpaceHost, itemEditorViewModel);
        }

        public static WorkspaceData CreateSource(IItemEditorViewModel itemEditorViewModel)
        {
            return DoCreate(Constants.SourceWorkSpace, itemEditorViewModel);
        }

        private static WorkspaceData DoCreate(string viewName, IItemEditorViewModel itemEditorViewModel)
        {
            return new WorkspaceData(string.Empty, viewName, itemEditorViewModel,
                string.Empty, true);
        }
    }
}
