using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IThrowable
{

    [SerializeField]
    private int baseDamage = 2;

    [SerializeField]
    [Tooltip("The higher it is the lower the damage will be. The formula is baseDamage * velocity / damageMultiplier")]
    private float damageMultiplier = 10;

    [SerializeField]
    private Rigidbody rb = null;
    public Rigidbody _rb { get => rb; set { rb = value; } }

    [SerializeField]
    private AnimationClip cliptToPlayBeforeThrowing = null;
    public AnimationClip _clipToPlayBeforeThrowing { get => cliptToPlayBeforeThrowing; set { cliptToPlayBeforeThrowing = value; } }

    [SerializeField]
    private Animator anim = null;
    public Animator _anim { get => anim; set { anim = value; } }

    [SerializeField]
    protected bool isThrowable = true;
    public bool _isThrowable { get => isThrowable; set { isThrowable = value; } }

    [SerializeField]
    protected int maxCollisions = 3;
    protected int currentCollisions = 0;

    [SerializeField]
    float maxLifetime = 10;

    private void OnEnable()
    {

    }

 
    public IEnumerator PlayAnimation(Vector3 startPosition, Vector3 locationToThrowTo, float throwForce)
    {
        
        yield return null;
    }

    public void Throw(Vector3 startPosition, Vector3 locationToThrowTo, float throwForce)
    {
        Debug.Log("Activated");
    }

   
    private void OnCollisionEnter(Collision col)
    {
        //Increase the number of collisions
        currentCollisions++;

        //If the rock hits something that can be hurt then it will hurt the hit object and deactivate itself (Eventually the amount of damage done will be dependant on the velocity of the rock)
        if(col.transform.GetComponent<IHurtable>() != null)
        {

            Debug.Log(baseDamage + " : " + rb.velocity.magnitude + " : " + (baseDamage * rb.velocity.magnitude / damageMultiplier));
            col.transform.GetComponent<IHurtable>().Damage((int)(baseDamage * rb.velocity.magnitude / damageMultiplier));
            thrautha_Throw.DeactivateRock(this.gameObject);
            gameObject.SetActive(false);
        }


        //If the rock has hit the maximum number of items it will deactivate itself
        if(currentCollisions == maxCollisions)
        {
            thrautha_Throw.DeactivateRock(this.gameObject);
            gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// If the rock hasn't hit the maximum number of items within it's lifetime then it wil wait until the end of it's lifetime and then deactivate itself
    /// </summary>
    /// <returns></returns>
    private IEnumerator LifetimeTimer()
    {
        yield return new WaitForSeconds(maxLifetime);
        thrautha_Throw.DeactivateRock(this.gameObject);
        gameObject.SetActive(false);
    }




}
