using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenNETCF.IoC.UI
{
    public class ShellReplacement : Form
    {
        public ShellReplacement()
            : this(true)
        {
        }

        public ShellReplacement(bool enableReplacement)
        {
            ShellReplacementEnabled = enableReplacement;
        }

        public bool ShellReplacementEnabled { get; set; }
    }
}
