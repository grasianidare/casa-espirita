﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects;
using DataAccess;

namespace BusinessLayer
{
    public class UsuariosBL
    {
        public bool InserirBL(Usuarios usu)
        {
            /*criar as regras de negocio*/
            UsuariosDA pessoasDA = new UsuariosDA();

            return pessoasDA.InserirDA(usu);
        }

        public bool EditarBL(Usuarios usu)
        {
            /*criar as regras de negocio*/
            UsuariosDA pessoasDA = new UsuariosDA();

            return pessoasDA.EditarDA(usu);
        }

        public bool ExcluirBL(Usuarios usu)
        {
            /*criar as regras de negocio*/
            UsuariosDA pessoasDA = new UsuariosDA();

            return pessoasDA.ExcluirDA(usu);
        }

        public List<Usuarios> PesquisarBL()
        {
            /*criar as regras de negocio*/
            UsuariosDA usuariosDA = new UsuariosDA();

            return usuariosDA.PesquisarDA();
        }
    }
}