using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Bird : Agent
{
    private const float COLLIDER_DISTANCE = 2.0f;

    private Rigidbody2D _rigidbody;
    private bool _isPressed;
    [SerializeField]
    private float _forceFactor = 4;

    public PipeSet pipes;
    public float counter;

    private void Update()
    {
        counter += Time.deltaTime;
    }

    public override void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void UpwardForce()
    {
        _rigidbody.AddForce(Vector2.up * _forceFactor, ForceMode2D.Impulse);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        AddReward(Time.fixedDeltaTime);

        int tap = Mathf.FloorToInt(actions.DiscreteActions[0]);

        if (tap == 0)
        {
            _isPressed = false;
        }
        if (tap == 1 && !_isPressed)
        {
            UpwardForce();
            _isPressed = true;
        }
    }
    public override void CollectObservations(VectorSensor vs)
    {
        Vector2 nextPipePos = pipes.GetNextPipe().localPosition;
     
        float vel = Mathf.Clamp(_rigidbody.velocity.y, -COLLIDER_DISTANCE, COLLIDER_DISTANCE);

        vs.AddObservation(transform.localPosition.y / COLLIDER_DISTANCE);
        vs.AddObservation(vel / COLLIDER_DISTANCE);
        vs.AddObservation(nextPipePos.y / COLLIDER_DISTANCE);
        vs.AddObservation(nextPipePos.x);
        vs.AddObservation(_isPressed ? 1f : -1f);
    }
    public override void OnEpisodeBegin()
    {
        _rigidbody.velocity = Vector2.zero;
        transform.localPosition = Vector2.zero;
        counter = 0f;
        pipes.ResetPosition();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetReward(-1);
        EndEpisode();
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        actionsOut.DiscreteActions.Array[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

}
