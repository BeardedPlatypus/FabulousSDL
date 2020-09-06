#include "pch.h"
#include "FabulousSdl.Viewport.Nucleus/View.h"

#include <string>


namespace FabulousSDL::Viewport::Nucleus
{

	void view::initialise()
	{
		SDL_Init(SDL_INIT_VIDEO);
		atexit(SDL_Quit);

		const std::string title = "SDL Native App";

		this->p_window_ = SDL_CreateWindow(title.c_str(), 100, 100, 1200, 600, SDL_WINDOW_SHOWN);
		this->p_renderer_ = SDL_CreateRenderer(this->p_window_, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
	}

	void view::initialise(const void* p_native_window)
	{
		SDL_Init(SDL_INIT_VIDEO);
		atexit(SDL_Quit);

		this->p_window_ = SDL_CreateWindowFrom(p_native_window);
#if _DEBUG
		std::string windowError = SDL_GetError();
#endif
		
		this->p_renderer_ = SDL_GetRenderer(this->p_window_);

#if _DEBUG
		std::string rendererError = SDL_GetError();
#endif


		int w, h;
		SDL_GetWindowSize(this->p_window_, &w, &h);
	}

	void view::update()
	{
		int w, h;
		SDL_GetWindowSize(this->p_window_, &w, &h);
		
		if (w != 0 && h != 0 && p_renderer_ == nullptr)
		{
			this->p_renderer_ = SDL_CreateRenderer(this->p_window_, -1, SDL_RENDERER_ACCELERATED | SDL_RENDERER_PRESENTVSYNC);
		}

		if (p_renderer_ == nullptr) return;
		
        SDL_Event sdl_event;
		while (SDL_PollEvent(&sdl_event))
		{
			if (sdl_event.type == SDL_QUIT) 
			{
				this->should_quit_ = true;
				break;
			}
		}

        SDL_SetRenderDrawColor(this->p_renderer_, 128, 216, 235, 255);
        SDL_RenderClear(this->p_renderer_);
        SDL_RenderPresent(this->p_renderer_);
	}

	bool view::should_quit() const
	{
		return this->should_quit_;
	}
}


