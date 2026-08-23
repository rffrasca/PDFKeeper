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

namespace PDFKeeper.Core.Models
{
    /// <summary>
    /// Application information data transfer object (DTO) that encapsulates details about the
    /// application, such as its base directory, company name, product name, and product version.
    /// </summary>
    public sealed class ApplicationInfoDto
    {
        /// <summary>
        /// The base directory of the application, typically where the executable is located.
        /// </summary>
        public string BaseDirectory { get; set; }

        /// <summary>
        /// The name of the company that developed the application.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The name of the product/application.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// The version of the product/application.
        /// </summary>
        public string ProductVersion { get; set; }
    }
}
