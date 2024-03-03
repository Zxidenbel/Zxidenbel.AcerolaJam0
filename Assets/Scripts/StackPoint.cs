using Card;
using UnityEngine;

public class StackPoint : MonoBehaviour
{
    [SerializeField] private string Name;
    [SerializeField] Vector3 offset;
    void Start()
    {
        ContainerRegistry.RegisterContainer(Name, new Card.Stack(transform.position, offset.x, offset.y, offset.z));
    }
}
