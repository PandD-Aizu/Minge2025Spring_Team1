using CharacterInfo;
using EnhanceCard.Result;

namespace EnhanceCard
{
    public class AttackResult : IEnhanceResult
    {
        private int value;
        public int GetValue => value;

        public AttackResult(int value)
        {
            this.value = value;
        }
    }
}