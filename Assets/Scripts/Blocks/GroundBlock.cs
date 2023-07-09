using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlock : MonoBehaviour, IBlock
{
    public BlockTypes GetBlockType()
    {
        return BlockTypes.Ground;
    }
}
