using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColor : State<Enemy>
{
    private Color color;
    private Enemy enemy;

    public override void Enter(Enemy owner)
    {
        enemy = owner;
        color = new Color(Random.value, Random.value, Random.value);
        GameController.Instance.StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        enemy.animator.SetBool("idle", true);
        enemy.NavMeshAgent.isStopped = true;
        yield return new WaitForSeconds(enemy.aiState.patrolWaitTime);
        enemy.ChangeState(enemy.GetParticularState(typeof(Patrol)));
    }
    public override void Execute(Enemy owner)
    {
        owner.UpdateLight(color);
    }

    public override void Exit(Enemy owner)
    {
        enemy.NavMeshAgent.isStopped = false;
        enemy.animator.SetBool("idle", false);
    }
}
