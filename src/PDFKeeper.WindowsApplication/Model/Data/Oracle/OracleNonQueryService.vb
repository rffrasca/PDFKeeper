'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Storage Solution
'* Copyright (C) 2009-2019 Robert F. Frasca
'*
'* This file is part of PDFKeeper.
'*
'* PDFKeeper is free software: you can redistribute it and/or modify
'* it under the terms of the GNU General Public License as published by
'* the Free Software Foundation, either version 3 of the License, or
'* (at your option) any later version.
'*
'* PDFKeeper is distributed in the hope that it will be useful,
'* but WITHOUT ANY WARRANTY; without even the implied warranty of
'* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'* GNU General Public License for more details.
'*
'* You should have received a copy of the GNU General Public License
'* along with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
'******************************************************************************
Public Class OracleNonQueryService
    Implements INonQueryService
    Private dataProvider As IDataProvider
    Dim sqlStatement As String

    Public Sub New()
        DataProviderHelper.SetDataProvider(dataProvider)
    End Sub

    Public Sub InsertDocument(title As String, _
                              author As String, _
                              subject As String, _
                              keywords As String, _
                              notes As String, _
                              pdfFile As String) Implements INonQueryService.InsertDocument
        ' Create the Anonymous PL/SQL block statement for the insert.
        sqlStatement = _
            " begin " & _
            " insert into pdfkeeper.docs values( " & _
            " pdfkeeper.docs_seq.NEXTVAL, " & _
            " q'[" & title & "]', " & _
            " q'[" & author & "]', " & _
            " q'[" & subject & "]', " & _
            " q'[" & keywords & "]', " & _
            " to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'), " & _
            " q'[" & notes & "]', " & _
            " :1, '') ;" & _
            " end ;"
        dataProvider.ExecuteBlobInsert(sqlStatement, pdfFile)
    End Sub

    Public Sub SetDocumentNotes(id As Integer, notes As String) Implements INonQueryService.SetDocumentNotes
        sqlStatement = "update pdfkeeper.docs " & _
            "set doc_notes =q'[" & notes & "]',doc_dummy = '' " & _
            "where doc_id = " & id
        dataProvider.ExecuteNonQuery(sqlStatement)
    End Sub

    Public Sub DeleteDocument(id As Integer) Implements INonQueryService.DeleteDocument
        sqlStatement = "delete from pdfkeeper.docs where doc_id = " & id
        dataProvider.ExecuteNonQuery(sqlStatement)
    End Sub
End Class
