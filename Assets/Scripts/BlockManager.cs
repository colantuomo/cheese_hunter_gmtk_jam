using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _blockList = new();
    private BlockTypes _selectedBlock;
    private void Start()
    {
        EventsManager.Singleton.OnAddSelectedBlock += OnAddSelectedBlock;
        EventsManager.Singleton.OnSelectBlock += OnSelectBlock;
    }

    private void OnSelectBlock(BlockTypes block)
    {
        _selectedBlock = block;
    }

    private void OnAddSelectedBlock(Vector3 blockPosition)
    {
        _blockList.ForEach(block =>
        {
            if (block.TryGetComponent(out IsBlock isBlock))
            {
                if (isBlock.GetBlockType() == _selectedBlock)
                {
                    Instantiate(block, blockPosition, Quaternion.identity);
                }
            }
        });
    }
}
