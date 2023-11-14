using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSDeckImporter
{
    public class WindowHelper
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public static Coordinate GetMouseCoordinate(string name)
        {
            IntPtr hWnd = FindWindow(null, name);
            RECT rc;
            GetWindowRect(hWnd, out rc);
            Rectangle rect = new Rectangle(rc.left, rc.top, rc.right, rc.bottom);


            Point p = Cursor.Position;

            Coordinate result = new Coordinate();
            if (rect.Contains(p))
            {
                result.X = Cursor.Position.X - rc.left;
                result.Y = Cursor.Position.Y - rc.top;
            }

            return result;
        }
    }

    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
