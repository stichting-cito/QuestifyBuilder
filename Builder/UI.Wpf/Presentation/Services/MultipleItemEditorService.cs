using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Cinch;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels;
using Questify.Builder.UI.Wpf.Presentation.ItemEditor.Views;
using Questify.Builder.UI.Wpf.Presentation.Types;
using Questify.Builder.UI.Wpf.Presentation.WinformsInterop;

namespace Questify.Builder.UI.Wpf.Presentation.Services
{
    [Export(typeof(IItemEditorService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class MultipleItemEditorService : IItemEditorService, IDisposable
    {
        private const string ViewName = Constants.ItemEditorFluentView;
        private const int MaxItems = 2;

        private readonly IWPF2WinVisualizerService _uiVisualizer;
        private ItemEditorViewModel _inFocusViewModel;


        [ImportingConstructor]
        public MultipleItemEditorService(IWPF2WinVisualizerService uiVisualizer
            , ICurrentItemEditorContext itemEditorContext)
        {
            _uiVisualizer = uiVisualizer;
            ItemsToBeOpen = new List<Guid>();
        }



        public List<Guid> ItemsToBeOpen { get; }
        private List<ItemEditorViewModel> _viewModels = new List<ItemEditorViewModel>();

        public void Show(Guid itemEntityId, bool canMoveBackward, bool canMoveForward)
        {
            if (_viewModels.Count <= 1)
            {
                Show(itemEntityId, canMoveBackward, canMoveForward, false);
            }
        }

        public void Show(Guid itemEntityId, bool canMoveBackward, bool canMoveForward, bool asSecondItem)
        {
            Show(itemEntityId, canMoveBackward, canMoveForward, asSecondItem, default(Guid));
        }

        public void Show(Guid itemEntityId, bool canMoveBackward, bool canMoveForward, bool asSecondItem, bool canChangeCode)
        {
            Show(itemEntityId, canMoveBackward, canMoveForward, asSecondItem, default(Guid), canChangeCode);
        }

        public void Show(Guid itemEntityId, bool canMoveBackward, bool canMoveForward, bool asSecondItem, Guid itemToReplaceEntityId)
        {
            Show(itemEntityId, canMoveBackward, canMoveForward, asSecondItem, itemToReplaceEntityId, true);
        }

        public void Show(Guid itemEntityId, bool canMoveBackward, bool canMoveForward, bool asSecondItem, Guid itemToReplaceEntityId, bool canChangeCode)
        {
            if (!itemToReplaceEntityId.Equals(default(Guid)) || _viewModels.Count < MaxItems)
            {
                if (CanOpenItemInEditor(asSecondItem, itemToReplaceEntityId))
                {
                    Init(asSecondItem, itemToReplaceEntityId);
                    _inFocusViewModel.ItemId.DataValue = itemEntityId;
                    ItemsToBeOpen.RemoveAll(g => _viewModels.All(vm => vm.ItemId.DataValue != g) && g != itemEntityId);

                    _inFocusViewModel.CanMoveBackInList.DataValue = canMoveBackward;
                    _inFocusViewModel.CanMoveNextInList.DataValue = canMoveForward;
                    _inFocusViewModel.CanChangeCode.DataValue = canChangeCode;
                }
                else
                {
                    ItemsToBeOpen.Remove(itemEntityId);
                }
            }
            else if (_viewModels.Count == MaxItems)
            {
                throw new ApplicationException(string.Format(Properties.Resources.MaxOpenItemsReached, MaxItems));
            }
        }

        private bool CanOpenItemInEditor(bool asSecondItem, Guid itemToReplaceEntityId)
        {
            if (!asSecondItem && _viewModels.Any())
            {
                if (itemToReplaceEntityId != default(Guid))
                {
                    return (!_viewModels.First(m => m.ItemId.DataValue.Equals(itemToReplaceEntityId)).ItemResourceEntity.DataValue.IsNew);
                }
                else
                {
                    if (_viewModels.First().ItemResourceEntity.DataValue == null)
                    {
                        return false;
                    }
                    return (!_viewModels.First().ItemResourceEntity.DataValue.IsNew);
                }
            }
            return true;
        }

        public void Make_NewItem(Guid itemLayoutTemplateId, int bankId, bool canMoveBackward, bool canMoveForward)
        {
            if (_viewModels.Count < MaxItems)
            {
                Init(false, default(Guid));
                _inFocusViewModel.CreateNewItem(itemLayoutTemplateId, bankId, canMoveBackward, canMoveForward);
            }
        }

        public bool FocusItem(Guid itemEntityId)
        {
            var vmToFocus = _viewModels.FirstOrDefault(vm => vm.ItemId.DataValue == itemEntityId);
            if (vmToFocus != null)
            {
                var viewToFocus = _uiVisualizer.GetOpenWindows().OfType<ItemEditorFluentWindow>().FirstOrDefault(w => w.DataContext == vmToFocus);
                viewToFocus?.Focus();
                return true;
            }
            return false;
        }


        private void Init(bool asSecondItem, Guid itemToReplaceEntityId)
        {
            if (!asSecondItem && _viewModels.Count == 1 || _viewModels.Count == MaxItems)
            {
                if (itemToReplaceEntityId != default(Guid))
                {
                    _inFocusViewModel = _viewModels.First(m => m.ItemId.DataValue.Equals(itemToReplaceEntityId));
                }
                else
                {
                    _inFocusViewModel = _viewModels.First();
                }
                ItemsToBeOpen.Remove(_inFocusViewModel.ItemId.DataValue);
            }
            else
            {
                _inFocusViewModel = new ItemEditorViewModel();
                _viewModels.Add(_inFocusViewModel);
                _uiVisualizer.Show(ViewName, _inFocusViewModel, true, HandleUiCompleted);
            }

            _inFocusViewModel.NewItemIdAssigned += _inFocusViewModel_NewItemIdAssigned;
        }

        private void HandleUiCompleted(object sender, UICompletedEventArgs e)
        {
            var vm = e.State as ItemEditorViewModel;
            if (_viewModels.Count == 1 && vm == _inFocusViewModel)
            {
                RemoveViewModel(ref _inFocusViewModel);
            }
            RemoveViewModel(ref vm);

            if (_viewModels.Count == 1)
            {
                _viewModels.First().ReloadData();
            }
        }

        private void _inFocusViewModel_NewItemIdAssigned(object sender, IdChangedEventArgs e)
        {
            if (e.PreviousId != Guid.Empty)
                ItemsToBeOpen.Remove(e.PreviousId);

            if (e.NewId != Guid.Empty && !ItemsToBeOpen.Contains(e.NewId))
                ItemsToBeOpen.Add(e.NewId);
        }

        private void RemoveViewModel(ref ItemEditorViewModel vm)
        {
            if (vm == default(ItemEditorViewModel))
                return;

            ItemsToBeOpen.Remove(vm.ItemId.DataValue);
            _viewModels.Remove(vm);

            if (!_viewModels.Any())
            {
                _viewModels = null;
                _viewModels = new List<ItemEditorViewModel>();
            }

            vm.NewItemIdAssigned -= _inFocusViewModel_NewItemIdAssigned;
            vm.Dispose();
            vm = null;
        }

        public void Dispose()
        {
            _inFocusViewModel?.Dispose();
        }
    }
}
