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

using PDFKeeper.Core.Enums;
using System;

namespace PDFKeeper.Core.Interfaces.Services
{
    /// <summary>
    /// Defines an interface that provides centralized exception handling for the application.
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Logs the exception and displays a formatted error message to the user.
        /// </summary>
        /// <param name="exception">
        /// The exception to handle.
        /// </param>
        /// <param name="exceptionType">
        /// The type of exception.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="exception"/> is null.
        /// </exception>
        void HandleException(Exception exception, ExceptionType exceptionType);
    }
}
