using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemBase : MonoBehaviour, IInventoryItem {

    //[SerializeField] private LayerMask dropMask;

    // Define a name for the item (IInventoryItem)
    public virtual string Name {
        get {
            return gameObject.name;
        }
    }

    // Define a sprite for the item (IInventoryItem)
    public Sprite _image;
    public Sprite Image {
        get {
            return _image;
        }
    }

    // Define a sprite for the item (IInventoryItem)
    public Sprite _quality;
    public Sprite Quality {
        get {
            return _quality;
        }
    }

    // Define a sprite for the item (IInventoryItem)
    public Text _nameTag;
    public Text NameTag {
        get {
            return _nameTag;
        }
    }

    public Vector3 pickPosition;
    public Vector3 pickRotation;
    public Vector3 DropRotation;


    // Set nametag to name of object
    private void Start() {
        _nameTag.text = Name;
    }


    // On using an item method
    public virtual void OnUse() {
        transform.localPosition = pickPosition;
        transform.localEulerAngles = pickRotation;
    }


    // On Picking up an item method
    public virtual void OnPickup() {
        gameObject.SetActive(false);
    }


    // On Dropping an item method
    public virtual void OnDrop() {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000)) {
            gameObject.SetActive(true);
            gameObject.transform.Find("NameTagCanvas").gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
            gameObject.transform.eulerAngles = DropRotation;
        }
    }
}
