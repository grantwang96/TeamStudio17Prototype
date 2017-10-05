using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ActionReplacingItem/CherryBombItem")]
public class CherryBombItem : ActionReplacingItem
{
    public float attackDuration;
    public float attackEndlag;
    public Vector2 attackOffset;
    public GameObject bomb;

    public float force;

    public override void RunFunction(Player caller)
    {
        if (caller.attackTimer <= 0)
        {
            GameObject newBomb = (GameObject)Instantiate(bomb, caller.transform.position + new Vector3(attackOffset.x * caller.facing, attackOffset.y), Quaternion.identity);
            newBomb.GetComponent<Rigidbody2D>().AddForce(
                (caller.transform.position + new Vector3(attackOffset.x * caller.facing, attackOffset.y)) * force,
                ForceMode2D.Impulse);
            caller.attackTimer = attackDuration + attackEndlag;
        }
    }
}

