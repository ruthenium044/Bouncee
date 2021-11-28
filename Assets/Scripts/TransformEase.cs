using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class TransformEase : MonoBehaviour
{
	[SerializeField] private UnityEasingTimer timer = new UnityEasingTimer();

	[System.Serializable]
	public struct Signature
	{
		private Component context;
		private MemberInfo valueMember;

		public Component Context => context;
		public MemberInfo ValueMember => valueMember;

		public Signature(Component context, MemberInfo member)
		{
			this.context = context;
			this.valueMember = member;
		}
	}

	[System.Serializable]
	public class EaseValue
	{
		public delegate object Lerp(object a, object b, float t);	

		public object from;
		public object to;
		private Lerp lerp;


		public EaseValue(Signature signature, System.Type type)
		{
			var valueMember = signature.ValueMember;
			var context = signature.Context;
			this.lerp = GetLerp(type);

			switch (valueMember.MemberType) {
				case MemberTypes.Field:
					this.from = (valueMember as FieldInfo).GetValue(context);
					this.to = (valueMember as FieldInfo).GetValue(context);
					break;

				case MemberTypes.Property:
					this.from = (valueMember as PropertyInfo).GetValue(context);
					this.to = (valueMember as PropertyInfo).GetValue(context);
					break;

				default:
					throw new UnityException("Cannot use member");
			}
		}

		private static Lerp GetLerp(System.Type type) 
		{
			if(type == typeof(Vector3)) {
				return ((a, b, t) => Vector3.Lerp((Vector3)a, (Vector3)b, t));
			}
			else if (type == typeof(Vector2)) {
				return ((a, b, t) => Vector2.Lerp((Vector2)a, (Vector2)b, t));
			}
			else if (type == typeof(float)) {
				return ((a, b, t) => Mathf.Lerp((float)a, (float)b, t));
			}

			throw new UnityException("Cannot find fitting lerp");
		}

		public void Update(Signature signature, float t)
		{
			var valueMember = signature.ValueMember;
			var context = signature.Context;
			switch (valueMember.MemberType) {
				case MemberTypes.Field:
					(valueMember as FieldInfo).SetValue(context, lerp(from, to, t));
					break;

				case MemberTypes.Property:
					(valueMember as PropertyInfo).SetValue(context, lerp(from, to, t));
					break;

				default:
					throw new UnityException("Cannot use member");
			}

		}
	}

	private Dictionary<Signature, EaseValue> easeSet = new Dictionary<Signature, EaseValue>();

	private void Awake()
	{
		timer.Start(this);
	}

	public void Evaluate(EaseData t)
	{
		foreach(var e in easeSet) {
			e.Value.Update(e.Key, (float)t.Eased);
		}
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


		Gizmos.color = Color.red;
	}
}
