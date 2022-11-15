using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 m_forwardVec = new Vector3(Mathf.Sqrt(0.5f), 0.0f, Mathf.Sqrt(0.5f));
    private Vector3 m_rightVec = new Vector3(Mathf.Sqrt(0.5f), 0.0f, -Mathf.Sqrt(0.5f));

    private Vector2 m_moveInput;
    public float m_moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVec = m_rightVec * m_moveInput.x;
        moveVec += m_forwardVec * m_moveInput.y;

        moveVec = moveVec.normalized * m_moveSpeed;

        transform.position += moveVec * Time.deltaTime;
    }

    public void OnMove(InputValue input)
    {
        m_moveInput = input.Get<Vector2>();
    }
}
