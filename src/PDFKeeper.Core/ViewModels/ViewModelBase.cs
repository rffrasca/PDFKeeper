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

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PDFKeeper.Core.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the OnPropertyChanged event to notify listeners that a property value has
        /// changed.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property used to notify listeners or null to automatically provide the
        /// calling property name.
        /// </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Checks if a property already matches a desired value.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="member">The reference to a property with both getter and setter.</param>
        /// <param name="value">The desired value for the property.</param>
        /// <param name="propertyName">
        /// The name of the property used to notify listeners or null to automatically provide the
        /// calling property name.
        /// </param>
        protected void SetProperty<T>(
#pragma warning disable CA1045 // Do not pass types by reference
            ref T member,
#pragma warning restore CA1045 // Do not pass types by reference
            T value,
            [CallerMemberName] string propertyName = null)
        {
            member = value;
            OnPropertyChanged(propertyName);
        }
    }
}
