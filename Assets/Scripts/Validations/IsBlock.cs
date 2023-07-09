using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsBlock : MonoBehaviour, IBlock
{
    [SerializeField]
    private BlockTypes blockType;

    public BlockTypes GetBlockType()
    {
        return blockType;
    }
}
