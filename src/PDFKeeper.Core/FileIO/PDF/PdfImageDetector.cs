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

using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Collections.Generic;

namespace PDFKeeper.Core.FileIO.PDF
{
    public class PdfImageDetector : IEventListener
    {
        /// <summary>
        /// Gets <c>true</c> or <c>false</c> if images were detected in PDF page source.
        /// </summary>
        public bool ImagesDetected { get; private set; }

        [CLSCompliant(false)]
        public void EventOccurred(IEventData data, EventType type)
        {
            if (type.Equals(EventType.RENDER_IMAGE))
            {
                ImagesDetected = true;
            }           
        }

        [CLSCompliant(false)]
        public ICollection<EventType> GetSupportedEvents()
        {
            return null;
        }
    }
}
