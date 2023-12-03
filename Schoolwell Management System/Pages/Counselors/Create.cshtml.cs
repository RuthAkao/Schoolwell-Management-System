using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Schoolwell_Management_System.Pages.Counselors
{
    public class CreateModel : PageModel
    {
        public CounselorInfo counselorInfo = new CounselorInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            counselorInfo.CounselorId = Request.Form["CounselorId"];
            counselorInfo.Name = Request.Form["Name"];
            counselorInfo.Age = Request.Form["Age"];
            counselorInfo.Sex = Request.Form["Sex"];
            counselorInfo.Specialization = Request.Form["Specialization"];
            counselorInfo.Email = Request.Form["Email"];
            counselorInfo.Phone = Request.Form["Phone"];

            if (counselorInfo.CounselorId.Length == 0 || counselorInfo.Name.Length == 0 || counselorInfo.Age.Length == 0 || 
                counselorInfo.Sex.Length == 0 || counselorInfo.Specialization.Length == 0 || counselorInfo.Email.Length == 0 || 
                counselorInfo.Phone.Length == 0)
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
                    string sql = "INSERT INTO Counselors (CounselorId, Name, Age, Sex, Specialization, Email, Phone) VALUES (@CounselorId, @Name, @Age, @Sex, @Specialization, @Email, @Phone)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CounselorId", counselorInfo.CounselorId);
                        command.Parameters.AddWithValue("@Name", counselorInfo.Name);
                        command.Parameters.AddWithValue("@Age", counselorInfo.Age);
                        command.Parameters.AddWithValue("@Sex", counselorInfo.Sex);
                        command.Parameters.AddWithValue("@Specialization", counselorInfo.Specialization);
                        command.Parameters.AddWithValue("@Email", counselorInfo.Email);
                        command.Parameters.AddWithValue("@Phone", counselorInfo.Phone);

                        command.ExecuteNonQuery();
                    }
                }

                // successMessage = "Counselor added successfully.";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            counselorInfo.CounselorId = ""; counselorInfo.Name = ""; counselorInfo.Age = ""; counselorInfo.Sex = ""; counselorInfo.Specialization = ""; counselorInfo.Email = ""; counselorInfo.Phone = "";
            successMessage = "New Counselor Added Successfully";

            Response.Redirect("/Counselors/Index");
        }
    }
}
