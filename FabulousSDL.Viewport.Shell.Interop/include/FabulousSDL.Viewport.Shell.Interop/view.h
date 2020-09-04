#pragma once
#include "FabulousSDL.Viewport.Nucleus/View.h"

namespace FabulousSDL {
namespace Viewport {
namespace Shell {
namespace Interop {

public ref class view sealed {
public:
    view();
    ~view();

    void initialise(void* p_native_window);
    void update();
    bool should_exit();

private:
    Nucleus::view* p_view_;
};
}
}
}
}

