-- ****************************************************************************
-- * PDFKeeper -- Open Source PDF Document Management
-- * Copyright (C) 2009-2024 Robert F. Frasca
-- *
-- * This file is part of PDFKeeper.
-- *
-- * PDFKeeper is free software: you can redistribute it and/or modify it
-- * under the terms of the GNU General Public License as published by the
-- * Free Software Foundation, either version 3 of the License, or (at your
-- * option) any later version.
-- *
-- * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
-- * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
-- * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
-- * more details.
-- *
-- * You should have received a copy of the GNU General Public License along
-- * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
-- ****************************************************************************

create database pdfkeeper;

create table pdfkeeper.docs(
	doc_id int primary key auto_increment not null,
	doc_title varchar(255) not null,
	doc_author varchar(255) not null,
	doc_subject varchar(255) not null,
	doc_keywords varchar(255),
	doc_added varchar(19) not null,
	doc_notes mediumtext,
	doc_pdf longblob not null,
	doc_category varchar(255),
	doc_flag bool default 0 not null
	constraint doc_flag_ck check (doc_flag = 0 or doc_flag = 1),
	doc_tax_year varchar(4),
	doc_text_annotations longtext,
	doc_text longtext,
	fulltext key (
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
	)
);

quit
