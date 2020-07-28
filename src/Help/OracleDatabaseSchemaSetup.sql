/******************************************************************************
** PDFKeeper -- Open Source PDF Document Management
** Copyright (C) 2009-2020  Robert F. Frasca
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

accept new_password char prompt 'Enter the password to set on the pdfkeeper account: '

create user pdfkeeper
	default tablespace users
	identified by "&new_password";

grant create session to pdfkeeper;
grant unlimited tablespace to pdfkeeper;
grant create table to pdfkeeper;

create sequence pdfkeeper.docs_seq
	start with 1
	increment by 1
	nocache;

create table pdfkeeper.docs(
	doc_id number(28) not null primary key,
	doc_title varchar2(2000) not null,
	doc_author varchar2(2000) not null,
	doc_subject varchar2(2000) not null,
	doc_keywords varchar2(4000),
	doc_added varchar2(19) not null,
	doc_notes varchar2(4000),
	doc_pdf blob not null,
	doc_dummy varchar2(1),
	doc_category varchar2(2000),
	doc_flag number(1) default 0 not null
	constraint doc_flag_ck check (doc_flag in (0,1)));

begin
	ctx_ddl.create_preference('ctxsys.pdfkeeper_lexer',
			      	  'world_lexer');
	ctx_ddl.create_preference('ctxsys.pdfkeeper_multi',
				  'multi_column_datastore');
	ctx_ddl.set_attribute('ctxsys.pdfkeeper_multi',
			      'columns','doc_title,
					 doc_author,
					 doc_subject,
					 doc_keywords,
					 doc_added,
					 doc_notes,
					 doc_pdf,
					 doc_category');
	ctx_ddl.set_attribute('ctxsys.pdfkeeper_multi','filter',
			      'N,N,N,N,N,N,Y,N');
	if (dbms_db_version.version >11) then
		ctx_ddl.create_preference('ctxsys.text_search_storage',
					  'basic_storage');
		ctx_ddl.set_attribute('ctxsys.text_search_storage',
				      'stage_itab',
				      'true');
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
