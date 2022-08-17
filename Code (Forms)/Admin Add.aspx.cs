using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Web_Project_Plagiarism_Checker__forms_
{
    public partial class admin_add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            label2.Visible = false;
        }
        string con_str = "Data Source=.\\SQLEXPRESS;Initial Catalog=Web Project;Integrated Security=True";
        protected void upload_btn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(con_str);
            con.Open();
            SqlCommand com = new SqlCommand("insert into Files_to_compare_with(FilePath) values (@a)", con);

              string path
                = Server.MapPath
                ("/Files_Admins/" +
                "" + FileUpload1.PostedFile.FileName);

            FileUpload1.SaveAs(path);

            SqlParameter p1 = new SqlParameter("@a", path);
            com.Parameters.Add(p1);
            com.ExecuteNonQuery();
            con.Close();


        }
    }
}