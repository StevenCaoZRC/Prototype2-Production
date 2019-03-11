using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCharaMove : MonoBehaviour
{
    public float m_speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movecharacter();
    }

    void Movecharacter()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 position = new Vector3(hor, 0, ver) * m_speed * Time.deltaTime;

        transform.Translate(position);
    }
}
