using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

[System.Serializable]
public enum TimeMode
{
	Update,
	UnscaledUpdate,
	FixedUpdate,
	UnscaledFixedUpdate,
	WaitForSeconds,
	UnscaledWaitForSeconds
}

[System.Serializable]
public struct TimedEvent
{
	[SerializeField] private double at;
	public double Time => at;

	[SerializeField] private UnityEvent onAt;
	public UnityEvent Method => onAt;
}

[System.Serializable]
public struct EaseData
{
	private double time;
	public double Time => time;
	private double eased;
	public double Eased => eased;

	public EaseData(double time, double eased)
	{
		this.time = time;
		this.eased = eased;
	}
}

[System.Serializable]
public class UnityEasingTimer
{
	[System.Serializable]
	private enum State
	{
		Playing,
		Pausing,
		Stopping
	}

	[SerializeField] private EasingUtility.Style easingStyle = EasingUtility.Style.Linear;
	[SerializeField] private TimeMode timeMode = TimeMode.Update;
	[SerializeField] private double customDeltaTime = 0.0f;
	[SerializeField] private double duration = 1.0;
	[SerializeField] private double speed = 1.0;
	[SerializeField] private bool isReverse = false;
	[SerializeField] private State state = State.Stopping; 
	[SerializeField] private UnityEvent onStartUnityEvent;
	[SerializeField] private UnityEvent<EaseData> onGoingUnityEvent;
	[SerializeField] private UnityEvent onEndUnityEvent;
	[SerializeField] private List<TimedEvent> timePointUnityEvents = new List<TimedEvent>();

	private EasingUtility.Function function = EasingUtility.GetFunction(EasingUtility.Style.Linear);
	private event System.Action OnStart;
	private event System.Action OnEnd;
	private event System.Action<EaseData> OnRun;
	private List<(double at, System.Action method)> timePointCallbacks = new List<(double, System.Action)>();

	private bool isRunning = false;
	private EaseData current = new EaseData(0.0, 0.0);
	public EaseData Current => current;
	private bool isResetting = false;
	private bool hasEasingStyleChanged = false;
	private EasingUtility.Style previousEasingStyle = EasingUtility.Style.Linear;

	public UnityEasingTimer(MonoBehaviour context) 
	{
		context.StartCoroutine(Run());
	}
	public UnityEasingTimer()
	{
	}

	public UnityEasingTimer SetEasing(EasingUtility.Function function)
	{
		this.function = function;
		return this;
	}
	public UnityEasingTimer SetEasing(EasingUtility.Style style)
	{
		easingStyle = style;
		hasEasingStyleChanged = true;
		return this;
	}
	public UnityEasingTimer SetDuration(double duration)
	{
		this.duration = duration;
		return this;
	}
	public UnityEasingTimer SetTimeMode(TimeMode mode)
	{
		timeMode = mode;
		return this;
	}
	public UnityEasingTimer SetSpeed(double speed)
	{
		this.speed = speed;
		return this;
	}
	public UnityEasingTimer SetReverse(bool reverse)
	{
		isResetting = reverse;
		return this;
	}
	public UnityEasingTimer SetCustomDeltaTime(double dt)
	{
		customDeltaTime = dt;
		return this;
	}
	public UnityEasingTimer AddStartEvent(System.Action e)
	{
		OnStart += e;
		return this;
	}
	public UnityEasingTimer RemoveStartEvent(System.Action e)
	{
		OnStart -= e;
		return this;
	}
	public UnityEasingTimer AddEndEvent(System.Action e)
	{
		OnEnd += e;
		return this;
	}
	public UnityEasingTimer RemoveEndEvent(System.Action e)
	{
		OnEnd -= e;
		return this;
	}
	public UnityEasingTimer AddRunEvent(System.Action<EaseData> e)
	{
		OnRun += e;
		return this;
	}
	public UnityEasingTimer RemoveRunEvent(System.Action<EaseData> e)
	{
		OnRun -= e;
		return this;
	}
	
	public UnityEasingTimer Play()
	{
		state = State.Playing;
		return this;
	}
	public UnityEasingTimer Pause()
	{
		state = State.Pausing;
		return this;
	}
	public UnityEasingTimer Reset()
	{
		isResetting = true;
		return this;
	}
	public UnityEasingTimer Stop()
	{
		state = State.Stopping;
		return this;
	}

	private void RefreshEasingFunction()
	{
		if(hasEasingStyleChanged) {
			function = EasingUtility.GetFunction(easingStyle);
			hasEasingStyleChanged = false;
		}
	}

	public void Start(MonoBehaviour context)
	{
		context.StartCoroutine(Run());
	}

	private IEnumerator Run()
	{
		if(isRunning) {
			yield break;
		}

		isRunning = true;
		while(isRunning) {
			if (state != State.Playing) {
				yield return WaitForPlaying();
			}
			yield return Playing();
		}
		isRunning = false;

	}
	
	private IEnumerator WaitForPlaying()
	{
		yield return new WaitUntil(() => {
			return state == State.Playing;
		});
	}
	private IEnumerator Playing()
	{
		if (isReverse) {
			End();
		}
		else {
			Begin();
		}

		yield return GetWaitingTime(timeMode);

		while (ShouldPlay()) {
			hasEasingStyleChanged = previousEasingStyle != easingStyle;
			if(previousEasingStyle != easingStyle) {
				hasEasingStyleChanged = true;
			}

			if (!Going()) {
				yield break;
			}

			previousEasingStyle = easingStyle;
			yield return GetWaitingTime(timeMode);
		}

		if (isReverse) {
			Begin();
		}
		else {
			End();
		}
	}

	private void Begin()
	{
		current = new EaseData(0.0, function(0.0f));
		OnStart?.Invoke();
		onStartUnityEvent?.Invoke();

		OnRun?.Invoke(current);
		onGoingUnityEvent?.Invoke(current);
	}
	private void End()
	{
		current = new EaseData(1.0, function(1.0f));
		OnRun?.Invoke(current);
		onGoingUnityEvent?.Invoke(current);

		onEndUnityEvent?.Invoke();
		OnEnd?.Invoke();
	}
	private bool Going()
	{
		switch (state) {
			case State.Pausing:
				return true;
			case State.Stopping:
				return false;
		}

		if(isResetting) {
			isResetting = false;
			return false;
		}


		var change = GetDeltaTime() / (isReverse ? -duration : duration);
		var previous = current.Time + change;

		RefreshEasingFunction();
		current = new EaseData(current.Time + (change * speed), function((float)current.Time));

		OnRun?.Invoke(current);

		foreach (var item in timePointCallbacks) {
			if (item.at <= current.Time && item.at >= previous) {
				item.method?.Invoke();
			}
		}
		foreach (var item in timePointUnityEvents) {
			if (item.Time <= current.Time && item.Time >= previous) {
				item.Method?.Invoke();
			}
		}

		return true;
	}
	private bool ShouldPlay()
	{
		if (isReverse) {
			return current.Time > 0.0f;
		}
		return current.Time < 1.0f;
	}
	
	private double GetDeltaTime()
	{
		switch (timeMode) {
			case TimeMode.Update:
				return Time.deltaTime;
			case TimeMode.UnscaledUpdate:
				return Time.unscaledDeltaTime;
			case TimeMode.FixedUpdate:
				return Time.fixedDeltaTime;
			case TimeMode.UnscaledFixedUpdate:
				return Time.fixedUnscaledDeltaTime;
			case TimeMode.WaitForSeconds:
				return customDeltaTime;
			case TimeMode.UnscaledWaitForSeconds:
				return customDeltaTime;
			default:
				throw new UnityException("Unknown TimeMode");
		}
	}
	private IEnumerator GetWaitingTime(TimeMode timeMode)
	{
		if (state == State.Pausing) {
			yield return new WaitUntil(() => {
				return state != State.Pausing || isResetting;
			});
		}

		switch (timeMode) {
			case TimeMode.Update:
				yield return null;
				break;
			case TimeMode.UnscaledUpdate:
				yield return null;
				break;
			case TimeMode.FixedUpdate:
				yield return new WaitForFixedUpdate();
				break;
			case TimeMode.UnscaledFixedUpdate:
				yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
				break;
			case TimeMode.WaitForSeconds:
				yield return new WaitForSeconds((float)customDeltaTime);
				break;
			case TimeMode.UnscaledWaitForSeconds:
				yield return new WaitForSecondsRealtime((float)customDeltaTime);
				break;
			default:
				throw new UnityException("Unknown TimeMode");
		}
	}
}

public abstract class EaseController : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private TimeMode timeMode;
    [SerializeField] private float deltaTime;

	//[SerializeField] private UnityEasingTimer timer;

    private bool isPlaying = false;
    private bool isRunning = false;

    public abstract void OnStart(); // Assume 0.0f
    public abstract void Evaluate(float t); // Assume [0.0f; 1.0f[
    public abstract void OnEnd(); // Assume 1.0f

    public void Play()
    {
		isPlaying = true;
        StartCoroutine(Run());
	}
    public void Pause()
    {
        isPlaying = false;
    }
	public void Stop()
	{
        Pause();
        StopCoroutine(Run());
	}
	public void Reverse()
	{
        Stop();
        Play();
	}

    private IEnumerator Run()
    {
        if(isRunning) {
            yield break;
		}
        isRunning = true;

        OnStart();

        float t = 0.0f;
		while (t < 1.0f && isRunning) {

			Evaluate(t);

			float change = (GetDeltaTime(timeMode) / duration);
			t += change;
			yield return GetWaitingTime(timeMode);
		}

		Evaluate(1.0f);
		OnEnd();

        isRunning = false;
    }

    private IEnumerator GetWaitingTime(TimeMode timeMode)
    {
        if(isPlaying) {
            yield return new WaitUntil(() => {
                return isPlaying;
            });
		}

		switch (timeMode) {
			case TimeMode.Update:
				yield return null;
				break;
			case TimeMode.UnscaledUpdate:
				yield return null;
				break;
			case TimeMode.FixedUpdate:
				yield return new WaitForFixedUpdate();
				break;
			case TimeMode.UnscaledFixedUpdate:
				yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
				break;
			case TimeMode.WaitForSeconds:
				yield return new WaitForSeconds(deltaTime);
                break;
			case TimeMode.UnscaledWaitForSeconds:
                yield return new WaitForSecondsRealtime(deltaTime);
                break;
            default:
				throw new UnityException("Unknown TimeMode");
		}
	}

	private float GetDeltaTime(TimeMode timeMode)
    {
		switch (timeMode) {
			case TimeMode.Update:
				return Time.deltaTime;
			case TimeMode.UnscaledUpdate:
				return Time.unscaledDeltaTime;
			case TimeMode.FixedUpdate:
				return Time.fixedDeltaTime;
			case TimeMode.UnscaledFixedUpdate:
				return Time.fixedUnscaledDeltaTime;
			case TimeMode.WaitForSeconds:
                return deltaTime;
			case TimeMode.UnscaledWaitForSeconds:
                return deltaTime;
            default:
				throw new UnityException("Unknown TimeMode");
		}
	}
}
