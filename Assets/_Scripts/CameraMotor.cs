using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

    [SerializeField]
    private Transform m_LookAt;
    [SerializeField]
    private Vector3 m_Offset = new Vector3(0f, 0f, -6.5f);
    [SerializeField]
    private float m_smoothSpeed = 0.125f;
    private bool m_smooth = true;

	void LateUpdate () {

        Vector3 desiredPosition = m_LookAt.transform.position + m_Offset;

        if(m_smooth)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPosition, m_smoothSpeed);
        }
        else
        {
            transform.position = desiredPosition;
        }
    }
}
