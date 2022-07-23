/******************************************************************************
** PDFKeeper -- Open Source PDF Document Management
** Copyright (C) 2009-2022 Robert F. Frasca
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
	doc_text_annotations clob,
	doc_text clob);

drop index pdfkeeper.docs_idx;

begin
	ctx_ddl.drop_preference('ctxsys.pdfkeeper_lexer');
	ctx_ddl.create_preference('pdfkeeper.pdfkeeper_lexer',
				  'world_lexer');
	
	ctx_ddl.unset_attribute('ctxsys.pdfkeeper_multi','columns');
	ctx_ddl.unset_attribute('ctxsys.pdfkeeper_multi','filter');
	ctx_ddl.drop_preference('ctxsys.pdfkeeper_multi');
	ctx_ddl.create_preference('pdfkeeper.pdfkeeper_multi',
				  'multi_column_datastore');
	ctx_ddl.set_attribute('pdfkeeper.pdfkeeper_multi',
			      'columns','doc_title,
					 doc_author,
					 doc_subject,
					 doc_keywords,
					 doc_added,
					 doc_notes,
					 doc_pdf,
					 doc_category,
					 doc_tax_year,
					 doc_text_annotations,
					 doc_text');
	ctx_ddl.set_attribute('pdfkeeper.pdfkeeper_multi','filter',
			      'N,N,N,N,N,N,Y,N,N,N,N');

	ctx_ddl.unset_attribute('ctxsys.text_search_storage','stage_itab');
	ctx_ddl.drop_preference('ctxsys.text_search_storage');
	ctx_ddl.create_preference('pdfkeeper.text_search_storage',
				  'basic_storage');
	ctx_ddl.set_attribute('pdfkeeper.text_search_storage',
			      'stage_itab',
			      'true');
end;
/

create index pdfkeeper.docs_idx on pdfkeeper.docs(doc_dummy)
indextype is ctxsys.context
parameters ('datastore pdfkeeper.pdfkeeper_multi
	     storage pdfkeeper.text_search_storage
	     lexer pdfkeeper.pdfkeeper_lexer
	     sync (on commit)');

quit
