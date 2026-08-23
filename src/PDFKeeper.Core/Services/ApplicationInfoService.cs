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

using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.Core.Models;
using System;
using System.Diagnostics;
using System.Reflection;

namespace PDFKeeper.Core.Services
{
    /// <summary>
    /// Default implementation of the <see cref="IApplicationInfoService"/> interface.
    /// </summary>
    public sealed class ApplicationInfoService : IApplicationInfoService
    {
        public ApplicationInfoDto GetApplicationInfo()
        {
            var assembly = Assembly.GetEntryAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return new ApplicationInfoDto
            {
                BaseDirectory = AppDomain.CurrentDomain.BaseDirectory,
                CompanyName = fileVersionInfo.CompanyName,
                ProductName = fileVersionInfo.ProductName,
                ProductVersion = fileVersionInfo.ProductVersion
            };
        }
    }
}
