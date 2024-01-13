using FreeCMS.DomainModels.System;

namespace FreeCMS.Service.System.Abstraction
{
	public interface ISystemObjectService
	{
		SystemObject GetByName(string objectName, string pluginName);
		SystemObject InsertSystemObject(string objectName, string pluginName);
		void RemoveSystemObject(string objectName, string pluginName);
		void RemoveByAssemblyName(string assemblyName);
		bool SystemObjectExist(string objectName, string pluginName);
	}
}
