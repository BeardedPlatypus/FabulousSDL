#pragma once
#include <SDL2/SDL.h>

namespace FabulousSDL::Viewport::Nucleus {

/// <summary>
/// <see cref="view"/> provides a wrapper around our SDL2 code.
/// </summary>
class view {
public:	
    /// <summary>
    /// Creates a new <see cref="view"/>.
    /// </summary>
    view() = default;
	
    /// <summary>
    /// Initialises this <see cref="view"/>.
    /// </summary>
    /// <remarks>
    /// This will create a new native window.
    /// </remarks>
    void initialise();
	
    /// <summary>
    /// Initialises this view in the provided <paramref name="p_native_window"/>
    /// </summary>
    /// <param name="p_native_window">A poitner to the native window.</param>
    void initialise(const void* p_native_window);

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
    [[nodiscard]] bool should_quit() const;
private:
    SDL_Window* p_window_ = nullptr;
    SDL_Renderer* p_renderer_ = nullptr;

    bool should_quit_ = false;
};
}

