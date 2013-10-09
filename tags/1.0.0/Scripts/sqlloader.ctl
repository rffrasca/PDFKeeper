-- ******************************************************************
-- *
-- * PDFKeeper -- PDF Document Storage for Small or Home Office
-- * Copyright (C) 2009 Robert F. Frasca
-- *
-- * This program is free software: you can redistribute it and/or
-- * modify it under the terms of the GNU General Public License as
-- * published by the Free Software Foundation, either version 3 of
-- * the License, or (at your option) any later version.
-- *
-- * This program is distributed in the hope that it will be
-- * useful, but WITHOUT ANY WARRANTY; without even the implied
-- * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
-- * PURPOSE.  See the GNU General Public License for more details.
-- *
-- * You should have received a copy of the GNU General Public
-- * License along with this program.  If not, see
-- * <http://www.gnu.org/licenses/>.
-- *
-- ******************************************************************

load data
infile 'pdfdb.txt'
append
into table pdfkeeper.docs
fields terminated by '|'
(
	doc_id "pdfkeeper.docs_seq.NEXTVAL",
	doc_title char(2000),
	doc_author char(2000),
	doc_subject char(2000),
	doc_keywords char(4000),
	doc_added "to_char(sysdate,'YYYY-MM-DD HH24:MI:SS')",
	doc_notes char(4000),
	pdf_filename filler char(512),
	doc_pdf lobfile(pdf_filename) terminated by eof
)
