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

using System;

namespace PDFKeeper.Core.Interfaces.Services.Security
{
    /// <summary>
    /// Defines an interface for a byte array that is pinned in memory
    /// to prevent it from being moved by the garbage collector.
    /// </summary>
    public interface IPinnedBytes : IDisposable
    {
        /// <summary>
        /// Returns the pinned byte array as a byte array.
        /// </summary>
        /// <returns>
        /// The pinned byte array.
        /// </returns>
        byte[] GetBytes();

        /// <summary>
        /// Returns the pinned byte array as a string, using UTF-8 encoding.
        /// </summary>
        /// <returns>
        /// The pinned byte array as a string, using UTF-8 encoding.
        /// </returns>
        string GetString();

        /// <summary>
        /// Clears the byte array and frees the GCHandle.
        /// </summary>
        /// <remarks>
        /// This method should be called when the PinnedBytes instance is no
        /// longer needed to ensure that sensitive data is cleared from memory.
        /// </remarks>
        void Clear();
    }
}
