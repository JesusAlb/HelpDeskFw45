using HelpDesk.Control.Catalogo;
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
    public partial class hdk_ViewReportEquipos : Form
    {
        private IList lista;

        public hdk_ViewReportEquipos(IList l)
        {
            InitializeComponent();
            lista = l;
            this.CenterToScreen();
        }

        private void hdk_ViewReportEquipos_Load(object sender, EventArgs e)
        {
     
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportEmbeddedResource = "HelpDesk.ViewReportes.Equipos.ReportEquipos.rdlc"; // bind reportviewer with .rdlc

            Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetEquipos", lista); // set the datasource
            reportViewer1.LocalReport.DataSources.Add(dataset);
            dataset.Value = lista;

            reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
    }
}
