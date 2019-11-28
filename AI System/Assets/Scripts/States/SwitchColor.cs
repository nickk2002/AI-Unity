using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColor : State<Enemy>
{
    private static readonly object padlock = new object();
    private static SwitchColor instance = null;
    private Color color;

    private SwitchColor()
    {
        
    }
    public static SwitchColor Instance
    {
        get
        {
            lock(padlock){
                if (instance == null)
                {
                    instance = new SwitchColor();
                }
            }
            return instance;
        }
    }
    public override void Enter(Enemy owner)
    {
        color = new Color(Random.value, Random.value, Random.value);
    }

    public override void Execute(Enemy owner)
    {
        owner.UpdateLight(color);
        owner.ChangeState(Patrol.Instance);
    }

    public override void Exit(Enemy owner)
    {
       
    }
}
