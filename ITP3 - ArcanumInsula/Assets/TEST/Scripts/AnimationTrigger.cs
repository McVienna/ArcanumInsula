using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{

    // Use this for initialization
    private GameObject m_animatedPlayer;
    private Animator m_animator;
    private Transform m_playerTransform;
    private Vector3 m_currPos;
    private Vector3 m_prevPos;



    void Start()
    {
        m_animatedPlayer = GameObject.FindGameObjectWithTag("player_anim");
        m_animator = m_animatedPlayer.GetComponent <Animator> ();
        m_currPos = new Vector3(0, 0, 0);
        m_prevPos = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_playerTransform = GetComponent<Transform>();

        m_currPos = m_playerTransform.position;

        if (m_currPos == m_prevPos)
        {
            m_animator.SetBool("IsMoving", false);
        }
        
        if(m_currPos != m_prevPos)
        {
            m_animator.SetBool("IsMoving", true);
        }

        m_prevPos = m_currPos;

    }
}
