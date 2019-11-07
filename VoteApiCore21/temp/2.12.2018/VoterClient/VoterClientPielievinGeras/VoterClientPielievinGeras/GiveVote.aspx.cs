using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using VoterClientPielievinGeras.lib;
using VoterClientPielievinGeras.Models;
using System.Text;
using System.Web.Services;


namespace VoterClientPielievinGeras
{
    public partial class GiveVote : System.Web.UI.Page
    {
        ApiHelper aph = new ApiHelper();
        BarChart chart = new BarChart();
        protected void  Page_Load (object sender, EventArgs e)
        {
            DrawChart();
        }

        private void DrawChart()
        {
            IEnumerable<Result> results = aph.getResult().Result;
            IEnumerable<Candidate> listCandidates = aph.getCandidates().Result;
            chart.title = "Voting Results";
            chart.addColumn("string", "Candidate");
            chart.addColumn("number", "Ratings");
            if (results != null && listCandidates != null)
            {
                foreach (Result item in results)
                {
                    Candidate can = listCandidates.Where(c => c.Id == item.candidate_id).FirstOrDefault();
                    if (can != null)
                    {
                        chart.addRow("'" + can.Name + "'," + item.count);
                    }
                }
            }
            
            ltScripts.Text = chart.generateChart(BarChart.ChartType.BarChart);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IEnumerable<Result> results = aph.getResult().Result;
            IEnumerable<Candidate> listCandidates = aph.getCandidates().Result;
            chart.title = "Voting Results";
            chart.addColumn("string", "Candidate");
            chart.addColumn("number", "Ratings");
            if (results != null && listCandidates != null)
            {
                foreach (Result item in results)
                {
                    Candidate can = listCandidates.Where(c => c.Id == item.candidate_id).FirstOrDefault();
                    if (can != null)
                    {
                        chart.addRow("'" + can.Name + "'," + item.count);
                    }
                }
            }

            ltScripts.Text = chart.generateChart(BarChart.ChartType.BarChart);
        }
    }
}