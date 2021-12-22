using System;
using System.Diagnostics;

public class CustomTimer : IDisposable
{
    private readonly string name;
    private readonly int testsNumber;
    private readonly Stopwatch watch;

    public CustomTimer(string n, int tn)
    {
        name = n;
        testsNumber = tn;
        watch = Stopwatch.StartNew();

    }
    
    public void Dispose()
    {
        watch.Stop();
        float ms = watch.ElapsedMilliseconds;
        UnityEngine.Debug.Log($"{name} Total: {ms:0.00} \n" + $" {ms / testsNumber} ms per test " + $"Number of tests: {testsNumber:N0}");
    }
}
