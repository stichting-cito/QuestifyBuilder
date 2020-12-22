using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using MEFedMVVM.ViewModelLocator;
using Questify.Builder.Logic;
using Questify.Builder.Logic.ContentModel;
using Questify.Builder.Logic.ContentModel.Scoring;
using Questify.Builder.Logic.HelperClasses;
using Questify.Builder.Logic.Scoring;
using Questify.Builder.Logic.Service.Interfaces;
using Questify.Builder.Model.ContentModel.EntityClasses;
using Questify.Builder.UI.Wpf.Presentation.Services;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    [ExportViewModel("ItemEditor.ScoreEditors.EncodingScoringVM")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal class EncodingScoringViewModel : ViewModelBase, IConceptScoringBrowserDataProvider, IConceptScoringBrowserObjectFactory
    {

        private readonly IAddAnswerCategory _addAnswerCategory;
        private readonly IViewAwareStatus _viewAwareStatusService;
        private ObservableCollection<EncodingScoreHierarchyPartVM> _data;
        private ObservableCollection<DataGridColumn> _dataGridColumns;
        private IItemEditorObjectFactory _itemEditorObjectFactory;
        private List<ScoringParameter> _scoringParameters;
        private Solution _solution;

        private ConceptScoringBrowser _conceptScoringBrowser;
        private IMathMlEditorPlugin _mathMlEditorPlugin;


        [ImportingConstructor]
        internal EncodingScoringViewModel(IViewAwareStatus viewAwareStatusService, IAddAnswerCategory addAnswerCategory)
        {
            _viewAwareStatusService = viewAwareStatusService;
            _addAnswerCategory = addAnswerCategory;

            _viewAwareStatusService.ViewLoaded += ViewAwareStatusServiceViewLoaded;
            RefreshScore = new SimpleCommand<object, object>(a => ColumnAndRowUpdate());
            AddAnswerCategory = new SimpleCommand<object, object>(a =>
            {
                _addAnswerCategory.AddAnswerCategory(_conceptScoringBrowser.CurrentScoringMapKey, _solution);
                ColumnAndRowUpdate();
            });

            RemoveAnswerCategory =
                new SimpleCommand<object, EncodingDataGridHeaderVm>(remove =>
                {
                    Debug.Assert(_conceptScoringBrowser?.CurrentConceptManipulator != null, "currentConceptManipulator is null!!");
                    _conceptScoringBrowser?.CurrentConceptManipulator.RemoveConcept(remove.ConceptId);
                    ColumnAndRowUpdate();
                });

            AvailableParams = new Dictionary<CombinedScoringMapKey, String>();
        }

        private void ColumnAndRowUpdate()
        {
            _conceptScoringBrowser.SyncWithSolution();

            PopulateColumns(_conceptScoringBrowser.CurrentScoringMapKey, _solution, _conceptScoringBrowser.ScorableItemColumns);

            ReEvaluteCanSelectForAllParts();
        }



        public ObservableCollection<EncodingScoreHierarchyPartVM> Data
        {
            get { return _data ?? (_data = new ObservableCollection<EncodingScoreHierarchyPartVM>()); }
            set
            {
                _data = value;

                NotifyPropertyChanged("Data");
            }
        }

        public ObservableCollection<DataGridColumn> DataGridColumns
        {
            get
            {
                return _dataGridColumns ?? (_dataGridColumns = new ObservableCollection<DataGridColumn>
                {
                    new DataGridTextColumn
                    {
                        Header = "Should Not be Visible"
                    }
                });
            }
            set
            {
                _dataGridColumns = value;
                NotifyPropertyChanged("DataGridColumns");
            }
        }

        public SimpleCommand<object, object> RefreshScore { get; private set; }
        public SimpleCommand<object, object> AddAnswerCategory { get; private set; }
        public SimpleCommand<object, EncodingDataGridHeaderVm> RemoveAnswerCategory { get; private set; }
        public Dictionary<CombinedScoringMapKey, String> AvailableParams { private set; get; }

        public CombinedScoringMapKey CurrentScoringMap
        {
            get { return (_conceptScoringBrowser != null) ? _conceptScoringBrowser.CurrentScoringMapKey : null; }
            set
            {
                if (value != _conceptScoringBrowser.CurrentScoringMapKey)
                {
                    Debug.Assert(_conceptScoringBrowser != null);
                    _conceptScoringBrowser.CurrentScoringMapKey = value;
                    ColumnAndRowUpdate();
                }
            }
        }

        internal EncodingScoreHierarchyPartVM MainScoreHierarchyPart
        {
            get { return (EncodingScoreHierarchyPartVM)_conceptScoringBrowser.MainHierarchyPart; }
        }




        private void ViewAwareStatusServiceViewLoaded()
        {
            if (Designer.IsInDesignMode) return;

            object view = _viewAwareStatusService.View;
            var workspaceData = (IWorkSpaceAware)view;
            var data =
                (Tuple<IEnumerable<ScoringParameter>, Solution, IItemEditorViewModel>)
                    workspaceData.WorkSpaceContextualData.DataValue;

            SolutionHelper.CleanConceptFindings(data.Item2, data.Item1);

            CombinedScoringMapKey firstCombinedScoreMap =
                new ScoringMap(data.Item1, data.Item2).GetMap().FirstOrDefault();
            if (firstCombinedScoreMap != null) firstCombinedScoreMap.GetConceptManipulator(data.Item2);

            Init(data.Item1, data.Item2, data.Item3);
        }

        private BitmapImage BytesToBitmapImage(byte[] imageBytes)
        {
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(imageBytes);
            bi.EndInit();
            bi.Freeze();
            return bi;
        }

        private void PopulateColumns(CombinedScoringMapKey combinedScoringMapKey, Solution solution, List<ScorableItemColumn> scorableItemColumns)
        {
            _dataGridColumns = new ObservableCollection<DataGridColumn>
            {
                new CustomBoundColumn
                {
                    Header = "",
                    Binding = new Binding(""),
                    Width = DataGridLength.SizeToCells,
                    TemplateName = "PartSelector"
                },
                new CustomBoundColumn
                {
                    Header = "Name",
                    Binding = new Binding(""),
                    Width = 200,
                    TemplateName = "PartName"
                }
            };


            if (combinedScoringMapKey == null) return;
            foreach (ScorableItemColumn scorableItemColumn in scorableItemColumns)
            {
                BitmapImage img;
                if (scorableItemColumn.OriginalValue.StartsWith("<math "))
                {
                    string firstFormula = scorableItemColumn.OriginalValue.Split('#')[0];

                    byte[] imageBytes = GetMathMlPlugin().RenderPng(firstFormula, MathMLHelper.CreateImageOptions(firstFormula, MathMLHelper.GetBaseFont()));
                    img = BytesToBitmapImage(imageBytes);
                }
                else
                {
                    img = default(BitmapImage);
                }

                _dataGridColumns.Add(new CustomBoundColumn
                {
                    Header =
                        new EncodingDataGridHeaderVm(scorableItemColumn.Caption)
                        {
                            HeaderImage = img,
                            CanDelete = !scorableItemColumn.IsRelatedToCorrectAnswer,
                            ConceptId = scorableItemColumn.ConceptId,
                        },
                    HeaderTemplateName = "AnswerCategory",
                    Binding = new Binding(string.Format("ConceptScorePart[{0}]", scorableItemColumn.ColumnIndex)),
                    Width = DataGridLength.SizeToHeader,
                    TemplateName = "ScoreCell"
                });
            }


            if (combinedScoringMapKey.CanAddAdditionalDerivates())
            {
                _dataGridColumns.Add(new CustomBoundColumn
                {
                    Header = "add",
                    Binding = new Binding(""),
                    Width = 35,
                    HeaderTemplateName = "AddAnswerCategory",
                    TemplateName = "Empty"
                });
            }

            DataGridColumns = _dataGridColumns;
        }

        private void ReEvaluteCanSelectForAllParts()
        {
            foreach (EncodingScoreHierarchyPartVM part in Data)
            {
                if (part.Depth > 0) part.CanSelect.DataValue = false || part.Parent.Selected.DataValue || part.Selected.DataValue;
            }
        }

        private IMathMlEditorPlugin GetMathMlPlugin()
        {
            return _mathMlEditorPlugin ?? (_mathMlEditorPlugin = PluginHelper.MathMlPlugin);
        }

        internal void Init(IEnumerable<ScoringParameter> scoringParameters, Solution solution,
            IItemEditorViewModel itemEditorViewModel)
        {
            _scoringParameters = scoringParameters.ToList();
            _solution = solution;
            _itemEditorObjectFactory = itemEditorViewModel.ItemEditorObjectFactory;

            ItemResourceEntity itmResource = itemEditorViewModel.ItemResourceEntity.DataValue;
            if (itmResource == null) return;
            _conceptScoringBrowser = new ConceptScoringBrowser(this, this, itmResource, _scoringParameters, _solution);

            NotifyPropertyChanged("CurrentScoringMap");

            AvailableParams = _conceptScoringBrowser.ScorableKeyCombinations;
            NotifyPropertyChanged("AvailableParams");


            PopulateColumns(_conceptScoringBrowser.CurrentScoringMapKey, solution, _conceptScoringBrowser.ScorableItemColumns);

            if (_conceptScoringBrowser.MainHierarchyPart == null || _conceptScoringBrowser.Structure == null) return;
            ObservableCollection<EncodingScoreHierarchyPartVM> structure = new ObservableCollection<EncodingScoreHierarchyPartVM>();
            foreach (IConceptScoringBrowserHierarchyPart hp in _conceptScoringBrowser.Structure)
            {
                structure.Add((EncodingScoreHierarchyPartVM)hp);
            }
            Data = structure;

        }


        ConceptStructurePartCustomBankPropertyEntity IConceptScoringBrowserDataProvider.PopulateConceptCustomBankPropertyHierarchy(Guid id)
        {
            return _itemEditorObjectFactory.PopulateConceptCustomBankPropertyHierarchy(id);
        }

        ConceptStructureCustomBankPropertyEntity IConceptScoringBrowserDataProvider.ReadConceptStructureCustomBankProperty(Guid customBankPropertyId)
        {
            object view = _viewAwareStatusService.View;
            var workspaceData = (IWorkSpaceAware)view;
            var data = (Tuple<IEnumerable<ScoringParameter>, Solution, IItemEditorViewModel>)workspaceData.WorkSpaceContextualData.DataValue;

            IItemEditorViewModel itemEditorViewModel = (IItemEditorViewModel)data.Item3;

            return itemEditorViewModel.CustomBankProperties.OfType<ConceptStructureCustomBankPropertyEntity>().FirstOrDefault(c => c.CustomBankPropertyId == customBankPropertyId);
        }



        public IConceptScoringBrowserHierarchyPart CreateHierarchyPart(ConceptStructurePartCustomBankPropertyEntity conceptPart, IConceptScoringBrowserHierarchyPart parent)
        {
            IConceptScoringBrowserHierarchyPart createdPart = null;

            if (parent != null)
            {
                EncodingScoreHierarchyPartVM castedParent = (EncodingScoreHierarchyPartVM)parent;
                createdPart = new EncodingScoreHierarchyPartVM(conceptPart, castedParent);
            }
            else
            {
                createdPart = new EncodingScoreHierarchyPartVM(conceptPart);
            }

            return createdPart;
        }

        public IConceptScoringBrowserScoreContainer CreatePartScoreContainer(IConceptScoringBrowserHierarchyPart conceptStructurePartTheScoreRelatesTo, string conceptId, int? score, IConceptScoreManipulator conceptScoreManipulator)
        {
            return new SingleConceptScoreVM((EncodingScoreHierarchyPartVM)conceptStructurePartTheScoreRelatesTo, conceptId, score, conceptScoreManipulator);
        }

    }

    internal static class CombinedScoringMapKeyExtensions
    {
        public static bool CanAddAdditionalDerivates(this CombinedScoringMapKey scoringMapKey)
        {
            if (!scoringMapKey.Any())
            {
                return false;
            }
            var scoringparameter = scoringMapKey.First().ScoringParameter;
            if (!(scoringparameter is ChoiceScoringParameter) && !(scoringparameter is GeogebraScoringParameter))
            {
                return true;
            }
            return (scoringMapKey.SetNumbers.Any());
        }
    }
}