using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models;
using Romain_Gauthier.Web.Models.Enum;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class PersonnelController : BaseApiController
    {
        private readonly IPersonnelService _personnelService;
        public PersonnelController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }

        public object Get()
        {
            var models = _personnelService.GetPersonnels().OrderByDescending(n => n.Name).Select(n => new PersonnelModel
            {
                City = n.City,
                Country = n.Country,
                Email = n.Email,
                Gender = (Gender)n.Gender,
                Headimgurl = n.Headimgurl,
                Id = n.Id,
                Language = n.Language,
                License = n.License,
                Name = n.Name,
                NickName = n.NickName,
                Province = n.Province,
                UpdateTime = n.UpdateTime,
                PhoneNum = n.PhoneNum
            }).ToArray();
            return models;
        }

        public object Post(PersonnelModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return Failed("email不能为空");
            }
            if (string.IsNullOrEmpty(model.PhoneNum))
            {
                return Failed("手机不能为空");
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                return Failed("姓名不能为空");
            }
            model.Email = model.Email.Trim();
            model.Name = model.Name.Trim();
            model.PhoneNum = model.PhoneNum.Trim();
            if (_personnelService.GetPersonnels().Any(n => n.PhoneNum == model.PhoneNum))
            {
                return Failed("该手机号码已存在");
            }
            if (_personnelService.GetPersonnels().Any(n => n.Email == model.Email))
            {
                return Failed("该email已存在");
            }
            var allLicense = _personnelService.GetLicense().ToArray();
            string license;
            var constant = new[]
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U",
                "V", "W", "X", "Y", "Z"
            };
            var rand = new Random();
            while (true)
            {
                var str = constant[rand.Next(0, 23)] + rand.Next(1, 9) +
                          constant[rand.Next(0, 23)] + rand.Next(1, 9) + constant[rand.Next(0, 23)] + rand.Next(1, 9);
                if (allLicense.All(n => n != str))
                {
                    license = str;
                    break;
                }
            }
            _personnelService.Insert(new Personnel
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Email = model.Email,
                PhoneNum = model.PhoneNum,
                License = license
            });
            return Success();
        }

        public object Put(PersonnelModel model)
        {
            var item = _personnelService.GetPersonnel(model.Id);
            if (item == null)
            {
                return Failed("找不人员");
            }
            if (item.IsDeleted)
            {
                return Failed("人员已被删除");
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                return Failed("email不能为空");
            }
            if (string.IsNullOrEmpty(model.PhoneNum))
            {
                return Failed("手机不能为空");
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                return Failed("姓名不能为空");
            }
            model.Email = model.Email.Trim();
            model.Name = model.Name.Trim();
            model.PhoneNum = model.PhoneNum.Trim();
            if (_personnelService.GetPersonnels().Any(n => n.PhoneNum == model.PhoneNum && n.Id != item.Id))
            {
                return Failed("该手机号码已存在");
            }
            if (_personnelService.GetPersonnels().Any(n => n.Email == model.Email && n.Id != item.Id))
            {
                return Failed("该email已存在");
            }
            item.Email = model.Email;
            item.Name = model.Name;
            item.PhoneNum = model.PhoneNum;
            _personnelService.Update();
            return Success();
        }

        public object Delete(Guid id)
        {
            var item = _personnelService.GetPersonnel(id);
            if (item == null)
            {
                return Failed("找不人员");
            }
            item.IsDeleted = true;
            _personnelService.Update();
            return Success();
        }
    }
}
