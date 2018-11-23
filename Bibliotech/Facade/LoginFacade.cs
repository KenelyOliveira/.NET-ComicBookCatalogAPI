using Bibliotech.Configuration;
using Bibliotech.Models;
using Bibliotech.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Bibliotech.Facade
{
    public class LoginFacade
    {
        #region Injecao

        private readonly UsuarioRepository _repoUsuario;
        private readonly SigningConfigurations _configLogin;
        private readonly TokenConfigurations _configToken;

        public LoginFacade(UsuarioRepository repoUsuario, SigningConfigurations configLogin, TokenConfigurations configToken) {
            _repoUsuario = repoUsuario;
            _configLogin = configLogin;
            _configToken = configToken;
        }

        #endregion

        public AutenticacaoUsuario Login(Usuario usuario)
        {
            bool credenciaisValidas = false;
            if (usuario != null && !string.IsNullOrWhiteSpace(usuario.Login))
            {
                var usuarioBase = _repoUsuario.GetUsuario(usuario);
                credenciaisValidas = (usuarioBase != null &&
                    usuario.Id == usuarioBase.Id &&
                    usuario.Nome == usuarioBase.Nome);
            }

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuario.Login, "Login"),
                    new[] {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Login)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(_configToken.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _configToken.Issuer,
                    Audience = _configToken.Audience,
                    SigningCredentials = _configLogin.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new AutenticacaoUsuario() {
                    IsAutenticado = true,
                    DataCriacao = dataCriacao,
                    DataExpiracao = dataExpiracao,
                    Token = token,
                    Mensagem = "OK"
                };
            }
            else
            {
                return new AutenticacaoUsuario()
                {
                    IsAutenticado = false,
                    Mensagem = "Falha ao autenticar!"
                };
            }
        }
    }
}
