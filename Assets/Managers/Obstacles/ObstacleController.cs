using UnityEngine;

namespace Obstacles
{
    public class ObstacleController : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float moveSpeed;
        
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.Translate(new Vector3(1, 0, 0) * (moveSpeed * Time.deltaTime));
        }
    }
}
