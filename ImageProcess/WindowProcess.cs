using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProcess
{
    public static  class WindowProcess
    {

        /// <summary>
        /// Set focus a windows current title contains 
        /// </summary>
        /// <param name="WindowTitle"></param>
        public static void SetFocus(string WindowTitle)
        {
            var title = AutoIt.AutoItX.WinGetTitle(WindowTitle);
            AutoIt.AutoItX.WinActivate(title);
        }

        public static void SetFocus(IntPtr WindowHandle)
        {
            AutoIt.AutoItX.WinActivate(WindowHandle);
        }


        /// <summary>
        /// Get Handle of a windows contains partial title
        /// </summary>
        /// <param name="WindowTitle"></param>
        /// <returns></returns>
        public static IntPtr GetHandle(string WindowTitle)
        {
            var title = AutoIt.AutoItX.WinGetTitle(WindowTitle);
            return AutoIt.AutoItX.WinGetHandle(title);
        }

        
       
    }
}
