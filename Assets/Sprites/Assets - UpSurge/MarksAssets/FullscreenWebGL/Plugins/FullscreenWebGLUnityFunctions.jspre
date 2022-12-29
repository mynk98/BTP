Module['FullscreenWebGL'] = Module['FullscreenWebGL'] || {};

Module['FullscreenWebGL'].onEnterFullscreen = function() {
	this.onEnterFullscreenInternal = this.onEnterFullscreenInternal || Module.cwrap("FullscreenWebGL_onEnterFullscreen", null, []);
	this.onEnterFullscreenInternal();
};

Module['FullscreenWebGL'].onExitFullscreen = function() {
	this.onExitFullscreenInternal = this.onExitFullscreenInternal || Module.cwrap("FullscreenWebGL_onExitFullscreen", null, []);
	this.onExitFullscreenInternal();
};

Module['FullscreenWebGL'].onFullscreenDetected = function() {
	this.onFullscreenDetectedInternal = this.onFullscreenDetectedInternal || Module.cwrap("FullscreenWebGL_onFullscreenDetected", null, []);
	this.onFullscreenDetectedInternal();
};

Module['FullscreenWebGL'].onFullscreenNotDetected = function() {
	this.onFullscreenNotDetectedInternal = this.onFullscreenNotDetectedInternal || Module.cwrap("FullscreenWebGL_onFullscreenNotDetected", null, []);
	this.onFullscreenNotDetectedInternal();
};
