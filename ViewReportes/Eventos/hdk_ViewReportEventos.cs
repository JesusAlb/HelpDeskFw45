using HelpDesk.Control.Solicitudes;
using HelpDesk.Modelo;
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
    public partial class hdk_ViewReportEventos : Form
    {
        private IList lista;

        public hdk_ViewReportEventos(IList l)
        {
            InitializeComponent();
            lista = l;
            this.CenterToScreen();

        }

        private void hdk_ViewReportEventos_Load(object sender, EventArgs e)
        {

            reportViewer1.LocalReport.DataSources.Clear(); 
            reportViewer1.LocalReport.ReportEmbeddedResource = "HelpDesk.ViewReportes.Eventos.ReportEventos.rdlc"; 

            Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetEventos", lista); // set the datasource
            reportViewer1.LocalReport.DataSources.Add(dataset);
            dataset.Value = lista;

            reportViewer1.LocalReport.Refresh();

            this.reportViewer1.RefreshReport();
        }
    }
}
