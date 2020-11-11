using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//  Script containing logic for AI navigation and performing death event when damaged
public class ChildController : MonoBehaviour
{
    //  Components for navigation
    private CharacterController controller;
    private NavMeshAgent agent;

    //  Candy pickup dropped on death
    public GameObject pickupPrefab;

    //  Spawner of this entity
    public ChildSpawner parent;

    //  Sprites used for selecting random look at start
    public List<Sprite> kidSprites;

    //  Points to navigate to
    public List<GameObject> destinationPoints;

    //  Vars for death event
    private Vector3 velocity;
    private Vector3 deathLocation;
    private bool dead;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //  Select random sprite for look
        GetComponent<SpriteRenderer>().sprite = kidSprites[Random.Range(0, kidSprites.Count)];

        //  Set initial destination point
        Vector3 dest = destinationPoints[Random.Range(0, destinationPoints.Count - 1)].transform.position;

        //  Start moving
        agent.speed = Random.Range(0.5f, 2f);
        agent.SetDestination(dest);
    }

    public void DamageEvent()
    {
        //  Spawn candy pickup
        Vector3 candySpawn = gameObject.transform.position;
        Instantiate(pickupPrefab, candySpawn + new Vector3(0, 0.5f, 0), Quaternion.Euler(0,0,0));

        //  Initialize death
        dead = true;
        velocity = controller.velocity;
        deathLocation = transform.position + new Vector3((transform.position.x - PlayerController.instance.gameObject.transform.position.x) * 10f, 100f, 0);
    }

    private void FixedUpdate()
    {
        //  Billboard towards camera
        transform.LookAt(Camera.main.transform, -Vector3.up);
        transform.localEulerAngles = new Vector3(-transform.localEulerAngles.x, 0, 0);

        //  If dead, sent flying and destroyed
        //  Else, continue movement
        if (dead)
        {
            agent.enabled = false;
            transform.position = Vector3.SmoothDamp(transform.position, deathLocation, ref velocity, 1f);
            Destroy(gameObject, 5f);
        }
        else
        {
            if(Vector3.Distance(transform.position, agent.destination) < 1f)
            {
                Vector3 dest = destinationPoints[Random.Range(0, destinationPoints.Count - 1)].transform.position;
                agent.SetDestination(dest);
            }
        }
    }

    //  Alert parent spawner
    private void OnDestroy()
    {
        parent.currentChildCount -= 1;
        parent.SpawnChildren();
    }
}
