using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Web_Project_Plagiarism_Checker__forms_
{
    public partial class Register : System.Web.UI.Page
    {
        string con_str = "Data Source=.\\SQLEXPRESS;Initial Catalog=Web Project;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            Label7.Visible = false;

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            string username = TextBox5.Text;
            string name = TextBox1.Text;
            string password = TextBox6.Text;
           SqlConnection con = new SqlConnection(con_str);

            con.Open();

            SqlCommand check = new SqlCommand("select * from Users where username = @reg_user_name", con);
            check.Parameters.Add(new SqlParameter("@reg_user_name", username));
            SqlDataReader r_check = check.ExecuteReader();
            if (!r_check.Read())
            {
      
            r_check.Close();
            SqlCommand cmd = new SqlCommand("insert into Users(name,username,password) values(@a,@b,@f)", con);
            SqlParameter p1 = new SqlParameter("@a", name);
            SqlParameter p2 = new SqlParameter("@b", username);
            SqlParameter p6 = new SqlParameter("@f", password);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p6);
            cmd.ExecuteNonQuery();
            Label7.Visible = true;
                
            }
            else
            {
                r_check.Close();
            }
            con.Close();


           

        }
    }
}