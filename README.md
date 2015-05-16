# Grasshopper :: Game Engine

Grasshopper is a C# and .Net-based game engine I'm developing to support my own
game development. It provides a lower-level core game engine that can be used
for various different types of games and handles general scenarios common to
all 3D games, such as loading and rendering of materials, meshes, textures and
so forth. It also provides a higher level game engine (and possibly more in
future) which builds on the baseline engine and provides the tools for
developing a specific style of game. The first engine of this type that I am creating
focuses on features supporting a 3D pixel art graphical style, drawing on
inspiration from Minecraft-style voxel grids and the 16 bit video game art
revival genre. This is the "GridWorld" project you'll see in the source code.

The engine is named as such because I "have much to learn, grasshopper".
Current support is for SharpDX, making the engine Windows-only for now, but
these are completely abstracted out into separate libraries and thus the engine
is designed to support other graphics layers and platforms. After the release
of Vulkan and DirectX 12, I'd like to implement support for both APIs. I have
no current plans to support mobile devices, but I may change my mind on this if
it turns out that a game I'm developing is suitable for adaptation to a touch
interface.

Grasshopper builds as NuGet packages into a build directory, from where it can
be easily referenced by other game projects. Seeing as it's difficult to
develop an engine without an attached use case to exercise the various
features, the solution also includes various samples which serve as a platform
to exercise the engine's features and prove that they work correctly.
