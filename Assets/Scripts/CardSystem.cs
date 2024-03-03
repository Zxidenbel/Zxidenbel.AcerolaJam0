using System.Collections.Generic;
using UnityEngine;

namespace Card {
    public readonly struct Position
    {
        public readonly int index;
        public readonly ICardContainer Container;
        public Position(ICardContainer Container, int index)
        {
            this.Container = Container;
            this.index = index;
        }
        public Vector3 WorldTransform()
        {
            return Container.CardPosition(index);
        }
    }

    public class NoContainer : ICardContainer
    {
        public Vector3 Transform;
        Vector3 ICardContainer.Position => Transform;
        public NoContainer(GameObject gameObject)
        {
            Transform = gameObject.transform.position;
        }
        Vector3 ICardContainer.CardPosition(int index) => Transform;
        void ICardContainer.CardRemoved(ICard card) { return; }
        void ICardContainer.CardInserted(int index, Card.ICard card) { return; }
    }

    public class Deck : ICardContainer
    {
        public Vector3 transform;
        public List<ICard> Cards = new List<ICard>();
        Vector3 ICardContainer.Position => transform;
        public float xOff = 0f;
        public float yOff = 0.02f;
        public float zOff = 0.05f;
        public Deck(Vector3 transform, float xOff, float yOff, float zOff)
        {
            this.transform = transform;
            this.xOff = xOff;
            this.yOff = yOff;
            this.zOff = zOff;
        }
        public Deck(Vector3 transform)
        {
            this.transform = transform;
        }
        public void ShuffleCards()
        {
            List<ICard> _newCards = new List<ICard>();
            for (int i = 0; i < Cards.Count; i++)
            {
                int _selectIndex = Random.Range(0, Cards.Count);
                _newCards.Add(Cards[_selectIndex]);
                Cards.RemoveAt(_selectIndex);
            }
            Cards = _newCards;
        }
        public ICard Draw()
        {
            return Cards[0];
        }
        Vector3 ICardContainer.CardPosition(int index)
        {
            int _inverseIndex = Cards.Count - index;
            return transform + new Vector3(xOff, yOff, zOff) * _inverseIndex;
        }
        void ICardContainer.CardInserted(int index, Card.ICard card)
        {
            Cards.Insert(index, card);
            foreach (ICard remainingCard in Cards)
            {
                remainingCard.SetPosition(new Position(this, Cards.IndexOf(remainingCard)));
            }
        }
        void ICardContainer.CardRemoved(Card.ICard card)
        {
            Cards.Remove(card);
            foreach (ICard remainingCard in Cards)
            {
                remainingCard.SetPosition(new Position(this, Cards.IndexOf(remainingCard)));
            }
        }
    }

    public class Stack : ICardContainer
    {
        private Vector3 transform;
        public List<ICard> Cards = new();
        Vector3 ICardContainer.Position => transform;
        public float xOff = 0f;
        public float yOff = -0.5f;
        public float zOff = 0.05f;

        public Stack(Vector3 transform, float xOff, float yOff, float zOff)
        {
            this.transform = transform;
            this.xOff = xOff;
            this.yOff = yOff;
            this.zOff = zOff;
        }
        public Stack(Vector3 transform)
        {
            this.transform = transform;
        }
        public Stack(Vector3 transform, List<ICard> cards)
        {
            this.transform = transform;
            this.Cards = cards;
        }
        Vector3 ICardContainer.CardPosition(int index)
        {
            int _inverseIndex = Cards.Count - index;
            return transform + new Vector3(xOff, yOff, zOff) * _inverseIndex;
        }
        void ICardContainer.CardInserted(int index, Card.ICard card)
        {
            Cards.Insert(index, card);
            foreach (ICard remainingCard in Cards)
            {
                remainingCard.SetPosition(new Position(this, Cards.IndexOf(remainingCard)));
            }
        }
        void ICardContainer.CardRemoved(Card.ICard card)
        {
            Cards.Remove(card);
            foreach (ICard remainingCard in Cards){
                remainingCard.SetPosition(new Position(this, Cards.IndexOf(remainingCard)));
            }
        }
    }

    public class Space : ICardContainer
    {
        Vector3 ICardContainer.Position { get => transform; }
        private Vector3 transform;
        public Space(Vector3 transform)
        {
            this.transform = transform;
        }

        Vector3 ICardContainer.CardPosition(int index)
        {
            return transform;
        }
        void ICardContainer.CardRemoved(Card.ICard card)
        {
            return;
        }
        void ICardContainer.CardInserted(int index, Card.ICard card)
        {
            return;
        }
    }

    public interface ICard
    {
        public Position Position { get; }
        public void MoveToPosition(Position pos);
        public void SetPosition(Position pos);
        public virtual void OnDestroy()
        {
            Position.Container.CardRemoved(this);
        }
    }

    public interface ICardContainer
    {
        public Vector3 Position { get; }
        public Vector3 CardPosition(int index);
        public abstract void CardRemoved(ICard card);
        public abstract void CardInserted(int index, ICard card);
    }
}
