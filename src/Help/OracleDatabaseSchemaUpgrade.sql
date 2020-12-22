/******************************************************************************
** PDFKeeper -- Open Source PDF Document Management
** Copyright (C) 2009-2021 Robert F. Frasca
**
** This file is part of PDFKeeper.
**
** PDFKeeper is free software: you can redistribute it and/or modify
** it under the terms of the GNU General Public License as published by
** the Free Software Foundation, either version 3 of the License, or
** (at your option) any later version.
**
** PDFKeeper is distributed in the hope that it will be useful,
** but WITHOUT ANY WARRANTY; without even the implied warranty of
** MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
** GNU General Public License for more details.
**
** You should have received a copy of the GNU General Public License
** along with PDFKeeper.  If not, see <http://www.gnu.org/licenses/>.
******************************************************************************/

alter table pdfkeeper.docs add(
	doc_category varchar2(2000),
	doc_flag number(1) default 0 not null
	constraint doc_flag_ck check (doc_flag in (0,1)));
alter table pdfkeeper.docs add(
	doc_tax_year number(4),
	doc_annotations clob,
	doc_text clob));

drop index pdfkeeper.docs_idx;

begin
	ctx_ddl.unset_attribute('ctxsys.pdfkeeper_multi','columns');
	ctx_ddl.unset_attribute('ctxsys.pdfkeeper_multi','filter');
	ctx_ddl.set_attribute('ctxsys.pdfkeeper_multi',
			      'columns','doc_title,
					 doc_author,
					 doc_subject,
					 doc_keywords,
					 doc_added,
					 doc_notes,
					 doc_pdf,
					 doc_category,
					 doc_tax_year,
					 doc_annotations,
					 doc_text');
	ctx_ddl.set_attribute('ctxsys.pdfkeeper_multi','filter',
			      'N,N,N,N,N,N,Y,N,N,N,N');
	if (dbms_db_version.version >11) then
		execute immediate 'create index pdfkeeper.docs_idx
				   on pdfkeeper.docs(doc_dummy) 
				   indextype is ctxsys.context 
				   parameters (''datastore ctxsys.pdfkeeper_multi
						 storage ctxsys.text_search_storage
					         lexer ctxsys.pdfkeeper_lexer
					         sync (on commit)'')';
	else
		execute immediate 'create index pdfkeeper.docs_idx
				   on pdfkeeper.docs(doc_dummy) 
				   indextype is ctxsys.context 
				   parameters (''datastore ctxsys.pdfkeeper_multi
					         lexer ctxsys.pdfkeeper_lexer
					    	 sync (on commit)'')';
	end if;
end;
/

quit
