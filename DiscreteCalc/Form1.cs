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

        BooleanFunctions boolean;

        int width;

        int tables = 0;

        TokenStream ts;

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
                ts = new TokenStream(function);
                boolean = new BooleanFunctions(tabPageOutput, tables, width, variables.Count);
                Highest();
            }
        }

        private string Highest()
        {
            string left = Higher();
            Token t = ts.Get();
            if(t.kind == '+')
            {
                left = boolean.Sum(left, Higher());
                return left;
            } else
            {
                ts.Putback(t);
                return left;
            }
        }

        private string Higher()
        {
            string left = Lower();
            Token t = ts.Get();
            if (t.kind == '@')
            {
                left = boolean.Mod2(left, Lower());
                return left;
            }
            else if (t.kind == '>')
            {
                left = boolean.Implication(left, Lower());
                return left;
            }
            else if (t.kind == '~')
            {
                //Произвести тильду left и нового Lower()
                //Вернуть var нового столбца
                //Вообще хз что это Оо
                return null;
            }
            else if (t.kind == '|')
            {
                left = boolean.Sheffer(left, Lower());
                return null;
            }
            else
            {
                ts.Putback(t);
                return left;
            }
        }

        private string Lower()
        {
            string left = Lowest();
            Token t = ts.Get();
            if (t.kind == '*')
            {
                left = boolean.Composition(left, Lowest());
                return left;
            }
            else if (t.kind == '$')
            {
                left = boolean.Pierce(left, Lowest());
                return left;
            }
            else
            {
                ts.Putback(t);
                return left;
            }
        }

        private string Lowest()
        {
            string left = Primary();
            Token t = ts.Get();
            if (t.kind == '_')
            {
                left = boolean.Negation(left);
                return left;
            }
            else
            {
                ts.Putback(t);
                return left;
            }
        }

        private string Primary()
        {
            Token t = ts.Get();
            if (t.kind == '(')
            {
                string left = Highest();
                t = ts.Get();
                if (t.kind != ')') MessageBox.Show("Where is )?");
                return left;
            }
            else if (t.index == -1)
            {
                return t.var;
            }
            else
            {
                MessageBox.Show("Primary error");
                return null;
            }
        }
        
        private void FillVariablesTables()
        {
            int index = variables.Count;
            foreach (string var in variables)
            {
                int height = (int)Math.Pow(2, index) / 2,
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
                    textBox.Location = new Point((variables.Count - index - 1) * width, 20 + i * 20);
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

    public class TokenStream
    {
        string function;
        public TokenStream(string _function)
        {
            function = _function;
            index = 0;
        }
        List<char> functionIcons = new List<char>()
            {
                '_', '*', '$', '@', '>', '~', '|', '+'
            };
        int index;
        
        public void Putback(Token t)
        {
            index-=t.kind=='?'?0:(t.kind==')' || t.kind=='(' ? 1:2);
            int _index = t.index;
            int i = 0;
            while(_index > 0)
            {
                _index /= 10;
                i++;
            }
            index -= _index;
        }

        public Token Get()
        {
            if(index >= function.Length)
            {
                //MessageBox.Show("Rofl request");
                return new Token('?');
            }
            //Число - операция
            //Скобка - скобка
            //Буква - переменная
            if(IsNum(function[index]))
            {
                string operation = "";
                while (index < function.Length && !functionIcons.Contains(function[index]))
                    operation += function[index++];
                operation += function[index++];
                return new Token(operation, true);
            }
            else if(IsLetter(function[index]))
            {
                string var = "";
                while (index < function.Length && IsLetter(function[index]))
                    var += function[index++];
                return new Token(var, false);
            }
            else if(index < function.Length && function[index] == '(' || function[index] == ')')
            {
                return new Token(function[index++]);
            } 
            else
            {
                MessageBox.Show("Rofl input, что-то не так с вводом");
                return new Token('?');
            }
        }

        private bool IsLetter(char v)
        {
            return (int)v >= 65 && (int)v <= 90 || (int)v >= 97 && (int)v <= 122;
        }

        private bool IsNum(char v)
        {
            return (int)v >= 48 && (int)v <= 57;
        }
    }

    public class Token
    {
        public int index;
        public char kind;
        public string var;

        public Token(char _kind)
        {
            kind = _kind;
            index = 0;
            var = "";
        }

        public Token(string str, bool isOperation)
        {
            var = "";
            index = 0;
            if(isOperation)
            {
                kind = str[str.Length - 1];
                index = int.Parse(str.Substring(0, str.Length - 1));
            } else
            {
                var = str;
                index = -1;
            }
        }
    }
}
