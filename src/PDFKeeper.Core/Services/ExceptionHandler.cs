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
using PDFKeeper.Core.Enums;
using PDFKeeper.Core.Helpers;
using PDFKeeper.Core.Properties;
using System;
using System.IO;

namespace PDFKeeper.Core.Services
{
    /// <summary>
    /// Default implementation of the <see cref="IExceptionHandler"/> interface.
    /// </summary>
    public sealed class ExceptionHandler : IExceptionHandler
    {
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandler"/> class.
        /// </summary>
        /// <param name="messageBoxService">A dialog service that displays messages.</param>
#pragma warning disable IDE0290 // Use primary constructor
        public ExceptionHandler(IMessageBoxService messageBoxService)
#pragma warning restore IDE0290 // Use primary constructor
        {
            this.messageBoxService = messageBoxService;
        }

        public void Handle(Exception exception, ExceptionType exceptionType)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var headerText = exceptionType == ExceptionType.UnhandledException
                ? Resources.UnhandledException
                : Resources.ThreadException;
            var logPath = Path.Combine(
                new ApplicationDirectory().GetDirectory(
                    ApplicationDirectory.SpecialName.Log).FullName,
                "PDFKeeper.log");

            LogException(exception, headerText, logPath);
            ShowException(exception, headerText, logPath);
        }

        /// <summary>
        /// Logs the exception to the PDFKeeper.log file.
        /// </summary>
        private static void LogException(Exception exception, string headerText, string logPath)
        {
            var message = string.Concat(
                "================================================================================",
                Environment.NewLine,
                DateTime.Now,
                ": ",
                headerText,
                Environment.NewLine,
                exception.ToString(),
                Environment.NewLine);

            File.AppendAllText(logPath, message);
        }

        /// <summary>
        /// Displays the formatted exception message to the user.
        /// </summary>
        private void ShowException(Exception exception, string headerText, string logPath)
        {
            var message = string.Concat(
                headerText,
                Environment.NewLine,
                Environment.NewLine,
                exception.GetType().FullName,
                ":",
                Environment.NewLine,
                exception.Message,
                Environment.NewLine,
                Environment.NewLine,
                ResourceHelper.GetString(
                    Resources.ResourceManager,
                    "StackTraceLogged",
                    logPath));

            messageBoxService.ShowMessage(message, true);
        }
    }
}
