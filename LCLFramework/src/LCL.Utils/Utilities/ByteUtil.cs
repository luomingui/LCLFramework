using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace LCL
{
    public static class ByteUtil
    {
        public static byte[] ComputeHash(byte[] data)
        {
            return ComputeHash(data, HashAlgorithmType.Sha1);
        }

        public static byte[] ComputeHash(byte[] data, HashAlgorithmType hashAlgorithmType)
        {
            return HashAlgorithm.Create(hashAlgorithmType.ToString()).ComputeHash(data);
        }

        public static string Decode(byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }

        public static void Save(byte[] data, string path)
        {
            File.WriteAllBytes(path, data);
        }

        public static string ToBase64String(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static string ToHex(IEnumerable<byte> bytes)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte num in bytes)
            {
                builder.Append(num.ToString("X2"));
            }
            return builder.ToString();
        }

        public static string ToHex(byte b)
        {
            return b.ToString("X2");
        }

        public static int ToInt(byte[] data, int startIndex)
        {
            return BitConverter.ToInt32(data, startIndex);
        }

        public static long ToInt64(byte[] data, int startIndex)
        {
            return BitConverter.ToInt64(data, startIndex);
        }

        public static MemoryStream ToMemoryStream(byte[] data)
        {
            return new MemoryStream(data);
        }
    }
}

