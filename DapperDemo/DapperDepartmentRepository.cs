using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DapperDemo
{
    public class DapperDepartmentRepository : IDepartmentRespository
    {
        private readonly IDbConnection _conn;
        //Constructor
        public DapperDepartmentRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _conn.Query<Department>("SELECT * FROM Departments;").ToList();
        }


        public void InsertDepartment(string newDepartmentName)
        {
            _conn.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
             new { departmentName = newDepartmentName });
        }
    }
}
