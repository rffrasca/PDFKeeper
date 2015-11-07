﻿'******************************************************************************
'*
'* PDFKeeper -- PDF Document Capture, Storage, and Search
'* Copyright (C) 2009-2015 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify it under
'* the terms of the GNU General Public License as published by the Free
'* Software Foundation, either version 3 of the License, or (at your option)
'* any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
'* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
'* FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
'* more details.
'*
'* You should have received a copy of the GNU General Public License along
'* with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'*
'******************************************************************************

Partial Class MainForm
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.menuStrip = New System.Windows.Forms.MenuStrip()
		Me.toolStripMenuItemFile = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemSavePDFtoDisk = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
		Me.toolStripMenuItemPrintDocumentNotes = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
		Me.toolStripMenuItemExit = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemEdit = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemInsertDateTimeIntoDocumentNotes = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
		Me.toolStripMenuItemCheckAll = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemUncheckAll = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemDeleteCheckedDocuments = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemView = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemRefresh = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemTools = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemCaptureFolder = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemDirectUploadFolder = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
		Me.toolStripMenuItemDirectUploadConfig = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemHelp = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripMenuItemContents = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
		Me.toolStripMenuItemCheckNewerVersion = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
		Me.toolStripMenuItemReportNewIssue = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
		Me.toolStripMenuItemAbout = New System.Windows.Forms.ToolStripMenuItem()
		Me.labelSearchText = New System.Windows.Forms.Label()
		Me.errorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
		Me.buttonSearch = New System.Windows.Forms.Button()
		Me.statusStrip = New System.Windows.Forms.StatusStrip()
		Me.toolStripStatusLabelMessage = New System.Windows.Forms.ToolStripStatusLabel()
		Me.toolStripStatusLabelUpdateStatus = New System.Windows.Forms.ToolStripStatusLabel()
		Me.toolStripStatusLabelUploading = New System.Windows.Forms.ToolStripStatusLabel()
		Me.toolStripStatusLabelCaptured = New System.Windows.Forms.ToolStripStatusLabel()
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog()
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog()
		Me.backgroundWorkerUpdateCheck = New System.ComponentModel.BackgroundWorker()
		Me.comboBoxSearchText = New System.Windows.Forms.ComboBox()
		Me.imageList = New System.Windows.Forms.ImageList(Me.components)
		Me.printDocumentNotes = New System.Drawing.Printing.PrintDocument()
		Me.printDialog = New System.Windows.Forms.PrintDialog()
		Me.processExplorer = New System.Diagnostics.Process()
		Me.processSearchPdfViewer = New System.Diagnostics.Process()
		Me.folderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
		Me.fileSystemWatcherDocumentCapture = New System.IO.FileSystemWatcher()
		Me.timerCaptureCheck = New System.Windows.Forms.Timer(Me.components)
		Me.tabControlMain = New System.Windows.Forms.TabControl()
		Me.tabPageSearch = New System.Windows.Forms.TabPage()
		Me.tabControlDocNotesKeywords = New System.Windows.Forms.TabControl()
		Me.tabPageDocumentNotes = New System.Windows.Forms.TabPage()
		Me.buttonDocumentNotesRevert = New System.Windows.Forms.Button()
		Me.buttonDocumentNotesUpdate = New System.Windows.Forms.Button()
		Me.textBoxDocumentNotes = New System.Windows.Forms.TextBox()
		Me.tabPageDocumentKeywords = New System.Windows.Forms.TabPage()
		Me.textBoxDocumentKeywords = New System.Windows.Forms.TextBox()
		Me.listViewDocs = New System.Windows.Forms.ListView()
		Me.columnHeaderID = New System.Windows.Forms.ColumnHeader(CType(resources.GetObject("listViewDocs.Columns"),Integer))
		Me.columnHeaderTitle = New System.Windows.Forms.ColumnHeader()
		Me.columnHeaderAuthor = New System.Windows.Forms.ColumnHeader()
		Me.columnHeaderSubject = New System.Windows.Forms.ColumnHeader()
		Me.columnHeaderAdded = New System.Windows.Forms.ColumnHeader()
		Me.tabPagePreview = New System.Windows.Forms.TabPage()
		Me.checkBoxDoNotResetZoomLevel = New System.Windows.Forms.CheckBox()
		Me.buttonZoomOut = New System.Windows.Forms.Button()
		Me.buttonZoomIn = New System.Windows.Forms.Button()
		Me.panelPreview = New System.Windows.Forms.Panel()
		Me.pictureBoxPreview = New System.Windows.Forms.PictureBox()
		Me.buttonPreviewNext = New System.Windows.Forms.Button()
		Me.buttonPreviewPrevious = New System.Windows.Forms.Button()
		Me.tabPageTextOnlyView = New System.Windows.Forms.TabPage()
		Me.textBoxTextOnlyView = New System.Windows.Forms.TextBox()
		Me.buttonTextOnlyNext = New System.Windows.Forms.Button()
		Me.buttonTextOnlyPrevious = New System.Windows.Forms.Button()
		Me.tabPageCapture = New System.Windows.Forms.TabPage()
		Me.groupBoxProperties = New System.Windows.Forms.GroupBox()
		Me.buttonSetToFileName = New System.Windows.Forms.Button()
		Me.textBoxKeywords = New System.Windows.Forms.TextBox()
		Me.buttonUpload = New System.Windows.Forms.Button()
		Me.labelKeywords = New System.Windows.Forms.Label()
		Me.buttonView = New System.Windows.Forms.Button()
		Me.comboBoxSubject = New System.Windows.Forms.ComboBox()
		Me.buttonDeselect = New System.Windows.Forms.Button()
		Me.buttonSave = New System.Windows.Forms.Button()
		Me.labelSubject = New System.Windows.Forms.Label()
		Me.comboBoxAuthor = New System.Windows.Forms.ComboBox()
		Me.labelAuthor = New System.Windows.Forms.Label()
		Me.textBoxTitle = New System.Windows.Forms.TextBox()
		Me.labelTitle = New System.Windows.Forms.Label()
		Me.groupBoxDocument = New System.Windows.Forms.GroupBox()
		Me.buttonViewOriginal = New System.Windows.Forms.Button()
		Me.textBoxPdfDocument = New System.Windows.Forms.TextBox()
		Me.groupBoxDocuments = New System.Windows.Forms.GroupBox()
		Me.buttonRename = New System.Windows.Forms.Button()
		Me.buttonDelete = New System.Windows.Forms.Button()
		Me.listBoxDocCaptureQueue = New System.Windows.Forms.ListBox()
		Me.processCapturePdfViewer = New System.Diagnostics.Process()
		Me.timerDirectUpload = New System.Windows.Forms.Timer(Me.components)
		Me.backgroundWorkerDirectUpload = New System.ComponentModel.BackgroundWorker()
		Me.menuStrip.SuspendLayout
		CType(Me.errorProvider,System.ComponentModel.ISupportInitialize).BeginInit
		Me.statusStrip.SuspendLayout
		CType(Me.fileSystemWatcherDocumentCapture,System.ComponentModel.ISupportInitialize).BeginInit
		Me.tabControlMain.SuspendLayout
		Me.tabPageSearch.SuspendLayout
		Me.tabControlDocNotesKeywords.SuspendLayout
		Me.tabPageDocumentNotes.SuspendLayout
		Me.tabPageDocumentKeywords.SuspendLayout
		Me.tabPagePreview.SuspendLayout
		Me.panelPreview.SuspendLayout
		CType(Me.pictureBoxPreview,System.ComponentModel.ISupportInitialize).BeginInit
		Me.tabPageTextOnlyView.SuspendLayout
		Me.tabPageCapture.SuspendLayout
		Me.groupBoxProperties.SuspendLayout
		Me.groupBoxDocument.SuspendLayout
		Me.groupBoxDocuments.SuspendLayout
		Me.SuspendLayout
		'
		'menuStrip
		'
		Me.menuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripMenuItemFile, Me.toolStripMenuItemEdit, Me.toolStripMenuItemView, Me.toolStripMenuItemTools, Me.toolStripMenuItemHelp})
		resources.ApplyResources(Me.menuStrip, "menuStrip")
		Me.menuStrip.Name = "menuStrip"
		'
		'toolStripMenuItemFile
		'
		Me.toolStripMenuItemFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripMenuItemSavePDFtoDisk, Me.toolStripSeparator1, Me.toolStripMenuItemPrintDocumentNotes, Me.toolStripSeparator2, Me.toolStripMenuItemExit})
		Me.toolStripMenuItemFile.Name = "toolStripMenuItemFile"
		resources.ApplyResources(Me.toolStripMenuItemFile, "toolStripMenuItemFile")
		'
		'toolStripMenuItemSavePDFtoDisk
		'
		resources.ApplyResources(Me.toolStripMenuItemSavePDFtoDisk, "toolStripMenuItemSavePDFtoDisk")
		Me.toolStripMenuItemSavePDFtoDisk.Name = "toolStripMenuItemSavePDFtoDisk"
		AddHandler Me.toolStripMenuItemSavePDFtoDisk.Click, AddressOf Me.ToolStripMenuItemSavePdfToDiskClick
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		resources.ApplyResources(Me.toolStripSeparator1, "toolStripSeparator1")
		'
		'toolStripMenuItemPrintDocumentNotes
		'
		resources.ApplyResources(Me.toolStripMenuItemPrintDocumentNotes, "toolStripMenuItemPrintDocumentNotes")
		Me.toolStripMenuItemPrintDocumentNotes.Name = "toolStripMenuItemPrintDocumentNotes"
		AddHandler Me.toolStripMenuItemPrintDocumentNotes.Click, AddressOf Me.ToolStripMenuItemPrintDocumentNotesClick
		'
		'toolStripSeparator2
		'
		Me.toolStripSeparator2.Name = "toolStripSeparator2"
		resources.ApplyResources(Me.toolStripSeparator2, "toolStripSeparator2")
		'
		'toolStripMenuItemExit
		'
		Me.toolStripMenuItemExit.Name = "toolStripMenuItemExit"
		resources.ApplyResources(Me.toolStripMenuItemExit, "toolStripMenuItemExit")
		AddHandler Me.toolStripMenuItemExit.Click, AddressOf Me.ToolStripMenuItemExitClick
		'
		'toolStripMenuItemEdit
		'
		Me.toolStripMenuItemEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripMenuItemInsertDateTimeIntoDocumentNotes, Me.toolStripSeparator4, Me.toolStripMenuItemCheckAll, Me.toolStripMenuItemUncheckAll, Me.toolStripMenuItemDeleteCheckedDocuments})
		Me.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit"
		resources.ApplyResources(Me.toolStripMenuItemEdit, "toolStripMenuItemEdit")
		'
		'toolStripMenuItemInsertDateTimeIntoDocumentNotes
		'
		resources.ApplyResources(Me.toolStripMenuItemInsertDateTimeIntoDocumentNotes, "toolStripMenuItemInsertDateTimeIntoDocumentNotes")
		Me.toolStripMenuItemInsertDateTimeIntoDocumentNotes.Name = "toolStripMenuItemInsertDateTimeIntoDocumentNotes"
		AddHandler Me.toolStripMenuItemInsertDateTimeIntoDocumentNotes.Click, AddressOf Me.ToolStripMenuItemInsertDateTimeIntoDocumentNotesClick
		'
		'toolStripSeparator4
		'
		Me.toolStripSeparator4.Name = "toolStripSeparator4"
		resources.ApplyResources(Me.toolStripSeparator4, "toolStripSeparator4")
		'
		'toolStripMenuItemCheckAll
		'
		resources.ApplyResources(Me.toolStripMenuItemCheckAll, "toolStripMenuItemCheckAll")
		Me.toolStripMenuItemCheckAll.Name = "toolStripMenuItemCheckAll"
		AddHandler Me.toolStripMenuItemCheckAll.Click, AddressOf Me.ToolStripMenuItemCheckAllClick
		'
		'toolStripMenuItemUncheckAll
		'
		resources.ApplyResources(Me.toolStripMenuItemUncheckAll, "toolStripMenuItemUncheckAll")
		Me.toolStripMenuItemUncheckAll.Name = "toolStripMenuItemUncheckAll"
		AddHandler Me.toolStripMenuItemUncheckAll.Click, AddressOf Me.ToolStripMenuItemUncheckAllClick
		'
		'toolStripMenuItemDeleteCheckedDocuments
		'
		resources.ApplyResources(Me.toolStripMenuItemDeleteCheckedDocuments, "toolStripMenuItemDeleteCheckedDocuments")
		Me.toolStripMenuItemDeleteCheckedDocuments.Name = "toolStripMenuItemDeleteCheckedDocuments"
		AddHandler Me.toolStripMenuItemDeleteCheckedDocuments.Click, AddressOf Me.ToolStripMenuItemDeleteCheckedDocumentsClick
		'
		'toolStripMenuItemView
		'
		Me.toolStripMenuItemView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripMenuItemRefresh})
		Me.toolStripMenuItemView.Name = "toolStripMenuItemView"
		resources.ApplyResources(Me.toolStripMenuItemView, "toolStripMenuItemView")
		'
		'toolStripMenuItemRefresh
		'
		resources.ApplyResources(Me.toolStripMenuItemRefresh, "toolStripMenuItemRefresh")
		Me.toolStripMenuItemRefresh.Name = "toolStripMenuItemRefresh"
		AddHandler Me.toolStripMenuItemRefresh.Click, AddressOf Me.ButtonSearchClick
		'
		'toolStripMenuItemTools
		'
		Me.toolStripMenuItemTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripMenuItemCaptureFolder, Me.toolStripMenuItemDirectUploadFolder, Me.toolStripSeparator8, Me.toolStripMenuItemDirectUploadConfig})
		Me.toolStripMenuItemTools.Name = "toolStripMenuItemTools"
		resources.ApplyResources(Me.toolStripMenuItemTools, "toolStripMenuItemTools")
		'
		'toolStripMenuItemCaptureFolder
		'
		resources.ApplyResources(Me.toolStripMenuItemCaptureFolder, "toolStripMenuItemCaptureFolder")
		Me.toolStripMenuItemCaptureFolder.Name = "toolStripMenuItemCaptureFolder"
		AddHandler Me.toolStripMenuItemCaptureFolder.Click, AddressOf Me.ToolStripMenuItemCaptureFolderClick
		'
		'toolStripMenuItemDirectUploadFolder
		'
		resources.ApplyResources(Me.toolStripMenuItemDirectUploadFolder, "toolStripMenuItemDirectUploadFolder")
		Me.toolStripMenuItemDirectUploadFolder.Name = "toolStripMenuItemDirectUploadFolder"
		AddHandler Me.toolStripMenuItemDirectUploadFolder.Click, AddressOf Me.ToolStripMenuItemDirectUploadFolderClick
		'
		'toolStripSeparator8
		'
		Me.toolStripSeparator8.Name = "toolStripSeparator8"
		resources.ApplyResources(Me.toolStripSeparator8, "toolStripSeparator8")
		'
		'toolStripMenuItemDirectUploadConfig
		'
		resources.ApplyResources(Me.toolStripMenuItemDirectUploadConfig, "toolStripMenuItemDirectUploadConfig")
		Me.toolStripMenuItemDirectUploadConfig.Name = "toolStripMenuItemDirectUploadConfig"
		AddHandler Me.toolStripMenuItemDirectUploadConfig.Click, AddressOf Me.ToolStripMenuItemDirectUploadConfigClick
		'
		'toolStripMenuItemHelp
		'
		Me.toolStripMenuItemHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripMenuItemContents, Me.toolStripSeparator3, Me.toolStripMenuItemCheckNewerVersion, Me.toolStripSeparator6, Me.toolStripMenuItemReportNewIssue, Me.toolStripSeparator5, Me.toolStripMenuItemAbout})
		Me.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp"
		resources.ApplyResources(Me.toolStripMenuItemHelp, "toolStripMenuItemHelp")
		AddHandler Me.toolStripMenuItemHelp.Click, AddressOf Me.ToolStripMenuItemHelpClick
		'
		'toolStripMenuItemContents
		'
		resources.ApplyResources(Me.toolStripMenuItemContents, "toolStripMenuItemContents")
		Me.toolStripMenuItemContents.Name = "toolStripMenuItemContents"
		AddHandler Me.toolStripMenuItemContents.Click, AddressOf Me.ToolStripMenuItemContentsClick
		'
		'toolStripSeparator3
		'
		Me.toolStripSeparator3.Name = "toolStripSeparator3"
		resources.ApplyResources(Me.toolStripSeparator3, "toolStripSeparator3")
		'
		'toolStripMenuItemCheckNewerVersion
		'
		Me.toolStripMenuItemCheckNewerVersion.Checked = true
		Me.toolStripMenuItemCheckNewerVersion.CheckState = System.Windows.Forms.CheckState.Checked
		Me.toolStripMenuItemCheckNewerVersion.Name = "toolStripMenuItemCheckNewerVersion"
		resources.ApplyResources(Me.toolStripMenuItemCheckNewerVersion, "toolStripMenuItemCheckNewerVersion")
		AddHandler Me.toolStripMenuItemCheckNewerVersion.Click, AddressOf Me.ToolStripMenuItemCheckNewerVersionClick
		'
		'toolStripSeparator6
		'
		Me.toolStripSeparator6.Name = "toolStripSeparator6"
		resources.ApplyResources(Me.toolStripSeparator6, "toolStripSeparator6")
		'
		'toolStripMenuItemReportNewIssue
		'
		Me.toolStripMenuItemReportNewIssue.Name = "toolStripMenuItemReportNewIssue"
		resources.ApplyResources(Me.toolStripMenuItemReportNewIssue, "toolStripMenuItemReportNewIssue")
		AddHandler Me.toolStripMenuItemReportNewIssue.Click, AddressOf Me.ToolStripMenuItemReportNewIssueClick
		'
		'toolStripSeparator5
		'
		Me.toolStripSeparator5.Name = "toolStripSeparator5"
		resources.ApplyResources(Me.toolStripSeparator5, "toolStripSeparator5")
		'
		'toolStripMenuItemAbout
		'
		Me.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout"
		resources.ApplyResources(Me.toolStripMenuItemAbout, "toolStripMenuItemAbout")
		AddHandler Me.toolStripMenuItemAbout.Click, AddressOf Me.ToolStripMenuItemAboutClick
		'
		'labelSearchText
		'
		resources.ApplyResources(Me.labelSearchText, "labelSearchText")
		Me.labelSearchText.Name = "labelSearchText"
		'
		'errorProvider
		'
		Me.errorProvider.ContainerControl = Me
		'
		'buttonSearch
		'
		resources.ApplyResources(Me.buttonSearch, "buttonSearch")
		Me.buttonSearch.Name = "buttonSearch"
		Me.buttonSearch.UseVisualStyleBackColor = true
		AddHandler Me.buttonSearch.Click, AddressOf Me.ButtonSearchClick
		'
		'statusStrip
		'
		Me.statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripStatusLabelMessage, Me.toolStripStatusLabelUpdateStatus, Me.toolStripStatusLabelUploading, Me.toolStripStatusLabelCaptured})
		resources.ApplyResources(Me.statusStrip, "statusStrip")
		Me.statusStrip.Name = "statusStrip"
		Me.statusStrip.ShowItemToolTips = true
		AddHandler Me.statusStrip.ItemClicked, AddressOf Me.StatusStripItemClicked
		'
		'toolStripStatusLabelMessage
		'
		Me.toolStripStatusLabelMessage.Name = "toolStripStatusLabelMessage"
		resources.ApplyResources(Me.toolStripStatusLabelMessage, "toolStripStatusLabelMessage")
		'
		'toolStripStatusLabelUpdateStatus
		'
		Me.toolStripStatusLabelUpdateStatus.Name = "toolStripStatusLabelUpdateStatus"
		resources.ApplyResources(Me.toolStripStatusLabelUpdateStatus, "toolStripStatusLabelUpdateStatus")
		Me.toolStripStatusLabelUpdateStatus.Spring = true
		'
		'toolStripStatusLabelUploading
		'
		Me.toolStripStatusLabelUploading.AutoToolTip = true
		resources.ApplyResources(Me.toolStripStatusLabelUploading, "toolStripStatusLabelUploading")
		Me.toolStripStatusLabelUploading.Name = "toolStripStatusLabelUploading"
		'
		'toolStripStatusLabelCaptured
		'
		Me.toolStripStatusLabelCaptured.AutoToolTip = true
		resources.ApplyResources(Me.toolStripStatusLabelCaptured, "toolStripStatusLabelCaptured")
		Me.toolStripStatusLabelCaptured.Name = "toolStripStatusLabelCaptured"
		AddHandler Me.toolStripStatusLabelCaptured.Click, AddressOf Me.ToolStripStatusLabelCapturedClick
		'
		'saveFileDialog
		'
		Me.saveFileDialog.DefaultExt = "pdf"
		resources.ApplyResources(Me.saveFileDialog, "saveFileDialog")
		Me.saveFileDialog.RestoreDirectory = true
		'
		'openFileDialog
		'
		Me.openFileDialog.DefaultExt = "html"
		resources.ApplyResources(Me.openFileDialog, "openFileDialog")
		Me.openFileDialog.Multiselect = true
		Me.openFileDialog.RestoreDirectory = true
		'
		'backgroundWorkerUpdateCheck
		'
		AddHandler Me.backgroundWorkerUpdateCheck.DoWork, AddressOf Me.BackgroundWorkerUpdateCheckDoWork
		AddHandler Me.backgroundWorkerUpdateCheck.RunWorkerCompleted, AddressOf Me.BackgroundWorkerUpdateCheckRunWorkerCompleted
		'
		'comboBoxSearchText
		'
		resources.ApplyResources(Me.comboBoxSearchText, "comboBoxSearchText")
		Me.comboBoxSearchText.FormattingEnabled = true
		Me.comboBoxSearchText.Name = "comboBoxSearchText"
		Me.comboBoxSearchText.Sorted = true
		AddHandler Me.comboBoxSearchText.DropDown, AddressOf Me.ComboBoxSearchTextDropDown
		AddHandler Me.comboBoxSearchText.TextChanged, AddressOf Me.ComboBoxSearchTextTextChanged
		'
		'imageList
		'
		Me.imageList.ImageStream = CType(resources.GetObject("imageList.ImageStream"),System.Windows.Forms.ImageListStreamer)
		Me.imageList.TransparentColor = System.Drawing.Color.Transparent
		Me.imageList.Images.SetKeyName(0, "page_find.gif")
		Me.imageList.Images.SetKeyName(1, "page.gif")
		Me.imageList.Images.SetKeyName(2, "page_text.gif")
		Me.imageList.Images.SetKeyName(3, "file_acrobat.gif")
		Me.imageList.Images.SetKeyName(4, "page_edit.gif")
		Me.imageList.Images.SetKeyName(5, "page_key.gif")
		'
		'printDocumentNotes
		'
		AddHandler Me.printDocumentNotes.PrintPage, AddressOf Me.PrintDocumentNotesPrintPage
		'
		'printDialog
		'
		Me.printDialog.Document = Me.printDocumentNotes
		Me.printDialog.UseEXDialog = true
		'
		'processExplorer
		'
		Me.processExplorer.StartInfo.Domain = ""
		Me.processExplorer.StartInfo.ErrorDialog = true
		Me.processExplorer.StartInfo.LoadUserProfile = false
		Me.processExplorer.StartInfo.Password = Nothing
		Me.processExplorer.StartInfo.StandardErrorEncoding = Nothing
		Me.processExplorer.StartInfo.StandardOutputEncoding = Nothing
		Me.processExplorer.StartInfo.UserName = ""
		Me.processExplorer.SynchronizingObject = Me
		'
		'processSearchPdfViewer
		'
		Me.processSearchPdfViewer.StartInfo.Domain = ""
		Me.processSearchPdfViewer.StartInfo.ErrorDialog = true
		Me.processSearchPdfViewer.StartInfo.FileName = "SumatraPDF.exe"
		Me.processSearchPdfViewer.StartInfo.LoadUserProfile = false
		Me.processSearchPdfViewer.StartInfo.Password = Nothing
		Me.processSearchPdfViewer.StartInfo.StandardErrorEncoding = Nothing
		Me.processSearchPdfViewer.StartInfo.StandardOutputEncoding = Nothing
		Me.processSearchPdfViewer.StartInfo.UserName = ""
		Me.processSearchPdfViewer.StartInfo.UseShellExecute = false
		Me.processSearchPdfViewer.SynchronizingObject = Me
		'
		'folderBrowserDialog
		'
		Me.folderBrowserDialog.ShowNewFolderButton = false
		'
		'fileSystemWatcherDocumentCapture
		'
		Me.fileSystemWatcherDocumentCapture.EnableRaisingEvents = true
		Me.fileSystemWatcherDocumentCapture.IncludeSubdirectories = true
		Me.fileSystemWatcherDocumentCapture.NotifyFilter = System.IO.NotifyFilters.FileName
		Me.fileSystemWatcherDocumentCapture.SynchronizingObject = Me
		AddHandler Me.fileSystemWatcherDocumentCapture.Created, AddressOf Me.FileSystemWatcherDocumentCaptureCreated
		AddHandler Me.fileSystemWatcherDocumentCapture.Deleted, AddressOf Me.FileSystemWatcherDocumentCaptureDeleted
		AddHandler Me.fileSystemWatcherDocumentCapture.[Error], AddressOf Me.FileSystemWatcherDocumentCaptureError
		AddHandler Me.fileSystemWatcherDocumentCapture.Renamed, AddressOf Me.FileSystemWatcherDocumentCaptureRenamed
		'
		'timerCaptureCheck
		'
		Me.timerCaptureCheck.Enabled = true
		Me.timerCaptureCheck.Interval = 5000
		AddHandler Me.timerCaptureCheck.Tick, AddressOf Me.TimerCaptureCheckTick
		'
		'tabControlMain
		'
		resources.ApplyResources(Me.tabControlMain, "tabControlMain")
		Me.tabControlMain.Controls.Add(Me.tabPageSearch)
		Me.tabControlMain.Controls.Add(Me.tabPagePreview)
		Me.tabControlMain.Controls.Add(Me.tabPageTextOnlyView)
		Me.tabControlMain.Controls.Add(Me.tabPageCapture)
		Me.tabControlMain.ImageList = Me.imageList
		Me.tabControlMain.Name = "tabControlMain"
		Me.tabControlMain.SelectedIndex = 0
		AddHandler Me.tabControlMain.SelectedIndexChanged, AddressOf Me.TabControlMainSelectedIndexChanged
		'
		'tabPageSearch
		'
		Me.tabPageSearch.Controls.Add(Me.tabControlDocNotesKeywords)
		Me.tabPageSearch.Controls.Add(Me.listViewDocs)
		Me.tabPageSearch.Controls.Add(Me.comboBoxSearchText)
		Me.tabPageSearch.Controls.Add(Me.labelSearchText)
		Me.tabPageSearch.Controls.Add(Me.buttonSearch)
		resources.ApplyResources(Me.tabPageSearch, "tabPageSearch")
		Me.tabPageSearch.Name = "tabPageSearch"
		Me.tabPageSearch.UseVisualStyleBackColor = true
		'
		'tabControlDocNotesKeywords
		'
		Me.tabControlDocNotesKeywords.Controls.Add(Me.tabPageDocumentNotes)
		Me.tabControlDocNotesKeywords.Controls.Add(Me.tabPageDocumentKeywords)
		resources.ApplyResources(Me.tabControlDocNotesKeywords, "tabControlDocNotesKeywords")
		Me.tabControlDocNotesKeywords.ImageList = Me.imageList
		Me.tabControlDocNotesKeywords.Name = "tabControlDocNotesKeywords"
		Me.tabControlDocNotesKeywords.SelectedIndex = 0
		'
		'tabPageDocumentNotes
		'
		resources.ApplyResources(Me.tabPageDocumentNotes, "tabPageDocumentNotes")
		Me.tabPageDocumentNotes.Controls.Add(Me.buttonDocumentNotesRevert)
		Me.tabPageDocumentNotes.Controls.Add(Me.buttonDocumentNotesUpdate)
		Me.tabPageDocumentNotes.Controls.Add(Me.textBoxDocumentNotes)
		Me.tabPageDocumentNotes.Name = "tabPageDocumentNotes"
		Me.tabPageDocumentNotes.UseVisualStyleBackColor = true
		'
		'buttonDocumentNotesRevert
		'
		resources.ApplyResources(Me.buttonDocumentNotesRevert, "buttonDocumentNotesRevert")
		Me.buttonDocumentNotesRevert.Name = "buttonDocumentNotesRevert"
		Me.buttonDocumentNotesRevert.UseVisualStyleBackColor = true
		AddHandler Me.buttonDocumentNotesRevert.Click, AddressOf Me.ButtonDocumentNotesRevertClick
		'
		'buttonDocumentNotesUpdate
		'
		resources.ApplyResources(Me.buttonDocumentNotesUpdate, "buttonDocumentNotesUpdate")
		Me.buttonDocumentNotesUpdate.Name = "buttonDocumentNotesUpdate"
		Me.buttonDocumentNotesUpdate.UseVisualStyleBackColor = true
		AddHandler Me.buttonDocumentNotesUpdate.Click, AddressOf Me.ButtonDocumentNotesUpdateClick
		'
		'textBoxDocumentNotes
		'
		Me.textBoxDocumentNotes.AcceptsReturn = true
		resources.ApplyResources(Me.textBoxDocumentNotes, "textBoxDocumentNotes")
		Me.textBoxDocumentNotes.Name = "textBoxDocumentNotes"
		AddHandler Me.textBoxDocumentNotes.TextChanged, AddressOf Me.TextBoxDocumentNotesTextChanged
		'
		'tabPageDocumentKeywords
		'
		resources.ApplyResources(Me.tabPageDocumentKeywords, "tabPageDocumentKeywords")
		Me.tabPageDocumentKeywords.Controls.Add(Me.textBoxDocumentKeywords)
		Me.tabPageDocumentKeywords.Name = "tabPageDocumentKeywords"
		Me.tabPageDocumentKeywords.UseVisualStyleBackColor = true
		'
		'textBoxDocumentKeywords
		'
		resources.ApplyResources(Me.textBoxDocumentKeywords, "textBoxDocumentKeywords")
		Me.textBoxDocumentKeywords.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.textBoxDocumentKeywords.Name = "textBoxDocumentKeywords"
		Me.textBoxDocumentKeywords.ReadOnly = true
		Me.textBoxDocumentKeywords.TabStop = false
		'
		'listViewDocs
		'
		resources.ApplyResources(Me.listViewDocs, "listViewDocs")
		Me.listViewDocs.BackColor = System.Drawing.SystemColors.Window
		Me.listViewDocs.CheckBoxes = true
		Me.listViewDocs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeaderID, Me.columnHeaderTitle, Me.columnHeaderAuthor, Me.columnHeaderSubject, Me.columnHeaderAdded})
		Me.listViewDocs.FullRowSelect = true
		Me.listViewDocs.GridLines = true
		Me.listViewDocs.HideSelection = false
		Me.listViewDocs.MultiSelect = false
		Me.listViewDocs.Name = "listViewDocs"
		Me.listViewDocs.UseCompatibleStateImageBehavior = false
		Me.listViewDocs.View = System.Windows.Forms.View.Details
		AddHandler Me.listViewDocs.ColumnClick, AddressOf Me.ListViewDocsColumnClick
		AddHandler Me.listViewDocs.ItemChecked, AddressOf Me.ListViewDocsItemChecked
		AddHandler Me.listViewDocs.SelectedIndexChanged, AddressOf Me.ListViewDocsSelectedIndexChanged
		AddHandler Me.listViewDocs.MouseDoubleClick, AddressOf Me.ListViewDocsMouseDoubleClick
		'
		'columnHeaderID
		'
		resources.ApplyResources(Me.columnHeaderID, "columnHeaderID")
		'
		'columnHeaderTitle
		'
		resources.ApplyResources(Me.columnHeaderTitle, "columnHeaderTitle")
		'
		'columnHeaderAuthor
		'
		resources.ApplyResources(Me.columnHeaderAuthor, "columnHeaderAuthor")
		'
		'columnHeaderSubject
		'
		resources.ApplyResources(Me.columnHeaderSubject, "columnHeaderSubject")
		'
		'columnHeaderAdded
		'
		resources.ApplyResources(Me.columnHeaderAdded, "columnHeaderAdded")
		'
		'tabPagePreview
		'
		Me.tabPagePreview.Controls.Add(Me.checkBoxDoNotResetZoomLevel)
		Me.tabPagePreview.Controls.Add(Me.buttonZoomOut)
		Me.tabPagePreview.Controls.Add(Me.buttonZoomIn)
		Me.tabPagePreview.Controls.Add(Me.panelPreview)
		Me.tabPagePreview.Controls.Add(Me.buttonPreviewNext)
		Me.tabPagePreview.Controls.Add(Me.buttonPreviewPrevious)
		resources.ApplyResources(Me.tabPagePreview, "tabPagePreview")
		Me.tabPagePreview.Name = "tabPagePreview"
		Me.tabPagePreview.UseVisualStyleBackColor = true
		'
		'checkBoxDoNotResetZoomLevel
		'
		resources.ApplyResources(Me.checkBoxDoNotResetZoomLevel, "checkBoxDoNotResetZoomLevel")
		Me.checkBoxDoNotResetZoomLevel.Name = "checkBoxDoNotResetZoomLevel"
		Me.checkBoxDoNotResetZoomLevel.UseVisualStyleBackColor = true
		AddHandler Me.checkBoxDoNotResetZoomLevel.CheckedChanged, AddressOf Me.CheckBoxDoNotResetZoomLevelCheckedChanged
		'
		'buttonZoomOut
		'
		resources.ApplyResources(Me.buttonZoomOut, "buttonZoomOut")
		Me.buttonZoomOut.Name = "buttonZoomOut"
		Me.buttonZoomOut.UseVisualStyleBackColor = true
		AddHandler Me.buttonZoomOut.Click, AddressOf Me.ButtonZoomOutClick
		'
		'buttonZoomIn
		'
		resources.ApplyResources(Me.buttonZoomIn, "buttonZoomIn")
		Me.buttonZoomIn.Name = "buttonZoomIn"
		Me.buttonZoomIn.UseVisualStyleBackColor = true
		AddHandler Me.buttonZoomIn.Click, AddressOf Me.ButtonZoomInClick
		'
		'panelPreview
		'
		resources.ApplyResources(Me.panelPreview, "panelPreview")
		Me.panelPreview.Controls.Add(Me.pictureBoxPreview)
		Me.panelPreview.Name = "panelPreview"
		'
		'pictureBoxPreview
		'
		resources.ApplyResources(Me.pictureBoxPreview, "pictureBoxPreview")
		Me.pictureBoxPreview.Name = "pictureBoxPreview"
		Me.pictureBoxPreview.TabStop = false
		AddHandler Me.pictureBoxPreview.MouseDoubleClick, AddressOf Me.ListViewDocsMouseDoubleClick
		'
		'buttonPreviewNext
		'
		resources.ApplyResources(Me.buttonPreviewNext, "buttonPreviewNext")
		Me.buttonPreviewNext.Name = "buttonPreviewNext"
		Me.buttonPreviewNext.UseVisualStyleBackColor = true
		AddHandler Me.buttonPreviewNext.Click, AddressOf Me.ButtonPreviewNextClick
		'
		'buttonPreviewPrevious
		'
		resources.ApplyResources(Me.buttonPreviewPrevious, "buttonPreviewPrevious")
		Me.buttonPreviewPrevious.Name = "buttonPreviewPrevious"
		Me.buttonPreviewPrevious.UseVisualStyleBackColor = true
		AddHandler Me.buttonPreviewPrevious.Click, AddressOf Me.ButtonPreviewPreviousClick
		'
		'tabPageTextOnlyView
		'
		Me.tabPageTextOnlyView.Controls.Add(Me.textBoxTextOnlyView)
		Me.tabPageTextOnlyView.Controls.Add(Me.buttonTextOnlyNext)
		Me.tabPageTextOnlyView.Controls.Add(Me.buttonTextOnlyPrevious)
		resources.ApplyResources(Me.tabPageTextOnlyView, "tabPageTextOnlyView")
		Me.tabPageTextOnlyView.Name = "tabPageTextOnlyView"
		Me.tabPageTextOnlyView.UseVisualStyleBackColor = true
		'
		'textBoxTextOnlyView
		'
		resources.ApplyResources(Me.textBoxTextOnlyView, "textBoxTextOnlyView")
		Me.textBoxTextOnlyView.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.textBoxTextOnlyView.Name = "textBoxTextOnlyView"
		Me.textBoxTextOnlyView.ReadOnly = true
		Me.textBoxTextOnlyView.TabStop = false
		AddHandler Me.textBoxTextOnlyView.MouseDoubleClick, AddressOf Me.ListViewDocsMouseDoubleClick
		'
		'buttonTextOnlyNext
		'
		resources.ApplyResources(Me.buttonTextOnlyNext, "buttonTextOnlyNext")
		Me.buttonTextOnlyNext.Name = "buttonTextOnlyNext"
		Me.buttonTextOnlyNext.UseVisualStyleBackColor = true
		AddHandler Me.buttonTextOnlyNext.Click, AddressOf Me.ButtonTextOnlyNextClick
		'
		'buttonTextOnlyPrevious
		'
		resources.ApplyResources(Me.buttonTextOnlyPrevious, "buttonTextOnlyPrevious")
		Me.buttonTextOnlyPrevious.Name = "buttonTextOnlyPrevious"
		Me.buttonTextOnlyPrevious.UseVisualStyleBackColor = true
		AddHandler Me.buttonTextOnlyPrevious.Click, AddressOf Me.ButtonTextOnlyPreviousClick
		'
		'tabPageCapture
		'
		Me.tabPageCapture.Controls.Add(Me.groupBoxProperties)
		Me.tabPageCapture.Controls.Add(Me.groupBoxDocument)
		Me.tabPageCapture.Controls.Add(Me.groupBoxDocuments)
		resources.ApplyResources(Me.tabPageCapture, "tabPageCapture")
		Me.tabPageCapture.Name = "tabPageCapture"
		Me.tabPageCapture.UseVisualStyleBackColor = true
		'
		'groupBoxProperties
		'
		resources.ApplyResources(Me.groupBoxProperties, "groupBoxProperties")
		Me.groupBoxProperties.Controls.Add(Me.buttonSetToFileName)
		Me.groupBoxProperties.Controls.Add(Me.textBoxKeywords)
		Me.groupBoxProperties.Controls.Add(Me.buttonUpload)
		Me.groupBoxProperties.Controls.Add(Me.labelKeywords)
		Me.groupBoxProperties.Controls.Add(Me.buttonView)
		Me.groupBoxProperties.Controls.Add(Me.comboBoxSubject)
		Me.groupBoxProperties.Controls.Add(Me.buttonDeselect)
		Me.groupBoxProperties.Controls.Add(Me.buttonSave)
		Me.groupBoxProperties.Controls.Add(Me.labelSubject)
		Me.groupBoxProperties.Controls.Add(Me.comboBoxAuthor)
		Me.groupBoxProperties.Controls.Add(Me.labelAuthor)
		Me.groupBoxProperties.Controls.Add(Me.textBoxTitle)
		Me.groupBoxProperties.Controls.Add(Me.labelTitle)
		Me.groupBoxProperties.Name = "groupBoxProperties"
		Me.groupBoxProperties.TabStop = false
		'
		'buttonSetToFileName
		'
		resources.ApplyResources(Me.buttonSetToFileName, "buttonSetToFileName")
		Me.buttonSetToFileName.Name = "buttonSetToFileName"
		Me.buttonSetToFileName.UseVisualStyleBackColor = true
		AddHandler Me.buttonSetToFileName.Click, AddressOf Me.ButtonSetToFilenameClick
		'
		'textBoxKeywords
		'
		resources.ApplyResources(Me.textBoxKeywords, "textBoxKeywords")
		Me.textBoxKeywords.Name = "textBoxKeywords"
		AddHandler Me.textBoxKeywords.TextChanged, AddressOf Me.CaptureComboBoxTextChanged
		'
		'buttonUpload
		'
		resources.ApplyResources(Me.buttonUpload, "buttonUpload")
		Me.buttonUpload.Name = "buttonUpload"
		Me.buttonUpload.UseVisualStyleBackColor = true
		AddHandler Me.buttonUpload.Click, AddressOf Me.ButtonUploadClick
		'
		'labelKeywords
		'
		resources.ApplyResources(Me.labelKeywords, "labelKeywords")
		Me.labelKeywords.Name = "labelKeywords"
		'
		'buttonView
		'
		resources.ApplyResources(Me.buttonView, "buttonView")
		Me.buttonView.Name = "buttonView"
		Me.buttonView.UseVisualStyleBackColor = true
		AddHandler Me.buttonView.Click, AddressOf Me.ButtonViewClick
		'
		'comboBoxSubject
		'
		resources.ApplyResources(Me.comboBoxSubject, "comboBoxSubject")
		Me.comboBoxSubject.Name = "comboBoxSubject"
		Me.comboBoxSubject.Sorted = true
		AddHandler Me.comboBoxSubject.DropDown, AddressOf Me.ComboBoxSubjectDropDown
		AddHandler Me.comboBoxSubject.SelectedIndexChanged, AddressOf Me.CaptureComboBoxTextChanged
		AddHandler Me.comboBoxSubject.TextChanged, AddressOf Me.CaptureComboBoxTextChanged
		'
		'buttonDeselect
		'
		resources.ApplyResources(Me.buttonDeselect, "buttonDeselect")
		Me.buttonDeselect.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.buttonDeselect.Name = "buttonDeselect"
		Me.buttonDeselect.UseVisualStyleBackColor = true
		AddHandler Me.buttonDeselect.Click, AddressOf Me.ButtonDeselectClick
		'
		'buttonSave
		'
		resources.ApplyResources(Me.buttonSave, "buttonSave")
		Me.buttonSave.Name = "buttonSave"
		Me.buttonSave.UseVisualStyleBackColor = true
		AddHandler Me.buttonSave.Click, AddressOf Me.ButtonSaveClick
		'
		'labelSubject
		'
		resources.ApplyResources(Me.labelSubject, "labelSubject")
		Me.labelSubject.Name = "labelSubject"
		'
		'comboBoxAuthor
		'
		resources.ApplyResources(Me.comboBoxAuthor, "comboBoxAuthor")
		Me.comboBoxAuthor.BackColor = System.Drawing.SystemColors.Window
		Me.comboBoxAuthor.Name = "comboBoxAuthor"
		Me.comboBoxAuthor.Sorted = true
		AddHandler Me.comboBoxAuthor.DropDown, AddressOf Me.ComboBoxAuthorDropDown
		AddHandler Me.comboBoxAuthor.SelectedIndexChanged, AddressOf Me.CaptureComboBoxTextChanged
		AddHandler Me.comboBoxAuthor.TextChanged, AddressOf Me.CaptureComboBoxTextChanged
		'
		'labelAuthor
		'
		resources.ApplyResources(Me.labelAuthor, "labelAuthor")
		Me.labelAuthor.Name = "labelAuthor"
		'
		'textBoxTitle
		'
		resources.ApplyResources(Me.textBoxTitle, "textBoxTitle")
		Me.textBoxTitle.BackColor = System.Drawing.SystemColors.Window
		Me.textBoxTitle.Name = "textBoxTitle"
		AddHandler Me.textBoxTitle.TextChanged, AddressOf Me.CaptureComboBoxTextChanged
		'
		'labelTitle
		'
		resources.ApplyResources(Me.labelTitle, "labelTitle")
		Me.labelTitle.Name = "labelTitle"
		'
		'groupBoxDocument
		'
		resources.ApplyResources(Me.groupBoxDocument, "groupBoxDocument")
		Me.groupBoxDocument.Controls.Add(Me.buttonViewOriginal)
		Me.groupBoxDocument.Controls.Add(Me.textBoxPdfDocument)
		Me.groupBoxDocument.Name = "groupBoxDocument"
		Me.groupBoxDocument.TabStop = false
		'
		'buttonViewOriginal
		'
		resources.ApplyResources(Me.buttonViewOriginal, "buttonViewOriginal")
		Me.buttonViewOriginal.Name = "buttonViewOriginal"
		Me.buttonViewOriginal.UseVisualStyleBackColor = true
		AddHandler Me.buttonViewOriginal.Click, AddressOf Me.ButtonViewOriginalClick
		'
		'textBoxPdfDocument
		'
		resources.ApplyResources(Me.textBoxPdfDocument, "textBoxPdfDocument")
		Me.textBoxPdfDocument.Name = "textBoxPdfDocument"
		Me.textBoxPdfDocument.ReadOnly = true
		Me.textBoxPdfDocument.TabStop = false
		'
		'groupBoxDocuments
		'
		resources.ApplyResources(Me.groupBoxDocuments, "groupBoxDocuments")
		Me.groupBoxDocuments.Controls.Add(Me.buttonRename)
		Me.groupBoxDocuments.Controls.Add(Me.buttonDelete)
		Me.groupBoxDocuments.Controls.Add(Me.listBoxDocCaptureQueue)
		Me.groupBoxDocuments.Name = "groupBoxDocuments"
		Me.groupBoxDocuments.TabStop = false
		'
		'buttonRename
		'
		resources.ApplyResources(Me.buttonRename, "buttonRename")
		Me.buttonRename.Name = "buttonRename"
		Me.buttonRename.UseVisualStyleBackColor = true
		AddHandler Me.buttonRename.Click, AddressOf Me.ButtonRenameClick
		'
		'buttonDelete
		'
		resources.ApplyResources(Me.buttonDelete, "buttonDelete")
		Me.buttonDelete.Name = "buttonDelete"
		Me.buttonDelete.UseVisualStyleBackColor = true
		AddHandler Me.buttonDelete.Click, AddressOf Me.ButtonDeleteClick
		'
		'listBoxDocCaptureQueue
		'
		resources.ApplyResources(Me.listBoxDocCaptureQueue, "listBoxDocCaptureQueue")
		Me.listBoxDocCaptureQueue.FormattingEnabled = true
		Me.listBoxDocCaptureQueue.Name = "listBoxDocCaptureQueue"
		Me.listBoxDocCaptureQueue.Sorted = true
		AddHandler Me.listBoxDocCaptureQueue.SelectedIndexChanged, AddressOf Me.ListBoxDocCaptureQueueSelectedIndexChanged
		'
		'processCapturePdfViewer
		'
		Me.processCapturePdfViewer.StartInfo.Domain = ""
		Me.processCapturePdfViewer.StartInfo.ErrorDialog = true
		Me.processCapturePdfViewer.StartInfo.FileName = "SumatraPDF.exe"
		Me.processCapturePdfViewer.StartInfo.LoadUserProfile = false
		Me.processCapturePdfViewer.StartInfo.Password = Nothing
		Me.processCapturePdfViewer.StartInfo.StandardErrorEncoding = Nothing
		Me.processCapturePdfViewer.StartInfo.StandardOutputEncoding = Nothing
		Me.processCapturePdfViewer.StartInfo.UserName = ""
		Me.processCapturePdfViewer.StartInfo.UseShellExecute = false
		Me.processCapturePdfViewer.SynchronizingObject = Me
		'
		'timerDirectUpload
		'
		Me.timerDirectUpload.Enabled = true
		Me.timerDirectUpload.Interval = 15000
		AddHandler Me.timerDirectUpload.Tick, AddressOf Me.TimerDirectUploadTick
		'
		'backgroundWorkerDirectUpload
		'
		AddHandler Me.backgroundWorkerDirectUpload.DoWork, AddressOf Me.BackgroundWorkerDirectUploadDoWork
		AddHandler Me.backgroundWorkerDirectUpload.RunWorkerCompleted, AddressOf Me.BackgroundWorkerDirectUploadRunWorkerCompleted
		'
		'MainForm
		'
		Me.AcceptButton = Me.buttonSearch
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
		Me.CancelButton = Me.buttonDeselect
		resources.ApplyResources(Me, "$this")
		Me.Controls.Add(Me.tabControlMain)
		Me.Controls.Add(Me.statusStrip)
		Me.Controls.Add(Me.menuStrip)
		Me.MainMenuStrip = Me.menuStrip
		Me.Name = "MainForm"
		AddHandler FormClosing, AddressOf Me.MainFormClosing
		AddHandler FormClosed, AddressOf Me.MainFormFormClosed
		AddHandler Load, AddressOf Me.MainFormLoad
		Me.menuStrip.ResumeLayout(false)
		Me.menuStrip.PerformLayout
		CType(Me.errorProvider,System.ComponentModel.ISupportInitialize).EndInit
		Me.statusStrip.ResumeLayout(false)
		Me.statusStrip.PerformLayout
		CType(Me.fileSystemWatcherDocumentCapture,System.ComponentModel.ISupportInitialize).EndInit
		Me.tabControlMain.ResumeLayout(false)
		Me.tabPageSearch.ResumeLayout(false)
		Me.tabControlDocNotesKeywords.ResumeLayout(false)
		Me.tabPageDocumentNotes.ResumeLayout(false)
		Me.tabPageDocumentNotes.PerformLayout
		Me.tabPageDocumentKeywords.ResumeLayout(false)
		Me.tabPageDocumentKeywords.PerformLayout
		Me.tabPagePreview.ResumeLayout(false)
		Me.panelPreview.ResumeLayout(false)
		Me.panelPreview.PerformLayout
		CType(Me.pictureBoxPreview,System.ComponentModel.ISupportInitialize).EndInit
		Me.tabPageTextOnlyView.ResumeLayout(false)
		Me.tabPageTextOnlyView.PerformLayout
		Me.tabPageCapture.ResumeLayout(false)
		Me.groupBoxProperties.ResumeLayout(false)
		Me.groupBoxProperties.PerformLayout
		Me.groupBoxDocument.ResumeLayout(false)
		Me.groupBoxDocument.PerformLayout
		Me.groupBoxDocuments.ResumeLayout(false)
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private buttonDelete As System.Windows.Forms.Button
	Private buttonRename As System.Windows.Forms.Button
	Private buttonSetToFileName As System.Windows.Forms.Button
	Private checkBoxDoNotResetZoomLevel As System.Windows.Forms.CheckBox
	Private buttonTextOnlyPrevious As System.Windows.Forms.Button
	Private buttonTextOnlyNext As System.Windows.Forms.Button
	Private textBoxTextOnlyView As System.Windows.Forms.TextBox
	Private tabPageTextOnlyView As System.Windows.Forms.TabPage
	Private buttonZoomIn As System.Windows.Forms.Button
	Private buttonZoomOut As System.Windows.Forms.Button
	Private panelPreview As System.Windows.Forms.Panel
	Private buttonPreviewPrevious As System.Windows.Forms.Button
	Private buttonPreviewNext As System.Windows.Forms.Button
	Private pictureBoxPreview As System.Windows.Forms.PictureBox
	Private tabPagePreview As System.Windows.Forms.TabPage
	Private backgroundWorkerDirectUpload As System.ComponentModel.BackgroundWorker
	Private toolStripMenuItemDirectUploadFolder As System.Windows.Forms.ToolStripMenuItem
	Private toolStripStatusLabelUploading As System.Windows.Forms.ToolStripStatusLabel
	Private timerDirectUpload As System.Windows.Forms.Timer
	Private toolStripMenuItemDirectUploadConfig As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
	Private buttonDeselect As System.Windows.Forms.Button
	Private toolStripMenuItemReportNewIssue As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
	Private toolStripStatusLabelMessage As System.Windows.Forms.ToolStripStatusLabel
	Private processCapturePdfViewer As System.Diagnostics.Process
	Private processSearchPdfViewer As System.Diagnostics.Process
	Private labelTitle As System.Windows.Forms.Label
	Private textBoxTitle As System.Windows.Forms.TextBox
	Private labelAuthor As System.Windows.Forms.Label
	Private comboBoxAuthor As System.Windows.Forms.ComboBox
	Private labelSubject As System.Windows.Forms.Label
	Private buttonSave As System.Windows.Forms.Button
	Private comboBoxSubject As System.Windows.Forms.ComboBox
	Private buttonView As System.Windows.Forms.Button
	Private labelKeywords As System.Windows.Forms.Label
	Private buttonUpload As System.Windows.Forms.Button
	Private textBoxKeywords As System.Windows.Forms.TextBox
	Private groupBoxProperties As System.Windows.Forms.GroupBox
	Private textBoxPdfDocument As System.Windows.Forms.TextBox
	Private buttonViewOriginal As System.Windows.Forms.Button
	Private groupBoxDocument As System.Windows.Forms.GroupBox
	Private listBoxDocCaptureQueue As System.Windows.Forms.ListBox
	Private groupBoxDocuments As System.Windows.Forms.GroupBox
	Private tabPageCapture As System.Windows.Forms.TabPage
	Private tabPageSearch As System.Windows.Forms.TabPage
	Private tabControlMain As System.Windows.Forms.TabControl
	Private toolStripMenuItemInsertDateTimeIntoDocumentNotes As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemUncheckAll As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemCheckAll As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemDeleteCheckedDocuments As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemSavePDFtoDisk As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemPrintDocumentNotes As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemExit As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemFile As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemEdit As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemView As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemRefresh As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemCheckNewerVersion As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemHelp As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemContents As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemAbout As System.Windows.Forms.ToolStripMenuItem
	Private fileSystemWatcherDocumentCapture As System.IO.FileSystemWatcher
	Private toolStripMenuItemTools As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItemCaptureFolder As System.Windows.Forms.ToolStripMenuItem
	Private timerCaptureCheck As System.Windows.Forms.Timer
	Private toolStripStatusLabelCaptured As System.Windows.Forms.ToolStripStatusLabel
	Private folderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
	Private processExplorer As System.Diagnostics.Process
	Private buttonDocumentNotesRevert As System.Windows.Forms.Button
	Private printDialog As System.Windows.Forms.PrintDialog
	Private printDocumentNotes As System.Drawing.Printing.PrintDocument
	Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private buttonDocumentNotesUpdate As System.Windows.Forms.Button
	Private textBoxDocumentKeywords As System.Windows.Forms.TextBox
	Private textBoxDocumentNotes As System.Windows.Forms.TextBox
	Private imageList As System.Windows.Forms.ImageList
	Private tabPageDocumentNotes As System.Windows.Forms.TabPage
	Private tabPageDocumentKeywords As System.Windows.Forms.TabPage
	Private tabControlDocNotesKeywords As System.Windows.Forms.TabControl
	Private comboBoxSearchText As System.Windows.Forms.ComboBox
	Private toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
	Private backgroundWorkerUpdateCheck As System.ComponentModel.BackgroundWorker
	Private toolStripStatusLabelUpdateStatus As System.Windows.Forms.ToolStripStatusLabel
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private buttonSearch As System.Windows.Forms.Button
	Private columnHeaderAdded As System.Windows.Forms.ColumnHeader
	Private columnHeaderSubject As System.Windows.Forms.ColumnHeader
	Private columnHeaderAuthor As System.Windows.Forms.ColumnHeader
	Private columnHeaderTitle As System.Windows.Forms.ColumnHeader
	Private columnHeaderID As System.Windows.Forms.ColumnHeader
	Private listViewDocs As System.Windows.Forms.ListView
	Private statusStrip As System.Windows.Forms.StatusStrip
	Private errorProvider As System.Windows.Forms.ErrorProvider
	Private labelSearchText As System.Windows.Forms.Label
	Private toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
	Private toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
	Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private menuStrip As System.Windows.Forms.MenuStrip
End Class