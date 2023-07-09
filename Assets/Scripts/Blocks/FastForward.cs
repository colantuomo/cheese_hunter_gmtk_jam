using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastForward : MonoBehaviour, IBlock
{
    [SerializeField]
    private float _newSpeed = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EventsManager.Singleton.UseFastForwardBlock(_newSpeed);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        EventsManager.Singleton.ResetBlockEffect();
    }

    public BlockTypes GetBlockType()
    {
        return BlockTypes.FastForward;
    }

}
