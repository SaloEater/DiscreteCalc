using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteCalc
{
    public static class Utils
    {
        public static string FromSymToBin(string _formula)
        {
            char[] formula = _formula.ToArray();
            for (int i = 0; i < formula.Length; i++) {
                if (formula[i] == '*' || formula[i] == ' ' || formula[i] == '+') {
                    continue;
                }
                if ((i + 1 < formula.Length) && formula[i + 1] == '_') {
                    formula[i] = '0';
                    formula[i + 1] = ' ';
                } else {
                    formula[i] = '1';
                }
            }
            _formula = string.Join("", formula).Replace(" ", "");
            return _formula;
        }
    }
}
