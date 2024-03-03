
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Card
{
    public class Character : ICanBeTargeted
    {
        public CharacterCard myCard;
        public string name;
        public int BasePower = 0;
        public int ModPower = 0;
        public int LostPower = 0;
        public int Power => BasePower + ModPower;
        public int BaseResolve = 0;
        public int ModResolve = 0;
        public int LostResolve = 0;
        public int Resolve => BaseResolve + ModResolve;
        public int BaseCondition = 0;
        public int ModCondition = 0;
        public int LostCondition = 0;
        public int Condition => BaseCondition + ModCondition;
        public int BaseHumanity = 5;
        public int ModHumanity = 0;
        public int LostHumanity = 0;
        public int Humanity => BaseHumanity + ModHumanity;
        public int orderInParty = 0;
        public List<Action> Actions = new List<Action>();
        public List<Trait> Traits = new List<Trait>();
        public string PortraitFileName;
        public string PortraitExpression 
            {
                get => portraitExpression; 
                set 
                { 
                    portraitExpression = value;
                portraitExpressionUpdated = true;
                }
            }
        private string portraitExpression = "Idle";
        public bool portraitExpressionUpdated = true;

        public Character()
        {

        }
        public void LoadSave(CharacterSave save)
        {
            name = save.name;
            BasePower = save.BasePower;
            BaseResolve = save.BaseResolve;
            BaseCondition = save.BaseCondition;
            BaseHumanity = save.BaseHumanity;
            LostPower = save.LostPower;
            LostResolve = save.LostResolve;
            LostCondition = save.LostCondition;
            LostHumanity = save.LostHumanity;
            List<TraitSave> _TraitSaveList = new(save.Traits);
            List<ActionSave> _ActionSaveList = new(save.Actions);
            Traits.Clear();
            Actions.Clear();
            foreach(TraitSave _traitsave in _TraitSaveList)
            {
                Traits.Add(Trait.TraitFromSave(_traitsave));
            }
            foreach(ActionSave _actionsave in _ActionSaveList)
            {
                Actions.Add(Action.LoadAction(_actionsave));
            }
        }
    }

    [Serializable] public struct CharacterSave
    {
        public string name;
        public int BasePower;
        public int BaseResolve;
        public int BaseCondition;
        public int BaseHumanity;
        public int LostPower;
        public int LostResolve;
        public int LostCondition;
        public int LostHumanity;
        public int OrderInParty;
        public ActionSave[] Actions;
        public TraitSave[] Traits;
        public CharacterSave(Character character)
        {
            name = character.name; 
            BasePower = character.BasePower;
            BaseResolve = character.BaseResolve;
            BaseCondition= character.BaseCondition;
            BaseHumanity = character.BaseHumanity;
            LostCondition = character.LostCondition;
            LostPower = character.LostPower;
            LostResolve = character.LostResolve;
            LostHumanity = character.LostHumanity;
            OrderInParty = character.orderInParty;
            List<ActionSave> _ActionList = new();
            List<TraitSave> _TraitList = new();
            foreach (Action _Act in character.Actions)
            {
                _ActionList.Add(new ActionSave(_Act));
            }
            foreach (Trait _Trait in character.Traits)
            {
                _TraitList.Add(new TraitSave(_Trait));
            }
            Actions = _ActionList.ToArray();
            Traits = _TraitList.ToArray();
        }
    }

    public interface ICanBeTargeted
    {

    }
}
