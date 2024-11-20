using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyBaseState
{
    private EnemyMovementSM esm;

    public EnemyShoot(EnemyMovementSM enemyStateMachine) : base("Shoot", enemyStateMachine)
    {
        esm = enemyStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        float DistToPlayer = Vector3.Distance(esm.enemy.transform.position, esm.target.position);

        if (esm.eHealth.health <= 65)
        {
            enemyStateMachine.ChangeState(esm.coverState);
            Debug.Log("HIDING!");
            esm.eAnim.SetBool("shoot", false);
            esm.eGun.gameObject.SetActive(false);
            esm.isShooting = false;
            esm.isHiding = true;
            AudioManager.manager.Stop("shootGun");
            AudioManager.manager.Play("walk");
        }

        if (esm.eHealth.health == 0)
        {
            esm.eHealth.StartCoroutine(esm.eHealth.Death());
        }

        if (!esm.playsm.weapon.gunEquipped && DistToPlayer >= esm.eGun.range || esm.playsm.weapon.gunEquipped && DistToPlayer >= esm.eGun.range)
        {
            enemyStateMachine.ChangeState(esm.chaseState);
            esm.eAnim.SetBool("shoot", false);
            esm.isChasing = true;
            esm.isShooting = false;
            esm.eGun.gameObject.SetActive(false);
            esm.agent.isStopped = false;
            AudioManager.manager.Stop("shootGun");
            AudioManager.manager.Play("sprinting");
        }

        if (esm.health.health <= 0)
        {
            enemyStateMachine.ChangeState(esm.patrolState);
            esm.eAnim.SetBool("playerDead", true);
            esm.isShooting = false;
            esm.isPatrol = true;
            AudioManager.manager.Stop("shootGun");
            AudioManager.manager.Play("walk");
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        esm.enemy.LookAt(esm.target);

        // Finds the distance between the enemy and the player
        Vector3 direction = esm.target.position - esm.enemy.transform.position;

        // Turns the enemy to face towards the player.
        esm.enemy.transform.rotation = Quaternion.Slerp(esm.enemy.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}
