namespace WinFormsLiteDbFromJson
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            panel2 = new Panel();
            dataGridView1 = new DataGridView();
            dbPathTextBox = new TextBox();
            button1 = new Button();
            panel1 = new Panel();
            FilteringGB = new GroupBox();
            FilterValueTB = new TextBox();
            AcceptFiltering = new Button();
            label3 = new Label();
            FilterOperationCb = new ComboBox();
            FilterOperationLable = new Label();
            FilterFieldLable = new Label();
            FilterFieldNameCb = new ComboBox();
            NameDbLable = new Label();
            CurrentDbName = new TextBox();
            AcceptNameBtn = new Button();
            NewDBNameLable = new Label();
            NewDbName = new TextBox();
            button2 = new Button();
            groupBox1 = new GroupBox();
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            checkBox1 = new CheckBox();
            openFileDialog1 = new OpenFileDialog();
            label2 = new Label();
            tableLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            FilteringGB.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel2, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(925, 325);
            tableLayoutPanel1.TabIndex = 18;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.AutoSize = true;
            panel2.Controls.Add(dataGridView1);
            panel2.Controls.Add(dbPathTextBox);
            panel2.Controls.Add(button1);
            panel2.Location = new Point(303, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(619, 319);
            panel2.TabIndex = 21;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.Location = new Point(3, 37);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(608, 273);
            dataGridView1.TabIndex = 10;
            // 
            // dbPathTextBox
            // 
            dbPathTextBox.Location = new Point(131, 6);
            dbPathTextBox.Name = "dbPathTextBox";
            dbPathTextBox.ReadOnly = true;
            dbPathTextBox.Size = new Size(423, 23);
            dbPathTextBox.TabIndex = 9;
            // 
            // button1
            // 
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(122, 29);
            button1.TabIndex = 8;
            button1.Text = "Указать папку БД";
            button1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(FilteringGB);
            panel1.Controls.Add(NameDbLable);
            panel1.Controls.Add(CurrentDbName);
            panel1.Controls.Add(AcceptNameBtn);
            panel1.Controls.Add(NewDBNameLable);
            panel1.Controls.Add(NewDbName);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(groupBox1);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(285, 319);
            panel1.TabIndex = 20;
            // 
            // FilteringGB
            // 
            FilteringGB.Controls.Add(FilterValueTB);
            FilteringGB.Controls.Add(AcceptFiltering);
            FilteringGB.Controls.Add(label3);
            FilteringGB.Controls.Add(FilterOperationCb);
            FilteringGB.Controls.Add(FilterOperationLable);
            FilteringGB.Controls.Add(FilterFieldLable);
            FilteringGB.Controls.Add(FilterFieldNameCb);
            FilteringGB.Location = new Point(10, 176);
            FilteringGB.Name = "FilteringGB";
            FilteringGB.Size = new Size(269, 138);
            FilteringGB.TabIndex = 25;
            FilteringGB.TabStop = false;
            FilteringGB.Text = "Фильтрация";
            // 
            // FilterValueTB
            // 
            FilterValueTB.Location = new Point(92, 81);
            FilterValueTB.Name = "FilterValueTB";
            FilterValueTB.Size = new Size(168, 23);
            FilterValueTB.TabIndex = 18;
            // 
            // AcceptFiltering
            // 
            AcceptFiltering.Location = new Point(6, 110);
            AcceptFiltering.Name = "AcceptFiltering";
            AcceptFiltering.Size = new Size(254, 22);
            AcceptFiltering.TabIndex = 17;
            AcceptFiltering.Text = "Применить";
            AcceptFiltering.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 85);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 15;
            label3.Text = "Значение";
            // 
            // FilterOperationCb
            // 
            FilterOperationCb.FormattingEnabled = true;
            FilterOperationCb.Location = new Point(92, 53);
            FilterOperationCb.Name = "FilterOperationCb";
            FilterOperationCb.Size = new Size(168, 23);
            FilterOperationCb.TabIndex = 14;
            // 
            // FilterOperationLable
            // 
            FilterOperationLable.AutoSize = true;
            FilterOperationLable.Location = new Point(6, 56);
            FilterOperationLable.Name = "FilterOperationLable";
            FilterOperationLable.Size = new Size(77, 15);
            FilterOperationLable.TabIndex = 13;
            FilterOperationLable.Text = "Тип фильтра";
            // 
            // FilterFieldLable
            // 
            FilterFieldLable.AutoSize = true;
            FilterFieldLable.Location = new Point(6, 29);
            FilterFieldLable.Name = "FilterFieldLable";
            FilterFieldLable.Size = new Size(54, 15);
            FilterFieldLable.TabIndex = 12;
            FilterFieldLable.Text = "Столбец";
            // 
            // FilterFieldNameCb
            // 
            FilterFieldNameCb.FormattingEnabled = true;
            FilterFieldNameCb.Location = new Point(92, 25);
            FilterFieldNameCb.Name = "FilterFieldNameCb";
            FilterFieldNameCb.Size = new Size(168, 23);
            FilterFieldNameCb.TabIndex = 11;
            // 
            // NameDbLable
            // 
            NameDbLable.AutoSize = true;
            NameDbLable.Location = new Point(16, 41);
            NameDbLable.Name = "NameDbLable";
            NameDbLable.Size = new Size(49, 15);
            NameDbLable.TabIndex = 24;
            NameDbLable.Text = "Имя БД";
            // 
            // CurrentDbName
            // 
            CurrentDbName.Location = new Point(102, 37);
            CurrentDbName.Name = "CurrentDbName";
            CurrentDbName.ReadOnly = true;
            CurrentDbName.Size = new Size(137, 23);
            CurrentDbName.TabIndex = 23;
            // 
            // AcceptNameBtn
            // 
            AcceptNameBtn.Location = new Point(240, 4);
            AcceptNameBtn.Name = "AcceptNameBtn";
            AcceptNameBtn.Size = new Size(30, 27);
            AcceptNameBtn.TabIndex = 22;
            AcceptNameBtn.Text = "Ок";
            AcceptNameBtn.UseVisualStyleBackColor = true;
            // 
            // NewDBNameLable
            // 
            NewDBNameLable.AutoSize = true;
            NewDBNameLable.Location = new Point(16, 10);
            NewDBNameLable.Name = "NewDBNameLable";
            NewDBNameLable.Size = new Size(85, 15);
            NewDBNameLable.TabIndex = 20;
            NewDBNameLable.Text = "Новое имя БД";
            // 
            // NewDbName
            // 
            NewDbName.Location = new Point(102, 6);
            NewDbName.Name = "NewDbName";
            NewDbName.Size = new Size(137, 23);
            NewDbName.TabIndex = 21;
            // 
            // button2
            // 
            button2.Location = new Point(16, 145);
            button2.Name = "button2";
            button2.Size = new Size(254, 31);
            button2.TabIndex = 19;
            button2.Text = "Обновить данные";
            button2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Location = new Point(16, 63);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(254, 79);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "Автозапрос";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 25);
            label1.Name = "label1";
            label1.Size = new Size(75, 15);
            label1.TabIndex = 5;
            label1.Text = "Период (мс)";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(114, 22);
            numericUpDown1.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 4;
            numericUpDown1.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(23, 51);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(211, 19);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Включить периодический запрос";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(926, 325);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            FilteringGB.ResumeLayout(false);
            FilteringGB.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private OpenFileDialog openFileDialog1;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private GroupBox FilteringGB;
        private TextBox FilterValueTB;
        private Button AcceptFiltering;
        private Label label3;
        private ComboBox FilterOperationCb;
        private Label FilterOperationLable;
        private Label FilterFieldLable;
        private ComboBox FilterFieldNameCb;
        private Label NameDbLable;
        private TextBox CurrentDbName;
        private Button AcceptNameBtn;
        private Label NewDBNameLable;
        private TextBox NewDbName;
        private Button button2;
        private GroupBox groupBox1;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private CheckBox checkBox1;
        private Panel panel2;
        private DataGridView dataGridView1;
        private TextBox dbPathTextBox;
        private Button button1;
    }
}
