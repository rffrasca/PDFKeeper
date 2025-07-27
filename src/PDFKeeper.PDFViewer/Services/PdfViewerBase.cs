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
using System.Diagnostics;
using System.IO;

namespace PDFKeeper.PDFViewer.Services
{
    public abstract class PdfViewerBase
    {
        /// <summary>
        /// Starts the bundled PDF viewer.
        /// </summary>
        /// <param name="args">The PDF viewer command line arguments.</param>
        /// <returns>The <see cref="Process.Id"/>.</returns>
        protected static int Start(string args)
        {
            using (var process = new Process())
            {
                var executingAssembly = new ExecutingAssembly();
                process.StartInfo.FileName = Path.Combine(
                    executingAssembly.DirectoryPath,
                    "SumatraPDF-3.5.2-64.exe");
                process.StartInfo.Arguments = args;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                return process.Id;
            }
        }
    }
}
