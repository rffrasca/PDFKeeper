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

using System.Security;

namespace PDFKeeper.Core.Interfaces.Services.Security
{
    /// <summary>
    /// Defines a service for secure string conversion and memory management.
    /// </summary>
    public interface ISecureDataService
    {
        /// <summary>
        /// Extracts the bytes from a <see cref="SecureString"/> into an <see cref="IPinnedBytes"/>
        /// instance.
        /// </summary>
        /// <remarks>
        /// Caller MUST call <see cref="SecureClear"/> on the returned <see cref="IPinnedBytes"/>
        /// instance when finished.
        /// </remarks>
        /// <param name="secureString">
        /// The <see cref="SecureString"/> to convert to an <see cref="IPinnedBytes"/> instance.
        /// </param>
        /// <returns>
        /// An <see cref="IPinnedBytes"/> instance containing the UTF-8 byte representation of
        /// the provided <see cref="SecureString"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="secureString"/> is <c>null</c>.
        /// </exception>
        IPinnedBytes ToPinnedByteArray(SecureString secureString);

        /// <summary>
        /// Clears the contents of the provided <see cref="IPinnedBytes"/> instance securely.
        /// </summary>
        /// <param name="pinnedBytes">
        /// The <see cref="IPinnedBytes"/> instance to clear.
        /// </param>
        void SecureClear(IPinnedBytes pinnedBytes);
    }
}
