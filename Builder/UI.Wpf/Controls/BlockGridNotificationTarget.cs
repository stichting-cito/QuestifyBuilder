using System;

namespace Questify.Builder.UI.Wpf.Controls
{
    [Flags]
    internal enum BlockGridNotificationTarget
    {
        None = 0x00, Cells = 0x01,
        CellsPresenter = 0x02,
        Columns = 0x04,
        ColumnCollection = 0x08,
        ColumnHeaders = 0x10,
        ColumnHeadersPresenter = 0x20,
        DataGrid = 0x40,
        DetailsPresenter = 0x80,
        RefreshCellContent = 0x100,
        RowHeaders = 0x200,
        Rows = 0x400,
        All = 0xFFF,
    }
}
