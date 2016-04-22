using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Romain_Gauthier.Library.Models;
using Romain_Gauthier.Library.Services;
using Romain_Gauthier.Web.Models;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class TrainAnswerController : BaseApiController
    {
        private readonly ITrainAnswerService _trainAnswerService;
        public TrainAnswerController(ITrainAnswerService trainAnswerService)
        {
            _trainAnswerService = trainAnswerService;
        }

        public object Get()
        {
            var qid = HttpContext.Current.Request["TrainQuestionId"];
            var models = _trainAnswerService.GetTrainAnswers();
            if (!string.IsNullOrEmpty(qid))
            {
                var id= new Guid(qid);
                models = models.Where(n => n.TrainQuestionId == id);
            }
            return models.Select(n => new TrainAnswerModel
            {
                Answer = n.Answer,
                Id = n.Id,
                IsCorrect = n.IsCorrect,
                TrainQuestionId = n.TrainQuestionId
            }).ToArray();
        }

        public object Post(TrainAnswerModel model)
        {
            if (string.IsNullOrEmpty(model.Answer))
            {
                return Failed("答案不能为空");
            }
            _trainAnswerService.Insert(new TrainAnswer
            {
                Id = Guid.NewGuid(),
                Answer = model.Answer,
                IsCorrect = model.IsCorrect,
                TrainQuestionId = model.TrainQuestionId
            });
            return Success();
        }

        public object Put(TrainAnswerModel model)
        {
            if (string.IsNullOrEmpty(model.Answer))
            {
                return Failed("答案不能为空");
            }
            var item = _trainAnswerService.GetTrainAnswer(model.Id);
            if (item == null)
            {
                return Failed("找不到答案");
            }
            item.IsCorrect = model.IsCorrect;
            item.Answer = model.Answer;
            _trainAnswerService.Update();
            return Success();
        }

        public object Delete(Guid id)
        {
            var item = _trainAnswerService.GetTrainAnswer(id);
            if (item == null)
            {
                return Failed("找不到答案");
            }
            item.IsDeleted = true;
            _trainAnswerService.Update();
            return Success();
        }
    }
}
