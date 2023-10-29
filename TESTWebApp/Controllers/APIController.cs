using Microsoft.AspNetCore.Mvc;
using TESTWebApp.UseCase.MajorWorkItems.Queries;
using TESTWebApp.UseCase.MiddleWorkItems.Queries;
using TESTWebApp.UseCase.MinorWorkItems.Queries;

// 参考：https://learn.microsoft.com/ja-jp/aspnet/core/mvc/controllers/routing?view=aspnetcore-7.0

namespace TESTWebApp.Controllers
{
    public class APIController : Controller
    {
        private readonly IMajorWorkItemQueryService _majorWorkItemQueryService;
        private readonly IMiddleWorkItemQueryService _middleWorkItemQueryService;
        private readonly IMinorWorkItemQueryService _minorWorkItemQueryService;

        public APIController(
            IMajorWorkItemQueryService majorWorkItemQueryService,
            IMiddleWorkItemQueryService middleWorkItemQueryService,
            IMinorWorkItemQueryService minorWorkItemQueryService) 
        { 
            _majorWorkItemQueryService = majorWorkItemQueryService;
            _middleWorkItemQueryService = middleWorkItemQueryService;
            _minorWorkItemQueryService = minorWorkItemQueryService;
        }

        [HttpGet]
        public ActionResult GetAllMajorWorkItems()
        {
            var responseData = _majorWorkItemQueryService.GetAllMajorWorkItem();
            return Json(responseData);
        }

        [HttpGet]
        public ActionResult GetMiddleWorkItems(string id)
        {
            var responseData = _middleWorkItemQueryService.GetAllMiddleWorkItem(id);
            return Json(responseData);
        }

        [HttpGet]
        public ActionResult GetMinorWorkItems(string id)
        {
            var responseData = _minorWorkItemQueryService.GetAllMinorWorkItem(id);
            return Json(responseData);
        }

        [HttpGet]
        [Route("api/majorWorkItems")]
        public ActionResult GetAllMajorWorkItemsV2()
        {
            var responseData = _majorWorkItemQueryService.GetAllMajorWorkItem();
            return Json(responseData);
        }
    }
}
