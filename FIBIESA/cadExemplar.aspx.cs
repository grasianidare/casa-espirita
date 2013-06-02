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
    public partial class cadExemplar : System.Web.UI.Page
    {
        Utils utils = new Utils();
        string v_operacao = "";

        #region funcoes
       
        private void CarregarDados(int id_exe)
        {
            ExemplaresBL exeBL = new ExemplaresBL();
            DataSet dsPar = exeBL.PesquisarBL(id_exe);
            
            if (dsPar.Tables[0].Rows.Count != 0)
            {
                hfId.Value = (string)dsPar.Tables[0].Rows[0]["id"].ToString(); 
                txtTombo.Text = (string)dsPar.Tables[0].Rows[0]["tombo"].ToString();
                ddlStatus.SelectedValue = (string)dsPar.Tables[0].Rows[0]["status"]; 
                hfIdObra.Value = (string)dsPar.Tables[0].Rows[0]["obraid"].ToString();
                txtObra.Text = (string)dsPar.Tables[0].Rows[0]["codigo"].ToString();
                lblDesObra.Text = (string)dsPar.Tables[0].Rows[0]["titulo"];
                ddlOrigem.SelectedValue = (string)dsPar.Tables[0].Rows[0]["origemid"].ToString();
            }
        }

        private void CarregarAtributos()
        {
           txtTombo.Attributes.Add("onkeypress", "return(Inteiros(this,event))");
        }

        private DataTable CriarDtPesquisa()
        {
            DataTable dt = new DataTable();
            DataColumn coluna1 = new DataColumn("ID", Type.GetType("System.Int32"));
            DataColumn coluna2 = new DataColumn("CODIGO", Type.GetType("System.String"));
            DataColumn coluna3 = new DataColumn("DESCRICAO", Type.GetType("System.String"));

            dt.Columns.Add(coluna1);
            dt.Columns.Add(coluna2);
            dt.Columns.Add(coluna3);

            return dt;
        }

        private void CarregarDdlOrigem()
        {
            OrigensBL oriBL = new OrigensBL();
            List<Origens> origens = oriBL.PesquisarBL();

            ddlOrigem.Items.Add(new ListItem("Selecione",""));
            foreach (Origens ltOri in origens)
                ddlOrigem.Items.Add(new ListItem(ltOri.Codigo + " - " + ltOri.Descricao, ltOri.Id.ToString()));

            ddlOrigem.SelectedIndex = 0;
        }
        private void ExibirMensagem(string mensagem)
        {
            ClientScript.RegisterStartupScript(System.Type.GetType("System.String"), "Alert",
               "<script language='javascript'> { window.alert(\"" + mensagem + "\") }</script>");
        }

        private void LimparCampos()
        {
            txtObra.Text = "";
            lblDesObra.Text = "";
            txtTombo.Text = "";
            ddlOrigem.SelectedIndex = 0;
            hfIdObra.Value = "";
            hfId.Value = "";
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            int id_exe = 0;            

            if (!IsPostBack)
            {
                CarregarAtributos();
                CarregarDdlOrigem();

                if (Request.QueryString["operacao"] != null)
                {
                    v_operacao = Request.QueryString["operacao"];

                    if (v_operacao == "edit")
                        if (Request.QueryString["id_exem"] != null)
                            id_exe = Convert.ToInt32(Request.QueryString["id_exem"].ToString());
                }

                if (v_operacao.ToLower() == "edit")
                    CarregarDados(id_exe);

                txtObra.Focus();
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewExemplar.aspx");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            ExemplaresBL exeBL = new ExemplaresBL();
            Exemplares exemplares = new Exemplares();
            exemplares.Id = utils.ComparaIntComZero(hfId.Value);
            exemplares.Obraid = utils.ComparaIntComZero(hfIdObra.Value);
            exemplares.Tombo = utils.ComparaIntComZero(txtTombo.Text);
            exemplares.Status = ddlStatus.SelectedValue;
            exemplares.OrigemId = utils.ComparaIntComNull(ddlOrigem.SelectedValue);

            if (exemplares.Id > 0)
            {
                if (this.Master.VerificaPermissaoUsuario("EDITAR"))
                   if(exeBL.EditarBL(exemplares))
                        ExibirMensagem("Exemplar atualizado com sucesso !");
                    else
                        ExibirMensagem("Não foi possível atualizar o exemplar. Revise as informações.");
                else
                    Response.Redirect("~/erroPermissao.aspx?nomeUsuario=" + ((Label)Master.FindControl("lblNomeUsuario")).Text + "&usuOperacao=operação", true);

            }
            else
            {
                if (this.Master.VerificaPermissaoUsuario("INSERIR"))
                    if(exeBL.InserirBL(exemplares))                     
                    {
                        ExibirMensagem("Exemplar gravado com sucesso !");
                        LimparCampos();
                        txtObra.Focus();
                    }
                    else
                        ExibirMensagem("Não foi possível gravar o exemplar. Revise as informações.");
                else
                    Response.Redirect("~/erroPermissao.aspx?nomeUsuario=" + ((Label)Master.FindControl("lblNomeUsuario")).Text + "&usuOperacao=operação", true);
            }
                        
        }

        protected void btnPesObra_Click(object sender, EventArgs e)
        {
            Session["tabelaPesquisa"] = null;
            DataTable dt = CriarDtPesquisa();
            ObrasBL obBL = new ObrasBL();
            Obras obr = new Obras();
            List<Obras> obras = obBL.PesquisarBL();

            foreach (Obras ltobr in obras)
            {
                DataRow linha = dt.NewRow();

                linha["ID"] = ltobr.Id;
                linha["CODIGO"] = ltobr.Codigo;
                linha["DESCRICAO"] = ltobr.Titulo;

                dt.Rows.Add(linha);
            }

            if (dt.Rows.Count > 0)
                Session["tabelaPesquisa"] = dt;


            Session["objBLPesquisa"] = obBL;
            Session["objPesquisa"] = obr;

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "WinOpen('/Pesquisar.aspx?caixa=" + txtObra.ClientID + "&id=" + hfIdObra.ClientID + "&lbl=" + lblDesObra.ClientID + "','',600,500);", true);
        }

        protected void txtObra_TextChanged(object sender, EventArgs e)
        {
            ObrasBL obrBL = new ObrasBL();
            List<Obras> obras = obrBL.PesquisarBL("CODIGO",txtObra.Text);
            
            lblDesObra.Text = "";
            hfIdObra.Value = "";
            foreach (Obras ltObr in obras)
            {
                hfIdObra.Value = ltObr.Id.ToString();
                lblDesObra.Text = ltObr.Titulo;
            }
            
        }

        protected void txtTombo_TextChanged(object sender, EventArgs e)
        {
            ExemplaresBL exeBL = new ExemplaresBL();

            if (exeBL.CodigoJaUtilizadoBL(utils.ComparaIntComZero(txtTombo.Text)))
            {
                lblInformacao.Text = "O tombo " + txtTombo.Text + " já existe. Informe um novo código.";
                txtTombo.Text = "";
                txtTombo.Focus();
            }
            else
            {
                lblInformacao.Text = "";
                ddlOrigem.Focus();
            }  
        }

       
    }
}