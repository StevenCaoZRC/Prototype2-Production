﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrollingImage : MonoBehaviour
{

    public float horizontalSpeed;
    public float verticalSpeed;

    RawImage myRawImage;

    public void Start()
    {

        myRawImage = GetComponent<RawImage>();

    }

    public void Update()
    {
        Rect currentUV = myRawImage.uvRect;
        currentUV.x -= Time.deltaTime * horizontalSpeed;
        currentUV.y -= Time.deltaTime * verticalSpeed;

        if (currentUV.x <= -1f || currentUV.x >= 1f)
        {
            currentUV.x = 0f;
        }

        if (currentUV.y <= -1f || currentUV.y >= 1f)
        {
            currentUV.y = 0f;
        }

        myRawImage.uvRect = currentUV;
    }
}
