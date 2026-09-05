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
using PDFKeeper.Core.Caching;
using PDFKeeper.Core.Enums;
using PDFKeeper.Core.HelpSystem;
using PDFKeeper.Core.Interfaces.Caching;
using PDFKeeper.Core.Interfaces.HelpSystem;
using PDFKeeper.Core.Interfaces.Navigation;
using PDFKeeper.Core.Interfaces.Rules;
using PDFKeeper.Core.Interfaces.Services;
using PDFKeeper.Core.Interfaces.Services.Pdf;
using PDFKeeper.Core.Interfaces.Services.Pdf.TextExtraction;
using PDFKeeper.Core.Interfaces.Services.Security;
using PDFKeeper.Core.Interfaces.Services.Upload;
using PDFKeeper.Core.Interfaces.Storage;
using PDFKeeper.Core.Navigation;
using PDFKeeper.Core.Rules;
using PDFKeeper.Core.Services;
using PDFKeeper.Core.Services.Pdf;
using PDFKeeper.Core.Services.Pdf.TextExtraction;
using PDFKeeper.Core.Services.Security;
using PDFKeeper.Core.Services.Upload;
using PDFKeeper.Core.Storage;
using PDFKeeper.Core.ViewModels;
using PDFKeeper.WinForms.HelpSystem;
using PDFKeeper.WinForms.Services;
using PDFKeeper.WinForms.Views;
using System;

namespace PDFKeeper.WinForms.Composition
{
    /// <summary>
    /// Builds the dependency injection container used by the application.
    /// </summary>
    internal static class CompositionRoot
    {
        /// <summary>
        /// Builds and returns an <see cref="IServiceProvider"/> containing services
        /// required by the application.
        /// </summary>
        /// <returns>The service provider.</returns>
        internal static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            AddSingletonServices(services);
            AddKeyedSingletonServices(services);
            AddTransientServices(services);
            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Adds singleton services to the collection.
        /// </summary>
        /// <param name="services">The service collection to populate.</param>
        internal static void AddSingletonServices(IServiceCollection services)
        {
            services.AddSingleton<IAliasService, AliasService>();
            services.AddSingleton<IApplicationFolderCleaner, ApplicationFolderCleaner>();
            services.AddSingleton<IApplicationFolderExplorer, ApplicationFolderExplorer>();
            services.AddSingleton<IApplicationFolderManager, ApplicationFolderManager>();
            services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();
            services.AddSingleton<IApplicationPolicyService, ApplicationPolicyService>();
            services.AddSingleton<IApplicationRegistryProvider, ApplicationRegistryProvider>();
            services.AddSingleton<IClipboardService, ClipboardService>();
            services.AddSingleton<IDocumentExportService, DocumentExportService>();
            services.AddSingleton<IExceptionHandler, ExceptionHandler>();
            services.AddSingleton<IFolderBrowserDialogService, FolderBrowserDialogService>();
            services.AddSingleton<IHelpFileResolver, HelpFileResolver>();
            services.AddSingleton<IHelpViewer, HelpViewer>();
            services.AddSingleton<IKeyedServiceResolver, KeyedServiceResolver>();
            services.AddSingleton<IMessageBoxService, MessageBoxService>();
            services.AddSingleton<IPdfAnnotationService, PdfAnnotationService>();
            services.AddSingleton<IPdfAttachmentService, PdfAttachmentService>();
            services.AddSingleton<IPdfFileCache, PdfFileCache>();
            services.AddSingleton<IPdfImageService, PdfImageService>();
            services.AddSingleton<IPdfListingService, PdfListingService>();
            services.AddSingleton<IPdfMetadataService, PdfMetadataService>();
            services.AddSingleton<IPasswordDialogService, PdfOwnerPasswordDialogService>();
            services.AddSingleton<IPdfPreviewService, PdfPreviewService>();
            services.AddSingleton<IPdfSecurityService, PdfSecurityService>();
            services.AddSingleton<IPdfSplitterService, PdfSplitterService>();
            services.AddSingleton<IPdfTextExtractionService, PdfTextExtractionService>();
            services.AddSingleton<IPdfUploadQueryService, PdfUploadQueryService>();
            services.AddSingleton<IPdfUploadService, PdfUploadService>();
            services.AddSingleton<IPdfUploadStagingService, PdfUploadStagingService>();
            services.AddSingleton<IPdfViewerService, PdfViewerService>();
            services.AddSingleton<IPrintDialogService, PrintDialogService>();
            services.AddSingleton<IPrintPreviewDialogService, PrintPreviewDialogService>();
            services.AddSingleton<IProcessService, ProcessService>();
            services.AddSingleton<IRule<string>, FilenameCharacterRule>();
            services.AddSingleton<ISecureDataService, SecureDataService>();
            services.AddSingleton<IUploadFolderMaintenanceService,
                UploadFolderMaintenanceService>();
            services.AddSingleton<IUploadFolderShortcutService, UploadFolderShortcutService>();
            services.AddSingleton<IUploadProfileManager, UploadProfileManager>();
            services.AddSingleton<IVirtualKeyService, VirtualKeyService>();
            services.AddSingleton<PdfTextExtractionStrategy>();
            services.AddSingleton<PdfOcrTextExtractionStrategy>();
        }

        /// <summary>
        /// Adds keyed singleton services to the collection.
        /// </summary>
        /// <param name="services">The service collection to populate.</param>
        internal static void AddKeyedSingletonServices(IServiceCollection services)
        {
            services.AddKeyedSingleton<IDialogService, AboutBoxDialogService>(
                DialogServiceKey.AboutBox);
            services.AddKeyedSingleton<IDialogService, AddPdfDialogService>(
                DialogServiceKey.AddPdf);
            services.AddKeyedSingleton<IDialogService, OptionsDialogService>(
                DialogServiceKey.Options);
            services.AddKeyedSingleton<IDialogService, SetAuthorDialogService>(
                DialogServiceKey.SetAuthor);
            services.AddKeyedSingleton<IDialogService, SetCategoryDialogService>(
                DialogServiceKey.SetCategory);
            services.AddKeyedSingleton<IDialogService, SetDateTimeAddedDialogService>(
                DialogServiceKey.SetDateTimeAdded);
            services.AddKeyedSingleton<IDialogService, SetPreviewPixelDensityDialogService>(
                DialogServiceKey.SetPreviewPixelDensity);
            services.AddKeyedSingleton<IDialogService, SetSubjectDialogService>(
                DialogServiceKey.SetSubject);
            services.AddKeyedSingleton<IDialogService, SetTaxYearDialogService>(
                DialogServiceKey.SetTaxYear);
            services.AddKeyedSingleton<IDialogService, SetTitleDialogService>(
                DialogServiceKey.SetTitle);
            services.AddKeyedSingleton<IDialogService, UploadProfileEditorDialogService>(
                DialogServiceKey.UploadProfileEditor);
            services.AddKeyedSingleton<IFileDialogService, OpenFileDialogService>(
                FileDialogServiceKey.OpenFile);
            services.AddKeyedSingleton<IFileDialogService, SaveFileDialogService>(
                FileDialogServiceKey.SaveFile);
        }

        /// <summary>
        /// Adds transient services to the collection.
        /// </summary>
        /// <param name="services">The service collection to populate.</param>
        internal static void AddTransientServices(IServiceCollection services)
        {
            services.AddTransient<AddPdfForm>();
            services.AddTransient<AddPdfViewModel>();
            services.AddTransient<FindDocumentsForm>();
            services.AddTransient<FindDocumentsViewModel>();
            services.AddTransient<LoginForm>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainForm>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<UploadProfilesForm>();
            services.AddTransient<UploadProfilesViewModel>();
        }
    }
}
