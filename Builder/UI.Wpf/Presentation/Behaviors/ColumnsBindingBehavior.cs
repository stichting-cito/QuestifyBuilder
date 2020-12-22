﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Questify.Builder.UI.Wpf.Presentation.Behaviors
{
    public class ColumnsBindingBehavior : Behavior<DataGrid>
    {
        public ObservableCollection<DataGridColumn> Columns
        {
            get { return (ObservableCollection<DataGridColumn>)base.GetValue(ColumnsProperty); }
            set { base.SetValue(ColumnsProperty, value); }
        }

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register("Columns",
            typeof(ObservableCollection<DataGridColumn>), typeof(ColumnsBindingBehavior),
                new PropertyMetadata(OnDataGridColumnsPropertyChanged));

        private static void OnDataGridColumnsPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var context = source as ColumnsBindingBehavior;
            if (context._datagridColumns == null) return;
            var oldItems = e.OldValue as ObservableCollection<DataGridColumn>;

            if (oldItems != null)
            {
                foreach (var one in oldItems)
                    context._datagridColumns.Remove(one);

                oldItems.CollectionChanged -= context.collectionChanged;
            }

            var newItems = e.NewValue as ObservableCollection<DataGridColumn>;

            if (newItems != null)
            {
                foreach (var one in newItems)
                    context._datagridColumns.Add(one);

                newItems.CollectionChanged += context.collectionChanged;
            }
        }

        private ObservableCollection<DataGridColumn> _datagridColumns;

        protected override void OnAttached()
        {
            base.OnAttached();

            this._datagridColumns = AssociatedObject.Columns;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this._datagridColumns = null;
        }


        private void collectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                        foreach (DataGridColumn one in e.NewItems)
                            _datagridColumns.Add(one);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (DataGridColumn one in e.OldItems)
                            _datagridColumns.Remove(one);
                    break;

                case NotifyCollectionChangedAction.Move:
                    _datagridColumns.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    _datagridColumns.Clear();
                    if (e.NewItems != null)
                        foreach (DataGridColumn one in e.NewItems)
                            _datagridColumns.Add(one);
                    break;
            }
        }
    }
}
