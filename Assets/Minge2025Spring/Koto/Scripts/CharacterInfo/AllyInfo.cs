using UnityEngine;

namespace CharacterInfo
{
    [CreateAssetMenu(fileName = "AllyInfo", menuName = "CharacterInfo/AllyInfo")]
    public class AllyInfo : ScriptableObject
    {
        [SerializeField] private AttackType          attackType;
        [SerializeField] private CharacterDeployInfo characterDeployInfo;
        [SerializeField] private CharacterState      characterState;
        [SerializeField] private CharacterDirection  characterDirection;
        [SerializeField] private LayerMask           characterDeployableLayer;
        [SerializeField] private LayerMask           characterRaycastLayer;    
        [SerializeField] private Sprite              characterSprite;
        [SerializeField] private string              characterName;
        [SerializeField] private int                 cost;
        [SerializeField] private int                 maxBlockNum;
        [SerializeField] private int                 blockNum;
        [SerializeField] private int                 maxAttackNum;
        [SerializeField] private int                 maxHp;
        [SerializeField] private int                 hp;
        [SerializeField] private int                 attack;
        [SerializeField] private float               attackCoolDown;
        [SerializeField] private int                 defence;
        
        public AttackType AttackType                   { get => attackType; set => attackType = value; }
        public CharacterDeployInfo CharacterDeployInfo { get => characterDeployInfo; set => characterDeployInfo = value; }
        public CharacterState CharacterState           { get => characterState; set => characterState = value; }
        public CharacterDirection CharacterDirection   { get => characterDirection; set => characterDirection = value; }
        public LayerMask CharacterDeployableLayer      { get => characterDeployableLayer; }
        public LayerMask CharacterRaycastLayer         { get => characterRaycastLayer; }
        public Sprite CharacterSprite                  { get => characterSprite; set => characterSprite = value; }
        public string CharacterName                    { get => characterName; set => characterName = value; }
        public int Cost                                { get => cost; set => cost = value; }
        public int BlockNum                            { get => blockNum; set => blockNum = value; }
        public int MaxAttackNum                       { get => maxAttackNum; set => maxAttackNum = value; }
        public int MaxHp                               { get => maxHp; set => maxHp = value; }
        public int Hp                                  { get => hp; set => hp = value; }
        public int Attack                              { get => attack; set => attack = value; }
        public float AttackCoolDown                    { get => attackCoolDown; set => attackCoolDown = value; }
        public int Defence                             { get => defence; set => defence = value; }
        
        public void Init()
        {
            characterDeployInfo = CharacterDeployInfo.NOT_DEPLOYED;
            characterState = CharacterState.WAIT;
            characterDirection = CharacterDirection.UP;
            hp = maxHp;
        }
    }
}