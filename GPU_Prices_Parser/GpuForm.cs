﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AngleSharp.Dom;
using GPU_Prices_Parser.Data;
using GPU_Prices_Parser.Data.Gpu;
using GPU_Prices_Parser.Extensions;
using GPU_Prices_Parser.Parsers.Products;
using OpenQA.Selenium;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace GPU_Prices_Parser
{
    internal partial class GpuForm : Form
    {
        private readonly IEnumerable<ProductParser> _productParsers;
        private readonly Store[] _storesToSearch;
        private readonly WebProvider _webProvider;

        public GpuForm(GpuModel[] gpuModelToSearch, Store[] storesToSearch, IEnumerable<ProductParser> productParsers)
        {
            InitializeComponent();

            _webProvider = new WebProvider();
            _storesToSearch = storesToSearch;
            _productParsers = productParsers;

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
            sendRequestBtn.Enabled = false;
            
            pricesTable.RowCount = 0;
            pricesTable.Enabled = false;

            plotView.Model.Title = (string) gpuList.SelectedItem + " Prices";
            plotView.Refresh();

            foreach (var store in _storesToSearch)
            {
                var productParser = _productParsers.FirstOrDefault(parser => parser.ParseStore == store.Name);
                
                if (productParser != null)
                {
                    var gpuInfo = await productParser!.ExtractAllInfo(
                            GpuModelHelper.GetModel((string) gpuList.SelectedItem), store);
                    
                    FillDataGridView(_storesToSearch.IndexOf(store), gpuInfo);
                    FileSaver.SaveGpuInfo(gpuInfo);
                }
                else throw new ArgumentException($"There is no corresponding parser for the store {store.Name}");
            }
            
            sendRequestBtn.Enabled = true;
        }

        private async Task PricesTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            plotView.Model.Subtitle = (string) pricesTable.Rows[e.RowIndex].HeaderCell.Value;
            plotView.Model.Series.Clear();

            foreach (var store in _storesToSearch)
            {
                var selectedGpu = (GpuNote) pricesTable[0, e.RowIndex].Value;
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

        private void FillDataGridView(int storeIndex, GpuNote[] gpuNotes)
        {
            lock (pricesTable)
            {
                if (pricesTable.Enabled)
                {
                    pricesTable.Rows.Add(gpuNotes.Length);

                    int j = 0;
                    for (int i = pricesTable.RowCount - gpuNotes.Length; i < pricesTable.RowCount; i++)
                    {
                        pricesTable.Rows[i].HeaderCell.Value = gpuNotes[j].Gpu.FullName;
                        pricesTable.Rows[i].Cells[0].Value = gpuNotes[j];
                        pricesTable.Rows[i].Cells[storeIndex + 1].Value = gpuNotes[j].Gpu.Price;
                        j++;
                    }
                }
                else
                {
                    pricesTable.Enabled = true;
                    pricesTable.RowCount = gpuNotes.Length;

                    for (int i = 0; i < pricesTable.RowCount; i++)
                    {
                        pricesTable.Rows[i].HeaderCell.Value = gpuNotes[i].Gpu.FullName;
                        pricesTable.Rows[i].Cells[0].Value = gpuNotes[i];
                        pricesTable.Rows[i].Cells[storeIndex + 1].Value = gpuNotes[i].Gpu.Price;
                    }
                }
            }
        }
    }
}