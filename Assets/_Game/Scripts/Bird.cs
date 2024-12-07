using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts
{
    public class Bird : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float acceleration;
        [SerializeField] private float jumpSpeed;
        
        public State currentState;
        private float _stateTimeCount;
        private float _velocity;
        
        private void Update()
        {
            _stateTimeCount += Time.deltaTime;
            switch (currentState)
            {
                case State.None:
                    break;
                case State.Idle:
                    break;
                case State.Move:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _velocity = jumpSpeed;
                        PlayAnimationMoveUp();
                    }
                    _velocity += acceleration * Time.deltaTime;
                    transform.position += _velocity * Time.deltaTime * Vector3.up;
                    break;
                case State.Dead:
                    break;
            }
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            ChangeState(State.Dead);
        }

        public void ChangeState(State newState)
        {
            if (newState == currentState)
                return;
            _stateTimeCount = 0;
            // exit old state
            switch (currentState)
            {
                case State.None:
                    break;
                case State.Idle:
                    break;
                case State.Move:
                    break;
                case State.Dead:
                    break;
            }

            currentState = newState;
            
            switch (currentState)
            {
                case State.None:
                    break;
                case State.Idle:
                    PlayAnimationIdle();
                    break;
                case State.Move:
                    break;
                case State.Dead:
                    PlayAnimationDead();
                    break;
            }
        }

        private void PlayAnimationIdle()
        {
            animator.transform.rotation = Quaternion.identity;
            animator.Play("Bird");
        }

        private void PlayAnimationMoveDown()
        {
            DOTween.Kill("_MoveDown");
            DOTween.Sequence().SetId("_MoveDown")
                .Append(animator.transform.DOLocalRotate(Vector3.forward * -90, 0.5f));

        }

        private void PlayAnimationMoveUp()
        {
            /*var tween = animator.transform.DOLocalRotate(Vector3.forward * 30, 0.2f).OnComplete(() =>
            {
                PlayAnimationMoveDown();
            });*/
            DOTween.Kill("_MoveTween");
            DOTween.Kill("_MoveDown");
            DOTween.Sequence().SetId("_MoveTween")
                .Append(animator.transform.DOLocalRotate(Vector3.forward * 30, 0.2f))
                .AppendCallback(PlayAnimationMoveDown);
        }

        private void PlayAnimationDead()
        {
            DOTween.Kill("_MoveTween");
            DOTween.Kill("_MoveDown");
            animator.Play("Dead");
        }
       public enum State
       {
           None,
           Idle,
           Move,
           Dead
       }
    }
}
