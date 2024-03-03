using Card;
using TMPro;
using UnityEngine;

public class TraitCard : MonoBehaviour, ICard
{
    public Trait trait;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textOwner;
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TextMeshProUGUI textType;
    public Position position;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float moveLerp;

    Position ICard.Position { get => position; }
    private void Awake()
    {
        position = new Position(new NoContainer(transform.gameObject), 0);
    }

    void Update()
    {
        textName.text = trait.name;
        textOwner.text = trait.Owner.name;
        textDescription.text = trait.description;
        textType.text = trait.type;
        targetPosition = position.WorldTransform();
        if (targetPosition != transform.position)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, moveLerp);
            moveLerp += 3 * Time.deltaTime; // Lerp speed determined here
        }
    }
    void ICard.MoveToPosition(Position pos)
    {
        startPosition = transform.position;
        position.Container.CardRemoved(this);
        position = pos;
        moveLerp = 0;
        pos.Container.CardInserted(pos.index, this);
    }
    void ICard.SetPosition(Card.Position pos)
    {
        startPosition = transform.position;
        position = pos;
        moveLerp = 0;
    }
    private void OnDestroy()
    {
        (this as ICard).OnDestroy();
    }
}
