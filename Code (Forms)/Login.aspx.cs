using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_Project_Plagiarism_Checker__forms_
{
    public partial class Login : System.Web.UI.Page
    {
        string con_str = "Data Source=.\\SQLEXPRESS;Initial Catalog=Web Project;Integrated Security=True";
        static public string unforall="";
        static public bool ad = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Label7.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(con_str);
            string username = TextBox1.Text, password= TextBox2.Text;
            con.Open();

            
            SqlCommand cmd = new SqlCommand("select * from Users where username=@a AND password=@b", con);
            SqlParameter p1 = new SqlParameter("@a", username);
            SqlParameter p2 = new SqlParameter("@b", password);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

            SqlDataReader r = cmd.ExecuteReader();
            if (!r.Read())
            {
                cmd = new SqlCommand("select * from Admins where username=@a AND password=@b", con);
                p1 = new SqlParameter("@a", username);
                p2 = new SqlParameter("@b", password);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                r.Close();

                r = cmd.ExecuteReader();

                if (!r.Read()) {
                    Label7.Visible = true;
                    

                }
                else
                {
                    ad = true;
                    unforall = "";
                }


                r.Close();
                    
                    
            }
            else
            {
                ad = false;
                unforall = username;
                Label7.Visible = true;

                r.Close();
                    
            }
            con.Close();

            //Page_Load(sender, e);
        }
    }
}