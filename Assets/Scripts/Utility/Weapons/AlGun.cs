using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class AlGun : MonoBehaviour
{
    // AI Gun Statistics
    public float damage;
    public float timeBetweenShooting, spread, range = 30f, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, totalAmmo, bullet;
    int bulletsLeft;

    // Gun Bools
    bool readyToShoot, reloading;

    // Reference
    public GameObject enemy;
    public GameObject FOV;
    public GameObject eGun;
    public GameObject target;
    public LayerMask Player;
    private RaycastHit eHit;
    public EnemyMovementSM esm;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        if (readyToShoot && !reloading && bulletsLeft > 0)
        {
            ShootGun();
        }
    }

    public void ShootGun()
    {
        readyToShoot = false;
        Invoke("ResetShot", timeBetweenShooting);
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = FOV.transform.forward + new Vector3(x, y, 0);

        Ray shooteRay = new Ray(FOV.transform.position, direction);
        Debug.DrawRay(FOV.transform.position, direction, Color.red);

        if (Physics.Raycast(shooteRay, out eHit, range, Player) || (Physics.Raycast(shooteRay, out eHit, range)))
        {
            if (eHit.collider.CompareTag("Player"))
            {
                esm.health.LoseHealth(esm.health.healthLoss);
                esm.health.takingDamage = true;
            }
            else
            {
                esm.health.takingDamage = false;
            }

        }

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsLeft <= 0)
        {
            ReloadGun();
            AudioManager.manager.Stop("shootGun");
            AudioManager.manager.Play("reloading");
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void ReloadGun()
    {
        if (totalAmmo > 0 && !reloading)
        {
            esm.eAnim.SetBool("reloading", true);
            reloading = true;
            readyToShoot = false;
            Invoke("ReloadFinished", reloadTime);
        }
    }

    private void ReloadFinished()
    {
        int bulletsReloaded = Mathf.Min(magazineSize, totalAmmo);
        bulletsLeft = bulletsReloaded;
        totalAmmo -= bulletsReloaded;
        esm.eAnim.SetBool("reloading", false);
        AudioManager.manager.Stop("reloading");
        reloading = false;
        readyToShoot = true;
    }
}
