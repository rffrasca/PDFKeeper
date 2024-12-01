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

using PDFKeeper.Core.Application;
using PDFKeeper.Core.Extensions;
using System;
using System.IO;

namespace PDFKeeper.Core.Commands
{
    public class UploadStagingCommand : ICommand
    {
        private readonly FileInfo pdfFile;
        private readonly FileInfo xmlFile;

        /// <summary>
        /// Initializes a new instance of the UploadStagingCommand class that stages the PDF and
        /// corresponding XML for uploading when the Execute method is called.
        /// </summary>
        /// <param name="pdfFile">The PDF FileInfo object.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UploadStagingCommand(FileInfo pdfFile)
        {
            this.pdfFile = pdfFile ?? throw new ArgumentNullException(nameof(pdfFile));
            xmlFile = pdfFile.ChangeExtension("xml");
        }

        public void Execute()
        {
            var targetPdfFile = pdfFile.AppendGuidToFileName();
            targetPdfFile = targetPdfFile.ChangeDirectory(new ApplicationDirectory().GetDirectory(
                ApplicationDirectory.SpecialName.UploadStaging));
            pdfFile.MoveTo(targetPdfFile.FullName);
            if (xmlFile.Exists)
            {
                xmlFile.MoveTo(targetPdfFile.ChangeExtension("xml").FullName);
            }
        }
    }
}
