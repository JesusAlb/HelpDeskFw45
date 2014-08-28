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

namespace HelpDesk.ViewReportes.Eventos
{
    public partial class hdk_ViewEstadisticasEventos : Form
    {
        DateTime fechaInicio;
        DateTime fechaFinal;
        hdk_ControlAcceso Control;

        public hdk_ViewEstadisticasEventos(DateTime fi, DateTime ff, hdk_ControlAcceso ca)
        {
            InitializeComponent();
            fechaInicio = fi;
            fechaFinal = ff;
            Control = ca;
            this.CenterToScreen();
        }

        private void hdk_ViewEstadisticasEventos_Load(object sender, EventArgs e)
        {
            IList lista = Control.DB.numEventosPorUsuario(fechaInicio, fechaFinal).ToList();
            IList lista1 = Control.DB.topCincoRecursosMasUsados(fechaInicio, fechaFinal).ToList();
            IList lista2 = Control.DB.numEventosPorMes(fechaInicio, fechaFinal).ToList();
            IList lista3 = Control.DB.topCincoRecursosMenosUsados(fechaInicio, fechaFinal).ToList();
 
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportEmbeddedResource = "HelpDesk.ViewReportes.Eventos.ReportGraficoEventos.rdlc"; // bind reportviewer with .rdlc

            Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("numEventosPorUsuario", lista); // set the datasource
            Microsoft.Reporting.WinForms.ReportDataSource dataset1 = new Microsoft.Reporting.WinForms.ReportDataSource("RecursosMasUsados", lista1); // set the datasource
            Microsoft.Reporting.WinForms.ReportDataSource dataset2 = new Microsoft.Reporting.WinForms.ReportDataSource("numEventosPorMes", lista2); // set the datasource
            Microsoft.Reporting.WinForms.ReportDataSource dataset3 = new Microsoft.Reporting.WinForms.ReportDataSource("RecursosMenosUsados", lista3); // set the datasource
            reportViewer1.LocalReport.DataSources.Add(dataset);
            dataset.Value = lista;
            reportViewer1.LocalReport.DataSources.Add(dataset1);
            dataset1.Value = lista1;
            reportViewer1.LocalReport.DataSources.Add(dataset2);
            dataset1.Value = lista2;
            reportViewer1.LocalReport.DataSources.Add(dataset3);
            dataset1.Value = lista3;

            reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
    }
}
