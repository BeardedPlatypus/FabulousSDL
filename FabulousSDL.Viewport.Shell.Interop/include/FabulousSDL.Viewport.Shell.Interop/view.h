#pragma once
#include "FabulousSDL.Viewport.Nucleus/View.h"

namespace FabulousSDL {
namespace Viewport {
namespace Shell {
namespace Interop {

/// <summary>
/// <see cref="view"/> provides a wrapper for the
/// native <see cref="Nucleus::view"/> class.
/// </summary>
public ref class view sealed {
public:
    /// <summary>
    /// Creates a new <see cref="view"/>.
    /// </summary>
    view();

    /// <summary>
    /// Dispose this <see cref="view"/>.
    /// </summary>
    ~view();

    /// <summary>
    /// Initialises this view in the provided <paramref name="p_native_window"/>
    /// </summary>
    /// <param name="p_native_window">A poitner to the native window.</param>
    void initialise(void* p_native_window);
	
    /// <summary>
    /// Updates this view by rendering a new frame to its window.
    /// </summary>
    void update();
	
    /// <summary>
    /// Get whether this <see cref="view"/> should quit.
    /// </summary>
    /// <returns>
    /// Whether this <see cref="view"/> should quit.
    /// </returns>
    bool should_exit();
private:
    Nucleus::view* p_view_;
};
}
}
}
}

