using EnhanceCard.Param;
using EnhanceCard.Result;

namespace EnhanceCard
{
    public interface ICardEffect
    {
        IEnhanceResult GiveEffect(IEnhanceParam param);
    }
}