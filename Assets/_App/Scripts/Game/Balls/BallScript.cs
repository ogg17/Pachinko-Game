using UnityEngine;

namespace VgGames.Game.Balls
{
    public class BallScript : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void StartBall()
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0;
            _rigidbody.simulated = true;
        }
    }
}