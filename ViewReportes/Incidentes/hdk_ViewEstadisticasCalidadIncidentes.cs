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

namespace HelpDesk.ViewReportes.Incidentes
{
    public partial class hdk_ViewEstadisticasCalidadIncidentes : Form
    {
        DateTime fechaInicio;
        DateTime fechaFinal;
        hdk_ControlAcceso Control;

        public hdk_ViewEstadisticasCalidadIncidentes(DateTime fi, DateTime fn, hdk_ControlAcceso ca)
        {
            InitializeComponent();
            fechaInicio = fi;
            fechaFinal = fn;
            Control = ca;
            this.CenterToScreen();
        }

        private void hdk_ViewEstadisticasCalidadIncidentes_Load(object sender, EventArgs e)
        {
            IList lista = Control.DB.promedioCalidadPorUsuarioIn(fechaInicio, fechaFinal).ToList();
            IList lista1 = Control.DB.promedioCalidadPorTipoIn(fechaInicio, fechaFinal).ToList();
            IList lista2 = Control.DB.promedioCalidadPorMesIn(fechaInicio, fechaFinal).ToList();
            IList lista3 = Control.DB.promedioCalidadPorDepto(fechaInicio, fechaFinal).ToList();

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportEmbeddedResource = "HelpDesk.ViewReportes.Incidentes.ReportGraficoCalidadIncidentes.rdlc"; // bind reportviewer with .rdlc

            Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("promedioCalidadUsuario", lista); // set the datasource
            Microsoft.Reporting.WinForms.ReportDataSource dataset1 = new Microsoft.Reporting.WinForms.ReportDataSource("promedioCalidadTipo", lista1); // set the datasource
            Microsoft.Reporting.WinForms.ReportDataSource dataset2 = new Microsoft.Reporting.WinForms.ReportDataSource("promedioCalidadMes", lista2); // set the datasource
            Microsoft.Reporting.WinForms.ReportDataSource dataset3 = new Microsoft.Reporting.WinForms.ReportDataSource("promedioCalidadDepto", lista3); // set the datasource
            reportViewer1.LocalReport.DataSources.Add(dataset);
            reportViewer1.LocalReport.DataSources.Add(dataset1);
            reportViewer1.LocalReport.DataSources.Add(dataset2);
            reportViewer1.LocalReport.DataSources.Add(dataset3);
            dataset.Value = lista;     
            dataset1.Value = lista1;
            dataset2.Value = lista2;
            dataset3.Value = lista3;

            reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
    }
}
