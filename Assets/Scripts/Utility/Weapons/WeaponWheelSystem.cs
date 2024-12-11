using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheelSystem : MonoBehaviour
{
    public WeaponWheelController weapons;
    public GameObject MainUI;
    public GameObject WeaponWheelPanel;
    public enum currentWeap { open, closed};
    private currentWeap weaponState;
    public bool open, closed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            switch (weaponState)
            {
                case currentWeap.closed:
                    {
                        weapons.WeaponWheel();
                        MainUI.SetActive(false);
                        WeaponWheelPanel.SetActive(true);
                        open = true;
                        closed = false;
                        weaponState = currentWeap.open;
                    }
                    break;
                case currentWeap.open:
                    {
                        weapons.CloseWheel();
                        MainUI.SetActive(true);
                        WeaponWheelPanel.SetActive(false);
                        open = false;
                        closed = true;
                        weaponState = currentWeap.closed;
                    }
                    break;
            }
        }
    }
}
