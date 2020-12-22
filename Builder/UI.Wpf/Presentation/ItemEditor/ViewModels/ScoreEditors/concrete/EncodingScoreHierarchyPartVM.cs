using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Cinch;
using Questify.Builder.Logic.Scoring;
using Questify.Builder.Model.ContentModel.EntityClasses;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    [DebuggerDisplay("Name : {PartName}, Selected={Selected.DataValue}, CanSelect={CanSelect.DataValue}, Depth={Depth}")]
    class EncodingScoreHierarchyPartVM : ViewModelBase, IConceptScoringBrowserHierarchyPart
    {

        private static readonly PropertyChangedEventArgs SelectedArgs = ObservableHelper.CreateArgs<EncodingScoreHierarchyPartVM>(x => x.Selected); private static readonly PropertyChangedEventArgs CanSelectedArgs = ObservableHelper.CreateArgs<EncodingScoreHierarchyPartVM>(x => x.CanSelect); private static readonly PropertyChangedEventArgs InputEnabledArgs = ObservableHelper.CreateArgs<EncodingScoreHierarchyPartVM>(x => x.InputEnabled);

        public EncodingScoreHierarchyPartVM(ConceptStructurePartCustomBankPropertyEntity conceptStructurePart)
        {
            Children = new List<EncodingScoreHierarchyPartVM>();

            Part = conceptStructurePart;
            PartName = Part == null ? "Part Name" : Part.Name;
            Parent = null;

            InputEnabled = new DataWrapper<bool>(this, InputEnabledArgs);
            SetSelectedToFalseNoNotification();
            CanSelect = new DataWrapper<bool>(this, CanSelectedArgs);

            Depth = -1;
        }

        internal void SetSelectedToFalseNoNotification()
        {
            Selected = new DataWrapper<bool>(this, SelectedArgs, ToggleCanSelectChildren);
        }

        void IConceptScoringBrowserHierarchyPart.SetIsSelectedToFalseNoNotification()
        {
            SetSelectedToFalseNoNotification();
        }

        public EncodingScoreHierarchyPartVM(ConceptStructurePartCustomBankPropertyEntity conceptStructurePart, EncodingScoreHierarchyPartVM parent)
            : this(conceptStructurePart)
        {
            Parent = parent;
            CanSelect.DataValue = parent.Selected.DataValue;
            parent.AddChild(this);
            Depth = parent.Depth + 1;
        }


        public string PartName { get; }
        public DataWrapper<bool> Selected { get; private set; }
        public DataWrapper<bool> InputEnabled { get; }

        bool IConceptScoringBrowserHierarchyPart.IsSelected
        {
            get { return Selected.DataValue; }
            set { Selected.DataValue = value; }
        }

        public DataWrapper<bool> CanSelect { get; internal set; }
        public EncodingScoreHierarchyPartVM Parent { get; }
        public List<EncodingScoreHierarchyPartVM> Children { get; }
        public ObservableCollection<IConceptScoringBrowserScoreContainer> ConceptScorePart { get; set; }

        public ConceptStructurePartCustomBankPropertyEntity Part { get; }

        public Guid Id => Part.ConceptStructurePartCustomBankPropertyId;

        public int Depth { get; }


        private void AddChild(EncodingScoreHierarchyPartVM child)
        {
            Children.Add(child);
        }

        private void ToggleCanSelectChildren()
        {
            foreach (var item in Children)
            {
                item.CanSelect.DataValue = Selected.DataValue;
                if (!Selected.DataValue)
                {
                    item.Selected.DataValue = false;
                }
            }

            if (ConceptScorePart != null)
            {
                foreach (var e in ConceptScorePart)
                {
                    ((SingleConceptScoreVM)e).Selected.DataValue = Selected.DataValue;
                }
            }

            ToggleInputEnabled();
        }

        private bool CheckToggleInputEnabled()
        {
            if (!Selected.DataValue)
            {
                return false;
            }

            if (Children.Count == 0)
            {
                return true;
            }

            return Children.Any(c => c.Selected.DataValue && c.InputEnabled.DataValue);
        }

        public void ToggleInputEnabled()
        {
            InputEnabled.DataValue = CheckToggleInputEnabled();

            ConceptScorePart?.OfType<SingleConceptScoreVM>().ToList().ForEach(c => c.InputEnabled.DataValue = InputEnabled.DataValue);

            Parent?.ToggleInputEnabled();
        }

        public bool IsParentPart(Guid partId)
        {
            var ret = Part.ConceptStructurePartCustomBankPropertyId == partId;

            return ret || Parent != null && Parent.IsParentPart(partId);
        }
    }
}
