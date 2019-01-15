using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    //Merged with GroundcheckAI

    // Use this for initialization
    private float m_movementSpeed = 2.0f; //--- Movement Speed of enemy - make this setable in GameObject Inspector (set public)
    private GameObject m_other = null; //--- GameObject which triggers collision
    private GameObject[] m_pointArray;//--- Array to save instantiated patrol points
    private GameObject m_patrolPoint;//--- Object to hold patrol Point
    private Transform m_otherTransform = null; //--- Trigger transform
    private Transform m_enemyTransform = null;//--- Transform to keep Track of enemy's position
    private Vector3 m_enemyPosition;//--- Vector that holds the raw position of the enemy
    private Vector3 m_boxSize;//--- Vector to store the Box Size of the groundCollider
    private bool m_isPatrouling;//--- bool to check whether enemy ios patrooling
    private bool m_reachedTarget;//--- bool to check wheter patrooling enemy reached his target
    private int m_temp_target_count = 0;//--- int counter to keep track of array items

    void Start()
    {
        var localScale_x = 0.0f;
        var localScale_y = 0.0f;

        BoxCollider groundCollider = this.GetComponent<BoxCollider>();

        //initialising important member variables
        m_boxSize = groundCollider.size;//--- Get Transform from Box/Ground collider
        m_pointArray = new GameObject[2]; //--- Array to keep Spawnpoints
        m_isPatrouling = true; //--- bool for "state machine" 
        m_enemyTransform = this.transform;//--- Get Transform for relative positioning
        m_enemyPosition = m_enemyTransform.position;

        //Get localScale of BoxCollider transform in order to Spawn patrolPoints in Area
        localScale_x = m_boxSize.x;
        localScale_y = m_boxSize.y;

        //Instanciate 2 patrolPoints inside Enemy Area
        for (int i = 0; i < 2; i++)
        {
            Debug.Log("Creating PatrolPoint" + i);

            //Create empty GameObject as Point for patrol
            m_patrolPoint = new GameObject();

            //Maybe need of unique identifier but not sure because using a private array
            m_patrolPoint.name = "patrol_Point" + i;
            m_patrolPoint.tag = "patrol_Point" + i;

            var leftSide = new Vector3((m_enemyPosition.x - (localScale_x / 2)), (m_enemyPosition.y + Random.Range(0.0f, localScale_y)), 0);
            var rightSide = new Vector3((m_enemyPosition.x + (localScale_x / 2)), (m_enemyPosition.y + Random.Range(0.0f, localScale_y)), 0);

            //Used to create Patrool Points in Enemy Area one at the left Side border one on the Right
            if (i == 0)
            {
                m_patrolPoint.transform.position = leftSide; //Left Side Patrool Point
            }
            else
            {
                m_patrolPoint.transform.position = rightSide; //Right Side Patrool Point
            }

            //Add Patrool points to array for KI to use the path
            m_pointArray[i] = m_patrolPoint;
        }

        //Look whether isTrigger is activated for BoxCollider(GroundCollider)
        if (groundCollider.isTrigger == false)
        {
            groundCollider.isTrigger = true;
        }

        m_reachedTarget = false;

        stoneEntered = false;
    }

    // Update is called once per frame
    void Update()
    {
        float step = m_movementSpeed * Time.deltaTime;
        GameObject target;

        //Get the right targe out of the array -> used mod%2 cuz there are only 2 Elements in Array e.g. the return value can only be 0 or 1 
        target = m_pointArray[m_temp_target_count % 2];

        if(stoneEntered == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_otherTransform.position, step);
        }

        //As long as the target was not reached
        if(m_reachedTarget == false && stoneEntered == false)
        {
            m_isPatrouling = true;

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

            //cast values as int to become the wished result -> 0.53 will be 1 so slight difference won't fake the result
            if (((int)transform.position.x == (int)target.transform.position.x) && ((int)transform.position.z == (int)target.transform.position.z))
            {
                m_reachedTarget = true;

                //if the target was reached raise the counter by 1 to become the next element out of the array
                m_temp_target_count++;
            }
        }

        m_reachedTarget = false;

        /*Keep this for movement towards Ritual Stone!!!
         * 
         *Checks whether stone entered EnemyArea (BoxCollider -> IsTrigger) - move towards stone if entered
         *if (stoneEntered == true)
         *{
         *  float step = m_movementSpeed * Time.deltaTime;
         *
         *  transform.position = Vector3.MoveTowards(transform.position, m_otherTransoform.position, step);
         *  Debug.Log("STONE (UPDATECALL)");
         *}
         *else if (stoneEntered == false)
         *{
         *  Debug.Log("NO STONE (UPDATECALL)");
         *}*/
    }

    //If Object enters Trigger Area of BoxCollider
    private void OnTriggerEnter(Collider other)
    {
        float step = m_movementSpeed * Time.deltaTime;

        if (other.tag == "ritual_Stone")
        {
            stoneEntered = true;

            m_other = GameObject.FindGameObjectWithTag("ritual_Stone");
            m_otherTransform = m_other.transform;
        }
    }

    //If Object leaves Trigger Area of BoxCollider
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ritual_Stone")
        {
            stoneEntered = false;
        }
    }

    private bool stoneEntered { get; set; }
}
