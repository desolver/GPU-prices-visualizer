using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Extensions;
using GPU_Prices_Parser.Parsers.Products;
using OxyPlot;
using OxyPlot.Series;

namespace GPU_Prices_Parser
{
    internal partial class GpuForm : Form
    {
        private readonly IEnumerable<IProductParser> _productParsers;
        private readonly Filter _filter;
        private readonly Store[] _storesToSearch;
        private readonly WebProvider _webProvider;
        private readonly LineSeries _lineSeries;
        
        public GpuForm() { }

        public GpuForm(GpuModel[] gpuModelToSearch, Store[] storesToSearch, IEnumerable<IProductParser> productParsers, Filter filter)
        {
            InitializeComponent();
            
            _filter = filter;
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

            //_webProvider.DownloadCompleted += OnDownloadComplete;

            
            ConfigureDataGridView();
            ConfigureComboBox(gpuModelToSearch.Select(model => model.ToString()).ToArray());
        }

        private void ConfigureComboBox(string[] gpuToSearch)
        {
            gpuList.DropDownStyle = ComboBoxStyle.DropDownList;
            gpuList.Text = "Select GPU model";
            gpuList.Items.AddRange(gpuToSearch.Select(model =>
            {
                model = model.Replace('_', ' ');
                return (object) model.ToUpperInvariant();
            }).ToArray());
        }

        private void ConfigureDataGridView()
        {
            pricesTable.ColumnCount = _storesToSearch.Length;
            pricesTable.TopLeftHeaderCell.Value = "GPU Name";

            for (int i = 0; i < _storesToSearch.Length; i++)
                pricesTable.Columns[i].HeaderText = _storesToSearch[i].Name.ToString();
        }
        
        private void FillDataGridView()
        {
            pricesTable.RowCount = 3;
        }

        private void OnDownloadComplete(Store store, IDocument document)
        {
            var productParser = _productParsers.FirstOrDefault(parser => parser.ParseStore == store.Name);
            
            if (productParser != null)
            {
                var gpuInfo = productParser.ParseAllInfo(GpuModelHelper.Parse((string) gpuList.SelectedItem), document);
                var tasks = gpuInfo.Select(info =>
                    Task.Run(() => FileSaver.SaveDataAsync(info, Program.GpuDirPath, info.Gpu.FullName + info.DateStamp)));
            }
            else throw new ArgumentException($"There is no corresponding parser for the store {store.Name}");
            
            _lineSeries.Points.Add(new DataPoint(50, 50));
            plotView.Refresh();
            
            gpuList.Enabled = true;
        }

        private void RequestButtonClick(object sender, EventArgs e)
        {
            // if (gpuName.Text.Length == 0)
            // {
            //     MessageBox.Show("Input line is empty..."); 
            //     return;
            // }
            
            // if (!_filter.IsMatchRegex(gpuName.Text))
            // {
            //     MessageBox.Show("Input string doesn't match the rules..."); 
            //     return;
            // }
            
            gpuList.Enabled = false;
            
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
