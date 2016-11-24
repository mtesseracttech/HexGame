using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{

    private int _index = 0;
    public Sprite[] Weapons;
    public Image WeaponImage;
    public Inventory Inventory;


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
            Inventory.RemoveItemFromInventory(0,1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Inventory.AddItem(0);
            Inventory.AddItem(1);
            Inventory.AddItem(2);
        }
    }

    private void WeaponSwap()
    {
        
        if (_index == 1)
        {
           // Debug.Log("First wepoan");
            WeaponImage.sprite = Weapons[0];
        }
        //check if we have a gun
        else if (_index == 2)
        {
            //Debug.Log("second weapon");
            WeaponImage.sprite = Weapons[1];
        }
        //check if we have a gun
        else if (_index == 3)
        {
           // Debug.Log("third weapon");
            WeaponImage.sprite = Weapons[2];
        }
        else if (_index == 4)
        {
            // Debug.Log("fourth weapon");
            WeaponImage.sprite = Weapons[3];
        }
        else if (_index == 5)
        {
            // Debug.Log("fifth weapon");
            WeaponImage.sprite = Weapons[4];
        }
        else if (_index >= 5)
            _index = 5;
        else if (_index <= 1)
            _index = 1;

      //  Debug.Log(_index);
    }
}
