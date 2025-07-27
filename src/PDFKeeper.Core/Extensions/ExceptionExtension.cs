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

using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.Application;
using PDFKeeper.Core.Helpers;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Services;
using System;
using System.IO;

namespace PDFKeeper.Core.Extensions
{
    public static class ExceptionExtension
    {
        private static string headerText;
        private static string logPath;

        public enum ExceptionType
        {
            UnhandledException,
            ThreadException
        }

        /// <summary>
        /// Handles an <see cref="Exception"/> of <see cref="ExceptionType"/> by logging and
        /// showing the <see cref="Exception"/> in a <c>MessageBox</c>.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> object.</param>
        /// <param name="exceptionType">The <see cref="ExceptionType"/>.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void HandleException(this Exception exception, ExceptionType exceptionType)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            if (exceptionType.Equals(ExceptionType.UnhandledException))
            {
                headerText = Resources.UnhandledException;
            }
            else if (exceptionType.Equals(ExceptionType.ThreadException))
            {
                headerText = Resources.ThreadException;
            }

            var applicationDirectory = new ApplicationDirectory();
            logPath = Path.Combine(
                applicationDirectory.GetDirectory(
                    ApplicationDirectory.SpecialName.Log).FullName,
                "PDFKeeper.log");
            LogException(exception);
            ShowException(exception);
        }

        /// <summary>
        /// Logs the <see cref="Exception"/>. 
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> object.</param>
        private static void LogException(Exception exception)
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
        /// Shows the <see cref="Exception"/> in a <c>MessageBox</c>.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> object.</param>
        private static void ShowException(Exception exception)
        {
            var messageBoxService = ServiceLocator.Services.GetService<IMessageBoxService>();
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
