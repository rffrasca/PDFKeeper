// ****************************************************************************
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
// ****************************************************************************

using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.Application;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Services;
using PDFKeeper.Core.ViewModels;
using System;
using System.Linq;

namespace PDFKeeper.Core.Presenters
{
    public class SetSubjectPresenter : PresenterBase<StringEnumerableViewModel>
    {
        private IMessageBoxService messageBoxService;

        public SetSubjectPresenter()
        {
            GetServices(ServicesLocator.Services);

            try
            {
                ViewModel = new StringEnumerableViewModel
                {
                    Items = ColumnData.GetSubjects(
                        null,
                        null,
                        null).OrderBy(subject => subject).ToArray()
                };
            }
            catch (DatabaseException ex)
            {
                messageBoxService.ShowMessage(ex.Message, true);
            }
        }

        protected override void GetServices(IServiceProvider serviceProvider)
        {
            messageBoxService = serviceProvider.GetService<IMessageBoxService>();
        }
    }
}
