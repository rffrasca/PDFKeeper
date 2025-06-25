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

        /// <summary>
        /// Notifies the view that a request was made to apply pending changes.
        /// </summary>
        public event EventHandler ApplyPendingChangesRequested;

        /// <summary>
        /// Notifies the view that a long running operation has started.
        /// </summary>
        public event EventHandler LongRunningOperationStarted;

        /// <summary>
        /// Notifies the view that a long running operation has finished.
        /// </summary>
        public event EventHandler LongRunningOperationFinished;

        /// <summary>
        /// Notifies the view that a close operation was cancelled.
        /// </summary>
        public event EventHandler ViewCloseCancelled;

        /// <summary>
        ///  Notifies the view that a request was made to close.
        /// </summary>
        public event EventHandler ViewCloseRequested;

        /// <summary>
        /// Notifies the view that a reset was requested.
        /// </summary>
        public event EventHandler ViewResetRequested;

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

        /// <summary>
        /// Raises the LongRunningOperationStarted event to notify the view that a long running
        /// operation has started.
        /// </summary>
        protected void OnLongRunningOperationStarted()
        {
            LongRunningOperationStarted?.Invoke(this, null);
        }

        /// <summary>
        /// Raises the LongRunningOperationFinished event to notify the view that a long running
        /// operation has finished.
        /// </summary>
        protected void OnLongRunningOperationFinished()
        {
            LongRunningOperationFinished?.Invoke(this, null);
        }

        /// <summary>
        /// Raises the ApplyPendingChangesRequested event to notify the view that a request was
        /// made to apply pending changes.
        /// </summary>
        protected void OnApplyPendingChangesRequested()
        {
            ApplyPendingChangesRequested?.Invoke(this, null);
        }

        /// <summary>
        /// Raises the ViewCloseCancelled event to notify the view that a close operation was
        /// cancelled.
        /// </summary>
        protected void OnViewCloseCancelled()
        {
            CancelViewClosing = true;
            ViewCloseCancelled?.Invoke(this, null);
        }

        /// <summary>
        /// Raises the ViewCloseRequested event to notify the view that a request was made to
        /// close.
        /// </summary>
        protected void OnViewCloseRequested()
        {
            CancelViewClosing = false;
            ViewCloseRequested?.Invoke(this, null);
        }

        /// <summary>
        /// Raises the ViewResetRequested event to notify the view that a reset was requested.
        /// </summary>
        protected void OnViewResetRequested()
        {
            ViewResetRequested?.Invoke(this, null);
        }
    }
}
