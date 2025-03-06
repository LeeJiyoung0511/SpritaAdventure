using UnityEngine;
using System.Collections;
using System;

public abstract class ItemEffect : ScriptableObject
{
    public float Duration; //지속시간
    public Action OnEndEffectEvent = delegate { }; //효과가 끝났을때 호출되는 이벤트

    //효과 적용
    public virtual void ApplyEffect(Player player)
    {
        player.StartCoroutine(IApplyEffect(player));
    }
    //효과 시작
    public abstract void StartEffect(Player player);
    //효과 끝
    public abstract void EndEffect(Player player);

    private IEnumerator IApplyEffect(Player player)
    {
        StartEffect(player);
        yield return new WaitForSeconds(Duration);
        EndEffect(player);
        OnEndEffectEvent();
    }
}
