using UnityEngine;

namespace Card {
    public class SpacePoint : MonoBehaviour
    {
        public Space Space;
        [SerializeField]public string SpaceName;

        void Start()
        {
            Space = new Space(transform.position);
            ContainerRegistry.RegisterContainer(SpaceName, Space);
        }
    }
}
