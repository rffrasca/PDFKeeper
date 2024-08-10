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

using PDFKeeper.Core.Helpers;
using System.Collections.ObjectModel;
using System.IO;

namespace PDFKeeper.Core.Rules
{
    internal class OracleWalletRule : RuleBase
    {
        private readonly string path;

        /// <summary>
        /// Initializes a new instance of the OracleWalletRule class that verifies the directory
        /// contains the required files.
        /// </summary>
        /// <param name="path">The Oracle Wallet path.</param>
        internal OracleWalletRule(string path)
        {
            this.path = path;
            CheckForViolation();
        }

        protected override void CheckForViolation()
        {
            ViolationFound = false;
            ViolationMessage = null;
            var requiredFiles = new Collection<string>
            {
                "cwallet.sso",
                "ewallet.p12",
                "ewallet.pem",
                "keystore.jks",
                "ojdbc.properties",
                "README",
                "sqlnet.ora",
                "tnsnames.ora",
                "truststore.jks"
            };
            foreach (var item in requiredFiles)
            {
                if (!new FileInfo(Path.Combine(path, item)).Exists)
                {
                    ViolationFound = true;
                    ViolationMessage = ResourceHelper.GetString("NotOracleWallet", path, null);
                }
            }
        }
    }
}
