using UnityEngine;

namespace UI.Transitions
{
    public class ThirdPointTransition : MonoBehaviour
    {
        [SerializeField] private RectTransform transformUI;
        
        [SerializeField] private Transform firstPoint;
        [SerializeField] private Transform secondPoint;
        [SerializeField] private Transform thirdPoint;

        [SerializeField] private bool onStartTransition;
        
        [SerializeField] private float duration = 1f;
        
        private void Start()
        {
            if (onStartTransition)
            {
                ToFirstTransition();
            }
        }

        public void ToFirstTransition()
        {
            Transition.MakeTransitionFromTo(transformUI, firstPoint.position, secondPoint.position, duration);
        }

        public void FromFirstTransition()
        {
            Transition.MakeTransitionFromTo(transformUI, secondPoint.position, firstPoint.position, duration);
        }
        
        public void ToSecondTransition()
        {
            Transition.MakeTransitionFromTo(transformUI, secondPoint.position, thirdPoint.position, duration);
        }

        public void FromSecondTransition()
        {
            Transition.MakeTransitionFromTo(transformUI, thirdPoint.position, secondPoint.position, duration);
        }
    }
}
