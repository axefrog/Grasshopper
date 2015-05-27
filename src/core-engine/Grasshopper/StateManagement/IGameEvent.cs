namespace Grasshopper.StateManagement
{
	public interface IGameEvent
	{
		string Type { get; }
	}

	public static class MeshLibraryEvent
	{
		public const string MeshAdded = "meshlib.mesh.added";
	}

	public class MeshAddedEvent : IGameEvent
	{
		public const string TypeId = "meshlib.mesh.added";
		public string Type { get { return TypeId; } }
	}

	// don't set your heart on using the global event queue; it may be more efficient to use something specific to the graphics pipeline.
}