using Coling.Repositorio.Contratos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Repositorio.Implementacion
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IConfiguration configuration;

        public UsuarioRepositorio(IConfiguration configuration) {
            this.configuration = configuration;
        }

        //public Task<string> DesencriptarPassword(string password)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<string> EncriptarPassword(string password)
        {
            string Encriptado = "";
            using (SHA256 sh256Hash= SHA256.Create())
            {
                byte[] bytes = sh256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                Encriptado = Convert.ToBase64String(bytes);

            }
            return Encriptado;
        }

        public async Task<ITokenData> CounstruirToken(string username, string password)
        {
            var claims = new List<Claim>()
            {
               new Claim("usuario",username),
               new Claim("rol","Admin"),
               new Claim("estado","Activo")

            };
            var secretkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["LaveSecreta"] ?? ""));
            TokenData tokenData = new TokenData();
            return tokenData;
        }

        public Task<bool> ValidarToken(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerificarCredenciales(string usuariox, string passwordx)
        {
            bool sw = false;
            string passEncriptado = await EncriptarPassword(passwordx);
            string consulta = "select count(idusuario) from Usuario where nombreuser='" + usuariox + "' and password='" + passwordx + "'";
            int Existe = conexion.EjecutarEscalar(consulta);
            if (Existe >0)
            {
                sw = true;
            }
            return sw;
        }

       
    }
}
