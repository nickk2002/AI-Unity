using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Shoot : State<Enemy>
{
    private Enemy enemy;

    public Shoot()
    {

    }
    void Draw(Transform enemy, Transform player)
    {
        Debug.Log("Shooting");
        Debug.DrawLine(enemy.position, player.position, Color.green);
    }
    IEnumerator SimulareShoot()
    {
        enemy.animator.SetBool("idle", true);
        enemy.NavMeshAgent.isStopped = true;
        while (enemy.CanSeePlayer()) // daca pun true nu se opreste corutina deloc
        {
           
            Draw(enemy.transform, Player.GameobjectInstance.transform);
            enemy.GiveDamage();
            yield return new WaitForSeconds(enemy.aiState.shootingDelay);
        }
    }
    IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(0.5f);
        enemy.ChangeState(enemy.GetParticularState(typeof(Alarm)));
    }
    public override void Enter(Enemy owner)
    {
        enemy = owner;
        GameController.Instance.StartCoroutine(SimulareShoot());
    }

    public override void Execute(Enemy owner)
    {
        if (!owner.CanSeePlayer())
        {
            GameController.Instance.StopCoroutine(SimulareShoot());
            GameController.Instance.StartCoroutine(ChangeState());
        }

    }

    public override void Exit(Enemy owner)
    { 
        owner.NavMeshAgent.isStopped = false;
        owner.animator.SetBool("idle", false);
    }
}
