using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using kimberly_ws.Data.DbContexts;
using kimberly_ws.Data.Dto;
using kimberly_ws.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace kimberly_ws.Repositories
{
    public class LoginRepository
    {
        #region [Variables & Constructor]
        private readonly RpvDbContext _context;
        private readonly AppSettings _appSettings;
        //private readonly IConfiguration _configuration;
        private readonly IServiceCollection _services;
        private readonly int repositorio = 28;
        
        public LoginRepository(IOptions<AppSettings> appSettings, RpvDbContext context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        #endregion

        #region [Public]
         public async Task<AuthDto> Authenticate(LoginDto auth)
        {            
            // Comprobamos si viene informado el login y el password
            if (string.IsNullOrEmpty(auth.Login) || string.IsNullOrEmpty(auth.Password))
                throw new NotContentException("Usuario o password no válidos");

            // Obtenemos los usuarios del login solicitado
            var usersJson = "";
            var conn = new SqlConnection(_context.ConnIntranet);
            SqlCommand cmd = new SqlCommand("sp_auth", conn);
            cmd.CommandTimeout = 120;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@login", System.Data.SqlDbType.NVarChar).Value = auth.Login;
            cmd.Parameters.Add("@pass", System.Data.SqlDbType.NVarChar).Value = auth.Password;

            await conn.OpenAsync();
            try
            {
                var dataReader = await cmd.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    usersJson += dataReader.GetString(0);
                }
            }
            catch (Exception e)
            {
                usersJson = "";
            }
            finally
            {
                await conn.CloseAsync();
            }
            
            
            
            if(usersJson == "")
                throw new NotContentException("El usuario no es válido");

            var users = (List<UserDto>) null;
            try
            {
                users  = JsonSerializer.Deserialize<List<UserDto>>(usersJson);
            }
            catch (Exception ex)
            {
                throw new Helpers.SqlException("Error de deserialización: \n" + ex.Message);
            }
            
            // Comprobamos si existe el usuario
            if (users == null) throw new NotContentException("El usuario no es válido");

            // Comprobamos si coincide el password encriptándolo primero
            //if (EncryptPassword(auth.Password) != user.Password) throw new BadRequestException("El password no es válido");
            
            // Convertimos la key de appsettings en un array de bytes
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            // Crea el descriptor del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // 	Establece las notificaciones de salida que se van a incluir en el token emitido.
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, users[0].IdPersona),
                    new Claim(ClaimTypes.Email, users[0].Detalle)
                }),

                // Establece la fecha que expira el token
                Expires = DateTime.UtcNow.AddDays(2),

                // Establece las credenciales que se utilizan para firmar el token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)               
            };

            // Creamos un manejador para el token
            var tokenHandler = new JwtSecurityTokenHandler();

            // Creamos el token en base al descriptor del token especificado
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Serializa el token
            var tokenString = tokenHandler.WriteToken(token);

            // Creamos el AuthDto de salida
            AuthDto response = new AuthDto
            {
                Users = usersJson,
                Token = tokenString
            };          

            return response;
        }
         
        #endregion
    }
}