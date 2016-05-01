'******************************************************************************
'*
'* PDFKeeper -- Free, Open Source PDF Capture, Upload, and Search.
'* Copyright (C) 2009-2016 Robert F. Frasca
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

Friend Module UserSettingsEnums
	''' <summary>
	''' XML Section Names.
	''' </summary>
	Friend Enum SectionName
		DatabaseConnectionForm
		MainForm
		MainFormDocumentPreviewTab
		CommonDialogs
	End Enum
	
	''' <summary>
	''' XML Key Names.
	''' </summary>
	Friend Enum KeyName
		LastUserName
		LastDataSource
		FormPositionTop
		FormPositionLeft
		FormPositionHeight
		FormPositionWidth
		FormPositionWindowState
		UpdateCheck
		DoNotResetZoomLevel
		OpenFileLastFolder
		SaveFileLastFolder
	End Enum
End Module
