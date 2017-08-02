using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

using Microsoft.AspNet.Membership.OpenAuth;

namespace Chata_IS.Account
{
    public partial class Manage : System.Web.UI.Page
    {

        private void del_Click(object sender, EventArgs e)
        {
            string id = ((Button)(sender)).ID.Split('_')[1];
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", id));

            if( SQLBase.Instance.Delete("delete from Vypis_brigad where id_vypisu = @id", parameters))
            Response.Redirect("Manage.aspx");
        }

        private void addCom_Click(object sender, EventArgs e)
        {
            string id = ((Button)(sender)).ID.Split('_')[1];
            TextBox tox = (TextBox)(mainDiv.FindControl("jmBox" + id));
            CKEditor.NET.CKEditorControl comCon = (CKEditor.NET.CKEditorControl)(mainDiv.FindControl("tControll" + id));
            DateTime nDate = DateTime.Now;

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id_vypisu", id));
            parameters.Add(new MySqlParameter("@jmeno", tox.Text));
            parameters.Add(new MySqlParameter("@text", comCon.Text));
            //parameters.Add(new MySqlParameter("@datum", nDate.ToString("yyyy-MM-dd H:mm:ss")));

            if (SQLBase.Instance.Insert("insert into Komentar_brigada (`id_vypisu`, `jmeno`, `text`, `datum`)" +
                " values (@id_vypisu,@jmeno,@text,NOW())", parameters))
            Response.Redirect("Manage.aspx");
        }

        protected string SuccessMessage
        {
            get;
            private set;
        }
        object[] toolbar;
        protected bool CanRemoveExternalLogins
        {
            get;
            private set;
        }

        protected void Page_Load()
        {

            toolbar = new object[]
			{
				new object[] { "Bold", "Italic", "Underline", "Strike", "Smiley"}
			};


            if (Session["UserAuthentication"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if(!SQLBase.Instance.isAdminLoggedIn())
            {
                addNews.Visible = false;
                
            }


            List<MySqlParameter> parameters = new List<MySqlParameter>();
            List<string>[] queryResult = SQLBase.Instance.Select(
                "SELECT id_vypisu,text_vypisu,datum FROM Vypis_brigad ORDER BY datum ASC", 3, parameters);
            if (!SQLBase.Instance.sqlStatementCompleted)
            {
          //      System.Diagnostics.Debug.WriteLine("SQL select failed.");
                return;
            }

         //   System.Diagnostics.Debug.WriteLine("SQL select results: " );
            // Prochazeni vysledku selectu
            int n1 = SQLBase.Instance.numberOfSelectedRows;
            for (int i = 0; i < n1; i++)
            {
                DateTime date;
                int id_vyp = Int32.Parse(queryResult[0][i]);
                string html = queryResult[1][i];
                DateTime.TryParse(queryResult[2][i], out date);


                System.Web.UI.HtmlControls.HtmlGenericControl NewDiv =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                NewDiv.ID = "bigDiv" + i;
                System.Web.UI.HtmlControls.HtmlGenericControl NewDiv2 =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                NewDiv2.ID = "vypDiv" + i;
                System.Web.UI.HtmlControls.HtmlGenericControl NewDiv3 =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");

                System.Web.UI.HtmlControls.HtmlGenericControl TopDiv =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                TopDiv.ID = "topDiv" + i;

                TopDiv.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
                TopDiv.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                TopDiv.Style.Add(HtmlTextWriterStyle.BorderColor, "#545454");
                TopDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#e27414");
                TopDiv.Style.Add(HtmlTextWriterStyle.Color, "white");
                TopDiv.Style.Add(HtmlTextWriterStyle.Width, "100%");
                TopDiv.Style.Add(HtmlTextWriterStyle.PaddingTop, "0px");
                TopDiv.Style.Add(HtmlTextWriterStyle.PaddingBottom, "0px");
                TopDiv.Style.Add(HtmlTextWriterStyle.PaddingLeft, "0px");
                TopDiv.Style.Add(HtmlTextWriterStyle.PaddingRight, "0px");
                TopDiv.Style.Add(HtmlTextWriterStyle.MarginBottom, "0px");
                TopDiv.Style.Add(HtmlTextWriterStyle.MarginTop, "0px");
                TopDiv.Style.Add(HtmlTextWriterStyle.MarginRight, "0px");
                TopDiv.Style.Add(HtmlTextWriterStyle.MarginLeft, "0px");
                TopDiv.Style.Add(HtmlTextWriterStyle.Display, "inline-block");

                Label tLabel = new Label();
                tLabel.Text = "<hgroup>  Popis brigády <hgroup/>";
                tLabel.Style.Add(HtmlTextWriterStyle.Color, "white");
                TopDiv.Controls.Add(tLabel);


                Label nLabel = new Label();
                NewDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#efefef");
                NewDiv.Style.Add(HtmlTextWriterStyle.Width, "99%");
                NewDiv.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
                NewDiv.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                NewDiv.Style.Add(HtmlTextWriterStyle.BorderColor, "#545454");
                NewDiv.Style.Add(HtmlTextWriterStyle.PaddingLeft, "0px");
                NewDiv.Style.Add(HtmlTextWriterStyle.PaddingRight, "0px");
                NewDiv.Style.Add(HtmlTextWriterStyle.MarginBottom, "60px");
                NewDiv.Style.Add(HtmlTextWriterStyle.TextAlign, "center");

                NewDiv2.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                NewDiv2.Style.Add(HtmlTextWriterStyle.Width, "99%");
                NewDiv2.Style.Add(HtmlTextWriterStyle.PaddingTop, "0px");
                NewDiv2.Style.Add(HtmlTextWriterStyle.PaddingBottom, "0px");
                NewDiv2.Style.Add(HtmlTextWriterStyle.PaddingLeft, "1px");
                NewDiv2.Style.Add(HtmlTextWriterStyle.PaddingRight, "1px");
                NewDiv2.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
                NewDiv2.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                NewDiv2.Style.Add(HtmlTextWriterStyle.BorderColor, "#545454");
                NewDiv2.Style.Add(HtmlTextWriterStyle.MarginRight, "0px");
                NewDiv2.Style.Add(HtmlTextWriterStyle.MarginLeft, "0px");
                NewDiv2.Style.Add(HtmlTextWriterStyle.Display, "inline-block");

                NewDiv3.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#ededed");
                NewDiv3.Style.Add(HtmlTextWriterStyle.Width, "99%");
                NewDiv3.Style.Add(HtmlTextWriterStyle.PaddingTop, "4px");
                NewDiv3.Style.Add(HtmlTextWriterStyle.PaddingBottom, "4px");
                NewDiv3.Style.Add(HtmlTextWriterStyle.PaddingLeft, "8px");
                NewDiv3.Style.Add(HtmlTextWriterStyle.PaddingRight, "8px");
                NewDiv3.Style.Add(HtmlTextWriterStyle.MarginRight, "0px");
                NewDiv3.Style.Add(HtmlTextWriterStyle.MarginLeft, "0px");
                NewDiv3.Style.Add(HtmlTextWriterStyle.Display, "inline-block");

                System.Web.UI.HtmlControls.HtmlGenericControl NewDivDat =
                   new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                NewDivDat.ID = "divdatum" + i;
                NewDivDat.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                NewDivDat.Style.Add(HtmlTextWriterStyle.BorderColor, "#545454");
                NewDivDat.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                NewDivDat.Style.Add(HtmlTextWriterStyle.Width, "99%");
                NewDivDat.Style.Add(HtmlTextWriterStyle.PaddingTop, "0px");
                NewDivDat.Style.Add(HtmlTextWriterStyle.PaddingBottom, "0px");
                NewDivDat.Style.Add(HtmlTextWriterStyle.PaddingLeft, "0px");
                NewDivDat.Style.Add(HtmlTextWriterStyle.PaddingRight, "0px");
                NewDivDat.Style.Add(HtmlTextWriterStyle.MarginBottom, "0px");
                NewDivDat.Style.Add(HtmlTextWriterStyle.MarginTop, "0px");
                NewDivDat.Style.Add(HtmlTextWriterStyle.MarginRight, "0px");
                NewDivDat.Style.Add(HtmlTextWriterStyle.MarginLeft, "0px");
                NewDivDat.Style.Add(HtmlTextWriterStyle.Display, "inline-block");


                nLabel.Text = html + "<br/>";

                Label datLabel = new Label();
                datLabel.Text =  "    Napsal admin dne: "+date.ToString("dd/MM/yyyy")+"   ";

                Button delete = new Button();
                delete.ID = "del_" + id_vyp;
                delete.Text = "Smazat";
                delete.OnClientClick = "return confirm('Chcete smazat výpis id:" + id_vyp + " ?')";
                delete.Click += del_Click;
                if (!SQLBase.Instance.isAdminLoggedIn())
                    delete.Visible = false;

                NewDivDat.Controls.Add(delete);
                NewDivDat.Controls.Add(datLabel);

                NewDiv2.Controls.Add(TopDiv);
                NewDiv2.Controls.Add(nLabel);
                NewDiv2.Controls.Add(NewDivDat);


                NewDiv.Controls.Add(NewDiv2);


                Label brLabel = new Label();
                brLabel.Text = "<br><hgroup><h3>  Komentáře:</h3></hgroup><br>";

                

                mainDiv.Controls.Add(NewDiv);

                List<MySqlParameter> parameters2 = new List<MySqlParameter>();
                parameters2.Add(new MySqlParameter("@id_vypisu", id_vyp));
                List<string>[] queryResult2 = SQLBase.Instance.Select(
                    "SELECT id_komentare,jmeno,text,datum FROM Komentar_brigada WHERE id_vypisu = @id_vypisu ORDER BY datum DESC", 4, parameters2,3);
                if (!SQLBase.Instance.sqlStatementCompleted || !SQLBase.Instance.foundSomeResult)
                {
                    //      System.Diagnostics.Debug.WriteLine("SQL select failed.");
                    brLabel.Text = brLabel.Text + "<div style=\"width:100%; background-color:f4e7cd; text-align:center;\"><h2>Ještě tu nikdo nenapsal komentář</h2></div>";
                    
                }

                int n2 = SQLBase.Instance.numberOfSelectedRows;
                for (int j = 0; j < n2; j++)
                {
                    DateTime date2 = SQLBase.Instance.parsDate;
                    int id_kom = Int32.Parse(queryResult2[0][j]);
                    string jmeno = queryResult2[1][j];
                    string kom = queryResult2[2][j];
                    
                   
                    string sAd = "";
                    if(j>0) sAd = "<hr><br/>";
                    brLabel.Text = brLabel.Text + sAd+"<div style=\"width:100%;background-color:white; text-align:left;\"><table style=\"width:100%;background-color:f4e7cd; text-align:left;\"><tr><td style=\"vertical-align: middle; width:15%;\">  <b>" + jmeno + "</b> napsal : <td style=\"width:60%;\">" + kom + "</td><td style=\"width:35%;\"> Dne " + date.ToString("dd.MM.yyyy") +"</td></tr></table> </div><br/>";
                }

                System.Web.UI.HtmlControls.HtmlGenericControl NewDivCom =
                   new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                NewDivCom.ID = "divdcom" + i;
                NewDivCom.Style.Add(HtmlTextWriterStyle.Width, "96%");
                NewDivCom.Style.Add(HtmlTextWriterStyle.PaddingTop, "0px");
                NewDivCom.Style.Add(HtmlTextWriterStyle.PaddingBottom, "0px");
                NewDivCom.Style.Add(HtmlTextWriterStyle.PaddingLeft, "16px");
                NewDivCom.Style.Add(HtmlTextWriterStyle.PaddingRight, "16px");
                NewDivCom.Style.Add(HtmlTextWriterStyle.MarginBottom, "0px");
                NewDivCom.Style.Add(HtmlTextWriterStyle.MarginTop, "0px");
                NewDivCom.Style.Add(HtmlTextWriterStyle.MarginRight, "0px");
                NewDivCom.Style.Add(HtmlTextWriterStyle.MarginLeft, "0px");
                NewDivCom.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#efefef");
                NewDivCom.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
                NewDivCom.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                NewDivCom.Style.Add(HtmlTextWriterStyle.Position, "relative");
                NewDivCom.Controls.Add(brLabel);
                NewDivCom.Controls.Add(NewDiv3);

                Label comLab = new Label();
                comLab.Text = "<hr><h3>  Vložit komentář: </h3><table style=\"margin-left:20px; vertical-align:top; min-width:500px; width:500px;\"><tr><td style=\"text-align:left; width:20%;\">Jméno : </td><td style=\"text-align:left;\">";

                TextBox jmBox = new TextBox();
                jmBox.ID = "jmBox" + id_vyp;

                CKEditor.NET.CKEditorControl tControl = new CKEditor.NET.CKEditorControl();
                tControl.ResizeEnabled = false;
                tControl.Height = 120;
                tControl.Width = 600;
                tControl.ToolbarCanCollapse = false;
                
                tControl.ID = "tControll" + id_vyp;
                tControl.config.toolbar = toolbar;


                Label comLab2 = new Label();
                comLab2.Text = "</td></tr><tr><td style=\"text-align:left; width:20%; \">Komentář : </td><td style=\"text-align:left;\">";

                Label comLab3 = new Label();
                comLab3.Text = "</td></tr><tr><td style=\"text-align:left; width:20%; \"></td><td style=\"text-align:right;\">";

                Button addCom = new Button();
                addCom.ID = "addCom_" + id_vyp;
                addCom.Text = "Přidat komentář";
                addCom.OnClientClick = "return confirm('Chcete vložit komentář?)";
                addCom.Click += addCom_Click;

                

                Label comLab4 = new Label();
                comLab4.Text = "</td></tr></table><br/><br/>";

                NewDivCom.Controls.Add(comLab);
                NewDivCom.Controls.Add(jmBox);
                NewDivCom.Controls.Add(comLab2);
                NewDivCom.Controls.Add(tControl);
                NewDivCom.Controls.Add(comLab3);
                NewDivCom.Controls.Add(addCom);
                NewDivCom.Controls.Add(comLab4);

                NewDiv.Controls.Add(NewDivCom);
            }
        }

        protected void Rezerv0_Click(object sender, EventArgs e)
        {

        }

        protected void AddBrigClick(object sender, EventArgs e)
        {
            Response.Redirect("AddBrig.aspx");
        }

        protected void AddNewsClick(object sender, EventArgs e)
        {
            Response.Redirect("AddNews.aspx");
        }

        protected void Sys_Click(object sender, EventArgs e)
        {

        }



        


    }
}