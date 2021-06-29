using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Data.Gpu;
using GPU_Prices_Parser.Extensions;
using GPU_Prices_Parser.Parsers.Files;
using GPU_Prices_Parser.Parsers.Products;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace GPU_Prices_Parser
{
    internal partial class GpuForm : Form
    {
        private readonly IEnumerable<ProductParser> _productParsers;
        private readonly Store[] _storesToSearch;

        private readonly Dictionary<string, int> _gpuInTable;
        
        public GpuForm(GpuModel[] gpuModelToSearch, Store[] storesToSearch, IEnumerable<ProductParser> productParsers)
        {
            InitializeComponent();

            _storesToSearch = storesToSearch;
            _productParsers = productParsers;
            _gpuInTable = new Dictionary<string, int>();
            
            foreach (var store in _storesToSearch)
                storeList.Items.Add(store.ToString()!);

            ConfigurePlotView();
            ConfigureDataGridView();
            ConfigureComboBox(gpuModelToSearch
                .Select(model => (object) GpuModelHelper.GetRepresentation(model))
                .ToArray());
        }

        private void ConfigurePlotView()
        {
            var model = new PlotModel
            {
                Title = "GPU Prices",
                Subtitle = "Amazing GPU prices in different stores",
                PlotType = PlotType.XY,
                Background = OxyColors.White,
                IsLegendVisible = true
            };

            var startDate = DateTime.Now.AddDays(-10);
            var endDate = DateTime.Now;

            var minValue = DateTimeAxis.ToDouble(startDate);
            var maxValue = DateTimeAxis.ToDouble(endDate);

            model.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = minValue, Maximum = maxValue,
                IntervalType = DateTimeIntervalType.Days,
                MajorStep = 1,
                TitleColor = OxyColors.DarkRed,
                TextColor = OxyColors.Green,
                StringFormat = "dd/MMM",
                Title = "Date"
            });

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = 10000, Maximum = 100000,
                Title = "Price",
                MinorStep = 1000,
                TitleColor = OxyColors.DarkRed,
                TextColor = OxyColors.DarkOrange
            });

            plotView.Model = model;
        }

        private void ConfigureComboBox(object[] gpuToSearch)
        {
            gpuList.DropDownStyle = ComboBoxStyle.DropDownList;
            gpuList.Text = "Select GPU model";
            gpuList.Items.AddRange(gpuToSearch);
        }

        private void ConfigureDataGridView()
        {
            pricesTable.ColumnCount = _storesToSearch.Length + 1;
            pricesTable.TopLeftHeaderCell.Value = "GPU Name";

            pricesTable.Columns[0].Visible = false;

            for (int i = 1; i < _storesToSearch.Length + 1; i++)
                pricesTable.Columns[i].HeaderText = _storesToSearch[i - 1].Name.ToString();
        }

        private async Task RequestButtonClick(object sender, EventArgs e)
        {
            pricesTable.RowCount = 0;
            SetEnabledControls(false);

            _gpuInTable.Clear();
            
            plotView.Model.Title = (string) gpuList.SelectedItem + " Prices";
            plotView.Refresh();

            foreach (var store in _storesToSearch)
            {
                var productParser = _productParsers.FirstOrDefault(parser => parser.ParseStore == store.Name);
                
                if (productParser != null)
                {
                    var gpuInfo = await productParser!.ExtractAllInfo(
                            GpuModelHelper.GetModel((string) gpuList.SelectedItem), store);
                    
                    FillDataGridView(_storesToSearch.IndexOf(store) + 1, gpuInfo);
                    FileSaver.SaveGpuInfo(gpuInfo);
                }
                else throw new ArgumentException($"There is no corresponding parser for the store {store.Name}");
            }

            SetEnabledControls(true);
        }

        private void LoadLocalData(object sender, EventArgs eventArgs)
        {
            SetEnabledControls(false);

            var selectedGpu = GpuModelHelper.GetModel((string) gpuList.SelectedItem);
            
            foreach (var store in _storesToSearch)
            {
                var path = Path.Combine(Program.GpuDirPath, GpuModelHelper.GetRepresentation(selectedGpu), 
                    store.ToString(), DateTime.Now.ToShortDateString());

                try
                {
                    var info = FileParser.ParseGpuFiles(path);
                    FillDataGridView(_storesToSearch.IndexOf(store) + 1, info);
                }
                catch (Exception)
                {
                    MessageBox.Show($"No such GPU info on your disk about {store}");
                }
            }
            
            SetEnabledControls(true);
        }

        private async Task PricesTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            plotView.Model.Subtitle = (string) pricesTable.Rows[e.RowIndex].HeaderCell.Value;
            plotView.Model.Series.Clear();

            var selectedGpu = (GpuNote) pricesTable[0, e.RowIndex].Value;
            
            foreach (var store in _storesToSearch)
            {
                var lineSeries = new LineSeries {Title = store.ToString()};
                
                var otherGpus = await SimilarGpuFilesReader.Find(
                    selectedGpu.Gpu.SerialNumber,
                    Path.Combine(Program.GpuDirPath,
                        GpuModelHelper.GetRepresentation(selectedGpu.Gpu.Model),
                        store.ToString()));

                foreach (var gpu in otherGpus)
                    lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(gpu.DateStamp.Date), (double) gpu.Gpu.Price));

                plotView.Model.Series.Add(lineSeries); 
            }

            plotView.Model.InvalidatePlot(true);
        }

        private void SetEnabledControls(bool enable)
        {
            localDataBtn.Enabled = enable;
            sendRequestBtn.Enabled = enable;
            pricesTable.Enabled = enable;
        }
        
        private void FillDataGridView(int storeIndex, GpuNote[] gpuNotes)
        {
            if (pricesTable.Rows.Count == 0)
            {
                pricesTable.RowCount = gpuNotes.Length;
                for (int i = 0; i < pricesTable.RowCount; i++)
                {
                    FillRow(pricesTable.Rows[i], storeIndex, gpuNotes[i]);
                    _gpuInTable.Add(gpuNotes[i].Gpu.SerialNumber, i);
                }
            }
            else
            {
                for (int i = 0; i < gpuNotes.Length; i++)
                {
                    var serialNumber = gpuNotes[i].Gpu.SerialNumber;
                    if (_gpuInTable.ContainsKey(serialNumber))
                    {
                        pricesTable.Rows[_gpuInTable[serialNumber]].Cells[storeIndex].Value = gpuNotes[i].Gpu.Price;
                    }
                    else
                    {
                        pricesTable.Rows.Add(1);
                        FillRow(pricesTable.Rows[pricesTable.RowCount - 1], storeIndex, gpuNotes[i]);
                        _gpuInTable.Add(gpuNotes[i].Gpu.SerialNumber, i);
                    }
                }
            }
        }

        private static void FillRow(DataGridViewRow row, int storeIndex, GpuNote gpuNote)
        {
            row.HeaderCell.Value = $"{gpuNote.Gpu.Name} [{gpuNote.Gpu.SerialNumber}]";
            row.Cells[0].Value = gpuNote;
            row.Cells[storeIndex].Value = gpuNote.Gpu.Price;
        }
    }
}