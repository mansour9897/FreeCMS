namespace FreeCMS.DomainModels.Cms
{
	public class Topic
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime LastModified { get; set; }
		public string Description { get; set; }
		public int? ParentId { get; set; }
		public virtual Topic Parent { get; set; }
		public virtual ICollection<Topic> Children { get; set; }
		public virtual ICollection<PostTopic> PostTopics { get; set; }
	}
}
