using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Schoolwell_Management_System.Pages.Counselors
{
    public class IndexModel : PageModel
    {
        public List<CounselorInfo> listCounselors = new List<CounselorInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=StudentsWell;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Counselors";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CounselorInfo counselorInfo = new CounselorInfo();
                                counselorInfo.Id = "" + reader.GetInt32(0);
                                counselorInfo.CounselorId = reader.GetString(1);
                                counselorInfo.Name = reader.GetString(2);
                                counselorInfo.Age = reader.GetInt32(3).ToString();
                                counselorInfo.Sex = reader.GetString(4);
                                counselorInfo.Specialization = reader.GetString(5);
                                counselorInfo.Email = reader.GetString(6);
                                counselorInfo.Phone = reader.GetString(7);
                                
                                listCounselors.Add(counselorInfo);
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

    public class CounselorInfo
    {
        public string? Id;
        public string? CounselorId;
        public string? Name;
        public string? Age;
        public string? Sex;
        public string? Specialization;
        public string? Email;
        public string? Phone;
    }
}
