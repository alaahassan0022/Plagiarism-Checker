using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Project_Plagiarism_Checker__forms_
{
    public partial class website_master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            adminadd.Visible = false;
            grid.Visible = false;
            if(Login.ad)
            {
                edid.Visible = false;
                jdid.Visible = false;
                regid.Visible = false;
                adminadd.Visible = true;
            }
            if(Login.unforall!="")
            {
                grid.Visible =  true;
                edid.Visible =  true;
                jdid.Visible =  true;
                regid.Visible = true;

            }
        }
    }
}