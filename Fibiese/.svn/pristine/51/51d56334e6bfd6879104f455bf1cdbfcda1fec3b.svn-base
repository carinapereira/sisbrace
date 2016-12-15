using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects;
using DataAccess;
using System.Data;
using FG;

namespace BusinessLayer
{
    public class ObrasBL : BaseBL
    {
        Utils utils = new Utils();

        private bool IsValid(Obras instancia)
        {
            bool valido;
            valido = instancia.Titulo.Length <= 100;

            return valido;
        }

        public Int32 InserirBL(Obras instancia)
        {
            if (IsValid(instancia))
            {
                ObrasDA varDA = new ObrasDA();

                return varDA.InserirDA(instancia);
            }
            else
                return 0;
        }

        public bool EditarBL(Obras instancia)
        {
            if (instancia.Id > 0 && IsValid(instancia))
            {
                ObrasDA varDA = new ObrasDA();

                return varDA.EditarDA(instancia);
            }
            else
                return false;
        }

        public bool ExcluirBL(Obras instancia)
        {
            if (instancia.Id > 0)
            {
                ObrasDA varDA = new ObrasDA();

                return varDA.ExcluirDA(instancia);
            }
            else
                return false;
        }

        public List<Obras> PesquisarBL()
        {
            /*criar as regras de negocio*/
            ObrasDA obrasDA = new ObrasDA();

            return obrasDA.PesquisarDA();
        }

        public DataSet PesquisarBL(int obra_id)
        {
            ObrasDA ObrasDA = new ObrasDA();

            return ObrasDA.PesquisarDA(obra_id);
        }

        public List<Obras> PesquisarBL(string campo, string valor)
        {
            ObrasDA obrasDA = new ObrasDA();

            return obrasDA.PesquisarDA(campo, valor);
        }

        public List<Obras> PesquisarBuscaBL(string valor)
        {
            /*criar as regras de negocio*/
            ObrasDA obrasDA = new ObrasDA();

            return obrasDA.PesquisarBuscaDA(valor);
        }

        public override List<Base> Pesquisar(string codDes)
        {
            ObrasDA obraDA = new ObrasDA();

            return obraDA.Pesquisar(codDes);
        }

        /// <summary>
        /// pesquisa realizada com mais de um id 
        /// </summary>
        /// <param name="valor">Id das obras separado por virgula</param>
        /// <returns>Retorna um List<Base></returns>
        public List<Base> PesquisarObras(string cods)
        {
            ObrasDA obraDA = new ObrasDA();

            return obraDA.PesquisarObras(cods);
        }

        public Obras ImportarAquivoMarc(string arquivoMarc)
        {
            string codigo;
            Obras obra = new Obras();

            string[] allLines = arquivoMarc.Split('\n');
            string[] dados; 

            List<string> myList = new List<string>();

            foreach (string linha in allLines)
            {
                codigo = linha.Substring(0, 3);
                switch (codigo)
                {
                    case "245":
                        {
                            dados = linha.Split('|');
                            obra.Titulo = dados[1];
                            break;
                        }
                    case "260":
                        {
                            dados = linha.Split('|');
                            foreach (string subLinha in dados)
                            {
                                switch (subLinha.Substring(0, 1))
                                {
                                    case "a": obra.LocalPublicacao = dados[1].Replace("a", "");
                                        break;
                                    case "b": obra.Editora = dados[1].Replace("b", "");
                                        break;
                                    case "c": obra.AnoPublicacao = utils.ComparaIntComNull(dados[1].Replace("c", ""));
                                        break;
                                }

                            }
                            break;
                        }
                    case "300":
                        {
                            dados = linha.Split('|');
                            obra.NroPaginas = utils.ComparaIntComNull(dados[1].Replace("a", ""));
                            break;
                        }
                    case "650":
                        {
                            dados = linha.Split('|');
                            foreach (string subLinha in dados)
                            {
                                obra.AssuntosAborda += subLinha;
                            }

                            break;
                        }
                    default:
                        return obra;
                }
                
            }
            
            return obra; 
        }
    }
}
