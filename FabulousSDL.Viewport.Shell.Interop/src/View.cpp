#include "pch.h"
#include "FabulousSDL.Viewport.Shell.Interop/view.h"


namespace FabulousSDL::Viewport::Shell::Interop {
	view::view() : p_view_(new Nucleus::view()) {}


	view::~view() {
		delete this->p_view_;
	}


	void view::initialise(void* p_native_window) {
		this->p_view_->initialise(p_native_window);
	}


	void view::update() {
		this->p_view_->update();
	}


	bool view::should_exit()
	{
		return this->p_view_->should_quit();
	}
}
