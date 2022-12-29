using UnityEngine;
using MarksAssets.FullscreenWebGL;

public class Example : MonoBehaviour
{
    public void EnterFullscreen() {
        FullscreenWebGL.EnterFullscreen("");
    }

    public void ExitFullscreen() {
        FullscreenWebGL.ExitFullscreen();
    }
}
