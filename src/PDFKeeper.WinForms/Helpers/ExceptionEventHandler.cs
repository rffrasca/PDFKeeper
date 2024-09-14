// *****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2024 Robert F. Frasca
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

using PDFKeeper.Core.Application;
using PDFKeeper.WinForms.Properties;
using PDFKeeper.WinForms.Services;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Helpers
{
    public static class ExceptionEventHandler
    {
        private static string logPath;

        public static void HandleThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            InitLogPath();
            var eventType = Resources.ThreadException;
            var fullName = e.Exception.GetType().FullName;
            var message = e.Exception.Message;
            var stackTrace = e.Exception.ToString();
            Log(eventType, stackTrace);
            Show(eventType, fullName, message);
            Application.Exit();
        }

        public static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }
            InitLogPath();
            var eventType = Resources.UnhandledException;
            var fullName = e.ExceptionObject.GetType().FullName;
            var stackTrace = e.ExceptionObject.ToString();
            Log(eventType, stackTrace);
            Show(eventType, fullName, null);
            Application.Exit();
        }

        private static void InitLogPath()
        {
            logPath = Path.Combine(
                new ApplicationDirectory().GetDirectory(
                    ApplicationDirectory.SpecialName.Log).FullName,
                "PDFKeeper.log");
        }

        private static void Log(string eventType, string trace)
        {
            var logMsg = string.Concat(
                "================================================================================",
                Environment.NewLine,
                DateTime.Now,
                ": ",
                eventType,
                Environment.NewLine,
                trace,
                Environment.NewLine);
            File.AppendAllText(logPath, logMsg);
        }

        private static void Show(string eventType, string name, string message)
        {
            var msg = string.Concat(
                eventType,
                Environment.NewLine,
                Environment.NewLine,
                name,
                ":",
                Environment.NewLine,
                message,
                Environment.NewLine,
                Environment.NewLine,
                ResourceHelper.GetString(
                    "StackTraceLogged",
                    logPath,
                    null));
            new MessageBoxService().ShowMessage(msg, true);
        }
    }
}
