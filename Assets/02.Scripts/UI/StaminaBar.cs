public class StaminaBar : BarBase
{
    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.Player.Stamina.OnChangedConditionEvent += UpdateBar;
    }
}
