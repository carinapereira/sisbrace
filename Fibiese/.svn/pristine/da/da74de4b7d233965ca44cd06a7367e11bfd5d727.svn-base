using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects;
using DataAccess;
using System.Data;

namespace BusinessLayer
{
    public class FormulariosBL
    {
        public bool InserirBL(Formularios formu)
        {
            /*criar as regras de negocio*/
            FormulariosDA formulariosDA = new FormulariosDA();

            return formulariosDA.InserirDA(formu);
        }

        public bool EditarBL(Formularios formu)
        {
            /*criar as regras de negocio*/
            FormulariosDA formulariosDA = new FormulariosDA();

            return formulariosDA.EditarDA(formu);
        }

        public bool ExcluirBL(Formularios formu)
        {
            /*criar as regras de negocio*/
            FormulariosDA formulariosDA = new FormulariosDA();

            return formulariosDA.ExcluirDA(formu);
        }

        public List<Formularios> PesquisarBL()
        {
            /*criar as regras de negocio*/
            FormulariosDA formulariosDA = new FormulariosDA();

            return formulariosDA.PesquisarDA();
        }

        public List<Formularios> PesquisarBL(string modulo)
        {
            /*criar as regras de negocio*/
            FormulariosDA formulariosDA = new FormulariosDA();

            return formulariosDA.PesquisarDA(modulo);
        }

        public List<Formularios> PesquisarBL(int id_for)
        {
            /*criar as regras de negocio*/
            FormulariosDA formulariosDA = new FormulariosDA();

            return formulariosDA.PesquisarDA(id_for);
        }              

        public List<Formularios> PesquisarBuscaBL(string valor)
        {
            /*criar as regras de negocio*/
            FormulariosDA formulariosDA = new FormulariosDA();

            return formulariosDA.PesquisarBuscaDA(valor);
        }

        public DataSet PesquisarForumlariosDA()
        {
            FormulariosDA formulariosDA = new FormulariosDA();
            return formulariosDA.PesquisarForumlariosDA();
        }
    }
}
