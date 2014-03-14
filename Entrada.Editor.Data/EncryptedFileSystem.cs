using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Entrada.Editor.Data;

namespace Entrada.Editor.Core
{
    public class EncryptedFileSystem
    {
        private const string salt = "2Kfm98acc!CCppooss10!";

        public static CryptoStream GetOutputStream(string path)
        {
            // Ensure the directory always exists.
            var dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir);

            using (var aes = new AesManaged())
            {
                var deriveBytes = new Rfc2898DeriveBytes(Settings.GetHashedPassword(), Encoding.UTF8.GetBytes(salt));

                aes.Padding = PaddingMode.PKCS7;
                aes.Key = deriveBytes.GetBytes(128 / 8);

                var stream = File.Create(path);
                stream.Write(BitConverter.GetBytes(aes.IV.Length), 0, sizeof(int));
                stream.Write(aes.IV, 0, aes.IV.Length);

                return new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            }
        }

        public static StreamWriter GetStreamWriter(string path)
        {
            return new StreamWriter(GetOutputStream(path));
        }

        public static StreamReader GetStreamReader(string path, Encoding encoding)
        {
            return new StreamReader(GetInputStream(path), encoding);
            //return new StreamReader(GetInputStream(path));
        }

        public static CryptoStream GetInputStream(string path)
        {
            using (var aes = new AesManaged())
            {
                var deriveBytes = new Rfc2898DeriveBytes(Settings.GetHashedPassword(), Encoding.UTF8.GetBytes(salt));

                aes.Padding = PaddingMode.PKCS7;
                aes.Key = deriveBytes.GetBytes(128 / 8);

                var stream = File.OpenRead(path);

                // Get the initialization vector from the encrypted stream
                aes.IV = ReadByteArray(stream);

                //stream.Read(BitConverter.GetBytes(aes.IV.Length), 0, sizeof(int));
                //stream.Read(aes.IV, 0, aes.IV.Length);

                return new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            }
        }

        public static MemoryStream GetDecryptedStream(string path)
        {
            using (var stream = GetInputStream(path))
            {
                var ms = new MemoryStream();

                stream.CopyTo(ms);

                ms.Position = 0;

                return ms;
            }
        }

        public static void WriteMemoryStream(string path, MemoryStream ms)
        {
            using (var stream = GetOutputStream(path))
            {
                ms.Position = 0;
                ms.CopyTo(stream);
            }
        }

        private static byte[] ReadByteArray(Stream s)
        {
            var rawLength = new byte[sizeof(int)];

            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
                throw new SystemException("Stream did not contain properly formatted byte array");

            var buffer = new byte[BitConverter.ToInt32(rawLength, 0)];

            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
                throw new SystemException("Did not read byte array properly");

            return buffer;
        }
    }
}
