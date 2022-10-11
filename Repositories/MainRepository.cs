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
    public class MainRepository
    {
        #region [Variables & Constructor]
        private readonly RpvDbContext _context;
        private readonly AppSettings _appSettings;
        //private readonly IConfiguration _configuration;
        private readonly IServiceCollection _services;
        private readonly int repositorio = 28;
        
        public MainRepository(IOptions<AppSettings> appSettings, RpvDbContext context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        #endregion

        #region [Getters]
        public async Task<int> GetAppVersion()
        {
            var res = -1;
            int rowcount = 0;
           
            
            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_getAppVersion", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();
            
                while (reader.Read())
                {
                    res = reader.GetInt32(0);
                }
            
                await reader.CloseAsync();

            }
            
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            
            return res;
        }
        
        public async Task<object> GetTodosCentros(string authorization, int idUser)
        {
            var res = "";
            int rowcount = 0;
            var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;
            
            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_getTodosCentros", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                cmd.Parameters.Add("@idUser", SqlDbType.NVarChar).Value = person;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();
            
                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
            
                await reader.CloseAsync();

            }
            // catch (NotContentException ex)
            // {   
            //     throw new NotContentException(ex.Message);
            // }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            
            //return JsonSerializer.Serialize<TareasOnDemand[]>(JsonSerializer.Deserialize<TareasOnDemand[]>(res));
            return res;
        }
        
        public async Task<object> GetTodosTipos(string authorization)
        {
            var res = "";
            int rowcount = 0;
            // var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            // var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            // var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;
            
            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_getTodosTipos", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
              

                SqlDataReader reader = await cmd.ExecuteReaderAsync();
            
                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
            
                await reader.CloseAsync();
                
                if(string.IsNullOrEmpty(res))
                    throw new NotContentException("Sin datos");

            }
            catch (NotContentException ex)
            {   
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            
            //return JsonSerializer.Serialize<TareasOnDemand[]>(JsonSerializer.Deserialize<TareasOnDemand[]>(res));
            return res;
        }
        
        public async Task<object> GetCentrosRuta(string authorization, int idUser)
        {
            var res = "";
            int rowcount = 0;
            var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;
            
            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_getCentrosRuta", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                cmd.Parameters.Add("@idUser", SqlDbType.NVarChar).Value = idUser;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();
            
                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
            
                await reader.CloseAsync();
                
                if(string.IsNullOrEmpty(res))
                    throw new NotContentException("No hay centros");

            }
            catch (NotContentException ex)
            {   
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            
            //return JsonSerializer.Serialize<TareasOnDemand[]>(JsonSerializer.Deserialize<TareasOnDemand[]>(res));
            return res;
        }
        
        public async Task<int> GetVisita(string authorization, int idUser, int idCentro)
        {
            var res = -1;
            int rowcount = 0;
            var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;
            
            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_getVisita", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                cmd.Parameters.Add("@idUser", SqlDbType.NVarChar).Value = idUser;
                cmd.Parameters.Add("@idCentro", SqlDbType.NVarChar).Value = idCentro;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();
            
                while (reader.Read())
                {
                    res = reader.GetInt32(0);
                }
            
                await reader.CloseAsync();
                
                if (res == -1)
                    throw new NotContentException("Error: " + "No se ha isertado la visita correctamente");

            }
            catch (NotContentException ex)
            {   
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            
            //return JsonSerializer.Serialize<TareasOnDemand[]>(JsonSerializer.Deserialize<TareasOnDemand[]>(res));
            return res;
        }
        
        public async Task<string> GetFotos(string authorization, int idCentro, int categoria)
        {
            var res = "";
            int rowcount = 0;
            var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            var idPerson = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_getFotos", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = idPerson;
                cmd.Parameters.Add("@idCentro", SqlDbType.Int).Value = idCentro;
                cmd.Parameters.Add("@categoria", SqlDbType.Int).Value = categoria;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetString(0);
                }

                await reader.CloseAsync();

                if (res == "")
                    throw new NotContentException("Sin datos");

            }
            catch (NotContentException ex)
            {
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            //return JsonSerializer.Serialize<TareasOnDemand[]>(JsonSerializer.Deserialize<TareasOnDemand[]>(res));
            return res;
        }
        
        public async Task<string> GetContactos(string authorization, int idCentro)
        {
            var res = "";
            int rowcount = 0;
            var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            var idPerson = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;
            
            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_getContactos", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = idPerson;
                cmd.Parameters.Add("@idCentro", SqlDbType.NVarChar).Value = idCentro;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();
            
                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
            
                await reader.CloseAsync();
                
                if (res == "")
                    throw new NotContentException("Sin datos");
                
            }
            catch (NotContentException ex)
            {   
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            
            //return JsonSerializer.Serialize<TareasOnDemand[]>(JsonSerializer.Deserialize<TareasOnDemand[]>(res));
            return res;
        }
        
        public async Task<string> GetIncidencias(string authorization, int idCentro)
        {
            var res = "";
            int rowcount = 0;
            var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_getIncidencias", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                cmd.Parameters.Add("@idCentro", SqlDbType.Int).Value = idCentro;
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
                
                if (string.IsNullOrEmpty(res))
                    throw new NotContentException("Sin datos");
                
            }
            catch (JsonException jsEx)
            {
                throw new NotContentException("Sin datos");
            }
            catch (NotContentException ex)
            {
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(ea.Ea) + JsonSerializer.Serialize(ea.EaRef);
            return res;
        }
        
        public async Task<string> GetTipoIncidencias(string authorization)
        {
            var res = "";
            int rowcount = 0;
            var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_getIncidenciasTipo", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
                
                if (string.IsNullOrEmpty(res))
                    throw new NotContentException("Sin datos");
                
            }
            catch (JsonException jsEx)
            {
                throw new NotContentException("Sin datos");
            }
            catch (NotContentException ex)
            {
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(ea.Ea) + JsonSerializer.Serialize(ea.EaRef);
            return res;
        }
        
        public async Task<string> GetIncidenciasAcciones(string authorization)
        {
            var res = "";
            int rowcount = 0;
            var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_getIncidenciasAcciones", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
                
                if (string.IsNullOrEmpty(res))
                    throw new NotContentException("Sin datos");
                
            }
            catch (JsonException jsEx)
            {
                throw new NotContentException("Sin datos");
            }
            catch (NotContentException ex)
            {
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(ea.Ea) + JsonSerializer.Serialize(ea.EaRef);
            return res;
        }
        
        public async Task<string> GetPromociones(string authorization, int idCentro)
        {
            var res = "";
            int rowcount = 0;
            var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_getPromociones", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                cmd.Parameters.Add("@idCentro", SqlDbType.Int).Value = idCentro;
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
                
                if (string.IsNullOrEmpty(res))
                    throw new NotContentException("Sin datos");
                
            }
            catch (JsonException jsEx)
            {
                throw new NotContentException("Sin datos");
            }
            catch (NotContentException ex)
            {
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(ea.Ea) + JsonSerializer.Serialize(ea.EaRef);
            return res;
        }
        
        public async Task<string> GetReportePromociones(string authorization, int idCentro)
        {
            var res = "";
            int rowcount = 0;
            // var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            // var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            // var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_getReportePromociones", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                cmd.Parameters.Add("@idCentro", SqlDbType.Int).Value = idCentro;
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
                
                if (string.IsNullOrEmpty(res))
                    throw new NotContentException("Sin datos");
                
            }
            catch (JsonException jsEx)
            {
                throw new NotContentException("Sin datos");
            }
            catch (NotContentException ex)
            {
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(ea.Ea) + JsonSerializer.Serialize(ea.EaRef);
            return res;
        }
        
        public async Task<string> GetProductos(string authorization,  string rotulo)
        {
            var res = "";
            int rowcount = 0;
            // var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            // var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            // var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_getProductos", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@rotulo", SqlDbType.NVarChar).Value = rotulo;
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
                
                if (string.IsNullOrEmpty(res))
                    throw new NotContentException("Sin datos");
                
            }
            catch (JsonException jsEx)
            {
                throw new NotContentException("Sin datos");
            }
            catch (NotContentException ex)
            {
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(ea.Ea) + JsonSerializer.Serialize(ea.EaRef);
            return res;
        }
        
        public async Task<string> GetReporteProductos(string authorization, int idCentro)
        {
            var res = "";
            int rowcount = 0;
            // var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            // var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            // var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_getReporteProductos", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                cmd.Parameters.Add("@idCentro", SqlDbType.Int).Value = idCentro;
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
                
                if (string.IsNullOrEmpty(res))
                    throw new NotContentException("Sin datos");
                
            }
            catch (JsonException jsEx)
            {
                throw new NotContentException("Sin datos");
            }
            catch (NotContentException ex)
            {
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(ea.Ea) + JsonSerializer.Serialize(ea.EaRef);
            return res;
        }
        
        public async Task<string> GetDocumentacion(string authorization, int idCentro, int idUsuario)
        {
            var res = "";
            int rowcount = 0;
            // var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            // var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            // var idPerson = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;
            
            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_getDocumentacion", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = idPerson;
                cmd.Parameters.Add("@idCentro", SqlDbType.NVarChar).Value = idCentro;
                cmd.Parameters.Add("@idUsuario", SqlDbType.NVarChar).Value = idUsuario;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();
            
                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
            
                await reader.CloseAsync();
                
                if (res == "")
                    throw new NotContentException("Sin datos");
                
            }
            catch (NotContentException ex)
            {   
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            
            //return JsonSerializer.Serialize<TareasOnDemand[]>(JsonSerializer.Deserialize<TareasOnDemand[]>(res));
            return res;
        }
        
        public async Task<string> GetVisibilidad(string authorization, int idCentro, int idVisita)
        {
            var res = "";
            int rowcount = 0;
            // var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            // var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            // var idPerson = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;
            
            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_getVisibilidad", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = idPerson;
                cmd.Parameters.Add("@idCentro", SqlDbType.NVarChar).Value = idCentro;
                cmd.Parameters.Add("@idVisita", SqlDbType.NVarChar).Value = idVisita;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();
            
                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
            
                await reader.CloseAsync();
                
                if (res == "")
                    throw new NotContentException("Sin datos");
                
            }
            catch (NotContentException ex)
            {   
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            
            //return JsonSerializer.Serialize<TareasOnDemand[]>(JsonSerializer.Deserialize<TareasOnDemand[]>(res));
            return res;
        }
        
        public async Task<string> GetRelIncidenciaAccion(string authorization)
        {
            var res = "";
            int rowcount = 0;
            // var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            // var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            // var idPerson = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;
            
            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_getRelIncidenciaAccion", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = idPerson;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();
            
                while (reader.Read())
                {
                    res += reader.GetString(0);
                }
            
                await reader.CloseAsync();
                
                if (res == "")
                    throw new NotContentException("Sin datos");
                
            }
            catch (NotContentException ex)
            {   
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
            
            //return JsonSerializer.Serialize<TareasOnDemand[]>(JsonSerializer.Deserialize<TareasOnDemand[]>(res));
            return res;
        }
        
        
        
        #endregion

        #region [Setters]
        public async Task<object> SetContacto(string authorization, ContactoKimberlyDto contacto)
        {
            var res = "";
            int rowcount = 0;
            var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_setContacto", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@person", SqlDbType.NVarChar).Value = person;
                cmd.Parameters.Add("@paramJson", SqlDbType.NVarChar).Value = JsonSerializer.Serialize(contacto);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetInt32(0);
                }

                await reader.CloseAsync();
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(grupo);
            return res;
        }
        
        public async Task<string> SetFoto(string authorization, FotoKimberlyDto t)
        {
            var res = "";
            int rowcount = 0;
            var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();

            try
            {
                if (t.UrlFoto.Length < 1)
                {
                    throw new Helpers.SqlException("Error");
                }
                if (t.IdFoto == 0)
                {
                    string pixName = F.Normalize(F.Normalize(t.Nombre)
                                                 + "_" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds()
                                                 + ".jpg");

                    //Subir foto...
                    string anio = DateTime.Now.ToLocalTime().ToString("yyyy");
                    string mes = DateTime.Now.ToLocalTime().ToString("MM");
                    Directory.CreateDirectory(@"F:\inetpub\fotos.rpv.es\repositorio\" + repositorio + "\\" + anio + "\\" + mes + "\\");
                    Directory.CreateDirectory(@"F:\inetpub\fotos.rpv.es\repositorio\" + repositorio + "\\thumbs\\" + anio + "\\" + mes + "\\");
                    pixName = anio + "/" + mes + "/" + pixName;
                    MemoryStream msImage = new MemoryStream(Convert.FromBase64String(t.UrlFoto));
                    Bitmap bmpImage = new Bitmap(msImage);
                    Bitmap resizedImage = F.CreateThumbnail(bmpImage, 1200, 900);
                    Bitmap resizedThumb = F.CreateThumbnail(bmpImage, 267, 200);

                    resizedImage.Save(@"F:\inetpub\fotos.rpv.es\repositorio\"+ repositorio + "\\" + pixName, ImageFormat.Jpeg);
                    resizedThumb.Save(@"F:\inetpub\fotos.rpv.es\repositorio\" + repositorio + "\\thumbs\\" + pixName, ImageFormat.Jpeg);

                    resizedThumb.Dispose();
                    resizedImage.Dispose();
                    bmpImage.Dispose();
                    msImage.Close();

                    t.UrlFoto = @"fotos.rpv.es/repositorio/" + repositorio + "/" + pixName;
                    t.Nombre = pixName;

                }
                
                //Subir row...
                SqlCommand cmd = new SqlCommand("sp_app_setFoto", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                cmd.Parameters.Add("@paramJson", SqlDbType.NVarChar).Value = JsonSerializer.Serialize(t);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetString(0);
                }

                await reader.CloseAsync();
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(grupo);
            return res;
        }
        
        public async Task<string> SetFotoCoordenadasCentro(string authorization, FotoCentroKimberlyDto t)
        {
            var res = "";
            int rowcount = 0;
            /*var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;*/

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            
            try
            {
                string pixName = t.IdCentro + ".jpg";

                //Subir foto...
                Directory.CreateDirectory(@"F:\inetpub\fotos.rpv.es\img\Visuales\kimberly\centros\");
                MemoryStream msImage = new MemoryStream(Convert.FromBase64String(t.Base64Foto));
                Bitmap bmpImage = new Bitmap(msImage);
                Bitmap resizedImage = F.CreateThumbnail(bmpImage, 1200, 900);
                resizedImage.Save(@"F:\inetpub\fotos.rpv.es\img\Visuales\kimberly\centros\" + pixName, ImageFormat.Jpeg);
                resizedImage.Dispose();
                bmpImage.Dispose();
                msImage.Close();
                
                //Updatear centro...
                SqlCommand cmd = new SqlCommand("sp_app_setLatitudLongitudCentro", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idCentro", SqlDbType.Int).Value = t.IdCentro;
                cmd.Parameters.Add("@idUser", SqlDbType.Int).Value = t.IdUsuario;
                cmd.Parameters.Add("@direccionCalculada", SqlDbType.NVarChar, Int32.MaxValue).Value = t.DireccionCalculada;
                cmd.Parameters.Add("@latitud", SqlDbType.Float).Value = t.Latitud;
                cmd.Parameters.Add("@longitud", SqlDbType.Float).Value = t.Longitud;
             
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetString(0);
                }

                await reader.CloseAsync();
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(grupo);
            return res;
        }
        
        public async Task<object> SetReporteProducto(string authorization, ReporteProductoKimberlyDto[] reporte)
        {
            var res = "";
            int rowcount = 0;
            // var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            // var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            // var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_setReporteProducto", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                cmd.Parameters.Add("@paramJson", SqlDbType.NVarChar).Value = JsonSerializer.Serialize(reporte);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetInt32(0);
                }

                await reader.CloseAsync();
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(grupo);
            return res;
        }
        
        public async Task<object> SetVisibilidad(string authorization, ReporteVisibilidadKimberlyDto reporte)
        {
            var res = "";
            int rowcount = 0;
            // var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            // var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            // var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_setReporteVisibilidad", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                cmd.Parameters.Add("@paramJson", SqlDbType.NVarChar).Value = JsonSerializer.Serialize(reporte);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetString(0);
                }

                await reader.CloseAsync();
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(grupo);
            return res;
        }
        
        public async Task<object> SetReportePromocion(string authorization, ReportePromocionKimberlyDto[] reporte)
        {
            var res = "";
            int rowcount = 0;
            // var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            // var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            // var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_setReportePromocion", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                cmd.Parameters.Add("@paramJson", SqlDbType.NVarChar).Value = JsonSerializer.Serialize(reporte);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetString(0);
                }

                await reader.CloseAsync();
            
                if (string.IsNullOrEmpty(res))
                    throw new NotContentException("Sin datos");
                    
            }
            catch (JsonException jsEx)
            {
                throw new NotContentException("Sin datos");
            }
            catch (NotContentException ex)
            {
                throw new NotContentException(ex.Message);
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(grupo);
            return res;
        }
        
        public async Task<object> SetReporteIncidencia(string authorization, ReporteIncidenciaKimberlyDto reporte)
        {
            var res = "";
            int rowcount = 0;
            // var tok = authorization.Replace("Bearer ", "").Replace(System.Environment.NewLine, "");
            // var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);
            // var person = jwttoken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;

            var conn = new SqlConnection(_context.ConnKimberly);
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_app_setReporteIncidencia", conn);
                cmd.CommandTimeout = 120;
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.Add("@idPerson", SqlDbType.NVarChar).Value = person;
                cmd.Parameters.Add("@paramJson", SqlDbType.NVarChar).Value = JsonSerializer.Serialize(reporte);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    res += reader.GetString(0);
                }

                await reader.CloseAsync();
            }
            catch (Exception e)
            {
                throw new Helpers.SqlException("Error: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }

            // return JsonSerializer.Serialize(grupo);
            return res;
        }
        
        
        #endregion
        
    }
}