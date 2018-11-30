using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscreteCalc
{
    class DiscreteCalc
    {
        BooleanFunctions boolean;

        TokenStream ts;

        public DiscreteCalc(string function, TabPage output, int tables, int width, int variablesAmount)
        {
            ts = new TokenStream(function);
            boolean = new BooleanFunctions(output, tables, width, variablesAmount);
        }

        public string Start()
        {
            return Highest();
        }

        private string Highest()
        {
            string left = Higher();
            Token t = ts.Get();
            if (t.kind == '+') {
                left = boolean.Sum(left, Higher());
                return left;
            } else {
                ts.Putback(t);
                return left;
            }
        }

        private string Higher()
        {
            string left = Lower();
            Token t = ts.Get();
            if (t.kind == '@') {
                left = boolean.Mod2(left, Lower());
                return left;
            } else if (t.kind == '>') {
                left = boolean.Implication(left, Lower());
                return left;
            } else if (t.kind == '~') {
                left = boolean.Equality(left, Lower());
                return left;
            } else if (t.kind == '|') {
                left = boolean.Sheffer(left, Lower());
                return left;
            } else {
                ts.Putback(t);
                return left;
            }
        }

        private string Lower()
        {
            string left = Lowest();
            Token t = ts.Get();
            if (t.kind == '*') {
                left = boolean.Composition(left, Lowest());
                return left;
            } else if (t.kind == '$') {
                left = boolean.Pierce(left, Lowest());
                return left;
            } else {
                ts.Putback(t);
                return left;
            }
        }

        private string Lowest()
        {
            string left = Primary();
            Token t = ts.Get();
            if (t.kind == '_') {
                left = boolean.Negation(left);
                return left;
            } else {
                ts.Putback(t);
                return left;
            }
        }

        private string Primary()
        {
            Token t = ts.Get();
            if (t.kind == '(') {
                string left = Highest();
                t = ts.Get();
                if (t.kind != ')') MessageBox.Show("Не хватает )?");
                return left;
            } else if (t.index == -1) {
                return t.var;
            } else {
                MessageBox.Show("Primary error");
                return null;
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
            index -= t.kind == '?' ? 0 : (t.kind == ')' || t.kind == '(' ? 1 : 2);
            int _index = t.index;
            int i = 0;
            while (_index > 0) {
                _index /= 10;
                i++;
            }
            index -= _index;
        }

        public Token Get()
        {
            if (index >= function.Length) {
                //MessageBox.Show("Rofl request");
                return new Token('?');
            }
            //Число - операция
            //Скобка - скобка
            //Буква - переменная
            if (IsNum(function[index])) {
                string operation = "";
                while (index < function.Length && !functionIcons.Contains(function[index]))
                    operation += function[index++];
                operation += function[index++];
                return new Token(operation, true);
            } else if (IsLetter(function[index])) {
                string var = "";
                while (index < function.Length && IsLetter(function[index]))
                    var += function[index++];
                return new Token(var, false);
            } else if (index < function.Length && function[index] == '(' || function[index] == ')') {
                return new Token(function[index++]);
            } else {
                MessageBox.Show("Не получилось получить символ из потока ввода");
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
            if (isOperation) {
                kind = str[str.Length - 1];
                index = int.Parse(str.Substring(0, str.Length - 1));
            } else {
                var = str;
                index = -1;
            }
        }
    }
}
