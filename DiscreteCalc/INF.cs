using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscreteCalc
{
    abstract class INF
    {
        protected List<List<int>> table;

        protected string answer;

        public void Put(List<List<int>> table)
        {
            this.table = table;
            answer = "";
        }

        public abstract string Count();
        public Panel FormUI()
        {
            Panel panel = new Panel();

            Label label = new Label();

            label.Text = GetType().Name + ": f(x)=";

            RichTextBox textBox = new RichTextBox();

            textBox.Text = answer;
            textBox.Location = new Point(0, label.Height);
            textBox.Size = new Size(answer.Length*4, answer.Length/2);
            
            panel.Size = new Size(textBox.Width, textBox.Height+label.Height);
            
            panel.Controls.Add(label);
            panel.Controls.Add(textBox);

            return panel;
        }

        public string Decode(List<string> variables)
        {
            string answer = this.answer;

            int i = 0;
            foreach (string var in variables) {
                answer = answer.Replace("<" + i++ + ">", var);
            }

            this.answer = answer;
            return answer;
        }
    }
}
