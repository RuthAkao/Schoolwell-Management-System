using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Schoolwell_Management_System.Pages.Students
{
    public class IndexModel : PageModel
    {
        public List<StudentInfo> listStudents = new List<StudentInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=StudentsWell;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Students";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentInfo studentInfo = new StudentInfo();
                                studentInfo.Id = "" + reader.GetInt32(0);
                                studentInfo.StdNo = reader.GetString(1);
                                studentInfo.Name = reader.GetString(2);
                                studentInfo.Age = reader.GetInt32(3).ToString();
                                studentInfo.Sex = reader.GetString(4);
                                studentInfo.Course = reader.GetString(5);
                                studentInfo.Email = reader.GetString(6);
                                studentInfo.Phone = reader.GetString(7);
                                
                                listStudents.Add(studentInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class StudentInfo
    {
        public string? Id;
        public string? StdNo;
        public string? Name;
        public string? Age;
        public string? Sex;
        public string? Course;
        public string? Email;
        public string? Phone;
    }
}
