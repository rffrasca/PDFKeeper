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

using Microsoft.Win32;
using PDFKeeper.Core.DataAccess;
using System;

namespace PDFKeeper.Core.Application
{
    internal class ApplicationPolicy
    {
        internal enum PolicyName
        {
            HideAllDocuments,
            BlockingUpload
        }

        /// <summary>
        /// Gets the application policy applied state.
        /// </summary>
        /// <param name="policy">The <see cref="PolicyName"/>.</param>
        /// <returns>
        /// <c>true</c> or <c>false</c> if policy is applied. When
        /// <see cref="DatabaseSession.PlatformName"/> is
        /// <see cref="DatabaseSession.CompatiblePlatformName.Sqlite"/>, <c>false</c> will be
        /// returned.
        /// </returns>
        internal static bool GetPolicyValue(PolicyName policy)
        {
            if (DatabaseSession.PlatformName.Equals(DatabaseSession.CompatiblePlatformName.Sqlite))
            {
                return false;               
            }
            else
            {
                return Convert.ToBoolean(Registry.GetValue(ApplicationRegistry.PoliciesKeyPath,
                    policy.ToString(), 0) is 1);
            }
        }
    }
}
