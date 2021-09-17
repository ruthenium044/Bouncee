using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformEase : EaseControllerBase
{
	[System.Serializable]
	public struct State
	{
		[SerializeField] public Vector3 position;
		[SerializeField] public Vector3 rotation;
		[SerializeField] public Vector3 scale;
	}

	public override void OnStart()
	{
		Apply(start);
	}

	public override void Evaluate(float t)
	{
		transform.position = Easing.Ease(Easing.Linear, start.position, end.position, t);
		transform.rotation = Quaternion.Euler(Easing.Ease(Easing.InSine, start.rotation, end.rotation, t));
		transform.localScale = Easing.Ease(Easing.Linear, start.scale, end.scale, t);
	}

	public override void OnEnd()
	{
		Apply(end);
	}

	[SerializeField] State start;
	[SerializeField] State end;

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

	private void Apply(State state)
	{
		transform.position = state.position;
		transform.rotation = Quaternion.Euler(state.rotation);
		transform.localScale = state.scale;
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space)) {
			Play();
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
