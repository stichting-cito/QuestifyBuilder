using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Questify.Builder.UI.Wpf.Controls
{

    public class BlockInRowSeparator : ContentControl
    {

        static BlockInRowSeparator()
        {
            var ownerType = typeof(BlockInRowSeparator);
        }

        public override void OnApplyTemplate()
        {
            PrepareSeparator(this, BlockInRowSeparatorOwner);

            base.OnApplyTemplate();
        }


        BlockInRow _owner;


        public BlockInRowSeparator()
        {
        }

        public BlockGridColumn Column
        {
            get { return (BlockGridSeparatorColumn)GetValue(ColumnProperty); }
            set { SetValue(ColumnProperty, value); }
        }

        public static readonly DependencyProperty ColumnProperty =
    DependencyProperty.Register("Column", typeof(BlockGridColumn), typeof(BlockGridSeparatorColumn), new PropertyMetadata(null));


        public bool IsInBetweenBlocks
        {
            get { return (bool)GetValue(IsInBetweenBlocksProperty); }
            set { SetValue(IsInBetweenBlocksProperty, value); }
        }

        public static readonly DependencyProperty IsInBetweenBlocksProperty =
    DependencyProperty.Register("IsInBetweenBlocks", typeof(bool), typeof(BlockInRowSeparator), new PropertyMetadata(false, new PropertyChangedCallback(IsBetweenBlocksProperty_PropertyChanged)));


        private static void IsBetweenBlocksProperty_PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            BlockInRowSeparator separator = (BlockInRowSeparator)obj;

            separator.BuildVisualTree();
        }



        internal void PrepareSeparator(object item, BlockInRow ownerBlockInRow)
        {
            System.Diagnostics.Debug.Assert(_owner == null || _owner == ownerBlockInRow, "_owner should be null before PrepareCell is called or the same value as the ownerRow.");

            _owner = ownerBlockInRow;

            BlockGrid blockGrid = _owner.BlockGrid;
            if (blockGrid != null)
            {

                BlockGrid owner = BlockInRowSeparatorOwner.BlockInRowOwner.BlockGridBlockRowOwner;

                if (owner != null)
                {
                    Column = owner.Columns.FirstOrDefault(x => x.GetType() == typeof(BlockGridSeparatorColumn));
                }

                if ((Content as FrameworkElement) == null)
                {
                    BuildVisualTree();

                    if (!NeedsVisualTree)
                    {
                        Content = item;
                    }
                }
            }

            BlockGridHelper.TransferProperty(this, StyleProperty);

            CoerceValue(ClipProperty);
        }

        internal void ClearSeparator(BlockInRow ownerRow)
        {
            System.Diagnostics.Debug.Assert(_owner == ownerRow, "_owner should be the same as the BlockGridGridRow that is clearing the cell.");
            _owner = null;
        }



        internal void BuildVisualTree()
        {
            if (NeedsVisualTree)
            {
                var column = Column as BlockGridSeparatorColumn;
                if (column != null)
                {
                    Content = column.BuildVisualTree(false, null, this);
                }
            }
        }



        protected override Size MeasureOverride(Size constraint)
        {
            BlockRowsPresenter blockRowsPresenter = BlockGridHelper.FindFirstVisualChild<BlockRowsPresenter>(BlockInRowSeparatorOwner);
            constraint.Height = blockRowsPresenter.DesiredSize.Height;
            constraint.Width = Column.Width;

            IsInBetweenBlocks = BlockInRowSeparatorOwner.BlockInRowOwner.BlockInRowHasSuccessor(BlockInRowSeparatorOwner);


            if (VisualChildrenCount > 0)
            {
                UIElement child = (UIElement)(this.GetVisualChild(0));
                child.Measure(constraint);

            }
            return constraint;
        }



        internal BlockInRow BlockInRowSeparatorOwner
        {
            get { return BlockGridHelper.FindParent<BlockInRow>(this); }
        }

        private bool NeedsVisualTree
        {
            get
            {
                return (ContentTemplate == null) && (ContentTemplateSelector == null);
            }
        }


    }
}
