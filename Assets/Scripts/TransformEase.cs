using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformEase : MonoBehaviour
{
	[System.Serializable]
	public struct State
	{
		[SerializeField] public Vector3 position;
		[SerializeField] public Vector3 rotation;
		[SerializeField] public Vector3 scale;
	}

	[SerializeField] private State start;
	[SerializeField] private State end;

	[SerializeField] private UnityEasingTimer timer = new UnityEasingTimer();

	private void Awake()
	{
		timer.Start(this);
	}

	public void OnStart()
	{
		Apply(start);
	}

	public void Evaluate(EaseData t)
	{
		transform.position = EasingUtility.Ease(EasingUtility.Linear, start.position, end.position, (float)t.Eased);
		transform.rotation = Quaternion.Euler(EasingUtility.Ease(EasingUtility.InSine, start.rotation, end.rotation, (float)t.Eased));
		transform.localScale = EasingUtility.Ease(EasingUtility.Linear, start.scale, end.scale, (float)t.Eased);
	}

	public void OnEnd()
	{
		Apply(end);
	}

	public void SetStartPosition(Vector3 position)
	{
		start.position = position;
	}
	public void SetStartRotation(Vector3 rotation)
	{
		start.rotation = rotation;
	}
	public void SetStartScale(Vector3 scale)
	{
		start.scale = scale;
	}
	public void SetEndPosition(Vector3 position)
	{
		end.position = position;
	}
	public void SetEndRotation(Vector3 rotation)
	{
		end.rotation = rotation;
	}
	public void SetEndScale(Vector3 scale)
	{
		end.scale = scale;
	}

	public void Apply(State state)
	{
		transform.position = state.position;
		transform.rotation = Quaternion.Euler(state.rotation);
		transform.localScale = state.scale;
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space)) {
			timer.SetDuration(2.0).SetSpeed(0.5).Play();
		}
		if(Input.GetKeyUp(KeyCode.P)) {
			timer.Pause();
		}
		if (Input.GetKeyUp(KeyCode.S)) {
			timer.Stop();
		}
		if (Input.GetKeyUp(KeyCode.R)) {
			timer.Reset();
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(start.position, start.scale / 10.0f);


		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(end.position, end.scale / 10.0f);
	}
}
