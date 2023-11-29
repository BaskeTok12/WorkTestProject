using DG.Tweening;
using UnityEngine;

namespace Common.CommonScripts
{
    public class Lifetime : MonoBehaviour
    {
        [Header("Lifetime")] 
        [SerializeField] private float maximumLifeTime;

        private void Start()
        {
            DOVirtual.DelayedCall(maximumLifeTime, () => Destroy(gameObject));
        }
    }
}