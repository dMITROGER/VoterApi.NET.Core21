using PielievinGerasApiVoter.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PielievinGerasApiVoter.lib
{
    public class BarChart
    {
        // Fields
        private string data = "";
        private string cols = "";
        private string rows = "";
        private string javascript;

        // Properties
        public string elementId { get; set; }

        public string hAxisTitle { get; set; }

        public string vAxisTitle { get; set; }

        public string color { get; set; }

        public string title { get; set; }


        // ChartTypes
        public enum ChartType
        {
            BarChart,
            PieChart,
            LineChart,
            ColumnChart,
            AreaChart,
            BubbleChart,
            CandlestickChart,
            ComboChart,
            GeoChart,
            ScatterChart,
            SteppedAreaChart,
            TableChart
        }
        // Color types
        public enum ColorType
        {
            black,
            blue,
            red,
            green,
            yellow,
            gray
        }

        // Methods
        public BarChart()
        {
            this.title = "Google Chart";
            this.elementId = "chart_div";
            this.color = "#e2431e";
        }

        public void addColumn(string type, string columnName)
        {
            string data = this.data;
            this.data = data + "data.addColumn('" + type + "', '" + columnName + "');";
            this.cols = this.cols + " {'type': '" + type + "', 'val': '" + columnName + "'}, ";
        }

        /*
         var json = { 
		       cols: [ {'type': 'string', val: 'aday' }, {'type': 'number', val: 'Oy Sayisi' }    ],
		    rows: [ { 'name': 'ismail', val: 200 }, { 'name': 'ismail3', val: 33 } ]
		 }
        */
        public void addRow(string value)
        {
            this.data = this.data + "data.addRow([" + value + "]);";
        }

        public void addRowJson(string name, string value)
        {
            this.rows = this.rows + "{ 'name': '" + name + "', 'val' : " + value + "},";
        }

        public string JSon()
        {
            return "{ 'cols': [ " + this.cols + " ]," +
                     "  'rows': [ " + this.rows + " ] " +
                     "}".Replace("'", "\"");
        }

        public string generateChart(ChartType chart)
        {
            this.javascript = "<script type=\"text/javascript\" src=\"https://www.google.com/jsapi\"></script>";
            this.javascript = this.javascript + "<script type=\"text/javascript\">";
            this.javascript = this.javascript + "google.load('visualization', '1.0', { 'packages': ['corechart']});";
            this.javascript = this.javascript + "google.setOnLoadCallback(drawChart);";
            this.javascript = this.javascript + "var data, options, chart,  view; ";
            this.javascript = this.javascript + "function drawChart() {";
            this.javascript = this.javascript + "data = new google.visualization.DataTable();";
            this.javascript = this.javascript + this.data;
            this.javascript = this.javascript + "options = {";
            this.javascript = this.javascript + "'title': '" + this.title + "',";
            this.javascript = this.javascript + "'colors': ['" + this.color + "'],";
            this.javascript = this.javascript + "vAxis: {title: '" + this.vAxisTitle + "' },";
            this.javascript = this.javascript + "hAxis: {title: '" + this.hAxisTitle + "' },";


            object javascript = this.javascript;
            this.javascript = string.Concat(new object[] { javascript, "'width': ", 1200, ", 'height': ", 600, "};" });
            string str = this.javascript;
            this.javascript = str + "chart = new google.visualization." + chart.ToString() + "(document.getElementById('" + this.elementId + "'));";
            this.javascript = this.javascript + "view = new google.visualization.DataView(data);                                              ";
            this.javascript = this.javascript + "view.setColumns([0, 1, { sourceColumn: 1, role: 'annotation' }]);                            ";
            this.javascript = this.javascript + "chart = new google.visualization.BarChart(document.getElementById('" + this.elementId + "'));";
            this.javascript = this.javascript + "chart.draw(view, options);	                                                                  ";
            this.javascript = this.javascript + "};" +
                "function setData(jsonData){     data =  new google.visualization.DataTable(jsonData);     " +
                "};                                                  " +
                "function getData(){                                 " +
                "    return data;		                             " +
                "};                                                  " +
                "function updateChart() {                            " +
                "    data = getData();                               " +
                "    chart.draw(data, options);                      " +
                "};                                                  " +
                " function setChart(json){                           " +
                "    data = new google.visualization.DataTable();    " +
                "    var json =  json;   for(var i=0; i<  json.cols.length; i++   ){     " +
                "        var col =  json.cols[i];                    " +
                "        data.addColumn( col.type, col.val );        " +
                "    };                                              " +
                "    var arr = [];                                   " +
                "    for(var i=0; i<  json.rows.length; i++   ){     " +
                "        var col =  json.rows[i];                    " +
                "        arr.push([ col.name,  col.val  ]);          " +
                "    };                                              " +
                "    data.addRows(arr);                             " +
                "    view = new google.visualization.DataView(data);                                                 " +
                "    view.setColumns([0, 1, { sourceColumn: 1, role: 'annotation' }]);                               " +
                "    chart = new google.visualization.BarChart(document.getElementById('" + this.elementId + "'));   " +
                "    chart.draw(view, options);	                                                                     " +
                "};                                                   " +
                "</script>";
            return this.javascript;
        }
    }
}
