using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Shoot : State<Enemy>
{
    private static Shoot instance = null;
    private static object padlock = new object();
    private Enemy enemy;

    private Shoot()
    {

    }
    public static Shoot Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                    instance = new Shoot();
                return instance;
            }
        }
    }
    void Draw(Transform enemy, Transform player)
    {
        Debug.DrawLine(enemy.position, player.position, Color.green);
    }
    IEnumerator SimulareShoot()
    {
        while (true)
        {
            Draw(enemy.transform, Player.Instance.transform);
            enemy.playerState.TakenDamageEvent.Invoke(enemy.damage);
            yield return new WaitForSeconds(enemy.aiState.shootingDelay);
        }
    }
    IEnumerator UpdatePlayerPosition()
    {
        while (true)
        {
            if (enemy.CanSeePlayer())
            {
                Debug.Log("change position");
                enemy.SetLastSeenPlayer(Player.Instance.transform.position);
            }
            yield return new WaitForSeconds(0.5f);
        }    
    }
    public override void Enter(Enemy owner)
    {
        enemy = owner;
        owner.NavMeshAgent.isStopped = true;
        GameController.Instance.CallCoroutine(SimulareShoot());
        GameController.Instance.CallCoroutine(UpdatePlayerPosition());
    }

    public override void Execute(Enemy owner)
    {
        if (!owner.CanSeePlayer())
        {
            GameController.Instance.StopAllCoroutines();
            Debug.Log("can't see player");
            owner.ChangeState(Alarm.Instance);
        }
    }

    public override void Exit(Enemy owner)
    {
        owner.aiState.numberAlerted = 0;
        owner.NavMeshAgent.isStopped = false;
        GameController.Instance.StopAllCoroutines();
    }
}
