using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Microsoft.AspNet.Membership.OpenAuth;

namespace Chata_IS.Account
{
    public partial class AddBrig : System.Web.UI.Page
    {
        protected string SuccessMessage
        {
            get;
            private set;
        }

        protected bool CanRemoveExternalLogins
        {
            get;
            private set;
        }

        protected void Page_Load()
        {
            if (Session["UserAuthentication"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!SQLBase.Instance.isAdminLoggedIn())
            {
                Response.Redirect("Login.aspx");
            }

            CKEditor1.config.toolbar = new object[]
			{
				new object[] { "Source", "-","Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "Print", "SpellChecker", "Scayt" },
				new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
				new object[] { "Form", "Checkbox", "Radio", "TextField", "Textarea", "Select", "Button", "ImageButton", "HiddenField" },
				"/",
				new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
				new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv" },
				new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
				new object[] { "BidiLtr", "BidiRtl" },
				new object[] { "Link", "Unlink", "Anchor" },
				new object[] { "Image", "Flash", "Table", "HorizontalRule", "Smiley", "SpecialChar", "PageBreak", "Iframe" },
				"/",
				new object[] { "Styles", "Format", "Font", "FontSize" },
				new object[] { "TextColor", "BGColor" },
				new object[] { "Maximize", "ShowBlocks", "-", "About" }
			};

        }

        protected void SaveNews(object sender, EventArgs e)
        {
            DateTime nDate = DateTime.Now;

            System.Diagnostics.Debug.WriteLine(CKEditor1.Text);

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@text_vypisu", CKEditor1.Text));
            parameters.Add(new MySqlParameter("@datum", nDate));

            if (
              SQLBase.Instance.Insert("insert into Vypis_brigad (`text_vypisu`, `datum`)" +
                " values (@text_vypisu,@datum);", parameters)
                )
            {

                System.Threading.Thread.Sleep(250);
                Response.Redirect("Manage.aspx");
            }
        }

        


    }
}