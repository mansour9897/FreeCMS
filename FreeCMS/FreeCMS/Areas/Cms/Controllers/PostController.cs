using AutoMapper;
using FreeCMS.Areas.Cms.ViewModels.Post;
using FreeCMS.Areas.Cms.ViewModels.Topic;
using FreeCMS.DomainModels.Cms;
using FreeCMS.DomainModels.Identity;
using FreeCMS.Service.CMS.Abstraction;
using FreeCMS.Service.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FreeCMS.Areas.Cms.Controllers
{
    [Area("CMS")]
    [Route("CMS/[controller]/[action]")]
    //[WeboAuthorize]
    //[ControllerInfo("مدیریت نوشته ها")]
    [Authorize]
    public class PostController : Controller
    {
        #region variables
        private readonly string _areaName = "CMS";
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ITopicService _topicService;
        private readonly IPostService _postService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        #endregion

        #region constructors

        public PostController(ILogger<PostController> logger, IMapper mapper, ITopicService topicService,
            IPostService postService, IHttpContextAccessor httpContextAccessor,
             UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._topicService = topicService;
            this._postService = postService;
            this._httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        #region actions
        //[ActionInfo("مشاهده همه نوشته ها", "فهرست نوشته ها")]
        public IActionResult Index(int? page)
        {
            var posts = _postService.GetAll().OrderByDescending(t => t.CreationDate);
            int pageSize = 5;
            int pageNumber = page ?? 1;
            return View(posts.ToPagedList(pageNumber, pageSize));
        }

        //[ActionInfo("ایجاد نوشته جدید", "نوشته جدید")]
        public IActionResult Create()
        {
            SetupTopicsSelector(null);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewPost model)
        {
            SetupTopicsSelector(model.SelectedTopics);
            if (ModelState.IsValid)
            {
                //automapper configured to convert metakeywords split character 
                //form '+' to ',' automatically 
                Post post = _mapper.Map<Post>(model);
                //set autorid
                var userId = _httpContextAccessor.HttpContext.User.GetId();
                post.AuthorId = userId;
                Post createdPost = _postService.Add(post);
                //clear all post topics
                _postService.RemovePostFromAllTopics(createdPost.Id);
                //add post to topics
                _postService.AddPostToTopics(createdPost.Id, model.SelectedTopics);

                return RedirectToAction("Index", "Post", new { area = _areaName });
            }
            return View(model);
        }

        //[ActionInfo("مشاهده جزییات نوشته", "مشاهده نوشته")]
        public IActionResult Details(int id)
        {
            Post post = _postService.GetPostWithPostTopics(id);
            Task.Run(async () =>
            {
                post.Author = await _userManager.FindByIdAsync(post.AuthorId);
            });
            

            if (post == null)
            {
                return NotFound();
            }
            SetupTopicsSelector(post.PostTopics.Select(pt => pt.TopicId).ToArray());
            return View(post);
        }

        //[ActionInfo("ویرایش جزییات نوشته", "ویرایش نوشته")]
        public IActionResult Edit(int id)
        {
            Post post = _postService.FindById(id);
            if (post == null)
            {
                return NotFound();
            }
            //automapper configured to convert metakeywords split character 
            //form ',' to '+' automatically
            EditablePost editablePost = _mapper.Map<EditablePost>(post);

            SetupTopicsSelector(post.PostTopics.Select(pt => pt.TopicId).ToArray());
            return View(editablePost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditablePost model)
        {
            SetupTopicsSelector(model.SelectedTopics);
            if (ModelState.IsValid)
            {
                //automapper configured to convert metakeywords split character 
                //form '+' to ',' automatically
                Post post = _mapper.Map<Post>(model);
                _postService.Update(post);
                //clear all post topics
                _postService.RemovePostFromAllTopics(model.Id);
                //add post to topics
                _postService.AddPostToTopics(model.Id, model.SelectedTopics);
                return RedirectToAction("Index", "Post", new { area = _areaName });
            }
            return View(model);
        }

        //[ActionInfo("حذف نوشته", "حذف نوشته")]
        public IActionResult Delete(int id)
        {
            Post post = _postService.GetPostWithPostTopics(id);
            if (post == null)
            {
                return NotFound();
            }
            SetupTopicsSelector(post.PostTopics.Select(pt => pt.TopicId).ToArray());
            return View(post);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            Post post = _postService.FindById(id);
            if (post == null)
                return NotFound();
            //clear all post topics
            _postService.RemovePostFromAllTopics(id);
            _postService.Delete(post);
            return RedirectToAction("Index", "Post", new { area = _areaName });
        }
        #endregion

        #region methods 
        private void SetupTopicsSelector(int[] selectedTopics)
        {
            ViewBag.TopicsList = _topicService.GetRootTopics()
                .Select(rt => ConvertTopicToCheckbox(selectedTopics, rt))
                .ToList();
        }

        private TopicCheckbox ConvertTopicToCheckbox(int[] selectedTopics, Topic topic)
        {
            var topicCheckbox = new TopicCheckbox
            {
                Id = topic.Id,
                Name = topic.Name,
                IsSelected = selectedTopics == null ? false : selectedTopics.Contains(topic.Id),
            };
            if (topic.Children is not null) topicCheckbox.Children = topic.Children.Select(c => ConvertTopicToCheckbox(selectedTopics, c)).ToList();
            
            return topicCheckbox;
        }
        #endregion
    }
}
