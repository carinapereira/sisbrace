using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects;
using DataAccess;
using System.Data;

namespace BusinessLayer
{
    public class EventosBL : BaseBL
    {
        private EventosDA eveDA = new EventosDA();

        public bool InserirBL(Eventos eve)
        {  
            return eveDA.InserirDA(eve);
        }

        public bool EditarBL(Eventos eve)
        {
            return eveDA.EditarDA(eve);
        }

        public bool ExcluirBL(Eventos eve)
        {
            return eveDA.ExcluirDA(eve);
        }

        public List<Eventos> PesquisarBL()
        {         
            return eveDA.PesquisarDA();
        }

        public List<Eventos> PesquisarBL(int eve)
        {          
            return eveDA.PesquisarDA(eve);
        }

        public List<Eventos> PesquisarBL(string campo, string valor)
        {
            return eveDA.PesquisarDA(campo,valor);
        }

        public List<Eventos> PesquisarBuscaBL(string valor)
        {
            return eveDA.PesquisarBuscaDA(valor);
        }

        public override List<Base> Pesquisar(string codDes)
        {
             return eveDA.Pesquisar(codDes);
        }

        public List<Base> PesquisarEventos(string codDes)
        {
            return eveDA.PesquisarEventos(codDes);
        }

        public DataSet PesquisarDataset(string codDes, string dataInicio, string dataFim)
        {
            return eveDA.PesquisarDataSet(codDes,dataInicio,dataFim);
        }

        public DataSet PesquisarDataset(string strPesquisa)
        {
            return eveDA.PesquisarDataSet(strPesquisa);
        }

    }
}
