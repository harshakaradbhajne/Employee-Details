using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using DataTableDemo.Models;
using System.Data.SqlClient;

namespace DataTableDemo.Controllers
{
    public class IndustryDemoController : Controller
    {
        // GET: IndustryDemo
        //Object of DataOperation class
        DataOperations dataOperations = new DataOperations();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetCountry()
        {
            List<Country> country = new List<Country>();
            DataSet ds = dataOperations.GetCountryData();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                country.Add(new Country()
                {
                    CountryId = Convert.ToInt32(dr["CountryId"]),
                    CountryName = dr["CountryName"].ToString(),
                });
            }
            return Json(country);

        }
        [HttpPost]
        public JsonResult GetState(int CountryId)
        {
            List<State> state = new List<State>();
            DataSet ds = dataOperations.GetStateData(CountryId);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                state.Add(new State()
                {

                    StateId = Convert.ToInt32(dr["StateId"]),
                    CountryId = Convert.ToInt32(dr["CountryId"]),
                    StateName = dr["StateName"].ToString(),
                });
            }
            return Json(state);

        }
        [HttpPost]
        public JsonResult LoadData()
        {
            List<EmpModel> emp = dataOperations.GetEmpData();
            return Json(emp);

        }
        [HttpPost]
        public JsonResult GetbyID(int empID)
        {
            List<EmpModel> emplist = new List<EmpModel>();
            DataSet ds = dataOperations.GetEmpDatabyID(empID);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                emplist.Add(new EmpModel()
                {
                    id = Convert.ToInt32(dr["EmployeeId"]),
                    Name = dr["Name"].ToString(),
                    Age = Convert.ToInt32(dr["Age"]),
                    CountryId = Convert.ToInt32(dr["CountryId"]),
                    StateId = Convert.ToInt32(dr["StateId"]),
                }) ; 
            }
            return Json(emplist);
        }
        [HttpPost]
        public JsonResult InsertUpdateEmployee(EmpModel empObj)
        {
            int result = dataOperations.InsertUpdateEmp(empObj);
            return Json(result);
        }
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            return Json(dataOperations.DeleteEmployee(ID));
        }
      


    }
}
