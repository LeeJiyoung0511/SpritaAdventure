public class StaminaBar : BarBase
{
    protected override void Start()
    {
        base.Start();
        GameManager.Instance.Player.Stamina.OnChangedConditionEvent += UpdateBar;
    }
}
