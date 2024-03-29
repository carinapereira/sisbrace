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
    public class ReservasDA : BaseDA
    {
        Utils utils = new Utils();
        #region funcoes
        private List<Reservas> CarregarObjReservas(SqlDataReader dr)
        {
            List<Reservas> reservas = new List<Reservas>();

            while (dr.Read())
            {
                Reservas reserva = new Reservas();
                reserva.Id = int.Parse(dr["ID"].ToString());
                reserva.PessoaId = int.Parse(dr["PESSOAID"].ToString());
                reserva.ExemplarId = int.Parse(dr["EXEMPLARID"].ToString());
                reserva.DataInicio = Convert.ToDateTime(dr["DATAINICIO"].ToString());
                reserva.DataFim = Convert.ToDateTime(dr["DATAFIM"].ToString());
                reservas.Add(reserva);
            }

            return reservas;
        }
        #endregion

        public bool InserirDA(Reservas instancia)
        {
            StringBuilder query = new StringBuilder();
            SqlParameter[] paramsToSP = new SqlParameter[4];

            paramsToSP[0] = new SqlParameter("@pessoaId", instancia.PessoaId);
            paramsToSP[1] = new SqlParameter("@exemplarId", instancia.ExemplarId);
            paramsToSP[2] = new SqlParameter("@dataInicio", instancia.DataInicio);
            paramsToSP[0] = new SqlParameter("@dataFim", instancia.DataFim);

            query.Append(@" insert into reservas(pessoaid, exemplarid, datainicio, datafim)");
            query.Append(@" values(@pessoaid, @exemplarid, @datainicio, @datafim)");

            try
            {
                return (SqlHelper.ExecuteNonQuery(
                    ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                    CommandType.Text, query.ToString(), paramsToSP) > 0);
            }
            catch (Exception e)
            {
                return false;
            }


        }

        public bool EditarDA(Reservas instancia)
        {
            StringBuilder query = new StringBuilder();
            SqlParameter[] paramsToSP = new SqlParameter[5];

            paramsToSP[0] = new SqlParameter("@id", instancia.Id);
            paramsToSP[1] = new SqlParameter("@pessoaId", instancia.PessoaId);
            paramsToSP[2] = new SqlParameter("@exemplarId", instancia.ExemplarId);
            paramsToSP[3] = new SqlParameter("@dataInicio", instancia.DataInicio);
            paramsToSP[4] = new SqlParameter("@dataFim", instancia.DataFim);

            query.Append(@" update reservas set pessoaid=@pessoaid, exemplarid=@exemplarid, datainicio=@datainicio, datafim=@datafim)");
            query.Append(@" where id=@id");

            try
            {
                return (SqlHelper.ExecuteNonQuery(
                    ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                    CommandType.Text, query.ToString(), paramsToSP) > 0);
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool ExcluirDA(Reservas instancia)
        {           
            try
            {
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.Text,
                                          string.Format("DELETE FROM reservas WHERE id = {0}", instancia.Id));

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Reservas> PesquisarDA()
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(
                ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                CommandType.Text, string.Format(@"SELECT * FROM RESERVAS "));
            
            List<Reservas> reservas = CarregarObjReservas(dr);

            dr.Close();
            return reservas;
        }

        public List<Reservas> PesquisarDA(int id)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(
                ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                CommandType.Text, string.Format(@"SELECT * FROM RESERVAS  WHERE ID = {0}", id));
            return CarregarObjReservas(dr);
        }

        public List<Reservas> PesquisarDA(string campo, string valor)
        {
            StringBuilder consulta = new StringBuilder("SELECT * FROM RESERVAS ");

            switch (campo.ToUpper())
            {
                case "CODIGO":
                    consulta.Append(string.Format("WHERE CODIGO = {0}", utils.ComparaIntComZero(valor)));
                    break;
                case "DESCRICAO":
                    consulta.Append(string.Format("WHERE DESCRICAO  LIKE '%{0}%'", valor));
                    break;
                default:
                    break;
            }

            SqlDataReader dr = SqlHelper.ExecuteReader(
                ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                CommandType.Text, consulta.ToString());

            List<Reservas> reservas = CarregarObjReservas(dr);

            dr.Close();
            return reservas;
        }

        public override List<Base> Pesquisar(string descricao)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(
                    ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                    CommandType.Text, string.Format(@"SELECT * FROM RESERVAS WHERE CODIGO = '{0}' OR DESCRICAO LIKE '%{1}%'",utils.ComparaIntComZero(descricao),  descricao));
            
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

    }
}
