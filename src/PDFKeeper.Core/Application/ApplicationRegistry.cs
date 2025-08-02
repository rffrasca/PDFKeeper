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

namespace PDFKeeper.Core.Application
{
    internal static class ApplicationRegistry
    {
        /// <summary>
        /// Gets the current user registry key path for the application.
        /// </summary>
        internal static string UserKeyPath => GetAbsoluteKeyPath(
            @"HKEY_CURRENT_USER\SOFTWARE");

        /// <summary>
        /// Gets the registry key path for application policies.
        /// </summary>
        internal static string PoliciesKeyPath => GetAbsoluteKeyPath(
            @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies");

        /// <summary>
        /// Gets the absolute registry key path for the application. 
        /// </summary>
        /// <param name="startingKeyPath">The starting registry key path.</param>
        /// <returns>The absolute key path.</returns>
        private static string GetAbsoluteKeyPath(string startingKeyPath)
        {
            var executingAssembly = new ExecutingAssembly();
            return string.Concat(
                startingKeyPath,
                @"\",
                executingAssembly.CompanyName,
                @"\",
                executingAssembly.ProductName);
        }
    }
}
