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

using Microsoft.Win32;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Enums;
using PDFKeeper.Core.Interfaces.Services;
using System;

namespace PDFKeeper.Core.Services
{
    /// <summary>
    /// Default implementation of the <see cref="IApplicationPolicyService"/> interface.
    /// </summary>
    public sealed class ApplicationPolicyService : IApplicationPolicyService
    {
        private readonly IApplicationRegistryProvider applicationRegistryProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationPolicyService"/> class.
        /// </summary>
        /// <param name="applicationRegistryProvider">
        /// The <see cref="IApplicationRegistryProvider"/> instance.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public ApplicationPolicyService(IApplicationRegistryProvider applicationRegistryProvider)
#pragma warning restore IDE0290 // Use primary constructor
        {
            this.applicationRegistryProvider = applicationRegistryProvider;
        }

        public bool GetPolicyValue(ApplicationPolicy applicationPolicy)
        {
            if (DatabaseSession.PlatformName != DatabaseSession.CompatiblePlatformName.Sqlite)
            {
                if (Convert.ToBoolean(
                Registry.GetValue(
                    applicationRegistryProvider.PoliciesKeyPath,
                    applicationPolicy.ToString(), 0)
                is 1))
                {
                    return true;
                }

                //
                // Legacy fallback
                //

                var legacyKey = GetLegacyKeyName(applicationPolicy);

                if (Convert.ToBoolean(
                    Registry.GetValue(applicationRegistryProvider.PoliciesKeyPath, legacyKey, 0)
                    is 1))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Maps the current policy name to its legacy registry key name.
        /// </summary>
        /// <param name="applicationPolicy">
        /// The application policy.
        /// </param>
        /// <returns>
        /// The legacy registry key name.
        /// </returns>
        private static string GetLegacyKeyName(ApplicationPolicy applicationPolicy)
        {
#pragma warning disable IDE0066 // Convert switch statement to expression
            switch (applicationPolicy)
            {
                case ApplicationPolicy.DisableAllDocumentsListing:
                    return "HideAllDocuments";
                case ApplicationPolicy.BlockUIDuringUpload:
                    return "BlockingUpload";
                default:
                    return applicationPolicy.ToString();
            }
#pragma warning restore IDE0066 // Convert switch statement to expression
        }
    }
}
