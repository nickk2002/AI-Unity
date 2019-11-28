using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Shoot : State<Enemy>
{
    private static Shoot instance = null;
    private static object padlock = new object();
    private float curentTime = 0;

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

    public override void Enter(Enemy owner)
    {
        curentTime = 0;
        owner.GetComponent<NavMeshAgent>().isStopped = true;
        
    }
    void Draw(Transform enemy,Transform player)
    {
        Debug.DrawLine(enemy.position, player.position,Color.green);
    }

    public override void Execute(Enemy owner)
    {

        if (curentTime >= owner.aiState.shootingDelay || curentTime == 0)
        {
            Debug.Log("Shoot");
            Draw(owner.transform, Player.Instance.transform);
            owner.aiState.lastSeendPlayer = Player.Instance.transform.position;
            owner.playerState.TakenDamageEvent.Invoke(owner.damage);
            curentTime = 0;
        }
        Debug.Log("wait to shoot");
        if (!owner.CanSeePlayer())
        { 
            Debug.Log("can't see player");
            owner.ChangeState(Alarm.Instance);
        }
        curentTime += Time.deltaTime;
    }

    public override void Exit(Enemy owner)
    {
        Debug.Log("Go to find him can't se him");
        owner.NavMeshAgent.isStopped = false;
    }
}
