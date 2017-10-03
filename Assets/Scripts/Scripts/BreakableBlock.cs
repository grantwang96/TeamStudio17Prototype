using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour, Block {
    public int breakPower;
	public void DestroyBlock(int power)
    {
        if(power > breakPower)
        {
            Destroy(gameObject);
        }
    }
}
