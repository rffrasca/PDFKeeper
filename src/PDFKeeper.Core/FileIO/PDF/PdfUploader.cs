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
using PDFKeeper.Core.Commands;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Extensions;
using PDFKeeper.Core.Helpers;
using PDFKeeper.Core.Models;
using PDFKeeper.Core.Rules;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;

namespace PDFKeeper.Core.FileIO.PDF
{
    internal class PdfUploader
    {
        private readonly ApplicationDirectory applicationDirectory;
        private readonly UploadProfileManager uploadProfileManager;
        private readonly DirectoryInfo uploadDirectory;
        private readonly DirectoryInfo uploadRejectedDirectory;
        private readonly DirectoryInfo uploadStagingDirectory;

        internal PdfUploader()
        {
            applicationDirectory = new ApplicationDirectory();
            uploadProfileManager = new UploadProfileManager();
            uploadDirectory = applicationDirectory.GetDirectory(
                ApplicationDirectory.SpecialName.Upload);
            uploadRejectedDirectory = applicationDirectory.GetDirectory(
                ApplicationDirectory.SpecialName.UploadRejected);
            uploadStagingDirectory = applicationDirectory.GetDirectory(
                ApplicationDirectory.SpecialName.UploadStaging);
        }

        /// <summary>
        /// Gets <c>true</c> or <c>false</c> if PDF files are ready to be uploaded.
        /// </summary>
        internal bool PdfFilesReadyToUpload
        {
            get
            {
                if (GetPdfFiles(uploadDirectory).Count > 0 ||
                    GetPdfFiles(uploadStagingDirectory).Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets <c>true</c> or <c>false</c> if the
        /// <see cref="ApplicationDirectory.SpecialName.UploadRejected"/> directory contain PDF
        /// files that were rejected by the Uploader.
        /// </summary>
        internal bool UploadRejectedContainsPdfFiles
        {
            get
            {
                if (uploadRejectedDirectory.GetFiles(
                    "*.pdf",
                    SearchOption.AllDirectories).Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Executes <see cref="ApplicationDirectory.SpecialName.Upload"/> directory maintenance
        /// tasks:
        /// <list type="table">
        /// 1. Creates missing Upload Profile directories.
        /// </list>
        /// <list type="table">
        /// 2. Deletes dormant Upload directories that are either empty and not an Upload Profile
        /// directory, or any empty sub-directory under each Upload Profile directory.
        /// </list>
        /// </summary>
        internal void ExecuteUploadDirectoryMaintenance()
        {
            CreateMissingUploadProfileDirectories();
            DeleteDormantUploadDirectories();
            DeleteEmptyUploadRejectedDirectories();
        }

        /// <summary>
        /// Executes the upload process by staging PDF files and uploading them to the repository.
        /// </summary>
        /// <remarks>
        /// This method performs two main operations: staging PDF files in the upload directory and
        /// uploading the staged files. Ensure that the file cache is properly initialized before
        /// calling this method.
        /// </remarks>
        /// <param name="fileCache">
        /// The file cache used to manage cached files associated with documents.
        /// </param>
        internal void ExecuteUpload(FileCache fileCache)
        {
            StagePdfFilesInUploadDirectory();
            UploadStagedPdfFiles(fileCache);
        }

        private static Collection<FileInfo> GetPdfFiles(DirectoryInfo directory)
        {
            var pdfFiles = new Collection<FileInfo>();

            foreach (var pdfFile in directory.GetPdfFilesOrderByLastWriteTime())
            {
                pdfFiles.Add(pdfFile);
            }

            return pdfFiles;
        }

        private void CreateMissingUploadProfileDirectories()
        {
            foreach (var name in uploadProfileManager.GetUploadProfileNames())
            {
                uploadDirectory.CreateSubdirectory(name);
            }
        }

        private void DeleteDormantUploadDirectories()
        {
            foreach (var directory in uploadDirectory.GetDirectories())
            {
                if (uploadProfileManager.GetUploadProfile(directory.Name) != null)
                {
                    foreach (var directoryL2 in directory.GetDirectories())
                    {
                        if (directoryL2.GetFiles(
                            "*",
                            SearchOption.AllDirectories).Length.Equals(0))
                        {
                            directoryL2.Delete(true);
                        }
                    }
                }
                else
                {
                    if (directory.GetFiles("*", SearchOption.AllDirectories).Length.Equals(0))
                    {
                        directory.Delete(true);
                    }
                }
            }
        }

        private void DeleteEmptyUploadRejectedDirectories()
        {
            foreach (var directory in uploadRejectedDirectory.GetDirectories())
            {
                if (directory.GetFiles("*", SearchOption.AllDirectories).Length.Equals(0))
                {
                    directory.Delete(true);
                }
            }
        }

        private void StagePdfFilesInUploadDirectory()
        {
            foreach (var pdfFile in GetPdfFiles(uploadDirectory))
            {
                pdfFile.WaitWhileLocked();
                var pdf = new PdfFile(pdfFile);
                
                if (DatabaseSession.InsertGranted)
                {
                    if (pdf.GetPasswordType() == PdfFile.PasswordType.None)
                    {
                        var xmlFile = new FileInfo(pdfFile.ChangeExtension("xml").FullName);
                        var dirName = pdfFile.FullName.Substring(uploadDirectory.FullName.Length + 1);
                        
                        if (dirName.Equals(pdfFile.Name, StringComparison.Ordinal))
                        {
                            dirName = uploadDirectory.FullName;
                        }
                        else
                        {
                            dirName = dirName.Substring(0,
                                dirName.IndexOf(Path.DirectorySeparatorChar));
                        }
                        
                        var pdfMetadata = new PdfMetadata(pdf);
                        
                        if (uploadProfileManager.GetUploadProfile(dirName) != null)
                        {
                            var uploadProfile = uploadProfileManager.GetUploadProfile(dirName);

                            if (!uploadProfile.Title.Equals(
                                UploadProfileToken.TitleToken,
                                StringComparison.Ordinal))
                            {
                                if (uploadProfile.Title.Equals(
                                UploadProfileToken.DateToken,
                                StringComparison.Ordinal))
                                {
                                    pdfMetadata.Title = UploadProfileTokenHelper.GetDate();
                                }
                                else if (uploadProfile.Title.Equals(
                                    UploadProfileToken.DateTimeToken,
                                    StringComparison.Ordinal))
                                {
                                    pdfMetadata.Title = UploadProfileTokenHelper.GetDateTime();
                                }
                                else if (uploadProfile.Title.Equals(
                                    UploadProfileToken.FileNameToken,
                                    StringComparison.Ordinal))
                                {
                                    pdfMetadata.Title = UploadProfileTokenHelper.GetFileName(pdfFile);
                                }
                                else
                                {
                                    pdfMetadata.Title = uploadProfile.Title;
                                }
                            }
                            else
                            {
                                pdfMetadata.Title ??= string.Empty;
                            }

                            if (!uploadProfile.Author.Equals(
                                UploadProfileToken.AuthorToken,
                                StringComparison.Ordinal))
                            {
                                pdfMetadata.Author = uploadProfile.Author;
                            }
                            else
                            {
                                pdfMetadata.Author ??= string.Empty;
                            }

                            if (!uploadProfile.Subject.Equals(
                                UploadProfileToken.SubjectToken,
                                StringComparison.Ordinal))
                            {
                                pdfMetadata.Subject = uploadProfile.Subject;
                            }
                            else
                            {
                                pdfMetadata.Subject ??= string.Empty;
                            }

                            if (!uploadProfile.Keywords.Equals(
                                UploadProfileToken.KeywordsToken,
                                StringComparison.Ordinal))
                            {
                                pdfMetadata.Keywords = uploadProfile.Keywords;
                            }
                            else
                            {
                                pdfMetadata.Keywords ??= string.Empty;
                            }

                            pdfMetadata.Category = uploadProfile.Category;
                            pdfMetadata.Flag = Convert.ToInt32(uploadProfile.FlagDocument);
                            pdfMetadata.TaxYear = uploadProfile.TaxYear;
                            pdfMetadata.OcrPdfTextAndImageDataPages =
                                uploadProfile.OcrPdfTextAndImageDataPages;
                            
                            try
                            {
                                new UploadStagingCommand(pdfMetadata.Write()).Execute(null);
                                pdfFile.DeleteToRecycleBin();
                                
                                if (xmlFile.Exists)
                                {
                                    xmlFile.DeleteToRecycleBin();
                                }
                            }
                            catch (Exception ex) when (
                                ex is iText.IO.Exceptions.IOException ||
                                ex is NullReferenceException)
                            {
                                MoveFileToUploadRejected(pdfFile);
                            }
                        }
                        else
                        {
                            if (new PdfMetadataRule(pdfMetadata.ExportUploadProfile()).ViolationFound)
                            {
                                MoveFileToUploadRejected(pdfFile);
                            }
                            else
                            {
                                new UploadStagingCommand(pdfFile).Execute(null);
                            }
                        }
                    }
                    else
                    {
                        MoveFileToUploadRejected(pdfFile);
                    }
                }
                else
                {
                    MoveFileToUploadRejected(pdfFile);
                }
            }
        }

        /// <summary>
        /// Processes and uploads staged PDF files from the staging directory to the document
        /// repository.
        /// </summary>
        /// <remarks>
        /// This method retrieves PDF files from the staging directory, extracts metadata and
        /// content from each file, and creates or updates corresponding document records in the
        /// repository. If a document already exists, its cached files are deleted before updating
        /// the record. After processing, the original PDF files and their associated XML metadata
        /// files (if present) are deleted from the staging directory.
        /// </remarks>
        /// <param name="fileCache">
        /// The file cache used to manage and delete cached files associated with documents.
        /// </param>
        private void UploadStagedPdfFiles(FileCache fileCache)
        {
            foreach (var pdfFile in GetPdfFiles(uploadStagingDirectory))
            {
                var pdf = new PdfFile(pdfFile);
                var document = new Document();
                var pdfMetadata = new PdfMetadata(pdf);
                document.Id = pdfMetadata.Id;
                document.Title = pdfMetadata.Title;
                document.Author = pdfMetadata.Author;
                document.Subject = pdfMetadata.Subject;
                document.Keywords = pdfMetadata.Keywords;
                
                if (document.Id.Equals(0))
                {
                    document.Added = DateTime.Now.ToString(
                        "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.CurrentCulture);
                    document.Notes = string.Empty;
                }

                document.Pdf = pdfFile.ReadAllBytes();
                document.Category = pdfMetadata.Category;
                document.Flag = pdfMetadata.Flag;
                document.TaxYear = pdfMetadata.TaxYear;
                document.TextAnnotations = pdf.GetTextAnnot();
                document.Text = pdf.GetText(pdfMetadata.OcrPdfTextAndImageDataPages);

                using (var documentRepository = DatabaseSession.GetDocumentRepository())
                {
                    if (document.Id.Equals(0))
                    {
                        documentRepository.InsertDocument(document);
                    }
                    else
                    {
                        fileCache.Delete(document.Id);
                        documentRepository.UpdateDocument(document, true);
                    }
                }
                
                pdfFile.Delete();
                var xmlFile = pdfFile.ChangeExtension("xml");
                
                if (xmlFile.Exists)
                {
                    xmlFile.Delete();
                }
            }
        }

        private void MoveFileToUploadRejected(FileInfo file)
        {
            var destFile = new FileInfo(
                file.FullName.Replace(
                    uploadDirectory.FullName,
                    uploadRejectedDirectory.FullName));
            destFile.Directory.Create();
            file.MoveTo(destFile.FullName);
        }
    }
}
