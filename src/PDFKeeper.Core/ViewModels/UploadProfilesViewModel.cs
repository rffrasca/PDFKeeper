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

using CommunityToolkit.Mvvm.Input;
using PDFKeeper.Core.Enums;
using PDFKeeper.Core.FileIO;
using PDFKeeper.Core.Helpers;
using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.Core.Properties;
using PDFKeeper.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PDFKeeper.Core.ViewModels
{
    /// <summary>
    /// View model for managing upload profiles, providing functionality to add, edit, and delete.
    /// </summary>
    [CLSCompliant(false)]
    public sealed class UploadProfilesViewModel : ViewModelBase
    {
        private readonly IMessageBoxService messageBoxService;
        private readonly IDialogService uploadProfileEditorDialogService;
        private readonly UploadProfileManager uploadProfileManager;
        private IEnumerable<string> uploadProfileNames;
        private bool editEnabled;
        private bool deleteEnabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadProfilesViewModel"/> class.
        /// </summary>
        /// <param name="keyedServiceResolver">
        /// The <see cref="IKeyedServiceResolver"/> instance.
        /// </param>
        /// <param name="messageBoxService">
        /// The <see cref="IMessageBoxService"/> instance.
        /// </param>
        public UploadProfilesViewModel(
            IKeyedServiceResolver keyedServiceResolver,
            IMessageBoxService messageBoxService)
        {
            if (keyedServiceResolver is null)
            {
                throw new ArgumentNullException(nameof(keyedServiceResolver));
            }

            this.messageBoxService = messageBoxService;
            uploadProfileEditorDialogService = keyedServiceResolver.GetRequiredKeyedService<
                IDialogService>(DialogServiceKey.UploadProfileEditor);
            uploadProfileManager = new UploadProfileManager();
            UploadProfilesDirectoryPath = uploadProfileManager.UploadProfilesDirectoryPath;
            InitializeCommands();
            GetUploadProfileNames();
        }

        public IRelayCommand GetUploadProfileNamesCommand { get; private set; }
        public IRelayCommand AddUploadProfileCommand { get; private set; }
        public IRelayCommand EditUploadProfileCommand { get; private set; }
        public IRelayCommand DeleteUploadProfileCommand { get; private set; }
        public string UploadProfilesDirectoryPath { get; set; }

        public IEnumerable<string> UploadProfileNames
        {
            get => uploadProfileNames;
            set => SetProperty(ref uploadProfileNames, value);
        }

        public string CurrentUploadProfileName { get; set; }

        public bool EditEnabled
        {
            get => editEnabled;
            set => SetProperty(ref editEnabled, value);
        }

        public bool DeleteEnabled
        {
            get => deleteEnabled;
            set => SetProperty(ref deleteEnabled, value);
        }

        private void InitializeCommands()
        {
            GetUploadProfileNamesCommand = new RelayCommand(GetUploadProfileNames);
            AddUploadProfileCommand = new RelayCommand(AddUploadProfile);
            EditUploadProfileCommand = new RelayCommand(EditUploadProfile);
            DeleteUploadProfileCommand = new RelayCommand(DeleteUploadProfile);
        }

        private void GetUploadProfileNames()
        {
            UploadProfileNames = [.. uploadProfileManager.GetUploadProfileNames()];
            if (UploadProfileNames.Any())
            {
                EditEnabled = true;
                DeleteEnabled = true;
            }
            else
            {
                EditEnabled = false;
                DeleteEnabled = false;
            }
        }

        private void AddUploadProfile() => 
            uploadProfileEditorDialogService.ShowDialog(GetWindowHandle.Invoke());

        private void EditUploadProfile() => uploadProfileEditorDialogService.ShowDialog(
            GetWindowHandle.Invoke(),
            CurrentUploadProfileName);

        private void DeleteUploadProfile()
        {
            var message = ResourceHelper.GetString(
                Resources.ResourceManager,
                "DeleteToRecycleBin",
                CurrentUploadProfileName);

            if (messageBoxService.ShowQuestion(GetWindowHandle.Invoke(), message) == 6)
            {
                uploadProfileManager.DeleteUploadProfile(CurrentUploadProfileName);
            }
        }
    }
}
