using FMXCore.WebAPI.Data;
using FMXCore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FMXCore.WebAPI.Controllers
{
    [Route("api-fmx/finances/")]
    public class FinancesController : ControllerBase
    {
        //Methodo GET
        [Route("search")]
        [HttpGet]
        public ActionResult search([FromServices] DataContext _context)
        {
            return Ok(_context.Finances);
        }

        //Methodo POST
        [Route("register")]
        [HttpPost]
        public ActionResult register([FromServices] DataContext _context, [FromBody] Finance financeModel)
        {
            if (ModelState.IsValid)
            {                
                var users = _context.Users
                            .Where(x => x.Document == financeModel.UserDocument)
                            .FirstOrDefault();

                if ( users != null )                
                {
                    var finances = _context.Finances
                                   .Where(x => x.Description == financeModel.Description 
                                            && x.DueDate == financeModel.DueDate 
                                            && x.Value == financeModel.Value
                                            && x.UserDocument == financeModel.UserDocument)
                                   .FirstOrDefault();
                    
                    if (finances == null)
                    {                        
                        financeModel.Date = DateTime.Now.Date;
                        financeModel.User = users;                       

                        try
                        {
                            _context.Finances.Add(financeModel);
                            _context.SaveChanges();
                            return Ok("Incluído com sucesso.");
                        }
                        catch (Exception ex)
                        {
                            return BadRequest($"Error: {ex}");
                        }
                    }
                    else
                    {
                        return BadRequest("Lançamento duplicado.");
                    }
                }
                else
                {
                    return NotFound("Usuário não encontrado para atribuir a este lançamento.");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //Methodo PUT
        [Route("update")]
        [HttpPut("{id}")]
        public ActionResult update([FromServices] DataContext _context, [FromBody] Finance financeModel, int id)        
        {
            if (ModelState.IsValid)
            {
                var finances = _context.Finances
                               .Where(x => x.Id == id)
                                .FirstOrDefault();

                if (finances != null)
                {                    
                    finances.Description = financeModel.Description;
                    finances.DueDate = financeModel.DueDate;
                    finances.Value = financeModel.Value;
                    finances.Type = financeModel.Type;

                    try
                    {
                        _context.SaveChanges();
                        return Ok("Alterado com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        return BadRequest($"Error: {ex}");
                    }
                }
                else
                {
                    return NotFound("Não localizado.");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //Methodo DELETE
        [Route("delete")]
        [HttpDelete("{id}")]
        public ActionResult delete([FromServices] DataContext _context, int id)
        {
            var finances = _context.Finances
                           .Where(x => x.Id == id)
                           .Single();

            if (finances != null)
            {               
                try
                {
                    _context.Finances.Remove(finances);
                    _context.SaveChanges();
                    return Ok("Deletado com sucesso.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error: {ex}");
                }               
            }
            else
            {
                return NotFound("Não localizado.");
            }
        }
    }
}
