﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataObjects;
using FG;

namespace FIBIESA
{
    public partial class cadBancosInstrucoes : System.Web.UI.Page
    {
        #region funcoes

        Utils utils = new Utils();
        string v_operacao = "";

        private void CarregarDDLBanco()
        {
            BancosBL banBL = new BancosBL();
            List<Bancos> bancos = banBL.PesquisarBL();

            ddlBanco.Items.Add(new ListItem("Selecione", ""));
            foreach (Bancos ltBan in bancos)
                ddlBanco.Items.Add(new ListItem(ltBan.Codigo.ToString() + " - " + ltBan.Descricao, ltBan.Id.ToString()));

            ddlBanco.SelectedIndex = 0;
        }

        private void carregarDados(int id_banIns)
        {
            BancosInstrucoesBL banInsBL = new BancosInstrucoesBL();

            List<BancosInstrucoes> banIns = banInsBL.PesquisarBL(id_banIns);

            foreach (BancosInstrucoes ltBanIns in banIns)
            {
                hfId.Value = ltBanIns.Id.ToString();
                txtCodigo.Text = ltBanIns.Codigo.ToString();
                txtDescricao.Text = ltBanIns.Descricao;
                ddlObrigDias.SelectedValue = ltBanIns.Nrdias.ToString();
                ddlBanco.SelectedValue = ltBanIns.Bancoid.ToString();
            }

        }

        private void CarregarAtributos()
        {
            txtCodigo.Attributes.Add("onkeypress", "return(Inteiros(this,event))");
        }

        private void ExibirMensagem(string mensagem)
        {
            ClientScript.RegisterStartupScript(System.Type.GetType("System.String"), "Alert",
               "<script language='javascript'> { window.alert(\"" + mensagem + "\") }</script>");
        }

        private void LimparCampos()
        {
            ddlBanco.SelectedIndex = -1;
            txtCodigo.Text = "";
            txtDescricao.Text = "";
            ddlObrigDias.SelectedValue = "false";
            hfId.Value = "";
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            int id_banIns = 0;

            CarregarAtributos();

            if (!IsPostBack)
            {
                if (Request.QueryString["operacao"] != null)
                {
                    v_operacao = Request.QueryString["operacao"];

                    if (v_operacao == "edit")
                        if (Request.QueryString["id_banIns"] != null)
                            id_banIns = Convert.ToInt32(Request.QueryString["id_banIns"].ToString());
                }

                CarregarDDLBanco();
                ddlObrigDias.SelectedValue = "false";
                if (v_operacao.ToLower() == "edit")
                    carregarDados(id_banIns);

                ddlBanco.Focus();

            }

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            BancosInstrucoesBL banBL = new BancosInstrucoesBL();
            BancosInstrucoes bancos = new BancosInstrucoes();

            bancos.Id = utils.ComparaIntComZero(hfId.Value);
            bancos.Codigo = txtCodigo.Text;
            bancos.Descricao = txtDescricao.Text;
            bancos.Nrdias = ddlObrigDias.SelectedValue == "true";
            bancos.Bancoid = utils.ComparaIntComZero(ddlBanco.SelectedValue);

            if (bancos.Id > 0)
            {
                if (banBL.EditarBL(bancos))
                    lblMensagem.Text = "Instrução atualizada com sucesso!";
                else
                    lblMensagem.Text = "Não foi possível atualizar a instrução. Revise as informações";


            }
            else
            {

                if (banBL.InserirBL(bancos))
                {
                    lblMensagem.Text = "Instrução gravada com sucesso!";
                    LimparCampos();
                }
                else
                    lblMensagem.Text = "Não foi possível gravar a instrução. Revise as informações";

            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewBancosInstrucoes.aspx");
        }

       
        
    }
}