using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models;
using Romain_Gauthier.Web.Infrastructure;

namespace Romain_Gauthier.Web.Controllers.API
{
    /// <summary>
    /// 提交答案
    /// </summary>
    public class SubmitAnswersController : BaseApiController
    {
        
        private readonly IPersonnelService _personnelService;
        public SubmitAnswersController( IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }

        public object Post(TrainAnswerModel[] models)
        {
            var openId = HttpContext.Current.User.Identity.Name;
            var currentUser = _personnelService.GetPersonnelByOpenId(openId);
            var answerIds = models.Select(n => n.Id).ToArray();
            if (currentUser.TrainAnswers.Any(p => answerIds.Contains(p.Id)))
            {
                return Failed("提交失败，您已提交过此次培训测评！");
            }
            var answers = _personnelService.GetTrainAnswers().Where(n => answerIds.Contains(n.Id)).ToArray();
            var score = answers.Where(n => n.IsCorrect).Sum(p => p.TrainQuestion.Score);
            foreach (var item in answers)
            {
                currentUser.TrainAnswers.Add(item);
            }
            _personnelService.Update();
            return Success(string.Format("提交成功！您的此次测评分数为:{0}", score));          
        }
    }
}
