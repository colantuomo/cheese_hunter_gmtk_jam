using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBlock : MonoBehaviour, IBlock
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print($"Collision: {collision.transform.name}");
        EventsManager.Singleton.UseJumpBlock();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        EventsManager.Singleton.ResetBlockEffect();
    }

    public BlockTypes GetBlockType()
    {
        return BlockTypes.Jump;
    }

}
