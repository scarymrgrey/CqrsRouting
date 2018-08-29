﻿using System;
using System.Security.Claims;
using System.Text;
using Incoding.CQRS;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using CQRS.Block;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Operations.Entities;


namespace Operations.Queries
{
    public class SignInQuery : QueryBase<Object>
    {
        [BindingBehavior(BindingBehavior.Never)]
        public string UserName { get; set; }
        public string Password { get; set; }
        protected override Object ExecuteResult()
        {
            var user = Repository.Query(whereSpecification: new User.Where.ByCredentials(UserName, Password))
                .FirstOrDefault();

            if (user == null)
                throw new InvalidParametersException( "Username or password is incorrect" );

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Config["jwt_secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                Id = user.Id,
                Username = user.Login,
                Token = tokenString
            }; 
        }
    }
}