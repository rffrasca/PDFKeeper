/******************************************************************************
**
** PDFKeeper -- PDF Document Storage for Small or Home Office
** Copyright (C) 2009-2012 Robert F. Frasca
**
** This program is free software: you can redistribute it and/or modify it
** under the terms of the GNU General Public License as published by the Free
** Software Foundation, either version 3 of the License, or (at your option)
** any later version.
**
** This program is distributed in the hope that it will be useful, but WITHOUT
** ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
** FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
** more details.
**
** You should have received a copy of the GNU General Public License along
** with this program.  If not, see <http://www.gnu.org/licenses/>.
**
******************************************************************************/

create user pdfkeeper
	default tablespace users
	identified by pdfkeeper;

grant connect, resource to pdfkeeper;

create sequence pdfkeeper.docs_seq
	start with 1
	increment by 1
	nocache;

create table pdfkeeper.docs(
	doc_id numeric(28) not null primary key,
	doc_title varchar2(2000) not null,
	doc_author varchar2(2000) not null,
	doc_subject varchar2(2000) not null,
	doc_keywords varchar2(4000),
	doc_added varchar2(19) not null,
	doc_notes varchar2(4000),
	doc_pdf blob not null,
	doc_dummy varchar2(1));

begin
	ctx_ddl.create_preference('ctxsys.pdfkeeper_lexer',
			      	  'basic_lexer');
	ctx_ddl.set_attribute('ctxsys.pdfkeeper_lexer','printjoins','_-');
	ctx_ddl.create_preference('ctxsys.pdfkeeper_multi',
				  'multi_column_datastore');
	ctx_ddl.set_attribute('ctxsys.pdfkeeper_multi',
			      'columns','doc_title,
					 doc_author,
					 doc_subject,
					 doc_keywords,
					 doc_added,
					 doc_notes,
					 doc_pdf');
	ctx_ddl.set_attribute('ctxsys.pdfkeeper_multi','filter',
			      'N,N,N,N,N,N,Y');
end;
/

create index pdfkeeper.docs_idx on pdfkeeper.docs(doc_dummy) 
indextype is ctxsys.context 
parameters ('datastore ctxsys.pdfkeeper_multi
	     lexer ctxsys.pdfkeeper_lexer sync (on commit)');

quit
