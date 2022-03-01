using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace ShopAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("JWT Authentication")]
    public class AccountController : ControllerBase
    {

        private readonly IJWTAuthManager _authentication;
  
         public AccountController(IJWTAuthManager authentication)
         {
             _authentication = authentication;
         }

        [HttpPost("Register")]
        [SwaggerOperation(Summary="Registers a new customer/login user")]
        [AllowAnonymous]
        public IActionResult Register([FromBody]UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Parameter is missing");
            }
            DynamicParameters dp_param = new DynamicParameters();
            dp_param.Add("email", user.Email, DbType.String);
            dp_param.Add("username", user.Username, DbType.String);
            dp_param.Add("password", user.Password, DbType.String);
            dp_param.Add("role", "Customer", DbType.String);
            dp_param.Add("retVal", DbType.String, direction: ParameterDirection.Output);
            var result = _authentication.Execute_Command<UserModel>("sp_registerUser", dp_param);
            if (result.code == 200)
            {
                Log.Information(result.message);
                return Ok(result);
            }
            Log.Warning(result.message);
            return BadRequest(result);
        }

        [HttpPost("Login")]
        [SwaggerOperation(Summary="Allows users to use this api")]
        [AllowAnonymous]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Parameter is missing");
            }
        
            DynamicParameters dp_param = new DynamicParameters();
            dp_param.Add("email",user.email,DbType.String);
            dp_param.Add("password",user.password,DbType.String);
            dp_param.Add("retVal", DbType.String, direction: ParameterDirection.Output);
            var result = _authentication.Execute_Command<UserModel>("sp_loginUser",dp_param);
            if (result.code == 200)
            {
                var token = _authentication.GenerateJWT(result.Data);
                Log.Information(token.message);
                return Ok(token);
            }
            Log.Warning(result.message);
            return NotFound(result.Data);
        }

        [HttpPost("GiveAdminRole")]
        [SwaggerOperation(Summary="Allows an admin to give another user admin role")]
        [Authorize(Roles="Admin")]
        public IActionResult GiveAdminToUser([FromQuery] string email)
        {
            if (email == string.Empty)
            {
                return BadRequest("Parameter is missing");
            }
        
            DynamicParameters dp_param = new DynamicParameters();
            dp_param.Add("email", email, DbType.String);
        
            dp_param.Add("retVal", DbType.String, direction: ParameterDirection.Output);
            var result = _authentication.Execute_Command<UserModel>("sp_GiveAdminUser", dp_param);
            if (result.code == 200)
            {
                Log.Information(result.message);
                return Ok(result);
            }
            Log.Warning(result.message);
            return NotFound(result);
        }

        [HttpDelete("Delete")]
        [SwaggerOperation(Summary="Deletes a login user")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            if (id == string.Empty)
            {
                return BadRequest("Parameter is missing");
            }
        
            DynamicParameters dp_param = new DynamicParameters();
            dp_param.Add("userid", id, DbType.String);
        
            dp_param.Add("retVal", DbType.String, direction: ParameterDirection.Output);
            var result = _authentication.Execute_Command<UserModel>("sp_deleteUser", dp_param);
            if (result.code == 200)
            {
                Log.Information(result.message);
                return Ok(result);
            }
            Log.Warning(result.message);
            return NotFound(result);
        } 
    }
}