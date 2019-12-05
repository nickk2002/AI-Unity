using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColor : State<Enemy>
{
    private static readonly object padlock = new object();
    private static SwitchColor instance = null;
    private Color color;
    private Enemy enemy;
    private bool started = false;
    private bool finished = false;

    public SwitchColor()
    {
        
    }
    public override void Enter(Enemy owner)
    {
        enemy = owner;
        started = false;
        finished = false;
        color = new Color(Random.value, Random.value, Random.value);
    }
    IEnumerator Wait()
    {
        Debug.Log("called coroutine");
        Debug.Log("trying to exit at time " + Time.time);

        if (started == false)
        {
            started = true;
            enemy.animator.SetBool("idle", true);
            enemy.NavMeshAgent.isStopped = true;
            yield return new WaitForSeconds(enemy.aiState.patrolWaitTime);
            enemy.NavMeshAgent.isStopped = false;
            enemy.animator.SetBool("idle", false);
            Debug.Log("exiting at time " + Time.time);
            finished = true;
        }
    }
    public override void Execute(Enemy owner)
    {
        owner.UpdateLight(color);
        
        if (started == false)
            GameController.Instance.StartCoroutine(Wait());
        if (finished == true)
        {
            Debug.Assert(owner.NavMeshAgent.isStopped == false, "enemy : " + owner.gameObject + started + "Nav mesh Agent coroutine has not finished corectly!");
            owner.ChangeState(owner.GetParticularState(typeof(Patrol)));
        }
    }

    public override void Exit(Enemy owner)
    {
        
    }
}
