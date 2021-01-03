using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonController : NPC, IInteractable
{
    [SerializeField]
    bool hasHatched = false;

    StateMachine stateMachine;

    [SerializeField]
    private float dragonAge;

    [SerializeField]
    private float flyingSkill, fireBreathingSkill, magicSkill;

    [SerializeField]
    private int mana, maxMana, manaGainedPerLevel, healthGainedPerLevel;

    [SerializeField]
    private int flyingAge, fireBreathingAge;

    [SerializeField]
    protected GameObject player = null;

    public enum BehaviourMode { Follow, Attack, Passive, Hunt, Idle, Playing, Eating }

    [SerializeField]
    protected BehaviourMode behaviourMode = BehaviourMode.Follow;

    public enum MovementType { Grounded, Flying, Falling, NoseDiving, Swimming }

    [SerializeField]
    [Tooltip("Average height the dragon should fly at when cruising")]
    private float flightCruisingHeight = 50;

    [SerializeField]
    GameObject head = null;

    [SerializeField]
    GameObject fireSpawnPoint = null;

    [SerializeField]
    [Tooltip("This is how far away the dragon can breathe fire. This will increase as the dragon gets older and levels up uit's skills")]
    float fireBreathingRange = 3;

    Animator anim = null;


    [SerializeField]
    private bool isFlying = false;

    float distanceFromGround;
    public float _distanceFromGround { get { return distanceFromGround; } }



    public override void InteractWith()
    {
        //Do something
    }


    // Start is called before the first frame update
    protected void Awake()
    {



        stateMachine = new StateMachine();

        agent = transform.GetComponent<NavMeshAgent>();

        if(hasHatched == false)
        {
            gameObject.SetActive(false);
        }
        
        if(player == null)
        {
            player = PlayerController.Instance.gameObject;
        }

        rb = transform.GetComponent<Rigidbody>();

        anim = transform.GetComponent<Animator>();


        var goToPlayer = new GoToPlayer(this, agent);
        var idle = new Idle(this, agent, anim);

        var attack = new Attack(this, agent, anim);
        var moveToTarget = new GoToAttackTarget(this, agent, anim);
        var findAttackTarget = new FindAttackTarget(this, agent, enemiesInRange);
        var dragonTakeOff = new DragonTakeOff(this, agent, anim, rb);
        var dragonReachCruisingHeight = new DragonReachCruisingHeight(this, rb, anim, flightCruisingHeight, 3);
        var dragonFlightCircle = new DragonFlightCircle(this, anim, rb);


        At(moveToTarget, attack, reachedTarget());
        At(goToPlayer, idle, tooFarFromPlayer());
        At(idle, goToPlayer, reachedPlayer());

        At(findAttackTarget, moveToTarget, isInCombat());
        At(dragonTakeOff, dragonReachCruisingHeight, () => agent.enabled == false);
        At(dragonReachCruisingHeight, dragonFlightCircle, () => distanceFromGround >= flightCruisingHeight);
        At(dragonFlightCircle, dragonReachCruisingHeight, () => distanceFromGround <= flightCruisingHeight - 1);
        

        stateMachine.AddAnyTransition(findAttackTarget, () => attackTarget == null && enemiesInRange.Count >= 1);
        stateMachine.AddAnyTransition(dragonTakeOff, () => isFlying == true && agent.enabled != false);

        void At(IState to, IState from, Func<bool> condition) => stateMachine.AddTransition(to, from, condition);


        Func<bool> reachedPlayer() => () => Vector3.Distance(PlayerController.Instance.transform.position, transform.position) >= 4f;
        Func<bool> tooFarFromPlayer() => () => Vector3.Distance(PlayerController.Instance.transform.position, transform.position) <= 7f;

        Func<bool> isInCombat() => () => attackTarget != null;
        Func<bool> reachedTarget() => () => Vector3.Distance(attackTarget.transform.position, transform.position) <= 1.5f;

        stateMachine.SetState(idle);

    }



   


    


    // Update is called once per frame
    void Update()
    {
        
        stateMachine.Tick();

        if(isFlying == true)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                distanceFromGround = hit.distance;
            }
        }

    }


    public void Hatch()
    {
        hasHatched = true;
        gameObject.SetActive(true);

    }


    public void SetName(string nameToSet)
    {
        name = nameToSet;
    }


    [ContextMenu("Age up")]
    protected void AgeUp()
    {
        transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
        dragonAge++;
        maxMana += manaGainedPerLevel;
        maxHealth += healthGainedPerLevel;
        health = maxHealth;
    }


    /// <summary>
    /// The will make the dragon breathe fire at the specified target
    /// </summary>
    /// <param name="target"></param>
    protected void BreatheFire(Vector3 target)
    {
        if(Vector3.Distance(fireSpawnPoint.transform.position, target) >= fireBreathingRange)
        {

        }

    }

    [ContextMenu("Test agent")]
    public void TestAgent()
    {
        agent.SetDestination(player.transform.position);
    }


    [ContextMenu("GetState")]
    public void GetState()
    {
        Debug.Log(stateMachine.GetState());
    }


  
   

   
}
