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
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace PDFKeeper.Core.Extensions
{
    internal static class SecureStringExtension
    {
        /// <summary>
        /// Gets the contents of the <see cref="SecureString"/> object as a <see cref="byte"/>
        /// array.
        /// </summary>
        /// <param name="secureString">The <see cref="SecureString"/> object.</param>
        /// <returns>The <see cref="byte"/> array.</returns>
        internal static byte[] GetAsByteArray(this SecureString secureString)
        {
            IntPtr intPtr = default;
            try
            {
                intPtr = Marshal.SecureStringToBSTR(secureString);
                return Encoding.UTF8.GetBytes(Marshal.PtrToStringAuto(intPtr));
            }
            finally
            {
                Marshal.ZeroFreeBSTR(intPtr);
            }
        }
    }
}
