﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObjects;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using InfrastructureSqlServer.Helpers;
using FG;

namespace DataAccess
{
    public class PessoasDA : BaseDA
    {
        Utils utils = new Utils();

        #region funcoes
        private List<Pessoas> CarregarObjPessoa(SqlDataReader dr)
        {
            List<Pessoas> pessoas = new List<Pessoas>();

            while (dr.Read())
            {
                Pessoas pes = new Pessoas();
                pes.Id = utils.ComparaIntComZero(dr["ID"].ToString());
                pes.Codigo = utils.ComparaIntComZero(dr["CODIGO"].ToString());
                pes.Nome = dr["NOME"].ToString();
                pes.NomeFantasia = dr["NOMEFANTASIA"].ToString();
                pes.CpfCnpj = dr["CPFCNPJ"].ToString();
                pes.Rg = dr["RG"].ToString();                
                pes.DtNascimento = utils.ComparaDataComNull(dr["DTNASCIMENTO"].ToString());
                pes.EstadoCivil = dr["ESTADOCIVIL"].ToString();
                pes.Endereco = dr["ENDERECO"].ToString();
                pes.Numero = dr["NUMERO"].ToString();
                pes.BairroId = utils.ComparaIntComNull(dr["BAIRROID"].ToString());
                pes.Cep = dr["CEP"].ToString();
                pes.CidadeId = utils.ComparaIntComNull(dr["CIDADEID"].ToString());
                pes.Complemento = dr["COMPLEMENTO"].ToString();               
                pes.Email = dr["EMAIL"].ToString();
                pes.Tipo = dr["TIPO"].ToString();
                pes.Obs = dr["OBS"].ToString();
                pes.CategoriaId = Convert.ToInt32(dr["CATEGORIAID"].ToString());
                pes.EnvEmail = bool.Parse(dr["ENVEMAIL"].ToString());
                pes.Telefone = dr["TELEFONE"].ToString();
                pes.Celular = dr["CELULAR"].ToString();
                pes.DtCadastro = DateTime.Parse(dr["DTCADASTRO"].ToString());
                pes.Status = utils.ComparaIntComZero(dr["STATUS"].ToString());           
                pes.Sexo = dr["SEXO"].ToString();
                pes.TipoAssociado = dr["TIPOASSOCIADO"].ToString();
                //pes.foto = (byte[])dr["FOTO"];

                Categorias catg = new Categorias();
                catg.Id = utils.ComparaIntComZero(dr["IDCATG"].ToString());
                catg.Codigo = utils.ComparaIntComZero(dr["CODCATG"].ToString());
                catg.Descricao = dr["DESCATG"].ToString();
                pes.Categorias = catg;

                if (pes.CidadeId > 0 )
                {
                    CidadesDA cidDA = new CidadesDA();
                    Cidades cid = new Cidades();
                    cid.Id = utils.ComparaIntComNull(dr["CIDADEID"].ToString());
                    cid.Codigo = utils.ComparaIntComNull(dr["CIDCODIGO"].ToString());
                    cid.Descricao = dr["CIDDESCRICAO"].ToString();
                    cid.UF = dr["UF"].ToString();
                    pes.Cidade = cid;
                }
                                              

                BairrosDA baiDA = new BairrosDA();
                Bairros bai = new Bairros();
                bai.Id = utils.ComparaIntComZero(dr["BAIRROID"].ToString());
                bai.Codigo = utils.ComparaIntComZero(dr["CODBAIRRO"].ToString());
                bai.Descricao = dr["DESBAIRRO"].ToString();
               
                pes.Bairro = bai;
               
                               
                pessoas.Add(pes);
            }
            return pessoas;
        }
        
        private List<Pessoas> CarregarObjPessoaSimples(SqlDataReader dr)
        {
            List<Pessoas> pessoas = new List<Pessoas>();

            while (dr.Read())
            {
                Pessoas pes = new Pessoas();
                pes.Id = utils.ComparaIntComZero(dr["ID"].ToString());
                pes.Codigo = utils.ComparaIntComZero(dr["CODIGO"].ToString());
                pes.Nome = dr["NOME"].ToString();
                pes.NomeFantasia = dr["NOMEFANTASIA"].ToString();
                pes.CpfCnpj = dr["CPFCNPJ"].ToString();
                pes.Tipo = dr["TIPO"].ToString();
                pes.Obs = dr["OBS"].ToString();
                pes.CategoriaId = Convert.ToInt32(dr["CATEGORIAID"].ToString());
                pes.DtCadastro = DateTime.Parse(dr["DTCADASTRO"].ToString());
                pes.Status = utils.ComparaIntComZero(dr["STATUS"].ToString());
                
                Categorias catg = new Categorias();

                catg.Id = utils.ComparaIntComZero(dr["IDCATG"].ToString());
                catg.Codigo = utils.ComparaIntComZero(dr["CODCATG"].ToString());
                catg.Descricao = dr["DESCATG"].ToString();

                pes.Categorias = catg;
                
                pessoas.Add(pes);
            }
            return pessoas;
        }
        private Int32 RetornaMaxCodigo()
        {
            Int32 codigo = 1;
            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                          CommandType.Text, string.Format(@" SELECT ISNULL(MAX(CODIGO),0) + 1 as COD FROM PESSOAS "));

            if (ds.Tables[0].Rows.Count != 0)
                codigo = utils.ComparaIntComZero(ds.Tables[0].Rows[0]["COD"].ToString());

            return codigo;
        }
        private int LerParametro(int codigo, string modulo)
        {
            ParametrosDA parDA = new ParametrosDA();
            DataSet dsPar = parDA.PesquisarDA(codigo, modulo);
            int valor = -1;

            if (dsPar.Tables[0].Rows.Count != 0)
                valor = utils.ComparaIntComZero(dsPar.Tables[0].Rows[0]["VALOR"].ToString());

            return valor;
        }
        #endregion

        public int InserirDA(Pessoas pes)
        {
            StringBuilder query = new StringBuilder();
            SqlParameter[] paramsToSP = new SqlParameter[24];

            paramsToSP[0] = new SqlParameter("@nome", pes.Nome.ToUpper());
            paramsToSP[1] = new SqlParameter("@nomefantasia", pes.NomeFantasia);
            paramsToSP[2] = new SqlParameter("@cpfcnpj", pes.CpfCnpj);
            paramsToSP[3] = new SqlParameter("@rg", pes.Rg);
            paramsToSP[4] = new SqlParameter("@dtnascimento", pes.DtNascimento == null ? (object)DBNull.Value : pes.DtNascimento);
            paramsToSP[5] = new SqlParameter("@estadocivil", pes.EstadoCivil);
            paramsToSP[6] = new SqlParameter("@endereco", pes.Endereco.ToUpper());
            paramsToSP[7] = new SqlParameter("@numero", pes.Numero);
            paramsToSP[8] = new SqlParameter("@bairroid", pes.BairroId == null ? (object)DBNull.Value : pes.BairroId);
            paramsToSP[9] = new SqlParameter("@cep", pes.Cep);
            paramsToSP[10] = new SqlParameter("@cidadeid", pes.CidadeId == null ? (object)DBNull.Value : pes.CidadeId);
            paramsToSP[11] = new SqlParameter("@complemento", pes.Complemento);
            paramsToSP[12] = new SqlParameter("@email", pes.Email);
            paramsToSP[13] = new SqlParameter("@status", pes.Status);
            paramsToSP[14] = new SqlParameter("@tipo", pes.Tipo);
            paramsToSP[15] = new SqlParameter("@obs", pes.Obs.ToUpper());
            paramsToSP[16] = new SqlParameter("@categoriaid", pes.CategoriaId);
            paramsToSP[17] = new SqlParameter("@envemail", pes.EnvEmail);
            paramsToSP[18] = new SqlParameter("@dtcadastro", pes.DtCadastro);
            paramsToSP[19] = new SqlParameter("@telefone", pes.Telefone);
            paramsToSP[20] = new SqlParameter("@celular", pes.Celular);
            paramsToSP[21] = new SqlParameter("@codigo", RetornaMaxCodigo());
            paramsToSP[22] = new SqlParameter("@sexo", pes.Sexo);
            paramsToSP[23] = new SqlParameter("@tipoassociado", pes.TipoAssociado);
            //paramsToSP[34] = new SqlParameter("@foto", pes.foto);

            query.Append(@" insert into pessoas(Nome, nomeFantasia, cpfCnpj, RG, dtNascimento, estadoCivil, Endereco, Numero, bairroId, CEP, cidadeId");
            query.Append(@" ,Complemento, Email, status, Tipo, Obs, categoriaId, envEmail, dtCadastro, Telefone, Celular, codigo,sexo,tipoAssociado)");
            query.Append(@" values(@Nome, @nomeFantasia, @cpfCnpj, @RG, @dtNascimento, @estadoCivil, @Endereco, @Numero, @bairroId, @CEP, @cidadeId, @Complemento");
            query.Append(@" , @Email, @status, @Tipo, @Obs, @categoriaId, @envEmail, @dtCadastro, @Telefone, @Celular, @codigo, @sexo, @tipoAssociado)");
            query.Append(@" SELECT SCOPE_IDENTITY() ");
            
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

        public bool EditarDA(Pessoas pes)
        {
            StringBuilder query = new StringBuilder();
            SqlParameter[] paramsToSP = new SqlParameter[25];

            paramsToSP[0] = new SqlParameter("@id", pes.Id);
            paramsToSP[1] = new SqlParameter("@codigo", pes.Codigo);
            paramsToSP[2] = new SqlParameter("@nome", pes.Nome.ToUpper());
            paramsToSP[3] = new SqlParameter("@nomefantasia", pes.NomeFantasia);
            paramsToSP[4] = new SqlParameter("@cpfcnpj", pes.CpfCnpj);
            paramsToSP[5] = new SqlParameter("@rg", pes.Rg);           
            paramsToSP[6] = new SqlParameter("@dtnascimento", pes.DtNascimento == null ? (object)DBNull.Value : pes.DtNascimento);
            paramsToSP[7] = new SqlParameter("@estadocivil", pes.EstadoCivil);
            paramsToSP[8] = new SqlParameter("@endereco", pes.Endereco.ToUpper());
            paramsToSP[9] = new SqlParameter("@numero", pes.Numero);
            paramsToSP[10] = new SqlParameter("@bairroid", pes.BairroId  == null ? (object)DBNull.Value : pes.BairroId);
            paramsToSP[11] = new SqlParameter("@cep", pes.Cep);
            paramsToSP[12] = new SqlParameter("@cidadeid", pes.CidadeId == null ? (object)DBNull.Value : pes.CidadeId);
            paramsToSP[13] = new SqlParameter("@complemento", pes.Complemento);           
            paramsToSP[14] = new SqlParameter("@email", pes.Email);
            paramsToSP[15] = new SqlParameter("@obs", pes.Obs.ToUpper());
            paramsToSP[16] = new SqlParameter("@categoriaid", pes.CategoriaId);
            paramsToSP[17] = new SqlParameter("@tipo", pes.Tipo);
            paramsToSP[18] = new SqlParameter("@envemail", pes.EnvEmail);
            paramsToSP[19] = new SqlParameter("@celular", pes.Celular);
            paramsToSP[20] = new SqlParameter("@telefone", pes.Telefone);
            paramsToSP[21] = new SqlParameter("@dtcadastro", pes.DtCadastro);
            paramsToSP[22] = new SqlParameter("@status", pes.Status);          
            paramsToSP[23] = new SqlParameter("@sexo", pes.Sexo);
            paramsToSP[24] = new SqlParameter("@tipoassociado", pes.TipoAssociado);
            //paramsToSP[35] = new SqlParameter("@foto", pes.foto);

            query.Append(@" update pessoas set Nome=@Nome, nomeFantasia=@nomeFantasia, cpfCnpj=@cpfCnpj, RG=@RG, dtNascimento=@dtNascimento ");
            query.Append(@" ,estadoCivil=@estadoCivil, Endereco=@Endereco, Numero=@Numero, bairroId=@bairroId, CEP=@CEP, cidadeId=@cidadeId ");
            query.Append(@" ,Complemento=@Complemento, Email=@Email, status=@status, Tipo=@Tipo, Obs=@Obs, categoriaId=@categoriaId, envEmail=@envEmail ");
            query.Append(@" ,dtCadastro=@dtCadastro, Telefone=@Telefone, Celular=@Celular, codigo=@codigo, sexo=@sexo ");
            query.Append(@"	,tipoAssociado=@tipoAssociado ");
            query.Append(@" where id=@id ");

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

        public bool ExcluirDA(Pessoas pes)
        {   
            try
            {
                SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["conexao"].ToString(), CommandType.Text,
                                          string.Format("DELETE FROM pessoas WHERE id = {0}", pes.Id));    

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Pessoas> PesquisarDA()
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append(@"SELECT P.*, C.ID IDCATG, C.CODIGO CODCATG, C.DESCRICAO DESCATG ");
            consulta.Append(@",CID.CODIGO CIDCODIGO, CID.DESCRICAO CIDDESCRICAO ");
            consulta.Append(@",CID.UF, B.CODIGO CODBAIRRO, B.DESCRICAO DESBAIRRO ");
            consulta.Append(@"FROM PESSOAS P ");
            consulta.Append(@"INNER JOIN CATEGORIAS C ON P.CATEGORIAID = C.ID  ");
            consulta.Append(@"LEFT JOIN CIDADES CID ON P.CIDADEID = CID.ID ");
            consulta.Append(@"LEFT JOIN BAIRROS B ON P.BAIRROID = B.ID ");
                      
                        
            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());
            List<Pessoas> pessoas = CarregarObjPessoa(dr);

            dr.Close();
            return pessoas;
        }

        public List<Pessoas> PesquisarDA(int id_pes)
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append(@"SELECT P.*, C.ID IDCATG, C.CODIGO CODCATG, C.DESCRICAO DESCATG ");
            consulta.Append(@",CID.CODIGO CIDCODIGO, CID.DESCRICAO CIDDESCRICAO ");
            consulta.Append(@",CID.UF, B.CODIGO CODBAIRRO, B.DESCRICAO DESBAIRRO ");
            consulta.Append(@"FROM PESSOAS P ");  
            consulta.Append(@"INNER JOIN CATEGORIAS C ON P.CATEGORIAID = C.ID  ");
            consulta.Append(@"LEFT JOIN CIDADES CID ON P.CIDADEID = CID.ID ");
            consulta.Append(@"LEFT JOIN BAIRROS B ON P.BAIRROID = B.ID ");
            consulta.Append(@"WHERE P.ID = {0}");
            
            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, string.Format(consulta.ToString(), id_pes));

          
            
            List<Pessoas> pessoas = CarregarObjPessoa(dr);

            dr.Close();
            return pessoas;
        }

        public DataSet PesquisaDataSetDA(int pesssoaid)
        {
            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, string.Format(@"SELECT P.*, C.ID IDCATG, C.CODIGO CODCATG, C.DESCRICAO DESCATG  " +
                                                                                                 " FROM PESSOAS P " +
                                                                                                 "     ,CATEGORIAS C " +
                                                                                                 " WHERE P.CATEGORIAID = C.ID " +
                                                                                                 " AND P.ID = {0}", pesssoaid));


            return ds;
        }

        public DataSet PesquisaDA(int id_pes)
        {
            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                              CommandType.Text, string.Format(@"SELECT * FROM PESSOAS WHERE ID = {0}", id_pes));

            return ds;
        }

        public List<Pessoas> PesquisarPorGeneroDA(int id_cat)
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append(@"SELECT P.*, C.ID IDCATG, C.CODIGO CODCATG, C.DESCRICAO DESCATG ");
            consulta.Append(@",CID.CODIGO CIDCODIGO, CID.DESCRICAO CIDDESCRICAO ");
            consulta.Append(@",CID.UF, B.CODIGO CODBAIRRO, B.DESCRICAO DESBAIRRO ");
            consulta.Append(@"FROM PESSOAS P ");
            consulta.Append(@"INNER JOIN CATEGORIAS C ON P.CATEGORIAID = C.ID  ");
            consulta.Append(@"LEFT JOIN CIDADES CID ON P.CIDADEID = CID.ID ");
            consulta.Append(@"LEFT JOIN BAIRROS B ON P.BAIRROID = B.ID ");
            consulta.Append(@"WHERE P.CATEGORIAID = {0} ");

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, string.Format(consulta.ToString() , id_cat));
            List<Pessoas> pessoas = CarregarObjPessoa(dr);

            dr.Close();
            return pessoas;
        }

        public List<Pessoas> PesquisarDA(string campo, string valor)
        {
            StringBuilder consulta = new StringBuilder();          
            consulta.Append(@"SELECT P.*, C.ID IDCATG, C.CODIGO CODCATG, C.DESCRICAO DESCATG ");
            consulta.Append(@",CID.CODIGO CIDCODIGO, CID.DESCRICAO CIDDESCRICAO ");
            consulta.Append(@",CID.UF, B.CODIGO CODBAIRRO, B.DESCRICAO DESBAIRRO ");
            consulta.Append(@"FROM PESSOAS P ");
            consulta.Append(@"INNER JOIN CATEGORIAS C ON P.CATEGORIAID = C.ID  ");
            consulta.Append(@"LEFT JOIN CIDADES CID ON P.CIDADEID = CID.ID ");
            consulta.Append(@"LEFT JOIN BAIRROS B ON P.BAIRROID = B.ID ");
            consulta.Append(@"WHERE P.CATEGORIAID = {0} ");

            switch (campo.ToUpper())
            {
                case "CODIGO":
                    consulta.Append(string.Format(" AND P.CODIGO = {0}", utils.ComparaIntComZero(valor)));
                    break;
                case "NOME":
                    consulta.Append(string.Format(" AND P.NOME  LIKE '%{0}%'", valor));
                    break;
                case "NOMECODIGO":
                    consulta.Append(string.Format(" AND P.NOME  LIKE '%{0}%' OR CODIGO = {1}", valor, utils.ComparaIntComZero(valor)));
                    break;
                default:
                    break;
            }

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());

            List<Pessoas> pessoas = CarregarObjPessoa(dr);

            dr.Close();
            return pessoas;
        }

        public List<Pessoas> PesquisarBuscaDA(string valor)
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append(@"SELECT P.*, C.ID IDCATG, C.CODIGO CODCATG, C.DESCRICAO DESCATG ");
            consulta.Append(@",CID.CODIGO CIDCODIGO, CID.DESCRICAO CIDDESCRICAO ");
            consulta.Append(@",CID.UF, B.CODIGO CODBAIRRO, B.DESCRICAO DESBAIRRO ");
            consulta.Append(@"FROM PESSOAS P ");
            consulta.Append(@"INNER JOIN CATEGORIAS C ON P.CATEGORIAID = C.ID  ");
            consulta.Append(@"LEFT JOIN CIDADES CID ON P.CIDADEID = CID.ID ");
            consulta.Append(@"LEFT JOIN BAIRROS B ON P.BAIRROID = B.ID ");
              
            if (valor != "" && valor != null)
                consulta.Append(string.Format(" WHERE (P.CODIGO = {0} OR  UPPER(P.NOME)  LIKE '%{1}%')", utils.ComparaIntComZero(valor), valor.ToUpper()));

            consulta.Append(" ORDER BY P.CODIGO ");

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());

            List<Pessoas> pessoas = CarregarObjPessoa(dr);

            dr.Close();
            return pessoas;
        }
        
        public List<Pessoas> PesquisarBuscaSimplesDA(string valor)
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append(@"SELECT P.*, C.ID IDCATG, C.CODIGO CODCATG, C.DESCRICAO DESCATG ");
            consulta.Append(@",CID.CODIGO CIDCODIGO, CID.DESCRICAO CIDDESCRICAO, CID.UF  ");
            consulta.Append(@",B.CODIGO CODBAIRRO, B.DESCRICAO DESBAIRRO ");
            consulta.Append(@"FROM PESSOAS P ");
            consulta.Append(@"INNER JOIN CATEGORIAS C ON P.CATEGORIAID = C.ID  ");
            consulta.Append(@"LEFT JOIN CIDADES CID ON P.CIDADEID = CID.ID ");
            consulta.Append(@"LEFT JOIN BAIRROS B ON P.BAIRROID = B.ID ");
                        
            if (valor != "" && valor != null)
                consulta.Append(string.Format(" WHERE (P.CODIGO = {0} OR  UPPER(P.NOME) LIKE '%{1}%')", utils.ComparaIntComZero(valor), valor.ToUpper()));

            consulta.Append(" ORDER BY P.CODIGO ");

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());

            List<Pessoas> pessoas = CarregarObjPessoaSimples(dr);
            
            dr.Close();
            return pessoas;
        }

        public List<Pessoas> PesquisarParticTurmaDA(string valor, int tur_id)
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append(@"SELECT P.*, C.ID IDCATG, C.CODIGO CODCATG, C.DESCRICAO DESCATG ");
            consulta.Append(@",CID.CODIGO CIDCODIGO, CID.DESCRICAO CIDDESCRICAO ");
            consulta.Append(@",CID.UF , B.CODIGO CODBAIRRO, B.DESCRICAO DESBAIRRO ");
            consulta.Append(@"FROM PESSOAS P ");
            consulta.Append(@"INNER JOIN CATEGORIAS C ON P.CATEGORIAID = C.ID  ");
            consulta.Append(@"LEFT JOIN CIDADES CID ON P.CIDADEID = CID.ID ");
            consulta.Append(@"LEFT JOIN BAIRROS B ON P.BAIRROID = B.ID ");          
            consulta.Append(@"WHERE 1 = 1 ");

            if(tur_id > 0)
                consulta.Append(string.Format(@"  AND NOT EXISTS(SELECT 1 FROM TURMASPARTICIPANTES T WHERE T.PESSOAID = P.ID AND T.TURMASID = {0})",tur_id));

            if (valor != "" && valor != null)
                consulta.Append(string.Format(" AND (P.CODIGO = {0} OR  P.NOME  LIKE '%{1}%')", utils.ComparaIntComZero(valor), valor));

            consulta.Append(" ORDER BY P.CODIGO ");

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());

            List<Pessoas> pessoas = CarregarObjPessoa(dr);
            
            dr.Close();
            return pessoas;
        }
        
        public override List<Base> Pesquisar(string descricao)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                      CommandType.Text, string.Format(@"SELECT * " +
                                                                                       " FROM PESSOAS WHERE CODIGO = '{0}' OR NOME LIKE '%{1}%'", utils.ComparaIntComZero(descricao), descricao));

            List<Base> ba = new List<Base>();

            while (dr.Read())
            {
                Base bas = new Base();
                bas.PesId1 = int.Parse(dr["ID"].ToString());
                bas.PesCodigo = dr["CODIGO"].ToString();
                bas.PesDescricao = dr["NOME"].ToString();

                ba.Add(bas);
            }

            dr.Close();
            return ba;
        }
        
        public List<Base> PesquisarPessoas(string valor)
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append(string.Format(@"SELECT *  FROM PESSOAS WHERE CODIGO IN ({0}) ", valor));

            consulta.Append(" ORDER BY CODIGO ");

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());

            List<Base> ba = new List<Base>();

            while (dr.Read())
            {
                Base bas = new Base();
                bas.PesId1 = int.Parse(dr["ID"].ToString());
                bas.PesCodigo = dr["CODIGO"].ToString();
                bas.PesDescricao = dr["NOME"].ToString();

                ba.Add(bas);
            }

            dr.Close();
            return ba;
        }

        public string VerificaSituacaoPessoa(int id_pes, bool financeiro, bool biblioteca)
        {
            StringBuilder v_erro = new StringBuilder();
            StringBuilder v_query = new StringBuilder();

            //VERIFICA SE EXISTE ALGUMA PENDENCIA FINANCEIRA
            if (financeiro)
            {
                v_query.Append(@"SELECT COUNT(1) QTDE ");
                v_query.Append(@"  FROM TITULOS T ");
                v_query.Append(@" WHERE T.PESSOAID = " + id_pes.ToString());
                v_query.Append(@"  AND CONVERT(DATE,T.DTVENCIMENTO,103) < CONVERT(DATE,GETDATE(),103)");
                v_query.Append(@"  AND (T.DTPAGAMENTO IS NULL  OR T.VALORPAGO < VALOR ) ");

                DataSet dsTit = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                    CommandType.Text, v_query.ToString());
                int v_qtd;

                if (dsTit.Tables[0].Rows.Count != 0)
                {
                    v_qtd = (Int32)dsTit.Tables[0].Rows[0]["QTDE"];

                    if (v_qtd > 0)
                        v_erro.Append("Cliente com pendencia financeira.");
                }
            }

            //VERIFICA SE EXISTE ALGUMA PENDENCIA NA BIBLIOTECA
            v_query.Clear();
            if (biblioteca)
            {
                v_query.Append(@"SELECT COUNT(1) QTDE ");
                v_query.Append(@"  FROM EMPRESTIMOS EMP ");
                v_query.Append(@"     , EMPRESTIMOMOV EMPMOV ");
                v_query.Append(@" WHERE EMP.ID = EMPMOV.EMPRESTIMOID ");
                v_query.Append(@"   AND EMP.PESSOAID = " + id_pes.ToString());
                v_query.Append(@"   AND (CONVERT(DATETIME,EMPMOV.DATAPREVISTAEMPRESTIMO,103) < CONVERT(DATETIME,GETDATE(),103)) ");
                v_query.Append(@"   AND EMPMOV.DATADEVOLUCAO IS NULL ");

                DataSet dsEmp = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                    CommandType.Text, v_query.ToString());
                int v_qtd;

                if (dsEmp.Tables[0].Rows.Count != 0)
                {
                    v_qtd = (Int32)dsEmp.Tables[0].Rows[0]["QTDE"];

                    if (v_qtd > 0)
                        v_erro.Append(" Cliente com pendencia na biblioteca.");
                }
            }

                       
            return v_erro.ToString();
        }

        public bool CPJCNPJJaCadastrado(Pessoas pes)
        {
            StringBuilder consulta = new StringBuilder();
            bool existe = false;
            
            consulta.Append(@"SELECT *  ");
            consulta.Append(@"  FROM PESSOAS P ");
            consulta.Append(string.Format(" WHERE CPFCNPJ = '{0}'", pes.CpfCnpj));
            if(pes.Id > 0)
                consulta.Append(string.Format(" AND ID != {0}", pes.Id));

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());
            existe = dr.HasRows;
            dr.Close();
            return existe;
        }

        /// <summary>
        /// Conta quantos titulos em aberto a pessoa tem. Retorna -1 em caso de erro.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int EstaDevendo(int id)
        {
            StringBuilder consulta = new StringBuilder();
            consulta.Append(" SELECT COUNT(*) AS QTD FROM TITULOS ");
            consulta.Append(" WHERE (DTPAGAMENTO > DTVENCIMENTO OR VALORPAGO < VALOR  OR VALORPAGO IS NULL OR DTPAGAMENTO IS NULL)");
            consulta.Append(" AND PESSOAID = " + id.ToString());

            SqlDataReader dr = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["conexao"].ToString(),
                                                                CommandType.Text, consulta.ToString());
            int qtd = -1;
            while (dr.Read())
            {
                qtd = utils.ComparaIntComZero(dr["QTD"].ToString());
            }

            dr.Close();
            return qtd;
        }
    }
}
