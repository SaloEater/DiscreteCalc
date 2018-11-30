using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscreteCalc
{
    public partial class Form1 : Form
    {
        List<string> variables;
        List<char> functionIcons;
        string function;
        string lastTableName;

        Simplifier simplifier;

        int width;

        int tables = 0;

        int globalY = 0;

        public Form1()
        {
            InitializeComponent();
            functionIcons = new List<char>()
            {
                '_', '*', '$', '@', '>', '~', '|', '+'
            };
            
            this.ActiveControl = textBoxInput;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (textBoxInput.Text == "")
            {
                MessageBox.Show("Надо ввести формулу");
            }
            else
            {
                tabPageOutput.Controls.Clear();
                variables = new List<string>();
                tables = 0;
                width = int.Parse(textBoxWidth.Text);
                tabControl1.SelectedIndex = 1;
                InitializeVariables(textBoxInput.Text);
                FillVariablesTables(); //Сохранение переменных
                ReforgeToNumber(); //Оцифровка функций
                lastTableName = (new DiscreteCalc(function, tabPageOutput, tables, width, variables.Count)).Start();
                globalY = (int)Math.Pow(2, variables.Count) * 20 + 40;
                string func = DeploySDNF();
                DeploySKNF();
                simplifier = new Simplifier(func, tabPageOutput, globalY);
                simplifier.Start();
            }
        }

        private string DeploySDNF()
        {
            if (!checkBoxSDNF.Checked) {
                return "";
            }
            return DeployNF(new SDNF());
        }

        private void DeploySKNF()
        {
            if (!checkBoxSKNF.Checked) {
                return;
            }
            DeployNF(new SKNF());
        }

        private string DeployNF(INF iNF)
        {
            List<List<int>> table = new List<List<int>>();

            for(int i = 0; i < variables.Count; i++) {
                List<int> _list = new List<int>();
                for (int j = 0; j < (int)Math.Pow(2, variables.Count); j++) {
                    _list.Add(GetValueFromTextBox(variables[i] + '_' + j));
                }
                table.Add(_list);
            }


            List<int> list = new List<int>();
            for (int j = 0; j < (int)Math.Pow(2, variables.Count); j++) {
                list.Add(GetValueFromTextBox(lastTableName + '_' + j));
            }
            table.Add(list);

            iNF.Put(table);
            iNF.Count();
            string answer = iNF.Decode(variables);
            Panel panel = iNF.FormUI();

            panel.Location = new Point(5, globalY);
            globalY += panel.Height;

            tabPageOutput.Controls.Add(panel);
            panel.BringToFront();

            return answer;
        }

        private int GetValueFromTextBox(string name)
        {
            foreach(Control c in tabPageOutput.Controls) {
                if (c.Name.Equals(name)) {
                    return int.Parse(c.Text);
                }
            }
            throw new KeyNotFoundException(name + " не найден");
        }

        private void FillVariablesTables()
        {
            int index = variables.Count;
            int j = index - 1;
            foreach (string var in variables)
            {
                int height = (int)Math.Pow(2, j--),
                    buf = height;
                bool zero = true;
                RichTextBox _textBox = new RichTextBox();
                _textBox.Location = new Point((variables.Count - index)*width, 0);
                _textBox.Size = new Size(width, 40);
                _textBox.Text = var;
                _textBox.Name = "main_" + var;
                index--;
                tabPageOutput.Controls.Add(_textBox);
                for (int i = 0; i < (int)Math.Pow(2, variables.Count); i++)
                {
                    RichTextBox textBox = new RichTextBox();
                    textBox.Location = new Point((variables.Count - index - 1) * width, 40 + i * 20);
                    textBox.Size = new Size(width, 20);
                    textBox.Text = zero?"0":"1";
                    textBox.Name = var + "_" + i;
                    tabPageOutput.Controls.Add(textBox);
                    buf--;
                    if(buf <= 0)
                    {
                        buf = height;
                        zero = !zero;
                    }
                }
                tables++;
            }
        }

        private void InitializeVariables(string text)
        {
            function = text.Split('=')[1].Replace(" ", "");
            text = text.Split('=')[0].Split('(')[1].Split(')')[0];
            string[] vars = text.Split(',');
            for (int i = 0; i < vars.Length; i++)
            {
                variables.Add(vars[i].Replace(" ", ""));
            }
        }

        private void ReforgeToNumber()
        {
            Dictionary<char, int> funcs = new Dictionary<char, int>();
            for(int i = 0; i < function.Length; i++)
            {
                char ch = function[i];
                if(functionIcons.Contains(ch))
                {
                    if(funcs.ContainsKey(ch))
                    {
                        funcs[ch]++;                        
                    } else
                    {
                        funcs.Add(ch, 0);
                    }
                    function = function.Insert(i, funcs[ch].ToString());
                    i++;
                }
            }
        }
    }    
}
