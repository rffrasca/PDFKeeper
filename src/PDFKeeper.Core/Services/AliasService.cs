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
using PDFKeeper.Core.FileIO.Serializers;
using PDFKeeper.Core.Properties;
using System;
using System.Collections.Generic;
using System.IO;

namespace PDFKeeper.Core.Services
{
    public class AliasService : IAliasService
    {
        private readonly FileInfo aliasesJsonFile;
        private readonly Dictionary<string, string> aliases;
        
        /// <summary>
        /// Initializes a new instance of the AliasService class, loading alias mappings from a
        /// JSON file or creating the file with default values if it does not exist.
        /// </summary>
        public AliasService()
        {
            var defaultAliases = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Author", Resources.Author },
                { "Subject", Resources.Subject },
                { "Category", Resources.Category },
                { "Tax Year", Resources.TaxYear }
            };
            aliasesJsonFile = new FileInfo(
                Path.Combine(
                    new ApplicationDirectory().GetDirectory(
                        ApplicationDirectory.SpecialName.ApplicationData).FullName,
                    "aliases.json"));

            if (!aliasesJsonFile.Exists)
            {
                JsonSerializer.SerializeToFile<Dictionary<string, string>>(
                    defaultAliases,
                    aliasesJsonFile);
                aliases = new Dictionary<string, string>(defaultAliases);
            }
            else
            {
                var loadedAliases = JsonSerializer.DeserializeFromFile<Dictionary<string, string>>(
                    aliasesJsonFile);
                aliases = new Dictionary<string, string>(defaultAliases);

                if (loadedAliases != null)
                {
                    foreach (var kvp in loadedAliases)
                    {
                        aliases[kvp.Key] = kvp.Value;
                    }
                }
            }
        }

        public string GetAlias(string key)
            => aliases.TryGetValue(key, out var alias) ? alias : key;

        public void SetAlias(string key, string alias)
        {
            aliases[key] = alias;
            JsonSerializer.SerializeToFile<Dictionary<string, string>>(aliases, aliasesJsonFile);
        }
    }
}
