<p align="center"><img src="https://github.com/BeardedPlatypus/FabulousSDL/blob/master/FabulousSDL.png?raw=true" alt="InteropWindow" title="InteropWindow" width=80% /></p>
*Demo application, the blue window is rendered by the SDL2 Code*

# Hosting a C++ SDL2 Window within a Fabulous WPF application

This repository shows a minimal implementation of hosting a native SDL2 window 
written in C++ within a [Fabulous for Xamarin.Forms](https://github.com/fsprojects/Fabulous) 
WPF application. It uses a similar `HwndHost` as used in my [SDL_WPF_Interop repository](https://github.com/BeardedPlatypus/SDL_WPF_Interop). 
This minimal implementation can be used as a starting point for developing your
own mixed F# Fabulous / C++ SDL2 application, as I will do for some of the tooling
for my [PacMan game](https://github.com/BeardedPlatypus/PacMan). 

## Installation

In order to build this repository, several tools need to be installed. The 
Azure DevOps provides an overview of the build steps. The application itself
is build with Visual Studio 2019.

### F# tooling and Xamarin.Forms

In order to build and run the application the F# tooling should be installed. 
These can be obtained from within the Visual Studio installer, which can be 
accessed from the `tools > Get Tools and Features ...` menu item.

Make sure you have at least the following features installed:

* F# desktop language support
* F# language support
* Xamarin

### vcpkg

The C++ dependencies can be installed through [vcpkg](https://github.com/microsoft/vcpkg).
It requires the following packages:

* SDL2:x64-windows
* SDL2-image:x64-windows

These can be installed as follows:

```bash
vcpkg.exe install SDL2:x64-windows SDL2-image:x64-windows
```

The application itself is build with the x64 architecture, as such it is 
important to add the `x64-windows` postfixes. My vcpkg installation defaults
to installing 32 bit libraries, which will lead to compile errors and 
run-time exceptions.

## Code organisation

The actual code is organised in five small projects located at the root level: 

* `FabulousSDL` is the F# project that contains the main Fabulous app file, 
   describing the actual application.
* `FabulousSDL.Viewport.Nucleus` is a static C++ library that contains the 
   native C++ SDL2 code. In particular the `view.h` and `view.cpp` files 
   describe how the actual SDL2 window looks.
* `FabulousSDL.Viewport.Shell.Interop` is a [C++/CLI project](https://docs.microsoft.com/en-us/cpp/dotnet/dotnet-programming-with-cpp-cli-visual-cpp?view=vs-2019). 
   The `view.h` file wraps the `Nucleus.view.h` making it available in the .NET
   components.
* `FabulousSDL.WPF` is the F# project that contains the WPF platform specific
   Xamarin.Forms code. In particular it provides the custom renderer for the 
   SDL2 viewport.
* `FabulousSDL.WPF.Components` is a C# WPF library that provides the 
   `ViewportControl`. The `ViewportControl` is the control that embeds the SDL2
   window, such that it can be used by the `FabulousSDL.WPF` project.

Unfortunately, my grasp of F#, Fabulous, and Xamarin.Forms is not sufficiently
advanced enough that I was capable of implementing the `FabulousSDL.WPF.Components.Viewport` 
directly in F#. However the current code seem so to work for this minimal example.

## References and further reading

* [Fabulous for Xamarin.Forms](https://github.com/fsprojects/Fabulous)
* [SDL2 Home Page](https://www.libsdl.org/)
* [msdn: implementing a custom renderer](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/custom-renderer/view)
* [Hosting a C++ D3D engine C# winforms](https://www.gamedev.net/articles/programming/graphics/hosting-a-c-d3d-engine-in-c-winforms-r2526/)


