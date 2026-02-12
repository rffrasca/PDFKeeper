// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2026 Robert F. Frasca
// *
// * This file is part of PDFKeeper.
// *
// * PDFKeeper is free software: you can redistribute it and/or modify it
// * under the terms of the GNU General Public License as published by the
// * Free Software Foundation, either version 3 of the License, or (at your
// * option) any later version.
// *
// * PDFKeeper is distributed in the hope that it will be useful, but WITHOUT
// * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// * more details.
// *
// * You should have received a copy of the GNU General Public License along
// * with PDFKeeper. If not, see <https://www.gnu.org/licenses/>.
// ****************************************************************************

using System.Data;

namespace PDFKeeper.Core.DataAccess.Repository
{
    public abstract class RepositoryBase<T1, T2>
    {
        protected T1 connStrBuilder;

        protected abstract DataTable ExecuteQuery(T2 command);
        protected abstract string GetSearchTermSnippets(int id, string searchTerm);

        protected virtual void GetDocsTableAccess()
        {
            DatabaseSession.SelectGranted = true;
            DatabaseSession.InsertGranted = true;
            DatabaseSession.UpdateGranted = true;
            DatabaseSession.DeleteGranted = true;
        }
    }
}
