using AutoMapper;
using FreeCMS.DomainModels.Identity;
using FreeCMS.Service.CMS.Abstraction;
using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FreeCMS.Areas.Cms.Controllers
{
	public class HomeController : Controller
	{
		#region variables
		private readonly ILogger _logger;
		private readonly IMapper _mapper;
		private readonly IPostService _postService;
		private readonly ISystemObjectService _systemObjectService;
		private readonly ICommentService _commentService;
		private readonly ITopicService _topicService;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IPageService _pageService;
		private readonly string _objectName = "Post";
		private readonly string _pluginName = "WeboCms";
		#endregion

		#region constrcutors
		public HomeController(ILogger<HomeController> logger, IMapper mapper, IPostService postService,
			ISystemObjectService systemObjectService, ICommentService commentService, ITopicService topicService,
			UserManager<ApplicationUser> userManager, IPageService pageService)
		{
			_logger = logger;
			_mapper = mapper;
			_postService = postService;
			_systemObjectService = systemObjectService;
			_commentService = commentService;
			_topicService = topicService;
			_userManager = userManager;
			_pageService = pageService;
		}
		#endregion

		#region actions
		public IActionResult Index(int? page)
		{
			var posts = _postService.GetPublishedPosts().OrderByDescending(t => t.CreationDate);
			int pageSize = 10;
			int pageNumber = page ?? 1;
			return View(posts.ToPagedList(pageNumber, pageSize));
		}
		public IActionResult Post(int id)
		{
			var post = _postService.FindById(id);
			if (post == null || post.IsPublished == false)
				return NotFound();
			//increase post view
			post.CountView++;
			_postService.Update(post);
			SetupCommentData(id);
			return View(post);
		}
		public IActionResult Category(int id, int? page)
		{
			var category = _topicService.FindById(id);
			if (category == null)
				return NotFound();
			ViewBag.CategoryId = id;
			ViewBag.CategoryName = category.Name;
			ViewBag.CategoryDescription = category.Description;
			ViewBag.HasSubCategory = category.Children.Count == 0 ? false : true;
			var posts = _postService.GetPostsByTopicId(id, true).OrderByDescending(t => t.CreationDate);
			int pageSize = 10;
			int pageNumber = page ?? 1;
			return View(posts.ToPagedList(pageNumber, pageSize));
		}

		public IActionResult CategoryName(string name, int? page)
		{
			var category = _topicService.FindByName(name);
			if (category == null)
				return NotFound();
			ViewBag.CategoryId = category.Id;
			ViewBag.CategoryName = category.Name;
			ViewBag.CategoryDescription = category.Description;
			ViewBag.HasSubCategory = category.Children.Count == 0 ? false : true;
			var posts = _postService.GetPostsByTopicId(category.Id, true).OrderByDescending(t => t.CreationDate);
			int pageSize = 10;
			int pageNumber = page ?? 1;
			return View("Category", posts.ToPagedList(pageNumber, pageSize));

		}

		public IActionResult Author(int id, int? page)
		{
			var author = _userManager.FindByIdAsync(id.ToString()).Result;
			if (author == null)
				return NotFound();
			ViewBag.AuthorId = id;
			ViewBag.AuthorName = author.FullName;
			var posts = _postService.GetPostsByAuthorId(id, true).OrderByDescending(t => t.CreationDate);
			int pageSize = 10;
			int pageNumber = page ?? 1;
			return View(posts.ToPagedList(pageNumber, pageSize));
		}
		public IActionResult Search(int? page, string query)
		{
			ViewBag.Query = query;
			var posts = _postService.Search(query);
			int pageSize = 10;
			int pageNumber = page ?? 1;
			return View(posts.ToPagedList(pageNumber, pageSize));
		}
		public IActionResult Page(int id)
		{
			var page = _pageService.FindById(id);
			if (page == null)
			{
				return NotFound();
			}
			return View(page);
		}
		public IActionResult PageByTitle(string id)
		{
			var page = _pageService.FindByTitle(id);
			if (page == null)
			{
				return NotFound();
			}
			return View(page);
		}
		#endregion

		#region  methods
		public void SetupCommentData(int postId)
		{
			int sysObjId = _systemObjectService.GetByName(_objectName, _pluginName).Id;
			ViewBag.SystemObjectId = sysObjId;
			ViewBag.CommentsCount = _commentService.GetAllByRecordId(sysObjId, postId.ToString(), true).Count;
		}
		#endregion
	}
}
