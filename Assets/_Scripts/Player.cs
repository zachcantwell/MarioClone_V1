using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float m_speed = 9f; 
    [SerializeField]
    private float m_gravity = 30f;
    [SerializeField]
    private float m_jumpForce = 10f;
    [SerializeField]
    private float m_playerToGroundOffset = 0.1f;

    private float m_inputDirection;
    private float m_verticalVelocity;

    private Vector3 m_moveVector;
    private Vector3 m_lastMotion;
    private CharacterController m_Controller;

    private bool m_secondJumpAvail = false;
    private bool m_isControllerGrounded = false;

	void Start () {
        m_Controller = GetComponent<CharacterController>();	
	}
	
	void Update () {

        m_moveVector = Vector3.zero;
        m_inputDirection = Input.GetAxis("Horizontal") * m_speed;

        Jump();

        m_moveVector.y = m_verticalVelocity;
        m_Controller.Move(m_moveVector * Time.deltaTime);
        m_lastMotion = m_moveVector;
    }

    private void Jump()
    {
        if(IsControllerGrounded())
        {
            m_verticalVelocity = 0f;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                m_verticalVelocity = m_jumpForce;
                m_secondJumpAvail = true;
            }

            m_moveVector.x = m_inputDirection;
        }
        else
        {
            m_verticalVelocity -= m_gravity * Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.Space) && m_secondJumpAvail)
            {
                m_secondJumpAvail = false;
                m_verticalVelocity = m_jumpForce;
            }
            m_moveVector.x = m_lastMotion.x; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Coin":
            LevelManager.Instance.SetScore(1);
            Destroy(other.gameObject);
            break;

            case "WinLocaton":
            LevelManager.Instance.Win();
            break;

            default:
            break;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(m_Controller.collisionFlags == CollisionFlags.Sides)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.DrawRay(hit.point, hit.normal, Color.red, 2f);
                m_moveVector = hit.normal * m_speed;
                m_verticalVelocity = m_jumpForce;
                m_secondJumpAvail = true;
            }
        }

        switch(hit.gameObject.tag)
        {
            case "JumpPad":
            m_secondJumpAvail = true;
            m_verticalVelocity = m_jumpForce * 2f;
            break;

            case "Teleporter":
            transform.position = hit.transform.GetChild(0).position;
            break;

            case "WinLocation":
            LevelManager.Instance.Win();
            break;

            default:
            break;
        }
    }

    private bool IsControllerGrounded()
    {
        //Vector3 leftRayStart;
        //Vector3 rightRayStart;

        //leftRayStart = m_Controller.bounds.center;
        //leftRayStart.x -= m_Controller.bounds.extents.x;

        //rightRayStart = m_Controller.bounds.center;
        //rightRayStart.x += m_Controller.bounds.extents.x;

        //float sideRayHeight = (m_Controller.height / 2) + m_playerToGroundOffset;

        //Debug.DrawRay(leftRayStart, Vector3.down, Color.red, sideRayHeight);
        //Debug.DrawRay(rightRayStart, Vector3.down, Color.green, sideRayHeight);

        //if(Physics.Raycast(leftRayStart, Vector3.down,  sideRayHeight))
        //{
        //    Debug.Log("leftraygrounded");
        //    m_isControllerGrounded = true;
        //    return m_isControllerGrounded;
        //}

        //if(Physics.Raycast(rightRayStart, Vector3.down,  sideRayHeight))
        //{
        //    Debug.Log("rightraygrounded");
        //    m_isControllerGrounded = true;
        //    return m_isControllerGrounded;
        //}
        //m_isControllerGrounded = false;
        //return m_isControllerGrounded;

        return Physics.Raycast(transform.position, Vector3.down, m_Controller.bounds.extents.y + m_playerToGroundOffset);
    }

}
