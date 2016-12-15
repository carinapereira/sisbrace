using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataObjects;
using FG;
using System.Data;
namespace FIBIESA
{
    public partial class viewRelFrequencia : System.Web.UI.Page
    {
        Utils utils = new Utils();
        #region Metodos
        public void CarregarDdlEvento()
        {
            EventosBL eveBL = new EventosBL();
            List<Eventos> lEventos = eveBL.PesquisarBL();

            foreach (Eventos eventos in lEventos)
                ddlEvento.Items.Add(new ListItem(eventos.Codigo + " - " + eventos.Descricao, eventos.Id.ToString()));

            ddlEvento.Items.Insert(0, "Selecione");
            ddlEvento.SelectedIndex = 0;
        }

        public void CarregarDdlTurma()
        {
            TurmasBL turBL = new TurmasBL();
            List<Turmas> lTurmas = turBL.PesquisarEveBL(Convert.ToInt16(ddlEvento.SelectedValue.ToString()));

            foreach (Turmas turmas in lTurmas)
                ddlTurma.Items.Add(new ListItem(turmas.Codigo + " - " + turmas.Descricao, turmas.Id.ToString()));

            ddlTurma.Items.Insert(0, "Selecione");
            ddlTurma.SelectedIndex = 0;
        }
                
        private void CarregarDdlParticipante()
        {
            TurmasParticipantesBL turParBL = new TurmasParticipantesBL();
            List<TurmasParticipantes> lTurmasParticipantes = turParBL.PesquisarBL(Convert.ToInt16(ddlTurma.SelectedValue.ToString()));

            foreach (TurmasParticipantes turmasParticipantes in lTurmasParticipantes)
                ddlParticipante.Items.Add(new ListItem(turmasParticipantes.Pessoa.Codigo + " - " + turmasParticipantes.Pessoa.Nome, turmasParticipantes.Pessoa.Id.ToString()));
            ddlParticipante.Items.Insert(0, new ListItem("Todos",""));
            ddlParticipante.SelectedIndex = 0;
        }
        
        private void ImprimirRelatorioTurmas()
        {
            TurmasBL turmasBL = new TurmasBL();

            Session["ldsRel"] = turmasBL.PesquisarDataset(ddlEvento.SelectedValue.ToString() == "Selecione" ? "" : ddlEvento.SelectedValue.ToString(), ddlTurma.SelectedValue.ToString() == "Selecione" ? "" : ddlTurma.SelectedValue.ToString(), txtDataInicio.Text, txtDataFim.Text).Tables[0];
            if (((DataTable)Session["ldsRel"]).Rows.Count != 0)
            {                                                                                                                                                                                                                                                                                                                                                                                                                                           //l//c 
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "WinOpen('/Relatorios/RelTurmas.aspx?Eventos=" + ddlEvento.SelectedValue.ToString() + "','',600,915);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Sua pesquisa não retornou dados.');", true);
            }
        }

        private void ImprimirRelatorioEventos()
        {
            EventosBL eventosBL = new EventosBL();
            string eventos = ddlEvento.SelectedValue.ToString() == "Selecione" ? "" : ddlEvento.SelectedValue.ToString(); 

            Session["ldsRel"] = eventosBL.PesquisarDataset(eventos, txtDataInicio.Text,txtDataFim.Text).Tables[0];
            if (((DataTable)Session["ldsRel"]).Rows.Count != 0)
            {                                                                                                                                                                                                                                                                                                                                                                                                                                           //l//c 
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "WinOpen('/Relatorios/RelCursos.aspx?Eventos=" + eventos + "','',600,825);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Sua pesquisa não retornou dados.');", true);
            }
        }

        private void ImprimirRelatorioFrequencia()
        {
            ChamadasBL chamadasBL = new ChamadasBL();
       
            int preenchido = 0;
            if (rbSemPreenchimento.Checked)
                preenchido = 0;
            else if (rbComPreenchimento.Checked)
                preenchido = 1;
   
            TurmasBL turBl = new TurmasBL();
            List<Turmas> lTurmas = turBl.PesquisarBL("ID", ddlTurma.SelectedValue.ToString());

            Session["ldsRel"] = chamadasBL.PesquisarDataSet(txtDataFim.Text, Convert.ToInt16(ddlTurma.SelectedValue), ddlParticipante.SelectedValue, preenchido, lTurmas[0].DiaSemana).Tables[0];
            if (((DataTable)Session["ldsRel"]).Rows.Count != 0)
            {                                                                                                                                                                                                                                                                                                                                                                                                                                           //l//c 
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "WinOpen('/Relatorios/RelFrequencia.aspx?mes=" + txtDataFim.Text + "','',600,1005);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Sua pesquisa não retornou dados.');", true);
            }
        }


        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarDdlEvento();           
                rbSemPreenchimento.Checked = true;
            }

        }

        protected void ddlEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTurma.Items.Clear();
            ddlParticipante.Items.Clear();      
            if (ddlEvento.SelectedIndex != 0)
                CarregarDdlTurma();
        }

        protected void ddlTurma_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlParticipante.Items.Clear();                    
            if (ddlTurma.SelectedIndex != 0)                          
               CarregarDdlParticipante();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
        }

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            if (rbEventos.Checked)
                ImprimirRelatorioEventos();
            else if (rbTurmas.Checked)
                ImprimirRelatorioTurmas();
            else if (rbFrequencias.Checked)
                ImprimirRelatorioFrequencia();           
            
        }
    }
}