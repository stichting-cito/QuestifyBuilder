using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Cinch;
using Cito.Tester.ContentModel;
using MEFedMVVM.Common;
using Questify.Builder.UI.Wpf.Presentation.Types;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor.ViewModels.ScoreEditors.concrete
{
    internal class CasVariableViewModel : ViewModelBase
    {
        private const int CMaxVar = 5;
        private const int CMaxSolutions = 10;
        private readonly string[] _possibleDegrees = { string.Empty, "1", "2", "3", "4", "5" };
        private readonly string[] _variablesNames = { "x", "y", "z", "a", "b", "c", "d", "e", "f", "g" };
        private const string DEGREEKEY = "deg";

        private const int CDefaultNrOfVars = 2;
        private const int CDefaultNrOfSolutions = 2;
        private static readonly string DefaultDegree = "";

        static PropertyChangedEventArgs CurrentNrOfSolutionsArgs = ObservableHelper.CreateArgs<CasVariableViewModel>(x => x.CurrentNrOfSolutions);
        static PropertyChangedEventArgs CurrentNrOfParamsArgs = ObservableHelper.CreateArgs<CasVariableViewModel>(x => x.CurrentNrOfParams);
        static PropertyChangedEventArgs DegreeArgs = ObservableHelper.CreateArgs<CasVariableViewModel>(x => x.CurrentDegree);

        private ObservableCollection<DataGridColumn> _dataGridColumns;

        public static CasVariableViewModel CreateFor(string stringValue)
        {
            var ret = new CasVariableViewModel();
            try
            {
                var converter = ExtendedNamedDecimalMap.FromString(stringValue);
                var names = converter.NamedDecimalMap.GetNames().ToList();
                if (names.Count > 0)
                {
                    var testValues = converter.NamedDecimalMap.GetValuesFor(names.First());

                    ret.CurrentNrOfParams.DataValue = names.Count;
                    ret.CurrentNrOfSolutions.DataValue = testValues.Count();

                    foreach (var name in names)
                    {
                        var selectedRow = ret.DataRows.Single(row => row.Name == name);
                        var values = converter.NamedDecimalMap.GetValuesFor(name).ToList();
                        Debug.Assert(selectedRow.Data.Count == values.Count, "Should be equal");
                        selectedRow.Data = new List<decimal>(values);
                    }
                }
                if (converter.Extensions.ContainsKey(DEGREEKEY))
                {
                    ret.CurrentDegree.DataValue = converter.Extensions[DEGREEKEY];
                }
            }
            catch (Exception)
            {
#if DEBUG
                throw new ArgumentException();
#endif
            }
            return ret;
        }

        public CasVariableViewModel()
        {
            NrOfParamsList = new List<int>(Enumerable.Range(1, CMaxVar));
            NrOfSolutionsList = new List<int>(Enumerable.Range(1, CMaxSolutions));
            DegreesList = _possibleDegrees.ToList<string>();
            CurrentNrOfSolutions = new DataWrapper<int>(this, CurrentNrOfSolutionsArgs) { DataValue = CDefaultNrOfSolutions };
            CurrentNrOfParams = new DataWrapper<int>(this, CurrentNrOfParamsArgs) { DataValue = CDefaultNrOfVars };
            CurrentDegree = new DataWrapper<string>(this, DegreeArgs) { DataValue = DefaultDegree };

            CurrentNrOfSolutions.OnChanged(w => w.DataValue).Do(wrapper => PopulateColumns());
            CurrentNrOfParams.OnChanged(w => w.DataValue).Do(wrapper => PopulateColumns());

            PopulateColumns();
        }

        public List<int> NrOfParamsList { get; private set; }
        public List<int> NrOfSolutionsList { get; private set; }
        public List<string> DegreesList { get; private set; }

        public DataWrapper<int> CurrentNrOfSolutions { get; private set; }
        public DataWrapper<int> CurrentNrOfParams { get; private set; }
        public DataWrapper<string> CurrentDegree { get; private set; }

        public ObservableCollection<CasVariableRow> DataRows { get; private set; }


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

        private void PopulateColumns()
        {
            _dataGridColumns = new ObservableCollection<DataGridColumn>
            {
                new CustomBoundColumn
                {
                    HeaderTemplate = (Application.Current==null)?null:(DataTemplate)Application.Current.FindResource("HeaderCasVariable"),
                    Binding = new Binding(""),
                    Width = 50,
                    TemplateName = "CasVariableLabel"
                }
            };

            for (int i = 0; i < CurrentNrOfSolutions.DataValue; i++)
            {
                _dataGridColumns.Add(new DataGridTextColumn()
                {
                    Header = (i + 1),
                    HeaderTemplate = (Application.Current == null) ? null : (DataTemplate)Application.Current.FindResource("HeaderCasSolution"),
                    Binding = new Binding(string.Format("Data[{0}]", i)),
                    Width = DataGridLength.SizeToHeader,
                    EditingElementStyle = (Application.Current == null) ? null : (Style)Application.Current.TryFindResource("EditCasSolution")
                });
            }

            DataGridColumns = _dataGridColumns;

            UpdateRows();
            UpdateParams();
        }

        private void UpdateRows()
        {
            if (DataRows == null)
                DataRows = new ObservableCollection<CasVariableRow>();

            while ((DataRows.Count < CurrentNrOfParams.DataValue))
            {
                var emptySolutionValues = Enumerable.Repeat(0M, CurrentNrOfSolutions.DataValue);
                DataRows.Add(new CasVariableRow(_variablesNames[DataRows.Count], new List<decimal>(emptySolutionValues)));
            }

            while (DataRows.Count > CurrentNrOfParams.DataValue)
            {
                DataRows.RemoveAt(DataRows.Count - 1);
            }

            NotifyPropertyChanged("DataRows");
        }


        private void UpdateParams()
        {
            foreach (var row in DataRows)
            {
                var lst = row.Data;

                if (lst.Count < CurrentNrOfSolutions.DataValue)
                {
                    var emptySolutionValues = Enumerable.Repeat(0M, CurrentNrOfSolutions.DataValue - lst.Count);
                    lst.AddRange(emptySolutionValues);
                }

                if (lst.Count > CurrentNrOfSolutions.DataValue)
                {
                    lst.RemoveRange(CurrentNrOfSolutions.DataValue, lst.Count - CurrentNrOfSolutions.DataValue);
                }

            }
        }


        public string GetStringValue()
        {
            var converter = new ExtendedNamedDecimalMap();
            foreach (var casVariableRow in DataRows)
            {
                converter.NamedDecimalMap.SetValuesFor(casVariableRow.Name, casVariableRow.Data);
            }
            if (CurrentDegree.DataValue != string.Empty)
            {
                converter.Extensions.Add(DEGREEKEY, CurrentDegree.DataValue);
            }
            return converter.ToString();
        }

    }

    internal class CasVariableRow
    {
        public CasVariableRow(string name, IEnumerable<decimal> values)
        {
            Name = name;
            Data = new List<decimal>(values);
        }


        public string Name { get; set; }
        public List<Decimal> Data { get; set; }

    }
}
