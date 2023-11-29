using System;
using Common.CommonScripts;
using Unity.Mathematics;
using UnityEngine;

namespace Main_Controller
{
    public class PlayerBallCollisionController : MonoBehaviour
    {
        public static event Action OnPlayerCircleDestroyed;

        [SerializeField] private ParticleSystem deathParticles;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Tags.ObstacleTrigger))
            {
                DestroyCircle();
            }
        }

        private void DestroyCircle()
        {
            Instantiate(deathParticles, transform.position, quaternion.identity);
            Destroy(gameObject);
            OnPlayerCircleDestroyed?.Invoke();
        }
    }
}
