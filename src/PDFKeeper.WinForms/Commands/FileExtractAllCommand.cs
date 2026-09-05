// *****************************************************************************
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
// *****************************************************************************

using PDFKeeper.Core.Enums;
using PDFKeeper.Core.ViewModels;
using System;
using System.Windows.Input;

namespace PDFKeeper.WinForms.Commands
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileExtractAllCommand"/> class that
    /// extracts all attached files from the PDF for the selected document to a directory
    /// or ZIP file when executed.
    /// </summary>
    internal class FileExtractAllCommand : ICommand
    {
        private readonly MainViewModel mainViewModel;
        private readonly PdfAttachmentType pdfAttachmentType;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileExtractAllCommand"/> class.
        /// </summary>
        /// <param name="mainViewModel">
        /// The <see cref="MainViewModel"/> instance.
        /// </param>
        /// <param name="pdfAttachmentType">
        /// The type of attached files in the PDF to extract.
        /// </param>
        internal FileExtractAllCommand(
            MainViewModel mainViewModel,
            PdfAttachmentType pdfAttachmentType)
        {
            this.mainViewModel = mainViewModel;
            this.pdfAttachmentType = pdfAttachmentType;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            throw new NotSupportedException();
        }

        public void Execute(object parameter)
        {
            mainViewModel.ExtractAllAttachedFromCurrentDocumentPdfCommand.Execute(
                pdfAttachmentType);
        }
    }
}
