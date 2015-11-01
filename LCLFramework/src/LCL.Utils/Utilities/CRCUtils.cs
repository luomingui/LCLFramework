using System;
using System.Collections;
using System.Text;

namespace LCL
{
    /// <summary>
    /// CRC–£—È∏®÷˙¿‡
    /// </summary>
    public sealed class CrcUtils
    {
        private static ushort[] CRC16Table = null;
        private static uint[] CRC32Table = null;

        private static void MakeCRC16Table()
        {
            if (CRC16Table != null) return;
            CRC16Table = new ushort[256];
            for (ushort i = 0; i < 256; i++)
            {
                ushort vCRC = i;
                for (int j = 0; j < 8; j++)
                    if (vCRC % 2 == 0)
                        vCRC = (ushort)(vCRC >> 1);
                    else vCRC = (ushort)((vCRC >> 1) ^ 0x8408);
                CRC16Table[i] = vCRC;
            }
        }

        private static void MakeCRC32Table()
        {
            if (CRC32Table != null) return;
            CRC32Table = new uint[256];
            for (uint i = 0; i < 256; i++)
            {
                uint vCRC = i;
                for (int j = 0; j < 8; j++)
                    if (vCRC % 2 == 0)
                        vCRC = (uint)(vCRC >> 1);
                    else vCRC = (uint)((vCRC >> 1) ^ 0xEDB88320);
                CRC32Table[i] = vCRC;
            }
        }

        private static ushort UpdateCRC16(byte AByte, ushort ASeed)
        {
            return (ushort)(CRC16Table[(ASeed & 0x000000FF) ^ AByte] ^ (ASeed >> 8));
        }

        private static uint UpdateCRC32(byte AByte, uint ASeed)
        {
            return (uint)(CRC32Table[(ASeed & 0x000000FF) ^ AByte] ^ (ASeed >> 8));
        }

        public static ushort CRC16(byte[] ABytes)
        {
            MakeCRC16Table();
            ushort Result = 0xFFFF;
            foreach (byte vByte in ABytes)
                Result = UpdateCRC16(vByte, Result);
            return (ushort)(~Result);
        }

        public static ushort CRC16(string AString, Encoding AEncoding)
        {
            return CRC16(AEncoding.GetBytes(AString));
        }

        public static ushort CRC16(string AString)
        {
            return CRC16(AString, Encoding.UTF8);
        }

        public static uint CRC32(byte[] ABytes)
        {
            MakeCRC32Table();
            uint Result = 0xFFFFFFFF;
            foreach (byte vByte in ABytes)
                Result = UpdateCRC32(vByte, Result);
            return (uint)(~Result);
        }

        public static uint CRC32(string AString, Encoding AEncoding)
        {
            return CRC32(AEncoding.GetBytes(AString));
        }

        public static uint CRC32(string AString)
        {
            return CRC32(AString, Encoding.UTF8);
        }
    }

}
