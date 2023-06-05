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
    [Authorize(Roles = "Admin")]
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
        [AllowAnonymous]
        public IActionResult Token(UserLoginDTO dto)
        {
            User? u = _disptacher.Dispatch(new LoginUserQuery(dto.Login, dto.Password));
            if (u is null)
            {
                return NotFound(new {message = "Token not found"});
            }
            return Ok(_token.GenerateToken(
                new Claim(ClaimTypes.NameIdentifier, u.Id.ToString()),
                new Claim(ClaimTypes.Role, u.IsAdmin ? "Admin" : "User")
                ));
        }




       
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Get()
        {
            IEnumerable<User>? users = _disptacher.Dispatch(new GetUsersQuery()).ToList();
            if(users is null || !users.Any())
            {
                return NotFound(new {message = "No user found in our Data Base"});
            }
            return Ok(users);
        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(UserLoginDTO dto)
        {
            User? u = _disptacher.Dispatch(new LoginUserQuery(dto.Login, dto.Password));
            if (u is null )
            {
                return NotFound(new {message = "You neet to check your informations please"});
            }
            
            return Ok(u);
        }

        [HttpPost("PostUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult PostUser(CreateUserDTO dto)
        {
            CQRS.IResult result =
                _disptacher.Dispatch(new CreateUserCommand(dto.First_name,dto.Last_name,dto.Login,dto.Password));
            if (result.IsSuccess)
            {
                _ = int.TryParse(result.Message, out int id);
                return Ok(new { IdUserInserted = id });
            }

            return BadRequest(new { message =  result.Message });
        }

        [HttpPut("PutUser")]
        [Authorize(Roles = "Admin")]
        public IActionResult PutUser(UpdateUserDTO dto)
        {
            CQRS.IResult result =
                _disptacher.Dispatch(
                    new UpdateUserCommand(dto.Id,dto.First_name,dto.Last_name,dto.Login,dto.Password)
                    );

            if (result.IsSuccess)
                return Ok(new { message = "User Updated as well" });

            return BadRequest(new { message =  result.Message });
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(int id)
        {
            CQRS.IResult result = _disptacher.Dispatch(new DeleteUserCommand(id));
            if(result.IsSuccess)
            {
                return Ok(new {message = $"User N° {id} hase been deleted"});
            }
            return BadRequest(new { message =  result.Message });
        }


        [HttpPost("CheckEmail")]
        public IActionResult CheckEmail(CheckEmailDTO dto)
        {
            bool IsEmailExist = _disptacher.Dispatch(new CheckEmailQuery(dto.EmailToCheck));
            return Ok(new { IsEmailExist });
        }

    }
}
