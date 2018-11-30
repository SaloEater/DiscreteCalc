using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscreteCalc
{
    class SDNF : INF
    {

        public override string Count()
        {
            string answer = "";

            int height = table[0].Count;

            for (int i = 0; i < height; i++) {
                int last = table.LastOrDefault()[i];
                if (last == 0) {
                    continue;
                }
                if (answer != "") {
                    answer += '+';
                }
                for (int j = 0; j < table.Count - 1; j++) {
                    string lastSymbol = table[j][i] == 0 ? "_" : "";
                    string element = "<" + j + ">" + lastSymbol;
                    if (j < table.Count - 2) {
                        element += '*';
                    }
                    answer += element;
                }
            }
            this.answer = answer;
            return answer;
        }
    }
}
