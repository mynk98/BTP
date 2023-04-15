using AOT;
using System;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Events;


namespace MarksAssets.FullscreenWebGL {
	public class FullscreenWebGL : MonoBehaviour
	{
		/*
			Makes your app go fullscreen. Possible options are "auto", "hide", "show". See https://developer.mozilla.org/en-US/docs/Web/API/FullscreenOptions/navigationUI
			You can also pass an empty string, in which case it defaults to "hide".
			If you called the "DetectFullscreen" method, then "EnterFullscreen" will also trigger "_onEnterFullscreenCallback"
			
			You must call this method on a pointerdown event. It won't work on a pointerenter, pointerup, onclick, etc. It MUST be a pointerdown. See the example scene.
			If you want to know more about this, read https://forum.unity.com/threads/popup-blocker-and-pointerdown-pointerclick.383233/
		*/
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_EnterFullscreen")]
		private static extern void FullscreenWebGL_EnterFullscreen(string option);

		public static void EnterFullscreen(string option) {
			#if UNITY_WEBGL && !UNITY_EDITOR
				FullscreenWebGL_EnterFullscreen(option);
			#endif
		}
		/*
			Makes your app exit fullscreen.
			If you called the "DetectFullscreen" method, then "ExitFullscreen" will also trigger "_onExitFullscreenCallback"
			
			You must call this method on a pointerdown event. It won't work on a pointerenter, pointerup, onclick, etc. It MUST be a pointerdown. See the example scene.
			If you want to know more about this, read https://forum.unity.com/threads/popup-blocker-and-pointerdown-pointerclick.383233/
		*/
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_ExitFullscreen")]
		private static extern void FullscreenWebGL_ExitFullscreen();

		public static void ExitFullscreen() {
			#if UNITY_WEBGL && !UNITY_EDITOR
				FullscreenWebGL_ExitFullscreen();
			#endif
		}
		/*
			Switches between fullscreen and non-fullscreen
			If you called the "DetectFullscreen" method, then "Toggle" will trigger "_onEnterFullscreenCallback" when it switches to fullscreen,
			and call "_onExitFullscreenCallback" when it switches to non-fullscreen.
			
			You must call this method on a pointerdown event. It won't work on a pointerenter, pointerup, onclick, etc. It MUST be a pointerdown. See the example scene.
			If you want to know more about this, read https://forum.unity.com/threads/popup-blocker-and-pointerdown-pointerclick.383233/
		*/
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_Toggle")]
		private static extern void FullscreenWebGL_Toggle(string option);

		public static void Toggle(string option) {
			#if UNITY_WEBGL && !UNITY_EDITOR
				FullscreenWebGL_Toggle(option);
			#endif
		}
		
		/*
			Attempts to detect fullscreen. If it's found, the method _onFullscreenDetectedCallback is called. If it's not, _onFullscreenNotDetectedCallback is called.
			If it is detected, it also calls _onEnterFullscreenCallback when the app enters fullscreen and calls _onExitFullscreenCallback when it exits fullscreen.
			If you want your app to react to fullscreen changes, you must call this method.
			
			If all you want to do is when the user clicks on start, the game starts and it goes fullscreen, but there is no actual mechanic that relies on fullscreen changes(for example, a fullscreen toggle button),
			then you don't need to call this method, because nothing in your app reacts to fullscreen changes.
		*/
		
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_Detect")]
		private static extern void FullscreenWebGL_Detect();

		public static void DetectFullscreen() {
			#if UNITY_WEBGL && !UNITY_EDITOR
				FullscreenWebGL_Detect();
			#endif
		}
		/*
			_onEnterFullscreenCallback and _onExitFullscreenCallback will no longer be called. Your app will no longer react to fullscreen changes.
		*/
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_DetectStop")]
		private static extern void FullscreenWebGL_DetectStop();

		public static void DetectStop() {
			#if UNITY_WEBGL && !UNITY_EDITOR
				FullscreenWebGL_Detect();
			#endif
		}
		/*
			return true if your app is currently on fullscreen, false if not.
		*/
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_IsFullscreen")]
		private static extern bool FullscreenWebGL_IsFullscreen();

		public static bool isFullscreen() {
			#if UNITY_WEBGL && !UNITY_EDITOR
				return FullscreenWebGL_IsFullscreen();
			#else
				return false;
			#endif
		}
		/*
			return true if your app is capable of going fullscreen, false if not.
		*/
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_IsFullscreenDetected")]
		private static extern bool FullscreenWebGL_IsFullscreenDetected();

		public static bool isFullscreenDetected() {
			#if UNITY_WEBGL && !UNITY_EDITOR
				return FullscreenWebGL_IsFullscreenDetected();
			#else
				return false;
			#endif
		}

		
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_SetUnityFunctions")]
		private static extern void SetUnityFunctions(Action _onEnterFullscreenCallback, Action _onExitFullscreenCallback, Action _onFullscreenDetectedCallback, Action _onFullscreenNotDetectedCallback);
		
		public bool detectFullscreenOnStart = true;//if true, DetectFullscreen method is called when the app starts.
		public UnityEvent onEnterFullscreenCallback;//you can pass a callback to be called when your app goes fullscreen
		public UnityEvent onExitFullscreenCallback;//you can pass a callback to be called when your app exits fullscreen
		public UnityEvent onFullscreenDetectedCallback;//you can pass a callback to be called if your app is detected to be capable of going fullscreen
		public UnityEvent onFullscreenNotDetectedCallback;//you can pass a callback to be called if your app is detected to NOT be capable of going fullscreen
		
		private static UnityEvent onEnterFullscreenCallbackStatic;
		private static UnityEvent onExitFullscreenCallbackStatic;
		private static UnityEvent onFullscreenDetectedCallbackStatic;
		private static UnityEvent onFullscreenNotDetectedCallbackStatic;
		
		void Awake() {
			onEnterFullscreenCallbackStatic       = onEnterFullscreenCallback;
			onExitFullscreenCallbackStatic        = onExitFullscreenCallback;
			onFullscreenDetectedCallbackStatic    = onFullscreenDetectedCallback;
			onFullscreenNotDetectedCallbackStatic = onFullscreenNotDetectedCallback;
			
			#if UNITY_WEBGL && !UNITY_EDITOR
			SetUnityFunctions(_onEnterFullscreenCallback, _onExitFullscreenCallback, _onFullscreenDetectedCallback, _onFullscreenNotDetectedCallback);
			
			if (detectFullscreenOnStart)
				DetectFullscreen();
			#endif
		}

		[MonoPInvokeCallback(typeof(Action))]
		private static void _onEnterFullscreenCallback() {
			if (onEnterFullscreenCallbackStatic != null) {
				onEnterFullscreenCallbackStatic.Invoke();
			}
		}
		
		[MonoPInvokeCallback(typeof(Action))]
		private static void _onExitFullscreenCallback() {
			if (onExitFullscreenCallbackStatic != null) {
				onExitFullscreenCallbackStatic.Invoke();
			}
		}
		
		[MonoPInvokeCallback(typeof(Action))]
		private static void _onFullscreenDetectedCallback() {
			if (onFullscreenDetectedCallbackStatic != null) {
				onFullscreenDetectedCallbackStatic.Invoke();
			}
		}
		
		[MonoPInvokeCallback(typeof(Action))]
		private static void _onFullscreenNotDetectedCallback() {
			if (onFullscreenNotDetectedCallbackStatic != null) {
				onFullscreenNotDetectedCallbackStatic.Invoke();
			}
		}
	}
}