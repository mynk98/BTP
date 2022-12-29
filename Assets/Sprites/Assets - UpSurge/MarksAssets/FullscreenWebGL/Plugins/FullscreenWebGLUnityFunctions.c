#include "emscripten.h"

void (*FullscreenWebGL_onEnterFullscreen_ref)();
void (*FullscreenWebGL_onExitFullscreen_ref)();
void (*FullscreenWebGL_onFullscreenDetected_ref)();
void (*FullscreenWebGL_onFullscreenNotDetected_ref)();

/**
	stores references to C# methods on Scripts/GyroCameraWebGL.cs
**/
void FullscreenWebGL_SetUnityFunctions(void (*_onEnterFS)(), void (*_onExitFS)(), void (*_onFSDetected), void (*_onFSNotDetected)) {
  FullscreenWebGL_onEnterFullscreen_ref       = _onEnterFS;
  FullscreenWebGL_onExitFullscreen_ref        = _onExitFS;
  FullscreenWebGL_onFullscreenDetected_ref    = _onFSDetected;
  FullscreenWebGL_onFullscreenNotDetected_ref = _onFSNotDetected;
}

/**
	the 3 functions below are the functions that are actually called
	by javascript. They run the referenced functions from C# set by GyroCamWebGL_set_unity_functions
	Their references are stored in Plugins/UnityFunctions.jspre using
	cwrap: https://emscripten.org/docs/porting/connecting_cpp_and_javascript/Interacting-with-code.html
	EMSCRIPTEN_KEEPALIVE: https://emscripten.org/docs/api_reference/emscripten.h.html#c.EMSCRIPTEN_KEEPALIVE
**/
void EMSCRIPTEN_KEEPALIVE FullscreenWebGL_onEnterFullscreen() {
  FullscreenWebGL_onEnterFullscreen_ref();
}

void EMSCRIPTEN_KEEPALIVE FullscreenWebGL_onExitFullscreen() {
  FullscreenWebGL_onExitFullscreen_ref();
}

void EMSCRIPTEN_KEEPALIVE FullscreenWebGL_onFullscreenDetected() {
  FullscreenWebGL_onFullscreenDetected_ref();
}

void EMSCRIPTEN_KEEPALIVE FullscreenWebGL_onFullscreenNotDetected() {
  FullscreenWebGL_onFullscreenNotDetected_ref();
}