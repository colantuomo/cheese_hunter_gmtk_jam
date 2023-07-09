using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EventsManager.Singleton.UseBackwardBlock();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        EventsManager.Singleton.ResetBlockEffect();
    }
}
