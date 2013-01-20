﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataObjects;
using BusinessLayer;
using FG;


namespace Admin
{
    public partial class cadPessoa : System.Web.UI.Page
    {
        #region variaveis
        Utils utils = new Utils();
        DataTable dtTelefones = new DataTable();
        string v_operacao = "";
        #endregion

        #region funcoes
        private void CarregarDadosPessoas(int id_pes)
        {
            string[] v_pesquisa;
            PessoasBL pesBL = new PessoasBL();
            List<Pessoas> pessoas = pesBL.PesquisarBL();

            foreach (Pessoas pes in pessoas)
            {
                hfId.Value = pes.Id.ToString();
                // pessoas.Codigo = 
                txtNome.Text = pes.Nome;
                txtNomeFantasia.Text = pes.NomeFantasia;                
                txtCpfCnpj.Text =  pes.CpfCnpj;
                txtRg.Text = pes.Rg;
                txtDataNascimento.Text =  pes.DtNascimento.ToString();
                ddlEstadoCivil.SelectedValue =  pes.EstadoCivil;
                txtNomeMae.Text =  pes.NomeMae;
                txtNomePai.Text = pes.NomePai;                
                txtCep.Text = pes.Cep;
                txtEndereco.Text =  pes.Endereco;
                txtNumero.Text = pes.Numero;                
                txtComplemento.Text = pes.Complemento;
                txtEmpresa.Text = pes.Empresa;
                txtEnderecoProf.Text = pes.EnderecoProf;
                txtNumeroProf.Text = pes.NumeroProf;                
                txtComplementoProf.Text = pes.ComplementoProf;                
                txtCepProf.Text = pes.CepProf;
                txtObservacao.Text = pes.Obs;
                txtDtCadastro.Text = pes.DtCadastro.ToString();
                               
                hfIdCidade.Value = pes.CidadeId.ToString();
                if (utils.ComparaIntComZero(hfIdCidade.Value) > 0)
                {
                    v_pesquisa = RetornarCodigoDecricaoCidade(utils.ComparaIntComZero(hfIdCidade.Value));
                    txtCidade.Text = v_pesquisa[0];
                    lblDesCidade.Text = v_operacao[1].ToString();
                }

                hfIdNaturalidade.Value = pes.Naturalidade.ToString();
                if (utils.ComparaIntComZero(hfIdNaturalidade.Value) > 0)
                {
                    v_pesquisa = RetornarCodigoDecricaoCidade(utils.ComparaIntComZero(hfIdNaturalidade.Value));
                    txtNaturalidade.Text = v_pesquisa[0];
                    lblDesNaturalidade.Text = v_operacao[1].ToString();
                }

                hfIdCidProf.Value = pes.CidadeProfId.ToString();
                if (utils.ComparaIntComZero(hfIdCidProf.Value) > 0)
                {
                    v_pesquisa = RetornarCodigoDecricaoCidade(utils.ComparaIntComZero(hfIdCidProf.Value));
                    txtCidadeProf.Text = v_pesquisa[0];
                    lblDesCidProf.Text = v_operacao[1].ToString();
                }
                
                hfIdBairro.Value = pes.BairroId.ToString();
                if (utils.ComparaIntComZero(hfIdBairro.Value) > 0)
                {
                    v_pesquisa = RetornarCodigoDecricaoBairro(utils.ComparaIntComZero(hfIdBairro.Value));
                    txtBairro.Text = v_pesquisa[0];
                    lblDesBairro.Text = v_operacao[1].ToString();
                }
                
                hfIdBairroProf.Value = pes.BairroProf.ToString();
                if (utils.ComparaIntComZero(hfIdBairroProf.Value) > 0)
                {
                    v_pesquisa = RetornarCodigoDecricaoBairro(utils.ComparaIntComZero(hfIdBairroProf.Value));
                    txtBairroProf.Text = v_pesquisa[0];
                    lblDesBairroProf.Text = v_operacao[1].ToString();
                }

                hfIdCategoria.Value = pes.CategoriaId.ToString();
                if (utils.ComparaIntComZero(hfIdCategoria.Value) > 0)
                {
                    CategoriasBL catBL = new CategoriasBL();
                    List<Categorias> categorias = catBL.PesquisarBL(utils.ComparaIntComZero(hfIdCategoria.Value));
                                        
                    txtCategoria.Text = categorias[0].Codigo.ToString();
                    lblDesCategoria.Text = categorias[0].Descricao;
                }                         
                
            }          
 
        }
        private void CarregarTabelaPesquisaCidade()
        {
            Session["tabelaPesquisa"] = null;
            DataTable dt = CriarDtPesquisa();

            CidadesBL cidBL = new CidadesBL();
            Cidades ci = new Cidades();
            List<Cidades> cidades = cidBL.PesquisarBL();

            foreach (Cidades cid in cidades)
            {
                DataRow linha = dt.NewRow();

                linha["ID"] = cid.Id;
                linha["CODIGO"] = cid.Codigo;
                linha["DESCRICAO"] = cid.Descricao;

                dt.Rows.Add(linha);
            }

            if (dt.Rows.Count > 0)
                Session["tabelaPesquisa"] = dt;


            Session["objBLPesquisa"] = cidBL;
            Session["objPesquisa"] = ci;
        }
        private void CarregarDadosTelefones(int id_pes)
        {
            DataTable dt = new DataTable();

            DataColumn coluna1 = new DataColumn("ID", Type.GetType("System.Int32"));
            DataColumn coluna2 = new DataColumn("DDD", Type.GetType("System.Int16"));
            DataColumn coluna3 = new DataColumn("DESCRICAO", Type.GetType("System.String"));
            DataColumn coluna4 = new DataColumn("PESSOAID", Type.GetType("System.Int32"));
            DataColumn coluna5 = new DataColumn("CODIGO", Type.GetType("System.Int32"));

            dt.Columns.Add(coluna1);
            dt.Columns.Add(coluna2);
            dt.Columns.Add(coluna3);
            dt.Columns.Add(coluna4);
            dt.Columns.Add(coluna5);

            TelefonesBL telBL = new TelefonesBL();

            List<Telefones> telefones = telBL.PesquisarBL(id_pes);

            foreach (Telefones tel in telefones)
            {
                DataRow linha = dt.NewRow();

                linha["ID"] = tel.Id;
                linha["DDD"] = tel.Ddd;
                linha["DESCRICAO"] = tel.Descricao;
                linha["PESSOAID"] = tel.PessoaId;
                linha["CODIGO"] = tel.Codigo;
            }

            dtgTelefones.DataSource = dt;
            dtgTelefones.DataBind();
        }
        private string[] RetornarCodigoDecricaoCidade(int id_cid)
        {
            string[] v_cidade = new string[2];
            CidadesBL cidBL = new CidadesBL();
            List<Cidades> cidades =  cidBL.PesquisarBL(id_cid);

            v_cidade[0] = cidades[0].Codigo.ToString();
            v_cidade[1] = cidades[0].Descricao;		 
	        
            return v_cidade;
        }
        private string[] RetornarCodigoDecricaoBairro(int id_bai)
        {
            string[] v_bairro = new string[2];
            BairrosBL baiBL = new BairrosBL();
            List<Bairros> bairros = baiBL.PesquisarBL(id_bai);

            v_bairro[0] = bairros[0].Codigo.ToString();
            v_bairro[1] = bairros[0].Descricao;
           
            return v_bairro;
        }
        private void CriarDtTelefones()
        {  
            if (dtTelefones.Columns.Count == 0)
            {
                DataColumn[] keys = new DataColumn[1];
                DataColumn coluna1 = new DataColumn("CODIGO",Type.GetType("System.Int32"));
                DataColumn coluna2 = new DataColumn("DDD", Type.GetType("System.Int16"));
                DataColumn coluna3 = new DataColumn("NUMERO", Type.GetType("System.String"));
                DataColumn coluna4 = new DataColumn("DESCRICAO", Type.GetType("System.String"));

                dtTelefones.Columns.Add(coluna1);
                dtTelefones.Columns.Add(coluna2);
                dtTelefones.Columns.Add(coluna3);
                dtTelefones.Columns.Add(coluna4);
                keys[0] = coluna1;                        

                dtTelefones.PrimaryKey = keys;
            }
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
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            int id_pes = 0;
            CriarDtTelefones();
            if (!IsPostBack)
            {
                if (Request.QueryString["operacao"] != null)
                {
                    v_operacao = Request.QueryString["operacao"];

                    if (v_operacao == "edit")
                        if (Request.QueryString["id_pes"] != null)
                            id_pes = Convert.ToInt32(Request.QueryString["id_pes"].ToString());
                }

                if (v_operacao.ToLower() == "edit")
                {
                    CarregarDadosPessoas(id_pes);
                    CarregarDadosTelefones(id_pes);
                }
            }

        }

        protected void btnPesCategoria_Click(object sender, EventArgs e)
        {
            Session["tabelaPesquisa"] = null;
            DataTable dt = CriarDtPesquisa();
            
            CategoriasBL catBL = new CategoriasBL();
            Categorias ca = new Categorias();
            List<Categorias> categorias = catBL.PesquisarBL();

            foreach (Categorias cat in categorias)
            {
                DataRow linha = dt.NewRow();

                linha["ID"] = cat.Id;
                linha["CODIGO"] = cat.Codigo;
                linha["DESCRICAO"] = cat.Descricao;

                dt.Rows.Add(linha);
            }

            if (dt.Rows.Count > 0)
                Session["tabelaPesquisa"] = dt;


            Session["objBLPesquisa"] = catBL;
            Session["objPesquisa"] = ca;

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "WinOpen('/Pesquisar.aspx?caixa=" + txtCategoria.ClientID + "&id=" + hfIdCategoria.ClientID + "&lbl=" + lblDesCategoria.ClientID + "','',600,500);", true);
        }

        protected void btnPesBairro_Click(object sender, EventArgs e)
        {
            Session["tabelaPesquisa"] = null;
            DataTable dt = CriarDtPesquisa();
            
            BairrosBL baiBL = new BairrosBL();
            Bairros ba = new Bairros();
            List<Bairros> bairros = baiBL.PesquisarBL();

            foreach (Bairros cat in bairros)
            {
                DataRow linha = dt.NewRow();

                linha["ID"] = cat.Id;
                linha["CODIGO"] = cat.Codigo;
                linha["DESCRICAO"] = cat.Descricao;

                dt.Rows.Add(linha);
            }

            if (dt.Rows.Count > 0)
                Session["tabelaPesquisa"] = dt;


            Session["objBLPesquisa"] = baiBL;
            Session["objPesquisa"] = ba;

            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "WinOpen('/Pesquisar.aspx?caixa=" + txtBairro.ClientID + "&id=" + hfIdBairro.ClientID + "&lbl=" + lblDesBairro.ClientID + "','',600,500);", true);
        }

        protected void btnPesNaturalidade_Click(object sender, EventArgs e)
        {
            CarregarTabelaPesquisaCidade();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "WinOpen('/Pesquisar.aspx?caixa=" + txtNaturalidade.ClientID + "&id=" + hfIdNaturalidade.ClientID + "&lbl=" + lblDesNaturalidade.ClientID + "','',600,500);", true);
        }

        protected void btnPesCidade_Click(object sender, EventArgs e)
        {
            CarregarTabelaPesquisaCidade();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "WinOpen('/Pesquisar.aspx?caixa=" + txtCidade.ClientID + "&id=" + hfIdCidade.ClientID + "&lbl=" + lblDesCidade.ClientID + "','',600,500);", true);
        }

        protected void btnPesCidProf_Click(object sender, EventArgs e)
        {
            CarregarTabelaPesquisaCidade();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "WinOpen('/Pesquisar.aspx?caixa=" + txtCidadeProf.ClientID + "&id=" + hfIdCidProf.ClientID + "&lbl=" + lblDesCidProf.ClientID + "','',600,500);", true);
        }
                
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewPessoa.aspx");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            PessoasBL pesBL = new PessoasBL();
            Pessoas pessoas = new Pessoas();

            pessoas.Id = utils.ComparaIntComZero(hfId.Value);
            //pessoas.Codigo = 
            pessoas.Nome = txtNome.Text;
            pessoas.NomeFantasia = txtNomeFantasia.Text;
            pessoas.CategoriaId = utils.ComparaIntComZero(hfIdCategoria.Value);
            pessoas.CpfCnpj = txtCpfCnpj.Text;
            pessoas.Rg = txtRg.Text;
            pessoas.DtNascimento = utils.ComparaDataComNull(txtDataNascimento.Text);
            pessoas.EstadoCivil = ddlEstadoCivil.SelectedValue;
            pessoas.NomeMae = txtNomeMae.Text;
            pessoas.NomePai = txtNomePai.Text;
            pessoas.Naturalidade = utils.ComparaIntComNull(hfIdNaturalidade.Value);
            pessoas.CidadeId = utils.ComparaIntComZero(hfIdCidade.Value);
            pessoas.Cep = txtCep.Text;
            pessoas.Endereco = txtEndereco.Text;
            pessoas.Numero = txtNumero.Text;
            pessoas.BairroId = utils.ComparaIntComZero(hfIdBairro.Value);
            pessoas.Complemento = txtComplemento.Text;
            pessoas.Empresa = txtEmpresa.Text;
            pessoas.EnderecoProf = txtEnderecoProf.Text;
            pessoas.NumeroProf = txtNumeroProf.Text;
            pessoas.CidadeProfId = utils.ComparaIntComNull(hfIdCidProf.Value);
            pessoas.ComplementoProf = txtComplementoProf.Text;
            pessoas.BairroProf = utils.ComparaIntComNull(hfIdBairroProf.Value);
            pessoas.CepProf = txtCepProf.Text;
            pessoas.Obs = txtObservacao.Text;
            pessoas.DtCadastro = DateTime.Now;
            
            if (pessoas.Id > 0)
                pesBL.EditarBL(pessoas);
            else
                pesBL.InserirBL(pessoas);

            Response.Redirect("viewPessoa.aspx");
        }

        protected void btnInserirTelefone_Click(object sender, EventArgs e)
        {
            bool altera = false;
            int codigo = 0;
            TelefonesBL telBL = new TelefonesBL();

            if (Session["dtTelefone"] != null)
                dtTelefones = (DataTable)Session["dtTelefone"];

            DataRow linha = dtTelefones.NewRow();

            codigo = utils.ComparaIntComZero(hfCodTelefone.Value);

            if (codigo == 0)
                codigo = telBL.RetornarMaxCodigoBL();

            if (dtTelefones.Rows.Contains(codigo))
            {
                linha = dtTelefones.Rows.Find(codigo);
                linha.BeginEdit();
                altera = true;
            }
            else
                altera = false;

            linha["CODIGO"] = codigo.ToString();
            linha["DDD"] = txtDDD.Text;
            linha["NUMERO"] = txtTelefone.Text;
            linha["DESCRICAO"] = ddlTipo.SelectedValue;

            if (altera)
                linha.EndEdit();
            else
                dtTelefones.Rows.Add(linha);

            Session["dtTelefone"] = dtTelefones;
            dtgTelefones.DataSource = dtTelefones;
            dtgTelefones.DataBind();

            txtTelefone.Text = "";
            txtDDD.Text = "";
            ddlTipo.SelectedIndex = 1;             
        }

              
    }
}