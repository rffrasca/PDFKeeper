/******************************************************************************
** PDFKeeper -- Open Source PDF Document Management
** Copyright (C) 2009-2023 Robert F. Frasca
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

create table docs(
	doc_id integer primary key autoincrement not null,
	doc_title text not null,
	doc_author text not null,
	doc_subject text not null,
	doc_keywords text,
	doc_added text not null,
	doc_notes text,
	doc_pdf blob not null,
	doc_category text,
	doc_flag integer default 0 check (doc_flag = 0 or doc_flag = 1),
	doc_tax_year text,
	doc_text_annotations text,
	doc_text text
);

create virtual table docs_index using fts5(
	doc_title,
	doc_author,
	doc_subject,
	doc_keywords,
	doc_added,
	doc_notes,
	doc_category,
	doc_tax_year,
	doc_text_annotations,
	doc_text,
	content='docs',
	content_rowid='doc_id',
	tokenize=porter
);

create trigger docs_after_insert after insert on docs begin
	insert into docs_index (
		rowid,
		doc_title,
		doc_author,
		doc_subject,
		doc_keywords,
		doc_added,
		doc_notes,
		doc_category,
		doc_tax_year,
		doc_text_annotations,
		doc_text
	) values(
		new.doc_id,
		new.doc_title,
		new.doc_author,
		new.doc_subject,
		new.doc_keywords,
		new.doc_added,
		new.doc_notes,
		new.doc_category,
		new.doc_tax_year,
		new.doc_text_annotations,
		new.doc_text
	);
end;

create trigger docs_before_update before update on docs begin
	delete from docs_index where rowid = old.doc_id;
end;

create trigger docs_after_update after update on docs begin
	insert into docs_index (
		rowid,
		doc_title,
		doc_author,
		doc_subject,
		doc_keywords,
		doc_added,
		doc_notes,
		doc_category,
		doc_tax_year,
		doc_text_annotations,
		doc_text
	) values(
		new.doc_id,
		new.doc_title,
		new.doc_author,
		new.doc_subject,
		new.doc_keywords,
		new.doc_added,
		new.doc_notes,
		new.doc_category,
		new.doc_tax_year,
		new.doc_text_annotations,
		new.doc_text
	);
end;

create trigger docs_before_delete before delete on docs begin
	delete from docs_index where rowid = old.doc_id;
end;

.exit
