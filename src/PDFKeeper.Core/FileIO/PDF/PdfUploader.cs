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

using PDFKeeper.Core.Application;
using PDFKeeper.Core.Commands;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Extensions;
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
        /// Are PDF files ready to be uploaded? (true or false)
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
        /// Does the UploadRejected directory contain PDF files that were rejected by the Uploader?
        /// (true or false)
        /// </summary>
        internal bool UploadRejectedContainsPdfFiles
        {
            get
            {
                if (uploadRejectedDirectory.GetFiles("*.pdf",
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
        /// <para>Executes upload directory maintenance tasks:</para>
        /// <list type="number">Creates missing upload profile directories.</list>
        /// <list type="number">
        /// Deletes dormant upload directories that are either empty and not a profile directory,
        /// or any empty sub-directory under each upload profile directory.
        /// </list>
        /// </summary>
        internal void ExecuteUploadDirectoryMaintenance()
        {
            CreateMissingUploadProfileDirectories();
            DeleteDormantUploadDirectories();
            DeleteEmptyUploadRejectedDirectories();
        }

        /// <summary>
        /// <para>Executes PDF upload tasks:</para>
        /// <list type="number">Stages all PDF files in Upload directories for uploading.</list>
        /// <list type="number">Uploads all PDF files in UploadStaging to the database.</list>
        /// </summary>
        internal void ExecuteUpload()
        {
            StagePdfFilesInUploadDirectory();
            UploadStagedPdfFiles();
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
                        if (directoryL2.GetFiles().Length.Equals(0))
                        {
                            directoryL2.Delete(true);
                        }
                    }
                }
                else
                {
                    if (directory.GetFiles().Length.Equals(0))
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
                if (directory.GetFiles().Length.Equals(0))
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
                    var pdfMetadata = new PdfMetadata(pdf, null);
                    if (uploadProfileManager.GetUploadProfile(dirName) != null)
                    {
                        var uploadProfile = uploadProfileManager.GetUploadProfile(dirName);
                        if (uploadProfile.Title.Equals(TitleToken.DateToken,
                            StringComparison.Ordinal))
                        {
                            pdfMetadata.Title = TitleToken.GetDate();
                        }
                        else if (uploadProfile.Title.Equals(TitleToken.DateTimeToken,
                            StringComparison.Ordinal))
                        {
                            pdfMetadata.Title = TitleToken.GetDateTime();
                        }
                        else if (uploadProfile.Title.Equals(TitleToken.FileNameToken,
                            StringComparison.Ordinal))
                        {
                            pdfMetadata.Title = TitleToken.GetFileName(pdfFile);
                        }
                        else
                        {
                            pdfMetadata.Title = uploadProfile.Title;
                        }
                        pdfMetadata.Author = uploadProfile.Author;
                        pdfMetadata.Subject = uploadProfile.Subject;
                        pdfMetadata.Keywords = uploadProfile.Keywords;
                        pdfMetadata.Category = uploadProfile.Category;
                        pdfMetadata.Flag = Convert.ToInt32(uploadProfile.FlagDocument);
                        pdfMetadata.TaxYear = uploadProfile.TaxYear;
                        pdfMetadata.OcrPdfTextAndImageDataPages =
                            uploadProfile.OcrPdfTextAndImageDataPages;
                        try
                        {
                            new UploadStagingCommand(pdfMetadata.Write()).Execute();
                            pdfFile.DeleteToRecycleBin();
                            if (xmlFile.Exists)
                            {
                                xmlFile.DeleteToRecycleBin();
                            }    
                        }
                        catch (iText.IO.Exceptions.IOException)
                        {
                            MoveFileToUploadRejected(pdfFile);
                            throw;
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
                            new UploadStagingCommand(pdfFile).Execute();
                        }
                    }
                }
                else
                {
                    MoveFileToUploadRejected(pdfFile);
                }
            }
        }

        private void UploadStagedPdfFiles()
        {
            foreach (var pdfFile in GetPdfFiles(uploadStagingDirectory))
            {
                var pdf = new PdfFile(pdfFile);
                var document = new Document();
                var pdfMetadata = new PdfMetadata(pdf, null);
                document.Title = pdfMetadata.Title;
                document.Author = pdfMetadata.Author;
                document.Subject = pdfMetadata.Subject;
                document.Keywords = pdfMetadata.Keywords;
                document.Added = DateTime.Now.ToString(
                    "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.CurrentCulture);
                document.Notes = string.Empty;
                document.Pdf = pdfFile.ReadAllBytes();
                document.Category = pdfMetadata.Category;
                document.Flag = pdfMetadata.Flag;
                document.TaxYear = pdfMetadata.TaxYear;
                document.TextAnnotations = pdf.GetTextAnnot();
                document.Text = pdf.GetText(pdfMetadata.OcrPdfTextAndImageDataPages);
                using (var documentRepository = DatabaseSession.GetDocumentRepository())
                {
                    documentRepository.InsertDocument(document);
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
            var destFile = new FileInfo(file.FullName.Replace(uploadDirectory.FullName,
                uploadRejectedDirectory.FullName));
            destFile.Directory.Create();
            file.MoveTo(destFile.FullName);
        }
    }
}
