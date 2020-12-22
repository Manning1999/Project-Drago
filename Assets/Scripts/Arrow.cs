using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField]
    protected int damage;

    [SerializeField]
    private float deactivateTime = 10;

    Rigidbody rb = null;


    bool shot = false;


    protected void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if(rb == null) rb = transform.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        shot = false;
    }

    private void Update()
    {
        if (shot == true)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }


    [ContextMenu("Activate")]
    [ContextMenu("Activate")]
    public void Activate(float bowPower)
    {
    {                                      

        transform.SetParent(null);
        shot = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        rb.AddForce(transform.forward * (1000 * bowPower / 1.2f));

        StartCoroutine(DeactivateTimer());
    }

    protected IEnumerator DeactivateTimer()
    {
        yield return new WaitForSeconds(deactivateTime);
        Bow.DeactivateArrow(this);
        gameObject.SetActive(false);
    }

   

    protected void OnTriggerEnter(Collider col)
    {

        if (shot == true && (col.transform.tag != "Player" && this.transform.tag == "PlayerArrow"))
        {
            shot = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.parent = col.transform;

            if (col.transform.GetComponent<IHurtable>() != null)
            {
                col.transform.GetComponent<IHurtable>().Damage(damage);
            }
        }
    }
}
