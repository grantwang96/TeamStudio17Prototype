using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBomb : MonoBehaviour {

    public float range;
    public float lifeTime;
    private float measuredLife = 0f;

	// Update is called once per frame
	void Update () {
        measuredLife += Time.deltaTime;
        if(measuredLife >= lifeTime) { Die(); }
	}

    void Die()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, range);
        if(colls.Length > 0)
        {
            foreach(Collider2D coll in colls)
            {
                if (coll.tag != this.tag && (coll.tag.Contains("Player"))) { Destroy(coll.gameObject); }
                else if (coll.GetComponent<BreakableBlock>() != null) { coll.GetComponent<BreakableBlock>().DestroyBlock(99); }
            }
        }
        Destroy(gameObject);
    }
}
