using System;

namespace Grasshopper.Graphics.Materials
{
	public interface IMaterialManager : IDisposable
	{
		void Add(MaterialSpec material);
		void SetActive(string id);
		void Remove(string id);
		bool Exists(string id);
	}
}
