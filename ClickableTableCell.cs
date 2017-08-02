using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Chata_IS
{
    public class ClickableTableCell : TableCell, IPostBackEventHandler, INamingContainer
    {
        private static readonly object click_event = new object();
        public DayButton dayButton = null;
        public takenDate takenDat = null;
        public ClickableTableCell()
        {
        }

        

        // public handles for adding and removing functions to be called on the click event
        public event EventHandler Click
        {
            add
            {
                Events.AddHandler(click_event, value);
            }
            remove
            {
                Events.RemoveHandler(click_event, value);
            }
        }

        // define parent function that will be called when the container is clicked
        protected void OnClick(EventArgs e)
        {
            EventHandler h = Events[click_event] as EventHandler;
            if (h != null)
            {
                h(this, e);
            }
        }

        // specify the "post back event reference" or id of the click event
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, 
                                Page.ClientScript.GetPostBackEventReference(this, "custom_click"));
        }

        // link the custom click id to the click function
        void System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            if(eventArgument == "custom_click")
            {
                this.OnClick(EventArgs.Empty);
            }
        }
    }
}