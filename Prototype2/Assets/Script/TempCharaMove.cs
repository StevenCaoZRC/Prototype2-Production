﻿using UnityEngine;

public class TempCharaMove : MonoBehaviour
{
    [HideInInspector]
    public bool m_canMove = true;
    private Vector3 m_velocity = Vector3.zero;
    private Vector3 m_rotation = Vector3.zero;
    private Vector3 m_camRotation = Vector3.zero;

    Quaternion m_targetRotation;
    Rigidbody m_rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movecharacter();
    }

    //Sets Movement vector of the character
    public void SetMovement(Vector3 _velocity)
    {
        m_velocity = _velocity;
    }

    //Sets Rotation vector of the character
    public void SetRotation(Vector3 _rotation)
    {
        m_rotation = _rotation;
    }

    public void Movecharacter()
    {
        if (m_canMove)
        {
            if (m_velocity != Vector3.zero)
            {
                //pos.y -= gravity * Time.deltaTime;
                Vector3 pos = m_rigidBody.position + m_velocity * Time.fixedDeltaTime;
                m_rigidBody.MovePosition(new Vector3(pos.x, pos.y-2.0f*Time.deltaTime, pos.z));
            }
            m_rigidBody.MoveRotation(m_rigidBody.rotation * Quaternion.Euler(m_rotation));
        }
    }
}
