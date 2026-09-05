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

using PDFKeeper.Core.Helpers;
using PDFKeeper.Core.Interfaces.Rules;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Properties;
using System;

namespace PDFKeeper.Core.Rules
{
    /// <summary>
    /// Specific implementation of the <see cref="IRule"/> interface that
    /// detects if a file contains '%' or '+' in the filename.
    /// </summary>
    public sealed class FilenameCharacterRule : IRule<string>
    {
        public RuleResult Evaluate(string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Contains("%") || value.Contains("+"))
            {
                var message = ResourceHelper.GetString(
                    Resources.ResourceManager,
                    "FileNameInvalid",
                    value);
                return new RuleResult(true, message);
            }

            return new RuleResult(false);
        }
    }
}
