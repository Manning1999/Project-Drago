using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonController : NPC, IInteractable
{

    bool hasHatched = false;

    [SerializeField]
    private float dragonAge;

    [SerializeField]
    private float flyingSkill, fireBreathingSkill, magicSkill;

    [SerializeField]
    private int mana, maxMana;

    [SerializeField]
    private int flyingAge, fireBreathingAge;

    [SerializeField]
    protected GameObject player = null;

    public enum BehaviourMode { Follow, Attack, Passive, Hunt, Idle, Playing, Eating }

    [SerializeField]
    protected BehaviourMode behaviourMode = BehaviourMode.Follow;

    public enum MovementType { Grounded, Flying, Falling, NoseDiving, Swimming }



    public override void InteractWith()
    {
        //Do something
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        agent = transform.GetComponent<NavMeshAgent>();

        if(hasHatched == false)
        {
            gameObject.SetActive(false);
        }
        
        if(player == null)
        {
            player = PlayerController.Instance.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(behaviourMode == BehaviourMode.Follow)
        {
            if(Vector3.Distance(transform.position, player.transform.position) >= 3.5f)
            {
                MoveToPlayersSide();
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


   

    [ContextMenu("Move To Player")]
    public void MoveToPlayersSide()
    {
        agent.SetDestination(player.transform.position + player.transform.right * -1.5f);
    }


    protected void AgeUp()
    {

    }


    

   
}
