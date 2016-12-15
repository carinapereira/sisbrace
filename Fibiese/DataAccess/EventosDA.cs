﻿using System;
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
    public class EventosDA : BaseDA
    {
        Utils utils = new Utils();

        #region funcoes
        private List<Eventos> CarregarObjEventos(SqlDataReader dr)
        {
            List<Eventos> eventos = new List<Eventos>();

            while (dr.Read())
            {
                Eventos eve = new Eventos();
                eve.Id = int.Parse(dr["ID"].ToString());
                eve.Codigo = int.Parse(dr["CODIGO"].ToString());
                eve.Descricao = dr["DESCRICAO"].ToString();
                eve.DtInicio = Convert.ToDateTime(dr["DTINICIO"].ToString());
                eve.DtFim = Convert.ToDateTime(dr["DTFIM"].ToString());

                eventos.Add(eve);
            }
            return eventos;
        }

        private Int32 RetornaMaxCodigo()
        {
            Int32 codigo = 1;
            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                          CommandType.Text, string.Format(@" SELECT ISNULL(MAX(CODIGO),0) + 1 as COD FROM EVENTOS "));

            if (ds.Tables[0].Rows.Count != 0)
                codigo = utils.ComparaIntComZero(ds.Tables[0].Rows[0]["COD"].ToString());

            return codigo;

        }
        #endregion
        public bool InserirDA(Eventos eve)
        {
            StringBuilder query = new StringBuilder();
            SqlParameter[] paramsToSP = new SqlParameter[4];

            paramsToSP[0] = new SqlParameter("@codigo", RetornaMaxCodigo());
            paramsToSP[1] = new SqlParameter("@descricao", eve.Descricao.ToUpper());
            paramsToSP[2] = new SqlParameter("@dtinicio", eve.DtInicio);
            paramsToSP[3] = new SqlParameter("@dtfim", eve.DtFim);

            query.Append(@" insert into eventos(codigo, descricao, dtinicio, dtfim)");
            query.Append(@" values(@codigo, @descricao, @dtinicio, @dtfim)");

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

        public bool EditarDA(Eventos eve)
        {
            StringBuilder query = new StringBuilder();
            SqlParameter[] paramsToSP = new SqlParameter[5];

            paramsToSP[0] = new SqlParameter("@id", eve.Id);
            paramsToSP[1] = new SqlParameter("@codigo", eve.Codigo);
            paramsToSP[2] = new SqlParameter("@descricao", eve.Descricao.ToUpper());
            paramsToSP[3] = new SqlParameter("@dtinicio", eve.DtInicio);
            paramsToSP[4] = new SqlParameter("@dtfim", eve.DtFim);

            query.Append(@" update eventos set codigo=@codigo, descricao=@descricao, dtinicio=@dtinicio, dtfim=@dtfim");
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

        public bool ExcluirDA(Eventos eve)
        {           
            try
            {
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.Text,
                                          string.Format("DELETE FROM eventos WHERE id = {0}", eve.Id));    

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<Eventos> PesquisarDA()
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, string.Format(@"SELECT * " +
                                                                                                 "  FROM EVENTOS " +
                                                                                                 " WHERE CONVERT(DATETIME,GETDATE(),103)  + ' 00:00:00.000' " +
                                                                                                 " BETWEEN  CONVERT(DATETIME,DTINICIO,103) AND  CONVERT(DATETIME,DTFIM,103)  + ' 23:59:59.999'  "
                                                                                                ));

            List<Eventos> eventos = CarregarObjEventos(dr);

            dr.Close();
            return eventos;

        }

        public List<Eventos> PesquisarDA(int id_cur)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                       CommandType.Text, string.Format(@"SELECT * " +
                                                                                       " FROM EVENTOS WHERE ID = {0}", id_cur));

            List<Eventos> eventos = CarregarObjEventos(dr);
            dr.Close();
            return eventos;

        }

        public List<Eventos> PesquisarDA(string campo, string valor)
        {
            string consulta;

            switch (campo.ToUpper())
            {
                case "CODIGO":
                    consulta = string.Format("SELECT * FROM EVENTOS WHERE CODIGO = {0}", utils.ComparaIntComZero(valor));
                    break;
                case "DESCRICAO":
                    consulta = string.Format("SELECT * FROM EVENTOS WHERE DESCRICAO  LIKE '%{0}%'", valor);
                    break;
                default:
                    consulta = "";
                    break;
            }

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta);

            List<Eventos> eventos = CarregarObjEventos(dr);
            dr.Close();
            return eventos;
        }

        public List<Eventos> PesquisarBuscaDA(string valor)
        {
            StringBuilder consulta = new StringBuilder(@"SELECT * FROM EVENTOS ");

            if (valor != "")
                consulta.Append(string.Format(" WHERE CODIGO = {0} OR  DESCRICAO  LIKE '%{1}%'", utils.ComparaIntComZero(valor), valor));

            consulta.Append(" ORDER BY CODIGO ");

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());

            List<Eventos> eventos = CarregarObjEventos(dr);
            dr.Close();
            return eventos;
        }

        public override List<Base> Pesquisar(string descricao)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                      CommandType.Text, string.Format(@"SELECT * " +
                                                                                       " FROM EVENTOS WHERE CODIGO = '{0}' OR DESCRICAO LIKE '%{1}%'", utils.ComparaIntComZero(descricao), descricao));

            List<Base> ba = new List<Base>();

            while (dr.Read())
            {
                Base bas = new Base();
                bas.PesId1 = int.Parse(dr["ID"].ToString());
                bas.PesDescricao = dr["DESCRICAO"].ToString();

                ba.Add(bas);
            }

            dr.Close();
            return ba;
        }

        public List<Base> PesquisarEventos(string codigos)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                      CommandType.Text, string.Format(@"SELECT * " +
                                                                                       " FROM EVENTOS WHERE CODIGO IN ({0})", codigos));

            List<Base> ba = new List<Base>();

            while (dr.Read())
            {
                Base bas = new Base();
                bas.PesId1 = int.Parse(dr["ID"].ToString());
                bas.PesDescricao = dr["DESCRICAO"].ToString();

                ba.Add(bas);
            }

            dr.Close();
            return ba;
        }

        public DataSet PesquisarDataSet(string codDes, string dataInicio, string dataFim)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(@"SELECT codigo " +
                                    ",descricao " +
                                    ",dtInicio " +
                                    ",dtfIM " +
                                " FROM EVENTOS WHERE 1 = 1 ");

            if (codDes != string.Empty)
                sqlQuery.Append(@" AND codigo IN (" + codDes + ")");


            if ((dataInicio != string.Empty) && (dataFim != string.Empty))
                sqlQuery.Append(@" AND dtInicio >= CONVERT(DATETIME,'" + dataInicio + "',103) AND dtFim <= CONVERT(DATETIME,'" + dataFim + "',103)");

           

            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                      CommandType.Text, sqlQuery.ToString());


            return ds;
        }

        public DataSet PesquisarDataSet(string strPesquisa)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(@"SELECT id
                                  ,codEvento
                                  ,EVENTO
                                  ,dtInicio
                                  ,dtFimEvento
                                  ,codTurma
                                  ,TURMA
                                  ,sala
                                  ,horaini
                                  ,horafim
                                  ,dtIni
                                  ,dtFim
                                  ,nromax
                            FROM VIEW_EVENTOS WHERE 1 = 1 ");

            if (strPesquisa != string.Empty)
                sqlQuery.Append(@" AND EVENTO LIKE '%" + strPesquisa + "%' ");

            sqlQuery.Append(@" AND dtFimEvento > CONVERT(DATETIME, GETDATE(),103)");

            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                      CommandType.Text, sqlQuery.ToString());


            return ds;
        }

    }

}
