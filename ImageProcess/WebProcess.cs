using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatiN.Core;

namespace AutomationProcess
{
    public static class WebProcess
    {

        public static WatiN.Core.IE Browser(string Title)
        {
            return new IE();
        }
    }
}
