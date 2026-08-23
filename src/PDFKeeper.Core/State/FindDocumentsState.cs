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

using PDFKeeper.Core.Models;
using System;

namespace PDFKeeper.Core.State
{
    /// <summary>
    /// Maintains shared application state for document‑finding operations, including the
    /// current <see cref="FindDocumentsParam"/> instance and a callback that is invoked
    /// whenever the parameter value changes.
    /// </summary>
    internal static class FindDocumentsState
    {
        private static FindDocumentsParam findDocumentsParam;

        /// <summary>
        /// Gets or sets an action that is invoked whenever the
        /// <see cref="FindDocumentsParam"/> value changes.
        /// </summary>
        internal static Action OnFindDocumentsParamChanged { get; set; }

        /// <summary>
        /// Gets or sets the current <see cref="FindDocumentsParam"/> used for document
        /// search operations. Setting this property updates the stored value and triggers
        /// <see cref="OnFindDocumentsParamChanged"/> if a callback has been assigned.
        /// </summary>
        internal static FindDocumentsParam FindDocumentsParam
        {
            get => findDocumentsParam;
            set
            {
                findDocumentsParam = value;
                OnFindDocumentsParamChanged?.Invoke();
            }
        }
    }
}
