using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MyApp.Models;
using MyApp.Db;

namespace MyApp.Db.DbOperations
{
    public class EmployeeRepository
    {

      
        public int AddEmployee(EmployeeModel employeeModel)
        {
         

            using (var context = new EmployeeDBEntities1() )
            {

                Employee emp = new Employee()
                {
                    FirstName = employeeModel.FirstName,
                    LastName = employeeModel.LastName,
                    Email = employeeModel.Email,
                    Code = employeeModel.Code
                     
                };

                if (employeeModel.Address != null)
                {
                    emp.Address = new Address()
                    {
                        Country = employeeModel.Address.Country,
                        State = employeeModel.Address.State,
                        Details=employeeModel.Address.Details
                        

                    };

                }


                context.Employees.Add(emp);

                context.SaveChanges();

                return emp.Id;


            }

        }


        public List<EmployeeModel> GetAllRecords()
        {
            using (var context = new EmployeeDBEntities1())
            {
                var result = context.Employees
                    .Select(x => new EmployeeModel()
                    {
                        Id=x.Id,
                        FirstName=x.FirstName,
                        LastName=x.LastName,
                        Code=x.Code,
                        Email=x.Email,
                        AddressId=x.AddressId,
                        Address= new AddressModel()
                        {
                            Id = x.Address.Id,
                            Details=x.Address.Details,
                            State= x.Address.State,
                            Country=x.Address.Country
                        }
                    }).ToList();
                return result;


           
            }
        }

        public EmployeeModel GetRecordsById(int id)
        {
            using (var context = new EmployeeDBEntities1())
            {
                var result = context.Employees
                    .Where( x => x.Id == id)
                    .Select(x => new EmployeeModel(){
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Code = x.Code,
                        Email = x.Email,
                        AddressId = x.AddressId,
                        Address = new AddressModel()
                        {
                            Id = x.Address.Id,
                            Details = x.Address.Details,
                            State = x.Address.State,
                            Country = x.Address.Country
                        }

                    }).FirstOrDefault();
                return result;
            }


        }

        public bool UpdateEmployee(int id, EmployeeModel model)
        {
            using (var context = new EmployeeDBEntities1())
            {
                var employee = context.Employees.FirstOrDefault(x => x.Id == id);

                if(employee != null)
                {
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Code = model.Code;
                    employee.Email = model.Email;
                }
                context.SaveChanges();
                return true;
            }


        }

        public bool DeleteEmployee(int id)
        {
            using (var context = new EmployeeDBEntities1())
            {
                var employee = context.Employees.FirstOrDefault(x => x.Id == id);
                if(employee != null)
                {
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                    return true;
                }

                return false;

            }
        }
    }
}
