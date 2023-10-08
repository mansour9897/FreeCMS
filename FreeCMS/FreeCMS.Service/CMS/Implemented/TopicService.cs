using FreeCMS.Common.Service;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Repository.CMS.Abstraction;
using FreeCMS.Service.CMS.Abstraction;

namespace FreeCMS.Service.CMS.Implemented
{
	public class TopicService : BaseService<Topic, int>, ITopicService
	{
		private readonly ITopicRepository _repo;
		public TopicService(ITopicRepository repository)
			: base(repository)
		{
			this._repo = repository;
		}

		public override Topic Add(Topic topic)
		{
			topic.CreationDate = DateTime.Now;
			topic.LastModified = DateTime.Now;
			return base.Add(topic);
		}
		public override bool Update(Topic topic)
		{
			topic.LastModified = DateTime.Now;
			return base.Update(topic);
		}
		public IList<Topic> GetRootTopics()
		{
			return _repo.List(t => t.ParentId == null).ToList();
		}
		public IList<Topic> GetTopicsExpectChildren(int id)
		{
			//get all topic expect the target topic
			IList<Topic> allTopics = this.List(t => t.Id != id);
			//for avoiding tracking problem use this syntax
			foreach (var child in this.List(t => t.Id == id).FirstOrDefault().Children)
			{
				RemoveChildren(allTopics, child);
			}
			return allTopics.ToList();
		}
		public bool TopicExists(string name)
		{
			if (this.List(t => t.Name == name).FirstOrDefault() != null)
				return true;
			return false;
		}
		public bool TopicExists(string name, int id)
		{
			if (this.List(t => t.Id != id && t.Name == name).FirstOrDefault() != null)
			{
				return true;
			}
			return false;
		}
		private IList<Topic> RemoveChildren(IList<Topic> allTopics, Topic topic)
		{
			//remove itself from result 
			allTopics.Remove(topic);
			if (topic.Children != null && topic.Children.Count > 0)
			{
				//remove children of children from result
				topic.Children.ToList().ForEach(c => RemoveChildren(allTopics, c));
				return allTopics;
			}
			return allTopics;
		}
		public Topic FindByName(string name)
		{
			var list = this.List(t => t.Name == name);
			return this.List(t => t.Name == name).FirstOrDefault();
		}
	}
}
