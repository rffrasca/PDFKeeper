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

using PDFKeeper.Core.Application;
using PDFKeeper.Core.FileIO;
using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.Core.Interfaces.Services.Pdf;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace PDFKeeper.Core.Services.Pdf
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfViewerService"/> interface.
    /// </summary>
    public sealed class PdfViewerService : IPdfViewerService
    {
        private readonly IFileCache fileCache;
        private readonly IProcessService processService;
        private readonly List<int> restrictedViewerPids;
        private readonly string bundledViewerPath;

        /// <summary>
        /// Creates a new instance of the <see cref="PdfViewerService"/> class.
        /// </summary>
        /// <param name="fileCache">The file cache.</param>
        /// <param name="processService">The process service.</param>
        public PdfViewerService(IFileCache fileCache, IProcessService processService)
        {
            this.fileCache = fileCache;
            this.processService = processService;
            var executingAssembly = new ExecutingAssembly();
#pragma warning disable IDE0028 // Simplify collection initialization
            restrictedViewerPids = new List<int>();
#pragma warning restore IDE0028 // Simplify collection initialization
            bundledViewerPath = Path.Combine(
                executingAssembly.DirectoryPath,
                "SumatraPDF-3.5.2-64.exe");
        }

        public void CloseRestrictedViewer()
        {
            foreach (var pid in restrictedViewerPids.ToArray())
            {
                processService.Close(pid);
                restrictedViewerPids.Remove(pid);
            }
        }

        public void OpenPdf(int documentId, bool openPdfWithDefaultApplication)
        {
            var pdfPath = fileCache.GetPdfFile(documentId).FullName;
            
            if (openPdfWithDefaultApplication)
            {
                Process.Start(pdfPath);
            }
            else
            {
                processService.Start(bundledViewerPath, $"\"{pdfPath}\"");
            }
        }

        public void OpenPdfInRestrictedViewer(string pdfPath)
        {
            restrictedViewerPids.Add(
                processService.Start(bundledViewerPath, $"-restrict \"{pdfPath}\""));
        }
    }
}
