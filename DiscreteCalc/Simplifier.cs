using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscreteCalc
{
    class Simplifier
    {
        string formula;
        TabPage tabPage;
        int globalY;
        public Simplifier(string formula, TabPage tabPage, int globalY)
        {
            this.formula = formula;
            this.tabPage = tabPage;
            this.globalY = globalY;
        }

        public void Start()
        {
            if (formula == "") {
                return;
            }
            SimplifierOperations operations = new SimplifierOperations();
            string numFormula = Utils.FromSymToBin(formula);

            Label label1 = new Label();
            label1.Text = "Исходная формула";
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(5, globalY);
            globalY += label1.Height;
            tabPage.Controls.Add(label1);

            TextBox textBox1 = new TextBox();
            textBox1.ReadOnly = true;
            textBox1.Location = new System.Drawing.Point(5, globalY);
            textBox1.Text = numFormula;

            Size size1 = TextRenderer.MeasureText(textBox1.Text, textBox1.Font);
            textBox1.Width = size1.Width;
            textBox1.Height = size1.Height;

            globalY += textBox1.Height;
            tabPage.Controls.Add(textBox1);

            List<List<char>> parts = new List<List<char>>();

            foreach (string str in numFormula.Split('+')) {
                List<char> part = str.Replace("*", "").ToList();
                parts.Add(part);
            }

            bool changes = false;

            while (!changes) {
                //left to right
                for (int i = 0; i < parts.Count - 1; i++) {
                    List<char> operation = new List<char>();
                    for (int j = i + 1; j < parts.Count; j++) {
                        string operationType = "";
                        if ((operation = operations.Glue(parts[i], parts[j])).Count != 0) {
                            operationType = "Склеивание";
                            Visualize(parts[i], parts[j], operation, operationType);
                            parts[i] = operation;
                            parts.RemoveAt(j);
                            j = i;
                            changes = true;
                            PrintFunctionValue(parts);
                        } else if ((operation = operations.Absorbtion(parts[i], parts[j])).Count != 0) {
                            operationType = "Поглощение";
                            Visualize(parts[i], parts[j], operation, operationType);
                            parts[i] = operation;
                            parts.RemoveAt(j);
                            j = i;
                            changes = true;
                            PrintFunctionValue(parts);
                        }
                    }
                }

                /*for (int i = 0; i < parts.Count - 1; i++) {
                    List<char> operation = new List<char>();
                    for (int j = i + 1; j < parts.Count; j++) {
                        string operationType = "";
                        operation = operations.GeneralGlue(parts[i], parts[j]);
                        operationType = "Обобщенное склеивание";
                        if (operation.Count == 0) {
                            continue;
                        }
                        Visualize(parts[i], parts[j], operation, operationType);
                        parts.Insert(i, operation);
                        //j = i;
                        changes = true;
                        PrintFunctionValue(parts);
                    }
                }*/

                //right to left
                for (int i = parts.Count - 1; i >= 0; i--) {
                    List<char> operation = new List<char>();
                    for (int j = i - 1; j >= 0; j--) {
                        string operationType = "";
                        if ((operation = operations.Absorbtion(parts[j], parts[i])).Count != 0) {
                            operationType = "Поглощение";
                            Visualize(parts[j], parts[i], operation, operationType);
                            parts[i] = operation;
                            parts.RemoveAt(j);
                            j = i;
                            changes = true;
                            PrintFunctionValue(parts);
                        }
                    }
                }
            }

            //PrintFunctionValue(parts);
        }

        private void PrintFunctionValue(List<List<char>> parts)
        {
            Label label = new Label();
            label.Text = "Текущая функция";
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(5, globalY);
            globalY += label.Height;
            tabPage.Controls.Add(label);

            TextBox textBox = new TextBox();
            textBox.ReadOnly = true;
            textBox.Location = new System.Drawing.Point(5, globalY);
            List<string> _parts = new List<string>();
            foreach (List<char> part in parts) {
                _parts.Add(string.Join("*", part));
            }
            textBox.Text = string.Join("+", _parts);

            Size size = TextRenderer.MeasureText(textBox.Text, textBox.Font);
            textBox.Width = size.Width;
            textBox.Height = size.Height;
            globalY += textBox.Height;
            tabPage.Controls.Add(textBox);
        }

        private void Visualize(List<char> left, List<char> right, List<char> answer, string operationType)
        {
            SimplifierResult result = new SimplifierResult(string.Join("*", left), string.Join("*", right), operationType, string.Join("*", answer));
            result.Location = new System.Drawing.Point(0, globalY);
            globalY += result.Height;
            tabPage.Controls.Add(result);
        }
    }
}
