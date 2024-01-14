// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2024 Robert F. Frasca
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

using PDFKeeper.Core.Models;
using System;

namespace PDFKeeper.Core.Application
{
    public static class FindDocumentsViewState
    {
        private static FindDocumentsParam findDocumentsParam;

        /// <summary>
        /// Notifies the subscribing view that the FindDocumentsParam property has changed.
        /// </summary>
        public static event EventHandler FindDocumentsParamChanged;

        /// <summary>
        /// Gets or sets the FindDocumentsParam object.
        /// </summary>
        public static FindDocumentsParam FindDocumentsParam
        {
            get => findDocumentsParam;
            set
            {
                findDocumentsParam = value;
                OnFindDocumentsParamChanged();
            }
        }

        /// <summary>
        /// Raises the FindDocumentsParamChanged event to notify the subscribing view that
        /// FindDocumentsParam property has changed.
        /// </summary>
        private static void OnFindDocumentsParamChanged()
        {
            FindDocumentsParamChanged?.Invoke(null, null);
        }
    }
}
