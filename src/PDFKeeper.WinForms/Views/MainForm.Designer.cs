// *****************************************************************************
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
// *****************************************************************************

namespace PDFKeeper.WinForms.Views
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                findDocumentsForm.Dispose();
                uploadProfilesForm.Dispose();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.FileAddToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FileOpenToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FileSaveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FileBurstToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FileExtractAllAttachmentsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FileExtractAllEmbeddedFilesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FileCopyPdfToClipboardToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.FilePrintToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolBarToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.EditUndoToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.EditCutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.EditCopyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.EditPasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.EditRestoreToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.EditAppendDateTimeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.EditAppendTextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolBarToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.DocumentsFindToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.DocumentsDeleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolBarToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolsOptionsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolsUploadProfilesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolBarToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.HelpContentsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FileSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.FileBurstToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExtractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExtractAllAttachmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExtractAllEmbeddedFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.FileCopyPdfToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.FilePrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilePrintPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileToolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileToolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditUndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.EditCutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditPasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.EditSelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.EditRestoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.EditAppendDateTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditAppendTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.EditFlagDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocumentsFindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocumentsToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.DocumentsSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocumentsSelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocumentsSelectNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocumentsToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.DocumentsSetTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocumentsSetAuthorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocumentsSetSubjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocumentsSetCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocumentsSetTaxYearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocumentsSetDateTimeAddedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocumentsToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.DocumentsDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSetPreviewPixelDensityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.ViewToolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewStatusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsUploadProfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsMoveDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpContentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.HelpAboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.DocumentsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.DocumentsCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusStripFillerLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.DocumentsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.UploadProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.RefreshingDocumentsImageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FlagImageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.UploadRejectedImageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.MainViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DocumentsDataGridView = new System.Windows.Forms.DataGridView();
            this.DocumentDataTabControl = new System.Windows.Forms.TabControl();
            this.NotesTabPage = new System.Windows.Forms.TabPage();
            this.NotesTextBox = new System.Windows.Forms.TextBox();
            this.KeywordsTabPage = new System.Windows.Forms.TabPage();
            this.KeywordsTextBox = new System.Windows.Forms.TextBox();
            this.TextTabPage = new System.Windows.Forms.TabPage();
            this.TextTextBox = new System.Windows.Forms.TextBox();
            this.SearchTermSnippetsTabPage = new System.Windows.Forms.TabPage();
            this.SearchTermSnippetsTextBox = new System.Windows.Forms.TextBox();
            this.HorizontalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.SearchResultsPanel = new System.Windows.Forms.Panel();
            this.VerticalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.PreviewTabControl = new System.Windows.Forms.TabControl();
            this.PreviewTabPage = new System.Windows.Forms.TabPage();
            this.PreviewPanel = new System.Windows.Forms.Panel();
            this.DocumentsListTimedRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.CheckForDocumentsListChangesTimer = new System.Windows.Forms.Timer(this.components);
            this.HelpProvider = new System.Windows.Forms.HelpProvider();
            this.UploadTimer = new System.Windows.Forms.Timer(this.components);
            this.UpdateCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.CheckForFlaggedDocumentsTimer = new System.Windows.Forms.Timer(this.components);
            this.SelectionColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FlagColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.ToolStrip.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentsDataGridView)).BeginInit();
            this.DocumentDataTabControl.SuspendLayout();
            this.NotesTabPage.SuspendLayout();
            this.KeywordsTabPage.SuspendLayout();
            this.TextTabPage.SuspendLayout();
            this.SearchTermSnippetsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HorizontalSplitContainer)).BeginInit();
            this.HorizontalSplitContainer.Panel1.SuspendLayout();
            this.HorizontalSplitContainer.Panel2.SuspendLayout();
            this.HorizontalSplitContainer.SuspendLayout();
            this.SearchResultsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VerticalSplitContainer)).BeginInit();
            this.VerticalSplitContainer.Panel1.SuspendLayout();
            this.VerticalSplitContainer.Panel2.SuspendLayout();
            this.VerticalSplitContainer.SuspendLayout();
            this.PreviewTabControl.SuspendLayout();
            this.PreviewTabPage.SuspendLayout();
            this.PreviewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolStrip
            // 
            this.ToolStrip.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::PDFKeeper.WinForms.Properties.Settings.Default, "ToolBarVisible", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileAddToolStripButton,
            this.FileOpenToolStripButton,
            this.FileSaveToolStripButton,
            this.FileBurstToolStripButton,
            this.FileExtractAllAttachmentsToolStripButton,
            this.FileExtractAllEmbeddedFilesToolStripButton,
            this.FileCopyPdfToClipboardToolStripButton,
            this.FilePrintToolStripButton,
            this.ToolBarToolStripSeparator1,
            this.EditUndoToolStripButton,
            this.EditCutToolStripButton,
            this.EditCopyToolStripButton,
            this.EditPasteToolStripButton,
            this.EditRestoreToolStripButton,
            this.EditAppendDateTimeToolStripButton,
            this.EditAppendTextToolStripButton,
            this.ToolBarToolStripSeparator2,
            this.DocumentsFindToolStripButton,
            this.DocumentsDeleteToolStripButton,
            this.ToolBarToolStripSeparator3,
            this.ToolsOptionsToolStripButton,
            this.ToolsUploadProfilesToolStripButton,
            this.ToolBarToolStripSeparator4,
            this.HelpContentsToolStripButton});
            resources.ApplyResources(this.ToolStrip, "ToolStrip");
            this.ToolStrip.Name = "ToolStrip";
            this.HelpProvider.SetShowHelp(this.ToolStrip, ((bool)(resources.GetObject("ToolStrip.ShowHelp"))));
            this.ToolStrip.Visible = global::PDFKeeper.WinForms.Properties.Settings.Default.ToolBarVisible;
            // 
            // FileAddToolStripButton
            // 
            this.FileAddToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FileAddToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.database_add;
            resources.ApplyResources(this.FileAddToolStripButton, "FileAddToolStripButton");
            this.FileAddToolStripButton.Name = "FileAddToolStripButton";
            this.FileAddToolStripButton.Tag = "";
            this.FileAddToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileOpenToolStripButton
            // 
            this.FileOpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FileOpenToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.file_acrobat;
            resources.ApplyResources(this.FileOpenToolStripButton, "FileOpenToolStripButton");
            this.FileOpenToolStripButton.Name = "FileOpenToolStripButton";
            this.FileOpenToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileSaveToolStripButton
            // 
            this.FileSaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FileSaveToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.database_save;
            resources.ApplyResources(this.FileSaveToolStripButton, "FileSaveToolStripButton");
            this.FileSaveToolStripButton.Name = "FileSaveToolStripButton";
            this.FileSaveToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileBurstToolStripButton
            // 
            this.FileBurstToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FileBurstToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.cut_red;
            resources.ApplyResources(this.FileBurstToolStripButton, "FileBurstToolStripButton");
            this.FileBurstToolStripButton.Name = "FileBurstToolStripButton";
            this.FileBurstToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileExtractAllAttachmentsToolStripButton
            // 
            this.FileExtractAllAttachmentsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FileExtractAllAttachmentsToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.icon_attachment;
            resources.ApplyResources(this.FileExtractAllAttachmentsToolStripButton, "FileExtractAllAttachmentsToolStripButton");
            this.FileExtractAllAttachmentsToolStripButton.Name = "FileExtractAllAttachmentsToolStripButton";
            this.FileExtractAllAttachmentsToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileExtractAllEmbeddedFilesToolStripButton
            // 
            this.FileExtractAllEmbeddedFilesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FileExtractAllEmbeddedFilesToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.page_attach;
            resources.ApplyResources(this.FileExtractAllEmbeddedFilesToolStripButton, "FileExtractAllEmbeddedFilesToolStripButton");
            this.FileExtractAllEmbeddedFilesToolStripButton.Name = "FileExtractAllEmbeddedFilesToolStripButton";
            this.FileExtractAllEmbeddedFilesToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileCopyPdfToClipboardToolStripButton
            // 
            this.FileCopyPdfToClipboardToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FileCopyPdfToClipboardToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.page_white_copy;
            resources.ApplyResources(this.FileCopyPdfToClipboardToolStripButton, "FileCopyPdfToClipboardToolStripButton");
            this.FileCopyPdfToClipboardToolStripButton.Name = "FileCopyPdfToClipboardToolStripButton";
            this.FileCopyPdfToClipboardToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FilePrintToolStripButton
            // 
            this.FilePrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FilePrintToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.action_print;
            resources.ApplyResources(this.FilePrintToolStripButton, "FilePrintToolStripButton");
            this.FilePrintToolStripButton.Name = "FilePrintToolStripButton";
            this.FilePrintToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // ToolBarToolStripSeparator1
            // 
            this.ToolBarToolStripSeparator1.Name = "ToolBarToolStripSeparator1";
            resources.ApplyResources(this.ToolBarToolStripSeparator1, "ToolBarToolStripSeparator1");
            // 
            // EditUndoToolStripButton
            // 
            this.EditUndoToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditUndoToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.arrow_undo;
            resources.ApplyResources(this.EditUndoToolStripButton, "EditUndoToolStripButton");
            this.EditUndoToolStripButton.Name = "EditUndoToolStripButton";
            this.EditUndoToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditCutToolStripButton
            // 
            this.EditCutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditCutToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.cut;
            resources.ApplyResources(this.EditCutToolStripButton, "EditCutToolStripButton");
            this.EditCutToolStripButton.Name = "EditCutToolStripButton";
            this.EditCutToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditCopyToolStripButton
            // 
            this.EditCopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditCopyToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.copy;
            resources.ApplyResources(this.EditCopyToolStripButton, "EditCopyToolStripButton");
            this.EditCopyToolStripButton.Name = "EditCopyToolStripButton";
            this.EditCopyToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditPasteToolStripButton
            // 
            this.EditPasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditPasteToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.action_paste;
            resources.ApplyResources(this.EditPasteToolStripButton, "EditPasteToolStripButton");
            this.EditPasteToolStripButton.Name = "EditPasteToolStripButton";
            this.EditPasteToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditRestoreToolStripButton
            // 
            this.EditRestoreToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditRestoreToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.arrow_rotate_anticlockwise;
            resources.ApplyResources(this.EditRestoreToolStripButton, "EditRestoreToolStripButton");
            this.EditRestoreToolStripButton.Name = "EditRestoreToolStripButton";
            this.EditRestoreToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditAppendDateTimeToolStripButton
            // 
            this.EditAppendDateTimeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditAppendDateTimeToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.date_new;
            resources.ApplyResources(this.EditAppendDateTimeToolStripButton, "EditAppendDateTimeToolStripButton");
            this.EditAppendDateTimeToolStripButton.Name = "EditAppendDateTimeToolStripButton";
            this.EditAppendDateTimeToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditAppendTextToolStripButton
            // 
            this.EditAppendTextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditAppendTextToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.page_text;
            resources.ApplyResources(this.EditAppendTextToolStripButton, "EditAppendTextToolStripButton");
            this.EditAppendTextToolStripButton.Name = "EditAppendTextToolStripButton";
            this.EditAppendTextToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // ToolBarToolStripSeparator2
            // 
            this.ToolBarToolStripSeparator2.Name = "ToolBarToolStripSeparator2";
            resources.ApplyResources(this.ToolBarToolStripSeparator2, "ToolBarToolStripSeparator2");
            // 
            // DocumentsFindToolStripButton
            // 
            this.DocumentsFindToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DocumentsFindToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.page_find;
            resources.ApplyResources(this.DocumentsFindToolStripButton, "DocumentsFindToolStripButton");
            this.DocumentsFindToolStripButton.Name = "DocumentsFindToolStripButton";
            this.DocumentsFindToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // DocumentsDeleteToolStripButton
            // 
            this.DocumentsDeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DocumentsDeleteToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.database_delete;
            resources.ApplyResources(this.DocumentsDeleteToolStripButton, "DocumentsDeleteToolStripButton");
            this.DocumentsDeleteToolStripButton.Name = "DocumentsDeleteToolStripButton";
            this.DocumentsDeleteToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // ToolBarToolStripSeparator3
            // 
            this.ToolBarToolStripSeparator3.Name = "ToolBarToolStripSeparator3";
            resources.ApplyResources(this.ToolBarToolStripSeparator3, "ToolBarToolStripSeparator3");
            // 
            // ToolsOptionsToolStripButton
            // 
            this.ToolsOptionsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolsOptionsToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.icon_settings;
            resources.ApplyResources(this.ToolsOptionsToolStripButton, "ToolsOptionsToolStripButton");
            this.ToolsOptionsToolStripButton.Name = "ToolsOptionsToolStripButton";
            this.ToolsOptionsToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // ToolsUploadProfilesToolStripButton
            // 
            this.ToolsUploadProfilesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolsUploadProfilesToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.folder;
            resources.ApplyResources(this.ToolsUploadProfilesToolStripButton, "ToolsUploadProfilesToolStripButton");
            this.ToolsUploadProfilesToolStripButton.Name = "ToolsUploadProfilesToolStripButton";
            this.ToolsUploadProfilesToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // ToolBarToolStripSeparator4
            // 
            this.ToolBarToolStripSeparator4.Name = "ToolBarToolStripSeparator4";
            resources.ApplyResources(this.ToolBarToolStripSeparator4, "ToolBarToolStripSeparator4");
            // 
            // HelpContentsToolStripButton
            // 
            this.HelpContentsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HelpContentsToolStripButton.Image = global::PDFKeeper.WinForms.Properties.Resources.icon_info;
            resources.ApplyResources(this.HelpContentsToolStripButton, "HelpContentsToolStripButton");
            this.HelpContentsToolStripButton.Name = "HelpContentsToolStripButton";
            this.HelpContentsToolStripButton.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.DocumentsToolStripMenuItem,
            this.ViewToolStripMenuItem,
            this.ToolsToolStripMenuItem,
            this.HelpToolStripMenuItem});
            resources.ApplyResources(this.MenuStrip, "MenuStrip");
            this.MenuStrip.Name = "MenuStrip";
            this.HelpProvider.SetShowHelp(this.MenuStrip, ((bool)(resources.GetObject("MenuStrip.ShowHelp"))));
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileAddToolStripMenuItem,
            this.FileToolStripSeparator1,
            this.FileOpenToolStripMenuItem,
            this.FileToolStripSeparator2,
            this.FileSaveToolStripMenuItem,
            this.FileSaveAsToolStripMenuItem,
            this.FileToolStripSeparator3,
            this.FileBurstToolStripMenuItem,
            this.FileExtractToolStripMenuItem,
            this.FileToolStripSeparator4,
            this.FileCopyPdfToClipboardToolStripMenuItem,
            this.FileToolStripSeparator5,
            this.FilePrintToolStripMenuItem,
            this.FilePrintPreviewToolStripMenuItem,
            this.FileToolStripSeparator6,
            this.FileExportToolStripMenuItem,
            this.FileToolStripSeparator7,
            this.FileExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            resources.ApplyResources(this.FileToolStripMenuItem, "FileToolStripMenuItem");
            // 
            // FileAddToolStripMenuItem
            // 
            this.FileAddToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.database_add;
            this.FileAddToolStripMenuItem.Name = "FileAddToolStripMenuItem";
            resources.ApplyResources(this.FileAddToolStripMenuItem, "FileAddToolStripMenuItem");
            this.FileAddToolStripMenuItem.Tag = "";
            this.FileAddToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileToolStripSeparator1
            // 
            this.FileToolStripSeparator1.Name = "FileToolStripSeparator1";
            resources.ApplyResources(this.FileToolStripSeparator1, "FileToolStripSeparator1");
            // 
            // FileOpenToolStripMenuItem
            // 
            this.FileOpenToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.file_acrobat;
            this.FileOpenToolStripMenuItem.Name = "FileOpenToolStripMenuItem";
            resources.ApplyResources(this.FileOpenToolStripMenuItem, "FileOpenToolStripMenuItem");
            this.FileOpenToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileToolStripSeparator2
            // 
            this.FileToolStripSeparator2.Name = "FileToolStripSeparator2";
            resources.ApplyResources(this.FileToolStripSeparator2, "FileToolStripSeparator2");
            // 
            // FileSaveToolStripMenuItem
            // 
            this.FileSaveToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.database_save;
            this.FileSaveToolStripMenuItem.Name = "FileSaveToolStripMenuItem";
            resources.ApplyResources(this.FileSaveToolStripMenuItem, "FileSaveToolStripMenuItem");
            this.FileSaveToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileSaveAsToolStripMenuItem
            // 
            this.FileSaveAsToolStripMenuItem.Name = "FileSaveAsToolStripMenuItem";
            resources.ApplyResources(this.FileSaveAsToolStripMenuItem, "FileSaveAsToolStripMenuItem");
            this.FileSaveAsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileToolStripSeparator3
            // 
            this.FileToolStripSeparator3.Name = "FileToolStripSeparator3";
            resources.ApplyResources(this.FileToolStripSeparator3, "FileToolStripSeparator3");
            // 
            // FileBurstToolStripMenuItem
            // 
            this.FileBurstToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.cut_red;
            this.FileBurstToolStripMenuItem.Name = "FileBurstToolStripMenuItem";
            resources.ApplyResources(this.FileBurstToolStripMenuItem, "FileBurstToolStripMenuItem");
            this.FileBurstToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileExtractToolStripMenuItem
            // 
            this.FileExtractToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileExtractAllAttachmentsToolStripMenuItem,
            this.FileExtractAllEmbeddedFilesToolStripMenuItem});
            this.FileExtractToolStripMenuItem.Name = "FileExtractToolStripMenuItem";
            resources.ApplyResources(this.FileExtractToolStripMenuItem, "FileExtractToolStripMenuItem");
            // 
            // FileExtractAllAttachmentsToolStripMenuItem
            // 
            this.FileExtractAllAttachmentsToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.icon_attachment;
            this.FileExtractAllAttachmentsToolStripMenuItem.Name = "FileExtractAllAttachmentsToolStripMenuItem";
            resources.ApplyResources(this.FileExtractAllAttachmentsToolStripMenuItem, "FileExtractAllAttachmentsToolStripMenuItem");
            this.FileExtractAllAttachmentsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileExtractAllEmbeddedFilesToolStripMenuItem
            // 
            this.FileExtractAllEmbeddedFilesToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.page_attach;
            this.FileExtractAllEmbeddedFilesToolStripMenuItem.Name = "FileExtractAllEmbeddedFilesToolStripMenuItem";
            resources.ApplyResources(this.FileExtractAllEmbeddedFilesToolStripMenuItem, "FileExtractAllEmbeddedFilesToolStripMenuItem");
            this.FileExtractAllEmbeddedFilesToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileToolStripSeparator4
            // 
            this.FileToolStripSeparator4.Name = "FileToolStripSeparator4";
            resources.ApplyResources(this.FileToolStripSeparator4, "FileToolStripSeparator4");
            // 
            // FileCopyPdfToClipboardToolStripMenuItem
            // 
            this.FileCopyPdfToClipboardToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.page_white_copy;
            this.FileCopyPdfToClipboardToolStripMenuItem.Name = "FileCopyPdfToClipboardToolStripMenuItem";
            resources.ApplyResources(this.FileCopyPdfToClipboardToolStripMenuItem, "FileCopyPdfToClipboardToolStripMenuItem");
            this.FileCopyPdfToClipboardToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileToolStripSeparator5
            // 
            this.FileToolStripSeparator5.Name = "FileToolStripSeparator5";
            resources.ApplyResources(this.FileToolStripSeparator5, "FileToolStripSeparator5");
            // 
            // FilePrintToolStripMenuItem
            // 
            this.FilePrintToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.action_print;
            this.FilePrintToolStripMenuItem.Name = "FilePrintToolStripMenuItem";
            resources.ApplyResources(this.FilePrintToolStripMenuItem, "FilePrintToolStripMenuItem");
            this.FilePrintToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FilePrintPreviewToolStripMenuItem
            // 
            this.FilePrintPreviewToolStripMenuItem.Name = "FilePrintPreviewToolStripMenuItem";
            resources.ApplyResources(this.FilePrintPreviewToolStripMenuItem, "FilePrintPreviewToolStripMenuItem");
            this.FilePrintPreviewToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileToolStripSeparator6
            // 
            this.FileToolStripSeparator6.Name = "FileToolStripSeparator6";
            resources.ApplyResources(this.FileToolStripSeparator6, "FileToolStripSeparator6");
            // 
            // FileExportToolStripMenuItem
            // 
            this.FileExportToolStripMenuItem.Name = "FileExportToolStripMenuItem";
            resources.ApplyResources(this.FileExportToolStripMenuItem, "FileExportToolStripMenuItem");
            this.FileExportToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // FileToolStripSeparator7
            // 
            this.FileToolStripSeparator7.Name = "FileToolStripSeparator7";
            resources.ApplyResources(this.FileToolStripSeparator7, "FileToolStripSeparator7");
            // 
            // FileExitToolStripMenuItem
            // 
            this.FileExitToolStripMenuItem.Name = "FileExitToolStripMenuItem";
            resources.ApplyResources(this.FileExitToolStripMenuItem, "FileExitToolStripMenuItem");
            this.FileExitToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditUndoToolStripMenuItem,
            this.EditToolStripSeparator1,
            this.EditCutToolStripMenuItem,
            this.EditCopyToolStripMenuItem,
            this.EditPasteToolStripMenuItem,
            this.EditToolStripSeparator2,
            this.EditSelectAllToolStripMenuItem,
            this.EditToolStripSeparator3,
            this.EditRestoreToolStripMenuItem,
            this.EditToolStripSeparator4,
            this.EditAppendDateTimeToolStripMenuItem,
            this.EditAppendTextToolStripMenuItem,
            this.EditToolStripSeparator5,
            this.EditFlagDocumentToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            resources.ApplyResources(this.EditToolStripMenuItem, "EditToolStripMenuItem");
            this.EditToolStripMenuItem.Tag = "";
            // 
            // EditUndoToolStripMenuItem
            // 
            this.EditUndoToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.arrow_undo;
            this.EditUndoToolStripMenuItem.Name = "EditUndoToolStripMenuItem";
            resources.ApplyResources(this.EditUndoToolStripMenuItem, "EditUndoToolStripMenuItem");
            this.EditUndoToolStripMenuItem.Tag = "";
            this.EditUndoToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditToolStripSeparator1
            // 
            this.EditToolStripSeparator1.Name = "EditToolStripSeparator1";
            resources.ApplyResources(this.EditToolStripSeparator1, "EditToolStripSeparator1");
            // 
            // EditCutToolStripMenuItem
            // 
            this.EditCutToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.cut;
            this.EditCutToolStripMenuItem.Name = "EditCutToolStripMenuItem";
            resources.ApplyResources(this.EditCutToolStripMenuItem, "EditCutToolStripMenuItem");
            this.EditCutToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditCopyToolStripMenuItem
            // 
            this.EditCopyToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.copy;
            this.EditCopyToolStripMenuItem.Name = "EditCopyToolStripMenuItem";
            resources.ApplyResources(this.EditCopyToolStripMenuItem, "EditCopyToolStripMenuItem");
            this.EditCopyToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditPasteToolStripMenuItem
            // 
            this.EditPasteToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.action_paste;
            this.EditPasteToolStripMenuItem.Name = "EditPasteToolStripMenuItem";
            resources.ApplyResources(this.EditPasteToolStripMenuItem, "EditPasteToolStripMenuItem");
            this.EditPasteToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditToolStripSeparator2
            // 
            this.EditToolStripSeparator2.Name = "EditToolStripSeparator2";
            resources.ApplyResources(this.EditToolStripSeparator2, "EditToolStripSeparator2");
            // 
            // EditSelectAllToolStripMenuItem
            // 
            this.EditSelectAllToolStripMenuItem.Name = "EditSelectAllToolStripMenuItem";
            resources.ApplyResources(this.EditSelectAllToolStripMenuItem, "EditSelectAllToolStripMenuItem");
            this.EditSelectAllToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditToolStripSeparator3
            // 
            this.EditToolStripSeparator3.Name = "EditToolStripSeparator3";
            resources.ApplyResources(this.EditToolStripSeparator3, "EditToolStripSeparator3");
            // 
            // EditRestoreToolStripMenuItem
            // 
            this.EditRestoreToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.arrow_rotate_anticlockwise;
            this.EditRestoreToolStripMenuItem.Name = "EditRestoreToolStripMenuItem";
            resources.ApplyResources(this.EditRestoreToolStripMenuItem, "EditRestoreToolStripMenuItem");
            this.EditRestoreToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditToolStripSeparator4
            // 
            this.EditToolStripSeparator4.Name = "EditToolStripSeparator4";
            resources.ApplyResources(this.EditToolStripSeparator4, "EditToolStripSeparator4");
            // 
            // EditAppendDateTimeToolStripMenuItem
            // 
            this.EditAppendDateTimeToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.date_new;
            this.EditAppendDateTimeToolStripMenuItem.Name = "EditAppendDateTimeToolStripMenuItem";
            resources.ApplyResources(this.EditAppendDateTimeToolStripMenuItem, "EditAppendDateTimeToolStripMenuItem");
            this.EditAppendDateTimeToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditAppendTextToolStripMenuItem
            // 
            this.EditAppendTextToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.page_text;
            this.EditAppendTextToolStripMenuItem.Name = "EditAppendTextToolStripMenuItem";
            resources.ApplyResources(this.EditAppendTextToolStripMenuItem, "EditAppendTextToolStripMenuItem");
            this.EditAppendTextToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // EditToolStripSeparator5
            // 
            this.EditToolStripSeparator5.Name = "EditToolStripSeparator5";
            resources.ApplyResources(this.EditToolStripSeparator5, "EditToolStripSeparator5");
            // 
            // EditFlagDocumentToolStripMenuItem
            // 
            this.EditFlagDocumentToolStripMenuItem.CheckOnClick = true;
            this.EditFlagDocumentToolStripMenuItem.Name = "EditFlagDocumentToolStripMenuItem";
            resources.ApplyResources(this.EditFlagDocumentToolStripMenuItem, "EditFlagDocumentToolStripMenuItem");
            this.EditFlagDocumentToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // DocumentsToolStripMenuItem
            // 
            this.DocumentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DocumentsFindToolStripMenuItem,
            this.DocumentsToolStripSeparator1,
            this.DocumentsSelectToolStripMenuItem,
            this.DocumentsToolStripSeparator2,
            this.DocumentsSetTitleToolStripMenuItem,
            this.DocumentsSetAuthorToolStripMenuItem,
            this.DocumentsSetSubjectToolStripMenuItem,
            this.DocumentsSetCategoryToolStripMenuItem,
            this.DocumentsSetTaxYearToolStripMenuItem,
            this.DocumentsSetDateTimeAddedToolStripMenuItem,
            this.DocumentsToolStripSeparator3,
            this.DocumentsDeleteToolStripMenuItem});
            this.DocumentsToolStripMenuItem.Name = "DocumentsToolStripMenuItem";
            resources.ApplyResources(this.DocumentsToolStripMenuItem, "DocumentsToolStripMenuItem");
            // 
            // DocumentsFindToolStripMenuItem
            // 
            this.DocumentsFindToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.page_find;
            this.DocumentsFindToolStripMenuItem.Name = "DocumentsFindToolStripMenuItem";
            resources.ApplyResources(this.DocumentsFindToolStripMenuItem, "DocumentsFindToolStripMenuItem");
            this.DocumentsFindToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // DocumentsToolStripSeparator1
            // 
            this.DocumentsToolStripSeparator1.Name = "DocumentsToolStripSeparator1";
            resources.ApplyResources(this.DocumentsToolStripSeparator1, "DocumentsToolStripSeparator1");
            // 
            // DocumentsSelectToolStripMenuItem
            // 
            this.DocumentsSelectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DocumentsSelectAllToolStripMenuItem,
            this.DocumentsSelectNoneToolStripMenuItem});
            this.DocumentsSelectToolStripMenuItem.Name = "DocumentsSelectToolStripMenuItem";
            resources.ApplyResources(this.DocumentsSelectToolStripMenuItem, "DocumentsSelectToolStripMenuItem");
            // 
            // DocumentsSelectAllToolStripMenuItem
            // 
            this.DocumentsSelectAllToolStripMenuItem.Name = "DocumentsSelectAllToolStripMenuItem";
            resources.ApplyResources(this.DocumentsSelectAllToolStripMenuItem, "DocumentsSelectAllToolStripMenuItem");
            this.DocumentsSelectAllToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // DocumentsSelectNoneToolStripMenuItem
            // 
            this.DocumentsSelectNoneToolStripMenuItem.Name = "DocumentsSelectNoneToolStripMenuItem";
            resources.ApplyResources(this.DocumentsSelectNoneToolStripMenuItem, "DocumentsSelectNoneToolStripMenuItem");
            this.DocumentsSelectNoneToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // DocumentsToolStripSeparator2
            // 
            this.DocumentsToolStripSeparator2.Name = "DocumentsToolStripSeparator2";
            resources.ApplyResources(this.DocumentsToolStripSeparator2, "DocumentsToolStripSeparator2");
            // 
            // DocumentsSetTitleToolStripMenuItem
            // 
            this.DocumentsSetTitleToolStripMenuItem.Name = "DocumentsSetTitleToolStripMenuItem";
            resources.ApplyResources(this.DocumentsSetTitleToolStripMenuItem, "DocumentsSetTitleToolStripMenuItem");
            this.DocumentsSetTitleToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // DocumentsSetAuthorToolStripMenuItem
            // 
            this.DocumentsSetAuthorToolStripMenuItem.Name = "DocumentsSetAuthorToolStripMenuItem";
            resources.ApplyResources(this.DocumentsSetAuthorToolStripMenuItem, "DocumentsSetAuthorToolStripMenuItem");
            this.DocumentsSetAuthorToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // DocumentsSetSubjectToolStripMenuItem
            // 
            this.DocumentsSetSubjectToolStripMenuItem.Name = "DocumentsSetSubjectToolStripMenuItem";
            resources.ApplyResources(this.DocumentsSetSubjectToolStripMenuItem, "DocumentsSetSubjectToolStripMenuItem");
            this.DocumentsSetSubjectToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // DocumentsSetCategoryToolStripMenuItem
            // 
            this.DocumentsSetCategoryToolStripMenuItem.Name = "DocumentsSetCategoryToolStripMenuItem";
            resources.ApplyResources(this.DocumentsSetCategoryToolStripMenuItem, "DocumentsSetCategoryToolStripMenuItem");
            this.DocumentsSetCategoryToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // DocumentsSetTaxYearToolStripMenuItem
            // 
            this.DocumentsSetTaxYearToolStripMenuItem.Name = "DocumentsSetTaxYearToolStripMenuItem";
            resources.ApplyResources(this.DocumentsSetTaxYearToolStripMenuItem, "DocumentsSetTaxYearToolStripMenuItem");
            this.DocumentsSetTaxYearToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // DocumentsSetDateTimeAddedToolStripMenuItem
            // 
            this.DocumentsSetDateTimeAddedToolStripMenuItem.Name = "DocumentsSetDateTimeAddedToolStripMenuItem";
            resources.ApplyResources(this.DocumentsSetDateTimeAddedToolStripMenuItem, "DocumentsSetDateTimeAddedToolStripMenuItem");
            this.DocumentsSetDateTimeAddedToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // DocumentsToolStripSeparator3
            // 
            this.DocumentsToolStripSeparator3.Name = "DocumentsToolStripSeparator3";
            resources.ApplyResources(this.DocumentsToolStripSeparator3, "DocumentsToolStripSeparator3");
            // 
            // DocumentsDeleteToolStripMenuItem
            // 
            this.DocumentsDeleteToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.database_delete;
            this.DocumentsDeleteToolStripMenuItem.Name = "DocumentsDeleteToolStripMenuItem";
            resources.ApplyResources(this.DocumentsDeleteToolStripMenuItem, "DocumentsDeleteToolStripMenuItem");
            this.DocumentsDeleteToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // ViewToolStripMenuItem
            // 
            this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewSetPreviewPixelDensityToolStripMenuItem,
            this.ViewToolStripSeparator,
            this.ViewToolBarToolStripMenuItem,
            this.ViewStatusBarToolStripMenuItem});
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            resources.ApplyResources(this.ViewToolStripMenuItem, "ViewToolStripMenuItem");
            this.ViewToolStripMenuItem.DropDownOpened += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // ViewSetPreviewPixelDensityToolStripMenuItem
            // 
            this.ViewSetPreviewPixelDensityToolStripMenuItem.Name = "ViewSetPreviewPixelDensityToolStripMenuItem";
            resources.ApplyResources(this.ViewSetPreviewPixelDensityToolStripMenuItem, "ViewSetPreviewPixelDensityToolStripMenuItem");
            this.ViewSetPreviewPixelDensityToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // ViewToolStripSeparator
            // 
            this.ViewToolStripSeparator.Name = "ViewToolStripSeparator";
            resources.ApplyResources(this.ViewToolStripSeparator, "ViewToolStripSeparator");
            // 
            // ViewToolBarToolStripMenuItem
            // 
            this.ViewToolBarToolStripMenuItem.Checked = true;
            this.ViewToolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ViewToolBarToolStripMenuItem.Name = "ViewToolBarToolStripMenuItem";
            resources.ApplyResources(this.ViewToolBarToolStripMenuItem, "ViewToolBarToolStripMenuItem");
            this.ViewToolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // ViewStatusBarToolStripMenuItem
            // 
            this.ViewStatusBarToolStripMenuItem.Checked = true;
            this.ViewStatusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ViewStatusBarToolStripMenuItem.Name = "ViewStatusBarToolStripMenuItem";
            resources.ApplyResources(this.ViewStatusBarToolStripMenuItem, "ViewStatusBarToolStripMenuItem");
            this.ViewStatusBarToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // ToolsToolStripMenuItem
            // 
            this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolsOptionsToolStripMenuItem,
            this.ToolsUploadProfilesToolStripMenuItem,
            this.ToolsMoveDatabaseToolStripMenuItem});
            this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            resources.ApplyResources(this.ToolsToolStripMenuItem, "ToolsToolStripMenuItem");
            // 
            // ToolsOptionsToolStripMenuItem
            // 
            this.ToolsOptionsToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.icon_settings;
            this.ToolsOptionsToolStripMenuItem.Name = "ToolsOptionsToolStripMenuItem";
            resources.ApplyResources(this.ToolsOptionsToolStripMenuItem, "ToolsOptionsToolStripMenuItem");
            this.ToolsOptionsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // ToolsUploadProfilesToolStripMenuItem
            // 
            this.ToolsUploadProfilesToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.folder;
            this.ToolsUploadProfilesToolStripMenuItem.Name = "ToolsUploadProfilesToolStripMenuItem";
            resources.ApplyResources(this.ToolsUploadProfilesToolStripMenuItem, "ToolsUploadProfilesToolStripMenuItem");
            this.ToolsUploadProfilesToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // ToolsMoveDatabaseToolStripMenuItem
            // 
            this.ToolsMoveDatabaseToolStripMenuItem.Name = "ToolsMoveDatabaseToolStripMenuItem";
            resources.ApplyResources(this.ToolsMoveDatabaseToolStripMenuItem, "ToolsMoveDatabaseToolStripMenuItem");
            this.ToolsMoveDatabaseToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpContentsToolStripMenuItem,
            this.HelpToolStripSeparator,
            this.HelpAboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            resources.ApplyResources(this.HelpToolStripMenuItem, "HelpToolStripMenuItem");
            // 
            // HelpContentsToolStripMenuItem
            // 
            this.HelpContentsToolStripMenuItem.Image = global::PDFKeeper.WinForms.Properties.Resources.icon_info;
            this.HelpContentsToolStripMenuItem.Name = "HelpContentsToolStripMenuItem";
            resources.ApplyResources(this.HelpContentsToolStripMenuItem, "HelpContentsToolStripMenuItem");
            this.HelpContentsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // HelpToolStripSeparator
            // 
            this.HelpToolStripSeparator.Name = "HelpToolStripSeparator";
            resources.ApplyResources(this.HelpToolStripSeparator, "HelpToolStripSeparator");
            // 
            // HelpAboutToolStripMenuItem
            // 
            this.HelpAboutToolStripMenuItem.Name = "HelpAboutToolStripMenuItem";
            resources.ApplyResources(this.HelpAboutToolStripMenuItem, "HelpAboutToolStripMenuItem");
            this.HelpAboutToolStripMenuItem.Click += new System.EventHandler(this.ToolStripItem_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.DataBindings.Add(new System.Windows.Forms.Binding("Visible", global::PDFKeeper.WinForms.Properties.Settings.Default, "StatusBarVisible", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DocumentsLabel,
            this.DocumentsCountLabel,
            this.StatusStripFillerLabel,
            this.DocumentsProgressBar,
            this.UploadProgressBar,
            this.RefreshingDocumentsImageLabel,
            this.FlagImageLabel,
            this.UploadRejectedImageLabel});
            resources.ApplyResources(this.StatusStrip, "StatusStrip");
            this.StatusStrip.Name = "StatusStrip";
            this.HelpProvider.SetShowHelp(this.StatusStrip, ((bool)(resources.GetObject("StatusStrip.ShowHelp"))));
            this.StatusStrip.ShowItemToolTips = true;
            this.StatusStrip.Visible = global::PDFKeeper.WinForms.Properties.Settings.Default.StatusBarVisible;
            // 
            // DocumentsLabel
            // 
            this.DocumentsLabel.Name = "DocumentsLabel";
            resources.ApplyResources(this.DocumentsLabel, "DocumentsLabel");
            // 
            // DocumentsCountLabel
            // 
            this.DocumentsCountLabel.Name = "DocumentsCountLabel";
            resources.ApplyResources(this.DocumentsCountLabel, "DocumentsCountLabel");
            // 
            // StatusStripFillerLabel
            // 
            resources.ApplyResources(this.StatusStripFillerLabel, "StatusStripFillerLabel");
            this.StatusStripFillerLabel.Name = "StatusStripFillerLabel";
            this.StatusStripFillerLabel.Spring = true;
            // 
            // DocumentsProgressBar
            // 
            this.DocumentsProgressBar.Name = "DocumentsProgressBar";
            resources.ApplyResources(this.DocumentsProgressBar, "DocumentsProgressBar");
            this.DocumentsProgressBar.Step = 1;
            // 
            // UploadProgressBar
            // 
            this.UploadProgressBar.Name = "UploadProgressBar";
            resources.ApplyResources(this.UploadProgressBar, "UploadProgressBar");
            this.UploadProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // RefreshingDocumentsImageLabel
            // 
            this.RefreshingDocumentsImageLabel.Image = global::PDFKeeper.WinForms.Properties.Resources.database_connect;
            this.RefreshingDocumentsImageLabel.Name = "RefreshingDocumentsImageLabel";
            resources.ApplyResources(this.RefreshingDocumentsImageLabel, "RefreshingDocumentsImageLabel");
            // 
            // FlagImageLabel
            // 
            this.FlagImageLabel.Image = global::PDFKeeper.WinForms.Properties.Resources.flag_red;
            this.FlagImageLabel.Name = "FlagImageLabel";
            resources.ApplyResources(this.FlagImageLabel, "FlagImageLabel");
            // 
            // UploadRejectedImageLabel
            // 
            this.UploadRejectedImageLabel.Image = global::PDFKeeper.WinForms.Properties.Resources.icon_alert;
            this.UploadRejectedImageLabel.Name = "UploadRejectedImageLabel";
            resources.ApplyResources(this.UploadRejectedImageLabel, "UploadRejectedImageLabel");
            this.UploadRejectedImageLabel.Click += new System.EventHandler(this.UploadRejectedImageLabel_Click);
            // 
            // PreviewPictureBox
            // 
            this.PreviewPictureBox.DataBindings.Add(new System.Windows.Forms.Binding("Image", this.MainViewModelBindingSource, "Preview", true));
            resources.ApplyResources(this.PreviewPictureBox, "PreviewPictureBox");
            this.PreviewPictureBox.Name = "PreviewPictureBox";
            this.HelpProvider.SetShowHelp(this.PreviewPictureBox, ((bool)(resources.GetObject("PreviewPictureBox.ShowHelp"))));
            this.PreviewPictureBox.TabStop = false;
            // 
            // MainViewModelBindingSource
            // 
            this.MainViewModelBindingSource.DataSource = typeof(PDFKeeper.Core.ViewModels.MainViewModel);
            // 
            // DocumentsDataGridView
            // 
            this.DocumentsDataGridView.AllowUserToAddRows = false;
            this.DocumentsDataGridView.AllowUserToDeleteRows = false;
            this.DocumentsDataGridView.AllowUserToResizeRows = false;
            this.DocumentsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DocumentsDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DocumentsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DocumentsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DocumentsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectionColumn,
            this.FlagColumn});
            this.DocumentsDataGridView.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.MainViewModelBindingSource, "DocumentsEnabled", true));
            resources.ApplyResources(this.DocumentsDataGridView, "DocumentsDataGridView");
            this.DocumentsDataGridView.MultiSelect = false;
            this.DocumentsDataGridView.Name = "DocumentsDataGridView";
            this.DocumentsDataGridView.RowHeadersVisible = false;
            this.DocumentsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.HelpProvider.SetShowHelp(this.DocumentsDataGridView, ((bool)(resources.GetObject("DocumentsDataGridView.ShowHelp"))));
            this.DocumentsDataGridView.StandardTab = true;
            this.DocumentsDataGridView.DataSourceChanged += new System.EventHandler(this.DocumentsDataGridView_DataSourceChanged);
            this.DocumentsDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DocumentsDataGridView_CellDoubleClick);
            this.DocumentsDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DocumentsDataGridView_CellValueChanged);
            this.DocumentsDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.DocumentsDataGridView_CurrentCellDirtyStateChanged);
            this.DocumentsDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DocumentsDataGridView_RowsAdded);
            this.DocumentsDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.DocumentsDataGridView_RowsRemoved);
            this.DocumentsDataGridView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.DocumentsDataGridView_Scroll);
            this.DocumentsDataGridView.SelectionChanged += new System.EventHandler(this.DocumentsDataGridView_SelectionChanged);
            this.DocumentsDataGridView.Sorted += new System.EventHandler(this.DocumentsDataGridView_Sorted);
            // 
            // DocumentDataTabControl
            // 
            this.DocumentDataTabControl.Controls.Add(this.NotesTabPage);
            this.DocumentDataTabControl.Controls.Add(this.KeywordsTabPage);
            this.DocumentDataTabControl.Controls.Add(this.TextTabPage);
            this.DocumentDataTabControl.Controls.Add(this.SearchTermSnippetsTabPage);
            this.DocumentDataTabControl.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.MainViewModelBindingSource, "DocumentDataEnabled", true));
            resources.ApplyResources(this.DocumentDataTabControl, "DocumentDataTabControl");
            this.DocumentDataTabControl.Multiline = true;
            this.DocumentDataTabControl.Name = "DocumentDataTabControl";
            this.DocumentDataTabControl.SelectedIndex = 0;
            this.HelpProvider.SetShowHelp(this.DocumentDataTabControl, ((bool)(resources.GetObject("DocumentDataTabControl.ShowHelp"))));
            // 
            // NotesTabPage
            // 
            this.NotesTabPage.Controls.Add(this.NotesTextBox);
            resources.ApplyResources(this.NotesTabPage, "NotesTabPage");
            this.NotesTabPage.Name = "NotesTabPage";
            this.HelpProvider.SetShowHelp(this.NotesTabPage, ((bool)(resources.GetObject("NotesTabPage.ShowHelp"))));
            this.NotesTabPage.UseVisualStyleBackColor = true;
            // 
            // NotesTextBox
            // 
            this.NotesTextBox.AcceptsReturn = true;
            this.NotesTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MainViewModelBindingSource, "Notes", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.NotesTextBox.DataBindings.Add(new System.Windows.Forms.Binding("ReadOnly", this.MainViewModelBindingSource, "NotesReadOnly", true));
            resources.ApplyResources(this.NotesTextBox, "NotesTextBox");
            this.NotesTextBox.Name = "NotesTextBox";
            this.HelpProvider.SetShowHelp(this.NotesTextBox, ((bool)(resources.GetObject("NotesTextBox.ShowHelp"))));
            this.NotesTextBox.Enter += new System.EventHandler(this.TextBox_Enter);
            this.NotesTextBox.GotFocus += new System.EventHandler(this.TextBox_Enter);
            this.NotesTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.NotesTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            this.NotesTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseUp);
            // 
            // KeywordsTabPage
            // 
            this.KeywordsTabPage.Controls.Add(this.KeywordsTextBox);
            resources.ApplyResources(this.KeywordsTabPage, "KeywordsTabPage");
            this.KeywordsTabPage.Name = "KeywordsTabPage";
            this.HelpProvider.SetShowHelp(this.KeywordsTabPage, ((bool)(resources.GetObject("KeywordsTabPage.ShowHelp"))));
            this.KeywordsTabPage.UseVisualStyleBackColor = true;
            // 
            // KeywordsTextBox
            // 
            this.KeywordsTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MainViewModelBindingSource, "Keywords", true));
            resources.ApplyResources(this.KeywordsTextBox, "KeywordsTextBox");
            this.KeywordsTextBox.Name = "KeywordsTextBox";
            this.KeywordsTextBox.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.KeywordsTextBox, ((bool)(resources.GetObject("KeywordsTextBox.ShowHelp"))));
            this.KeywordsTextBox.Enter += new System.EventHandler(this.TextBox_Enter);
            this.KeywordsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.KeywordsTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            this.KeywordsTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseUp);
            // 
            // TextTabPage
            // 
            this.TextTabPage.Controls.Add(this.TextTextBox);
            resources.ApplyResources(this.TextTabPage, "TextTabPage");
            this.TextTabPage.Name = "TextTabPage";
            this.HelpProvider.SetShowHelp(this.TextTabPage, ((bool)(resources.GetObject("TextTabPage.ShowHelp"))));
            this.TextTabPage.UseVisualStyleBackColor = true;
            // 
            // TextTextBox
            // 
            this.TextTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MainViewModelBindingSource, "Text", true));
            resources.ApplyResources(this.TextTextBox, "TextTextBox");
            this.TextTextBox.Name = "TextTextBox";
            this.TextTextBox.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.TextTextBox, ((bool)(resources.GetObject("TextTextBox.ShowHelp"))));
            this.TextTextBox.Enter += new System.EventHandler(this.TextBox_Enter);
            this.TextTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.TextTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            this.TextTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseUp);
            // 
            // SearchTermSnippetsTabPage
            // 
            this.SearchTermSnippetsTabPage.Controls.Add(this.SearchTermSnippetsTextBox);
            resources.ApplyResources(this.SearchTermSnippetsTabPage, "SearchTermSnippetsTabPage");
            this.SearchTermSnippetsTabPage.Name = "SearchTermSnippetsTabPage";
            this.HelpProvider.SetShowHelp(this.SearchTermSnippetsTabPage, ((bool)(resources.GetObject("SearchTermSnippetsTabPage.ShowHelp"))));
            this.SearchTermSnippetsTabPage.UseVisualStyleBackColor = true;
            // 
            // SearchTermSnippetsTextBox
            // 
            this.SearchTermSnippetsTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.MainViewModelBindingSource, "SearchTermSnippets", true));
            resources.ApplyResources(this.SearchTermSnippetsTextBox, "SearchTermSnippetsTextBox");
            this.SearchTermSnippetsTextBox.Name = "SearchTermSnippetsTextBox";
            this.SearchTermSnippetsTextBox.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.SearchTermSnippetsTextBox, ((bool)(resources.GetObject("SearchTermSnippetsTextBox.ShowHelp"))));
            this.SearchTermSnippetsTextBox.Enter += new System.EventHandler(this.TextBox_Enter);
            this.SearchTermSnippetsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.SearchTermSnippetsTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            this.SearchTermSnippetsTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseUp);
            // 
            // HorizontalSplitContainer
            // 
            this.HorizontalSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.HorizontalSplitContainer, "HorizontalSplitContainer");
            this.HorizontalSplitContainer.Name = "HorizontalSplitContainer";
            // 
            // HorizontalSplitContainer.Panel1
            // 
            this.HorizontalSplitContainer.Panel1.Controls.Add(this.SearchResultsPanel);
            this.HelpProvider.SetShowHelp(this.HorizontalSplitContainer.Panel1, ((bool)(resources.GetObject("HorizontalSplitContainer.Panel1.ShowHelp"))));
            // 
            // HorizontalSplitContainer.Panel2
            // 
            this.HorizontalSplitContainer.Panel2.Controls.Add(this.VerticalSplitContainer);
            this.HelpProvider.SetShowHelp(this.HorizontalSplitContainer.Panel2, ((bool)(resources.GetObject("HorizontalSplitContainer.Panel2.ShowHelp"))));
            this.HelpProvider.SetShowHelp(this.HorizontalSplitContainer, ((bool)(resources.GetObject("HorizontalSplitContainer.ShowHelp"))));
            this.HorizontalSplitContainer.TabStop = false;
            // 
            // SearchResultsPanel
            // 
            this.SearchResultsPanel.Controls.Add(this.DocumentsDataGridView);
            resources.ApplyResources(this.SearchResultsPanel, "SearchResultsPanel");
            this.SearchResultsPanel.Name = "SearchResultsPanel";
            this.HelpProvider.SetShowHelp(this.SearchResultsPanel, ((bool)(resources.GetObject("SearchResultsPanel.ShowHelp"))));
            // 
            // VerticalSplitContainer
            // 
            resources.ApplyResources(this.VerticalSplitContainer, "VerticalSplitContainer");
            this.VerticalSplitContainer.Name = "VerticalSplitContainer";
            // 
            // VerticalSplitContainer.Panel1
            // 
            this.VerticalSplitContainer.Panel1.Controls.Add(this.DocumentDataTabControl);
            this.HelpProvider.SetShowHelp(this.VerticalSplitContainer.Panel1, ((bool)(resources.GetObject("VerticalSplitContainer.Panel1.ShowHelp"))));
            // 
            // VerticalSplitContainer.Panel2
            // 
            this.VerticalSplitContainer.Panel2.Controls.Add(this.PreviewTabControl);
            this.HelpProvider.SetShowHelp(this.VerticalSplitContainer.Panel2, ((bool)(resources.GetObject("VerticalSplitContainer.Panel2.ShowHelp"))));
            this.HelpProvider.SetShowHelp(this.VerticalSplitContainer, ((bool)(resources.GetObject("VerticalSplitContainer.ShowHelp"))));
            this.VerticalSplitContainer.TabStop = false;
            // 
            // PreviewTabControl
            // 
            this.PreviewTabControl.Controls.Add(this.PreviewTabPage);
            resources.ApplyResources(this.PreviewTabControl, "PreviewTabControl");
            this.PreviewTabControl.Name = "PreviewTabControl";
            this.PreviewTabControl.SelectedIndex = 0;
            this.HelpProvider.SetShowHelp(this.PreviewTabControl, ((bool)(resources.GetObject("PreviewTabControl.ShowHelp"))));
            this.PreviewTabControl.TabStop = false;
            // 
            // PreviewTabPage
            // 
            this.PreviewTabPage.Controls.Add(this.PreviewPanel);
            resources.ApplyResources(this.PreviewTabPage, "PreviewTabPage");
            this.PreviewTabPage.Name = "PreviewTabPage";
            this.HelpProvider.SetShowHelp(this.PreviewTabPage, ((bool)(resources.GetObject("PreviewTabPage.ShowHelp"))));
            this.PreviewTabPage.UseVisualStyleBackColor = true;
            // 
            // PreviewPanel
            // 
            resources.ApplyResources(this.PreviewPanel, "PreviewPanel");
            this.PreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PreviewPanel.Controls.Add(this.PreviewPictureBox);
            this.PreviewPanel.Name = "PreviewPanel";
            this.HelpProvider.SetShowHelp(this.PreviewPanel, ((bool)(resources.GetObject("PreviewPanel.ShowHelp"))));
            // 
            // DocumentsListTimedRefreshTimer
            // 
            this.DocumentsListTimedRefreshTimer.Enabled = true;
            this.DocumentsListTimedRefreshTimer.Interval = 60000;
            this.DocumentsListTimedRefreshTimer.Tick += new System.EventHandler(this.DocumentsListTimedRefreshTimer_Tick);
            // 
            // CheckForDocumentsListChangesTimer
            // 
            this.CheckForDocumentsListChangesTimer.Enabled = true;
            this.CheckForDocumentsListChangesTimer.Interval = 5000;
            this.CheckForDocumentsListChangesTimer.Tick += new System.EventHandler(this.CheckForDocumentsListChangesTimer_Tick);
            // 
            // UploadTimer
            // 
            this.UploadTimer.Enabled = true;
            this.UploadTimer.Interval = 15000;
            this.UploadTimer.Tick += new System.EventHandler(this.UploadTimer_Tick);
            // 
            // UpdateCheckTimer
            // 
            this.UpdateCheckTimer.Enabled = true;
            this.UpdateCheckTimer.Interval = 3600000;
            this.UpdateCheckTimer.Tick += new System.EventHandler(this.UpdateCheckTimer_Tick);
            // 
            // CheckForFlaggedDocumentsTimer
            // 
            this.CheckForFlaggedDocumentsTimer.Enabled = true;
            this.CheckForFlaggedDocumentsTimer.Interval = 5000;
            this.CheckForFlaggedDocumentsTimer.Tick += new System.EventHandler(this.CheckForFlaggedDocumentsTimer_Tick);
            // 
            // SelectionColumn
            // 
            this.SelectionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.SelectionColumn, "SelectionColumn");
            this.SelectionColumn.Name = "SelectionColumn";
            // 
            // FlagColumn
            // 
            this.FlagColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.FlagColumn, "FlagColumn");
            this.FlagColumn.Name = "FlagColumn";
            this.FlagColumn.ReadOnly = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HorizontalSplitContainer);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.ToolStrip);
            this.Controls.Add(this.MenuStrip);
            this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.Name = "MainForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentsDataGridView)).EndInit();
            this.DocumentDataTabControl.ResumeLayout(false);
            this.NotesTabPage.ResumeLayout(false);
            this.NotesTabPage.PerformLayout();
            this.KeywordsTabPage.ResumeLayout(false);
            this.KeywordsTabPage.PerformLayout();
            this.TextTabPage.ResumeLayout(false);
            this.TextTabPage.PerformLayout();
            this.SearchTermSnippetsTabPage.ResumeLayout(false);
            this.SearchTermSnippetsTabPage.PerformLayout();
            this.HorizontalSplitContainer.Panel1.ResumeLayout(false);
            this.HorizontalSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HorizontalSplitContainer)).EndInit();
            this.HorizontalSplitContainer.ResumeLayout(false);
            this.SearchResultsPanel.ResumeLayout(false);
            this.VerticalSplitContainer.Panel1.ResumeLayout(false);
            this.VerticalSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VerticalSplitContainer)).EndInit();
            this.VerticalSplitContainer.ResumeLayout(false);
            this.PreviewTabControl.ResumeLayout(false);
            this.PreviewTabPage.ResumeLayout(false);
            this.PreviewPanel.ResumeLayout(false);
            this.PreviewPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip ToolStrip;
        internal System.Windows.Forms.ToolStripButton FileAddToolStripButton;
        internal System.Windows.Forms.ToolStripButton FileOpenToolStripButton;
        internal System.Windows.Forms.ToolStripButton FileSaveToolStripButton;
        internal System.Windows.Forms.ToolStripButton FileBurstToolStripButton;
        internal System.Windows.Forms.ToolStripButton FilePrintToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolBarToolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton EditUndoToolStripButton;
        internal System.Windows.Forms.ToolStripButton EditCutToolStripButton;
        internal System.Windows.Forms.ToolStripButton EditCopyToolStripButton;
        internal System.Windows.Forms.ToolStripButton EditPasteToolStripButton;
        internal System.Windows.Forms.ToolStripButton EditRestoreToolStripButton;
        internal System.Windows.Forms.ToolStripButton EditAppendDateTimeToolStripButton;
        internal System.Windows.Forms.ToolStripButton EditAppendTextToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolBarToolStripSeparator2;
        internal System.Windows.Forms.ToolStripButton DocumentsFindToolStripButton;
        internal System.Windows.Forms.ToolStripButton DocumentsDeleteToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolBarToolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton ToolsOptionsToolStripButton;
        internal System.Windows.Forms.ToolStripButton ToolsUploadProfilesToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator ToolBarToolStripSeparator4;
        internal System.Windows.Forms.ToolStripButton HelpContentsToolStripButton;
        internal System.Windows.Forms.MenuStrip MenuStrip;
        internal System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem FileAddToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator FileToolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem FileOpenToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator FileToolStripSeparator2;
        internal System.Windows.Forms.ToolStripMenuItem FileSaveToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem FileSaveAsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator FileToolStripSeparator3;
        internal System.Windows.Forms.ToolStripMenuItem FileBurstToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator FileToolStripSeparator4;
        internal System.Windows.Forms.ToolStripMenuItem FileCopyPdfToClipboardToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator FileToolStripSeparator5;
        internal System.Windows.Forms.ToolStripMenuItem FilePrintToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem FilePrintPreviewToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator FileToolStripSeparator6;
        internal System.Windows.Forms.ToolStripMenuItem FileExportToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator FileToolStripSeparator7;
        internal System.Windows.Forms.ToolStripMenuItem FileExitToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem EditUndoToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator EditToolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem EditCutToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem EditCopyToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem EditPasteToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator EditToolStripSeparator2;
        internal System.Windows.Forms.ToolStripMenuItem EditSelectAllToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator EditToolStripSeparator3;
        internal System.Windows.Forms.ToolStripMenuItem EditRestoreToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator EditToolStripSeparator4;
        internal System.Windows.Forms.ToolStripMenuItem EditAppendDateTimeToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem EditAppendTextToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator EditToolStripSeparator5;
        internal System.Windows.Forms.ToolStripMenuItem EditFlagDocumentToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DocumentsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DocumentsFindToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator DocumentsToolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem DocumentsSelectToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DocumentsSelectAllToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DocumentsSelectNoneToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator DocumentsToolStripSeparator2;
        internal System.Windows.Forms.ToolStripMenuItem DocumentsSetTitleToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DocumentsSetAuthorToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DocumentsSetSubjectToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DocumentsSetCategoryToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DocumentsSetTaxYearToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator DocumentsToolStripSeparator3;
        internal System.Windows.Forms.ToolStripMenuItem DocumentsDeleteToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ViewSetPreviewPixelDensityToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ViewToolStripSeparator;
        internal System.Windows.Forms.ToolStripMenuItem ViewToolBarToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ViewStatusBarToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ToolsOptionsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ToolsUploadProfilesToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ToolsMoveDatabaseToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem HelpContentsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator HelpToolStripSeparator;
        internal System.Windows.Forms.ToolStripMenuItem HelpAboutToolStripMenuItem;
        internal System.Windows.Forms.StatusStrip StatusStrip;
        internal System.Windows.Forms.ToolStripStatusLabel DocumentsLabel;
        internal System.Windows.Forms.ToolStripStatusLabel DocumentsCountLabel;
        internal System.Windows.Forms.ToolStripStatusLabel StatusStripFillerLabel;
        internal System.Windows.Forms.ToolStripProgressBar DocumentsProgressBar;
        internal System.Windows.Forms.ToolStripStatusLabel RefreshingDocumentsImageLabel;
        internal System.Windows.Forms.ToolStripStatusLabel FlagImageLabel;
        internal System.Windows.Forms.ToolStripStatusLabel UploadRejectedImageLabel;
        internal System.Windows.Forms.PictureBox PreviewPictureBox;
        internal System.Windows.Forms.DataGridView DocumentsDataGridView;
        internal System.Windows.Forms.TabControl DocumentDataTabControl;
        internal System.Windows.Forms.TabPage NotesTabPage;
        internal System.Windows.Forms.TextBox NotesTextBox;
        internal System.Windows.Forms.TabPage KeywordsTabPage;
        internal System.Windows.Forms.TextBox KeywordsTextBox;
        internal System.Windows.Forms.TabPage TextTabPage;
        internal System.Windows.Forms.TextBox TextTextBox;
        internal System.Windows.Forms.TabPage SearchTermSnippetsTabPage;
        internal System.Windows.Forms.TextBox SearchTermSnippetsTextBox;
        internal System.Windows.Forms.SplitContainer HorizontalSplitContainer;
        internal System.Windows.Forms.Panel SearchResultsPanel;
        internal System.Windows.Forms.SplitContainer VerticalSplitContainer;
        internal System.Windows.Forms.TabControl PreviewTabControl;
        internal System.Windows.Forms.TabPage PreviewTabPage;
        internal System.Windows.Forms.Panel PreviewPanel;
        private System.Windows.Forms.BindingSource MainViewModelBindingSource;
        internal System.Windows.Forms.Timer DocumentsListTimedRefreshTimer;
        internal System.Windows.Forms.Timer CheckForDocumentsListChangesTimer;
        internal System.Windows.Forms.HelpProvider HelpProvider;
        internal System.Windows.Forms.Timer UploadTimer;
        internal System.Windows.Forms.Timer UpdateCheckTimer;
        internal System.Windows.Forms.Timer CheckForFlaggedDocumentsTimer;
        private System.Windows.Forms.ToolStripMenuItem DocumentsSetDateTimeAddedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileExtractToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton FileExtractAllAttachmentsToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem FileExtractAllAttachmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileExtractAllEmbeddedFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton FileExtractAllEmbeddedFilesToolStripButton;
        private System.Windows.Forms.ToolStripProgressBar UploadProgressBar;
        private System.Windows.Forms.ToolStripButton FileCopyPdfToClipboardToolStripButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectionColumn;
        private System.Windows.Forms.DataGridViewImageColumn FlagColumn;
    }
}

