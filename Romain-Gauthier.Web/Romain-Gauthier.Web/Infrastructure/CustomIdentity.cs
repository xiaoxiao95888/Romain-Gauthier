using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Romain_Gauthier.Web.Models;

namespace Romain_Gauthier.Web.Infrastructure
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }

        public LoginViewModel LoginViewModel { get; }

        public string Name => LoginViewModel == null ? "" : LoginViewModel.Email;

        public string AuthenticationType => "Custom";

        public bool IsAuthenticated => LoginViewModel != null;
    }
}