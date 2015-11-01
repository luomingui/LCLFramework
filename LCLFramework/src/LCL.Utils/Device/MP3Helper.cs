using System;
using System.IO;
using System.Runtime.InteropServices;

namespace LCL.Utils
{
    /// <summary>
    /// MP3文件播放操作辅助类
    /// </summary>
    public class MP3Helper
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, System.Text.StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        
        public static void Play(string MP3_FileName,bool Repeat)
        {
            mciSendString("open \"" + MP3_FileName + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            mciSendString("play MediaFile" + (Repeat ? " repeat" :String.Empty), null, 0, IntPtr.Zero);
        }
        public static void Play(byte[] MP3_EmbeddedResource, bool Repeat)
        {
            extractResource(MP3_EmbeddedResource, Path.GetTempPath() + "resource.tmp");
            mciSendString("open \"" + Path.GetTempPath() + "resource.tmp" + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            mciSendString("play MediaFile" + (Repeat ? " repeat" : String.Empty), null, 0, IntPtr.Zero);
        }

        public static void Pause()
        {
            mciSendString("stop MediaFile", null, 0, IntPtr.Zero);
        }

        public static void Stop()
        {
            mciSendString("close MediaFile", null, 0, IntPtr.Zero);
        }

        private static void extractResource(byte[] res,string filePath)
        {
            FileStream fs;
            BinaryWriter bw;

            if (!File.Exists(filePath))
            {
                fs = new FileStream(filePath, FileMode.OpenOrCreate);
                bw = new BinaryWriter(fs);

                foreach (byte b in res)
                    bw.Write(b);

                bw.Close();
                fs.Close();
            }
        }
    }
}
