﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TRMDestopUI.Library.Models;

namespace TRMDestopUI.Library.Api
{
    public class UserEndpoint : IUserEndpoint
    {
        private readonly IAPIHelper _aPIHelper;

        public UserEndpoint(IAPIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }

        public async Task<List<UserModel>> GetAll()
        {
            using (HttpResponseMessage response = await _aPIHelper.ApiClient.GetAsync("/api/User/Admin/GetAllUsers"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<UserModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<Dictionary<string, string>> GetAllRoles()
        {
            using (HttpResponseMessage response = await _aPIHelper.ApiClient.GetAsync("/api/User/Admin/GetAllRoles"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Dictionary<string, string>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task AddUserToRole(string UserId, string RoleName)
        {
            var data = new { UserId, RoleName };

            using (HttpResponseMessage response = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/User/Admin/AddRole", data))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public async Task RemoveUserFromRole(string UserId, string RoleName)
        {
            var data = new { UserId, RoleName };

            using (HttpResponseMessage response = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/User/Admin/RemoveRole", data))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
