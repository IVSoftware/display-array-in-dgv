using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Display_Array
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if(!(DesignMode | IsHandleInitialized))
            {
                IsHandleInitialized = true;
                InitDataGridView();
            }
        }

        // Set data source property once. Clear it, Add to it, but no reason to nullify it.
        BindingList<LogList> DataSource { get; } = new BindingList<LogList>();
        private void InitDataGridView()
        {
            dataGridView1.DataSource = DataSource;
            // Auto config columns by adding at least one Record.
            DataSource.Add(
                new LogList(
                    product: "LMG450",
                    // Four parts
                    partList: new string[]
                    {
                        "PCT2000",
                        "WCT100",
                        "ZEL-0812LN",
                        "EN61000-3-3/-11",
                    }
                ));
            DataSource.Add(
                new LogList(
                    product: "LMG600N", 
                    // Three parts
                    partList: new string[] 
                    { 
                        "LTC2280",
                        "BMS6815",
                        "ZEL-0812LN",
                    } 
                ));
            DataSource.Add(
                new LogList(
                    product: "Long Array",
                    // 75 parts
                    partList: Enumerable.Range(1, 75).Select(x => $"{ x }").ToArray()
                ));

            // Use string indexer to access columns.
            dataGridView1
                .Columns[nameof(LogList.Product)]
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1
                .Columns[nameof(LogList.PartList)]
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        // Handles the astonishingly corner case of
        // multiple calls to OnHandleCreated
        bool IsHandleInitialized = false;

        private void buttonModify_Click(object sender, EventArgs e)
        {
            DataSource[0][1] = "Changed";
            dataGridView1.Refresh();
        }
    }
    class LogList
    {
        public LogList(string product, string[] partList)
        {
            Product = product;
            _partList = partList;
        }
        public string Product { get; set; }

        public string this[int index]
        {
            get
            {
                if(index < _partList.Length)
                {
                    return _partList[index];
                }
                else
                {
                    // Need better error messaging here
                    return null;
                }
            }
            set
            {
                if(index < _partList.Length)
                {
                    _partList[index] = value;
                }
                else
                {
                    // Need better error messaging here
                }
            }
        }
        private string[] _partList;
        public string PartList => string.Join(",", _partList);
    }
}
