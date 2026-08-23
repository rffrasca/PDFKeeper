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
using System;
using System.Diagnostics;

namespace PDFKeeper.Core.Services
{
    /// <summary>
    /// Default implementation of the <see cref="IProcessService"/> interface.
    /// </summary>
    public sealed class ProcessService : IProcessService
    {
        public int Start(string filePath, string args = null)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = filePath;
                process.StartInfo.Arguments = args ?? string.Empty;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                return process.Id;
            }
        }

        public void StartAndWaitForExit(string filePath, string args = null)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = filePath;
                process.StartInfo.Arguments = args ?? string.Empty;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                process.WaitForExit();
            }
        }

        public void Close(int pid)
        {
            try
            {
                using (var process = Process.GetProcessById(pid))
                {
                    process.CloseMainWindow();
                    process.Kill();
                }
            }
            catch (ArgumentException) { }
        }
    }
}
