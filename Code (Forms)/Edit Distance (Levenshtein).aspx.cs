using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IKVM;
using TikaOnDotNet.TextExtraction;
using System.IO;
using F23.StringSimilarity;
using System.Data.SqlClient;


namespace Web_Project_Plagiarism_Checker__forms_
{
    public partial class edit_dis : System.Web.UI.Page
    {
        string[] all_files_paths;
        string[] all_files_content;
        double[] all_ed;
        string new_file_content = "";
        string con_str = "Data Source=.\\SQLEXPRESS;Initial Catalog=Web Project;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            ed_lbl2.Visible = false;
        }

        protected void upload_btn_Click(object sender, EventArgs e)
        {
            load_all_files();

            string path
                = Server.MapPath
                ("/Files_Users/" +
                "" + FileUpload1.PostedFile.FileName);

            FileUpload1.SaveAs(path);

            convert_all_to_str(path);

            double p = compare_all_ed_and_show_min();
            if (Login.unforall != "")
            {
                string fn = FileUpload1.PostedFile.FileName;
                saveindb(p, fn);
            }
        }

        void saveindb(double p, string fn)
        {
            SqlConnection sc = new SqlConnection(con_str);
            sc.Open();
            SqlCommand cmd = new SqlCommand("insert into Users_Files (username,FileName,Plagiarism_Percentage_ed) values (@x,@y,@z);", sc);
            SqlParameter p1 = new SqlParameter("@x", Login.unforall);
            SqlParameter p2 = new SqlParameter("@y", fn);
            SqlParameter p3 = new SqlParameter("@z", float.Parse(p.ToString()));
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.ExecuteNonQuery();
            sc.Close();

        }

        void load_all_files()
        {

            SqlConnection sc = new SqlConnection(con_str);
            sc.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from Files_to_compare_with;", sc);
            SqlDataReader r = cmd.ExecuteReader();

            if (r.Read())
            {
                int nof = (Convert.ToInt32(r[0]));
                if (nof > 0)
                {
                    all_files_paths = new string[nof];
                    all_files_content = new string[nof];
                    all_ed = new double[nof];

                }
            }
            r.Close();

            cmd = new SqlCommand("select * from Files_to_compare_with;", sc);
            r = cmd.ExecuteReader();
            int c = 0;
            if (r.HasRows)
            {
                while (r.Read())
                {
                    all_files_paths[c++] = r[0].ToString();
                }
            }
            r.Close();

            sc.Close();
        }

        void convert_all_to_str(string path_of_new)
        {

            for (int i = 0; i < all_files_paths.Length; i++)
            {
                all_files_content[i] = convert_to_str(all_files_paths[i]);
            }
            new_file_content = convert_to_str(path_of_new);
        }

        private string convert_to_str(string filepath)
        {
            TextExtractor textExtractor = new TextExtractor();
            TextExtractionResult textExtractionResult = textExtractor.Extract(filepath);
            //TextBox1.Rows = 10;

            string txt = textExtractionResult.Text;
          
            return txt;
        }

        

        double compare_all_ed_and_show_min()
        {

            for (int i = 0; i < all_files_content.Length; i++)
            {
                all_ed[i] = compare_ed(all_files_content[i], new_file_content);
                //sum_of_ed += Math.Pow(all_ed[i], 2);

            }

            double mindist = all_ed[0];
            int minind = -1;

            double maxdist = all_ed[0];
            int maxind = -1;

            for (int i = 0; i < all_ed.Length; i++)
            {
                if (all_ed[i] <= mindist)
                {
                    mindist = all_ed[i];
                    minind = i;
                }
                if (all_ed[i] >= maxdist)
                {
                    maxdist = all_ed[i];
                    maxind = i;
                }
            }
            //double mindist_norm = mindist / (Math.Sqrt(sum_of_ed));
            double mindist_norm = mindist / maxdist;

            double p = (1 - mindist_norm) * 100;

            ed_lbl1.Visible = true;
            ed_lbl2.Visible = true;
            ed_lbl2.Text = "Plagiarism Percentage is: "
                + p.ToString() + "%";
            return p;

        }

        private double compare_ed(string s1, string s2)
        {
            Levenshtein l = new Levenshtein();

            double ed = l.Distance(s1, s2);

            return ed;
        }
    }
}