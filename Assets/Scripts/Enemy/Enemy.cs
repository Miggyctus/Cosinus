using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 lastKnownPos;
    private Target enemyTarget;
    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    public Vector3 LastKnownPos { get => lastKnownPos; set => lastKnownPos = value; }
    private float currentHealth; 
    public Path path;
    [Header("Sight Values")]
    public float sightDistance = 20f;
    public float fieldOfview = 85f;
    public float eyeHeight;
    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;
    //just for debugging
    [SerializeField] private string currentState;
    void Start()
    {
        path = GameObject.Find("Path").GetComponent<Path>();
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        enemyTarget = GetComponent<Target>();
        currentHealth = enemyTarget.health;
        stateMachine.Initialise();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
        if(enemyTarget.TookDamage())
        {
            transform.LookAt(player.transform);
            enemyTarget.setDamaged(false);
        }
    }
    public bool CanSeePlayer()
    {
        if(player != null)
        {
            //is the player close enough to be seen
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if(angleToPlayer >= -fieldOfview && angleToPlayer <= fieldOfview)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if(Physics.Raycast(ray, out hitInfo, sightDistance)) 
                    { 
                        if(hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);

                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
