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
            button1 = new Button();
            openFileDialog1 = new OpenFileDialog();
            label2 = new Label();
            dbPathTextBox = new TextBox();
            checkBox1 = new CheckBox();
            groupBox1 = new GroupBox();
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            button2 = new Button();
            NewDbName = new TextBox();
            NewDBNameLable = new Label();
            dataGridView1 = new DataGridView();
            AcceptNameBtn = new Button();
            CurrentDbName = new TextBox();
            NameDbLable = new Label();
            FilterFieldNameCb = new ComboBox();
            FilterFieldLable = new Label();
            FilterOperationLable = new Label();
            FilterOperationCb = new ComboBox();
            label3 = new Label();
            FilteringGB = new GroupBox();
            FilterValueTB = new TextBox();
            AcceptFiltering = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            FilteringGB.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(284, 7);
            button1.Name = "button1";
            button1.Size = new Size(122, 29);
            button1.TabIndex = 0;
            button1.Text = "Указать папку БД";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 0;
            // 
            // dbPathTextBox
            // 
            dbPathTextBox.Location = new Point(412, 10);
            dbPathTextBox.Name = "dbPathTextBox";
            dbPathTextBox.ReadOnly = true;
            dbPathTextBox.Size = new Size(423, 23);
            dbPathTextBox.TabIndex = 1;
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
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Location = new Point(12, 67);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(254, 79);
            groupBox1.TabIndex = 4;
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
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // button2
            // 
            button2.Location = new Point(12, 149);
            button2.Name = "button2";
            button2.Size = new Size(254, 31);
            button2.TabIndex = 5;
            button2.Text = "Обновить данные";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // NewDbName
            // 
            NewDbName.Location = new Point(98, 10);
            NewDbName.Name = "NewDbName";
            NewDbName.Size = new Size(137, 23);
            NewDbName.TabIndex = 6;
            // 
            // NewDBNameLable
            // 
            NewDBNameLable.AutoSize = true;
            NewDBNameLable.Location = new Point(12, 14);
            NewDBNameLable.Name = "NewDBNameLable";
            NewDBNameLable.Size = new Size(85, 15);
            NewDBNameLable.TabIndex = 6;
            NewDBNameLable.Text = "Новое имя БД";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(284, 52);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(551, 266);
            dataGridView1.TabIndex = 7;
            // 
            // AcceptNameBtn
            // 
            AcceptNameBtn.Location = new Point(236, 8);
            AcceptNameBtn.Name = "AcceptNameBtn";
            AcceptNameBtn.Size = new Size(30, 27);
            AcceptNameBtn.TabIndex = 8;
            AcceptNameBtn.Text = "Ок";
            AcceptNameBtn.UseVisualStyleBackColor = true;
            AcceptNameBtn.Click += AcceptNameBtn_Click;
            // 
            // CurrentDbName
            // 
            CurrentDbName.Location = new Point(98, 41);
            CurrentDbName.Name = "CurrentDbName";
            CurrentDbName.ReadOnly = true;
            CurrentDbName.Size = new Size(137, 23);
            CurrentDbName.TabIndex = 9;
            // 
            // NameDbLable
            // 
            NameDbLable.AutoSize = true;
            NameDbLable.Location = new Point(12, 45);
            NameDbLable.Name = "NameDbLable";
            NameDbLable.Size = new Size(49, 15);
            NameDbLable.TabIndex = 10;
            NameDbLable.Text = "Имя БД";
            // 
            // FilterFieldNameCb
            // 
            FilterFieldNameCb.FormattingEnabled = true;
            FilterFieldNameCb.Location = new Point(92, 25);
            FilterFieldNameCb.Name = "FilterFieldNameCb";
            FilterFieldNameCb.Size = new Size(168, 23);
            FilterFieldNameCb.TabIndex = 11;
            FilterFieldNameCb.SelectedIndexChanged += FilterFieldNameCb_SelectedIndexChanged;
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
            // FilterOperationLable
            // 
            FilterOperationLable.AutoSize = true;
            FilterOperationLable.Location = new Point(6, 56);
            FilterOperationLable.Name = "FilterOperationLable";
            FilterOperationLable.Size = new Size(77, 15);
            FilterOperationLable.TabIndex = 13;
            FilterOperationLable.Text = "Тип фильтра";
            // 
            // FilterOperationCb
            // 
            FilterOperationCb.FormattingEnabled = true;
            FilterOperationCb.Location = new Point(92, 53);
            FilterOperationCb.Name = "FilterOperationCb";
            FilterOperationCb.Size = new Size(168, 23);
            FilterOperationCb.TabIndex = 14;
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
            // FilteringGB
            // 
            FilteringGB.Controls.Add(FilterValueTB);
            FilteringGB.Controls.Add(AcceptFiltering);
            FilteringGB.Controls.Add(label3);
            FilteringGB.Controls.Add(FilterOperationCb);
            FilteringGB.Controls.Add(FilterOperationLable);
            FilteringGB.Controls.Add(FilterFieldLable);
            FilteringGB.Controls.Add(FilterFieldNameCb);
            FilteringGB.Location = new Point(6, 180);
            FilteringGB.Name = "FilteringGB";
            FilteringGB.Size = new Size(269, 138);
            FilteringGB.TabIndex = 17;
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
            AcceptFiltering.Click += AcceptFiltering_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(847, 330);
            Controls.Add(FilteringGB);
            Controls.Add(NameDbLable);
            Controls.Add(CurrentDbName);
            Controls.Add(AcceptNameBtn);
            Controls.Add(dataGridView1);
            Controls.Add(NewDBNameLable);
            Controls.Add(NewDbName);
            Controls.Add(button2);
            Controls.Add(groupBox1);
            Controls.Add(dbPathTextBox);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            FilteringGB.ResumeLayout(false);
            FilteringGB.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private OpenFileDialog openFileDialog1;
        private Label label2;
        private TextBox dbPathTextBox;
        private CheckBox checkBox1;
        private GroupBox groupBox1;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private Button button2;
        private TextBox NewDbName;
        private Label NewDBNameLable;
        private DataGridView dataGridView1;
        private Button AcceptNameBtn;
        private TextBox CurrentDbName;
        private Label NameDbLable;
        private ComboBox FilterFieldNameCb;
        private Label FilterFieldLable;
        private Label FilterOperationLable;
        private ComboBox FilterOperationCb;
        private Label label3;
        private GroupBox FilteringGB;
        private Button AcceptFiltering;
        private TextBox FilterValueTB;
    }
}
