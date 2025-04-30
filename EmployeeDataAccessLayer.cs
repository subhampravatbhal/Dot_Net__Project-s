using System.Data;
using System.Data.SqlClient;
using Todo_List.Models;

namespace Todo_List
{
    public class EmployeeDataAccessLayer
    {
        string cs = ConnectionString.dbcs;

        public List<Employees> GetAllEmployees()
        {
            List<Employees> emplist = new List<Employees>();

            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employees emp = new Employees();
                    emp.Id = Convert.ToInt32(reader["Id"]);
                    emp.Name = reader["Name"].ToString() ?? "";
                    emp.Gender = reader["Gender"].ToString() ?? "";
                    emp.Age = Convert.ToInt32(reader["Age"]);
                    emp.Salary = Convert.ToInt32(reader["salary"]);
                    emp.City = reader["City"].ToString() ??"";
                    emplist.Add(emp);
                }   
            }
            return emplist;
        }
        public Employees getEmployeeByID(int? id)
        {
            Employees emp = new Employees();
            using(SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from Employee_table where id = @id", connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id); 
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    emp.Id = Convert.ToInt32(reader["Id"]);
                    emp.Name = reader["Name"].ToString() ?? "";
                    emp.Gender = reader["Gender"].ToString() ?? "";
                    emp.Age = Convert.ToInt32(reader["Age"]);
                    emp.Salary = Convert.ToInt32(reader["salary"]);
                    emp.City = reader["City"].ToString() ?? "";
                  
                }
            }   
            return emp;
        }
        public void AddEmployee(Employees emp)
        {
            using(SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", emp.Name);
                cmd.Parameters.AddWithValue("@gender", emp.Gender);
                cmd.Parameters.AddWithValue("@age", emp.Age);
                cmd.Parameters.AddWithValue("@salary", emp.Salary);
                cmd.Parameters.AddWithValue("@city", emp.City);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateEmployee(Employees emp)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", emp.Id);
                cmd.Parameters.AddWithValue("@name", emp.Name);
                cmd.Parameters.AddWithValue("@gender", emp.Gender);
                cmd.Parameters.AddWithValue("@age", emp.Age);
                cmd.Parameters.AddWithValue("@salary", emp.Salary);
                cmd.Parameters.AddWithValue("@city", emp.City);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteEmployee(int? id)
        {
            using(SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id",id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
           
        }
    }
}
