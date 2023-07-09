using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _groundBlockCount, _jumpBlockCount, _fastForwardBlockCount, _backwardBlockCount;

    private void Start()
    {
        _groundBlockCount.text = 0.ToString();
        _jumpBlockCount.text = 0.ToString();
        _fastForwardBlockCount.text = 0.ToString();
        EventsManager.Singleton.OnBlockDatabaseUpdated += OnBlockDatabaseUpdated;
    }

    private void OnBlockDatabaseUpdated(Dictionary<BlockTypes, int> database)
    {
        foreach (BlockTypes block in database.Keys)
        {
            switch (block)
            {
                case BlockTypes.Ground:
                    _groundBlockCount.text = database[block].ToString();
                    break;
                case BlockTypes.FastForward:
                    _fastForwardBlockCount.text = database[block].ToString();
                    break;
                case BlockTypes.Jump:
                    _jumpBlockCount.text = database[block].ToString();
                    break;
                case BlockTypes.Backward:
                    _backwardBlockCount.text = database[block].ToString();
                    break;
            }
        }
    }

    public void SelectGroundBlock()
    {
        EventsManager.Singleton.SelectBlock(BlockTypes.Ground);
    }

    public void SelectJumpBlock()
    {
        EventsManager.Singleton.SelectBlock(BlockTypes.Jump);
    }

    public void SelectFastForwardBlock()
    {
        EventsManager.Singleton.SelectBlock(BlockTypes.FastForward);
    }

    public void SelectBackwardBlock()
    {
        EventsManager.Singleton.SelectBlock(BlockTypes.Backward);
    }
}
