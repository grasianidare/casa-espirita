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
    public partial class cadOrigem : System.Web.UI.Page
    {
        Utils utils = new Utils();
        string v_operacao = "";

        #region funcoes
        public DataTable dtbPesquisa
        {
            get
            {
                if (Session["_dtbPesquisa_cadOrigem"] != null)
                    return (DataTable)Session["_dtbPesquisa_cadOrigem"];
                else
                    return null;
            }
            set { Session["_dtbPesquisa_cadOrigem"] = value; }
        }

        private void CarregarDados(int id_bai)
        {
            OrigensBL origemBL = new OrigensBL();
            List<Origens> origens = origemBL.PesquisarBL(id_bai);

            foreach (Origens orig in origens)
            {
                hfId.Value = orig.Id.ToString();
                txtCodigo.Text = orig.Codigo.ToString();
                txtDescricao.Text = orig.Descricao;
            }

        }
        private void CarregarAtributos()
        {
            txtCodigo.Attributes.Add("onkeypress", "return(Inteiros(this,event))");
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            int id_origem = 0;

            CarregarAtributos();

            if (!IsPostBack)
            {

                if (Request.QueryString["operacao"] != null)
                {
                    v_operacao = Request.QueryString["operacao"];

                    if (v_operacao == "edit")
                        if (Request.QueryString["id_bai"] != null)
                            id_origem = Convert.ToInt32(Request.QueryString["id_bai"].ToString());
                }

                if (v_operacao.ToLower() == "edit")
                    CarregarDados(id_origem);
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewOrigem.aspx");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            OrigensBL origemBL = new OrigensBL();
            Origens origens = new Origens();
            origens.Id = utils.ComparaIntComZero(hfId.Value);
            origens.Codigo = utils.ComparaIntComZero(txtCodigo.Text);
            origens.Descricao = txtDescricao.Text;

            if (origens.Id > 0)
            {
                if (this.Master.VerificaPermissaoUsuario("EDITAR"))
                    origemBL.EditarBL(origens);
                else
                    Response.Redirect("~/erroPermissao.aspx?nomeUsuario=" + ((Label)Master.FindControl("lblNomeUsuario")).Text + "&usuOperacao=operação", true);

            }
            else
            {
                if (this.Master.VerificaPermissaoUsuario("INSERIR"))
                    origemBL.InserirBL(origens);
                else
                    Response.Redirect("~/erroPermissao.aspx?nomeUsuario=" + ((Label)Master.FindControl("lblNomeUsuario")).Text + "&usuOperacao=operação", true);
            }

            Response.Redirect("viewOrigem.aspx");
        }
    }
}