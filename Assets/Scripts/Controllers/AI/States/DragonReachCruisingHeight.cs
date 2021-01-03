using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonReachCruisingHeight : IState
{


    private readonly DragonController npc;
    private Animator anim;
    Vector3 targetPosition;
    float flapRate;
    Rigidbody rb;
    float flapForce = 500;
    float cruisingHeight;


    public void OnEnter()
    {
        npc.StartCoroutine(FlapTimer());
        //Start flapping animation
    }

    public void OnExit()
    {

    }

    public void Tick()
    {

    }

    private IEnumerator FlapTimer()
    {
        yield return new WaitForSeconds(1 / flapRate);
        rb.AddForce(npc.transform.up * flapForce, ForceMode.Impulse);
        Debug.Log("Flap");
        if(npc._distanceFromGround <= cruisingHeight) npc.StartCoroutine(FlapTimer());

    }

    public DragonReachCruisingHeight(DragonController _npc, Rigidbody _rb, Animator _anim, float _cruisingHeight, float _flapRate)
    {
        
        npc = _npc;
        anim = _anim;
        flapRate = _flapRate;
        rb = _rb;
        cruisingHeight = _cruisingHeight;

    }
}
