﻿namespace CMS.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Manage Encryption Decryption
    /// </summary>
    /// <CreatedBy>Harshil Kalariya</CreatedBy>
    /// <CreatedDate>22-Oct-2020</CreatedDate>
    /// <ModifyBy></ModifyBy>
    /// <ModifyDate></ModifyDate>
    public class EncryptionDecryption
    {
        #region Variable Declaration

        /// <summary>
        /// key String
        /// </summary>
        private static string keyString = "08TSITSW-ESTO-BSBM-2BOB-03DEC20B15YE";

        /// <summary>
        /// AES cryptography key
        /// </summary>
        private static string passPhrase = "edPUeWmCadulOeb8taDwHlniS03IbihkXQXoDnsIA0gyCATFGl920lneFrYk";

        #endregion

        #region Triple DES

        /// <summary>
        /// Get Encrypted Value of Passed value
        /// </summary>
        /// <param name="value">value to Encrypted</param>
        /// <returns>encrypted string</returns>
        public static string EncryptByTripleDES(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return GetEncryptByTripleDES(keyString, value);
            }

            return string.Empty;
        }

        /// <summary>
        /// Get Decrypted value of passed encrypted string
        /// </summary>
        /// <param name="value">value to Decrypted</param>
        /// <returns>Decrypted string</returns>
        public static string DecryptByTripleDES(string value)
        {
            return GetDecryptByTripleDES(keyString, value);
        }

        /// <summary>
        /// Encrypt value
        /// </summary>
        /// <param name="strKey">Passphrase for Encrypt</param>
        /// <param name="strData">Message to Encrypt</param>
        /// <returns>encrypted string</returns>
        private static string GetEncryptByTripleDES(string strKey, string strData)
        {
            byte[] results;
            System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below
            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(strKey));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider tdesAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            tdesAlgorithm.Key = tdesKey;
            tdesAlgorithm.Mode = CipherMode.ECB;
            tdesAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] dataToEncrypt = utf8.GetBytes(strData);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform encryptor = tdesAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(results);
        }

        /// <summary>
        /// decrypt value
        /// </summary>
        /// <param name="strKey">Passphrase for Decrypt</param>
        /// <param name="strData">Message to Decrypt</param>
        /// <returns>Decrypted string</returns>
        private static string GetDecryptByTripleDES(string strKey, string strData)
        {
            if (string.IsNullOrEmpty(strData))
            {
                return string.Empty;
            }

            byte[] results;
            System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below
            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(strKey));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider tdesAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            tdesAlgorithm.Key = tdesKey;
            tdesAlgorithm.Mode = CipherMode.ECB;
            tdesAlgorithm.Padding = PaddingMode.PKCS7;

            strData = strData.Replace(" ", "+"); // Replace space with plus sign in encrypted value if any.

            try
            {
                // Step 4. Convert the input string to a byte[]
                byte[] dataToDecrypt = Convert.FromBase64String(strData);

                // Step 5. Attempt to decrypt the string
                ICryptoTransform decryptor = tdesAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return utf8.GetString(results);
        }

        #endregion

        #region AES

        /// <summary>
        /// Encrpyts the sourceString, returns this result as an Aes encrpyted, BASE64 encoded string
        /// </summary>
        /// <param name="plainSourceStringToEncrypt">a plain, Framework string (ASCII, null terminated)</param>
        /// <returns>
        /// returns an Aes encrypted, BASE64 encoded string
        /// </returns>
        public static string EncryptByAES(string plainSourceStringToEncrypt)
        {
            ////Set up the encryption objects
            using (AesCryptoServiceProvider acsp = GetProvider(Encoding.Default.GetBytes(passPhrase)))
            {
                ///// byte[] sourceBytes = Encoding.ASCII.GetBytes(plainSourceStringToEncrypt);
                //// change ASCII to UTF8 Because German character not supported in ASCII 
                //// ASCII convert German character ü to ? so change
                byte[] sourceBytes = Encoding.UTF8.GetBytes(plainSourceStringToEncrypt);
                ICryptoTransform ictE = acsp.CreateEncryptor();

                ////Set up stream to contain the encryption
                MemoryStream msS = new MemoryStream();

                ////Perform the encrpytion, storing output into the stream
                CryptoStream csS = new CryptoStream(msS, ictE, CryptoStreamMode.Write);
                csS.Write(sourceBytes, 0, sourceBytes.Length);
                csS.FlushFinalBlock();

                ////sourceBytes are now encrypted as an array of secure bytes
                byte[] encryptedBytes = msS.ToArray(); ////.ToArray() is important, don't mess with the buffer

                ////return the encrypted bytes as a BASE64 encoded string
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        /// <summary>
        /// Decrypts a BASE64 encoded string of encrypted data, returns a plain string
        /// </summary>
        /// <param name="base64StringToDecrypt">an Aes encrypted AND base64 encoded string</param>
        /// <returns>returns a plain string</returns>
        public static string DecryptByAES(string base64StringToDecrypt)
        {
            ////Set up the encryption objects
            using (AesCryptoServiceProvider acsp = GetProvider(Encoding.Default.GetBytes(passPhrase)))
            {
                byte[] rawBytes = Convert.FromBase64String(base64StringToDecrypt);
                ICryptoTransform ictD = acsp.CreateDecryptor();

                ////RawBytes now contains original byte array, still in Encrypted state

                ////Decrypt into stream
                MemoryStream msD = new MemoryStream(rawBytes, 0, rawBytes.Length);
                CryptoStream csD = new CryptoStream(msD, ictD, CryptoStreamMode.Read);
                ////csD now contains original byte array, fully decrypted

                ////return the content of msD as a regular string
                return new StreamReader(csD).ReadToEnd();
            }
        }

        #region
        public static string EncryptBySHA1(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hashCode = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hashCode.Length * 2);

                foreach (byte b in hashCode)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        #endregion

        private static AesCryptoServiceProvider GetProvider(byte[] key)
        {
            AesCryptoServiceProvider result = new AesCryptoServiceProvider();
            result.BlockSize = 128;
            result.KeySize = 128;
            result.Mode = CipherMode.CBC;
            result.Padding = PaddingMode.PKCS7;

            result.GenerateIV();
            result.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            byte[] realKey = GetKey(key, result);
            result.Key = realKey;
            return result;
        }

        private static byte[] GetKey(byte[] suggestedKey, SymmetricAlgorithm p)
        {
            byte[] kRaw = suggestedKey;
            List<byte> kList = new List<byte>();

            for (int i = 0; i < p.LegalKeySizes[0].MinSize; i += 8)
            {
                kList.Add(kRaw[(i / 8) % kRaw.Length]);
            }

            byte[] k = kList.ToArray();
            return k;
        }
        #endregion
    }
}
