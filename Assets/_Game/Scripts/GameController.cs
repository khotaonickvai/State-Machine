using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Bird bird;
        private State _currentState;

        private static GameController _instance;

        public static GameController Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            ChangeState(State.Begin);
        }

        private void Update()
        {
            UpdateCurrentState();
        }

        private void UpdateCurrentState()
        {
            switch (_currentState)
            {
                case State.None:
                    break;
                case State.Begin:
                    if(Input.GetKeyDown(KeyCode.Space))
                        ChangeState(State.Play);
                    break;
                case State.Play:
                    if (bird.currentState == Bird.State.Dead)
                    {
                        ChangeState(State.GameOver);
                    }
                    break;
                case State.GameOver:
                    if(Input.GetKeyDown(KeyCode.Space))
                        ChangeState(State.Begin);
                    break;
            }
        }

        private void ChangeState(State newState)
        {
            if (newState == _currentState)
                return;
            // exit current state

            switch (_currentState)
            {
                case State.None:
                    break;
                case State.Begin:
                    break;
                case State.Play:
                    break;
                case State.GameOver:
                    break;
            }

            _currentState = newState;
            switch (newState)
            {
                case State.None:
                    break;
                case State.Begin:
                    ResetBird();
                    break;
                case State.Play:
                    bird.ChangeState(Bird.State.Move);
                    break;
                case State.GameOver:
                    break;
            }
        }

        private void ResetBird()
        {
            Debug.Log("reset bird");
            var birdTransform = bird.transform;
            birdTransform.position = Vector3.zero;
            birdTransform.rotation = Quaternion.identity;
            bird.ChangeState(Bird.State.Idle);
        }
        private enum State
        {
            None,
            Begin,
            Play,
            GameOver
        }
    }
}
