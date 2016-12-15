﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObjects
{
    public class Usuarios
    {
        private Int32 _id;
        public Int32 Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
               
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private Int32? _nrTentLogin;
        public Int32? NrTentLogin
        {
            get { return _nrTentLogin; }
            set { _nrTentLogin = value; }
        }

        private DateTime? _dhTentLogin;
        public DateTime? DhTentLogin
        {
            get { return _dhTentLogin; }
            set { _dhTentLogin = value; }
        }

        private Int32 _categoriaId;
        public Int32 CategoriaId
        {
            get { return _categoriaId; }
            set { _categoriaId = value; }
        }

        private Categorias _categoria;
        public Categorias Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }
        
        private Int32 _usuarioId;

        public Int32 UsuarioId
        {
            get { return _usuarioId; }
            set { _usuarioId = value; }
        }

    }
}
