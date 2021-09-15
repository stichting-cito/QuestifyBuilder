using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.HelperClasses;
using Questify.Builder.Logic.Service.Factories;
using Questify.Builder.Logic.Service.Model.Entities;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views.ScoreEditors;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    [ExportViewModel("ItemEditor.AspectReferencesScoringVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AspectReferencesScoringViewModel : ViewModelBase, IViewModel2ViewCommandSupport, IAspectReferencesScoringViewModel
    {
        static readonly PropertyChangedEventArgs AspectRefsArgs = ObservableHelper.CreateArgs<AspectReferencesScoringViewModel>(x => x.AspectRefs); static PropertyChangedEventArgs AspectScoreEditorArgs = ObservableHelper.CreateArgs<AspectReferencesScoringViewModel>(x => x.AspectScoreEditor); static PropertyChangedEventArgs SumOfMaxScoreArgs = ObservableHelper.CreateArgs<AspectReferencesScoringViewModel>(x => x.SumOfMaxScore); static PropertyChangedEventArgs MoveButtonEnabledArgs = ObservableHelper.CreateArgs<AspectReferencesScoringViewModel>(x => x.MoveButtonEnabled); static PropertyChangedEventArgs RemoveButtonEnabledArgs = ObservableHelper.CreateArgs<AspectReferencesScoringViewModel>(x => x.RemoveButtonEnabled); static PropertyChangedEventArgs EditButtonEnabledArgs = ObservableHelper.CreateArgs<AspectReferencesScoringViewModel>(x => x.EditButtonEnabled);
        private readonly IViewAwareStatus _viewAwareStatusService;
        private readonly IWinFormsWindowService winFormsWindowService;
        private AspectEditorMultipleView _view;
        private string _controllerId;
        IItemEditorViewModel _itemEditorVm = null; private Solution _itemSolution;
        public List<AspectReferenceViewModel> SelectedItems { get; private set; }
        private AspectReferenceCollection _refColl;

        [ImportingConstructor]
        public AspectReferencesScoringViewModel(IViewAwareStatus viewAwareStatusService)
        {
            _viewAwareStatusService = viewAwareStatusService;
            viewAwareStatusService.ViewLoaded += viewAwareStatusService_ViewLoaded;
            viewAwareStatusService.ViewUnloaded += viewAwareStatusService_ViewUnloaded;

            SelectedItems = new List<AspectReferenceViewModel>();

            InitCommands();
            InitDataWrappers();

            EnableButtons();
            winFormsWindowService = ViewModelRepository.Instance.Resolver.Container.GetExport<IWinFormsWindowService>().Value;
        }

        void viewAwareStatusService_ViewLoaded()
        {
            if (!Designer.IsInDesignMode)
            {
                var view = _viewAwareStatusService.View;
                IWorkSpaceAware workspaceData = (IWorkSpaceAware)view;
                _view = (AspectEditorMultipleView)view;
                _view.SetViewModel(this);

                var dataValue = (Tuple<IItemEditorViewModel, string, string>)workspaceData.WorkSpaceContextualData.DataValue;
                _itemEditorVm = dataValue.Item1;
                _controllerId = dataValue.Item2;
                var defaultAspectIdentifier = dataValue.Item3;
                _itemSolution = _itemEditorVm.AssessmentItem.DataValue.Solution;

                if (RefColl == null)
                {
                    _refColl = new AspectReferenceCollection(_controllerId);
                    _itemSolution.AspectReferenceSetCollection.Add(RefColl);
                }
                if (!string.IsNullOrEmpty(defaultAspectIdentifier) && !AspectHelper.IsDefaultResourceAspect(defaultAspectIdentifier))
                {
                    AddDefaultAspectRef(defaultAspectIdentifier);
                }

                AspectRefs.DataValue = ToViewModel(RefColl.Items);
                UpdateMaxScore();

                _itemEditorVm.Updated += ItemEditor_Updated;
                if (!_itemEditorVm.IsLoading)
                    Update();

                if (AspectScoreEditor.DataValue != null)
                {
                    AspectScoreEditor.DataValue.Dispose();
                    AspectScoreEditor.DataValue = null;
                }
            }
        }

        void viewAwareStatusService_ViewUnloaded()
        {
            if (_itemEditorVm != null)
            {
                _itemEditorVm.Updated -= ItemEditor_Updated;
            }

            _viewAwareStatusService.ViewLoaded -= viewAwareStatusService_ViewLoaded;
            _viewAwareStatusService.ViewUnloaded -= viewAwareStatusService_ViewUnloaded;
        }

        public SimpleCommand<object, object> AddAspectRefs { get; private set; }
        public SimpleCommand<object, object> RemoveAspectRefs { get; private set; }
        public SimpleCommand<object, object> EditAspectRef { get; private set; }

        public SimpleCommand<object, object> MoveAspectRefsUp { get; private set; }
        public SimpleCommand<object, object> MoveAspectRefsDown { get; private set; }
        public DataWrapper<System.Collections.ObjectModel.ObservableCollection<AspectReferenceViewModel>> AspectRefs { private set; get; }
        public DataWrapper<int> SumOfMaxScore { private set; get; }
        public DataWrapper<WorkspaceData> AspectScoreEditor { private set; get; }
        public DataWrapper<bool> MoveButtonEnabled { private set; get; }
        public DataWrapper<bool> RemoveButtonEnabled { private set; get; }
        public DataWrapper<bool> EditButtonEnabled { private set; get; }

        private AspectReferenceCollection RefColl
        {
            get
            {
                if (_refColl == null)
                {
                    _refColl = _itemSolution?.AspectReferenceSetCollection?.FirstOrDefault(ars => ars.Id.Equals(_controllerId, StringComparison.InvariantCultureIgnoreCase));
                }
                return _refColl;
            }
        }

        private void InitCommands()
        {
            AddAspectRefs = new SimpleCommand<object, object>(o => DoAddAspectRefs());
            RemoveAspectRefs = new SimpleCommand<object, object>(o => DoRemoveAspectRefs());
            EditAspectRef = new SimpleCommand<object, object>(o => DoEditAspectRef());
            MoveAspectRefsUp = new SimpleCommand<object, object>(o => DoMoveAspectRefsUp());
            MoveAspectRefsDown = new SimpleCommand<object, object>(o => DoMoveAspectRefsDown());
        }

        private void InitDataWrappers()
        {
            AspectRefs = new DataWrapper<System.Collections.ObjectModel.ObservableCollection<AspectReferenceViewModel>>(this, AspectRefsArgs);
            AspectScoreEditor = new DataWrapper<WorkspaceData>(this, AspectScoreEditorArgs);
            SumOfMaxScore = new DataWrapper<int>(this, SumOfMaxScoreArgs);
            MoveButtonEnabled = new DataWrapper<bool>(this, MoveButtonEnabledArgs);
            RemoveButtonEnabled = new DataWrapper<bool>(this, RemoveButtonEnabledArgs);
            EditButtonEnabled = new DataWrapper<bool>(this, EditButtonEnabledArgs);
        }

        public void DoPostSaveTasks()
        {
        }

        public void DoPreSaveTasks()
        {
            if (AspectScoreEditor.DataValue?.ViewModelInstance != null)
            {
                IViewModel2ViewCommandSupport VmCmdSupp = AspectScoreEditor.DataValue.ViewModelInstance as IViewModel2ViewCommandSupport;
                VmCmdSupp?.DoPreSaveTasks();
            }

            ICommandSupport CmdSupp = _view as ICommandSupport;
            CmdSupp?.DoPreSaveTasks();
        }

        public void DoTaskBeforeClosing()
        {
        }

        public void KillView()
        {
            if (_view != null)
            {
                _view.Dispose();
                _view = null;
            }
        }
        protected override void OnDispose()
        {
            if (_itemEditorVm != null)
            {
                _itemEditorVm.Updated -= ItemEditor_Updated;
                _itemEditorVm = null;
            }

            if (_view != null)
            {
                _view.Dispose();
                _view = null;
            }

            if (AspectScoreEditor.DataValue != null)
            {
                AspectScoreEditor.DataValue.Dispose();
                AspectScoreEditor.DataValue = null;
            }

            base.OnDispose();
        }

        private void ItemEditor_Updated(object sender, StringEventArgs e)
        {
            if (e.StringValue == "DoUpdate")
                Update();
        }

        private void UpdateMaxScore()
        {
            if (RefColl != null)
            {
                SumOfMaxScore.DataValue = RefColl.GetMaxScore();
            }
            else
            {
                SumOfMaxScore.DataValue = 0;
            }
        }

        private System.Collections.ObjectModel.ObservableCollection<AspectReferenceViewModel> ToViewModel(List<AspectReference> aspectReferences)
        {
            var arViewModels = new System.Collections.ObjectModel.ObservableCollection<AspectReferenceViewModel>();
            if (aspectReferences == null)
            {
                return arViewModels;
            }

            var aspectsInBank = DtoFactory.Aspect.GetResourcesForBank(((ItemEditorViewModel)_itemEditorVm).ItemResourceEntity.DataValue.BankId);

            foreach (var ar in aspectReferences)
            {
                var aspectDto = aspectsInBank.FirstOrDefault(a => a.Name.Equals(ar.SourceName, StringComparison.InvariantCultureIgnoreCase));
                var aspect = GetAspect(aspectDto.ResourceId);
                arViewModels.Add(new AspectReferenceViewModel()
                {
                    name = ar.SourceName,
                    title = aspectDto.Title,
                    maxScore = ar.MaxScore,
                    maxTranslatedScore = aspect != null && aspect.AspectScoreIsTranslated() ?
                                            (int)aspect.AspectScoreTranslationTable.Max(t => t.TranslatedScore) :
                                            (int?)null
                });
            }

            return arViewModels;
        }

        private Aspect GetAspect(Guid id)
        {
            var aspectResource = ResourceFactory.Instance.GetResourceByIdWithOption(id, new Cito.Tester.Common.ResourceRequestDTO()) as AspectResourceEntity;
            aspectResource.EnsureResourceData();
            if (aspectResource.ResourceData != null && aspectResource.ResourceData.BinData.Any())
            {
                return Cito.Tester.Common.SerializeHelper.XmlDeserializeFromByteArray(aspectResource.ResourceData.BinData, typeof(Aspect), true) as Aspect;
            }

            return null;
        }

        private void DoAddAspectRefs()
        {
            var newAspectDtos = winFormsWindowService.OpenSelectAspectDialog(((ItemEditorViewModel)_itemEditorVm).ItemResourceEntity.DataValue.BankId, AspectRefs.DataValue.Select(a => a.name).ToList(), true, true);
            if (newAspectDtos != null && newAspectDtos.Any())
            {
                AddNewAspectRefs(ConvertToAspectRefs(newAspectDtos));
            }
        }

        private IEnumerable<AspectReference> ConvertToAspectRefs(IEnumerable<AspectResourceDto> aspectDtos)
        {
            var result = new List<AspectReference>();
            aspectDtos.ToList().ForEach(ar =>
            {
                result.Add(new AspectReference() { SourceName = ar.Name, MaxScore = ar.RawScore });
            });
            return result.ToArray();
        }

        private void AddDefaultAspectRef(string aspectIdentifier)
        {
            var aspectEntity = AspectHelper.GetAspect(aspectIdentifier, ((ItemEditorViewModel)_itemEditorVm).ItemResourceEntity.DataValue.BankId);
            if (aspectEntity != null)
            {
                AddNewAspectRefs(new AspectReference[] { new AspectReference() { SourceName = aspectEntity.Name, MaxScore = aspectEntity.RawScore } });
            }
        }

        private void AddNewAspectRefs(IEnumerable<AspectReference> newAspectRefs)
        {
            var aspectRefsToAdd = GetAspectReferencesToAdd(newAspectRefs);
            if (aspectRefsToAdd != null && aspectRefsToAdd.Any())
            {
                aspectRefsToAdd.ToList().ForEach(ar =>
                {
                    RefColl.Items.Add(ar);
                });
                AspectRefs.DataValue = ToViewModel(RefColl.Items);
                UpdateMaxScore();
            }
        }

        private IEnumerable<AspectReference> GetAspectReferencesToAdd(IEnumerable<AspectReference> newAspectRefs)
        {
            if (RefColl == null)
            {
                return Enumerable.Empty<AspectReference>();
            }

            var referencedAspectIdentifers = RefColl.Items.Select(ar => ar.SourceName);
            return newAspectRefs.Where(a => !referencedAspectIdentifers.Contains(a.SourceName));
        }

        private void DoRemoveAspectRefs()
        {
            if (SelectedItems.Any())
            {
                if (RefColl != null)
                {
                    for (int i = 0; i < SelectedItems.Count; i++)
                    {
                        var toRemove = RefColl.Items.Where(a => SelectedItems.Select(s => s.name).ToList().Contains(a.SourceName)).ToList();
                        toRemove.ForEach(r =>
                        {
                            RefColl.Items.Remove(r);
                        });
                        AspectRefs.DataValue.Remove(SelectedItems[i]);
                    }
                    UpdateMaxScore();
                }
            }
        }

        private void DoMoveAspectRefsUp()
        {
            if (SelectedItems.Count() == 1)
            {
                MoveAspectRef(SelectedItems[0].name, true);
            }
        }

        private void DoMoveAspectRefsDown()
        {
            if (SelectedItems.Count() == 1)
            {
                MoveAspectRef(SelectedItems[0].name, false);
            }
        }

        private void MoveAspectRef(string aspectRefName, bool moveUp)
        {
            if (RefColl != null)
            {
                var aspectToMove = RefColl.Items.SingleOrDefault(a => a.SourceName.Equals(aspectRefName, StringComparison.InvariantCultureIgnoreCase));
                if (aspectToMove != null)
                {
                    int oldIndex = RefColl.Items.IndexOf(aspectToMove);
                    if (moveUp)
                    {
                        if (oldIndex == 0)
                        {
                            return;
                        }
                        else
                        {
                            RefColl.Items.RemoveAt(oldIndex);
                            RefColl.Items.Insert(oldIndex - 1, aspectToMove);
                        }
                    }
                    else
                    {
                        if (oldIndex == (RefColl.Items.Count() - 1))
                        {
                            return;
                        }
                        else
                        {
                            RefColl.Items.RemoveAt(oldIndex);
                            RefColl.Items.Insert(oldIndex + 1, aspectToMove);
                        }
                    }
                    AspectRefs.DataValue = ToViewModel(RefColl.Items);
                    AspectRefs.DataValue.Single(a => a.name.Equals(aspectToMove.SourceName, StringComparison.InvariantCultureIgnoreCase)).isSelected = true;
                }
            }
        }

        internal void Update()
        {
            if (_itemEditorVm.HasError.DataValue)
            {
                return;
            }

            IEnumerable<ScoringParameter> scoringParameters = _itemEditorVm.ParameterSetCollection.DataValue.DeepFetchInlineScoringParameters();
            _itemSolution.FixRemovedScoringParameters(scoringParameters); _itemEditorVm.EnableElementsOnCompletion();
        }

        public void SyncSelectedItems()
        {
            SelectedItems.ForEach(s =>
            {
                AspectRefs.DataValue.Single(a => a.name.Equals(s.name, StringComparison.InvariantCultureIgnoreCase)).isSelected = true;
            });
        }

        public void EnableButtons()
        {
            MoveButtonEnabled.DataValue = SelectedItems != null && SelectedItems.Any() && SelectedItems.Count == 1;
            RemoveButtonEnabled.DataValue = SelectedItems != null && SelectedItems.Any();
            EditButtonEnabled.DataValue = SelectedItems != null && SelectedItems.Any() && SelectedItems.Count == 1;
        }

        private void DoEditAspectRef()
        {
            if (SelectedItems.Count() == 1)
            {
                if (AspectScoreEditor.DataValue != null)
                {
                    DoPreSaveTasks();
                }

                if (AspectScoreEditor?.DataValue?.ViewModelInstance != null)
                {
                    ((AspectReferenceScoringViewModel)AspectScoreEditor.DataValue.ViewModelInstance).KillView();
                    ((AspectReferenceScoringViewModel)AspectScoreEditor.DataValue.ViewModelInstance).Dispose();
                    GC.Collect();
                }

                var aspectScoreEditorDataValue = new Tuple<IItemEditorViewModel, IAspectReferencesScoringViewModel, string, string, bool, bool, bool>(
                    _itemEditorVm,
                    this,
                    SelectedItems.First().name,
                    _controllerId,
                    _itemEditorVm.AssessmentItem.DataValue.Solution.AutoScoring == false,
                    true,
                    SelectedItems.First().maxTranslatedScore == null);

                AspectScoreEditor.DataValue = new WorkspaceData(
                    String.Empty,
                    Constants.AspectScoringSingleWorkSpace,
                    aspectScoreEditorDataValue,
                    String.Empty,
                    false);
            }
            else
            {
                AspectScoreEditor.DataValue = null;
            }
        }

        public void UpdateMaxScore(string aspectRefName)
        {
            var ar = AspectRefs.DataValue.FirstOrDefault(a => a.name.Equals(aspectRefName, StringComparison.InvariantCultureIgnoreCase));
            if (ar != null && RefColl != null)
            {
                var newMax = RefColl.Items.FirstOrDefault(i => i.SourceName.Equals(aspectRefName, StringComparison.InvariantCultureIgnoreCase));
                if (newMax != null)
                {
                    ar.maxScore = newMax.MaxScore;
                }
            }
            UpdateMaxScore();
        }
    }
}
