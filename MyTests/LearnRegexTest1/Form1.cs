using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LearnRegexTest1
{
    public partial class Form1 : Form
    {
        Regex _regex = null;
        private Match _match = null;
        private RegexOptions _regexOptions = RegexOptions.None;

        public Form1()
        {
            InitializeComponent();
        }

    }
}
