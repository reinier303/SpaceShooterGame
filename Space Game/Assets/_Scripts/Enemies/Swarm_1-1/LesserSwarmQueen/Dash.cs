using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : BaseState
{
    protected float postMoveWaitTime, timeBetweenDashes, dashTime, chargeTime, dashSpeed = 1;
    protected int dashes = 1;
    private SpriteRenderer spriteRenderer;


    public Dash(GameObject myGameObject, Transform playerTransform, ObjectPooler objectPoolerRef, BaseBoss bossScriptRef, System.Action nextMoveTypeRef, float postMoveTime, float dashHaltTime, float dashDuration, float chargeUpTime, float dashMovementMultiplier, int dashAmount) : base(myGameObject, playerTransform, objectPoolerRef, bossScriptRef, nextMoveTypeRef)
    {
        postMoveWaitTime = postMoveTime;
        timeBetweenDashes = dashHaltTime;
        dashTime = dashDuration;
        dashes = dashAmount;
        chargeTime = chargeUpTime;
        dashSpeed = dashMovementMultiplier;
        spriteRenderer = bossGameObject.GetComponent<SpriteRenderer>();
    }

    public override void PerformMove()
    {
        bossScript.StartCoroutine(PerformMoveIEnumerator());
    }

    private IEnumerator PerformMoveIEnumerator()
    {
        bossScript.StartCoroutine(ChargeAnimation());
        yield return new WaitForSeconds(chargeTime);
        for (int i = 0; i < dashes; i++)
        {
            bossScript.Speed.multiplier = dashSpeed;
            yield return new WaitForSeconds(dashTime);
            bossScript.Speed.multiplier = 1;
            yield return new WaitForSeconds(timeBetweenDashes);
        }
        yield return new WaitForSeconds(postMoveWaitTime);
        nextMoveType.Invoke();
    }

    protected virtual IEnumerator ChargeAnimation()
    {
        //Use LeanTween
        Color startColor = spriteRenderer.color;
        LeanTween.color(bossGameObject, new Color(1,0,0), chargeTime/2);
        yield return new WaitForSeconds(chargeTime / 2);
        LeanTween.color(bossGameObject, startColor, chargeTime / 2);
    }
}
