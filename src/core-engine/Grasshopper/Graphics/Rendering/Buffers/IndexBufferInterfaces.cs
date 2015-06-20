using System;
using Grasshopper.Platform;

namespace Grasshopper.Graphics.Rendering.Buffers
{
	public interface IIndexBufferManagerFactory
	{
		IIndexBufferManager Create();
	}

	public interface IIndexBufferManager : IActivatablePlatformResourceManager<IIndexBufferResource>
	{
		IIndexBufferResource Create(string id);
	}

	public interface IIndexBufferResource : IActivatablePlatformResource
	{
		IIndexBufferDataWriter BeginWrite(int totalIndicesInBuffer);
	}

	public interface IIndexBufferDataWriter : IDisposable
	{
		string Id { get; }
		int TotalVertices { get; }
		long Position { get; set; }
		long Length { get; }
		void Write(uint data);
		void Write(uint[] data);
	}
}
