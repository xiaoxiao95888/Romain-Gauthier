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
using WebGrease.Css.Extensions;

namespace Romain_Gauthier.Web.Controllers.API
{
    public class TrainQuestionController : BaseApiController
    {
        private readonly ITrainQuestionService _trainQuestionService;
        public TrainQuestionController(ITrainQuestionService trainQuestionService)
        {
            _trainQuestionService = trainQuestionService;
        }

        public object Get()
        {
            var trainArticleId = HttpContext.Current.Request["TrainArticleId"];
            var models = _trainQuestionService.GetTrainQuestions();
            if (!string.IsNullOrEmpty(trainArticleId))
            {
                var id = new Guid(trainArticleId);
                models = models.Where(n => n.TrainArticleId == id);
            }
            var model = models.ToArray().OrderBy(n => n.TrainArticleId).Select(n => new TrainQuestionModel
            {
                Id = n.Id,
                Question = n.Question,
                Score = n.Score,
                TrainArticleName=n.TrainArticle.Title,
                TrainAnswerModels = n.TrainAnswers.Where(p=>!p.IsDeleted).Select(p => new TrainAnswerModel
                {
                    Id = p.Id,
                    Answer = p.Answer,
                    IsCorrect = p.IsCorrect,
                    TrainQuestionId = n.Id
                }).ToArray(),
                TrainArticleId = n.TrainArticleId
            }).ToArray();
            return model;
        }

        public object Post(TrainQuestionModel model)
        {
            if (string.IsNullOrEmpty(model.Question))
            {
                return Failed("问题不能为空");
            }
            if (model.Score == 0)
            {
                return Failed("分数不能为空");
            }
            _trainQuestionService.Insert(new TrainQuestion
            {
                Id = Guid.NewGuid(),
                Question = model.Question,
                Score = model.Score,
                TrainArticleId = model.TrainArticleId
            });
            return Success();
        }

        public object Put(TrainQuestionModel model)
        {
            if (string.IsNullOrEmpty(model.Question))
            {
                return Failed("问题不能为空");
            }
            if (model.Score == 0)
            {
                return Failed("分数不能为空");
            }
            var item = _trainQuestionService.GetTrainQuestion(model.Id);
            if (item == null || item.IsDeleted)
            {
                return Failed("找不到该问题");
            }
            item.Question = model.Question;
            item.Score = model.Score;
            _trainQuestionService.Update();
            return Success();
        }

        public object Delete(Guid id)
        {
            var item = _trainQuestionService.GetTrainQuestion(id);
            if (item == null || item.IsDeleted)
            {
                return Failed("找不到该问题");
            }
            item.IsDeleted = true;
            item.TrainAnswers.ForEach(n => n.IsDeleted = true);
            _trainQuestionService.Update();
            return Success();
        }
    }
}
