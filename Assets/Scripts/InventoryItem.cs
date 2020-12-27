using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryItem : MonoBehaviour, IInteractable
{

    [SerializeField]
    protected string name;
    public string _name { get { return name; } protected set { name = value; } }

    [SerializeField]
    protected string description;
    public string _description { get { return description; } protected set { description = value; } }

    [SerializeField]
    protected float weight;
    public float _weight { get { return weight; } protected set { weight = value; } }

    [SerializeField]
    protected bool isEquipable = true;
    public bool _isEquipable { get { return isEquipable; } protected set { isEquipable = value; } }

    [SerializeField]
    protected bool dualWieldable = true;
    public bool _dualWieldable { get { return dualWieldable; } protected set { dualWieldable = value; } }

    [SerializeField]
    protected int monetaryValue;
    public int _monetaryValue { get { return monetaryValue; } protected set { monetaryValue = value; } }

    [SerializeField]
    protected bool isQuestItem = false;
    public bool _isQuestItem { get { return isQuestItem; } protected set { isQuestItem = value; } }

    public UnityEvent OnInteract = new UnityEvent();
    UnityEvent IInteractable.OnInteract { get { return OnInteract; } }

    protected bool isInteractable = true;
    public bool _isInteractable { get { return isInteractable; } protected set { isInteractable = value; } }

    protected bool isUsable = true;
    public bool _isUsable { get { return isUsable; } protected set { isUsable = value; } }

    [SerializeField]
    protected Sprite icon;

    Rigidbody rb = null;
    Collider coll = null;


    protected Mesh model;

    protected bool isEquipped = false;

    // Start is called before the first frame update
    void Start()
    {
        if (model == null)
        {
            model = transform.GetComponent<MeshFilter>().mesh;
        }
        if (rb == null)
        {
            rb = transform.GetComponent<Rigidbody>();
        }
        if (coll == null)
        {

            coll = transform.GetComponent<Collider>();
        }
    }

    public virtual void AddToInventory()
    {
        InventoryController.Instance.AddItemToInventory(transform.GetComponent<InventoryItem>());
        this.gameObject.SetActive(false);
    }

    public virtual void RemoveFromInventory()
    {
        //Do something when the item is removed
    }

    public virtual void Equip(GameObject parent)
    {
        if(isEquipable == true)
        {
            transform.position = parent.transform.position;
            transform.parent = parent.transform;
            isEquipped = true;
        }
    }

    public virtual void Unequip()
    {

    }

    public void InteractWith()
    {
        AddToInventory();
    }

    /// <summary>
    /// This checks whether the item is able to be used
    /// </summary>
    public virtual void Use()
    {
        if(isUsable == true)
        {
            UseFunctionality();
        }
    }

    public void SetIsUsable(bool set)
    {
        isUsable = set;
    }

    protected virtual void UseFunctionality()
    {
        //Do something
    }

    public void Drop(Vector3 positionToDrop)
    {
        gameObject.SetActive(true);
        transform.position = positionToDrop;
        
    }

}
