// ****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2025 Robert F. Frasca
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

using PDFKeeper.Core.Commands;
using System;

namespace PDFKeeper.Core.Presenters
{
    public abstract class PresenterBase<T>
    {
        public T ViewModel { get; protected set; }
        public bool CancelViewClosing { get; set; }
        public Action OnApplyPendingChangesRequested { get; set; }
        public Action OnLongRunningOperationStarted { get; set; }
        public Action OnLongRunningOperationFinished { get; set; }
        public Action OnViewCloseCancelled { get; set; }
        public Action OnViewCloseRequested { get; set; }
        public Action OnViewResetRequested { get; set; }
        
        /// <summary>
        /// Executes the command object.
        /// </summary>
        /// <param name="command">The command object.</param>
        public void ExecuteCommand(ICommand command)
        {
            if (command != null)
            {
                command.Execute();
            }
        }

        protected abstract void GetServices(IServiceProvider serviceProvider);
    }
}
