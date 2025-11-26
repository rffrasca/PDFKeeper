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

using Microsoft.Extensions.DependencyInjection;
using PDFKeeper.Core.Application;
using PDFKeeper.Core.DataAccess;
using PDFKeeper.Core.FileIO.PDF;
using PDFKeeper.Core.Services;
using PDFKeeper.Core.ViewModels;
using PDFKeeper.WinForms.Commands;
using PDFKeeper.WinForms.Dialogs;
using PDFKeeper.WinForms.Properties;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static PDFKeeper.Core.ViewModels.MainViewModel;

namespace PDFKeeper.WinForms.Views
{
    internal partial class MainForm : Form
    {
        private readonly MainViewModel viewModel;
        private readonly IHelpService helpService;
        private readonly DataGridViewSortProperties dataGridViewSortProperties;
        private int dataGridViewScrollPosition;
        private readonly FindDocumentsForm findDocumentsForm;
        private readonly UploadProfilesForm uploadProfilesForm;
        private readonly ProgressForm progressForm;

        // Message that is sent when the contents of the clipboard have changed.
        private const int WM_CLIPBOARDUPDATE = 0x31D;

        public MainForm()
        {
            InitializeComponent();
            viewModel = new MainViewModel(Handle);
            MainViewModelBindingSource.DataSource = viewModel;
            helpService = ServiceLocator.Services.GetService<IHelpService>();
            dataGridViewSortProperties = new DataGridViewSortProperties();
            HelpProvider.HelpNamespace = new HelpFile().FullName;
            findDocumentsForm = new FindDocumentsForm();
            uploadProfilesForm = new UploadProfilesForm();
            progressForm = new ProgressForm();
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
            SetActions();
            SetTags();
        }

        private void SetActions()
        {
            viewModel.OnLongOperationStarted = () => Cursor = Cursors.WaitCursor;
            viewModel.OnLongOperationFinished = () => Cursor = Cursors.Default;
            viewModel.OnCloseView = () => Close();
            viewModel.OnCheckForUpdate = () => TagCommand.Invoke(UpdateCheckTimer);

            viewModel.OnGetViewState = () =>
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

                if (Settings.Default.MainFormState.Equals(FormWindowState.Minimized))
                {
                    WindowState = FormWindowState.Normal;
                }
                else
                {
                    WindowState = Settings.Default.MainFormState;
                }

                HorizontalSplitContainer.SplitterDistance = Settings.Default.HorizontalSplitterDistance;
                VerticalSplitContainer.SplitterDistance = Settings.Default.VerticalSplitterDistance;                
            };

            viewModel.OnUndoNotes = () => NotesTextBox.Undo();

            viewModel.OnCutNotes = () =>
            {
                NotesTextBox.Cut();
                viewModel.SelectedNotes = NotesTextBox.SelectedText;
            };

            viewModel.OnCopyText = () => GetTextBoxWithFocus().Copy();
            viewModel.OnPasteNotes = () => NotesTextBox.Paste();

            viewModel.OnSelectAllText = () =>
            {
                var textBox = GetTextBoxWithFocus();
                textBox.SelectAll();

                switch (textBox.Name)
                {
                    case "NotesTextBox":
                        viewModel.SelectedNotes = NotesTextBox.SelectedText;
                        break;
                    case "KeywordsTextBox":
                        viewModel.SelectedKeywords = KeywordsTextBox.SelectedText;
                        break;
                    case "TextTextBox":
                        viewModel.SelectedText = TextTextBox.SelectedText;
                        break;
                    case "SearchTermSnippetsTextBox":
                        viewModel.SelectedSearchTermSnippets = SearchTermSnippetsTextBox.SelectedText;
                        break;
                }
            };

            viewModel.OnFindDocuments = () =>
            {
                ToolTip.Active = false;
                findDocumentsForm.ShowDialog();
            };

            viewModel.OnSelectAllDocuments = (selection) =>
            {
                foreach (DataGridViewRow row in DocumentsDataGridView.Rows)
                {
                    row.Cells[0].Value = selection;
                }
                
                DocumentsDataGridView.RefreshEdit();
            };

            viewModel.OnSettingsChanged = () =>
            {
                viewModel.CompactLocalDatabaseAfterDelete =
                    Settings.Default.CompactLocalDatabaseAfterDelete;
                viewModel.PreviewPixelDensity =
                    Settings.Default.PreviewPixelDensity;
                viewModel.ShowPdfWithDefaultApplication =
                    Settings.Default.ShowPdfWithDefaultApplication;
                viewModel.ViewSize =
                    Settings.Default.MainFormSize;
                viewModel.ToolStripVisible =
                    Settings.Default.ToolBarVisible;
                viewModel.StatusStripVisible =
                    Settings.Default.StatusBarVisible;
            };

            viewModel.OnManageUploadProfiles = ()
                => uploadProfilesForm.ShowDialog();
            viewModel.OnShowHelp = ()
                => helpService.ShowHelp<Control>(this, HelpFile.Topic.UsingPDFKeeper);

            viewModel.OnPdfDoDragDrop = (pdfFile) =>
            {
                DocumentsDataGridView.DoDragDrop(
                    new DataObject(
                        DataFormats.FileDrop,
                        new string[] { pdfFile.FullName }),
                    DragDropEffects.Copy);
            };

            viewModel.OnScrollToEndOfNotesText = () =>
            {
                NotesTextBox.Select();
                NotesTextBox.Select(NotesTextBox.Text.Length, 0);
                NotesTextBox.ScrollToCaret();
            };

            viewModel.OnBlockingUploadStarted = () =>
            {
                BeginInvoke((MethodInvoker)delegate ()
                {
                    progressForm.Message = Resources.PdfUploadInProgress;
                    progressForm.ShowDialog(this);
                });
            };

            viewModel.OnBlockingUploadFinished = () => progressForm.Close();

            viewModel.OnProgressBarPerformStep = () =>
            {
                DocumentsProgressBar.PerformStep();
                Application.DoEvents();
            };

            viewModel.OnCheckedDocumentsProcessed = ()
                => ToolStripItem_Click(DocumentsSelectNoneToolStripMenuItem, null);
            viewModel.OnCheckForFlaggedDocumentsStarted = ()
                => CheckForFlaggedDocumentsTimer.Stop();
            viewModel.OnCheckForFlaggedDocumentsFinished = ()
                => CheckForFlaggedDocumentsTimer.Start();
            viewModel.OnCheckForDocumentsListChangesStarted = ()
                => CheckForDocumentsListChangesTimer.Stop();
            viewModel.OnCheckForDocumentsListChangesFinished = ()
                => CheckForDocumentsListChangesTimer.Start();
            viewModel.OnUploadPdfFilesStarted = ()
                => UploadTimer.Stop();
            viewModel.OnUploadPdfFilesFinished = ()
                => UploadTimer.Start();
            viewModel.OnSetDocumentsListHasChangesStarted = ()
                => DocumentsListTimedRefreshTimer.Stop();
            viewModel.OnSetDocumentsListHasChangesFinished = ()
                => DocumentsListTimedRefreshTimer.Start();

            viewModel.OnSetViewState = () =>
            {
                Settings.Default.HorizontalSplitterDistance =
                    HorizontalSplitContainer.SplitterDistance;
                Settings.Default.VerticalSplitterDistance =
                    VerticalSplitContainer.SplitterDistance;
                Settings.Default.MainFormLocation = Location;

                if (WindowState.Equals(FormWindowState.Normal))
                {
                    Settings.Default.MainFormSize = Size;
                }

                Settings.Default.MainFormState = WindowState;
            };
        }

        private void SetTags()
        {
            FileAddToolStripMenuItem.Tag =
                viewModel.AddPdfCommand;
            FileAddToolStripButton.Tag = 
                viewModel.AddPdfCommand;
            FileOpenToolStripMenuItem.Tag =
                viewModel.OpenPdfForEachSelectedDocumentCommand;
            FileOpenToolStripButton.Tag =
                viewModel.OpenPdfForEachSelectedDocumentCommand;
            FileSaveToolStripMenuItem.Tag =
                viewModel.SaveNotesCommand;
            FileSaveToolStripButton.Tag = 
                viewModel.SaveNotesCommand;
            FileSaveAsToolStripMenuItem.Tag =
                viewModel.PdfOrTextSaveAsCommand;
            FileBurstToolStripMenuItem.Tag =
                viewModel.BurstCurrentDocumentPdfCommand;
            FileBurstToolStripButton.Tag =
                viewModel.BurstCurrentDocumentPdfCommand;
            FileExtractAllAttachmentsToolStripMenuItem.Tag =
                new FileExtractAllCommand(
                    viewModel,
                    PdfFile.AttachedFilesType.Attachment);
            FileExtractAllAttachmentsToolStripButton.Tag =
                new FileExtractAllCommand(
                    viewModel,
                    PdfFile.AttachedFilesType.Attachment);
            FileExtractAllEmbeddedFilesToolStripMenuItem.Tag =
                new FileExtractAllCommand(
                    viewModel,
                    PdfFile.AttachedFilesType.EmbeddedFile);
            FileExtractAllEmbeddedFilesToolStripButton.Tag =
                new FileExtractAllCommand(
                    viewModel,
                    PdfFile.AttachedFilesType.EmbeddedFile);
            FileCopyPdfToClipboardToolStripMenuItem.Tag =
                viewModel.CopyCurrentDocumentPdfToClipboardCommand;
            FileCopyPdfToClipboardToolStripButton.Tag =
                viewModel.CopyCurrentDocumentPdfToClipboardCommand;
            FilePrintToolStripMenuItem.Tag =
                viewModel.PrintDocumentDataTextCommand;
            FilePrintToolStripButton.Tag =
                viewModel.PrintDocumentDataTextCommand;
            FilePrintPreviewToolStripMenuItem.Tag =
                viewModel.PrintDocumentDataTextWithPreviewCommand;
            FileExportToolStripMenuItem.Tag =
                viewModel.ExportEachSelectedDocumentCommand;
            FileExitToolStripMenuItem.Tag =
                viewModel.CloseCommand;
            EditUndoToolStripMenuItem.Tag =
                viewModel.UndoNotesCommand;
            EditUndoToolStripButton.Tag =
                viewModel.UndoNotesCommand;
            EditCutToolStripMenuItem.Tag =
                viewModel.CutNotesCommand;
            EditCutToolStripButton.Tag =
                viewModel.CutNotesCommand;
            EditCopyToolStripMenuItem.Tag =
                viewModel.CopyTextCommand;
            EditCopyToolStripButton.Tag =
                viewModel.CopyTextCommand;
            EditPasteToolStripMenuItem.Tag =
                viewModel.PasteNotesCommand;
            EditPasteToolStripButton.Tag =
                viewModel.PasteNotesCommand;
            EditSelectAllToolStripMenuItem.Tag =
                viewModel.SelectAllTextCommand;
            EditRestoreToolStripMenuItem.Tag =
                viewModel.RestoreNotesCommand;
            EditRestoreToolStripButton.Tag =
                viewModel.RestoreNotesCommand;
            EditAppendDateTimeToolStripMenuItem.Tag =
                viewModel.AppendDateTimeIntoNotesCommand;
            EditAppendDateTimeToolStripButton.Tag =
                viewModel.AppendDateTimeIntoNotesCommand;
            EditAppendTextToolStripMenuItem.Tag =
                viewModel.AppendTextFromFileIntoNotesCommand;
            EditAppendTextToolStripButton.Tag = 
                viewModel.AppendTextFromFileIntoNotesCommand;
            EditFlagDocumentToolStripMenuItem.Tag =
                viewModel.UpdateCurrentDocumentFlagStateCommand;
            DocumentsFindToolStripMenuItem.Tag =
                viewModel.FindDocumentsCommand;
            DocumentsFindToolStripButton.Tag =
                viewModel.FindDocumentsCommand;
            DocumentsSelectAllToolStripMenuItem.Tag =
                viewModel.SelectAllDocumentsCommand;
            DocumentsSelectNoneToolStripMenuItem.Tag =
                viewModel.SelectNoDocumentsCommand;
            DocumentsSetTitleToolStripMenuItem.Tag =
                viewModel.SetTitleOnEachSelectedDocumentCommand;
            DocumentsSetAuthorToolStripMenuItem.Tag =
                viewModel.SetAuthorOnEachSelectedDocumentCommand;
            DocumentsSetSubjectToolStripMenuItem.Tag =
                viewModel.SetSubjectOnEachSelectedDocumentCommand;
            DocumentsSetCategoryToolStripMenuItem.Tag =
                viewModel.SetCategoryOnEachSelectedDocumentCommand;
            DocumentsSetTaxYearToolStripMenuItem.Tag =
                viewModel.SetTaxYearOnEachSelectedDocumentCommand;
            DocumentsSetDateTimeAddedToolStripMenuItem.Tag =
                viewModel.SetDateTimeAddedOnEachSelectedDocumentCommand;
            DocumentsDeleteToolStripMenuItem.Tag =
                viewModel.DeleteEachSelectedDocumentCommand;
            DocumentsDeleteToolStripButton.Tag =
                viewModel.DeleteEachSelectedDocumentCommand;
            ViewSetPreviewPixelDensityToolStripMenuItem.Tag =
                viewModel.SetPreviewPixelDensityCommand;
            ViewToolBarToolStripMenuItem.Tag =
                viewModel.ToggleToolStripVisibleStateCommand;
            ViewStatusBarToolStripMenuItem.Tag =
                viewModel.ToggleStatusStripVisibleStateCommand;
            ToolsOptionsToolStripMenuItem.Tag =
                viewModel.ShowOptionsCommand;
            ToolsOptionsToolStripButton.Tag =
                viewModel.ShowOptionsCommand;
            ToolsUploadProfilesToolStripMenuItem.Tag =
                viewModel.ManageUploadProfilesCommand;
            ToolsUploadProfilesToolStripButton.Tag =
                viewModel.ManageUploadProfilesCommand;
            ToolsMoveDatabaseToolStripMenuItem.Tag =
                viewModel.MoveLocalDatabaseCommand;
            HelpContentsToolStripMenuItem.Tag =
                viewModel.ShowHelpCommand;
            HelpContentsToolStripButton.Tag =
                viewModel.ShowHelpCommand;
            HelpAboutToolStripMenuItem.Tag =
                viewModel.ShowAboutBoxCommand;
            UpdateCheckTimer.Tag =
                new UpdateCheckCommand();
            CheckForFlaggedDocumentsTimer.Tag =
                viewModel.CheckForFlaggedDocumentsCommand;
            CheckForDocumentsListChangesTimer.Tag =
                viewModel.CheckForDocumentsListChangesCommand;
            UploadTimer.Tag =
                viewModel.UploadPdfFilesCommand;
            DocumentsListTimedRefreshTimer.Tag =
                viewModel.SetDocumentsListHasChangesCommand;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg.Equals(WM_CLIPBOARDUPDATE))
            {
                viewModel.ClipboardUpdateCommand.Execute(null);
            }

            base.WndProc(ref m);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            viewModel.ViewLoadCommand.Execute(GetStartupAction());
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (viewModel != null)
            {
                viewModel.ViewMinimized = WindowState.Equals(FormWindowState.Minimized);
            }
        }

        private void ToolStripItem_Click(object sender, EventArgs e)
        {
            TagCommand.Invoke(sender);
        }

        private void DocumentsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            viewModel.OpenPdfForCurrentDocumentCommand.Execute(
                Settings.Default.ShowPdfWithDefaultApplication);
        }

        private void DocumentsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            viewModel.SetCheckedDocumentIdsCommand.Execute(GetCheckedDocumentIds());
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
            DocumentsDataGridView.Columns[1].DefaultCellStyle.NullValue = null;
            DocumentsDataGridView.Columns[1].AutoSizeMode =
                DataGridViewAutoSizeColumnMode.AllCells;
            DocumentsDataGridView.Columns[2].HeaderCell.Value = Resources.ID;
            DocumentsDataGridView.Columns[2].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            DocumentsDataGridView.Columns[2].ReadOnly = true;
            DocumentsDataGridView.Columns[3].HeaderCell.Value = Resources.Title;
            DocumentsDataGridView.Columns[3].ReadOnly = true;
            DocumentsDataGridView.Columns[4].HeaderCell.Value = Resources.Author;
            DocumentsDataGridView.Columns[4].ReadOnly = true;
            DocumentsDataGridView.Columns[5].HeaderCell.Value = Resources.Subject;
            DocumentsDataGridView.Columns[5].ReadOnly = true;
            DocumentsDataGridView.Columns[6].HeaderCell.Value = Resources.Category;
            DocumentsDataGridView.Columns[6].ReadOnly = true;
            DocumentsDataGridView.Columns[7].HeaderCell.Value = Resources.TaxYear;
            DocumentsDataGridView.Columns[7].ReadOnly = true;
            DocumentsDataGridView.Columns[8].HeaderCell.Value = Resources.Added;
            DocumentsDataGridView.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DocumentsDataGridView.Columns[8].ReadOnly = true;
            DocumentsDataGridView.Columns[9].Visible = false;
            
            if (DocumentsDataGridView.RowCount > 0)
            {
                DocumentsDataGridView.Columns[8].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.DisplayedCells;
                
                if (DocumentsDataGridView.Columns[8].Displayed)
                {
                    DocumentsDataGridView.Columns[8].AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill;
                }

                DocumentsDataGridView.Columns[8].MinimumWidth = (int)Math.Round(
                    DocumentsDataGridView.Columns[8].FillWeight + 20f);
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

        private void DocumentsDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            new MainFormMouseDownCommand(e, this, viewModel).Execute(null);
        }

        private void DocumentsDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var image = Resources.flag_red;

            foreach (DataGridViewRow row in DocumentsDataGridView.Rows)
            {
                if (Convert.ToInt32(row.Cells[9].Value).Equals(1))
                {
                    row.Cells[1].Value = image;
                }
            }

            DocumentsCountLabel.Text = DocumentsDataGridView.Rows.Count.ToString();
        }

        private void DocumentsDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            DocumentsCountLabel.Text = DocumentsDataGridView.Rows.Count.ToString();
            DocumentsDataGridView_CellValueChanged(this, null);
        }

        private void DocumentsDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            dataGridViewScrollPosition = e.NewValue;
        }

        private void DocumentsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            new DocumentSelectionChangedCommand(this, viewModel).Execute(null);
        }

        private void DocumentsDataGridView_Sorted(object sender, EventArgs e)
        {
            dataGridViewSortProperties.SortedColumn = DocumentsDataGridView.SortedColumn;
            dataGridViewSortProperties.SortOrder = DocumentsDataGridView.SortOrder;
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            new TextBoxEnterCommand((TextBox)sender, viewModel).Execute(null);
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            new TextBoxSelectedTextCommand((TextBox)sender, this, viewModel).Execute(null);
        }

        private void TextBox_MouseUp(object sender, MouseEventArgs e)
        {
            new TextBoxSelectedTextCommand((TextBox)sender, this, viewModel).Execute(null);
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            new TextBoxLeaveCommand((TextBox)sender, viewModel).Execute(null);
        }

        private void UploadRejectedImageLabel_Click(object sender, EventArgs e)
        {
            viewModel.ExploreUploadRejectedFolderCommand.Execute(null);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TagCommand.Invoke(sender);
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(viewModel.ToolStripVisible):
                    ToolStrip.Visible = viewModel.ToolStripVisible;
                    break;
                case nameof(viewModel.StatusStripVisible):
                    StatusStrip.Visible = viewModel.StatusStripVisible;
                    break;
                case nameof(viewModel.FileAddMenuEnabled):
                    FileAddToolStripMenuItem.Enabled = viewModel.FileAddMenuEnabled;
                    FileAddToolStripButton.Enabled = viewModel.FileAddMenuEnabled;
                    break;
                case nameof(viewModel.FileOpenMenuEnabled):
                    FileOpenToolStripMenuItem.Enabled = viewModel.FileOpenMenuEnabled;
                    FileOpenToolStripButton.Enabled = viewModel.FileOpenMenuEnabled;
                    break;
                case nameof(viewModel.FileSaveMenuEnabled):
                    FileSaveToolStripMenuItem.Enabled = viewModel.FileSaveMenuEnabled;
                    FileSaveToolStripButton.Enabled = viewModel.FileSaveMenuEnabled;
                    break;
                case nameof(viewModel.FileSaveAsMenuEnabled):
                    FileSaveAsToolStripMenuItem.Enabled = viewModel.FileSaveAsMenuEnabled;
                    break;
                case nameof(viewModel.FileBurstMenuEnabled):
                    FileBurstToolStripMenuItem.Enabled = viewModel.FileBurstMenuEnabled;
                    FileBurstToolStripButton.Enabled = viewModel.FileBurstMenuEnabled;
                    break;
                case nameof(viewModel.FileExtractMenuEnabled):
                    FileExtractToolStripMenuItem.Enabled = viewModel.FileExtractMenuEnabled;
                    break;
                case nameof(viewModel.FileExtractAllAttachmentsMenuEnabled):
                    FileExtractAllAttachmentsToolStripMenuItem.Enabled =
                        viewModel.FileExtractAllAttachmentsMenuEnabled;
                    FileExtractAllAttachmentsToolStripButton.Enabled =
                        viewModel.FileExtractAllAttachmentsMenuEnabled;
                    break;
                case nameof(viewModel.FileExtractAllEmbeddedFilesMenuEnabled):
                    FileExtractAllEmbeddedFilesToolStripMenuItem.Enabled =
                        viewModel.FileExtractAllEmbeddedFilesMenuEnabled;
                    FileExtractAllEmbeddedFilesToolStripButton.Enabled =
                        viewModel.FileExtractAllEmbeddedFilesMenuEnabled;
                    break;
                case nameof(viewModel.FileCopyPdfToClipboardEnabled):
                    FileCopyPdfToClipboardToolStripMenuItem.Enabled =
                        viewModel.FileCopyPdfToClipboardEnabled;
                    FileCopyPdfToClipboardToolStripButton.Enabled =
                        viewModel.FileCopyPdfToClipboardEnabled;
                    break;
                case nameof(viewModel.FilePrintMenuEnabled):
                    FilePrintToolStripMenuItem.Enabled = viewModel.FilePrintMenuEnabled;
                    FilePrintToolStripButton.Enabled = viewModel.FilePrintMenuEnabled;
                    break;
                case nameof(viewModel.FilePrintPreviewMenuEnabled):
                    FilePrintPreviewToolStripMenuItem.Enabled =
                        viewModel.FilePrintPreviewMenuEnabled;
                    break;
                case nameof(viewModel.FileExportMenuEnabled):
                    FileExportToolStripMenuItem.Enabled = viewModel.FileExportMenuEnabled;
                    break;
                case nameof(viewModel.EditUndoMenuEnabled):
                    EditUndoToolStripMenuItem.Enabled = viewModel.EditUndoMenuEnabled;
                    EditUndoToolStripButton.Enabled = viewModel.EditUndoMenuEnabled;
                    break;
                case nameof(viewModel.EditCutMenuEnabled):
                    EditCutToolStripMenuItem.Enabled = viewModel.EditCutMenuEnabled;
                    EditCutToolStripButton.Enabled = viewModel.EditCutMenuEnabled;
                    break;
                case nameof(viewModel.EditCopyMenuEnabled):
                    EditCopyToolStripMenuItem.Enabled = viewModel.EditCopyMenuEnabled;
                    EditCopyToolStripButton.Enabled = viewModel.EditCopyMenuEnabled;
                    break;
                case nameof(viewModel.EditPasteMenuEnabled):
                    EditPasteToolStripMenuItem.Enabled = viewModel.EditPasteMenuEnabled;
                    EditPasteToolStripButton.Enabled = viewModel.EditPasteMenuEnabled;
                    break;
                case nameof(viewModel.EditSelectAllMenuEnabled):
                    EditSelectAllToolStripMenuItem.Enabled = viewModel.EditSelectAllMenuEnabled;
                    break;
                case nameof(viewModel.EditRestoreMenuEnabled):
                    EditRestoreToolStripMenuItem.Enabled = viewModel.EditRestoreMenuEnabled;
                    EditRestoreToolStripButton.Enabled = viewModel.EditRestoreMenuEnabled;
                    break;
                case nameof(viewModel.EditAppendDateTimeMenuEnabled):
                    EditAppendDateTimeToolStripMenuItem.Enabled =
                        viewModel.EditAppendDateTimeMenuEnabled;
                    EditAppendDateTimeToolStripButton.Enabled =
                        viewModel.EditAppendDateTimeMenuEnabled;
                    break;
                case nameof(viewModel.EditAppendTextMenuEnabled):
                    EditAppendTextToolStripMenuItem.Enabled = viewModel.EditAppendTextMenuEnabled;
                    EditAppendTextToolStripButton.Enabled = viewModel.EditAppendTextMenuEnabled;
                    break;
                case nameof(viewModel.EditFlagDocumentMenuEnabled):
                    EditFlagDocumentToolStripMenuItem.Enabled =
                        viewModel.EditFlagDocumentMenuEnabled;
                    break;
                case nameof(viewModel.EditFlagDocumentMenuChecked):
                    EditFlagDocumentToolStripMenuItem.Checked =
                        viewModel.EditFlagDocumentMenuChecked;
                    break;
                case nameof(viewModel.DocumentsFindMenuEnabled):
                    DocumentsFindToolStripMenuItem.Enabled = viewModel.DocumentsFindMenuEnabled;
                    DocumentsFindToolStripButton.Enabled = viewModel.DocumentsFindMenuEnabled;
                    break;
                case nameof(viewModel.DocumentsSelectMenuEnabled):
                    DocumentsSelectToolStripMenuItem.Enabled =
                        viewModel.DocumentsSelectMenuEnabled;
                    break;
                case nameof(viewModel.DocumentsSetTitleMenuEnabled):
                    DocumentsSetTitleToolStripMenuItem.Enabled =
                        viewModel.DocumentsSetTitleMenuEnabled;
                    break;
                case nameof(viewModel.DocumentsSetAuthorMenuEnabled):
                    DocumentsSetAuthorToolStripMenuItem.Enabled =
                        viewModel.DocumentsSetAuthorMenuEnabled;
                    break;
                case nameof(viewModel.DocumentsSetSubjectMenuEnabled):
                    DocumentsSetSubjectToolStripMenuItem.Enabled =
                        viewModel.DocumentsSetSubjectMenuEnabled;
                    break;
                case nameof(viewModel.DocumentsSetCategoryMenuEnabled):
                    DocumentsSetCategoryToolStripMenuItem.Enabled =
                        viewModel.DocumentsSetCategoryMenuEnabled;
                    break;
                case nameof(viewModel.DocumentsSetTaxYearMenuEnabled):
                    DocumentsSetTaxYearToolStripMenuItem.Enabled =
                        viewModel.DocumentsSetTaxYearMenuEnabled;
                    break;
                case nameof(viewModel.DocumentsSetDateTimeAddedMenuEnabled):
                    DocumentsSetDateTimeAddedToolStripMenuItem.Enabled =
                        viewModel.DocumentsSetDateTimeAddedMenuEnabled;
                    break;
                case nameof(viewModel.DocumentsDeleteMenuEnabled):
                    DocumentsDeleteToolStripMenuItem.Enabled =
                        viewModel.DocumentsDeleteMenuEnabled;
                    DocumentsDeleteToolStripButton.Enabled =
                        viewModel.DocumentsDeleteMenuEnabled;
                    break;
                case nameof(viewModel.ViewSetPreviewPixelDensityMenuEnabled):
                    ViewSetPreviewPixelDensityToolStripMenuItem.Enabled =
                        viewModel.ViewSetPreviewPixelDensityMenuEnabled;
                    break;
                case nameof(viewModel.ViewToolBarChecked):
                    ViewToolBarToolStripMenuItem.Checked = viewModel.ViewToolBarChecked;
                    break;
                case nameof(viewModel.ViewStatusBarChecked):
                    ViewStatusBarToolStripMenuItem.Checked = viewModel.ViewStatusBarChecked;
                    break;
                case nameof(viewModel.ToolsUploadProfilesMenuEnabled):
                    ToolsUploadProfilesToolStripMenuItem.Enabled =
                        viewModel.ToolsUploadProfilesMenuEnabled;
                    ToolsUploadProfilesToolStripButton.Enabled =
                        viewModel.ToolsUploadProfilesMenuEnabled;
                    break;
                case nameof(viewModel.ToolsMoveDatabaseMenuVisible):
                    ToolsMoveDatabaseToolStripMenuItem.Visible =
                        viewModel.ToolsMoveDatabaseMenuVisible;
                    break;
                case nameof(viewModel.Documents):
                    DocumentsDataGridView.DataSource = viewModel.Documents;
                    break;
                case nameof(viewModel.CurrentDocumentId):
                    if (viewModel.CurrentDocumentId > 0)
                    {
                        foreach (DataGridViewRow row in DocumentsDataGridView.Rows)
                        {
                            if (Convert.ToInt32(
                                row.Cells[2].Value).Equals(
                                viewModel.CurrentDocumentId))
                            {
                                row.Selected = true;

                                try
                                {
                                    DocumentsDataGridView.FirstDisplayedScrollingRowIndex =
                                        dataGridViewScrollPosition;
                                }
                                catch (ArgumentOutOfRangeException) { }
                            }
                        }
                    }

                    break;
                case nameof(viewModel.Notes):
                    NotesTextBox.Text = NotesTextBox.Text.TrimStart();
                    viewModel.NotesTextChangedCommand.Execute(NotesTextBox.CanUndo);
                    break;
                case nameof(viewModel.SearchTermSnippetsVisible):
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

                    break;
                case nameof(viewModel.DocumentsProgressBarVisible):
                    DocumentsProgressBar.Visible = viewModel.DocumentsProgressBarVisible;
                    break;
                case nameof(viewModel.DocumentsProgressBarMinimum):
                    DocumentsProgressBar.Minimum = viewModel.DocumentsProgressBarMinimum;
                    break;
                case nameof(viewModel.DocumentsProgressBarMaximum):
                    DocumentsProgressBar.Maximum = viewModel.DocumentsProgressBarMaximum;
                    break;
                case nameof(viewModel.UploadProgressBarVisible):
                    BeginInvoke((MethodInvoker)delegate ()
                    {
                        UploadProgressBar.Visible = viewModel.UploadProgressBarVisible;
                    });

                    break;
                case nameof(viewModel.RefreshingDocumentsImageVisible):
                    RefreshingDocumentsImageLabel.Visible =
                        viewModel.RefreshingDocumentsImageVisible;
                    Application.DoEvents();
                    break;
                case nameof(viewModel.FlagImageVisible):
                    FlagImageLabel.Visible = viewModel.FlagImageVisible;
                    Application.DoEvents();
                    break;
                case nameof(viewModel.UploadRejectedImageVisible):
                    UploadRejectedImageLabel.Visible = viewModel.UploadRejectedImageVisible;
                    Application.DoEvents();
                    break;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            new FormClosingCommand(e, viewModel).Execute(null);
        }

        /// <summary>
        /// Gets the <see cref="StartupAction"/> that needs to be performed.
        /// </summary>
        /// <returns>The <see cref="StartupAction"/>.</returns>
        private static StartupAction GetStartupAction()
        {
            var startupAction = StartupAction.None;

            if (Settings.Default.FindFlaggedDocumentsOnStartup)
            {
                startupAction = StartupAction.FindFlaggedDocuments;
            }
            else if (Settings.Default.ShowAllDocumentsOnStartup &&
                DatabaseSession.PlatformName.Equals(
                    DatabaseSession.CompatiblePlatformName.Sqlite))
            {
                startupAction = StartupAction.ShowAllDocuments;
            }

            return startupAction;
        }

        /// <summary>
        /// Gets a <see cref="Collection{T}"/> of checked document ID's in
        /// <see cref="MainForm.DocumentsDataGridView"/>. 
        /// </summary>
        /// <returns>The <see cref="Collection{T}"/> of checked document ID's.</returns>
        private Collection<int> GetCheckedDocumentIds()
        {
            var ids = new Collection<int>();

            try
            {
                foreach (DataGridViewRow row in DocumentsDataGridView.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        ids.Add(Convert.ToInt32(row.Cells[2].Value));
                    }
                }
            }
            catch (NullReferenceException) { }
            
            return ids;
        }

        /// <summary>
        /// Gets the <see cref="TextBox"/> on <see cref="MainForm"/> with focus.
        /// </summary>
        /// <returns>The <see cref="TextBox"/> with focus.</returns>
        private TextBox GetTextBoxWithFocus()
        {
            TextBox result = null;
            
            if (NotesTextBox.Focused)
            {
                result = NotesTextBox;
            }
            else if (KeywordsTextBox.Focused)
            {
                result = KeywordsTextBox;
            }
            else if (TextTextBox.Focused)
            {
                result = TextTextBox;
            }
            else if (SearchTermSnippetsTextBox.Focused)
            {
                result = SearchTermSnippetsTextBox;
            }
            
            return result;
        }
    }
}
