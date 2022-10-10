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

insert into docs_index(docs_index) values('rebuild');

drop trigger docs_after_insert;
drop trigger docs_after_update;
drop trigger docs_after_delete;

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
