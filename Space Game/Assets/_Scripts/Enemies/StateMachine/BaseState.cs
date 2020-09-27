using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public BaseState(GameObject myGameObject, Transform playerTransform, ObjectPooler objectPoolerRef, BaseBoss bossScriptRef, System.Action nextMoveTypeRef)
    {
        bossGameObject = myGameObject;
        player = playerTransform;
        objectPooler = objectPoolerRef;
        bossScript = bossScriptRef;
        nextMoveType = nextMoveTypeRef;
    }

    protected GameObject bossGameObject;
    protected Transform player;
    protected ObjectPooler objectPooler;
    protected BaseBoss bossScript;
    protected System.Action nextMoveType;

    /// <summary>
    /// Make sure to call the move to next state at the end.
    /// </summary>
    public abstract void PerformMove();
}
