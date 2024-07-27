// *****************************************************************************
// * PDFKeeper -- Open Source PDF Document Management
// * Copyright (C) 2009-2024 Robert F. Frasca
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
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Security;
using System.Windows.Forms;

namespace PDFKeeper.WinForms.Components
{
    public partial class SecureTextBox : TextBox
    {
        public SecureTextBox()
        {
            InitializeComponent();
            InitSecureString();
        }

        public SecureTextBox(IContainer container)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            container.Add(this);

            InitializeComponent();
            InitSecureString();
        }

        /// <summary>
        /// Initializes a new SecureString for the current instance of the SecureTextBox class.
        /// This method is called by the constructors of this class and is to be called when the
        /// SecureString needs to be initialized after it has been disposed, allowing for re-entry
        /// in the same instance of the implementing view.
        /// </summary>
        public void InitSecureString()
        {
            SecureText = new SecureString();
        }

        /// <summary>
        /// Gets the text entered in the text box securely as a SecureString object.
        /// </summary>
        public SecureString SecureText { get; private set; }

        private void SecureTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Delete))
            {
                // Remove the character after the cursor or all selected characters from the
                // SecureString.
                if (SelectionLength > 0)
                {
                    RemoveSelectedCharsFromSecureString();
                }
                else if (SelectionStart < Text.Length)
                {
                    SecureText.RemoveAt(SelectionStart);
                }
                SetTextBoxTextAndCursorPosition(SelectionStart);
                e.Handled = true;
            }
            else if (e.KeyCode.Equals(Keys.Escape) || e.KeyCode.Equals(Keys.Enter))
            {
                e.Handled = true;
            }
        }

        private void SecureTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)Keys.Back)) 
            {
                // Remove the character before the cursor or all selected characters from the
                // SecureString.
                if (SelectionLength > 0)
                {
                    RemoveSelectedCharsFromSecureString();
                    SetTextBoxTextAndCursorPosition(SelectionStart);
                }
                else if (SelectionStart > 0)
                {
                    SecureText.RemoveAt(SelectionStart - 1);
                    SetTextBoxTextAndCursorPosition(SelectionStart - 1);
                }
            }
            else
            {
                if (SelectionLength > 0)
                {
                    RemoveSelectedCharsFromSecureString();
                }

                // Insert printable character into SecureString.
                SecureText.InsertAt(SelectionStart, e.KeyChar);

                SetTextBoxTextAndCursorPosition(SelectionStart + 1);
            }
            e.Handled = true;
        }

        private void RemoveSelectedCharsFromSecureString()
        {
            for (int i = 0, loopTo = SelectionLength - 1; i <= loopTo; i++)
            {
                SecureText.RemoveAt(SelectionStart);
            }
        }

        private void SetTextBoxTextAndCursorPosition(int cursorPosition)
        {
            // Set the Text property of the TextBox to a string of asterisks matching the length of
            // the SecureString.
            Text = new string('*', SecureText.Length);
            // This next step sets the cursor position to the specified value. It must be performed
            // after setting the Text property and cannot be moved out of this method.
            SelectionStart = cursorPosition;
        }
    }
}
