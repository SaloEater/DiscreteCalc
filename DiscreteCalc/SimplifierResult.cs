using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscreteCalc
{
    public partial class SimplifierResult : UserControl
    {
        public SimplifierResult(string left, string right, string action, string answer)
        {
            InitializeComponent();
            textBoxLeft.Text = left;
            textBoxRight.Text = right;
            labelAction.Text = action;
            textBoxAnswer.Text = answer;
        }
    }
}
