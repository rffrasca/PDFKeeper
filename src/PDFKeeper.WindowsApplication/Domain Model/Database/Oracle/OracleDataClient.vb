'******************************************************************************
'* PDFKeeper -- Open Source PDF Document Management
'* Copyright (C) 2009-2019  Robert F. Frasca
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
Public Class OracleDataClient
    Inherits OracleDataClientBase
    Implements IDataClient

    Public Sub TestConnection() Implements IDataClient.TestConnection
        Open()
        Close()
    End Sub

    Public Function GetAllAuthors() As DataTable Implements IDataClient.GetAllAuthors
        Dim sqlStatement As String = _
            "select doc_author,count(doc_author) " & _
            "from pdfkeeper.docs " & _
            "group by doc_author"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetAllSubjects() As DataTable Implements IDataClient.GetAllSubjects
        Dim sqlStatement As String = _
            "select doc_subject,count(doc_subject) " & _
            "from pdfkeeper.docs " & _
            "group by doc_subject"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetAllSubjectsByAuthor(author As String) As DataTable Implements IDataClient.GetAllSubjectsByAuthor
        Dim sqlStatement As String = _
            "select doc_subject " & _
            "from pdfkeeper.docs " & _
            "where doc_author = :doc_author " & _
            "group by doc_subject"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_author", author)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetAllCategories() As DataTable Implements IDataClient.GetAllCategories
        Dim sqlStatement As String = _
            "select doc_category,count(doc_category) " & _
            "from pdfkeeper.docs " & _
            "group by doc_category having count(doc_category) > 0"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetAllRecordsBySearchString(searchValue As String) As DataTable Implements IDataClient.GetAllRecordsBySearchString
        Dim sqlStatement As String = _
            "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_added " & _
            "from pdfkeeper.docs " & _
            "where (contains(doc_dummy,:doc_dummy))>0"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_dummy", searchValue)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetAllRecordsByAuthor(author As String) As DataTable Implements IDataClient.GetAllRecordsByAuthor
        Dim sqlStatement As String = _
            "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_added " & _
            "from pdfkeeper.docs " & _
            "where doc_author = :doc_author"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_author", author)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetAllRecordsBySubject(subject As String) As DataTable Implements IDataClient.GetAllRecordsBySubject
        Dim sqlStatement As String = _
           "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_added " & _
           "from pdfkeeper.docs " & _
           "where doc_subject = :doc_subject"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_subject", subject)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetAllRecordsByAuthorAndSubject(author As String, subject As String) As DataTable Implements IDataClient.GetAllRecordsByAuthorAndSubject
        Dim sqlStatement As String = _
           "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_added " & _
           "from pdfkeeper.docs " & _
           "where doc_author = :doc_author and doc_subject = :doc_subject"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_author", author)
            oraCommand.Parameters.Add("doc_subject", subject)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetAllRecordsByCategory(category As String) As DataTable Implements IDataClient.GetAllRecordsByCategory
        Dim sqlStatement As String = _
           "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_added " & _
           "from pdfkeeper.docs " & _
           "where doc_category = :doc_category"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_category", category)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetAllRecordsByDateAdded(dateAdded As String) As DataTable Implements IDataClient.GetAllRecordsByDateAdded
        Dim sqlStatement As String = _
          "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_added " & _
          "from pdfkeeper.docs " & _
          "where doc_added like :doc_added || '%'"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_added", dateAdded)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetTotalRecordsCount() As Integer Implements IDataClient.GetTotalRecordsCount
        Dim sqlStatement As String = "select count(*) from pdfkeeper.docs"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            Return QueryToObject(oraCommand)
        End Using
    End Function

    Public Function GetFlaggedRecordsCount() As Integer Implements IDataClient.GetFlaggedRecordsCount
        Dim sqlStatement As String = _
            "select count(doc_flag) " & _
            "from pdfkeeper.docs " & _
            "where doc_flag = 1"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            Return QueryToObject(oraCommand)
        End Using
    End Function

    Public Function GetAllRecords() As DataTable Implements IDataClient.GetAllRecords
        Dim sqlStatement As String = _
            "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_added " & _
            "from pdfkeeper.docs"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetAllFlaggedRecords() As DataTable Implements IDataClient.GetAllFlaggedRecords
        Dim sqlStatement As String = _
            "select doc_id,doc_title,doc_author,doc_subject,doc_category,doc_added " & _
            "from pdfkeeper.docs " & _
            "where doc_flag = 1"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetNotesById(id As Integer) As DataTable Implements IDataClient.GetNotesById
        Dim sqlStatement As String = _
            "select doc_notes " & _
            "from pdfkeeper.docs " & _
            "where doc_id = :doc_id"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.CommandType = CommandType.Text
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_id", id)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetKeywordsById(id As Integer) As DataTable Implements IDataClient.GetKeywordsById
        Dim sqlStatement As String = _
           "select doc_keywords " & _
           "from pdfkeeper.docs " & _
           "where doc_id = :doc_id"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_id", id)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetCategoryById(id As Integer) As DataTable Implements IDataClient.GetCategoryById
        Dim sqlStatement As String = _
           "select doc_category " & _
           "from pdfkeeper.docs " & _
           "where doc_id = :doc_id"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_id", id)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Function GetFlagStateById(id As Integer) As DataTable Implements IDataClient.GetFlagStateById
        Dim sqlStatement As String = _
          "select doc_flag " & _
          "from pdfkeeper.docs " & _
          "where doc_id = :doc_id"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_id", id)
            Return QueryToDataTable(oraCommand)
        End Using
    End Function

    Public Sub GetPdfById(id As Integer, pdfFile As String) Implements IDataClient.GetPdfById
        Dim sqlStatement As String = _
          "select doc_pdf " & _
          "from pdfkeeper.docs " & _
          "where doc_id = :doc_id"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_id", id)
            QueryBlobToFile(oraCommand, pdfFile)
        End Using
    End Sub

    Public Sub CreateRecord(title As String, author As String, subject As String, keywords As String, notes As String, pdfFile As String, category As String, flag As Integer) Implements IDataClient.CreateRecord
        Dim sqlStatement As String = _
            " begin " & _
            " insert into pdfkeeper.docs values( " & _
            " pdfkeeper.docs_seq.NEXTVAL, " & _
            " :doc_title, " & _
            " :doc_author, " & _
            " :doc_subject, " & _
            " :doc_keywords, " & _
            " to_char(sysdate,'YYYY-MM-DD HH24:MI:SS'), " & _
            " :doc_notes, " & _
            " :doc_pdf, " & _
            " '', " & _
            " :doc_category, " & _
            " :doc_flag) ;" & _
            " end ;"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            Dim fileInfo As New FileInfo(pdfFile)
            Dim blob As Byte() = fileInfo.ToByteArray
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_title", title)
            oraCommand.Parameters.Add("doc_author", author)
            oraCommand.Parameters.Add("doc_subject", subject)
            oraCommand.Parameters.Add("doc_keywords", keywords)
            oraCommand.Parameters.Add("doc_notes", notes)
            oraCommand.Parameters.Add("doc_pdf", OracleDbType.Blob, blob, ParameterDirection.Input)
            oraCommand.Parameters.Add("doc_category", category)
            oraCommand.Parameters.Add("doc_flag", flag)
            ExecuteNonQuery(oraCommand)
        End Using
    End Sub

    Public Sub UpdateNotesById(id As Integer, notes As String) Implements IDataClient.UpdateNotesById
        Dim sqlStatement As String = _
          "update pdfkeeper.docs " & _
          "set doc_notes = :doc_notes,doc_dummy = ''" & _
          "where doc_id = :doc_id"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_notes", notes)
            oraCommand.Parameters.Add("doc_id", id)
            ExecuteNonQuery(oraCommand)
        End Using
    End Sub

    Public Sub UpdateCategoryById(id As Integer, category As String) Implements IDataClient.UpdateCategoryById
        Dim sqlStatement As String = _
            "update pdfkeeper.docs " & _
            "set doc_category = :doc_category,doc_dummy = '' " & _
            "where doc_id = :doc_id"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_category", category)
            oraCommand.Parameters.Add("doc_id", id)
            ExecuteNonQuery(oraCommand)
        End Using
    End Sub

    Public Sub UpdateFlagStateById(id As Integer, flag As Integer) Implements IDataClient.UpdateFlagStateById
        Dim sqlStatement As String = _
             "update pdfkeeper.docs " & _
             "set doc_flag = :doc_flag " & _
             "where doc_id = :doc_id"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_flag", flag)
            oraCommand.Parameters.Add("doc_id", id)
            ExecuteNonQuery(oraCommand)
        End Using
    End Sub

    Public Sub DeleteRecordById(id As Integer) Implements IDataClient.DeleteRecordById
        Dim sqlStatement As String = _
          "delete from pdfkeeper.docs " & _
          "where doc_id = :doc_id"
        Using oraCommand As New OracleCommand(sqlStatement, Connection)
            oraCommand.BindByName = True
            oraCommand.Parameters.Add("doc_id", id)
            ExecuteNonQuery(oraCommand)
        End Using
    End Sub
End Class
