﻿using System;
using System.Linq.Expressions;
using Incoding;
using Incoding.Data;

namespace Operations.Entities
{
    public class User : EntityBase
    {
        public string Login { get; set; }
        public string Pwd { get; set; }

        public abstract class Where
        {
            public class ByCredentials : Specification<User>
            {
                private readonly string _username;
                private readonly string _userpassword;

                public ByCredentials(string username, string userpassword)
                {
                    _username = username;
                    _userpassword = userpassword;
                }
                public override Expression<Func<User, bool>> IsSatisfiedBy()
                {
                    return r => r.Pwd == _userpassword && r.Login == _username;
                }
            }
        }
    }
}
