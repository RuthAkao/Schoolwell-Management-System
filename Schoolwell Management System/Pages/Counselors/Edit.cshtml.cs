using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Schoolwell_Management_System.Pages.Counselors
{
    public class EditModel : PageModel
    {
        public CounselorInfo CounselorInfo = new CounselorInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string Id = Request.Query["Id"];

            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=StudentsWell;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM counselors WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CounselorInfo.Id = "" + reader.GetInt32(0).ToString();
                                CounselorInfo.CounselorId = reader.GetString(1);
                                CounselorInfo.Name = reader.GetString(2);
                                CounselorInfo.Age = reader.GetInt32(3).ToString();
                                CounselorInfo.Sex = reader.GetString(4);
                                CounselorInfo.Specialization = reader.GetString(5);
                                CounselorInfo.Email = reader.GetString(6);
                                CounselorInfo.Phone = reader.GetString(7);
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            CounselorInfo.Id = Request.Form["Id"];
            CounselorInfo.CounselorId = Request.Form["CounselorId"];
            CounselorInfo.Name = Request.Form["Name"];
            CounselorInfo.Age = Request.Form["Age"];
            CounselorInfo.Sex = Request.Form["Sex"];
            CounselorInfo.Specialization = Request.Form["Specialization"];
            CounselorInfo.Email = Request.Form["Email"];
            CounselorInfo.Phone = Request.Form["Phone"];

            if (CounselorInfo.CounselorId.Length == 0 || CounselorInfo.Name.Length == 0 || CounselorInfo.Age.Length == 0 ||
                CounselorInfo.Sex.Length == 0 || CounselorInfo.Specialization.Length == 0 || CounselorInfo.Email.Length == 0 ||
                CounselorInfo.Phone.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                string connectionstring = "Data Source=.\\SQLEXPRESS;Initial Catalog=counselorsWell;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql = "UPDATE counselors " +
                        "SET CounselorId=@CounselorId, Name=@Name, Age=@Age, Sex=@Sex, Specialization=@Specialization, Email=@Email, Phone=@Phone " +
                        "WHERE Id=@Id";  // Add this line to include the Id parameter in the query

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Add the Id parameter
                        command.Parameters.AddWithValue("@Id", CounselorInfo.Id);
                        command.Parameters.AddWithValue("@CounselorId", CounselorInfo.CounselorId);
                        command.Parameters.AddWithValue("@Name", CounselorInfo.Name);
                        command.Parameters.AddWithValue("@Age", CounselorInfo.Age);
                        command.Parameters.AddWithValue("@Sex", CounselorInfo.Sex);
                        command.Parameters.AddWithValue("@Specialization", CounselorInfo.Specialization);
                        command.Parameters.AddWithValue("@Email", CounselorInfo.Email);
                        command.Parameters.AddWithValue("@Phone", CounselorInfo.Phone);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/counselors/Index");
        }
    }
}