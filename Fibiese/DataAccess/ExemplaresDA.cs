﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects;
using FG;
using System.Data.SqlClient;
using InfrastructureSqlServer.Helpers;
using System.Configuration;
using System.Data;


namespace DataAccess
{
    public class ExemplaresDA : BaseDA
    {
        
        Utils utils = new Utils();
        #region funcoes
        private List<Exemplares> CarregarObjExemplares(SqlDataReader dr)
        {
            List<Exemplares> tipoObra = new List<Exemplares>();

            while (dr.Read())
            {
                Exemplares tipo = new Exemplares();
                tipo.Id = int.Parse(dr["ID"].ToString());
                tipo.Obraid = int.Parse(dr["OBRAID"].ToString());
                tipo.Tombo = int.Parse(dr["TOMBO"].ToString());
                tipo.Status = dr["STATUS"].ToString();                
                tipo.OrigemId = utils.ComparaIntComNull(dr["ORIGEMID"].ToString());
                tipo.Obs = dr["OBS"].ToString();
                              
                Obras obras = new Obras();

                obras.Id = int.Parse(dr["OBRAid"].ToString());
                obras.Codigo = int.Parse(dr["CODIGO"].ToString());
                obras.Titulo = dr["TITULO"].ToString();
                
                tipo.Obras = obras;
                              
                tipoObra.Add(tipo);
            }

            return tipoObra;
        }
        #endregion

        public bool InserirDA(Exemplares instancia)
        {
            StringBuilder query = new StringBuilder();
            SqlParameter[] paramsToSP = new SqlParameter[5];

            paramsToSP[0] = new SqlParameter("@obraid", instancia.Obraid);
            paramsToSP[1] = new SqlParameter("@status", instancia.Status);
            paramsToSP[2] = new SqlParameter("@tombo", instancia.Tombo);
            paramsToSP[3] = new SqlParameter("@origemId", instancia.OrigemId == null ? (object)DBNull.Value : instancia.OrigemId);
            paramsToSP[4] = new SqlParameter("@obs", instancia.Obs.ToUpper());

            query.Append(@" insert into exemplares(obraid, status, tombo, origemid, obs)");
            query.Append(@" values(@obraid, @status, @tombo, @origemid, @obs)");
                      
            try
            {
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                           CommandType.Text, query.ToString(), paramsToSP);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool EditarDA(Exemplares instancia)
        {
            StringBuilder query = new StringBuilder();
            SqlParameter[] paramsToSP = new SqlParameter[6];

            paramsToSP[0] = new SqlParameter("@id", instancia.Id);
            paramsToSP[1] = new SqlParameter("@obraid", instancia.Obraid);
            paramsToSP[2] = new SqlParameter("@status", instancia.Status);
            paramsToSP[3] = new SqlParameter("@tombo", instancia.Tombo);
            paramsToSP[4] = new SqlParameter("@origemId", instancia.OrigemId == null ? (object)DBNull.Value : instancia.OrigemId);
            paramsToSP[5] = new SqlParameter("@obs", instancia.Obs.ToUpper());

            query.Append(@" update exemplares set obraid=@obraid, status=@status, tombo=@tombo, origemid=@origemid, obs=@obs");
            query.Append(@" where id=@id");

            try
            {
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                            CommandType.Text, query.ToString(), paramsToSP);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ExcluirDA(Exemplares instancia)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.Text,
                                          string.Format("DELETE FROM exemplares WHERE id = {0}", instancia.Id));    
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<Exemplares> PesquisarDA()
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(
                ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                CommandType.Text, string.Format(@"SELECT E.* FROM EXEMPLARES E, OBRAS O WHERE E.OBRAID = O.ID ORDER BY O.CODIGO  "));
            
            List<Exemplares> exemplares = CarregarObjExemplares(dr);

            dr.Close();
            return exemplares;
        }

        public Exemplares LerDA(int id)
        {
            SqlDataReader ds = SqlHelper.ExecuteReader(
                ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                CommandType.Text, string.Format(@"SELECT E.*, O.ID IDOBRA, O.CODIGO, O.TITULO FROM EXEMPLARES E, "
                    + " OBRAS O  WHERE E.OBRAID = O.ID AND E.ID = {0}", id));

            List<Exemplares> oi = CarregarObjExemplares(ds);

            ds.Close();
            
            if (oi.Count == 1)
                return oi[0];
            else
                return null;
        }
        
        public DataSet PesquisarDA(int id)
        {
            DataSet ds = SqlHelper.ExecuteDataset(
                ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                CommandType.Text, string.Format(@"SELECT E.*, O.ID IDOBRA, O.CODIGO, O.TITULO FROM EXEMPLARES E, OBRAS O  WHERE E.OBRAID = O.ID AND E.ID = {0}", id));

            return ds;
        }

        public List<Exemplares> PesquisarDA(string campo, string valor)
        {
            StringBuilder consulta = new StringBuilder("SELECT E.*, O.codigo, o.titulo FROM EXEMPLARES E, OBRAS O WHERE E.OBRAID = O.ID ");

            switch (campo.ToUpper())
            {
                case "CODIGO":
                    consulta.Append(string.Format(" AND O.CODIGO = {0}", utils.ComparaIntComZero(valor)));
                    break;
                case "TITULO":
                    consulta.Append(string.Format(" AND O.TITULO  LIKE '%{0}%'", valor));
                    break;
                case "ID":
                    consulta.Append(string.Format(" AND E.ID  = {0}", valor));
                    break;
                default:
                    break;
            }

            SqlDataReader dr = SqlHelper.ExecuteReader(
                ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                CommandType.Text, consulta.ToString());
                      
            List<Exemplares> exemplares = CarregarObjExemplares(dr);

            dr.Close();
            return exemplares;
        }

        public List<Exemplares> PesquisarBuscaDA(string valor)
        {
            StringBuilder consulta = new StringBuilder(@"SELECT E.*, O.ID IDOBRA, O.CODIGO, O.TITULO FROM EXEMPLARES E, OBRAS O WHERE E.OBRAID = O.ID ");

            if (valor != "" && valor != null)
                consulta.Append(string.Format(" AND (O.CODIGO = {0} OR  O.TITULO  LIKE '%{1}%') ", utils.ComparaIntComZero(valor), valor));

            consulta.Append(" ORDER BY O.CODIGO ");

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());

            List<Exemplares> exemplares = CarregarObjExemplares(dr);

            dr.Close();
            return exemplares;
        }

         /// <summary>
        /// Pesquisa Exemplares disponiveis para emprestimo (não emprestados)
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<Exemplares> PesquisarDisponiveis(string valor)
        {
            //StringBuilder consulta = new StringBuilder(@"SELECT * FROM EXEMPLARES E, OBRAS O WHERE E.OBRAID = O.ID ");

            StringBuilder consulta = new StringBuilder(@"SELECT EX.*, OB.ID IDOBRA, OB.TITULO, OB.CODIGO FROM EXEMPLARES EX ");

            consulta.Append(" INNER JOIN OBRAS OB ON OB.ID = EX.OBRAID");
            consulta.Append(" WHERE EX.STATUS = 'A'");
            consulta.Append(" AND EX.ID NOT IN (");
            consulta.Append(" SELECT EXE.ID FROM EMPRESTIMOMOV MOV");
            consulta.Append(" INNER JOIN EMPRESTIMOS  EMP ON EMP.ID = MOV.EMPRESTIMOID");
            consulta.Append(" INNER JOIN EXEMPLARES EXE ON EXE.ID = EMP.EXEMPLARID");
            consulta.Append(" WHERE DATADEVOLUCAO IS NULL)");

            if (valor != "" && valor != null)
                consulta.Append(string.Format(" AND (EX.TOMBO = {0} OR  UPPER(OB.TITULO)  LIKE '%{1}%') ", utils.ComparaIntComZero(valor), valor.ToUpper()));

            consulta.Append(" ORDER BY EX.TOMBO ");

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());

            List<Exemplares> exemplares = CarregarObjExemplares(dr);

            dr.Close();
            return exemplares;
        }

        public DataSet PesquisarExemplaresEmprestimo(string valor)
        {
            StringBuilder consulta = new StringBuilder();

            consulta.Append(@"SELECT EX.* "+
                             ", OB.TITULO "+
                             ", OB.CODIGO "+ 
                             ", TOB.QTDDIAS "+
                             ",(SELECT MOV.DATAPREVISTAEMPRESTIMO "+
                             "    FROM EMPRESTIMOMOV MOV "+
                             "        ,EMPRESTIMOS  EMP "+
                             "   WHERE MOV.DATADEVOLUCAO IS NULL "+ 
                             "     AND MOV.EMPRESTIMOID = EMP.ID "+
                             "     AND EMP.EXEMPLARID = EX.ID ) DATAPREVISTAEMPRESTIMO " +
                             "   FROM EXEMPLARES EX  "+
                             "   INNER JOIN OBRAS OB ON OB.ID = EX.OBRAID "+
                             "   INNER JOIN TIPOSOBRAS TOB ON TOB.ID = OB.TIPOSOBRAID " + 
                             "   WHERE EX.STATUS = 'A' ");

            if (valor != "" && valor != null)
                consulta.Append(string.Format(" AND EX.TOMBO = {0} ", utils.ComparaIntComZero(valor)));

            consulta.Append(" ORDER BY OB.CODIGO ");

            DataSet ds = SqlHelper.ExecuteDataset(
                ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                CommandType.Text, consulta.ToString());

            return ds;
 
        }

        public DataSet PesquisarExemplaresDevolucao(string valor)
        {
            StringBuilder consulta = new StringBuilder();

            consulta.Append(@"SELECT OB.TITULO " +
                             "      ,EX.TOMBO  " +
                             "      ,EM.ID " +
                             "      ,P.NOME " +
                             "      ,P.ID PES_ID " +
                             "      ,MOV.DATAPREVISTAEMPRESTIMO " +
                             "      ,MOV.ID MOVID " +
                             "      ,CASE WHEN (MOV.DATAPREVISTAEMPRESTIMO < CAST (GETDATE() AS DATE)) " +
                             "           THEN 'Atrasado' ELSE 'Emprestado' END AS SITUACAO " +
                             "  FROM EXEMPLARES EX " +
                             "      ,EMPRESTIMOS EM " +
                             "      ,EMPRESTIMOMOV MOV " +
                             "      ,OBRAS OB " +
                             "      ,PESSOAS P " +
                             " WHERE EX.ID = EM.EXEMPLARID " +
                             "   AND EM.ID = MOV.EMPRESTIMOID  " +   
                             "   AND EX.OBRAID = OB.ID  " + 
                             "   AND EM.PESSOAID = P.ID " +
                             "   AND MOV.DATADEVOLUCAO IS NULL ");                             

            if (valor != "" && valor != null)
                consulta.Append(string.Format(" AND EX.TOMBO = {0} ", utils.ComparaIntComZero(valor)));
                 

            DataSet ds = SqlHelper.ExecuteDataset(
                ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                CommandType.Text, consulta.ToString());

            return ds;

        }

        public override List<Base> Pesquisar(string descricao)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(
                    ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                    CommandType.Text, string.Format(@"SELECT * FROM EXEMPLARES WHERE CODIGO = '{0}' OR DESCRICAO LIKE '%{1}%'", utils.ComparaIntComZero(descricao), descricao));
            
            List<Base> ba = new List<Base>();

            while (dr.Read())
            {
                Base bas = new Base();
                bas.PesId1 = int.Parse(dr["ID"].ToString());
                bas.PesCodigo = dr["CODIGO"].ToString();
                bas.PesDescricao = dr["DESCRICAO"].ToString();

                ba.Add(bas);
            }

            dr.Close();
            return ba;
        }

        public DataSet PesquisarBuscaExemplaresDA(string tombo)
        {
            StringBuilder consulta = new StringBuilder(@"SELECT E.*, O.ID IDOBRA, O.CODIGO, O.TITULO FROM EXEMPLARES E, OBRAS O WHERE E.OBRAID = O.ID ");

            if (tombo != string.Empty && tombo != null)
                consulta.Append(string.Format(" AND E.TOMBO IN ({0})  ", tombo));

            consulta.Append(" ORDER BY O.CODIGO, E.TOMBO ");

            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());

            return ds;
        }

        public bool CodigoJaUtilizadoDA(Int32 codigo)
        {
            DataSet dsInst = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                       CommandType.Text, string.Format(@"SELECT 1 COD " +
                                                                                        "  FROM EXEMPLARES " +
                                                                                        " WHERE TOMBO = {0} ", codigo));
            int cod = 0;

            if (dsInst.Tables[0].Rows.Count != 0)
                cod = (int)dsInst.Tables[0].Rows[0]["COD"];

            if (cod == 1)
                return true;
            else
                return false;

        }

        public DataSet PesquisarDataSet(string strPesquisa)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(@"SELECT id,
                                     obraid,
                                     titulo,
                                     autor,
                                     status
                              FROM VIEW_EXEMPLARES  WHERE 1 = 1"); 
           

            if (strPesquisa != string.Empty)
                sqlQuery.Append(@"AND titulo LIKE '%" + strPesquisa + "%' ");
                sqlQuery.Append(@" OR autor LIKE '%" + strPesquisa + "%' ");

            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                      CommandType.Text, sqlQuery.ToString());


            return ds;
        }

    }
}