using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{

    private int _index = 0;
    public Sprite[] weapons;
    public Image weaponImage;
    private Inventory _inventory;

    void Start()
    {
        _inventory = GetComponent<Inventory>();
    }

    public void LeftArrowClickChangeWeapon(bool clicked)
    {
        if (clicked)
        {
            _index++;
        }
     }

    public void RightArrowClickChangeWeapon(bool clicked)
    {
        if (clicked)
        {
            _index--;
        }
    }

    void FixedUpdate()
    {
        WeaponSwap();
        if (Input.GetKeyDown(KeyCode.A))
        {
            _inventory.RemoveItemFromInventory(5,1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _inventory.AddItem(5);
            _inventory.AddItem(6);
            _inventory.AddItem(7);
        }
    }

    private void WeaponSwap()
    {
        
        if (_index == 1)
        {
            Debug.Log("First wepoan");
            weaponImage.sprite = weapons[0];
        }
        //check if we have a gun
        else if (_index == 2)
        {
            Debug.Log("second weapon");
            weaponImage.sprite = weapons[1];

        }
        //check if we have a gun
        else if (_index == 3)
        {
            Debug.Log("third weapon");
            weaponImage.sprite = weapons[2];

        }
        else if (_index >= 3)
            _index = 3;
        else if (_index <= 1)
            _index = 1;

        Debug.Log(_index);
    }
}
