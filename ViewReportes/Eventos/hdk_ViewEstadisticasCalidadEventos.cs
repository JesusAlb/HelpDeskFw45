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
    public partial class hdk_ViewEstadisticasCalidadEventos : Form
    {
        DateTime fechaInicio;
        DateTime fechaFinal;
        hdk_ControlAcceso Control;

        public hdk_ViewEstadisticasCalidadEventos(DateTime fi, DateTime ff, hdk_ControlAcceso ca)
        {
            InitializeComponent();
            fechaInicio = fi;
            fechaFinal = ff;
            Control = ca;
            this.CenterToScreen();
        }

        private void hdk_ViewEstadisticasCalidadEventos_Load(object sender, EventArgs e)
        {
            IList lista = Control.DB.promedioCalidadPorUsuarioEv(fechaInicio, fechaFinal).ToList();
            IList lista1 = Control.DB.promedioCalidadPorMesEv(fechaInicio, fechaFinal).ToList();

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportEmbeddedResource = "HelpDesk.ViewReportes.Eventos.ReportGraficoCalidadEventos.rdlc"; // bind reportviewer with .rdlc

            Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("promedioCalidadUsuario", lista); // set the datasource
            Microsoft.Reporting.WinForms.ReportDataSource dataset1 = new Microsoft.Reporting.WinForms.ReportDataSource("promedioCalidadMes", lista1); // set the datasource
            reportViewer1.LocalReport.DataSources.Add(dataset);
            dataset.Value = lista;
            reportViewer1.LocalReport.DataSources.Add(dataset1);
            dataset1.Value = lista1;

            reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
    }
}
