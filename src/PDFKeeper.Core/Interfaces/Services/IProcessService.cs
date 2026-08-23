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

namespace PDFKeeper.Core.Interfaces.Services
{
    /// <summary>
    /// Defines a service for managing processes.
    /// </summary>
    public interface IProcessService
    {
        /// <summary>
        /// Starts a process using the specified file path and optional
        /// arguments, returning the process ID (PID) of the started process.
        /// </summary>
        /// <param name="filePath">
        /// The path to the executable file to start.
        /// </param>
        /// <param name="args">
        /// The optional arguments to pass to the process.
        /// </param>
        /// <returns>
        /// The process ID (PID) of the started process.
        /// </returns>
        int Start(string filePath, string args = null);

        /// <summary>
        /// Starts a process using the specified file path and optional
        /// arguments, and then waits for the process to exit.
        /// </summary>
        /// <param name="filePath">
        /// The path to the executable file to start.
        /// </param>
        /// <param name="args">
        /// The optional arguments to pass to the process.
        /// </param>
        void StartAndWaitForExit(string filePath, string args = null);

        /// <summary>
        /// Closes the process with the specified process ID (PID).
        /// </summary>
        /// <param name="pid">
        /// The process ID (PID) of the process to close.
        /// </param>
        void Close(int pid);
    }
}
