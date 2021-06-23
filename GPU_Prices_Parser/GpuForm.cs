using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Data.Gpu;
using GPU_Prices_Parser.Parsers.Products;
using OxyPlot;
using OxyPlot.Series;

namespace GPU_Prices_Parser
{
    internal partial class GpuForm : Form
    {
        private readonly IEnumerable<IProductParser> _productParsers;
        private readonly Store[] _storesToSearch;
        private readonly WebProvider _webProvider;
        private readonly LineSeries _lineSeries;

        public GpuForm()
        {
        }

        public GpuForm(GpuModel[] gpuModelToSearch, Store[] storesToSearch, IEnumerable<IProductParser> productParsers)
        {
            InitializeComponent();

            _webProvider = new WebProvider();
            _storesToSearch = storesToSearch;
            _productParsers = productParsers;

            var pm = new PlotModel
            {
                Title = "GPU Prices",
                Subtitle = "Amazing GPU prices in different stores",
                PlotType = PlotType.Cartesian,
                Background = OxyColors.White,
                IsLegendVisible = true
            };

            _lineSeries = new LineSeries();
            pm.Series.Add(_lineSeries);
            plotView.Model = pm;

            foreach (var store in _storesToSearch)
                storeList.Items.Add(store.ToString()!);

            _webProvider.DownloadCompleted += OnDownloadComplete;

            ConfigureDataGridView();
            ConfigureComboBox(gpuModelToSearch
                .Select(model => (object) GpuModelHelper.GetRepresentation(model))
                .ToArray());
        }

        private void ConfigureComboBox(object[] gpuToSearch)
        {
            gpuList.DropDownStyle = ComboBoxStyle.DropDownList;
            gpuList.Text = "Select GPU model";
            gpuList.Items.AddRange(gpuToSearch);
        }

        private void ConfigureDataGridView()
        {
            pricesTable.ColumnCount = _storesToSearch.Length;
            pricesTable.TopLeftHeaderCell.Value = "GPU Name";

            for (int i = 0; i < _storesToSearch.Length; i++)
                pricesTable.Columns[i].HeaderText = _storesToSearch[i].Name.ToString();
        }

        private void FillDataGridView(Store store, GpuNote[] gpuNotes)
        {
            lock (pricesTable)
            {
                if (pricesTable.Enabled)
                {
                    pricesTable.RowCount += gpuNotes.Length;
                    int j = 0;
                    for (int i = pricesTable.RowCount - gpuNotes.Length; i < pricesTable.RowCount; i++)
                    {
                        pricesTable.Rows[i].HeaderCell.Value = gpuNotes[j].Gpu.FullName;
                        pricesTable[store.Name.ToString(), i].Value = gpuNotes[j].Gpu.Price;
                        j++;
                    }
                }
                else
                {
                    pricesTable.Enabled = true;
                    pricesTable.RowCount = gpuNotes.Length;
                    
                    for (int i = 1; i < pricesTable.RowCount; i++)
                    {
                        pricesTable.Rows[i].HeaderCell.Value = gpuNotes[i].Gpu.FullName;
                        pricesTable[store.Name.ToString(), i].Value = gpuNotes[i].Gpu.Price;
                    }
                }
            }
        }

        private void OnDownloadComplete(Store store, IDocument document)
        {
            var productParser = _productParsers.FirstOrDefault(parser => parser.ParseStore == store.Name);

            if (productParser != null)
            {
                var gpuModel = GpuModelHelper.Parse((string) gpuList.SelectedItem);
                var gpuInfo = productParser.ExtractAllInfo(gpuModel, document);

                FillDataGridView(store, gpuInfo);
                
                Parallel.ForEach(gpuInfo, async info =>
                {
                    var saveFolderPath = Path.Combine(Program.GpuDirPath, 
                        GpuModelHelper.GetRepresentation(info.Gpu.Model), info.Gpu.CellingStore.ToString());
                    await FileSaver.SaveDataAsync(info, saveFolderPath, info.ToString());
                });
            }
            else throw new ArgumentException($"There is no corresponding parser for the store {store.Name}");

            _lineSeries.Points.Add(new DataPoint(50, 50));
            plotView.Refresh();

            gpuList.Enabled = true;
        }

        private void RequestButtonClick(object sender, EventArgs e)
        {
            gpuList.Enabled = false;
            pricesTable.RowCount = 0;
            pricesTable.Enabled = false;
            
            plotView.Model.Title = (string) gpuList.SelectedItem + " Prices";
            plotView.Refresh();

            foreach (var store in _storesToSearch)
            foreach (var url in store.Urls)
                _webProvider.SendRequest(url, store);
        }

        private void PricesTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // индекс левых хэдеров -1, верхних хэдеров -1
        }
    }
}