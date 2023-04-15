mergeInto(LibraryManager.library, {	
	FullscreenWebGL_EnterFullscreen: function(option) {
		if (document.fullscreenElement !== null) {
			return;
		}
		option = UTF8ToString(option);
		option = option !== "hide" && option !== "show" && option !== "auto" ? "hide" : option;
		Module.asmLibraryArg._FullscreenWebGL_EnterFullscreen.option = option;
		
		const elem = document.querySelector("canvas");
		
		new Promise(function (resolve, reject) {
			elem.addEventListener('pointerup', function () {
				/* Pointerup is necessary here for requesting full screen, because the permission requires user interaction.
				   Pointerup runs after the pointerdown from the C# side.
				*/
				
				const fs = elem.requestFullscreen || elem.webkitRequestFullscreen || elem.mozRequestFullScreen || elem.msRequestFullscreen;
				if (!fs) {
					reject(Object.create({
							get name() {
								return "UnsupportedAPIError"
							},
							get message() {
								return "Your browser does not support the \'Fullscreen\' API"
							}
						})
					);
				
				}
					
				fs.call(elem, { navigationUI: option }).then(
					function() {
						resolve("granted");
					}
				).catch(
					function(err) {
						reject(err);
					}
				);

			}, { once: true });
		})
		.catch(
			function(rej) {
				console.error(rej);
			}
		)
	},
	FullscreenWebGL_ExitFullscreen: function() {
		if (document.fullscreenElement === null) {
			return;
		}

		new Promise(function (resolve, reject) {
			document.documentElement.addEventListener('pointerup', function () {
				document.exitFullscreen().then(
					function() {
						resolve("granted");
					}
				).catch(
					function(err) {
						reject(err);
					}
				);
			}, { once: true });
		})
		.catch(
			function(rej) {
				console.error(rej);
			}
		)
	},
	FullscreenWebGL_Toggle: function(option) {
		if (document.fullscreenElement === null)
			Module.asmLibraryArg._FullscreenWebGL_EnterFullscreen(option);
		else
			Module.asmLibraryArg._FullscreenWebGL_ExitFullscreen();
	},
	FullscreenWebGL_Detect: function() {
		if (document.fullscreenEnabled) {
			if (Module.asmLibraryArg._FullscreenWebGL_Detect.fullscreenChangeHandler === undefined) {
				Module['FullscreenWebGL'].onFullscreenDetected();
				Module.asmLibraryArg._FullscreenWebGL_Detect.fullscreenChangeHandler =
				function () {
					if (document.fullscreenElement) {
						Module['FullscreenWebGL'].onEnterFullscreen();
					} else {
						Module['FullscreenWebGL'].onExitFullscreen();
					}
				};
				
				document.addEventListener("fullscreenchange", Module.asmLibraryArg._FullscreenWebGL_Detect.fullscreenChangeHandler);
			}
		} else {
			Module['FullscreenWebGL'].onFullscreenNotDetected();
		}
	},
	FullscreenWebGL_DetectStop: function() {
		if (Module.asmLibraryArg._FullscreenWebGL_Detect.fullscreenChangeHandler !== undefined) {
			document.removeEventListener("fullscreenchange", Module.asmLibraryArg._FullscreenWebGL_Detect.fullscreenChangeHandler);
			Module.asmLibraryArg._FullscreenWebGL_Detect.fullscreenChangeHandler = undefined;
		}
	},
	FullscreenWebGL_IsFullscreen: function() {
		if (document.fullscreenElement === null)
			return false;
		else return true;
	},
	FullscreenWebGL_IsFullscreenDetected: function() {
		if (document.fullscreenEnabled)
			return true;
		else return false;
	}
});