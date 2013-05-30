﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects;
using DataAccess;
using System.Data;

namespace BusinessLayer
{
    public class DoacoesBL
    {
        public bool InserirBL(Doacoes doa)
        {
            /*criar as regras de negocio*/
            DoacoesDA doacoesDA = new DoacoesDA();

            return doacoesDA.InserirDA(doa);
        }

        public bool EditarBL(Doacoes doa)
        {
            /*criar as regras de negocio*/
            DoacoesDA doacoesDA = new DoacoesDA();

            return doacoesDA.InserirDA(doa);
        }

        public bool ExcluirBL(Doacoes doa)
        {
            /*criar as regras de negocio*/
            DoacoesDA doacoesDA = new DoacoesDA();

            return doacoesDA.ExcluirDA(doa);
        }
        
        public List<Doacoes> PesquisarBL()
        {
            /*criar as regras de negocio*/
            DoacoesDA doacoesDA = new DoacoesDA();

            return doacoesDA.PesquisarDA();
        }

        public List<Doacoes> PesquisarBuscaBL(string valor)
        {
            DoacoesDA doacoesDA = new DoacoesDA();

            return doacoesDA.PesquisarBuscaDA(valor);
        }

        public List<Doacoes> PesquisarBL(int id_doa)
        {
            /*criar as regras de negocio*/
            DoacoesDA doacoesDA = new DoacoesDA();

            return doacoesDA.PesquisarDA(id_doa);
        }

        public DataSet PesquisarDataset(string codPessoa,string valorIni,string valorFim,string dataIni,string dataFim)
        {
            /*criar as regras de negocio*/
            DoacoesDA doacoesDA = new DoacoesDA();

            return doacoesDA.PesquisarDataset(codPessoa,valorIni,valorFim,dataIni,dataFim);
        }

        public DataSet PesquisarDataset(int id_doa)
        {
            /*criar as regras de negocio*/
            DoacoesDA doacoesDA = new DoacoesDA();

            return doacoesDA.PesquisarDataSet(id_doa);
        }
    }
}