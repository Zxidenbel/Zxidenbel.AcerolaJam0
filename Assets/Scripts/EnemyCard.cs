using Card;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyCard : MonoBehaviour, ICard
{
    [SerializeField] public Enemy Enemy;
    [SerializeField] private UnityEngine.UI.Image Portrait;
    [SerializeField] private UnityEngine.UI.Image Status;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textPower;
    [SerializeField] private TextMeshProUGUI textResolve;
    [SerializeField] private TextMeshProUGUI textCondition;
    [SerializeField] private SpriteAtlas enemyPortraitAtlas; //testing
    [SerializeField] private SpriteAtlas statusIcons;
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float moveLerp;
    private Position Position;
    Position ICard.Position { get => Position; }

    private void Awake()
    {
        Enemy.myCard = this;
        Position = new Position(new NoContainer(transform.gameObject), 0);
    }

    void Update()
    {
        Portrait.sprite = enemyPortraitAtlas.GetSprite(Enemy.PortraitFileName);
        textName.text = Enemy.name;
        textPower.text = Enemy.Power.ToString();
        textCondition.text = Enemy.Condition.ToString();
        targetPosition = Position.WorldTransform();
        if (targetPosition != transform.position)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, moveLerp);
            moveLerp += 3 * Time.deltaTime; // Lerp speed determined here
        }
    }

    void ICard.MoveToPosition(Position pos)
    {
        startPosition = transform.position;
        Position.Container.CardRemoved(this);
        Position = pos;
        moveLerp = 0;
        Position.Container.CardInserted(pos.index, this);
    }
    void ICard.SetPosition(Card.Position pos)
    {
        startPosition = transform.position;
        Position = pos;
        moveLerp = 0;
    }
    private void OnDestroy()
    {
        (this as ICard).OnDestroy();
    }
}
