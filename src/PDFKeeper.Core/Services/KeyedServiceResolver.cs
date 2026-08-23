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

using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.Interfaces.Services;
using System;

namespace PDFKeeper.Core.Services
{
    /// <summary>
    /// Default implementation of the <see cref="IKeyedServiceResolver"/> interface.
    /// </summary>
    public sealed class KeyedServiceResolver : IKeyedServiceResolver
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedServiceAccessor"/> class.
        /// </summary>
        /// <param name="serviceProvider">
        /// The <see cref="IServiceProvider"/> instance.
        /// </param>
#pragma warning disable IDE0290 // Use primary constructor
        public KeyedServiceResolver(IServiceProvider serviceProvider)
#pragma warning restore IDE0290 // Use primary constructor
        {
            this.serviceProvider = serviceProvider;
        }

        public T GetRequiredKeyedService<T>(object key)
        {
            return serviceProvider.GetRequiredKeyedService<T>(key);
        }
    }
}
