//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;


//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration.Ini;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using BackendAPI.DataAccess.Abstract;
//using BackendAPI.Dtos;
//using BackendAPI.Entities;
//using BackendAPI.DataAccess.Concrete;
//using Microsoft.VisualStudio.Services.Users;
//using User = BackendAPI.Entities.User;

//namespace BackendAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private IAuthRepository _authRepository;
//        private IConfiguration _configuration;

//        public AuthController(IAuthRepository authRepository,IConfiguration configuration)
//        {
//            _authRepository = authRepository;
//            _configuration = configuration;

//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] UserForRegister userForRegister)
//        {
//            if (await _authRepository.UserExists(userForRegister.UserEmail))
//            {
//                ModelState.AddModelError("UserEmail", "UserEmail already exists");
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);

//            }

//            var userToCreate = new User
//            {
//                userEmail = userForRegister.UserEmail,
//                role = "admin"
//            };

//            var createdUser = await _authRepository.Register(userToCreate, userForRegister.Password);
//            return StatusCode(201, new { message = "Kullanıcı başarıyla oluşturuldu." });

//        }

//        [HttpPost("login")]
//        public async Task<ActionResult> Login([FromBody] UserForLogin userForLogin)
//        {
//            var user = await _authRepository.Login(userForLogin.UserEmail, userForLogin.Password);
//            if (user == null)
//            {
//                return Unauthorized();
//            }

//            var tokenHandler=new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings: Token").Value);

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                   {
//                new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
//                new Claim(ClaimTypes.Email, user.userEmail),
//                new Claim(ClaimTypes.Role, user.role)
//                   }),
//                Expires = DateTime.Now.AddDays(1),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            var tokenString = tokenHandler.WriteToken(token);

//            return Ok(tokenString);
//        }
//    }
//}


//using BackendAPI.DataAccess.Abstract;
//using BackendAPI.Dtos;
//using BackendAPI.Entities;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace BackendAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly IAuthRepository _authRepository;
//        private readonly IConfiguration _configuration;

//        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
//        {
//            _authRepository = authRepository;
//            _configuration = configuration;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] UserForRegister userForRegister)
//        {
//            if (await _authRepository.UserExists(userForRegister.Email))
//            {
//                return BadRequest(new { error = "Bu email adresi zaten kayıtlı!" });
//            }

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            // Şifreyi hash'leyelim
//            byte[] passwordHash, passwordSalt;
//            CreatePasswordHash(userForRegister.Password, out passwordHash, out passwordSalt);

//            // Eğer şifre boş veya null ise hata verebilir, bunu kontrol et!
//            if (passwordHash == null || passwordSalt == null)
//            {
//                Console.WriteLine("❌ HATA: PasswordHash veya PasswordSalt NULL!");
//                return BadRequest(new { error = "Şifre hashleme hatası!" });
//            }


//            var userToCreate = new User
//            {
//                Email = userForRegister.Email,
//                PasswordHash = passwordHash,
//                PasswordSalt = passwordSalt,
//                role="User"
//            };

//            var createdUser = await _authRepository.Register(userToCreate, userForRegister.Password);

//            return StatusCode(201, new { message = "Kullanıcı başarıyla oluşturuldu.", userEmail = createdUser.Email });
//        }

//        [HttpPost("login")]
//        public async Task<ActionResult> Login([FromBody] UserForLogin userForLogin)
//        {
//            var user = await _authRepository.Login(userForLogin.Email, userForLogin.Password);
//            if (user == null)
//            {
//                return Unauthorized(new { error = "Geçersiz e-posta veya şifre." });
//            }

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Token"]);

//            // Kullanıcının rollerini liste olarak işleyelim
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
//                new Claim(ClaimTypes.Email, user.Email),
//                new Claim(ClaimTypes.Role, user.role) // Eğer rol bir array olursa join'lemelisin
//            };

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(claims),
//                Expires = DateTime.UtcNow.AddDays(1), // Token süresi 1 gün
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            var tokenString = tokenHandler.WriteToken(token);

//            return Ok(new { token = tokenString, userEmail = user.Email, role = user.role });
//        }

//        // Şifre Hashleme Fonksiyonu
//        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
//        {
//            using (var hmac = new HMACSHA512())
//            {
//                passwordSalt = hmac.Key;
//                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
//            }
//        }
//    }
//}







using BackendAPI.DataAccess;
using BackendAPI.DataAccess.Abstract;
using BackendAPI.Dtos;
using BackendAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        // 🔥 DOĞRU CONSTRUCTOR
        public AuthController(AppDbContext context, IAuthRepository authRepository, IConfiguration configuration)
        {
            _context = context;
            _authRepository = authRepository;
            _configuration = configuration;
        }

        // ✅ Kullanıcı Kayıt Metodu
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegister userForRegister)
        {
            if (await _authRepository.UserExists(userForRegister.Email))
            {
                return BadRequest(new { error = "Bu email adresi zaten kayıtlı!" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Şifreyi hash'le
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(userForRegister.Password, out passwordHash, out passwordSalt);

            var userToCreate = new User
            {
                Email = userForRegister.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                role = "User"
            };

            // Kullanıcıyı kaydet
            var createdUser = await _authRepository.Register(userToCreate, userForRegister.Password);

            return StatusCode(201, new { message = "Kullanıcı başarıyla oluşturuldu.", userEmail = createdUser.Email });
        }

        // ✅ Kullanıcı Giriş Metodu
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLogin userForLogin)
        {
            var user = await _authRepository.Login(userForLogin.Email, userForLogin.Password);
            bool isSuccess = user != null;

            // Kullanıcının IP adresini al
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            // Log kaydı oluştur
            var log = new Log
            {
                durum = isSuccess,
                islemTipi = "Kullanıcı Girişi",
                aciklama = isSuccess ? "Başarılı giriş" : "Başarısız giriş",
                zaman = DateTime.UtcNow,
                userIp = ipAddress,
                userId = isSuccess ? user.userId : (int?)null
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();

            if (!isSuccess)
            {
                return Unauthorized(new { error = "Geçersiz e-posta veya şifre." });
            }

            // 🔥 JWT Token oluştur
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Token"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString, userEmail = user.Email, role = user.role });
        }

        // ✅ Log Kayıtlarını Getiren Metod (Frontend için)
        [HttpGet("logs")]
        public async Task<ActionResult<IEnumerable<Log>>> GetLogs()
        {
            var logs = await Task.Run(() => _context.Logs.OrderByDescending(l => l.zaman).ToList());
            return Ok(logs);
        }

        // ✅ Şifre Hashleme Fonksiyonu
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
