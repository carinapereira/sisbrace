﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects;
using InfrastructureSqlServer.Helpers;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using FG;

namespace DataAccess
{
    public class TurmasDiarioDA
    {
        Utils utils = new Utils();

        public Int32 InserirDA(TurmasDiario tur)
        {
            StringBuilder query = new StringBuilder();
            SqlParameter[] paramsToSP = new SqlParameter[3];

            paramsToSP[0] = new SqlParameter("@data", tur.Data);
            paramsToSP[1] = new SqlParameter("@obs", tur.Obs);
            paramsToSP[2] = new SqlParameter("@turmaId", tur.TurmaId);

            query.Append(@" insert into turmasdiario(data, obs, turmaid)");
            query.Append(@" values(@data, @obs, @turmaid) SELECT SCOPE_IDENTITY()");

            try
            {
                var id = SqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.Text, query.ToString(), paramsToSP);

                return Convert.ToInt32(id);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public bool EditarDA(TurmasDiario tur)
        {
            StringBuilder query = new StringBuilder();
            SqlParameter[] paramsToSP = new SqlParameter[4];

            paramsToSP[0] = new SqlParameter("@id", tur.Id);
            paramsToSP[1] = new SqlParameter("@data", tur.Data);
            paramsToSP[2] = new SqlParameter("@obs", tur.Obs);
            paramsToSP[3] = new SqlParameter("@turmaId", tur.TurmaId);

            query.Append(@" update turmasdiario set data=@data, obs=@obs, turmaid=@turmaid");
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

        public bool ExcluirDA(TurmasDiario tur)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.Text,
                                          string.Format("DELETE FROM turmasdiario WHERE id = {0}", tur.Id));    

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public DataSet PesquisarDA(int id_tur, DateTime data)
        {
            StringBuilder v_query = new StringBuilder();

            v_query.Append(@"SELECT id,obs ");
            v_query.Append(@" FROM turmasDiario");
            v_query.Append(@" WHERE CONVERT(DATE,data,103) = CONVERT(DATE,'" + data + "',103)");
            v_query.Append(@"  AND turmaId = {0} ");


            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                       CommandType.Text, string.Format(v_query.ToString(), id_tur));

            return ds;
        }
    }
}