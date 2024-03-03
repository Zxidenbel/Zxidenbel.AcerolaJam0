using Card;
using System.Collections.Generic;
using System;

namespace Card
{
    public abstract partial class Trait
    {
        public string name { get; protected set; }
        public int turns = -1;
        public Character Owner = null;
        public string type { get; protected set; }
        public string description { get; protected set; }

        public virtual void updateModifications(Character character) { return; }
        public virtual void turnEnd(Character character) { return; }
        public virtual void cardsDrawn(Character character, List<ICard> cards) { return; }
        public virtual void didAction(Character character, Card.Action action) { return; }
        public static Trait TraitFromSave(TraitSave save)
        {
            if (save.name == "Aberrant")
            {
                return new Trait.Aberrant();
            }
            return null;
        }
    }

    [Serializable]
    public struct TraitSave
    {
        public TraitSave(Trait trait)
        {
            name = trait.name;
            turns = trait.turns;
        }
        public string name;
        public int turns;
    }
}