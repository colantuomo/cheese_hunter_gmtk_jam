using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDatabase : MonoBehaviour
{

    private readonly Dictionary<BlockTypes, int> _blocksInventory = new();
    private BlockTypes _blockSelected;
    private void Start()
    {
        EventsManager.Singleton.OnTryAddSelectedBlock += TryAddSelectedBlock;
        EventsManager.Singleton.OnDeleteAItem += OnDeleteAItem;
        EventsManager.Singleton.OnSelectBlock += OnSelectBlock;
        InitDatabase();
    }

    private void InitDatabase()
    {
        _blocksInventory.Add(BlockTypes.Jump, 0);
        _blocksInventory.Add(BlockTypes.FastForward, 0);
        _blocksInventory.Add(BlockTypes.Ground, 0);
        _blocksInventory.Add(BlockTypes.Backward, 0);
    }

    private void OnSelectBlock(BlockTypes block)
    {
        _blockSelected = block;
    }

    private void TryAddSelectedBlock(Vector3 position)
    {
        if (_blocksInventory.TryGetValue(_blockSelected, out int value))
        {
            if (value <= 0)
            {
                return;
            }
            value--;
            _blocksInventory.Remove(_blockSelected);
            _blocksInventory.Add(_blockSelected, value);
        }
        else
        {
            _blocksInventory.Add(_blockSelected, 0);
        }
        EventsManager.Singleton.AddSelectedBlock(position);
        EmitDatabaseEvent();
    }

    private void OnDeleteAItem(BlockTypes blockType)
    {
        if (_blocksInventory.TryGetValue(blockType, out int value))
        {
            value++;
            if (value <= 0) value = 0;
            _blocksInventory.Remove(blockType);
            _blocksInventory.Add(blockType, value);
        }
        EventsManager.Singleton.SelectBlock(blockType);
        EmitDatabaseEvent();
    }

    private void EmitDatabaseEvent()
    {
        EventsManager.Singleton.BlockDatabaseUpdated(_blocksInventory);
    }
}
