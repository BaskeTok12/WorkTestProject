using UI;
using UI.Scale_Manager;
using UnityEngine;

namespace Obstacles
{
    public class ObstacleController : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float moveSpeed;
        [Header("For Scaling")]
        [SerializeField] private Scaler scaler;
        
        private void Update()
        {
            Move();
        }

        private void OnEnable()
        {
            ScaleManager.OnObstaclesNeedToScale += scaler.ScaleToZero;
        }

        private void OnDisable()
        {
            ScaleManager.OnObstaclesNeedToScale -= scaler.ScaleToZero;
        }

        private void Move()
        {
            transform.Translate(new Vector3(1, 0, 0) * (moveSpeed * Time.deltaTime));
        }
    }
}
