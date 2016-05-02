using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class TrainingScoringController : BaseApiController
    {
        private readonly IPersonnelService _personnelService;
        public TrainingScoringController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }
        public object Get()
        {
            var detailmodel = _personnelService.GetPersonnels().Where(n => n.TrainAnswers.Any()).SelectMany(n => n.TrainAnswers, (person, answer) => new { p = person, a = answer }).ToArray().Select(n => new TrainingScoringDetailModel
            {
                PersonnelModel = new PersonnelModel
                {
                    Id = n.p.Id,
                    Name = n.p.Name,
                    PhoneNum = n.p.PhoneNum,
                    NickName = n.p.NickName,
                },
                TrainArticleId = n.a.TrainQuestion.TrainArticleId,
                TrainArticleTitle = n.a.TrainQuestion.TrainArticle.Title,
                Question = n.a.TrainQuestion.Question,
                Answer = n.a.Answer,
                IsCorrect = n.a.IsCorrect,
                Score = n.a.IsCorrect ? n.a.TrainQuestion.Score : 0,
            }).OrderBy(n => n.TrainArticleId).ThenBy(n => n.PersonnelModel.Id).ThenBy(n => n.IsCorrect).ToArray();
            var model = detailmodel.GroupBy(n => new { n.PersonnelModel.Id, n.TrainArticleId }).Select(n => new
            {
                PersonnelModel = n.Select(p => p.PersonnelModel).Where(p => p.Id == n.Key.Id).FirstOrDefault(),
                TrainArticleId = n.Key.TrainArticleId,
                TrainArticleTitle = n.Where(p => p.TrainArticleId == n.Key.TrainArticleId).Select(p => p.TrainArticleTitle).FirstOrDefault(),
                SumScore = n.Sum(p => p.IsCorrect ? p.Score : 0),
                Details = n
            }).ToArray();
            return model;
        }
    }
}
