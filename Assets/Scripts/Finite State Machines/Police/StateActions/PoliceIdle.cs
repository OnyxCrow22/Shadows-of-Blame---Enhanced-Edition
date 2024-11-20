using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoliceIdle : PoliceBaseState
{
    private PoliceMovementSM wanted;

    public PoliceIdle(PoliceMovementSM policeMachine) : base("PoliceIdle", policeMachine)
    {
        wanted = policeMachine;
    }

    public override void Enter()
    {
        
    }

    public override void UpdateLogic()
    {
        if (Vector3.Distance(wanted.player.transform.position, wanted.PoliceAI.transform.position) >= 0.5f && Vector3.Distance(wanted.player.transform.position, wanted.PoliceAI.transform.position) < 120)
        {
            policeMachine.ChangeState(wanted.patrolState);
            wanted.PoliceAnim.SetBool("walking", true);
            wanted.isPatrolling = true;
        }
        if (wanted.pHealth.health == 0)
        {
            wanted.pHealth.StartCoroutine(wanted.pHealth.PoliceDeath());
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
