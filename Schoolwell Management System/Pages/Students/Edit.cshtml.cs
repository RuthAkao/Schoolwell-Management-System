using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Schoolwell_Management_System.Pages.Students
{
    public class EditModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
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
                    string sql = "SELECT * FROM Students WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentInfo.Id = "" + reader.GetInt32(0).ToString();
                                studentInfo.StdNo = reader.GetString(1);
                                studentInfo.Name = reader.GetString(2);
                                studentInfo.Age = reader.GetInt32(3).ToString();
                                studentInfo.Sex = reader.GetString(4);
                                studentInfo.Course = reader.GetString(5);
                                studentInfo.Email = reader.GetString(6);
                                studentInfo.Phone = reader.GetString(7);
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
            studentInfo.Id = Request.Form["Id"];
            studentInfo.StdNo = Request.Form["StdNo"];
            studentInfo.Name = Request.Form["Name"];
            studentInfo.Age = Request.Form["Age"];
            studentInfo.Sex = Request.Form["Sex"];
            studentInfo.Course = Request.Form["Course"];
            studentInfo.Email = Request.Form["Email"];
            studentInfo.Phone = Request.Form["Phone"];

            if (studentInfo.StdNo.Length == 0 || studentInfo.Name.Length == 0 || studentInfo.Age.Length == 0 ||
                studentInfo.Sex.Length == 0 || studentInfo.Course.Length == 0 || studentInfo.Email.Length == 0 ||
                studentInfo.Phone.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                string connectionstring = "Data Source=.\\SQLEXPRESS;Initial Catalog=StudentsWell;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql = "UPDATE Students " +
                        "SET StdNo=@StdNo, Name=@Name, Age=@Age, Sex=@Sex, Course=@Course, Email=@Email, Phone=@Phone " +
                        "WHERE Id=@Id";  // Add this line to include the Id parameter in the query

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Add the Id parameter
                        command.Parameters.AddWithValue("@Id", studentInfo.Id);
                        command.Parameters.AddWithValue("@StdNo", studentInfo.StdNo);
                        command.Parameters.AddWithValue("@Name", studentInfo.Name);
                        command.Parameters.AddWithValue("@Age", studentInfo.Age);
                        command.Parameters.AddWithValue("@Sex", studentInfo.Sex);
                        command.Parameters.AddWithValue("@Course", studentInfo.Course);
                        command.Parameters.AddWithValue("@Email", studentInfo.Email);
                        command.Parameters.AddWithValue("@Phone", studentInfo.Phone);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Students/Index");
        }
    }
}