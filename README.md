<p align="center"><img src="https://github.com/BeardedPlatypus/FabulousSDL/blob/master/FabulousSDL.png?raw=true" alt="InteropWindow" title="InteropWindow" width=100% /></p>

*Demo application, the blue top-middle window is rendered by the native SDL2 Code*

# Hosting a C++ SDL2 Window within a Fabulous WPF application

[![Build Status](https://dev.azure.com/mwtegelaers/FabulousSDL/_apis/build/status/BeardedPlatypus.FabulousSDL?branchName=master)](https://dev.azure.com/mwtegelaers/FabulousSDL/_build/latest?definitionId=23&branchName=master)

This repository shows a minimal implementation of hosting a native SDL2 window 
written in C++ within a [Fabulous for Xamarin.Forms](https://github.com/fsprojects/Fabulous) 
WPF application. It uses a similar `HwndHost` as used in my [SDL_WPF_Interop repository](https://github.com/BeardedPlatypus/SDL_WPF_Interop). 
This minimal implementation can be used as a starting point for developing your
own mixed F# Fabulous / C++ SDL2 application, as I will do for some of the tooling
for my [PacMan game](https://github.com/BeardedPlatypus/PacMan). 

## Installation

In order to build this repository, several tools need to be installed. The 
[Azure DevOps yml](https://github.com/BeardedPlatypus/FabulousSDL/blob/master/azure-pipelines.yml) 
provides an overview of the build steps. The application itself is build 
with Visual Studio 2019.

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

## Additional implementation notes

Unfortunately, my grasp of F#, Fabulous, and Xamarin.Forms is not sufficiently
advanced enough that I was capable of implementing the `FabulousSDL.WPF.Components.Viewport` 
directly in F#. However the current code seem so to work for this minimal example.

Furthermore, there is some additional code within the `ViewportControl` that 
manages the initialisation of the `ViewportHost`. While experimenting I found that
unfortunately the `Loaded` event of the user control does not have the correct 
width and height when the `Viewport` is embedded in a grid or other layout 
(save the `Frame` for some reason). If the actual `SDL_Renderer` is initialised 
with a 0 x 0 window, it will fail resulting in a black screen. In order to avoid 
this situation we basically listen to the first size changed with an actual size
after the `Loaded` event, and initialise the `ViewportHost` with these values. 

## References and further reading

* [Fabulous for Xamarin.Forms](https://github.com/fsprojects/Fabulous)
* [SDL2 Home Page](https://www.libsdl.org/)
* [msdn: implementing a custom renderer](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/custom-renderer/view)
* [Hosting a C++ D3D engine C# winforms](https://www.gamedev.net/articles/programming/graphics/hosting-a-c-d3d-engine-in-c-winforms-r2526/)
* [Hosting a Win32 window in WPF sample](https://github.com/Microsoft/WPF-Samples/tree/master/Migration%20and%20Interoperability/WPFHostingWin32Control)

