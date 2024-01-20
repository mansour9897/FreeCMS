using FreeCMS.DomainModels.System;
using FreeCMS.Extensions.Attributes;
using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
namespace FreeCMS.Areas.Core.Controllers
{
    [Area("Core")]
    [Route("Core/[controller]/[action]")]
    [Authorize]
    [ControllerInfo("مدیریت دیدگاه ها","سیستم")]
    public class CommentController:Controller
    {
        #region variables ...
        private readonly ICommentService _commentService;
        #endregion

        #region constructors ...
        public CommentController(ICommentService commentService)
        {
            this._commentService = commentService;
        }
        #endregion

        #region actions ...
        [ActionInfo("فهرست دیدگاه ها","مشاهده همه دیدگاه های موجود")]
        public IActionResult Index(int? page)
        {
            var comments = _commentService.GetAll().OrderByDescending(c => c.CreateDate).ToList();
            int pageSize = 5;
            int pageNumber = page ?? 1;
            return View(comments.ToPagedList(pageNumber,pageSize));
        }
        [ActionInfo("مشاهده دیدگاه","مشاهده جزییات یک دیدگاه")]
        public IActionResult Details(int id)
        {
            Comment comment = _commentService.FindById(id);
            if(comment is null)
            {
                return NotFound();
            }
            return View(comment);
        }
        [ActionInfo("حذف دیدگاه","حذف دیدگاه")]
        public IActionResult Delete(int id)
        {
            Comment comment = _commentService.FindById(id);
            if(comment is null)
            {
                return NotFound();
            }
            return View(comment);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            _commentService.Delete(id);
            return RedirectToAction("Index","Comment",new { area="Core"});
        }
        [HttpPost]
        public IActionResult VerifyComment(int id)
        {
            Comment comment = _commentService.FindById(id);
            if(comment != null)
            {
                comment.IsVerified = true;
                _commentService.Update(comment);
            }
            return RedirectToAction("Details","Comment",new { area="Core",id=id});
        }
        [HttpPost]
        public IActionResult RejectComment(int id)
        {
            Comment comment = _commentService.FindById(id);
            if(comment != null)
            {
                comment.IsVerified = false;
                _commentService.Update(comment);
            }
            return RedirectToAction("Details","Comment",new { area="Core",id=id});
        }
        #endregion

    }
}