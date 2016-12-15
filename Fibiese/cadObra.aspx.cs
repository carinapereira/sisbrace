using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataObjects;
using BusinessLayer;
using FG;
using System.Data;
using System.IO;


namespace Admin
{
    public partial class cadObra : System.Web.UI.Page
    {
        Utils utils = new Utils();
        string v_operacao = "";
        DataTable dtAutores = new DataTable();
        DataTable dtExcluidos = new DataTable();
        #region funcoes
        public DataTable dtbPesquisa
        {
            get
            {
                if (Session["_dtbPesquisa_cadObras"] != null)
                    return (DataTable)Session["_dtbPesquisa_cadObras"];
                else
                    return null;
            }
            set { Session["_dtbPesquisa_cadObras"] = value; }
        }
        private void CarregarDdlTiposObra()
        {
            TiposObrasBL tpObBL = new TiposObrasBL();
            List<TiposObras> tiposObras = tpObBL.PesquisarBL();

            ddlTipoObra.Items.Add(new ListItem("Selecione", ""));
            foreach (TiposObras lttpOb in tiposObras)
                ddlTipoObra.Items.Add(new ListItem(lttpOb.Descricao, lttpOb.Id.ToString()));

            ddlTipoObra.SelectedIndex = 0;
        }
      
        private void CarregarDados(int id_bai)
        {
            ObrasBL obraBL = new ObrasBL();
            DataSet dsOb = obraBL.PesquisarBL(id_bai);

            if (dsOb.Tables[0].Rows.Count != 0)
            {
                hfId.Value = (string)dsOb.Tables[0].Rows[0]["id"].ToString();
                lblCodigo.Text = (string)dsOb.Tables[0].Rows[0]["codigo"].ToString();
                txtTitulo.Text = (string)dsOb.Tables[0].Rows[0]["titulo"].ToString();
                txtISBN.Text = (string)dsOb.Tables[0].Rows[0]["isbn"].ToString();
                txtLocalPublic.Text = (string)dsOb.Tables[0].Rows[0]["localpublicacao"].ToString();
                txtNroEdicao.Text = (string)dsOb.Tables[0].Rows[0]["nroedicao"].ToString();
                txtNroPags.Text = (string)dsOb.Tables[0].Rows[0]["nropaginas"].ToString();
                txtVolume.Text = (string)dsOb.Tables[0].Rows[0]["volume"].ToString();
                txtDataReimpressao.Text = dsOb.Tables[0].Rows[0]["datareimpressao"].ToString() != string.Empty ? Convert.ToDateTime(dsOb.Tables[0].Rows[0]["datareimpressao"]).ToString("dd/MM/yyyy") : "";
                txtAnoPublicacao.Text = dsOb.Tables[0].Rows[0]["anopublicacao"].ToString() != string.Empty ? Convert.ToInt32(dsOb.Tables[0].Rows[0]["anopublicacao"]).ToString() : "";
                txtAssuntosAborda.Text = (string)dsOb.Tables[0].Rows[0]["assuntosaborda"].ToString();
                txtEditora.Text = (string)dsOb.Tables[0].Rows[0]["editora"].ToString();
                ddlTipoObra.SelectedValue = (string)dsOb.Tables[0].Rows[0]["tiposobraid"].ToString();
                txtCdu.Text = (string)dsOb.Tables[0].Rows[0]["cdu"].ToString();
                txtAutores.Text = dsOb.Tables[0].Rows[0]["autores"].ToString(); 

            }
          
        }
        private void CarregarAtributos()
        {
            txtNroEdicao.Attributes.Add("onkeypress", "return(Reais(this,event))");
            txtNroPags.Attributes.Add("onkeypress", "return(Reais(this,event))");
            txtVolume.Attributes.Add("onkeypress", "return(Reais(this,event))");
            txtDataReimpressao.Attributes.Add("onkeypress", "return(formatar(this,'##/##/####',event))");
        }
        private void ExibirMensagem(string mensagem)
        {
            ScriptManager.RegisterStartupScript(
                                    upnlPesquisa,
                                    this.GetType(),
                                    "Alert",
                                    "window.alert(\"" + mensagem + "\");",
                                    true);
        }
        private void LimparCampos()
        {
            lblCodigo.Text = "Código gerado automaticamente.";
            txtTitulo.Text = "";
            txtVolume.Text = "";
            txtNroPags.Text = "";
            txtNroEdicao.Text = "";
            txtLocalPublic.Text = "";
            txtISBN.Text = "";
            txtDataReimpressao.Text = "";
            txtAnoPublicacao.Text = "";
            txtAssuntosAborda.Text = "";
            txtEditora.Text = "";
            ddlTipoObra.SelectedIndex = 0;
            dtAutores = null;
            Session["dtAutores"] = dtAutores;
            tcPrincipal.ActiveTabIndex = 0;
            txtCdu.Text = "";
        }
      
        private void CriarDtItens()
        {
            DataColumn[] keys = new DataColumn[2];

            if (dtAutores.Columns.Count == 0)
            {
                DataColumn coluna1 = new DataColumn("ID", Type.GetType("System.Int32"));
                DataColumn coluna2 = new DataColumn("CODIGO", Type.GetType("System.Int32"));
                DataColumn coluna3 = new DataColumn("DESCRICAO", Type.GetType("System.String"));
                DataColumn coluna4 = new DataColumn("TIPO", Type.GetType("System.String"));
                DataColumn coluna5 = new DataColumn("AUTORESID", Type.GetType("System.Int32"));
                DataColumn coluna6 = new DataColumn("OBRAID", Type.GetType("System.Int32"));

                dtAutores.Columns.Add(coluna1);
                dtAutores.Columns.Add(coluna2);
                dtAutores.Columns.Add(coluna3);
                dtAutores.Columns.Add(coluna4);
                dtAutores.Columns.Add(coluna5);
                dtAutores.Columns.Add(coluna6);

                keys[0] = coluna5;
                keys[1] = coluna6;

                dtAutores.PrimaryKey = keys;
            }
        }
        private void CriaDtExcluidos()
        {
            if (dtExcluidos.Columns.Count == 0)
            {
                DataColumn coluna1 = new DataColumn("IDCODIGO", Type.GetType("System.String"));
                DataColumn coluna2 = new DataColumn("TIPO", Type.GetType("System.String"));

                dtExcluidos.Columns.Add(coluna1);
                dtExcluidos.Columns.Add(coluna2);
            }
        }
      
        private bool AutorJaIncluido(DataTable dtTable, string id, string id_autor, string sessao)
        {
            object[] keys = new object[2];
            keys[0] = utils.ComparaIntComZero(id_autor);
            keys[1] = utils.ComparaIntComZero(id);

            if (Session[sessao] != null)
                dtTable = (DataTable)Session[sessao];

            if (dtTable.Rows.Contains(keys))
                return true;
            else
                return false;
        }
                    
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            int id_bai = 0;

            CarregarAtributos();
            CriarDtItens();
            CriaDtExcluidos();

            if (!IsPostBack)
            {
                Session["dtAutores"] = null;
                Session["tbexcluidos"] = null;
                hfOrdem.Value = "1";

                if (Request.QueryString["operacao"] != null)
                {
                    v_operacao = Request.QueryString["operacao"];

                    if (v_operacao == "edit")
                        if (Request.QueryString["id_bai"] != null)
                            id_bai = Convert.ToInt32(Request.QueryString["id_bai"].ToString());
                }

                CarregarDdlTiposObra();

                if (v_operacao.ToLower() == "edit")
                    CarregarDados(id_bai);
                else
                    lblCodigo.Text = "Código gerado automaticamente.";

                txtTitulo.Focus();


            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewObra.aspx");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            ObrasBL obraBL = new ObrasBL();
            Obras obras = new Obras();
            obras.Id = utils.ComparaIntComZero(hfId.Value);
            obras.Codigo = utils.ComparaIntComZero(lblCodigo.Text);
            obras.Titulo = txtTitulo.Text;
            obras.NroEdicao = utils.ComparaIntComNull(txtNroEdicao.Text);
            obras.Editora = txtEditora.Text;
            obras.NroPaginas = utils.ComparaIntComNull(txtNroPags.Text);
            obras.Volume = utils.ComparaIntComNull(txtVolume.Text);
            obras.Isbn = txtISBN.Text;
            obras.AssuntosAborda = txtAssuntosAborda.Text;
            obras.AnoPublicacao = utils.ComparaIntComNull(txtAnoPublicacao.Text);
            obras.DataReimpressao = utils.ComparaDataComNull(txtDataReimpressao.Text);
            obras.TiposObraId = utils.ComparaIntComNull(ddlTipoObra.SelectedValue);
            obras.LocalPublicacao = txtLocalPublic.Text;
            obras.Cdu = txtCdu.Text;
            obras.Autores = txtAutores.Text;
            
            if (obras.Id > 0)
            {
                if (obraBL.EditarBL(obras))
                    ExibirMensagem("Obra atualizada com sucesso !");                
            }
            else
            {
                int id_obra;
                id_obra = obraBL.InserirBL(obras);
                if (id_obra > 0)
                {                    
                    ExibirMensagem("Obra gravada com sucesso !");
                    LimparCampos();
                    txtTitulo.Focus();
                }
                else
                    ExibirMensagem("Não foi possível gravar a obra. Revise as informações.");

            }
        }

        protected void dtgAutores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                utils.CarregarEfeitoGrid("#c8defc", "#ffffff", e);

            if (e.Row.RowType == DataControlRowType.DataRow)
                utils.CarregarJsExclusao("Deseja excluir este registro?", 0, e);
        }

        protected void dtgAutores_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (dtbPesquisa != null)
            {
                string ordem = e.SortExpression;

                DataView m_DataView = new DataView(dtbPesquisa);

                if (ViewState["dtbPesquisa_sort"] != null)
                {
                    if (ViewState["dtbPesquisa_sort"].ToString() == e.SortExpression)
                    {
                        m_DataView.Sort = ordem + " DESC";
                        ViewState["dtbPesquisa_sort"] = null;
                    }
                    else
                    {
                        m_DataView.Sort = ordem;
                        ViewState["dtbPesquisa_sort"] = e.SortExpression;
                    }
                }
                else
                {
                    m_DataView.Sort = ordem;
                    ViewState["dtbPesquisa_sort"] = e.SortExpression;
                }

                dtbPesquisa = m_DataView.ToTable();              
            }
        }

        protected void btnAutor_Click(object sender, EventArgs e)
        {
                  ModalPopupExtenderPesAutor.Enabled = true;
            ModalPopupExtenderPesAutor.Show();
        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            

        }
       
        protected void btnSelect_Click(object sender, EventArgs e)
        {

          
            ImageButton btndetails = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;

                     
            ModalPopupExtenderPesAutor.Enabled = false;
            ModalPopupExtenderPesAutor.Hide();
       

        }

        protected void grdPesquisaAutor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                utils.CarregarEfeitoGrid("#c8defc", "#ffffff", e);
        }

        protected void btnCanel_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderPesAutor.Enabled = false;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
                  ModalPopupExtenderPesAutor.Enabled = true;
            txtPesAutor.Text = "";
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtArquivoMarc.Text = "";
        }

        protected void btnImportar_Click(object sender, EventArgs e)
        {
            ObrasBL obraBL = new ObrasBL();
            Obras obra = obraBL.ImportarAquivoMarc(txtArquivoMarc.Text);          
            txtTitulo.Text = obra.Titulo;
            txtISBN.Text = obra.Isbn;
            txtLocalPublic.Text = obra.LocalPublicacao;
            txtNroEdicao.Text =obra.NroEdicao.ToString();
            txtNroPags.Text = obra.NroPaginas.ToString();
            txtVolume.Text = obra.Volume.ToString();
            txtDataReimpressao.Text = obra.DataReimpressao.ToString();
            txtAnoPublicacao.Text = obra.AnoPublicacao.ToString();
            txtAssuntosAborda.Text = obra.ToString();
            //ddlEditora.SelectedValue = (string)dsOb.Tables[0].Rows[0]["editoraid"].ToString();
            //ddlTipoObra.SelectedValue = (string)dsOb.Tables[0].Rows[0]["tiposobraid"].ToString();
            txtCdu.Text = obra.Cdu;
            txtArquivoMarc.Text = "";
            tcPrincipal.ActiveTabIndex = 0;
        
        }

    }
}