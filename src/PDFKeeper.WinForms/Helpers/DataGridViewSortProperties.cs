// *****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2025 Robert F. Frasca
// *
// * This file is part of PDFKeeper.
// *
// * PDFKeeper is free software: you can redistribute it and/or modify it
// * under the terms of the GNU General Public License as published by the
// * Free Software Foundation, either version 3 of the License, or (at your
// * option) any later version.
// *
// * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
// * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// * more details.
// *
// * You should have received a copy of the GNU General Public License along
// * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Commands
{
    public class DataGridViewSortProperties
    {
        private DataGridViewColumn sortedColumn;
        private SortOrder sortOrder;
        private int sortColumnIndex;
        private ListSortDirection sortDirection;

        public DataGridViewSortProperties()
        {
            sortColumnIndex = 2;
            sortDirection = ListSortDirection.Ascending;
        }

        public DataGridViewColumn SortedColumn
        {
            get => sortedColumn;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                sortedColumn = value;
                sortColumnIndex = value.Index;
            }
        }

        public SortOrder SortOrder
        {
            get => sortOrder;
            set
            {
                if (value.Equals(SortOrder.Ascending))
                {
                    sortDirection = ListSortDirection.Ascending;
                }
                else if (value.Equals(SortOrder.Descending))
                {
                    sortDirection = ListSortDirection.Descending;
                }
                sortOrder = value;
            }
        }

        public int SortColumnIndex => sortColumnIndex;
        public ListSortDirection SortDirection => sortDirection;
    }
}
