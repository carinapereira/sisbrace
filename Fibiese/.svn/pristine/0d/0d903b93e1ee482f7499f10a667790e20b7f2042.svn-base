using System;
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
    public class JurosMultasDA
    {
        Utils utils = new Utils();
        #region funcoes
        private List<JurosMultas> CarregarObjJurosMultas(SqlDataReader dr)
        {
            List<JurosMultas> jurosMultas = new List<JurosMultas>();

            while (dr.Read())
            {
                JurosMultas jm = new JurosMultas();
                jm.Id = int.Parse(dr["ID"].ToString());
                jm.MesAno = Convert.ToDateTime(dr["MESANO"].ToString());
                jm.PercJurosDias = utils.ComparaDecimalComZero(dr["PERCJUROSDIA"].ToString());
                jm.PercJurosMes = utils.ComparaDecimalComZero(dr["PERCJUROSMES"].ToString());
                jm.PercMultaDias = utils.ComparaDecimalComZero(dr["PERCMULTADIA"].ToString());
                jm.PercMultaMes = utils.ComparaDecimalComZero(dr["PERCMULTAMES"].ToString());

                jurosMultas.Add(jm);
            }
            return jurosMultas;  
        }
        #endregion
        public bool InserirDA(JurosMultas jm)
        {
            StringBuilder query = new StringBuilder();
            SqlParameter[] paramsToSP = new SqlParameter[5];

            paramsToSP[0] = new SqlParameter("@mesano", jm.MesAno);
            paramsToSP[1] = new SqlParameter("@percjurosdia", jm.PercJurosDias);
            paramsToSP[2] = new SqlParameter("@percjurosmes", jm.PercJurosMes);
            paramsToSP[3] = new SqlParameter("@percmultadia", jm.PercMultaDias);
            paramsToSP[4] = new SqlParameter("@percmultames", jm.PercMultaMes);

            query.Append(@" insert into jurosmultas(mesano, percjurosdia, percjurosmes, percmultadia, percmultames)");
            query.Append(@" values(@mesano, @percjurosdia, @percjurosmes, @percmultadia, @percmultames)");

            try
            {
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.Text, query.ToString(), paramsToSP);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EditarDA(JurosMultas jm)
        {
            StringBuilder query = new StringBuilder();
            SqlParameter[] paramsToSP = new SqlParameter[6];

            paramsToSP[0] = new SqlParameter("@id", jm.Id);
            paramsToSP[1] = new SqlParameter("@mesano", jm.MesAno);
            paramsToSP[2] = new SqlParameter("@percjurosdia", jm.PercJurosDias);
            paramsToSP[3] = new SqlParameter("@percjurosmes", jm.PercJurosMes);
            paramsToSP[4] = new SqlParameter("@percmultadia", jm.PercMultaDias);
            paramsToSP[5] = new SqlParameter("@percmultames", jm.PercMultaMes);

            query.Append(@" update jurosmultas set mesano=@mesano, percjurosdia=@percjurosdia, percjurosmes=@percjurosmes, percmultadia=@percmultadia, percmultames=@percmultames");
            query.Append(@" where id=@id");

            try
            {
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.Text, query.ToString(), paramsToSP);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ExcluirDA(JurosMultas jm)
        {          
            try
            {
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.Text,
                                          string.Format("DELETE FROM jurosmultas WHERE id = {0}", jm.Id));    

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<JurosMultas> PesquisarDA()
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, string.Format(@"SELECT * FROM JUROSMULTAS "));

            List<JurosMultas> jurosMultas = CarregarObjJurosMultas(dr);
            
            dr.Close();
            return jurosMultas;    
        }

        public List<JurosMultas> PesquisarDA(int id_jm)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                               CommandType.Text, string.Format(@"SELECT * FROM JUROSMULTAS WHERE ID = {0}",id_jm));

            List<JurosMultas> jurosMultas = CarregarObjJurosMultas(dr);

            dr.Close();
            return jurosMultas; 
        }

        public List<JurosMultas> PesquisarDA(string campo, DateTime? valor)
        {
            string consulta;
            
            
            switch (campo.ToUpper())
            {
                case "MESANO":
                    consulta = string.Format("SELECT * FROM JUROSMULTAS WHERE MESANO = CONVERT(DATETIME,'{0}',101)", valor != null? Convert.ToDateTime(valor).ToString("MM/yyyy") : "");
                    break;                
                default:
                    consulta = "";
                    break;
            }

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta);

            List<JurosMultas> jurosMultas = CarregarObjJurosMultas(dr);
            dr.Close();
            return jurosMultas;
        }
    }
}
