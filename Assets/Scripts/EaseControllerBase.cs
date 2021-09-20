using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EaseControllerBase : MonoBehaviour
{
    public enum TimeMode
    {
        Update,
        UnscaledUpdate,
        FixedUpdate,
        UnscaledFixedUpdate,
        WaitForSeconds,
        UnscaledWaitForSeconds
	}

    [SerializeField] private TimeMode timeMode;
    [SerializeField] private float waitTime;

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
        while(t < 1.0f && isRunning) {

            Evaluate(t);

            t += GetDeltaTime(timeMode);
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
				yield return new WaitForSeconds(waitTime);
                break;
			case TimeMode.UnscaledWaitForSeconds:
                yield return new WaitForSecondsRealtime(waitTime);
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
                return waitTime;
			case TimeMode.UnscaledWaitForSeconds:
                return waitTime;
            default:
				throw new UnityException("Unknown TimeMode");
		}
	}
}
