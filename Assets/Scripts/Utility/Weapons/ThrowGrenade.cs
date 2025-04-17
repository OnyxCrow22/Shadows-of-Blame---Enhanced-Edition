using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public float throwForce = 40;
    public GameObject grenade;
    public PlayerMovementSM playsm;
    public enum ThrowState { notReady, ready};
    private ThrowState currentThrow;
    public float delay = 20;
    Rigidbody rb;
    GameObject newGrenade;

    private void Update()
    {
        InputCheck();   
    }

    void InputCheck()
    {
        switch (currentThrow)
        {
            case ThrowState.ready:
                {
                    Throw();
                    playsm.throwingGrenade = true;
                    playsm.hasThrownGrenade = true;
                    currentThrow = ThrowState.ready;
                    break;
                }
            case ThrowState.notReady:
                {
                    playsm.throwingGrenade = false;
                    currentThrow = ThrowState.notReady;
                    break;
                }
        }
        if (playsm.pControls.Player.Throw.IsPressed() && !playsm.weapon.gunEquipped)
        {
            Throw();
            playsm.throwingGrenade = true;
            playsm.hasThrownGrenade = true;
        }

        if (!playsm.pControls.Player.Throw.IsPressed())
        {
            playsm.throwingGrenade = false;
        }

        delay -= Time.deltaTime;

        if (delay <= 0)
        {
            playsm.hasThrownGrenade = false;
            delay = 20;
        }
    }

    void Throw()
    {
        newGrenade = Instantiate(grenade, transform.position, transform.rotation);
        rb = newGrenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        Destroy(newGrenade, 2);
    }
}
