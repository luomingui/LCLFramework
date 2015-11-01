using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LCL.Utils
{
    /// <summary>
    /// 摄像头操作辅助类，包括开启、关闭、抓图、设置等功能
    /// </summary>
    public class Camera
    {
        private IntPtr lwndC;
        private IntPtr mControlPtr;
        private int mWidth;
        private int mHeight;

        // 构造函数
        public Camera(IntPtr handle, int width, int height)
        {
            mControlPtr = handle;
            mWidth = width;
            mHeight = height;
        }

        // 帧回调的委托
        public delegate void RecievedFrameEventHandler(byte[] data);
        public event RecievedFrameEventHandler RecievedFrame;
        private AviCapture.FrameEventHandler mFrameEventHandler;

        /// <summary>
        /// 关闭摄像头
        /// </summary>
        public void CloseWebcam()
        {
            this.capDriverDisconnect(this.lwndC);
        }

        /// <summary>
        /// 开启摄像头
        /// </summary>
        public void StartWebCam()
        {
            byte[] lpszName = new byte[100];
            byte[] lpszVer = new byte[100];

            AviCapture.capGetDriverDescriptionA(1, lpszName, 100, lpszVer, 100);
            this.lwndC = AviCapture.capCreateCaptureWindowA(lpszName, AviCapture.WS_VISIBLE + AviCapture.WS_CHILD, 0, 0, mWidth, mHeight, mControlPtr, 0);

            if (this.capDriverConnect(this.lwndC, 0))
            {
                this.capPreviewRate(this.lwndC, 66);

                this.capPreview(this.lwndC, true);
                this.capOverlay(this.lwndC, true);
                AviCapture.BITMAPINFO bitmapinfo = new AviCapture.BITMAPINFO();
                bitmapinfo.bmiHeader.biSize = AviCapture.SizeOf(bitmapinfo.bmiHeader);
                bitmapinfo.bmiHeader.biWidth = this.mWidth;
                bitmapinfo.bmiHeader.biHeight = this.mHeight;
                bitmapinfo.bmiHeader.biPlanes = 1;
                bitmapinfo.bmiHeader.biBitCount = 24;
                this.capSetVideoFormat(this.lwndC, ref bitmapinfo, AviCapture.SizeOf(bitmapinfo));

                this.mFrameEventHandler = new AviCapture.FrameEventHandler(FrameCallBack);
                this.capSetCallbackOnFrame(this.lwndC, this.mFrameEventHandler);
                AviCapture.SetWindowPos(this.lwndC, 0, 0, 0, mWidth, mHeight, 6);
            }
        }

        /// <summary>
        /// 抓图到文件
        /// </summary>
        /// <param name="path"></param>
        public void GrabImage(string path)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(path);
            AviCapture.SendMessage(lwndC, AviCapture.WM_CAP_SAVEDIB, 0, hBmp.ToInt32());
        }

        /// <summary>
        /// 抓图到剪切板
        /// </summary>
        /// <returns></returns>
        public bool GrabImageToClipBoard()
        {
            return AviCapture.SendMessage(lwndC, AviCapture.WM_CAP_EDIT_COPY, 0, 0);
        }

        /// <summary>
        /// 弹出色彩设置对话框
        /// </summary>
        public void SetCaptureSource()
        {
            AviCapture.CAPDRIVERCAPS caps = new AviCapture.CAPDRIVERCAPS();
            AviCapture.SendMessage(lwndC, AviCapture.WM_CAP_GET_CAPS, AviCapture.SizeOf(caps), ref  caps);
            if (caps.fHasDlgVideoSource)
            {
                AviCapture.SendMessage(lwndC, AviCapture.WM_CAP_DLG_VIDEOSOURCE, 0, 0);
            }
        }

        /// <summary>
        /// 弹出视频格式设置对话框
        /// </summary>
        public void SetCaptureFormat() 
        {
            AviCapture.CAPDRIVERCAPS caps = new AviCapture.CAPDRIVERCAPS();
            AviCapture.SendMessage(lwndC, AviCapture.WM_CAP_GET_CAPS, AviCapture.SizeOf(caps), ref  caps);
            if (caps.fHasDlgVideoSource)
            {
                AviCapture.SendMessage(lwndC, AviCapture.WM_CAP_DLG_VIDEOFORMAT, 0, 0);
            }
        }


        #region 以下为私有函数
        private bool capDriverConnect(IntPtr lwnd, short i)
        {
            return AviCapture.SendMessage(lwnd, AviCapture.WM_CAP_DRIVER_CONNECT, i, 0);
        }

        private bool capDriverDisconnect(IntPtr lwnd)
        {
            return AviCapture.SendMessage(lwnd, AviCapture.WM_CAP_DRIVER_DISCONNECT, 0, 0);
        }

        private bool capPreview(IntPtr lwnd, bool f)
        {
            return AviCapture.SendMessage(lwnd, AviCapture.WM_CAP_SET_PREVIEW, f, 0);
        }

        private bool capPreviewRate(IntPtr lwnd, short wMS)
        {
            return AviCapture.SendMessage(lwnd, AviCapture.WM_CAP_SET_PREVIEWRATE, wMS, 0);
        }

        private bool capSetCallbackOnFrame(IntPtr lwnd, AviCapture.FrameEventHandler lpProc)
        {
            return AviCapture.SendMessage(lwnd, AviCapture.WM_CAP_SET_CALLBACK_FRAME, 0, lpProc);
        }

        private bool capSetVideoFormat(IntPtr hCapWnd, ref AviCapture.BITMAPINFO BmpFormat, int CapFormatSize)
        {
            return AviCapture.SendMessage(hCapWnd, AviCapture.WM_CAP_SET_VIDEOFORMAT, CapFormatSize, ref BmpFormat);
        }

        private void FrameCallBack(IntPtr lwnd, IntPtr lpVHdr)
        {
            AviCapture.VIDEOHDR videoHeader = new AviCapture.VIDEOHDR();
            byte[] VideoData;
            videoHeader = (AviCapture.VIDEOHDR)AviCapture.GetStructure(lpVHdr, videoHeader);
            VideoData = new byte[videoHeader.dwBytesUsed];
            AviCapture.Copy(videoHeader.lpData, VideoData);
            if (this.RecievedFrame != null)
                this.RecievedFrame(VideoData);
        }
        private bool capOverlay(IntPtr lwnd, bool f)
        {
            return AviCapture.SendMessage(lwnd, AviCapture.WM_CAP_SET_OVERLAY, f, 0);
        } 
        #endregion

    }

    /// <summary>
    /// 视频辅助类
    /// </summary>
    internal class AviCapture
    {
        //通过调用acicap32.dll进行读取摄像头数据
        [DllImport("avicap32.dll")]
        public static extern IntPtr capCreateCaptureWindowA(byte[] lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nID);
        [DllImport("avicap32.dll")]
        public static extern bool capGetDriverDescriptionA(short wDriver, byte[] lpszName, int cbName, byte[] lpszVer, int cbVer);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, int lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, FrameEventHandler lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref BITMAPINFO lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref CAPDRIVERCAPS lParam);
        [DllImport("User32.dll")]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        [DllImport("avicap32.dll")]
        public static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);

        //部分常量
        public const int WM_USER = 0x400;
        public const int WS_CHILD = 0x40000000;
        public const int WS_VISIBLE = 0x10000000;
        public const int SWP_NOMOVE = 0x2;
        public const int SWP_NOZORDER = 0x4;
        public const int WM_CAP_DRIVER_CONNECT = WM_USER + 10;
        public const int WM_CAP_DRIVER_DISCONNECT = WM_USER + 11;
        public const int WM_CAP_SET_CALLBACK_FRAME = WM_USER + 5;
        public const int WM_CAP_SET_PREVIEW = WM_USER + 50;
        public const int WM_CAP_SET_PREVIEWRATE = WM_USER + 52;
        public const int WM_CAP_SET_VIDEOFORMAT = WM_USER + 45;
        public const int WM_CAP_SAVEDIB = WM_USER + 25;
        public const int WM_CAP_SET_OVERLAY = WM_USER + 51;
        public const int WM_CAP_GET_CAPS = WM_USER + 14;
        public const int WM_CAP_DLG_VIDEOFORMAT = WM_USER + 41;
        public const int WM_CAP_DLG_VIDEOSOURCE = WM_USER + 42;
        public const int WM_CAP_DLG_VIDEODISPLAY = WM_USER + 43;
        public const int WM_CAP_EDIT_COPY = WM_USER + 30;
        public const int WM_CAP_SET_SEQUENCE_SETUP = WM_USER + 64;
        public const int WM_CAP_GET_SEQUENCE_SETUP = WM_USER + 65;


        // 结构
        [StructLayout(LayoutKind.Sequential)]
        //VideoHdr
        public struct VIDEOHDR
        {
            [MarshalAs(UnmanagedType.I4)]
            public int lpData;
            [MarshalAs(UnmanagedType.I4)]
            public int dwBufferLength;
            [MarshalAs(UnmanagedType.I4)]
            public int dwBytesUsed;
            [MarshalAs(UnmanagedType.I4)]
            public int dwTimeCaptured;
            [MarshalAs(UnmanagedType.I4)]
            public int dwUser;
            [MarshalAs(UnmanagedType.I4)]
            public int dwFlags;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] dwReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        //BitmapInfoHeader
        public struct BITMAPINFOHEADER
        {
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biSize;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biWidth;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biHeight;
            [MarshalAs(UnmanagedType.I2)]
            public short biPlanes;
            [MarshalAs(UnmanagedType.I2)]
            public short biBitCount;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biCompression;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biSizeImage;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biXPelsPerMeter;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biYPelsPerMeter;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biClrUsed;
            [MarshalAs(UnmanagedType.I4)]
            public Int32 biClrImportant;
        }

        [StructLayout(LayoutKind.Sequential)]
        //BitmapInfo
        public struct BITMAPINFO
        {
            [MarshalAs(UnmanagedType.Struct, SizeConst = 40)]
            public BITMAPINFOHEADER bmiHeader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public Int32[] bmiColors;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CAPDRIVERCAPS
        {
            [MarshalAs(UnmanagedType.U2)]
            public UInt16 wDeviceIndex;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fHasOverlay;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fHasDlgVideoSource;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fHasDlgVideoFormat;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fHasDlgVideoDisplay;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fCaptureInitialized;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fDriverSuppliesPalettes;
            [MarshalAs(UnmanagedType.I4)]
            public int hVideoIn;
            [MarshalAs(UnmanagedType.I4)]
            public int hVideoOut;
            [MarshalAs(UnmanagedType.I4)]
            public int hVideoExtIn;
            [MarshalAs(UnmanagedType.I4)]
            public int hVideoExtOut;
        }


        public delegate void FrameEventHandler(IntPtr lwnd, IntPtr lpVHdr);

        // 公共函数
        public static object GetStructure(IntPtr ptr, ValueType structure)
        {
            return Marshal.PtrToStructure(ptr, structure.GetType());
        }

        public static object GetStructure(int ptr, ValueType structure)
        {
            return GetStructure(new IntPtr(ptr), structure);
        }

        public static void Copy(IntPtr ptr, byte[] data)
        {
            Marshal.Copy(ptr, data, 0, data.Length);
        }

        public static void Copy(int ptr, byte[] data)
        {
            Copy(new IntPtr(ptr), data);
        }

        public static int SizeOf(object structure)
        {
            return Marshal.SizeOf(structure);
        }
    }
}
