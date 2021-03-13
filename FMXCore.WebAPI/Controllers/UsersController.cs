using FMXCore.WebAPI.Data;
using FMXCore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FMXCore.WebAPI.Controllers
{
    [Route("api-fmx/users/")]
    public class UsersController : ControllerBase    {

        //Method GET
        [Route("search")]
        [HttpGet("{document}")]
        public ActionResult search([FromServices] DataContext _context, String document)
        {
             Double credit = 0;
             Double debit = 0;

            var users = _context.Users
                        .Where( x => x.Document == document)
                        .FirstOrDefault();

            if (users != null)
            {
                var financeUser = _context.Finances
                                .Where(x => x.UserDocument == users.Document)
                                .ToList();

                foreach (var i in financeUser)
                {

                    if (i.Type == "D")
                    {
                        debit += i.Value;
                    }
                    else if (i.Type == "R")
                    {
                        credit += i.Value;
                    }
                }

                var balance = (credit - debit).ToString("F");

                users.BalanceFin = balance;

                return Ok(users);
            }
            else
            {
                return BadRequest("CPF inexistente.");
            }
        }

        //Method POST
        [Route("register")]
        [HttpPost]
        public ActionResult register([FromServices] DataContext _context, [FromBody] User userModel)
        {
            if (ModelState.IsValid)
            {
                var users = _context.Users
                            .Where(x => x.Document == userModel.Document)
                            .FirstOrDefault();

                if (users == null)
                {
                    try
                    {
                        _context.Users.Add(userModel);
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
                    return BadRequest("CPF já existente na base.");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //Method PUT
        [Route("update")]
        [HttpPut("{document}")]
        public ActionResult update([FromServices] DataContext _context, [FromBody] User userModel, String document)
        {
            if (ModelState.IsValid)
            {
                var users = _context.Users
                            .Where(x => x.Document == document)
                            .FirstOrDefault();

                
                if (users != null)
                {
                    users.Name = userModel.Name;
                    users.BirthDate = userModel.BirthDate;
                    users.Adress = userModel.Adress;
                    users.District = userModel.District;
                    users.City = userModel.City;
                    users.State = userModel.State;
                    users.Country = userModel.Country;

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
                    return BadRequest("Id informado não localizado.");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //Method DELETE
        [Route("delete")]
        [HttpDelete("{id}")]
        public ActionResult delete([FromServices] DataContext _context, int id)
        {
            var users = _context.Users
                        .Where(x => x.Id == id)
                        .Single();

            if (users != null)
            {
                var financeUser = _context.Finances
                                .Where(x => x.UserDocument == users.Document)
                                .ToList();

                if (financeUser.Count == 0)
                {
                    try
                    {
                        _context.Users.Remove(users);
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
                    return BadRequest("Não é possível excluir o usuário. Existem lançamentos atribuídos a seu CPF.");
                }
            }
            else
            {
                return BadRequest("Id informado não localizado.");
            }           
        }
    }
}
