﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;

namespace Romain_Gauthier.Service.Services
{
    public class PersonnelService : BaseService, IPersonnelService
    {
        public PersonnelService(RomainGauthierDataContext dbContext)
            : base(dbContext)
        {
        }

        public Personnel GetPersonnel(Guid id)
        {
            return DbContext.Personnels.FirstOrDefault(n => n.Id == id);
        }

        public Personnel GetPersonnel(string id)
        {
            try
            {
                var _id = new Guid(id);
                return GetPersonnel(_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<Personnel> GetPersonnels()
        {
            return DbContext.Personnels.Where(n => !n.IsDeleted);
        }

        public IQueryable<string> GetLicense()
        {
            return DbContext.Personnels.Select(n => n.License);
        }

        public void Insert(Personnel personnel)
        {
            DbContext.Personnels.Add(personnel);
            Update();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }

        public IQueryable<PersonnelGroup> GetPersonnelGroups()
        {
            return DbContext.PersonnelGroups.Where(n => !n.IsDeleted);
        }

        public IQueryable<TrainAnswer> GetTrainAnswers()
        {
            return DbContext.TrainAnswers.Where(n => !n.IsDeleted);
        }

        public Personnel GetPersonnelByOpenId(string openId)
        {
            return DbContext.Personnels.FirstOrDefault(n => n.OpenId == openId);
        }
    }
}
