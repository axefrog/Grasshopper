using System;

namespace Grasshopper.Graphics.Materials
{
	public interface IMaterialManager : IDisposable
	{
		void Initialize(MaterialSpec material);
		void SetActive(string id);
		void Uninitialize(string id);
		bool IsInitialized(string id);
	}
}
