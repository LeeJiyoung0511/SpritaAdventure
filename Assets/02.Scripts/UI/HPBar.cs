public class HPBar : BarBase
{
    protected override void Start()
    {
        base.Start();
        GameManager.Instance.Player.Hp.OnChangedConditionEvent += UpdateBar;
    }
}
