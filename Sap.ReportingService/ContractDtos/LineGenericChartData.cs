using System.Collections.Generic;
using System.Linq;

namespace Sap.ReportingService.ContractDtos
{
    public abstract class LineGenericChartData
    {
        protected LineGenericChartData(string title, string chartVendor)
        {
            Title = title;
            ChartVendor = chartVendor;
            Labels = new List<string>();
            DataSets = new List<DataSet>();
        }

        public string Title { get; protected set; }
        public string ChartVendor { get; protected set; }
        public List<string> Labels { get; protected set; }
        public List<DataSet> DataSets { get; protected set; }
        /// <summary>
        /// For me to avoid confusion
        /// </summary>
        /// <param name="dataSet"></param>
        public void AddDataSet(DataSet dataSet)
        {
            if (dataSet.Data.Any(a => a > 0))
            {
                DataSets.Add(dataSet);
            }
        }
        /// <summary>
        /// For me to avoid confusion
        /// </summary>
        /// <param name="label"></param>
        public void AddLabel(string label)
        {
            Labels.Add(label);
        }
    }

    public class DataSet
    {
        protected DataSet()
        {

        }
        public static DataSet CreateDataSet(string label, double? value = null)
        {
            var dataSet = new DataSet()
            {
                Label = label,
                Data = new List<double>() { }
            };
            if (value.HasValue)
            {
                dataSet.Data.Add((double)value);
            }
            return dataSet;
        }

        public void AddData(double val)
        {
            Data.Add(val);
        }
        public string Label { get; protected set; }
        public List<double> Data { get; protected set; }
    }
}
