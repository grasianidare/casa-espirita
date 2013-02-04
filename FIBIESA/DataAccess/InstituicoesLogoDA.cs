﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects;
using System.Data;
using System.Data.SqlClient;
using InfrastructureSqlServer.Helpers;
using System.Configuration;

namespace DataAccess
{
    public class InstituicoesLogoDA
    {
        #region funcoes
        private List<InstituicoesLogo> CarregarObjInstituicoes(SqlDataReader dr)
        {
            List<InstituicoesLogo> instituicoesLogo = new List<InstituicoesLogo>();

            while (dr.Read())
            {
                InstituicoesLogo insL = new InstituicoesLogo();
                insL.Id = int.Parse(dr["ID"].ToString());
                insL.InstituicoesId = int.Parse(dr["INSTITUICOESID"].ToString());
                //insL.Imagem = dr["IMAGEM"].ToString();
                
                instituicoesLogo.Add(insL);
            }
            return instituicoesLogo;
        }
        #endregion

        public bool InserirDA(InstituicoesLogo insL)
        {
            SqlParameter[] paramsToSP = new SqlParameter[2];

            paramsToSP[0] = new SqlParameter("@instituicoesid", insL.InstituicoesId);
            paramsToSP[1] = new SqlParameter("@imagem", insL.Imagem);
                       
            //SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.StoredProcedure, "", paramsToSP);

            return true;
        }

        public bool EditarDA(InstituicoesLogo insL)
        {
            SqlParameter[] paramsToSP = new SqlParameter[3];

            paramsToSP[0] = new SqlParameter("@id", insL.Id);
            paramsToSP[1] = new SqlParameter("@instituicoesid", insL.InstituicoesId);
            paramsToSP[2] = new SqlParameter("@imagem", insL.Imagem);
           
            //SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.StoredProcedure, "", paramsToSP);

            return true;
        }

        public bool ExcluirDA(InstituicoesLogo insL)
        {
            SqlParameter[] paramsToSP = new SqlParameter[1];

            paramsToSP[0] = new SqlParameter("@id", insL.Id);

            //SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.StoredProcedure, "", paramsToSP);

            return true;
        }

        public List<InstituicoesLogo> PesquisarDA()
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, string.Format(@"SELECT * FROM INSTITUICOESLOGO "));

            List<InstituicoesLogo> instituicoesLogo = CarregarObjInstituicoes(dr);

            return instituicoesLogo;

        }

        public List<InstituicoesLogo> PesquisarDA(int id_insL)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                       CommandType.Text, string.Format(@"SELECT * " +
                                                                                       " FROM INSTITUICOESLOGO WHERE ID = {0}", id_insL));

            List<InstituicoesLogo> instituicoesLogo = CarregarObjInstituicoes(dr);

            return instituicoesLogo;
        }
    }
}
