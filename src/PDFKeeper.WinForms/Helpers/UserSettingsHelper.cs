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

using PDFKeeper.WinForms.Properties;
using System.Configuration;

namespace PDFKeeper.WinForms.Helpers
{
    public class UserSettingsHelper
    {
        private readonly Configuration configuration;

        public UserSettingsHelper()
        {
            configuration = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.PerUserRoamingAndLocal);
        }

        /// <summary>
        /// Upgrades user settings when user.config does not exist for the current version and
        /// UpgradeSettings is set to true in user.config.
        /// </summary>
        public void Upgrade()
        {
            if (!configuration.HasFile)
            {
                if (Settings.Default.UpgradeSettings)
                {
                    Settings.Default.Upgrade();
                    Settings.Default.UpgradeSettings = false;
                    Settings.Default.Save();
                }
            }
        }
    }
}
