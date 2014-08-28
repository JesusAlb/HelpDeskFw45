using HelpDesk.Control.Solicitudes.Incidentes;
using HelpDesk.Principal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelpDesk.ViewReportes
{
    public partial class hdk_ViewReportIncidentes : Form
    {
        IList lista;

        public hdk_ViewReportIncidentes(IList l)
        {
            InitializeComponent();
            this.CenterToScreen();
            lista = l;
            
        }

        private void hdk_ViewReportIncidentes_Load(object sender, EventArgs e)
        {

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportEmbeddedResource = "HelpDesk.ViewReportes.Incidentes.ReportIncidentes.rdlc"; // bind reportviewer with .rdlc

            Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetIncidentes", lista); // set the datasource
            reportViewer1.LocalReport.DataSources.Add(dataset);
            dataset.Value = lista;

            reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
    }
}
