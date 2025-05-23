namespace EnhanceCard
{
    public interface ICardEffect
    {
        EnhanceResult GiveEffect(EnhanceParam param);
    }
}