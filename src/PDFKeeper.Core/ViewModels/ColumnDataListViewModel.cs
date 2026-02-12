// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2026 Robert F. Frasca
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
// ****************************************************************************

using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Services;
using System;
using System.Collections.Generic;

namespace PDFKeeper.Core.ViewModels
{
    [CLSCompliant(false)]
    public class ColumnDataListViewModel : ViewModelBase
    {
        private readonly ColumnName columnName;
        private IMessageBoxService messageBoxService;
        private IEnumerable<string> items;

        /// <summary>
        /// Column name data to list.
        /// <para>
        /// <see cref="Author"/>, <see cref="Subject"/>, and <see cref="Category"/> are from the
        /// repository. <see cref="TaxYear"/> is a range of tax years starting with the last 25
        /// years and 1 year into the future.
        /// </para>
        /// </summary>
        public enum ColumnName { Author, Subject, Category, TaxYear }

        public ColumnDataListViewModel(ColumnName columnName)
        {
            this.columnName = columnName;
            GetServices(ServiceLocator.Services);
            GetColumnData();
        }
        
        public IEnumerable<string> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        protected override void GetServices(IServiceProvider serviceProvider)
        {
            messageBoxService = serviceProvider.GetService<IMessageBoxService>();
        }

        private void GetColumnData()
        {
            try
            {
                switch (columnName)
                {
                    case ColumnName.Author:
                        Items = ColumnData.GetAuthors(null, null, null);
                        break;
                    case ColumnName.Subject:
                        Items = ColumnData.GetSubjects(null, null, null);
                        break;
                    case ColumnName.Category:
                        Items = ColumnData.GetCategories(null, null, null);
                        break;
                    case ColumnName.TaxYear:
                        Items = ColumnData.GetRangeOfTaxYears();
                        break;
                }
            }
            catch (DatabaseException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
            }
        }
    }
}
