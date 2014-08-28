using HelpDesk.Modelo;
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
    public partial class hdk_ViewReportRequerimientos : Form
    {
        int idE;

        public hdk_ViewReportRequerimientos(int id)
        {
            InitializeComponent();
            this.CenterToScreen();
            idE = id;
            
        }

        private void hdk_ViewReportRequerimientos_Load(object sender, EventArgs e)
        {
            dbhelpdeskEntities dbHelp = new dbhelpdeskEntities();
            IList lista = dbHelp.ViewEventoConRequerimientos.Where(a => a.evento == idE).ToList();
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportEmbeddedResource = "HelpDesk.ViewReportes.Eventos.ReportRequerimientos.rdlc"; // bind reportviewer with .rdlc
            Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetReqAEventos", lista); // set the datasource
            reportViewer1.LocalReport.DataSources.Add(dataset);
            dataset.Value = lista;
            reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
    }
}
