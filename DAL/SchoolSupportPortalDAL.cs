using MySqlConnector;
using SchoolSupportPortal.Models;
using System.Data;

namespace SchoolSupportPortal.DAL
{
    public class SchoolSupportPortalDAL
    {
        private readonly string _connectionString;
        public SchoolSupportPortalDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<SchoolDetails> Tbl_SchoolDetails_CRUD_Operations(SchoolDetails school)
        {
            var schools = new List<SchoolDetails>();

            string CleanParam(string? value)
            {
                return string.IsNullOrWhiteSpace(value) || value.Trim().ToLower() == "string" ? null : value;
            }

            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                using (var cmd = new MySqlCommand("Proc_Tbl_SchoolDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("p_ID", (object?)CleanParam(school.ID) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_SchoolName", (object?)CleanParam(school.SchoolName) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_ContactNumber", (object?)CleanParam(school.ContactNumber) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_ContactName", (object?)CleanParam(school.ContactName) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_EmailID", (object?)CleanParam(school.EmailID) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_DateOfVisited", school.DateOfVisited ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("p_EnquieryFor", (object?)CleanParam(school.EnquieryFor) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_IDCardType", (object?)CleanParam(school.IDCardType) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_NoOfStudents", (object?)CleanParam(school.NoOfStudents) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_AmountForIDs", (object?)CleanParam(school.AmountForIDs) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_AmountForStudentErp", (object?)CleanParam(school.AmountForStudentErp) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_Address", (object?)CleanParam(school.Address) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_CreatedBy", (object?)CleanParam(school.CreatedBy) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_CreatedIP", (object?)CleanParam(school.CreatedIP) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_ModifiedBy", (object?)CleanParam(school.ModifiedBy) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_ModifiedIP", (object?)CleanParam(school.ModifiedIP) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("p_Flag", (object?)CleanParam(school.Flag) ?? DBNull.Value);

                    conn.Open();

                    if (!string.IsNullOrEmpty(school.Flag))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var s = new SchoolDetails
                                {
                                    ID = reader["ID"]?.ToString(),
                                    SchoolName = reader["SchoolName"]?.ToString(),
                                    ContactNumber = reader["ContactNumber"]?.ToString(),
                                    ContactName = reader["ContactName"]?.ToString(),
                                    EmailID = reader["EmailID"]?.ToString(),
                                    DateOfVisited = reader["DateOfVisited"] == DBNull.Value ? null : Convert.ToDateTime(reader["DateOfVisited"]),
                                    EnquieryFor = reader["EnquieryFor"]?.ToString(),
                                    IDCardType = reader["IDCardType"]?.ToString(),
                                    NoOfStudents = reader["NoOfStudents"]?.ToString(),
                                    AmountForIDs = reader["AmountForIDs"]?.ToString(),
                                    AmountForStudentErp = reader["AmountForStudentErp"]?.ToString(),
                                    Address = reader["Address"]?.ToString(),
                                    CreatedBy = reader["CreatedBy"]?.ToString(),
                                    CreatedIP = reader["CreatedIP"]?.ToString(),
                                    CreatedDate = reader["CreatedDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["CreatedDate"]),
                                    ModifiedBy = reader["ModifiedBy"]?.ToString(),
                                    ModifiedIP = reader["ModifiedIP"]?.ToString(),
                                    ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["ModifiedDate"]),
                                    Status = reader["Message"]?.ToString()
                                };

                                schools.Add(s);
                            }
                        }
                    }
                }

                return schools;
            }
            catch (Exception ex)
            {
                return new List<SchoolDetails>
        {
            new SchoolDetails
            {
                Status = $"ERROR: {ex.Message}"
            }
        };
            }
        }

    }
}
