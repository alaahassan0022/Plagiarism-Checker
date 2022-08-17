using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Web_Project_Plagiarism_Checker__forms_
{

    public partial class gen_report : System.Web.UI.Page
    {
        string con_str = "Data Source=.\\SQLEXPRESS;Initial Catalog=Web Project;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Login.unforall != "") {

                SqlConnection con = new SqlConnection(con_str);
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from  Users_Files where username=@a ", con);
                SqlParameter p1 = new SqlParameter("@a", Login.unforall);
                cmd.Parameters.Add(p1);
                SqlDataReader r = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                GridView1.DataSource = r;
                GridView1.DataBind();
                
                r.Close();
                con.Close();
            }
            
        }



    

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
    }
}