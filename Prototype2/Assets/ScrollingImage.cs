using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrollingImage : MonoBehaviour
{
    public float m_verticalSpeed;

    RawImage m_rawImage;

    public void Start()
    {

         m_rawImage = GetComponent<RawImage>();

    }

    public void Update()
    {
        Rect currentUV =  m_rawImage.uvRect;
        currentUV.y -= Time.deltaTime * m_verticalSpeed;
        if (currentUV.y <= -1f || currentUV.y >= 1f)
        {
            currentUV.y = 0f;
        }

         m_rawImage.uvRect = currentUV;
    }
}
