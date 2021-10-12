using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngCrud.Controllers
{
    [RoutePrefix("Api/Employee")]
    public class EmployeeAPIController : ApiController
    {

        WebAPIdbEntities ObjEnt = new WebAPIdbEntities();

        [HttpGet]
        [Route("GetAllEmployeeDetails")]
        public IQueryable<EmployeeDetail> GetEmployeeDetails()
        {
            try
            {
                return ObjEnt.EmployeeDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetEmployee/{EmployeeId}")]
        public IHttpActionResult GetEmployeeDetailsById(String EmployeeId)
        {
            EmployeeDetail objEmp = new EmployeeDetail();
            int EmpId = Convert.ToInt32(EmployeeId);
            try
            {
                objEmp = ObjEnt.EmployeeDetails.Find(EmpId);
                if (objEmp == null)
                    return NotFound();
            }
            catch { throw; }
            return Ok(objEmp);
        }

        [HttpPost]
        [Route("InsertEmployee")]
        public IHttpActionResult PostEmployee(EmployeeDetail ObjEmp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                ObjEnt.EmployeeDetails.Add(ObjEmp);
                ObjEnt.SaveChanges();
            }
            catch { throw; }

            return Ok(ObjEmp);
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public IHttpActionResult PutEmployee(EmployeeDetail ObjEmp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                EmployeeDetail employee = new EmployeeDetail();
                employee = ObjEnt.EmployeeDetails.Find(ObjEmp.EmpId);
                if (employee != null)
                {
                    employee.EmpName = ObjEmp.EmpName;
                    employee.EmailId = ObjEmp.EmailId;
                    employee.Address = ObjEmp.Address;
                    employee.DateOfBirth = ObjEmp.DateOfBirth;
                    employee.Gender = ObjEmp.Gender;
                    employee.PinCode = ObjEmp.PinCode;
                }
                int i = this.ObjEnt.SaveChanges();
            }
            catch 
            {
                throw;
            }

            return Ok(ObjEmp);
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public IHttpActionResult DeleteEmployee(int Id)
        {
            EmployeeDetail objEmp = new EmployeeDetail();
            objEmp = ObjEnt.EmployeeDetails.Find(Id);
            if (objEmp == null)
            {
                return BadRequest();

            }
            ObjEnt.EmployeeDetails.Remove(objEmp);
            ObjEnt.SaveChanges();
            return Ok(objEmp);
        }

    }
}
