﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects;
using System.Data;
using System.Data.SqlClient;
using InfrastructureSqlServer.Helpers;
using System.Configuration;
using FG;

namespace DataAccess
{
    public class DoacoesDA
    {
        Utils utils = new Utils();
        #region funcoes
        private List<Doacoes> CarregarObjDoacoes(SqlDataReader dr)
        {
            PessoasDA pesDA = new PessoasDA();
            UsuariosDA usuDA = new UsuariosDA();
            List<Doacoes> Doacoes = new List<Doacoes>();

            while (dr.Read())
            {
                Doacoes doa = new Doacoes();
                doa.Id = int.Parse(dr["ID"].ToString());
                doa.PessoaId = int.Parse(dr["PESSOAID"].ToString());
                doa.Data = Convert.ToDateTime(dr["DATA"].ToString());
                doa.Valor = Convert.ToDecimal(dr["VALOR"].ToString());
                doa.UsuarioId = int.Parse(dr["USUARIOID"].ToString());

                if (doa.PessoaId > 0)
                {
                    List<Pessoas> pes = pesDA.PesquisarDA(doa.PessoaId);
                    Pessoas pessoa = new Pessoas();

                    foreach (Pessoas ltPes in pes)
                    {
                        pessoa.Id = ltPes.Id;
                        pessoa.Codigo = ltPes.Codigo;
                        pessoa.Nome = ltPes.Nome;
                    }

                    doa.Pessoa = pessoa;
                }

                if (doa.UsuarioId > 0)
                {
                    List<Usuarios> usu = usuDA.PesquisarDA(doa.UsuarioId);
                    Usuarios usuarios = new Usuarios();

                    foreach (Usuarios ltUsu in usu)
                    {
                        usuarios.Id = ltUsu.Id;
                        usuarios.Login = ltUsu.Login;
                        usuarios.Nome = ltUsu.Nome;
                    }

                    doa.Usuario = usuarios;
                }

                Doacoes.Add(doa);
            }
            return Doacoes;
        }
        #endregion

        public bool InserirDA(Doacoes doa)
        {
            SqlParameter[] paramsToSP = new SqlParameter[4];

            paramsToSP[0] = new SqlParameter("@pessoaid", doa.PessoaId);
            paramsToSP[1] = new SqlParameter("@data", doa.Data);
            paramsToSP[2] = new SqlParameter("@valor", doa.Valor);
            paramsToSP[3] = new SqlParameter("@usuarioid", doa.UsuarioId);

            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.StoredProcedure, "stp_insert_doacoes", paramsToSP);

            return true;
        }

        public bool EditarDA(Doacoes doa)
        {
            SqlParameter[] paramsToSP = new SqlParameter[5];

            paramsToSP[0] = new SqlParameter("@id", doa.Id);
            paramsToSP[1] = new SqlParameter("@pessoaid", doa.PessoaId);
            paramsToSP[2] = new SqlParameter("@data", doa.Data);
            paramsToSP[3] = new SqlParameter("@valor", doa.Valor);
            paramsToSP[4] = new SqlParameter("@usuarioid", doa.UsuarioId);

            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.StoredProcedure, "stp_update_doacoes", paramsToSP);

            return true;
        }

        public bool ExcluirDA(Doacoes doa)
        {
            SqlParameter[] paramsToSP = new SqlParameter[1];

            paramsToSP[0] = new SqlParameter("@id", doa.Id);

            SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.StoredProcedure, "stp_delete_doacoes", paramsToSP);

            return true;
        }

        public List<Doacoes> PesquisarDA()
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, string.Format(@"SELECT * FROM DOACOES "));

            List<Doacoes> Doacoes = CarregarObjDoacoes(dr);
           
            return Doacoes;

        }

        public List<Doacoes> PesquisarBuscaDA(string valor)
        {
            StringBuilder consulta = new StringBuilder(@"SELECT * FROM DOACOES D, PESSOAS P  WHERE D.PESSOAID = P.ID ");

            if (valor != "" && valor != null)
                consulta.Append(string.Format(" AND (P.CODIGO = {0} OR  P.NOME  LIKE '%{1}%')", utils.ComparaIntComZero(valor), valor));

            consulta.Append(" ORDER BY CODIGO ");

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());

            List<Doacoes> doacoes = CarregarObjDoacoes(dr);

            return doacoes;
        }
        
        public List<Doacoes> PesquisarDA(int id_doa)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                       CommandType.Text, string.Format(@"SELECT * " +
                                                                                       " FROM DOACOES WHERE ID = {0}", id_doa));

            List<Doacoes> Doacoes = CarregarObjDoacoes(dr);
                        
            return Doacoes;
        }
              

    }
}
