using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Visor_imagenes_WPF
{
    class Fondo_Class
    {
        private const uint SPI_SETDESKTOPWALLPAPER = 20;
        private const uint SPIF_UPDATEINIFILE = 0x01;
        private const uint SPIF_SENDWINICHANGE = 0x02;

        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_MONITORPOWER = 0xF170;

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, uint fwinIni);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public static void SetMonitorState(int state, IntPtr handle)
        {
            SendMessage(handle, WM_SYSCOMMAND, SC_MONITORPOWER, (int)state);
        }

        public static void SetDesktopWallpaper(String path) {
            if (!SystemParametersInfo(SPI_SETDESKTOPWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWINICHANGE)) {
                throw new Win32Exception();
            }
        }
    }
}
