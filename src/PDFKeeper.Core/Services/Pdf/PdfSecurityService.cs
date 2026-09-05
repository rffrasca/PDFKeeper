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

using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using PDFKeeper.Core.Enums;
using PDFKeeper.Core.Interfaces.Services.Pdf;
using PDFKeeper.Core.Interfaces.Services.Security;
using System;

namespace PDFKeeper.Core.Services.Pdf
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfSecurityService"/> interface.
    /// </summary>
    public sealed class PdfSecurityService : IPdfSecurityService
    {
        private readonly ISecureDataService secureDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfSecurityService"/> class.
        /// </summary>
        /// <param name="secureDataService">
        /// The <see cref="ISecureDataService"/> instance.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public PdfSecurityService(ISecureDataService secureDataService)
#pragma warning restore IDE0290 // Use primary constructor
        {
            this.secureDataService = secureDataService;
        }

        public PdfPasswordType GetPasswordType(string pdfPath)
        {
            try
            {
                using (var reader = new PdfReader(pdfPath))
                {
                    using (var document = new PdfDocument(reader))
                    {
                        if (reader.IsOpenedWithFullPermission())
                        {
                            return PdfPasswordType.None;
                        }
                        else
                        {
                            return PdfPasswordType.Owner;
                        }
                    }
                }
            }
            catch (BadPasswordException)
            {
                return PdfPasswordType.User;
            }
            catch (iText.IO.Exceptions.IOException)
            {
                return PdfPasswordType.Unknown;
            }
        }

        public bool ValidatePdfOwnerPassword(string pdfPath, byte[] pdfOwnerPassword)
        {
            if (pdfOwnerPassword is null)
            {
                throw new ArgumentNullException(nameof(pdfOwnerPassword));
            }

            try
            {
                using (var reader = new PdfReader(
                    pdfPath,
                    new ReaderProperties().SetPassword(pdfOwnerPassword)))
                {
                    using (var document = new PdfDocument(reader)) { }
                }

                return true;
            }
            catch (BadPasswordException)
            {
                return false;
            }
        }
    }
}
