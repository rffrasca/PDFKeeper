'******************************************************************************
'*
'* PDFKeeper -- Capture, Upload, and Search for PDF Documents
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

Public MustInherit Class NotifyPropertyChangedBase
	Implements INotifyPropertyChanged
	
	Public Event PropertyChanged As PropertyChangedEventHandler _
		Implements INotifyPropertyChanged.PropertyChanged
	
	''' <summary>
	''' Call from property setter where property changed notification is
	''' needed.
	''' </summary>
	''' <param name="propertyName">Name of property changed.</param>
	Public Sub OnPropertyChanged(ByVal propertyName As String)
		RaiseEvent PropertyChanged(Me, _
			New PropertyChangedEventArgs(propertyName))
	End Sub
End Class
