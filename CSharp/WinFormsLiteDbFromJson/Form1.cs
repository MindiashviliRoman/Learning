using System.Data;
using WinFormsLiteDbFromJson.Controllers;
using WinFormsLiteDbFromJson.Entities;
using WinFormsLiteDbFromJson.Filters;

namespace WinFormsLiteDbFromJson
{
    public partial class Form1 : Form
    {
        private string _lastDbPath;
        private string _lastDbName;
        private SetupData _setupData;
        private DataController _dataController;

        public Form1(SetupData setupData, DataController dataController)
        {
            _setupData = setupData;
            _dataController = dataController;
            _setupData.DbPathChanged += dbPathTextBoxChangeText;
            _dataController.DataUpdated += OnUpdatedData;
            _dataController.NwDbCreated += OnUpdatedData;
            _lastDbPath = _setupData.DbPath;
            _lastDbName = _setupData.DbName;
            InitializeComponent();

            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            //dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = _dataController.GetData<User>();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            //dataGridView1.
        }

        private void OnUpdatedData()
        {
            dataGridView1.DataSource = _dataController.GetData<User>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dbPathTextBox.Text = _lastDbPath;
            CurrentDbName.Text = _lastDbName;
            NewDbName.Text = _lastDbName;

            FilterFieldNameCb.DataSource = _dataController.GetFieldNames();
        }

        private void Form1_Close(object sender, EventArgs e)
        {
            if (_setupData == null)
                return;

            _setupData.DbPathChanged -= dbPathTextBoxChangeText;

            if (_dataController == null)
                return;

            _dataController.DataUpdated -= OnUpdatedData;
            _dataController.NwDbCreated -= OnUpdatedData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_setupData == null)
            {
                return;
            }
            using (var dialog = new FolderBrowserDialog())
            {
                if (string.IsNullOrEmpty(_lastDbPath))
                {
                    _lastDbPath = _setupData.StartDbPath;
                }
                dialog.InitialDirectory = _lastDbPath;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _setupData.OnDbPathChanged(dialog.SelectedPath);
                }
            }
        }

        private void dbPathTextBoxChangeText(string dbPath)
        {
            dbPathTextBox.Text = dbPath;
            _lastDbPath = dbPath;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (_setupData == null)
            {
                return;
            }
            _setupData.OnDataUpdateChanged(checkBox1.Checked);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (_setupData == null)
            {
                return;
            }

            var delayValue = (int)numericUpDown1.Value;
            _setupData.OnDelayChanged(delayValue);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _dataController.UpdateData();
        }

        private void AcceptNameBtn_Click(object sender, EventArgs e)
        {
            if (NewDbName.Text.Length > 0)
            {
                _lastDbName = NewDbName.Text;
                CurrentDbName.Text = _lastDbName;
                _setupData.OnDbNameChanged(_lastDbName);
            }
        }

        private void FilterFieldNameCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dataController.FillFiltersCombobox(FilterOperationCb, FilterFieldNameCb.SelectedItem.ToString());
        }

        private void AcceptFiltering_Click(object sender, EventArgs e)
        {
            var propName = FilterFieldNameCb.SelectedItem.ToString();

            var filter = FilterOperationCb.SelectedItem as FilterOperation<String>;

            filter.SetFilter(FilterValueTB.Text);

            var prop = typeof(User).GetProperty(propName);

            dataGridView1.DataSource = _dataController.GetFiltredData<User>(filter, propName);
        }

    }
}
