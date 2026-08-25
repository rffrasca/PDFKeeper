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

using PDFKeeper.Core.Interfaces.Caching;
using PDFKeeper.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace PDFKeeper.Core.Caching
{
    /// <summary>
    /// Default implementation of the <see cref="IPdfMemoryCache"/> interface.
    /// </summary>
    public sealed class PdfMemoryCache : IPdfMemoryCache
    {
        private readonly ConcurrentDictionary<int, (byte[] Hash, byte[] EncryptedPdf)> cache;
        private readonly byte[] key;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfMemoryCache"/> class.
        /// </summary>
        /// <remarks>
        /// A single encryption key is created per cache instance. Initialization vectors (IVs)
        /// are generated per encryption operation and embedded in the encrypted payload, ensuring
        /// secure and compliant handling without requiring persistent IV storage.
        /// </remarks>
        public PdfMemoryCache()
        {
            cache = new ConcurrentDictionary<int, (byte[] Hash, byte[] EncryptedPdf)>();

            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                key = aes.Key;
            }
        }

        public bool TryGetPdf(int documentId, out PdfCacheEntry pdfCacheEntry)
        {
            if (cache.TryGetValue(documentId, out var entry))
            {
                var decryptedPdf = Decrypt(entry.EncryptedPdf);
                pdfCacheEntry = new PdfCacheEntry(entry.Hash, decryptedPdf);
                return true;
            }

            pdfCacheEntry = null;
            return false;
        }

        public void StorePdf(int documentId, byte[] hash, byte[] pdfBytes)
        {
            if (pdfBytes is null)
            {
                throw new ArgumentNullException(nameof(pdfBytes));
            }

            var encryptedPdf = Encrypt(pdfBytes);
            cache[documentId] = (hash, encryptedPdf);
        }

        public void RemovePdf(int documentId)
        {
            cache.TryRemove(documentId, out _);
        }

        /// <summary>
        /// Encrypts the specified data using AES‑256 in CBC mode.
        /// A new random initialization vector (IV) is generated for each
        /// encryption operation to ensure security and CA5401 compliance.
        /// </summary>
        /// <param name="data">
        /// The content to encrypt.
        /// </param>
        /// <returns>
        /// A byte array containing the IV followed by the encrypted data.
        /// The IV is stored in cleartext as the first <c>BlockSize / 8</c>
        /// bytes of the returned array.
        /// </returns>
        /// <remarks>
        /// The IV is generated per encryption and prepended to the ciphertext.
        /// This allows the corresponding <see cref="Decrypt(byte[])"/> method
        /// to extract the IV and correctly decrypt the content without requiring
        /// persistent IV storage.
        /// </remarks>
        private byte[] Encrypt(byte[] data)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();
                var iv = aes.IV;

                using (var encryptor = aes.CreateEncryptor())
                {
                    var encrypted = encryptor.TransformFinalBlock(data, 0, data.Length);
                    var result = new byte[iv.Length + encrypted.Length];
                    Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                    Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);
                    return result;
                }
            }
        }

        /// <summary>
        /// Decrypts data previously encrypted by <see cref="Encrypt(byte[])"/>.
        /// Extracts the initialization vector (IV) from the beginning of the
        /// encrypted payload and uses it to perform AES‑256 decryption.
        /// </summary>
        /// <param name="data">
        /// The encrypted content, consisting of the IV followed by the ciphertext.
        /// </param>
        /// <returns>
        /// The decrypted content.
        /// </returns>
        /// <remarks>
        /// The first <c>BlockSize / 8</c> bytes of <paramref name="data"/> contain the IV
        /// generated during encryption. The remaining bytes contain the ciphertext. This method
        /// reconstructs the IV, configures the AES instance, and performs the decryption.
        /// </remarks>
        private byte[] Decrypt(byte[] data)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                var ivLength = aes.BlockSize / 8;
                var iv = new byte[ivLength];
                Buffer.BlockCopy(data, 0, iv, 0, ivLength);
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                {
                    var encrypted = new byte[data.Length - ivLength];
                    Buffer.BlockCopy(data, ivLength, encrypted, 0, encrypted.Length);
                    return decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
                }
            }
        }
    }
}
