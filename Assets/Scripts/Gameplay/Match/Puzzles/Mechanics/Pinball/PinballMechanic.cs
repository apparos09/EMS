using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RM_EM
{
    // The pinball mechanic.
    public class PinballMechanic : SpawnMechanic
    {
        [Header("Pinball")]

        // The spawn point for the balls.
        [Tooltip("Ball spawn point.")]
        public GameObject ballSpawn;

        // The pinball death zone. Every ball that's position is lower than this object is considered 'dead'.
        [Tooltip("The death zone of the balls. All balls below this y-pos are considered dead.")]
        public GameObject pinballDeathZone;

        // The pinball gate.
        public PinballGate gate;

        // Enables the hit sound for the pinball balls. Disabled because they're annoying.
        public bool hitSoundsEnabled = true;

        [Header("Pinball/Balls")]

        // The ball pool.
        public Queue<BallValue> ballPool = new Queue<BallValue>();

        // The ball prefab.
        public BallValue ballPrefab;

        // The value sprites for the ball.
        public PuzzleValueSprites valueSprites;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Set the puzzle type.
            puzzle.puzzle = Puzzle.puzzleType.pinball;
        }

        // Generates a ball.
        public BallValue GenerateBall(char value, Vector3 spawnPos)
        {
            // The ball value being generated.
            BallValue ball;

            // Checks if there are balls in the pool.
            if (ballPool.Count == 0)
            {
                // The new ball, which is generated from a prefab.
                ball = Instantiate(ballPrefab);
            }
            else // Re-use a deactivated ball.
            {
                ball = ballPool.Dequeue();
                ball.gameObject.SetActive(true);
            }

            // Set the mechanic and parent.
            ball.mechanic = this;
            ball.transform.parent = transform;

            // Sets the ball char and sprite.
            ball.SetValueAndSprite(value, valueSprites);

            // Sets the position, rotation, and velocity.
            ball.transform.position = spawnPos;
            ball.transform.rotation = Quaternion.identity;
            ball.rigidbody.velocity = Vector2.zero;

            // Add the ball to the value list.
            puzzleValues.Add(ball);

            return ball;
        }

        // Returns the ball.
        public void ReturnBall(BallValue ball)
        {
            // Reset rotation (not needed)
            ball.transform.rotation = Quaternion.identity;

            // Set the rigidbody to zero.
            ball.rigidbody.velocity = Vector2.zero;

            // Clear the list.
            ball.touchingBalls.Clear();

            // Disable.
            ball.gameObject.SetActive(false);

            // Put the ball back in the pool and remove it from the puzzle values list.
            ballPool.Enqueue(ball);
            puzzleValues.Remove(ball);
        }

        // Returns 'true' if the ball is in the death zone.
        public bool BallInDeathZone(BallValue ball)
        {
            return ball.transform.position.y < pinballDeathZone.transform.position.y;
        }

        // Updates the mechanic.
        public override void UpdateMechanic()
        {
            // Updates the spawn timer.
            bool spawn = UpdateSpawnTimer();

            // Checks if the values have been maxed out.
            bool maxedValues = HasPuzzleValuesCountReachedMax();

            // If a ball should be spawned.
            if (spawn && !maxedValues)
            {
                // Generate the ball.
                GenerateBall(puzzle.GetRandomPuzzleValue(), ballSpawn.transform.position);

                // Resets the spawn timer.
                ResetSpawnTimerToMax();
            }
        }

        // Resets the mechanic.
        public override void ResetMechanic()
        {
            // Goes through all the ball values.
            // This goes in reverse because the balls are being removed from the list.
            for (int i = puzzleValues.Count - 1; i >= 0; i--)
            {
                // Cast to Ball value.
                BallValue ball = (BallValue)puzzleValues[i];

                // Kill the ball.
                ball.Kill();
            }

            // Makes sure the gate is closed.
            gate.CloseGate();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}