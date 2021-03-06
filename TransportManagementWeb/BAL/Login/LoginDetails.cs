﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using TransportManagementWeb.Global;
using System.Data.Entity;

namespace TransportManagementWeb.BAL.Login
{
    public class LoginDetails
    {
        TransportManagementEntities _db = null;

        /// <summary>
        /// Get Authenticate User credentials
        /// </summary>
        /// <param name="UserName">Username</param>
        /// <param name="Password">Password</param>
        /// <returns>Enums</returns>
        public Enums.LoginMessage GetLogin(string UserName, string Password)
        {
            //string _passwordHash = Utility.GetHashString(Password);
            _db = new TransportManagementEntities();

            var _userRow = _db.UserDetails.Where(x => x.UserName.Equals(UserName) && x.Password.Equals(Password)).FirstOrDefault();

            if (_userRow != null)
            {
                UserData.UserId = _userRow.UserId;
                UserData.Username = _userRow.UserName;
                UserData.FirstName = _userRow.Name;
                UserData.MiddleName = _userRow.EmailId;
                return Enums.LoginMessage.Authenticated;
            }
            else
                return Enums.LoginMessage.InvalidCreadential;
        }
    }
}