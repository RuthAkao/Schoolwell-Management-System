using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Schoolwell_Management_System.Pages.Students
{
    public class CreateModel : PageModel
    {
        public StudentInfo studentInfo = new StudentInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            studentInfo.StdNo = Request.Form["StdNo"];
            studentInfo.Name = Request.Form["Name"];
            studentInfo.Age = Request.Form["Age"];
            studentInfo.Sex = Request.Form["Sex"];
            studentInfo.Course = Request.Form["Course"];
            studentInfo.Email = Request.Form["Email"]; ;
            studentInfo.Phone = Request.Form["Phone"];

            if (studentInfo.StdNo.Length == 0 || studentInfo.Name.Length == 0 || studentInfo.Age.Length == 0 || 
                studentInfo.Sex.Length == 0 || studentInfo.Course.Length == 0 || studentInfo.Email.Length == 0 || 
                studentInfo.Phone.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            // save the new student into the database
            try
            {
                string connectionstring = "Data Source=.\\SQLEXPRESS;Initial Catalog=StudentsWell;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string sql = "INSERT INTO Students (StdNo, Name, Age, Sex, Course, Email, Phone) VALUES (@StdNo, @Name, @Age, @Sex, @Course, @Email, @Phone)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
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

            studentInfo.StdNo = ""; studentInfo.Name = ""; studentInfo.Age = ""; studentInfo.Sex = ""; studentInfo.Course = ""; studentInfo.Email =  ""; studentInfo.Phone = ""; 
            successMessage = "New Student Added Successfully";

            Response.Redirect("/Students/Index");
        }
    }
}
