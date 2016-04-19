using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Romain_Gauthier.Web.Models.Enum;

namespace Romain_Gauthier.Web.Models
{
    public class PersonnelModel
    {
        public Guid Id { get; set; }
        public string License { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string OpenId { get; set; }
        public string NickName { get; set; }
        public Gender? Gender { get; set; }

        public string GenderStr
        {
            get { return Gender != null ? Gender.ToString() : string.Empty; }
        }

        public string Language { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Headimgurl { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}