public class HPBar : BarBase
{
    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.Player.Hp.OnChangedConditionEvent += UpdateBar;
    }
}
