using UnityEngine;

[CreateAssetMenu(fileName = "SpeedBoost", menuName = "Scriptable Object/SpeedBoost", order = int.MaxValue)]
public class SpeedBoost : ItemEffect
{
    public float AddSpeed;

    public override void StartEffect(Player player)
    {
        player.Controller.MoveSpeed += AddSpeed;
    }

    public override void EndEffect(Player player)
    {
        player.Controller.MoveSpeed = -AddSpeed;
    }
}
