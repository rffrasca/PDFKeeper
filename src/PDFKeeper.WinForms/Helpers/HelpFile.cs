// ****************************************************************************
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
// ****************************************************************************

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Helpers
{
    public class HelpFile
    {
        private readonly string helpFile;

        public HelpFile()
        {
            var product = Application.ProductName;
            helpFile = String.Concat(product, CultureInfo.CurrentCulture.ToString(), ".chm");
            if (!File.Exists(helpFile))
            {
                helpFile = String.Concat(product, ".en-US.chm");
            }
        }

        /// <summary>
        /// Gets the full name of the help file based on the current culture. If the help file is
        /// not available for the current culture, the help file for en-US is returned.
        /// </summary>
        public string FullName => helpFile;

        /// <summary>
        /// Gets the license topic file name.
        /// </summary>
        public static string LicenseTopicFileName => "COPYING.html";

        /// <summary>
        /// Gets the Third-Party Notices topic file name.
        /// </summary>
        public static string ThirdPartyNoticesTopicFileName => "THIRD-PARTY-NOTICES.html";

        /// <summary>
        /// Gets the Donate topic file name.
        /// </summary>
        public static string DonateTopicFileName => "Donate.html";
        
        /// <summary>
        /// Shows a topic page in the help file and waits for the help dialog to be closed by the
        /// user.
        /// </summary>
        /// <param name="topic">The topic file name that is contained in the help file.</param>
        public void Show(string topic)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.Windows),
                    "hh.exe");
                process.StartInfo.Arguments = String.Concat("ms-its:", helpFile, "::", topic);
                process.Start();
                process.WaitForExit();
            }
        }

        /// <summary>
        /// Shows a topic page in the help file modelessly.
        /// </summary>
        /// <param name="topic">The topic file name that is contained in the help file.</param>
        /// <param name="control">
        /// The parent dialog, form, or control object of the Help dialog.
        /// </param>
        public void Show(string topic, Control control)
        {
            Help.ShowHelp(control, helpFile, topic);
        }
    }
}
