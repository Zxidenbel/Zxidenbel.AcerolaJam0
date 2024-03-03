using System;

namespace Card
{
    public abstract partial class Enemy : ICanBeTargeted
    {
        public EnemyCard myCard;
        public string name { get; protected set; }
        public int Power { get; protected set; }
        public int Condition { get; protected set; }
        public string PortraitFileName { get; protected set; }
        public static Enemy LoadEnemy(EnemySave enemySave)
        {
            return null;
        }

        public virtual void EnemyTurnStart() { }
    }

    [Serializable]
    public struct EnemySave
    {
        public string name;
        public EnemySave(Enemy enemy)
        {
            this.name = enemy.name;
        }
    }
}