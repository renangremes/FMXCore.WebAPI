using FMXCore.WebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Linq;

namespace FMXCore.WebAPI.Controllers
{
    [Route("api-fmx/reports/")]
    public class ReportsController : ControllerBase
    {       

        //Methodo GET
        [Route("searchFinances")]
        [HttpGet("{document, dateInitial, dateFinal}")]
        public ActionResult searchFinances([FromServices] DataContext _context, String document, DateTime dateInitial, DateTime dateFinal)
        { 
            if (document != null)
            {
                if (dateInitial == DateTime.MinValue && dateFinal == DateTime.MinValue)
                {                    
                    var finances = _context.Finances
                                  .Where(x => x.UserDocument == document)
                                  .ToList();

                    finances = finances.OrderBy(y => y.Date).ToList();

                    DataReport dataReport = new DataReport(finances);
                    ArrayList listFinance = dataReport.reportFinance();

                    if (finances.Count > 0)
                    {                        
                        return Ok(listFinance);
                    }
                    else
                    {
                        return NotFound("Não existem lançamentos para este CPF.");
                    }
                }
                else if (dateInitial != DateTime.MinValue && dateFinal != DateTime.MinValue && dateFinal > dateInitial)
                {
                    var finances = _context.Finances
                                  .Where(x => x.UserDocument == document && x.Date >= dateInitial && x.Date <= dateFinal)
                                  .ToList();

                    finances = finances.OrderBy(y => y.Date).ToList();

                    DataReport dataReport = new DataReport(finances);
                    ArrayList listFinance = dataReport.reportFinance();

                    if (finances.Count > 0)
                    {
                        return Ok(listFinance);
                    }
                    else
                    {
                        return NotFound("Não existem lançamentos para os parâmetros informados.");
                    }
                }
                else
                {
                    return BadRequest("Informe um período válido");
                }
            }
            else
            {
                return BadRequest("Informe um CPF.");
            }
        }  
    }
}
