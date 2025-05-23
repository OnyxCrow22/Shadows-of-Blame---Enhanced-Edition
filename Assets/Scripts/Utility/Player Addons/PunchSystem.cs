using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSystem : MonoBehaviour
{
    [Header("Punching statistics")]
    public int damage;

    [Header("Punch References")]
    public GameObject FOV;
    RaycastHit punchHit;
    public LayerMask Enemy, NPC;
    public PlayerMovementSM playsm;

    private void Update()
    {
        InputCheck();
    }

    public void InputCheck()
    {
        if (playsm.pControls.Player.Attack.IsPressed() && !playsm.weapon.gunEquipped)
        {
            PunchSomething();
        }
    }

    private void PunchSomething()
    {
        float punchRange = 8;
        Ray punchRay = new Ray(FOV.transform.position, FOV.transform.forward);
        Debug.DrawRay(FOV.transform.position, FOV.transform.forward, Color.cyan);

        if (Physics.Raycast(punchRay, out punchHit, punchRange, Enemy | NPC))
        {
            IDamageable damageable = punchHit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.LoseHealth(damage);
                AudioManager.manager.Play("Punch");
            }
        } 
    }
}
