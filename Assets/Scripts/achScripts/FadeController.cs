﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{

    static FadeController _instance = null;
    public static FadeController instance
    {
        get
        {
            return _instance;
        }
    }

    public Image fadepnl;

    IEnumerator fadeout()
    {
        float fadecount = 0;
        while (fadecount <= 1.0f)
        {
            fadecount += 0.02f;
            yield return new WaitForSeconds(0.01f);
            fadepnl.color = new Color(fadepnl.color.r, fadepnl.color.g, fadepnl.color.b, fadecount);
        }
    }
    IEnumerator fadein()
    {
        float fadecount = 1.0f;
        while (fadecount >= 0.0f)
        {
            fadecount -= 0.02f;
            yield return new WaitForSeconds(0.01f);
            fadepnl.color = new Color(fadepnl.color.r, fadepnl.color.g, fadepnl.color.b, fadecount);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void objFadeout()
    {
        Debug.Log("out");
        StartCoroutine(fadeout());
    }

    public void objFadein()
    {
        Debug.Log("in");
        StartCoroutine(fadein());
    }
}