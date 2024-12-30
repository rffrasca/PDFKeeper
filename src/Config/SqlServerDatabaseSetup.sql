-- ****************************************************************************
-- * PDFKeeper -- Open Source PDF Document Management
-- * Copyright (C) 2009-2025 Robert F. Frasca
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

use master
go

exec sp_configure 'contained database authentication', 1
go
reconfigure
go

create database pdfkeeper containment = partial
go

use pdfkeeper
go

create table docs
(
	doc_id int primary key clustered not null identity,
	doc_title nvarchar(255) not null,
	doc_author nvarchar(255) not null,
	doc_subject nvarchar(255) not null,
	doc_keywords nvarchar(255),
	doc_added char(19) not null,
	doc_notes nvarchar(max),
	doc_pdf varbinary(max) not null,
	doc_category nvarchar(255),
	doc_flag bit not null default 0,	
	doc_tax_year char(4),
	doc_text_annotations nvarchar(max),
	doc_text nvarchar(max)
)
go

create unique index docs_idx on docs(doc_id)
go

create fulltext catalog docs_cat with accent_sensitivity = off
go

create fulltext index on docs
(
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
key index docs_idx on docs_cat with change_tracking auto
go

print 'Done.'
go

exit
