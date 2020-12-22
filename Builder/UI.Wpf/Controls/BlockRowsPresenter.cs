using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Questify.Builder.UI.Wpf.Controls
{
    public class BlockRowsPresenter : ItemsControl
    {
        static BlockRowsPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BlockRowsPresenter), new FrameworkPropertyMetadata(typeof(BlockRowsPresenter)));
        }

        private List<BlockRow> _blockRowsInBlock = new List<BlockRow>();


        internal List<BlockRow> BlockRowsInBlock { get { return _blockRowsInBlock; } }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            BlockGridBlockRow parentBlockGridRow = BlockGridHelper.FindVisualParent<BlockGridBlockRow>(this);

            if (parentBlockGridRow != null)
            {
                for (int rowIndex = 0; rowIndex < BlockRowsInBlock.Count; rowIndex++)
                {
                    BlockRow br = BlockRowsInBlock[rowIndex];

                    double forcedHeight = parentBlockGridRow.GetBlockRowMaxRowHeight(rowIndex);

                    if (br.DesiredSize.Height < forcedHeight)
                    {
                        br.ForcedHeight = forcedHeight;

                        br.Measure(new Size(br.DesiredSize.Width, forcedHeight));
                    }
                }
            }

            return base.ArrangeOverride(arrangeBounds);
        }



        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is BlockRow);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new BlockRow();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            BlockRow blockRow = element as BlockRow;

            if (blockRow != null)
            {
                blockRow.Prepare(item, BlockRowOwner);
                _blockRowsInBlock.Add(blockRow);
            }
        }

        internal BlockInRow BlockRowOwner
        {
            get { return BlockGridHelper.FindParent<BlockInRow>(this); }
        }
    }
}
