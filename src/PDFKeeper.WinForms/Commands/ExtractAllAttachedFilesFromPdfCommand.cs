// *****************************************************************************
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
// *****************************************************************************

using PDFKeeper.Core.Commands;
using PDFKeeper.Core.FileIO.PDF;
using PDFKeeper.Core.Presenters;

namespace PDFKeeper.WinForms.Commands
{
    public class ExtractAllAttachedFilesFromPdfCommand : ICommand
    {
        private readonly MainPresenter presenter;
        private readonly PdfFile.AttachedFilesType attachedFilesType;

        /// <summary>
        /// Initializes a new instance of the ExtractAllAttachedFilesFromPdfCommand class that
        /// extracts all attached files from the PDF for the selected document to a directory or
        /// ZIP file when executed.
        /// </summary>
        /// <param name="presenter">
        /// The MainPresenter instance.
        /// </param>
        /// <param name="attachedFilesType">
        /// The type of attached files in the PDF to extract.
        /// </param>
        public ExtractAllAttachedFilesFromPdfCommand(
            MainPresenter presenter,
            PdfFile.AttachedFilesType attachedFilesType)
        {
            this.presenter = presenter;
            this.attachedFilesType = attachedFilesType;
        }

        public void Execute()
        {
            presenter.ExtractAllAttachedFilesFromCurrentDocumentPdf(attachedFilesType);
        }
    }
}
