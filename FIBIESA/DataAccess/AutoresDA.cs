﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects;
using FG;
using System.Data.SqlClient;
using InfrastructureSqlServer.Helpers;
using System.Configuration;
using System.Data;

namespace DataAccess
{
    public class AutoresDA : BaseDA
    {
        Utils utils = new Utils();

        #region funcoes
        private List<Autores> CarregarObjAutores(SqlDataReader dr)
        {
            List<Autores> autor = new List<Autores>();

            while (dr.Read())
            {
                Autores aut = new Autores();
                aut.Id = int.Parse(dr["ID"].ToString());
                aut.Codigo = int.Parse(dr["CODIGO"].ToString());
                aut.Descricao = dr["DESCRICAO"].ToString();
                aut.TipoId = int.Parse(dr["TIPOID"].ToString());
                
                TiposDeAutoresDA tAutDA = new TiposDeAutoresDA();
                List<TiposDeAutores> tAut = tAutDA.PesquisarDA(aut.TipoId);
                TiposDeAutores tiposDeAutores = new TiposDeAutores();

                foreach (TiposDeAutores lttAut in tAut)
                {
                    tiposDeAutores.Id = lttAut.Id;
                    tiposDeAutores.Codigo = lttAut.Codigo;
                    tiposDeAutores.Descricao = lttAut.Descricao;

                    aut.TiposDeAutores = tiposDeAutores;
                }

                autor.Add(aut);
            }

            return autor;
        }

        private Int32 RetornaMaxCodigo()
        {
            Int32 codigo = 1;
            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                          CommandType.Text, string.Format(@" SELECT ISNULL(MAX(CODIGO),0) + 1 as COD FROM AUTORES "));

            if (ds.Tables[0].Rows.Count != 0)
                codigo = utils.ComparaIntComZero(ds.Tables[0].Rows[0]["COD"].ToString());

            return codigo;
        }


        #endregion

        public bool InserirDA(Autores instancia)
        {
            SqlParameter[] paramsToSP = new SqlParameter[3];

            paramsToSP[0] = new SqlParameter("@codigo", RetornaMaxCodigo());
            paramsToSP[1] = new SqlParameter("@descricao", instancia.Descricao.ToUpper());
            paramsToSP[2] = new SqlParameter("@tipoId", instancia.TipoId);

            try
            {
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                            CommandType.StoredProcedure, "stp_insert_autores", paramsToSP);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
           
        }

        public bool EditarDA(Autores instancia)
        {
            SqlParameter[] paramsToSP = new SqlParameter[4];

            paramsToSP[0] = new SqlParameter("@id", instancia.Id);
            paramsToSP[1] = new SqlParameter("@codigo", instancia.Codigo);
            paramsToSP[2] = new SqlParameter("@descricao", instancia.Descricao.ToUpper());
            paramsToSP[3] = new SqlParameter("@tipoId", instancia.TipoId);

            try
            {
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                            CommandType.StoredProcedure, "stp_update_autores", paramsToSP);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ExcluirDA(Autores instancia)
        {
            SqlParameter[] paramsToSP = new SqlParameter[1];

            paramsToSP[0] = new SqlParameter("@id", instancia.Id);

            try
            {
                SqlHelper.ExecuteNonQuery(
                    ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                    CommandType.StoredProcedure, "stp_delete_Autores", paramsToSP);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Autores> PesquisarDA()
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(
                ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                CommandType.Text, string.Format(@"SELECT * FROM AUTORES "));
            return CarregarObjAutores(dr);
        }

        public List<Autores> PesquisarDA(int id)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(
                ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                CommandType.Text, string.Format(@"SELECT * FROM AUTORES  WHERE ID = {0}", id));
            return CarregarObjAutores(dr);
        }

        public List<Autores> PesquisarDA(string campo, string valor)
        {
            StringBuilder consulta = new StringBuilder("SELECT * FROM AUTORES ");

            switch (campo.ToUpper())
            {
                case "CODIGO":
                    consulta.Append(string.Format("WHERE CODIGO = {0}", utils.ComparaIntComZero(valor)));
                    break;
                case "DESCRICAO":
                    consulta.Append(string.Format("WHERE DESCRICAO  LIKE '%{0}%'", valor));
                    break;
                default:
                    break;
            }

            SqlDataReader dr = SqlHelper.ExecuteReader(
                ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                CommandType.Text, consulta.ToString());

            return CarregarObjAutores(dr);
        }

        public List<Autores> PesquisarBuscaDA(string valor)
        {
            StringBuilder consulta = new StringBuilder(@"SELECT * FROM AUTORES ");

            if (valor != "" && valor != null)
                consulta.Append(string.Format(" WHERE CODIGO = {0} OR  DESCRICAO  LIKE '%{1}%'", utils.ComparaIntComZero(valor), valor));

            consulta.Append(" ORDER BY CODIGO ");

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());

            List<Autores> autores = CarregarObjAutores(dr);

            return autores;
        }

        public override List<Base> Pesquisar(string descricao)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(
                    ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                    CommandType.Text, string.Format(@"SELECT * FROM TIPOSDEAUTORES WHERE CODIGO = '{0}' OR DESCRICAO LIKE '%{1}%'",utils.ComparaIntComZero(descricao), descricao));
            
            List<Base> ba = new List<Base>();

            while (dr.Read())
            {
                Base bas = new Base();
                bas.PesId1 = int.Parse(dr["ID"].ToString());
                bas.PesCodigo = dr["CODIGO"].ToString();
                bas.PesDescricao = dr["DESCRICAO"].ToString();

                ba.Add(bas);
            }
            return ba;
        }
    }
}
