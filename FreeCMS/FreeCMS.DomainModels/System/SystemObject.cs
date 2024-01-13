using System.ComponentModel.DataAnnotations;

namespace FreeCMS.DomainModels.System
{
	public class SystemObject
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string PluginName { get; set; }

		[Timestamp]
		public byte[] Timestamp { get; set; }

	}
}
