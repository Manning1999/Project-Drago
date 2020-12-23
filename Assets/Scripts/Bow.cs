using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : InventoryItem
{

    public static List<Arrow> activeArrowPool = new List<Arrow>();
    public static List<Arrow> inactiveArrowPool = new List<Arrow>();

    [SerializeField]
    private GameObject arrowPrefab = null; 

    [SerializeField]
    private GameObject nockLocation = null;

    Arrow nockedArrow = null;

    Animator anim = null;

    [SerializeField]
    [Tooltip("This effects how quickly the bow gains power. It acts as a multiplier")]
    float powerGainRate = 1;

    float bowPower = 0f;
    [SerializeField]
    [Tooltip("This effects the bow's maximum range")]
    float maxBowPower = 2;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponent<Animator>();
        anim.SetFloat("DrawWeight", powerGainRate);
    }

    public void OnEnable()
    {
        if (anim == null) anim = transform.GetComponent<Animator>();
        anim.SetFloat("DrawWeight", powerGainRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEquipped == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Use();
            }
            if (Input.GetMouseButtonUp(0))
            {
                ReleaseBow();
            }
        }
    }

    public override void Equip(GameObject parent)
    {
        base.Equip(parent);
        transform.rotation = parent.transform.rotation;
        transform.GetComponent<Collider>().enabled = false;
    }

    public override void Unequip()
    {
        base.Unequip();
    }

    protected override void UseFunctionality()
    {
        DrawBow();
    }

    protected void DrawBow()
    {
        if (nockedArrow == null && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("Draw");
            NockArrow();
            StartCoroutine(BowPowerTimer());
        }
    }

    protected void ReleaseBow()
    {
        anim.SetTrigger("Release");
        nockedArrow.Activate(bowPower);
        bowPower = 0;
        nockedArrow = null;

    }

    private IEnumerator BowPowerTimer()
    {
        yield return new WaitForSeconds(0.1f);
        bowPower += powerGainRate / 10f;
        anim.SetFloat("Power", bowPower);

        if(nockedArrow != null && bowPower <= maxBowPower)
        {
            StartCoroutine(BowPowerTimer());
        }

    }



    private void NockArrow()
    {
        if(inactiveArrowPool.Count == 0)
        {
            GameObject newArrow = Instantiate(arrowPrefab, nockLocation.transform.position, Quaternion.identity);
            newArrow.transform.SetParent(nockLocation.transform, true);




            nockedArrow = newArrow.transform.GetComponent<Arrow>();
            nockedArrow.transform.localRotation = Quaternion.Euler(90, 0, 0);
            activeArrowPool.Add(nockedArrow);
            nockedArrow.transform.position = nockLocation.transform.position;


        }
        else
        {
            nockedArrow = inactiveArrowPool[0];
            nockedArrow.gameObject.SetActive(true);
            nockedArrow.transform.SetParent(nockLocation.transform, true);
            nockedArrow.transform.localRotation = Quaternion.Euler(90, 0, 0);
            activeArrowPool.Add(nockedArrow);
            nockedArrow.transform.position = nockLocation.transform.position;
            inactiveArrowPool.Remove(nockedArrow);
            

        }
    }

    public static void DeactivateArrow(Arrow arrowToDeactivate)
    {

        inactiveArrowPool.Add(arrowToDeactivate);
        activeArrowPool.Remove(arrowToDeactivate);

    }
}
