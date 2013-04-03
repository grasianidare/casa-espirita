﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects;
using DataAccess;
using System.Data;

namespace BusinessLayer
{
    public class ItensEstoqueBL
    {
        public bool InserirBL(ItensEstoque id_itEst)
        {
            /*criar as regras de negocio*/
            ItensEstoqueDA itenEstoqueDA = new ItensEstoqueDA();

            return itenEstoqueDA.InserirDA(id_itEst);
        }

        public bool EditarBL(ItensEstoque id_itEst)
        {
            /*criar as regras de negocio*/
            ItensEstoqueDA itensEstoqueDA = new ItensEstoqueDA();

            return itensEstoqueDA.EditarDA(id_itEst);
        }

        public bool ExcluirBL(ItensEstoque id_itEst)
        {
            /*criar as regras de negocio*/
            ItensEstoqueDA ItensEstoqueDA = new ItensEstoqueDA();

            return ItensEstoqueDA.ExcluirDA(id_itEst);
        }

        public List<ItensEstoque> PesquisarBL()
        {
            /*criar as regras de negocio*/
            ItensEstoqueDA itensEstoqueDA = new ItensEstoqueDA();

            return itensEstoqueDA.PesquisarDA();
        }

        public List<ItensEstoque> PesquisarBL(Int32 id_obra)
        {
            /*criar as regras de negocio*/
            ItensEstoqueDA itensEstoqueDA = new ItensEstoqueDA();

            return itensEstoqueDA.PesquisarDA(id_obra);
        }

        public List<ItensEstoque> PesquisarBL(string campo, string valor)
        {
            /*criar as regras de negocio*/
            ItensEstoqueDA itensEstoqueDA = new ItensEstoqueDA();

            return itensEstoqueDA.PesquisarDA(campo, valor);
        }

        public DataSet PesquisarItensEstoqueBL(int id_movEst)
        {
            ItensEstoqueDA itEstDA = new ItensEstoqueDA();

            return itEstDA.PesquisarItensEstoqueDA(id_movEst);
        }
    }
}