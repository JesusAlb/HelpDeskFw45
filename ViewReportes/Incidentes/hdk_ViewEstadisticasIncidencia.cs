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
    public partial class hdk_ViewEstadisticasIncidencia : Form
    {
        DateTime fechaInicio;
        DateTime fechaFinal;
        hdk_ControlAcceso Control;

        public hdk_ViewEstadisticasIncidencia(DateTime fi, DateTime fn, hdk_ControlAcceso ca)
        {
            InitializeComponent();
            fechaInicio = fi;
            fechaFinal = fn;
            Control = ca;
            this.CenterToScreen();
        }

        private void hdk_ViewEstadisticasIncidencia_Load(object sender, EventArgs e)
        {
            IList lista = Control.DB.numIncidentesPorTipo(fechaInicio, fechaFinal).ToList();
            IList lista1 = Control.DB.numIncidentesPorUsuario(fechaInicio, fechaFinal).ToList();
            IList lista2 = Control.DB.numIncidentesPorMes(fechaInicio, fechaFinal).ToList();
            IList lista3 = Control.DB.numIncidentesPorDepto(fechaInicio, fechaFinal).ToList();

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportEmbeddedResource = "HelpDesk.ViewReportes.Incidentes.ReportGraficoIncidentes.rdlc"; // bind reportviewer with .rdlc

            Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetNumInTipo", lista); // set the datasource
            Microsoft.Reporting.WinForms.ReportDataSource dataset1 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetNumInUsuario", lista1); // set the datasource
            Microsoft.Reporting.WinForms.ReportDataSource dataset2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetNumInMes", lista2);
            Microsoft.Reporting.WinForms.ReportDataSource dataset3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetNumInDepto", lista3); // set the datasource
            reportViewer1.LocalReport.DataSources.Add(dataset);
            dataset.Value = lista;
            reportViewer1.LocalReport.DataSources.Add(dataset1);
            dataset1.Value = lista1;
            reportViewer1.LocalReport.DataSources.Add(dataset2);
            dataset2.Value = lista2;
            reportViewer1.LocalReport.DataSources.Add(dataset3);
            dataset3.Value = lista3;

            reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
    }
}
