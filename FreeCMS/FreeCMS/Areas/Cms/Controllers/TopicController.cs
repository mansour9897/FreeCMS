using AutoMapper;
using FreeCMS.Areas.Cms.ViewModels.Topic;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Service.CMS.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace FreeCMS.Areas.Cms.Controllers
{
	[Area("CMS")]
	[Route("CMS/[controller]/[action]")]
	//[WeboAuthorize]
	//[ControllerInfo("مدیریت دسته ها")]
	public class TopicController : Controller
	{
		#region variables
		private readonly string _areaName = "CMS";
		private readonly ITopicService _topicService;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;
		#endregion

		#region construtors
		public TopicController(ITopicService topicService, IMapper mapper, ILogger<TopicController> logger)
		{
			this._topicService = topicService;
			this._mapper = mapper;
			this._logger = logger;
		}
		#endregion

		#region actions
		//[ActionInfo("مشاهده همه دسته ها", "فهرست دسته ها")]
		public IActionResult Index(int? page)
		{
			var topics = _topicService.GetAll().OrderByDescending(t => t.CreationDate);
			int pageSize = 5;
			int pageNumber = page ?? 1;
			return View(topics.ToPagedList(pageNumber, pageSize));
		}
		 
				//[ActionInfo("ایجاد دسته جید", "ایجاد دسته")]
		public IActionResult Create()
		{
			SetupParentList(null);
			return View();
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(NewTopic model)
		{
			SetupParentList(model.ParentId);
			if (ModelState.IsValid)
			{
				if (_topicService.TopicExists(model.Name))
				{
					ModelState.AddModelError("Name", "دسته ای با این  نام وجود دارد.");
					return View(model);
				}
				var topic = _mapper.Map<Topic>(model);
				_topicService.Add(topic);
				return RedirectToAction("Index", "Topic", new { area = _areaName });
			}
			return View(model);
		}
		
		//[ActionInfo("مشاهده جزییات دسته", "جزییات دسته")]
		public IActionResult Details(int id)
		{
			var topic = _topicService.FindById(id);
			if (topic == null)
				return NotFound();
			return View(topic);
		}

		//[ActionInfo("ویرایش اطلاعات دسته", "ویرایش دسته")]
		public IActionResult Edit(int id)
		{
			var topic = _topicService.FindById(id);
			if (topic == null)
				return NotFound();
			var editableTopic = _mapper.Map<EditableTopic>(topic);
			SetupEditableParentList(topic.Id, topic.ParentId);
			return View(editableTopic);
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(EditableTopic model)
		{
			if (ModelState.IsValid)
			{
				if (_topicService.TopicExists(model.Name, model.Id))
				{
					ModelState.AddModelError("Name", "دسته ای با این  نام وجود دارد.");
					SetupEditableParentList(model.Id, model.ParentId);
					return View(model);
				}
				var topic = _mapper.Map<Topic>(model);
				_topicService.Update(topic);
				return RedirectToAction("Index", "Topic", new { area = _areaName });
			}
			SetupEditableParentList(model.Id, model.ParentId);
			return View(model);
		}
		
		//[ActionInfo("حذف دسته", "حذف دسته")]
		public IActionResult Delete(int id)
		{
			var topic = _topicService.FindById(id);
			if (topic == null)
				return NotFound();
			return View(topic);
		}
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult ConfirmDelete(int id)
		{
			var topic = _topicService.FindById(id);
			if (topic == null)
				return NotFound();
			_topicService.Delete(topic);
			return RedirectToAction("Index", "Topic", new { area = _areaName });
		}
		#endregion

		#region methods
		private void SetupParentList(int? parentId)
		{
			ViewBag.ParentId = new SelectList(_topicService.GetAll(), "Id", "Name", parentId);
		}
		private void SetupEditableParentList(int topicId, int? parentId)
		{
			ViewBag.ParentId = new SelectList(_topicService.GetTopicsExpectChildren(topicId), "Id", "Name", parentId);
		}
		#endregion
	}
}
