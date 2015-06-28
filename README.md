# Grasshopper :: Game Engine

Grasshopper is a free, BSD-licensed, **code-first** game engine written entirely in **C# and .Net**.  
[Scroll down for some pretty animations!](#capabilities-and-samples)

### Modular and Somewhat Unopiniated

The engine is built as a set of modular layers over a raw, basic set of platform abstractions. This
means that you can use as much or as little of it as you want, and most things should be relatively
easy to replace, or just not use if you don't like them, or want to do things differently.

A common problem with the claim of a given engine or framework being modular and replaceable is that
you have to spend huge amounts of time and effort deep diving to try and figure out the implications
of swapping out or changing anything, and how and where it is even practical to do so. Grasshopper
gets around this two ways: (1) a lot of thought is being put into separating concerns in clear,
logical ways so that you really can just pick and choose the abstractions you're interested in without
having to worry too much about what subsystems may be affected by your choice of abstractions, and
(2) it is a core value for the engine that there should be a highly comprehensive set of well-commented sample projects
that teach you how to use the engine from first principles, only introducing higher-level abstractions
on an as-needed basis. Where an abstraction is used in a sample, there will usually be a prior sample that
makes use of lower-level components instead.

It should be noted that if a feature is missing from an abstraction, it's probably just because I haven't
had a need for it yet, or haven't known enough to include it. Pull requests are welcome! (Though subject
to review first - the codebase must still meet a minimum standard of quality and design, in my view).

### Who is this for?

If you like **starting from scratch** with a blank project, **writing a few lines of code** and seeing something happen,
this engine is for you. If you want **the engine's tool set to get out of your face** and stop telling
you what your game development process should look like, this engine is for you. If you have an
interest in **procedural content generation**, and wish the **engine focussed on your needs**, rather than
the needs of a huge, traditional, team-based, prefabricated art pipeline, then this engine is for you.

### Platform Non-Specific

The engine's design allows for alternate platform targets to **Windows**, such as **OS X** and **Linux**. While
the initial version only supports Windows, every line of code is written so that it abstracts away
anything platform-specific to an underlying layer. Support for mobile and console platforms may be added later
but this is very low priority. I like gaming on the desktop, so that's where my focus is.

### Capabilities and Samples

*THIS IS A WORK IN PROGRESS! The engine's current capabilities extend roughly as far as what was
required to produce the animated sample images below.*

![](https://raw.github.com/axefrog/Grasshopper/master/samples/Images/cube-rainbow.gif)
![](https://raw.github.com/axefrog/Grasshopper/master/samples/Images/cube.gif)
![](https://raw.github.com/axefrog/Grasshopper/master/samples/Images/cubes-rainbow.gif)
![](https://raw.github.com/axefrog/Grasshopper/master/samples/Images/cubes.gif)
![](https://raw.github.com/axefrog/Grasshopper/master/samples/Images/cubes-freelook.gif)
![](https://raw.github.com/axefrog/Grasshopper/master/samples/Images/cube-multitexture.gif)

1. **[Hello World](https://github.com/axefrog/Grasshopper/tree/master/samples/Core/01.%20HelloWorld)** - Demonstrates setting up a graphics context and opening a couple of windows.
2. **[Simple Quad](https://github.com/axefrog/Grasshopper/tree/master/samples/Core/02.%20SimpleQuad)** - Shows how to render a simple untextured 2D quad in a window
3. **[Simple Instancing](https://github.com/axefrog/Grasshopper/tree/master/samples/Core/04.%20SimpleInstancing)** - Expands on the previous sample by rendering multiple instances of the quad in a single instanced draw call
4. **[Simple Cube](https://github.com/axefrog/Grasshopper/tree/master/samples/Core/04.%20SimpleCube)** - Moves into 3D territory by showing how to generate a procedural cube and add a basic rotation over time
5. **[Many Cubes](https://github.com/axefrog/Grasshopper/tree/master/samples/Core/05.%20ManyCubes)** - Applies the principles in the previous two samples to generate a scene with thousands of spinning cubes
6. **[Simple Textured Cube](https://github.com/axefrog/Grasshopper/tree/master/samples/Core/06.%20SimpleTexturedCube)** - Introduces textures and materials by adding a texture to the cube we rendered in sample #4.
7. **[Many Textured Cubes](https://github.com/axefrog/Grasshopper/tree/master/samples/Core/07.%20ManyTexturedCubes)** - Combines what we learned in the previous two samples
8. **[Free Look Camera](https://github.com/axefrog/Grasshopper/tree/master/samples/Core/08.%20FreeLookCamera)** - Expands on the previous sample by introducing input and implementing a simple keyboard- and mouse-controlled camera view, allowing you to fly through the cube instances.
9. **[Multitextured Cube](https://github.com/axefrog/Grasshopper/tree/master/samples/Core/09.%20MultiTexturedCube)** - Using sample #6 as a base, demonstrates use of a texture array to place different textures on different faces and thus replicate a typical Minecraft block.

### Goals: Why I Am Doing This

* I want to work on my various game ideas in a coding environment I am both
  comfortable with and productive in.
* I want to learn, and get extremely good at, 3D game programming. The engine is
  named as such because I "have much to learn, grasshopper", in this regard. I
  will be applying my general software engineering/architecture experience to build
  the best engine I can, employing solid, best-practice software engineering principles.
* I want to to approach game development from a minimal, code-first perspective.
  Just a few lines of code should be all I need to start making things happen.
* I want an engine that I know the codebase of from top to bottom, am able to
  extend and improve as much as I like, and which doesn't stand in my way when
  I have an idea I'd like to implement.
* I want an engine which is designed in such a way that it facilitates, rather
  than hinders, procedural content generation and consumption at run-time. Techniques and algorithms that
  favour pre-baked content will be deprioritised in this regard unless absolutely needed.
  I would like almost all relevant engine components, from materials, to animations,
  to 3D models, to textures, to be able to be dynamically/procedurally constructed/generated at run-
  time without any tooling-related preprocessing required. I also want the process of generating, streaming, loading and unloading
  content and assets to be extremely easy and able to work without interrupting the user experience.
* Even though the engine is first and foremost for my own needs, I'd like it to
  be well-documented with a full set of XML code comments, and extensive samples
  that demonstrate how to use the engine from absolute first principles, without
  overwhelming other people who may be interested in using it.

### Schedule and Road Map

There is no "release date" or ETA on the completion of anything on my to-do list.
It's done when it's done, and that's entirely based on when I choose to work on it.

See the issues list for an idea of the road map. If you know what you're doing and
want to contribute, please fork the project and submit pull requests. If you're not
sure if I'll accept a pull request, discuss your plans with me first. I can be reached
by email at *axefrog* at *gmail*.
