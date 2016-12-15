using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using DataObjects;

namespace BusinessLayer
{
    public class CategoriasBL : BaseBL
    {
        private bool IsValid(Categorias cat)
        {
            bool valido;
            valido = cat.Descricao.Length <= 70;          
            return valido;
        }

        public bool InserirBL(Categorias cat)
        {
            CategoriasDA categoriasDA = new CategoriasDA();
            if (IsValid(cat))
                return categoriasDA.InserirDA(cat);
            else
                return false;
        }

        public bool EditarBL(Categorias cat)
        {            
            CategoriasDA categoriasDA = new CategoriasDA();

            if (IsValid(cat))
                return categoriasDA.EditarDA(cat);
            else
                return false;
        }

        public bool ExcluirBL(Categorias cat)
        {
            /*criar as regras de negocio*/
            CategoriasDA categoriasDA = new CategoriasDA();

            return categoriasDA.ExcluirDA(cat);
        }

        public List<Categorias> PesquisarBL()
        {
            /*criar as regras de negocio*/
            CategoriasDA categoriasDA = new CategoriasDA();

            return categoriasDA.PesquisarDA();
        }

        public List<Categorias> PesquisarBL(int id_cat)
        {
            CategoriasDA categoriasDA = new CategoriasDA();

            return categoriasDA.PesquisarDA(id_cat);
        }

        public List<Categorias> PesquisarBL(string campo, string valor)
        {
            CategoriasDA categoriasDA = new CategoriasDA();

            return categoriasDA.PesquisarDA(campo, valor);
        }

        public override List<Base> Pesquisar(string codDes)
        {
            CategoriasDA catDA = new CategoriasDA();

            return catDA.Pesquisar(codDes);
        }

        public List<Categorias> PesquisarBuscaBL(string valor)
        {
            CategoriasDA categoriaDA = new CategoriasDA();

            return categoriaDA.PesquisarBuscaDA(valor);
        }
    }
}
