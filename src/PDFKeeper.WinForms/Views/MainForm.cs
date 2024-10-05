// *****************************************************************************
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
// *****************************************************************************

using AutoUpdaterDotNET;
using PDFKeeper.Core.Application;
using PDFKeeper.Core.Commands;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.Presenters;
using PDFKeeper.Core.ViewModels;
using PDFKeeper.PDFViewer.Services;
using PDFKeeper.WinForms.Commands;
using PDFKeeper.WinForms.Dialogs;
using PDFKeeper.WinForms.Helpers;
using PDFKeeper.WinForms.Properties;
using PDFKeeper.WinForms.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Views
{
    public partial class MainForm : Form
    {
        private readonly MainPresenter presenter;
        private readonly MainViewModel viewModel;
        private readonly DataGridViewSortProperties dataGridViewSortProperties;
        private int dataGridViewScrollPosition;
        private readonly HelpFile helpFile;
        private readonly FindDocumentsForm findDocumentsForm;

        // Message that is sent when the contents of the clipboard have changed.
        private const int WM_CLIPBOARDUPDATE = 0x31D;

        public MainForm()
        {
            InitializeComponent();

            presenter = new MainPresenter(
                Handle,
                new PdfViewerService(),
                new FolderBrowserDialogService(),
                new MessageBoxService(),
                new FolderExplorerService(),
                new SetTitleDialogService(),
                new SetAuthorDialogService(),
                new SetSubjectDialogService(),
                new SetCategoryDialogService(),
                new SetTaxYearDialogService(),
                new SetDateTimeAddedDialogService(),
                new OpenFileDialogService(),
                new SaveFileDialogService(),
                new PrintDialogService(),
                new PrintPreviewDialogService());
            viewModel = presenter.ViewModel;
            MainViewModelBindingSource.DataSource = presenter.ViewModel;
            dataGridViewSortProperties = new DataGridViewSortProperties();
            helpFile = new HelpFile();
            HelpProvider.HelpNamespace = helpFile.FullName;
            findDocumentsForm = new FindDocumentsForm();
            AddEventHandlers();
            AddTags();
        }

        private void AddEventHandlers()
        {
            presenter.LongRunningOperationStarted += MainForm_LongRunningOperationStarted;
            presenter.LongRunningOperationFinished += MainForm_LongRunningOperationFinished;
            presenter.CheckedDocumentsProcessed += MainForm_CheckedDocumentsProcessed;
            presenter.ScrollToEndOfNotesTextRequested += MainForm_ScrollToEndOfNotesTextRequested;
            presenter.ProgressBarPerformStepRequested += MainForm_ProgressBarPerformStepRequested;
            viewModel.PropertyChanged += MainForm_PropertyChanged;
        }

        private void AddTags()
        {
            FileAddToolStripMenuItem.Tag = new DialogShowCommand(new AddPdfForm(), null);
            FileAddToolStripButton.Tag = new DialogShowCommand(new AddPdfForm(), null);
            FileOpenToolStripMenuItem.Tag = new OpenPdfCommand(presenter);
            FileOpenToolStripButton.Tag = new OpenPdfCommand(presenter);
            FileSaveToolStripMenuItem.Tag = new SaveCommand(presenter);
            FileSaveToolStripButton.Tag = new SaveCommand(presenter);
            FileSaveAsToolStripMenuItem.Tag = new SaveAsCommand(presenter);
            FileBurstToolStripMenuItem.Tag = new BurstPdfCommand(presenter);
            FileBurstToolStripButton.Tag = new BurstPdfCommand(presenter);
            FileCopyPdfToClipboardToolStripMenuItem.Tag = new CopyPdfToClipboardCommand(presenter);
            FilePrintToolStripMenuItem.Tag = new PrintTextCommand(presenter, false);
            FilePrintToolStripButton.Tag = new PrintTextCommand(presenter, false);
            FilePrintPreviewToolStripMenuItem.Tag = new PrintTextCommand(presenter, true);
            FileExportToolStripMenuItem.Tag = new ExportCommand(presenter);
            FileExitToolStripMenuItem.Tag = new CloseCommand(this);
            EditUndoToolStripMenuItem.Tag = new UndoCommand(this);
            EditUndoToolStripButton.Tag = new UndoCommand(this);
            EditCutToolStripMenuItem.Tag = new CutCommand(this, presenter);
            EditCutToolStripButton.Tag = new CutCommand(this, presenter);
            EditCopyToolStripMenuItem.Tag = new CopyCommand(this);
            EditCopyToolStripButton.Tag = new CopyCommand(this);
            EditPasteToolStripMenuItem.Tag = new PasteCommand(this);
            EditPasteToolStripButton.Tag = new PasteCommand(this);
            EditSelectAllToolStripMenuItem.Tag = new SelectAllCommand(this, presenter);
            EditRestoreToolStripMenuItem.Tag = new RestoreCommand(presenter);
            EditRestoreToolStripButton.Tag = new RestoreCommand(presenter);
            EditAppendDateTimeToolStripMenuItem.Tag = new AppendDateTimeIntoNotesCommand(
                presenter);
            EditAppendDateTimeToolStripButton.Tag = new AppendDateTimeIntoNotesCommand(presenter);
            EditAppendTextToolStripMenuItem.Tag = new AppendTextFromFileIntoNotes(presenter);
            EditAppendTextToolStripButton.Tag = new AppendTextFromFileIntoNotes(presenter);
            EditFlagDocumentToolStripMenuItem.Tag = new FlagStateToggleCommand(presenter);
            DocumentsFindToolStripMenuItem.Tag = new DialogShowCommand(findDocumentsForm, null);
            DocumentsFindToolStripButton.Tag = new DialogShowCommand(findDocumentsForm, null);
            DocumentsSelectAllToolStripMenuItem.Tag = new SelectAllDocumentsCommand(this, true);
            DocumentsSelectNoneToolStripMenuItem.Tag = new SelectAllDocumentsCommand(this, false);
            DocumentsSetTitleToolStripMenuItem.Tag = new SetTitleCommand(presenter);
            DocumentsSetAuthorToolStripMenuItem.Tag = new SetAuthorCommand(presenter);
            DocumentsSetSubjectToolStripMenuItem.Tag = new SetSubjectCommand(presenter);
            DocumentsSetCategoryToolStripMenuItem.Tag = new SetCategoryCommand(presenter);
            DocumentsSetTaxYearToolStripMenuItem.Tag = new SetTaxYearCommand(presenter);
            DocumentsSetDateTimeAddedToolStripMenuItem.Tag = new SetDateTimeAddedCommand(
                presenter);
            DocumentsDeleteToolStripMenuItem.Tag = new DeleteCommand(presenter);
            DocumentsDeleteToolStripButton.Tag = new DeleteCommand(presenter);
            ViewToolStripMenuItem.Tag = new ViewMenuCheckedStateCommand(this);
            ViewSetPreviewPixelDensityToolStripMenuItem.Tag = new DialogShowCommand(
                new SetPreviewPixelDensityForm(),
                new SetPreviewImageCommand(presenter));
            ViewToolBarToolStripMenuItem.Tag = new ToolBarToggleCommand(this);
            ViewStatusBarToolStripMenuItem.Tag = new StatusBarToggleCommand(this);
            ToolsOptionsToolStripMenuItem.Tag = new DialogShowCommand(new OptionsForm(), null);
            ToolsOptionsToolStripButton.Tag = new DialogShowCommand(new OptionsForm(), null);
            ToolsUploadProfilesToolStripMenuItem.Tag = new DialogShowCommand(
                new UploadProfilesForm(),
                null);
            ToolsUploadProfilesToolStripButton.Tag = new DialogShowCommand(
                new UploadProfilesForm(),
                null);
            ToolsMoveDatabaseToolStripMenuItem.Tag = new MoveDatabaseCommand(presenter);
            HelpContentsToolStripMenuItem.Tag = new HelpFileShowCommand(helpFile, this);
            HelpContentsToolStripButton.Tag = new HelpFileShowCommand(helpFile, this);
            HelpAboutToolStripMenuItem.Tag = new DialogShowCommand(new AboutBox(), null);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg.Equals(WM_CLIPBOARDUPDATE))
            {
                if (Clipboard.ContainsText())
                {
                    if (NotesTextBox.Focused)
                    {
                        presenter.SetPasteEnabledState(true);
                    }
                }
            }
            base.WndProc(ref m);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            presenter.AddClipboardFormatListener();
            UpdateCheckTimer_Tick(this, null);
            GetFormState();
            presenter.SetInitialState();
            if (Settings.Default.FindFlaggedDocumentsOnStartup)
            {
                presenter.GetListOfFlaggedDocuments();
            }
            else if (Settings.Default.ShowAllDocumentsOnStartup &&
                DatabaseSession.PlatformName.Equals(
                    DatabaseSession.CompatiblePlatformName.Sqlite))
            {
                presenter.GetListOfAllDocuments();
            }
        }

        private void GetFormState()
        {
            if (Settings.Default.MainFormLocation != null)
            {
                // Workaround for rare bug that can cause this form to be positioned off the screen.
                if (Settings.Default.MainFormLocation.Equals(new Point(-32000, -32000)))
                {
                    Location = new Point(0, 0);
                }
                else
                {
                    Location = Settings.Default.MainFormLocation;
                }
            }
            if (Settings.Default.MainFormSize != null)
            {
                Size = Settings.Default.MainFormSize;
            }
            WindowState = Settings.Default.MainFormState;
            HorizontalSplitContainer.SplitterDistance = Settings.Default.HorizontalSplitterDistance;
            VerticalSplitContainer.SplitterDistance = Settings.Default.VerticalSplitterDistance;
        }

        private void ToolStripItem_Click(object sender, EventArgs e)
        {
            if (sender.GetType().Name.Equals("ToolStripMenuItem", StringComparison.Ordinal))
            {
                presenter.ExecuteCommand(((ToolStripMenuItem)sender).Tag as ICommand);
            }
            else if (sender.GetType().Name.Equals("ToolStripButton", StringComparison.Ordinal))
            {
                presenter.ExecuteCommand(((ToolStripButton)sender).Tag as ICommand);
            }
        }

        private void DocumentsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            presenter.OpenPdfForCurrentDocument(Settings.Default.ShowPdfWithDefaultApplication);
        }

        private void DocumentsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            presenter.SetCheckedDocumentIds(GetCheckedDocumentIds());
        }

        private void DocumentsDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DocumentsDataGridView.IsCurrentCellDirty)
            {
                DocumentsDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DocumentsDataGridView_DataSourceChanged(object sender, EventArgs e)
        {
            DocumentsDataGridView.Columns[0].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;
            DocumentsDataGridView.Columns[1].HeaderCell.Value = Resources.ID;
            DocumentsDataGridView.Columns[1].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            DocumentsDataGridView.Columns[1].ReadOnly = true;
            DocumentsDataGridView.Columns[2].HeaderCell.Value = Resources.Title;
            DocumentsDataGridView.Columns[2].ReadOnly = true;
            DocumentsDataGridView.Columns[3].HeaderCell.Value = Resources.Author;
            DocumentsDataGridView.Columns[3].ReadOnly = true;
            DocumentsDataGridView.Columns[4].HeaderCell.Value = Resources.Subject;
            DocumentsDataGridView.Columns[4].ReadOnly = true;
            DocumentsDataGridView.Columns[5].HeaderCell.Value = Resources.Category;
            DocumentsDataGridView.Columns[5].ReadOnly = true;
            DocumentsDataGridView.Columns[6].HeaderCell.Value = Resources.TaxYear;
            DocumentsDataGridView.Columns[6].ReadOnly = true;
            DocumentsDataGridView.Columns[7].HeaderCell.Value = Resources.Added;
            DocumentsDataGridView.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DocumentsDataGridView.Columns[7].ReadOnly = true;
            if (DocumentsDataGridView.RowCount > 0)
            {
                DocumentsDataGridView.Columns[7].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.DisplayedCells;
                if (DocumentsDataGridView.Columns[7].Displayed)
                {
                    DocumentsDataGridView.Columns[7].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;
                }
                DocumentsDataGridView.Columns[7].MinimumWidth =
                    (int)Math.Round(DocumentsDataGridView.Columns[7].FillWeight + 20f);
                DocumentsDataGridView.Sort(
                    DocumentsDataGridView.Columns[dataGridViewSortProperties.SortColumnIndex],
                    dataGridViewSortProperties.SortDirection);
                if (Settings.Default.SelectLastDocumentRow)
                {
                    DocumentsDataGridView.Rows[DocumentsDataGridView.Rows.Count - 1].Selected =
                        true;
                    DocumentsDataGridView.FirstDisplayedScrollingRowIndex =
                        DocumentsDataGridView.RowCount - 1;
                }
            }
            else
            {
                DocumentsDataGridView.Select();
            }
        }

        private void DocumentsDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DocumentsCountLabel.Text = DocumentsDataGridView.Rows.Count.ToString();
        }

        private void DocumentsDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            DocumentsDataGridView_RowsAdded(this, null);
            DocumentsDataGridView_CellValueChanged(this, null);
        }

        private void DocumentsDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            dataGridViewScrollPosition = e.NewValue;
        }

        private void DocumentsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            viewModel.CurrentDocumentId = 0;    // No row is selected.
            if (DocumentsDataGridView.SelectedRows.Count > 0)   // To prevent empty DataGridView.
            {
                viewModel.CurrentDocumentId = Convert.ToInt32(
                    DocumentsDataGridView.SelectedRows[0].Cells[1].Value);
            }
            presenter.DocumentSelectionChanged(Settings.Default.PreviewPixelDensity);
        }

        private void DocumentsDataGridView_Sorted(object sender, EventArgs e)
        {
            dataGridViewSortProperties.SortedColumn = DocumentsDataGridView.SortedColumn;
            dataGridViewSortProperties.SortOrder = DocumentsDataGridView.SortOrder;
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            SetTextBoxFocusedState(textBox, true);
            presenter.SetTextBoxEnterState(textBox.CanUndo);
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBoxHelper.SyncSelectedTextWithViewModel((TextBox)sender, this, viewModel);
            presenter.SetStateForTextBoxSelectedText();
        }

        private void TextBox_MouseUp(object sender, MouseEventArgs e)
        {
            TextBoxHelper.SyncSelectedTextWithViewModel((TextBox)sender, this, viewModel);
            presenter.SetStateForTextBoxSelectedText();
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            SetTextBoxFocusedState((TextBox)sender, false);
            presenter.SetTextBoxLeaveState();
        }

        private void UploadRejectedImageLabel_Click(object sender, EventArgs e)
        {
            presenter.ExploreUploadRejected();
        }

        private void UpdateCheckTimer_Tick(object sender, EventArgs e)
        {
            AutoUpdater.RunUpdateAsAdmin = false;
            AutoUpdater.Start(ApplicationUri.AutoUpdaterConfig.AbsoluteUri);
        }

        private async void CheckForFlaggedDocumentsTimer_Tick(object sender, EventArgs e)
        {
            CheckForFlaggedDocumentsTimer.Stop();
            await Task.Run(() => presenter.CheckForFlaggedDocuments()).ConfigureAwait(true);
            CheckForFlaggedDocumentsTimer.Start();
        }

        private void CheckForDocumentsListChangesTimer_Tick(object sender, EventArgs e)
        {
            CheckForDocumentsListChangesTimer.Stop();
            presenter.CheckForDocumentsListChanges();
            CheckForDocumentsListChangesTimer.Start();
        }

        private async void UploadTimer_Tick(object sender, EventArgs e)
        {
            UploadTimer.Stop();
            await Task.Run(() => presenter.ExecuteUploadDirectoryMaintenance()).ConfigureAwait(true);
            await Task.Run(() => presenter.ExecuteUpload()).ConfigureAwait(true);
            presenter.CheckForRejectedPdfFiles();
            UploadTimer.Start();
        }

        private void DocumentsListTimedRefreshTimer_Tick(object sender, EventArgs e)
        {
            DocumentsListTimedRefreshTimer.Stop();
            presenter.SetDocumentsListHasChanges();
            DocumentsListTimedRefreshTimer.Start();
        }

        private void MainForm_LongRunningOperationStarted(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
        }

        private void MainForm_LongRunningOperationFinished(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void MainForm_CheckedDocumentsProcessed(object sender, EventArgs e)
        {
            ToolStripItem_Click(DocumentsSelectNoneToolStripMenuItem, null);
        }

        private void MainForm_ScrollToEndOfNotesTextRequested(object sender, EventArgs e)
        {
            NotesTextBox.Select();
            NotesTextBox.Select(NotesTextBox.Text.Length, 0);
            NotesTextBox.ScrollToCaret();
        }

        private void MainForm_ProgressBarPerformStepRequested(object sender, EventArgs e)
        {
            ProgressBar.PerformStep();
            Application.DoEvents();
        }

        private void MainForm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("FileOpenMenuEnabled", StringComparison.Ordinal))
            {
                FileOpenToolStripMenuItem.Enabled = viewModel.FileOpenMenuEnabled;
                FileOpenToolStripButton.Enabled = viewModel.FileOpenMenuEnabled;
            }
            else if (e.PropertyName.Equals("FileSaveMenuEnabled", StringComparison.Ordinal))
            {
                FileSaveToolStripMenuItem.Enabled = viewModel.FileSaveMenuEnabled;
                FileSaveToolStripButton.Enabled = viewModel.FileSaveMenuEnabled;
            }
            else if (e.PropertyName.Equals("FileSaveAsMenuEnabled", StringComparison.Ordinal))
            {
                FileSaveAsToolStripMenuItem.Enabled = viewModel.FileSaveAsMenuEnabled;
            }
            else if (e.PropertyName.Equals("FileBurstMenuEnabled", StringComparison.Ordinal))
            {
                FileBurstToolStripMenuItem.Enabled = viewModel.FileBurstMenuEnabled;
                FileBurstToolStripButton.Enabled = viewModel.FileBurstMenuEnabled;
            }
            else if (e.PropertyName.Equals(
                "FileCopyPdfToClipboardEnabled",
                StringComparison.Ordinal))
            {
                FileCopyPdfToClipboardToolStripMenuItem.Enabled =
                    viewModel.FileCopyPdfToClipboardEnabled;
            }
            else if (e.PropertyName.Equals("FilePrintMenuEnabled", StringComparison.Ordinal))
            {
                FilePrintToolStripMenuItem.Enabled = viewModel.FilePrintMenuEnabled;
                FilePrintToolStripButton.Enabled = viewModel.FilePrintMenuEnabled;
            }
            else if (e.PropertyName.Equals(
                "FilePrintPreviewMenuEnabled",
                StringComparison.Ordinal))
            {
                FilePrintPreviewToolStripMenuItem.Enabled = viewModel.FilePrintPreviewMenuEnabled;
            }
            else if (e.PropertyName.Equals("FileExportMenuEnabled", StringComparison.Ordinal))
            {
                FileExportToolStripMenuItem.Enabled = viewModel.FileExportMenuEnabled;
            }
            else if (e.PropertyName.Equals("EditUndoMenuEnabled", StringComparison.Ordinal))
            {
                EditUndoToolStripMenuItem.Enabled = viewModel.EditUndoMenuEnabled;
                EditUndoToolStripButton.Enabled = viewModel.EditUndoMenuEnabled;
            }
            else if (e.PropertyName.Equals("EditCutMenuEnabled", StringComparison.Ordinal))
            {
                EditCutToolStripMenuItem.Enabled = viewModel.EditCutMenuEnabled;
                EditCutToolStripButton.Enabled = viewModel.EditCutMenuEnabled;
            }
            else if (e.PropertyName.Equals("EditCopyMenuEnabled", StringComparison.Ordinal))
            {
                EditCopyToolStripMenuItem.Enabled = viewModel.EditCopyMenuEnabled;
                EditCopyToolStripButton.Enabled = viewModel.EditCopyMenuEnabled;
            }
            else if (e.PropertyName.Equals("EditPasteMenuEnabled", StringComparison.Ordinal))
            {
                EditPasteToolStripMenuItem.Enabled = viewModel.EditPasteMenuEnabled;
                EditPasteToolStripButton.Enabled = viewModel.EditPasteMenuEnabled;
            }
            else if (e.PropertyName.Equals("EditSelectAllMenuEnabled", StringComparison.Ordinal))
            {
                EditSelectAllToolStripMenuItem.Enabled = viewModel.EditSelectAllMenuEnabled;
            }
            else if (e.PropertyName.Equals("EditRestoreMenuEnabled", StringComparison.Ordinal))
            {
                EditRestoreToolStripMenuItem.Enabled = viewModel.EditRestoreMenuEnabled;
                EditRestoreToolStripButton.Enabled = viewModel.EditRestoreMenuEnabled;
            }
            else if (e.PropertyName.Equals(
                "EditAppendDateTimeMenuEnabled",
                StringComparison.Ordinal))
            {
                EditAppendDateTimeToolStripMenuItem.Enabled = 
                    viewModel.EditAppendDateTimeMenuEnabled;
                EditAppendDateTimeToolStripButton.Enabled =
                    viewModel.EditAppendDateTimeMenuEnabled;
            }
            else if (e.PropertyName.Equals("EditAppendTextMenuEnabled", StringComparison.Ordinal))
            {
                EditAppendTextToolStripMenuItem.Enabled = viewModel.EditAppendTextMenuEnabled;
                EditAppendTextToolStripButton.Enabled = viewModel.EditAppendTextMenuEnabled;
            }
            else if (e.PropertyName.Equals(
                "EditFlagDocumentMenuEnabled",
                StringComparison.Ordinal))
            {
                EditFlagDocumentToolStripMenuItem.Enabled = viewModel.EditFlagDocumentMenuEnabled;
            }
            else if (e.PropertyName.Equals(
                "EditFlagDocumentMenuChecked",
                StringComparison.Ordinal))
            {
                EditFlagDocumentToolStripMenuItem.Checked = viewModel.EditFlagDocumentMenuChecked;
            }
            else if (e.PropertyName.Equals("DocumentsFindMenuEnabled", StringComparison.Ordinal))
            {
                DocumentsFindToolStripMenuItem.Enabled = viewModel.DocumentsFindMenuEnabled;
                DocumentsFindToolStripButton.Enabled = viewModel.DocumentsFindMenuEnabled;
            }
            else if (e.PropertyName.Equals("DocumentsSelectMenuEnabled", StringComparison.Ordinal))
            {
                DocumentsSelectToolStripMenuItem.Enabled = viewModel.DocumentsSelectMenuEnabled;
            }
            else if (e.PropertyName.Equals(
                "DocumentsSetTitleMenuEnabled",
                StringComparison.Ordinal))
            {
                DocumentsSetTitleToolStripMenuItem.Enabled = 
                    viewModel.DocumentsSetTitleMenuEnabled;
            }
            else if (e.PropertyName.Equals(
                "DocumentsSetAuthorMenuEnabled",
                StringComparison.Ordinal))
            {
                DocumentsSetAuthorToolStripMenuItem.Enabled = 
                    viewModel.DocumentsSetAuthorMenuEnabled;
            }
            else if (e.PropertyName.Equals(
                "DocumentsSetSubjectMenuEnabled",
                StringComparison.Ordinal))
            {
                DocumentsSetSubjectToolStripMenuItem.Enabled = 
                    viewModel.DocumentsSetSubjectMenuEnabled;
            }
            else if (e.PropertyName.Equals(
                "DocumentsSetCategoryMenuEnabled",
                StringComparison.Ordinal))
            {
                DocumentsSetCategoryToolStripMenuItem.Enabled = 
                    viewModel.DocumentsSetCategoryMenuEnabled;
            }
            else if (e.PropertyName.Equals(
                "DocumentsSetTaxYearMenuEnabled",
                StringComparison.Ordinal))
            {
                DocumentsSetTaxYearToolStripMenuItem.Enabled =
                    viewModel.DocumentsSetTaxYearMenuEnabled;
            }
            else if (e.PropertyName.Equals(
                "DocumentsSetDateTimeAddedMenuEnabled",
                StringComparison.Ordinal))
            {
                DocumentsSetDateTimeAddedToolStripMenuItem.Enabled =
                    viewModel.DocumentsSetDateTimeAddedMenuEnabled;
            }
            else if (e.PropertyName.Equals("DocumentsDeleteMenuEnabled", StringComparison.Ordinal))
            {
                DocumentsDeleteToolStripMenuItem.Enabled = viewModel.DocumentsDeleteMenuEnabled;
                DocumentsDeleteToolStripButton.Enabled = viewModel.DocumentsDeleteMenuEnabled;
            }
            else if (e.PropertyName.Equals(
                "ViewSetPreviewPixelDensityMenuEnabled",
                StringComparison.Ordinal))
            {
                ViewSetPreviewPixelDensityToolStripMenuItem.Enabled = 
                    viewModel.ViewSetPreviewPixelDensityMenuEnabled;
            }
            else if (e.PropertyName.Equals(
                "ToolsMoveDatabaseMenuVisible",
                StringComparison.Ordinal))
            {
                ToolsMoveDatabaseToolStripMenuItem.Visible = 
                    viewModel.ToolsMoveDatabaseMenuVisible;
            }
            else if (e.PropertyName.Equals("Documents", StringComparison.Ordinal))
            {
                DocumentsDataGridView.DataSource = viewModel.Documents;
            }
            else if (e.PropertyName.Equals("CurrentDocumentId", StringComparison.Ordinal))
            {
                if (viewModel.CurrentDocumentId > 0)
                {
                    foreach (DataGridViewRow row in DocumentsDataGridView.Rows)
                    {
                        if (Convert.ToInt32(
                            row.Cells[1].Value).Equals(
                            viewModel.CurrentDocumentId))
                        {
                            row.Selected = true;
                            try
                            {
                                DocumentsDataGridView.FirstDisplayedScrollingRowIndex = 
                                    dataGridViewScrollPosition;
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                            }
                        }
                    }
                }
            }
            else if (e.PropertyName.Equals("Notes", StringComparison.Ordinal))
            {
                NotesTextBox.Text = NotesTextBox.Text.TrimStart();
                presenter.OnNotesTextChanged(NotesTextBox.CanUndo);
            }
            else if (e.PropertyName.Equals("SearchTermSnippetsVisible", StringComparison.Ordinal))
            {
                if (viewModel.SearchTermSnippetsVisible)
                {
                    if (!DocumentDataTabControl.TabPages.Contains(SearchTermSnippetsTabPage))
                    {
                        DocumentDataTabControl.TabPages.Insert(3, SearchTermSnippetsTabPage);
                    }
                }
                else
                {
                    DocumentDataTabControl.TabPages.Remove(SearchTermSnippetsTabPage);
                }
            }
            else if (e.PropertyName.Equals("ProgressBarVisible", StringComparison.Ordinal))
            {
                ProgressBar.Visible = viewModel.ProgressBarVisible;
            }
            else if (e.PropertyName.Equals("ProgressBarMinimum", StringComparison.Ordinal))
            {
                ProgressBar.Minimum = viewModel.ProgressBarMinimum;
            }
            else if (e.PropertyName.Equals("ProgressBarMaximum", StringComparison.Ordinal))
            {
                ProgressBar.Maximum = viewModel.ProgressBarMaximum;
            }
            else if (e.PropertyName.Equals(
                "RefreshingDocumentsImageVisible",
                StringComparison.Ordinal))
            {
                RefreshingDocumentsImageLabel.Visible = viewModel.RefreshingDocumentsImageVisible;
                Application.DoEvents();
            }
            else if (e.PropertyName.Equals("FlagImageVisible", StringComparison.Ordinal))
            {
                FlagImageLabel.Visible = viewModel.FlagImageVisible;
                Application.DoEvents();
            }
            else if (e.PropertyName.Equals("UploadRunningImageVisible", StringComparison.Ordinal))
            {
                UploadRunningImageLabel.Visible = viewModel.UploadRunningImageVisible;
                Application.DoEvents();
            }
            else if (e.PropertyName.Equals("UploadRejectedImageVisible", StringComparison.Ordinal))
            {
                UploadRejectedImageLabel.Visible = viewModel.UploadRejectedImageVisible;
                Application.DoEvents();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (viewModel.NotesChanged)
            {
                presenter.SaveNotesPromptBeforeClosing();
            }
            e.Cancel = presenter.CancelViewClosing;
            presenter.WaitForUploadToFinish();
            presenter.RemoveClipboardFormatListener();
            SetFormState();
        }

        private void SetFormState()
        {
            Settings.Default.HorizontalSplitterDistance =
                HorizontalSplitContainer.SplitterDistance;
            Settings.Default.VerticalSplitterDistance = VerticalSplitContainer.SplitterDistance;
            Settings.Default.MainFormLocation = Location;
            if (WindowState.Equals(FormWindowState.Normal))
            {
                Settings.Default.MainFormSize = Size;
            }
            Settings.Default.MainFormState = WindowState;
        }

        private Collection<int> GetCheckedDocumentIds()
        {
            var ids = new Collection<int>();
            try
            {
                foreach (DataGridViewRow row in DocumentsDataGridView.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        ids.Add(Convert.ToInt32(row.Cells[1].Value));
                    }
                }
            }
            catch (NullReferenceException) { }
            return ids;
        }

        /// <summary>
        /// Sets the text box focused state in the ViewModel.
        /// </summary>
        /// <param name="textBox">The TextBox object.</param>
        /// <param name="enabled">Set focus to enabled? (true or false)</param>
        private void SetTextBoxFocusedState(TextBox textBox, bool enabled)
        {
            if (textBox.Equals(NotesTextBox))
            {
                viewModel.NotesFocused = enabled;
            }
            else if (textBox.Equals(KeywordsTextBox))
            {
                viewModel.KeywordsFocused = enabled;
            }
            else if (textBox.Equals(TextTextBox))
            {
                viewModel.TextFocused = enabled;
            }
            else if (textBox.Equals(SearchTermSnippetsTextBox))
            {
                viewModel.SearchTermSnippetsFocused = enabled;
            }
        }
    }
}
