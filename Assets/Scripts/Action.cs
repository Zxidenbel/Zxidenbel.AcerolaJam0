using Card;
using System;
using UnityEngine;

namespace Card
{
    public abstract partial class Action
    {
        public Character Owner { get; protected set; }
        public Color color { get; protected set; }
        public string name { get; protected set; }
        public string type { get; protected set; }
        public string description { get; protected set; }
        public ICanBeTargeted target { get; set; }

        public abstract void executeAction();
        public static Action LoadAction(ActionSave save)
        {
            return null;
        }
    }

    [Serializable]
    public struct ActionSave
    {
        public string name;
        public ActionSave(Action action)
        {
            name = action.name;
        }
    }
}