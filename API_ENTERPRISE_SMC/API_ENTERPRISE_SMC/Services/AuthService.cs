using API_ENTERPRISE_SMC.Extensions;
using API_ENTERPRISE_SMC.Models;
using API_ENTERPRISE_SMC.Models.RequestModels;
using API_ENTERPRISE_SMC.Models.ResponsModels;
using API_ENTERPRISE_SMC.Repository.Interfaces;
using API_ENTERPRISE_SMC.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API_ENTERPRISE_SMC.Services
{
    public class AuthService : IAuthService
    {
  
        public IAuthRepository AuthRepo;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository AuthRepo, IConfiguration configuration)
        {
            this.AuthRepo = AuthRepo;
            this._configuration = configuration;
        }
        public async Task<ResponsAuth> ResponsToken(RequestAuthUser uss)
        {
            var success = false;
            var token = "";
            //Se obtiene el objetop AuthUser
            var user = await this.AuthRepo.GetAccess(uss);
            //Objeto respuesta
            ResponsAuth obj = new ResponsAuth();

            try
            {
                //Se valida el acceso para validar el success y crear el token
                if (user.Items.ElementAt(0).acceso == true)
                {
                    success = true;
                    token = GenerateToken(user.Items.ElementAt(0));
                }
                else {
                    token = "Error de acceso";
                }

                //Se alamacena el objeto AuthUser
                obj.success = success;
                obj.token = token;
                obj.user = user.Items.ElementAt(0);

                return obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return obj;
        }

        public string GenerateToken(AuthUser uss)
        {

            // Leemos el secret_key desde nuestro appseting
            var secretKey = _configuration.GetValue<string>("SecretKey");
            //Si se quiere agregar complejidad a la secret key se hara aqui:
            //

            //
            var key = Encoding.ASCII.GetBytes(secretKey);

            // Creamos el objeto UserData (Datos que persisten en el token)
            var UserData = new
            {
                id = uss.id,
                userName = uss.userName,
                lastName = uss.lastName,
                name = uss.name,
                administrator = uss.administrator,
                rol = uss.rol
            };
            // Creamos los claims (pertenencias, características) del usuario
            var claims = new ClaimsIdentity(new[]
            {
                new Claim("UserData", JsonConvert.SerializeObject(UserData))
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                // Nuestro token va a durar un día
                Expires = DateTime.UtcNow.AddDays(1),
                // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);

        }

        public async Task<QueryResult<ResponsBranch>> GetBranches()
        {
            var sedes = await this.AuthRepo.GetBranches();
            return sedes;
        }
    }
}
