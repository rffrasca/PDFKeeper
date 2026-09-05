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
using System.Security;
using System.Text;

namespace PDFKeeper.Core.Services.Security
{
    /// <summary>
    /// Default implementation of the <see cref="ISecureDataService"/> interface.
    /// </summary>
    public sealed class SecureDataService : ISecureDataService
    {
        public IPinnedBytes ToPinnedByteArray(SecureString secureString)
        {
            if (secureString is null)
            {
                throw new ArgumentNullException(nameof(secureString));
            }

            IntPtr bstrPtr = IntPtr.Zero;

            try
            {
                bstrPtr = Marshal.SecureStringToBSTR(secureString);

                unsafe
                {
                    char* chars = (char*)bstrPtr.ToPointer();
                    int length = secureString.Length;
                    int byteCount = Encoding.UTF8.GetByteCount(chars, length);
                    byte[] bytes = new byte[byteCount];
                    GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

                    fixed (byte* bytesPtr = bytes)
                    {
                        Encoding.UTF8.GetBytes(chars, length, bytesPtr, byteCount);
                    }

                    return new PinnedBytes(bytes, handle);
                }
            }
            finally
            {
                if (bstrPtr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(bstrPtr);
                }
            }
        }

        public void SecureClear(IPinnedBytes pinnedBytes)
        {
            pinnedBytes?.Clear();
        }
    }
}
