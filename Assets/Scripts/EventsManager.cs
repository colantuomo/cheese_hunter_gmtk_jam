using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Singleton { get; private set; }
    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Singleton = this;
        }
        DontDestroyOnLoad(this);
    }

    public event Action<BlockTypes> OnDeleteAItem;
    public void DeleteAItem(BlockTypes blockType)
    {
        OnDeleteAItem?.Invoke(blockType);
    }

    public event Action<float> OnUseFastForwardBlock;
    public void UseFastForwardBlock(float speed)
    {
        OnUseFastForwardBlock?.Invoke(speed);
    }

    public event Action OnUseJumpBlock;
    public void UseJumpBlock()
    {
        OnUseJumpBlock?.Invoke();
    }

    public event Action OnResetBlockEffect;
    public void ResetBlockEffect()
    {
        OnResetBlockEffect?.Invoke();
    }

    public event Action<Vector3> OnAddSelectedBlock;
    public void AddSelectedBlock(Vector3 blockPosition)
    {
        OnAddSelectedBlock?.Invoke(blockPosition);
    }

    public event Action<Vector3> OnTryAddSelectedBlock;
    public void TryAddSelectedBlock(Vector3 blockPosition)
    {
        OnTryAddSelectedBlock?.Invoke(blockPosition);
    }

    public event Action<BlockTypes> OnSelectBlock;
    public void SelectBlock(BlockTypes blockType)
    {
        OnSelectBlock?.Invoke(blockType);
    }

    public event Action<Dictionary<BlockTypes, int>> OnBlockDatabaseUpdated;
    public void BlockDatabaseUpdated(Dictionary<BlockTypes, int> database)
    {
        OnBlockDatabaseUpdated?.Invoke(database);
    }

    public event Action OnUseBackwardBlock;
    public void UseBackwardBlock()
    {
        OnUseBackwardBlock?.Invoke();
    }
}
