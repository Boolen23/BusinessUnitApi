using BusinessUnitApi.Db;
using BusinessUnitApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BusinessUnitApi.Controllers
{
    public class ApiController : Controller
    {
        public ApiController(LiteDbContext db)
        {
            ctx = db;
        }
        private LiteDbContext ctx;
        public IActionResult Index() => View();



        [HttpGet("GetAllBusinessUnits")]
        [Produces("text/json")]
        public IActionResult GetAllBusinessUnits()
        {
            var data = ctx.GetAllBusinessUnits();
            return Ok(data);
        }
        [HttpDelete("DeleteBusinessUnit")]
        public IActionResult DeleteBusinessUnit(int id)
        {
            try
            {
                ctx.DeleteBusinessUnit(id);
                return Ok();
            }
            catch(Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(e.Message);
            }
        }
        [HttpPost("AddBusinessUnit")]
        public async Task<IActionResult> AddBusinessUnit(string Name, string Inn, string Kpp, string Type)
        {
            try
            {
                BusinessUnit bu = new BusinessUnit()
                {
                    Name = Name?.Trim(),
                    Inn = Inn?.Trim(),
                    Kpp = Kpp?.Trim(),
                    TypeString = Type
                };
                await ctx.InsertBusinessUnit(bu);
                return Ok();
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(e.Message);
            }
        }


    }
}
