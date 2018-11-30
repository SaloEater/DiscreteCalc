namespace DiscreteCalc
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageInput = new System.Windows.Forms.TabPage();
            this.checkBoxSKNF = new System.Windows.Forms.CheckBox();
            this.checkBoxSDNF = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPageInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageInput);
            this.tabControl1.Controls.Add(this.tabPageOutput);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageInput
            // 
            this.tabPageInput.Controls.Add(this.checkBoxSKNF);
            this.tabPageInput.Controls.Add(this.checkBoxSDNF);
            this.tabPageInput.Controls.Add(this.label1);
            this.tabPageInput.Controls.Add(this.textBoxWidth);
            this.tabPageInput.Controls.Add(this.buttonConfirm);
            this.tabPageInput.Controls.Add(this.textBoxInput);
            this.tabPageInput.Location = new System.Drawing.Point(4, 22);
            this.tabPageInput.Name = "tabPageInput";
            this.tabPageInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInput.Size = new System.Drawing.Size(792, 424);
            this.tabPageInput.TabIndex = 0;
            this.tabPageInput.Text = "Формула";
            this.tabPageInput.UseVisualStyleBackColor = true;
            // 
            // checkBoxSKNF
            // 
            this.checkBoxSKNF.AutoSize = true;
            this.checkBoxSKNF.Location = new System.Drawing.Point(596, 79);
            this.checkBoxSKNF.Name = "checkBoxSKNF";
            this.checkBoxSKNF.Size = new System.Drawing.Size(59, 17);
            this.checkBoxSKNF.TabIndex = 5;
            this.checkBoxSKNF.Text = "СКНФ";
            this.checkBoxSKNF.UseVisualStyleBackColor = true;
            // 
            // checkBoxSDNF
            // 
            this.checkBoxSDNF.AutoSize = true;
            this.checkBoxSDNF.Location = new System.Drawing.Point(596, 56);
            this.checkBoxSDNF.Name = "checkBoxSDNF";
            this.checkBoxSDNF.Size = new System.Drawing.Size(61, 17);
            this.checkBoxSDNF.TabIndex = 4;
            this.checkBoxSDNF.Text = "СДНФ";
            this.checkBoxSDNF.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(593, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ширина ячейки";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(683, 34);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(100, 20);
            this.textBoxWidth.TabIndex = 2;
            this.textBoxWidth.Text = "100";
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(9, 34);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(31, 23);
            this.buttonConfirm.TabIndex = 1;
            this.buttonConfirm.Text = "Ok";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(9, 7);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(775, 20);
            this.textBoxInput.TabIndex = 0;
            this.textBoxInput.Text = "f(a,b,c,d)=(c_*d|a*b_)>((b*c)_)$((a*b~a*d)_)";
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.AutoScroll = true;
            this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(792, 424);
            this.tabPageOutput.TabIndex = 1;
            this.tabPageOutput.Text = "Таблица истинности";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPageInput.ResumeLayout(false);
            this.tabPageInput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageInput;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.CheckBox checkBoxSKNF;
        private System.Windows.Forms.CheckBox checkBoxSDNF;
    }
}

