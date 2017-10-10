using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ActionReplacingItem/InfinigunItem")]
public class InfinigunItem : ActionReplacingItem
{
    public float attackDuration;
    public float attackEndlag;
    public Vector2 attackOffset;
    public GameObject bulletPrefab;

    public float force;

    public override void RunFunction(Player caller)
    {
        if (caller.attackTimer <= 0)
        {
            GameObject newBullet = (GameObject)Instantiate(bulletPrefab, caller.transform.position + new Vector3(attackOffset.x * caller.facing, attackOffset.y), Quaternion.identity);
            newBullet.tag = caller.tag;
            newBullet.GetComponent<Rigidbody2D>().AddForce(
                (new Vector3(attackOffset.x * caller.facing, attackOffset.y)) * force,
                ForceMode2D.Impulse);
            caller.attackTimer = attackDuration + attackEndlag;
        }
    }
}

