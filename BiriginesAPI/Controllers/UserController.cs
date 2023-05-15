using BiriginesAPI.DTO;
using Borigines.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Commands;
using Models.Queries;
using Tools.CQRS;
using Tools.JWT.Interfaces;
using CQRS = Tools.CQRS.Commands;
using System.Security.Claims;


namespace BiriginesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDisptacher _disptacher;
        private readonly IToken _token;

        public UserController(IDisptacher disptacher, IToken token)
        {
            _disptacher = disptacher;
            _token = token;
        }



        //// just for Token test
        [HttpPost("Token")]
        public IActionResult Token(UserLoginDTO dto)
        {
            User? u = _disptacher.Dispatch(new LoginUserQuery(dto.Login, dto.Password));
            if (u is null)
            {
                return NotFound();
            }
            return Ok(_token.GenerateToken(
                new Claim(ClaimTypes.NameIdentifier, u.Id.ToString()),
                new Claim(ClaimTypes.Role, u.IsAdmin ? "Admin" : "User")
                ));
        }




        [Authorize(Roles = "Admin")]
        [HttpGet]
        
        public IActionResult Get()
        {
            return Ok(_disptacher.Dispatch(new GetUsersQuery()).ToList());
        }


        [HttpPost("Login")]
        public IActionResult Login(UserLoginDTO dto)
        {
            User? u = _disptacher.Dispatch(new LoginUserQuery(dto.Login, dto.Password));
            if (u is null )
            {
                return NotFound();
            }
            return Ok(u);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("PostUser")]
        
        public IActionResult PostUser(CreateUserDTO dto)
        {
            CQRS.IResult result =
                _disptacher.Dispatch(new CreateUserCommand(dto.First_name,dto.Last_name,dto.Login,dto.Password));
            if (result.IsSuccess)
                return NoContent();

            return BadRequest(new { result.Message });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("PutUser")]
        public IActionResult PutUser(UpdateUserDTO dto)
        {
            CQRS.IResult result =
                _disptacher.Dispatch(
                    new UpdateUserCommand(dto.Id,dto.First_name,dto.Last_name,dto.Login,dto.Password)
                    );

            if (result.IsSuccess)
                return NoContent();

            return BadRequest(new { result.Message });
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            CQRS.IResult result = _disptacher.Dispatch(new DeleteUserCommand(id));
            if(result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(new { result.Message });
        }


    }
}
