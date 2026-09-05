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

using PDFKeeper.Core.Interfaces.Services.Security;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PDFKeeper.Core.Services.Security
{
    /// <summary>
    /// Default implementation of the <see cref="IPinnedBytes"/> interface.
    /// </summary>
    public sealed class PinnedBytes : IPinnedBytes
    {
        private readonly byte[] bytes;
        private readonly GCHandle handle;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="PinnedBytes"/> class.
        /// </summary>
        /// <param name="bytes">
        /// The byte array to be pinned.
        /// </param>
        /// <param name="handle">
        /// The GCHandle associated with the pinned byte array.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public PinnedBytes(byte[] bytes, GCHandle handle)
#pragma warning restore IDE0290 // Use primary constructor
        {
            this.bytes = bytes;
            this.handle = handle;
        }

        public byte[] GetBytes()
        {
            return bytes;
        }

        public string GetString()
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public void Clear()
        {
            if (!disposed)
            {
                if (bytes != null)
                {
                    Array.Clear(bytes, 0, bytes.Length);
                }

                if (handle.IsAllocated)
                {
                    handle.Free();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Clear();
        }
    }
}
