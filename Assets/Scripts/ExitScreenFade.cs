using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.Core;
using Liminal.Core.Fader;

public class ExitScreenFade : MonoBehaviour
{
    float fadeDuration = 0.5f;
    Color color = Color.black;

    // Start is called before the first frame update
    void Start()
    {

    }

    void expEnd()
    {
        var fader = ScreenFader.Instance;
        fader.FadeTo(color, fadeDuration);
        ExperienceApp.End();
    }
    // Update is called once per frame
    void Update()
    {

    }
}

