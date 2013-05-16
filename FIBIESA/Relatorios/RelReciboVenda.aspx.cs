﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using DataObjects;
using BusinessLayer;

namespace FIBIESA.Relatorios
{
    public partial class RelReciboVenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.geraRelatorio();
            }
        }

        DataTable lDtVenda;

        private void geraRelatorio()
        {
            VendasBL vendaBL = new VendasBL();
            Vendas venda = new Vendas();
            int vendaid = Convert.ToInt16(Request.QueryString["vendaid"].ToString());
            lDtVenda = vendaBL.PesquisarBLDataSet(vendaid).Tables[0];
            if (lDtVenda.Rows.Count > 0)
            {


                InstituicoesBL instBL = new InstituicoesBL();
                Instituicoes inst = new Instituicoes();

                InstituicoesLogoBL instLogoBL = new InstituicoesLogoBL();
                InstituicoesLogo instLogo = new InstituicoesLogo();

                VendaItensBL vendaItensBL = new VendaItensBL();
                VendaItens vendaItens = new VendaItens();

                ReportDataSource rptDatasourceInstituicao = new ReportDataSource("DataSet_Instituicao", instBL.PesquisarDsBL().Tables[0]);
                ReportDataSource rptDatasourceInstituicaoLogo = new ReportDataSource("DataSet_InstituicaoLogo", instLogoBL.PesquisarDsBL().Tables[0]);
                ReportDataSource rptDatasourceVenda = new ReportDataSource("DataSet_Venda", lDtVenda);
                DataSet lds = vendaItensBL.PesquisarBLDataSet(vendaid);
                ReportDataSource rptDatasourceVendaItem = new ReportDataSource("DataSet_VendaItens", lds.Tables[0]);
                decimal valorTotal = Convert.ToDecimal(lds.Tables[0].Compute("Sum(valor)","").ToString());
                decimal descontoTotal = Convert.ToDecimal(lds.Tables[0].Compute("Sum(desconto)","").ToString());
                NumeroPorExtenso numeroPorExtenso = new NumeroPorExtenso(valorTotal - descontoTotal);
                string valorExtenso = numeroPorExtenso.ToString();
                ReportParameter[] param = new ReportParameter[1];
                param[0] = new ReportParameter("valorExtenso", valorExtenso);

                ReportViewer1.LocalReport.SetParameters(param);
                ReportViewer1.LocalReport.DataSources.Add(rptDatasourceInstituicao);
                ReportViewer1.LocalReport.DataSources.Add(rptDatasourceInstituicaoLogo);
                ReportViewer1.LocalReport.DataSources.Add(rptDatasourceVenda);
                ReportViewer1.LocalReport.DataSources.Add(rptDatasourceVendaItem);

                ReportViewer1.LocalReport.Refresh();
                //Session["ldsRel"] = null;
            }
            else
            {
                divRelatorio.Visible = false;
                divMensagem.Visible = true;
                lblMensagem.Text = "Este relatorio não possui dados.";
            }

        }
    }
}