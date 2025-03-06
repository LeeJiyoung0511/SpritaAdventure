using UnityEngine;

[CreateAssetMenu(fileName = "Invincible", menuName = "Scriptable Object/Invincible", order = int.MaxValue)]
public class Invincible : ItemEffect
{
    public override void StartEffect(Player player)
    {
        //레이어를 무적 상태로 변경
        player.gameObject.layer  = LayerMask.NameToLayer("Invincible");
    }

    public override void EndEffect(Player player)
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
