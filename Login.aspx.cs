using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace Chata_IS.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {

            }
        }

        protected void OnAuthenticate(object sender, AuthenticateEventArgs e)
        {
            string username = Login1.UserName;
            string pwd = Login1.Password;
            

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@login", username));
            parameters.Add(new MySqlParameter("@heslo", pwd));

            System.Diagnostics.Debug.WriteLine("Creating and executing SQL select");
            List<string>[] queryResult = SQLBase.Instance.Select(
                "SELECT id_clena FROM Clen where login=@login and heslo=@heslo", 1, parameters);


            if (SQLBase.Instance.foundSomeResult)
            {
                // sucess
                System.Diagnostics.Debug.WriteLine("Login success");
                Session["UserAuthentication"] = username;
                Session["UserID"] = queryResult[0][0];



                GlobalData.Instance.loggedID = Int32.Parse(queryResult[0][0]);



                HttpCookie myCookie = new HttpCookie("lid");
                DateTime now = DateTime.Now;

                // Set the cookie value.
                myCookie.Value = GlobalData.Instance.loggedID.ToString();
                // Set the cookie expiration date.
                myCookie.Expires = now.AddYears(50); // For a cookie to effectively never expire

                // Add the cookie.
                Response.Cookies.Add(myCookie);

                Session.Timeout = 1;
                FormsAuthentication.RedirectFromLoginPage(username, true);




                Response.Redirect("Manage.aspx");
            }
            else
            {
                // fail
                System.Diagnostics.Debug.WriteLine("Login failed");
            }

     
        }

        protected void UserName_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}