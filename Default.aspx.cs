using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Chata_IS
{
    public partial class _Default : Page
    {
        private void del_Click(object sender, EventArgs e)
        {
            string id = ((Button)(sender)).ID.Split('_')[1];
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id",id));
            SQLBase.Instance.Delete("delete from Novinka where id_novinky = @id", parameters);
            Response.Redirect("Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            List<string>[] queryResult = SQLBase.Instance.Select(
                "SELECT html_text,datum,id_novinky FROM Novinka ORDER BY datum DESC", 3, parameters);
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
                string html = queryResult[0][i];
                DateTime.TryParse(queryResult[1][i], out date);
                System.Web.UI.HtmlControls.HtmlGenericControl NewDiv = 
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");  
                NewDiv.ID = "divcreated"+i;
                Label nLabel = new Label();
                NewDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                NewDiv.Style.Add(HtmlTextWriterStyle.Width, "99%");
                NewDiv.Style.Add(HtmlTextWriterStyle.BorderWidth,"0px");
                NewDiv.Style.Add(HtmlTextWriterStyle.BorderStyle,"solid");
                NewDiv.Style.Add(HtmlTextWriterStyle.BorderColor, "#545454");
                NewDiv.Style.Add(HtmlTextWriterStyle.PaddingTop,"5px");
                NewDiv.Style.Add(HtmlTextWriterStyle.PaddingBottom, "5px");
                NewDiv.Style.Add(HtmlTextWriterStyle.PaddingLeft, "5px");
                NewDiv.Style.Add(HtmlTextWriterStyle.PaddingRight, "5px");
                NewDiv.Style.Add(HtmlTextWriterStyle.MarginBottom,"20px");
                NewDiv.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
                AktualityDiv.Controls.Add(NewDiv);
                NewDiv.Controls.Add(nLabel);
                nLabel.Text = html;

                System.Web.UI.HtmlControls.HtmlGenericControl NewDivDat =
                    new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                NewDiv.ID = "divdatum"+i;
                NewDivDat.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                Label nLabel2 = new Label();

                AktualityDiv.Controls.Add(NewDivDat);
                NewDivDat.Controls.Add(nLabel2);
                nLabel2.Text = date.ToString("dd/MM/yyyy") + "<br/><hr><br/>";

                Button delete = new Button();
                delete.ID = "del_" + queryResult[2][i];
                delete.Text = "Smazat";
                delete.OnClientClick = "return confirm('Chcete smazat novinku id:" + queryResult[2][i] + " ?')";
                if (!SQLBase.Instance.isAdminLoggedIn())
                    delete.Visible = false;
                NewDivDat.Controls.Add(delete);
                delete.Click += del_Click;

            }
        }
    }
}