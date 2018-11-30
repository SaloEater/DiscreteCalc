using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscreteCalc
{
    class BooleanFunctions
    {
        private TabPage tabPageOutput;
        private int tables,
            width,
            vars;

        public BooleanFunctions(TabPage tabPageOutput, int tables, int width, int vars)
        {
            this.tabPageOutput = tabPageOutput;
            this.tables = tables;
            this.width = width;
            this.vars = vars;
        }

        private void _textBox_Click(object sender, EventArgs e)
        {
            Console.WriteLine(((Control)sender).Location);
        }

        public string Sum(string left, string right)
        {
            string currentTableName = left + "+" + right;

            List<string> certainLeftTable = GetTableFor(left);
            List<string> certainRightTable = GetTableFor(right);

            RichTextBox _textBox = new RichTextBox();
            _textBox.Location = new Point(tables * width, 0);
            _textBox.Size = new Size(width, 40);
            _textBox.Text = "(" + left + ") + (" + right + ")";
            _textBox.Name = "main_" + currentTableName;
            _textBox.Click += _textBox_Click;
            tabPageOutput.Controls.Add(_textBox);
            for (int i = 0; i < (int)Math.Pow(2, vars); i++)
            {
                RichTextBox textBox = new RichTextBox();
                textBox.Location = new Point(tables * width, 40 + i * 20);
                textBox.Size = new Size(width, 20);
                textBox.Text = certainLeftTable[i] == "0" && certainRightTable[i] == "0" ? "0" : "1";
                textBox.Name = currentTableName + "_" + i;
                textBox.Click += _textBox_Click;
                tabPageOutput.Controls.Add(textBox);
            }
            tables++;
            return currentTableName;
        }

        public string Sheffer(string left, string right)
        {
            string currentTableName = left + "|" + right;

            List<string> certainLeftTable = GetTableFor(left);
            List<string> certainRightTable = GetTableFor(right);

            RichTextBox _textBox = new RichTextBox();
            _textBox.Location = new Point(tables * width, 0);
            _textBox.Size = new Size(width, 40);
            _textBox.Text = "(" + left + ") | (" + right + ")";
            _textBox.Name = "main_" + currentTableName;
            _textBox.Click += _textBox_Click;
            tabPageOutput.Controls.Add(_textBox);
            for (int i = 0; i < (int)Math.Pow(2, vars); i++)
            {
                RichTextBox textBox = new RichTextBox();
                textBox.Location = new Point(tables * width, 40 + i * 20);
                textBox.Size = new Size(width, 20);
                textBox.Text = certainLeftTable[i] == "1" && certainLeftTable[i] == "1" ? "0" : "1";
                textBox.Name = currentTableName + "_" + i;
                textBox.Click += _textBox_Click;
                tabPageOutput.Controls.Add(textBox);
            }
            tables++;
            return currentTableName;
        }

        public string Implication(string left, string right)
        {
            string currentTableName = left + ">" + right;

            List<string> certainLeftTable = GetTableFor(left);
            List<string> certainRightTable = GetTableFor(right);

            RichTextBox _textBox = new RichTextBox();
            _textBox.Location = new Point(tables * width, 0);
            _textBox.Size = new Size(width, 40);
            _textBox.Text = "(" + left + ") > (" + right + ")";
            _textBox.Name = "main_" + currentTableName;
            _textBox.Click += _textBox_Click;
            tabPageOutput.Controls.Add(_textBox);
            for (int i = 0; i < (int)Math.Pow(2, vars); i++)
            {
                RichTextBox textBox = new RichTextBox();
                textBox.Location = new Point(tables * width, 40 + i * 20);
                textBox.Size = new Size(width, 20);
                textBox.Text = int.Parse(certainLeftTable[i]) <= int.Parse(certainRightTable[i]) ? "1" : "0";
                textBox.Name = currentTableName + "_" + i;
                textBox.Click += _textBox_Click;
                tabPageOutput.Controls.Add(textBox);
            }
            tables++;
            return currentTableName;
        }

        public string Mod2(string left, string right)
        {
            string currentTableName = left + "@" + right;

            List<string> certainLeftTable = GetTableFor(left);
            List<string> certainRightTable = GetTableFor(right);

            RichTextBox _textBox = new RichTextBox();
            _textBox.Location = new Point(tables * width, 0);
            _textBox.Size = new Size(width, 40);
            _textBox.Text = "(" + left + ") @ (" + right + ")";
            _textBox.Name = "main_" + currentTableName;
            _textBox.Click += _textBox_Click;
            tabPageOutput.Controls.Add(_textBox);
            for (int i = 0; i < (int)Math.Pow(2, vars); i++)
            {
                RichTextBox textBox = new RichTextBox();
                textBox.Location = new Point(tables * width, 40 + i * 20);
                textBox.Size = new Size(width, 20);
                textBox.Text = certainLeftTable[i] == certainRightTable[i] ? "0" : "1";
                textBox.Name = currentTableName + "_" + i;
                textBox.Click += _textBox_Click;
                tabPageOutput.Controls.Add(textBox);
            }
            tables++;
            return currentTableName;
        }

        public string Pierce(string left, string right)
        {
            string currentTableName = left + "$" + right;

            List<string> certainLeftTable = GetTableFor(left);
            List<string> certainRightTable = GetTableFor(right);

            RichTextBox _textBox = new RichTextBox();
            _textBox.Location = new Point(tables * width, 0);
            _textBox.Size = new Size(width, 40);
            _textBox.Text = "(" + left + ") $ (" + right + ")";
            _textBox.Name = "main_" + currentTableName;
            _textBox.Click += _textBox_Click;
            tabPageOutput.Controls.Add(_textBox);
            for (int i = 0; i < (int)Math.Pow(2, vars); i++)
            {
                RichTextBox textBox = new RichTextBox();
                textBox.Location = new Point(tables * width, 40 + i * 20);
                textBox.Size = new Size(width, 20);
                textBox.Text = (int.Parse(Reverse(certainLeftTable[i])) * int.Parse(Reverse(certainRightTable[i]))).ToString();
                textBox.Name = currentTableName + "_" + i;
                textBox.Click += _textBox_Click;
                tabPageOutput.Controls.Add(textBox);
            }
            tables++;
            return currentTableName;
        }

        public string Composition(string left, string right)
        {
            string currentTableName = left + "*" + right;

            List<string> certainLeftTable = GetTableFor(left);
            List<string> certainRightTable = GetTableFor(right);

            RichTextBox _textBox = new RichTextBox();
            _textBox.Location = new Point(tables * width, 0);
            _textBox.Size = new Size(width, 40);
            _textBox.Text = "(" + left + ") * (" + right + ")";
            _textBox.Name = "main_" + currentTableName;
            _textBox.Click += _textBox_Click;
            tabPageOutput.Controls.Add(_textBox);
            for (int i = 0; i < (int)Math.Pow(2, vars); i++)
            {
                RichTextBox textBox = new RichTextBox();
                textBox.Location = new Point(tables * width, 40 + i * 20);
                textBox.Size = new Size(width, 20);
                textBox.Text = certainLeftTable[i] == "1" && certainRightTable[i] == "1" ? "1" : "0";
                textBox.Name = currentTableName + "_" + i;
                textBox.Click += _textBox_Click;
                tabPageOutput.Controls.Add(textBox);
            }
            tables++;
            return currentTableName;
        }

        public string Equality(string left, string right)
        {
            string currentTableName = left + "~" + right;

            List<string> certainLeftTable = GetTableFor(left);
            List<string> certainRightTable = GetTableFor(right);

            RichTextBox _textBox = new RichTextBox();
            _textBox.Location = new Point(tables * width, 0);
            _textBox.Size = new Size(width, 40);
            _textBox.Text = "(" + left + ") ~ (" + right + ")";
            _textBox.Name = "main_" + currentTableName;
            _textBox.Click += _textBox_Click;
            tabPageOutput.Controls.Add(_textBox);
            for (int i = 0; i < (int)Math.Pow(2, vars); i++) {
                RichTextBox textBox = new RichTextBox();
                textBox.Location = new Point(tables * width, 40 + i * 20);
                textBox.Size = new Size(width, 20);
                textBox.Text = certainLeftTable[i] == certainRightTable[i] ? "1" : "0";
                textBox.Name = currentTableName + "_" + i;
                textBox.Click += _textBox_Click;
                tabPageOutput.Controls.Add(textBox);
            }
            tables++;
            return currentTableName;
        }

        public string Negation(string tableName)
        {
            string currentTableName = tableName + '_';

            List<string> certainTable = GetTableFor(tableName);

            RichTextBox _textBox = new RichTextBox();
            _textBox.Location = new Point(tables * width, 0);
            _textBox.Size = new Size(width, 40);
            _textBox.Text = "(" + tableName + ")_";
            _textBox.Name = "main_" + currentTableName;
            _textBox.Click += _textBox_Click;
            tabPageOutput.Controls.Add(_textBox);
            for (int i = 0; i < (int)Math.Pow(2, vars); i++)
            {
                RichTextBox textBox = new RichTextBox();
                textBox.Location = new Point(tables * width, 40 + i * 20);
                textBox.Size = new Size(width, 20);
                textBox.Text = Reverse(certainTable[i]);
                textBox.Name = currentTableName + "_" + i;
                textBox.Click += _textBox_Click;
                tabPageOutput.Controls.Add(textBox);
            }
            tables++;
            return currentTableName;
        }

        public string Reverse(string v)
        {
            return v == "1" ? "0" : "1";
        }

        public List<string> GetTableFor(string tableName)
        {
            List<string> certainTable = new List<string>();

            for (int i = 0; i < Math.Pow(2, vars); i++)
            {
                foreach (Control c in tabPageOutput.Controls)
                {
                    if (c.Name == tableName + "_" + i)
                        certainTable.Add(c.Text);
                }
            }

            return certainTable;
        }
    }
}
