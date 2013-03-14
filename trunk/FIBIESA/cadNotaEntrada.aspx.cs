﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataObjects;
using BusinessLayer;
using FG;
using System.Data;

namespace Admin
{
    public partial class cadNotaEntrada : System.Web.UI.Page
    {
        Utils utils = new Utils();
        DataTable dtItens = new DataTable();

        #region funcoes

        private void CarregarAtributos()
        {            
            txtNumero.Attributes.Add("onkeypress", "return(Inteiros(this,event))");
            txtSerie.Attributes.Add("onkeypress", "return(Inteiros(this,event))");
            txtQtde.Attributes.Add("onkeypress", "return(Inteiros(this,event))");
            txtNumero.Attributes.Add("onkeypress", "return(Inteiros(this,event))");
            txtValor.Attributes.Add("onkeypress", "return(Real(this,event))");
            txtValorVenda.Attributes.Add("onkeypress", "return(Real(this,event))");
            txtTotItens.Attributes.Add("onkeypress", "return(Inteiros(this,event))");
            //txtTotal.Attributes.Add("onkeypress", "return(Real(this,event))");
           
        }

        private void CriarDtItens()
        {
            if (dtgItens.Columns.Count > 0)
            {
                DataColumn coluna1 = new DataColumn("IDITEM", Type.GetType("System.Int32"));
                DataColumn coluna2 = new DataColumn("DESCITEM", Type.GetType("System.String"));
                DataColumn coluna3 = new DataColumn("QUANTIDADE", Type.GetType("System.Int32"));
                DataColumn coluna4 = new DataColumn("VALOR", Type.GetType("System.Decimal"));
                DataColumn coluna5 = new DataColumn("VALORTOTAL", Type.GetType("System.Decimal"));
                DataColumn coluna6 = new DataColumn("VALORVENDA", Type.GetType("System.Decimal"));

                dtItens.Columns.Add(coluna1);
                dtItens.Columns.Add(coluna2);
                dtItens.Columns.Add(coluna3);
                dtItens.Columns.Add(coluna4);
                dtItens.Columns.Add(coluna5);
                dtItens.Columns.Add(coluna6);
            }
        }

        private void LimparCamposItem()
        {
            txtItem.Text = "";
            lblDesItem.Text = "";
            txtQtde.Text = "";
            txtValor.Text = "";
            txtValorVenda.Text = "";           
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarAtributos();
            }
        }

        protected void btnPesItem_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "WinOpen('/PesquisarItens.aspx?caixa=" + txtItem.ClientID + "&id=" + hfIdItem.ClientID + "&lbl=" + lblDesItem.ClientID + "&valor" + "','',600,500);", true);
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            NotasEntradaBL ntEBL = new NotasEntradaBL();
            NotasEntrada notaEntrada = new NotasEntrada();
            NotasEntradaItensBL ntEiBL = new NotasEntradaItensBL();
            NotasEntradaItens notaEntradaItens = new NotasEntradaItens();

            notaEntrada.Numero = utils.ComparaIntComZero(txtNumero.Text);
            notaEntrada.Serie = utils.ComparaShortComZero(txtSerie.Text);
            notaEntrada.Data = Convert.ToDateTime(txtData.Text);

            if (Session["dtItens"] != null)
                dtItens = (DataTable)Session["dtItens"];

            if (this.Master.VerificaPermissaoUsuario("INSERIR"))
            {
                if (dtItens.Rows.Count > 0)
                {
                    int id = ntEBL.InserirBL(notaEntrada);

                    if (id > 0)
                    {
                        foreach (DataRow linha in dtItens.Rows)
                        {
                            notaEntradaItens.NotaEntradaId = id;
                            notaEntradaItens.ItemEstoqueId = utils.ComparaIntComZero(linha["IDITEM"].ToString());
                            notaEntradaItens.Quantidade = utils.ComparaIntComZero(linha["QUANTIDADE"].ToString());
                            notaEntradaItens.Valor = utils.ComparaDecimalComZero(linha["VALOR"].ToString());

                            ntEiBL.InserirBL(notaEntradaItens);
                        }
                    }
                }

            }
            else
                Response.Redirect("~/erroPermissao.aspx?nomeUsuario=" + ((Label)Master.FindControl("lblNomeUsuario")).Text + "&usuOperacao=operação", true);

            Response.Redirect("cadNotaEntrada.aspx");

        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
         
            if (Session["dtItens"] != null)
                dtItens = (DataTable)Session["dtItens"];

           /* DataRow linha = dtItens.NewRow();
            linha["IDITEM"] = utils.ComparaIntComZero(hfIdItem.ToString());
            linha["QUANTIDADE"] = utils.ComparaIntComZero(txtQtde.Text);            
            linha["VALOR"] = utils.ComparaDecimalComZero(txtValor.Text);
            linha["VALORTOTAL"] = utils.ComparaDecimalComZero(txtValor.Text) * utils.ComparaIntComZero(txtQtde.Text);
            linha["VALORVENDA"] = utils.ComparaDecimalComZero(txtValorVenda.Text);

            dtItens.Rows.Add(linha);*/
            DataColumn coluna1 = new DataColumn("IDITEM", Type.GetType("System.Int32"));
            DataColumn coluna2 = new DataColumn("DESCITEM", Type.GetType("System.String"));
            DataColumn coluna3 = new DataColumn("QUANTIDADE", Type.GetType("System.Int32"));
            DataColumn coluna4 = new DataColumn("VALOR", Type.GetType("System.Decimal"));
            DataColumn coluna5 = new DataColumn("VALORTOTAL", Type.GetType("System.Decimal"));
            DataColumn coluna6 = new DataColumn("VALORVENDA", Type.GetType("System.Decimal"));

            dtItens.Columns.Add(coluna1);
            dtItens.Columns.Add(coluna2);
            dtItens.Columns.Add(coluna3);
            dtItens.Columns.Add(coluna4);
            dtItens.Columns.Add(coluna5);
            dtItens.Columns.Add(coluna6);

            DataRow linha = dtItens.NewRow();
            linha["IDITEM"] = 1;
            linha["QUANTIDADE"] = 12;
            linha["VALOR"] = 25;
            linha["VALORTOTAL"] = 300;
            linha["VALORVENDA"] = 35;

            dtItens.Rows.Add(linha);

            linha = dtItens.NewRow();
            linha["IDITEM"] = 2;
            linha["QUANTIDADE"] = 10;
            linha["VALOR"] = 20;
            linha["VALORTOTAL"] = 200;
            linha["VALORVENDA"] = 30;

            dtItens.Rows.Add(linha);
            
            Session["dtItens"] = dtItens;

            dtgItens.DataSource = dtItens;
            dtgItens.DataBind();

            LimparCamposItem();
          
        }

        protected void dtgItens_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}