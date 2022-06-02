using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Data;
using Newtonsoft.Json;
using System.Net.Http;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeRegController : ControllerBase
    {
        private readonly ApiContext _context;

        public EmployeeRegController(ApiContext context)
        {
            _context = context;
        }

        //create

        [HttpPost]
        public JsonResult Register(EmployeeReg employee)
        {
            if (employee.EmployeeID == 0)
            {
                _context.Employee.Add(employee);
            }
            else
            {
                var RegInDB = _context.Employee.Find(employee.EmployeeID);

                if (RegInDB == null)
                {
                    return new JsonResult(NotFound());
                }

                RegInDB = employee;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(employee));
        }

        //get

        //[HttpGet]
        //public JsonResult Get(int ID)
        //{
        //    var result = _context.Employee.Find(ID);

        //    if (result == null)
        //        return new JsonResult(NotFound());

        //    return new JsonResult(Ok(result));
        //}

        //get all

        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.Employee.ToList();

            return new JsonResult(Ok(result));
        }
    }
}
