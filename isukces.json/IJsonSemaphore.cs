using System.Threading;

namespace iSukces.Json;

public interface IJsonSemaphore
{
    void WaitOne();
    void ReleaseOne();
}

public class MutexJsonSemaphore : IJsonSemaphore
{
    private readonly Mutex _mutex;

    public static IJsonSemaphore FromMutex(Mutex mutex)
    {
        return new MutexJsonSemaphore(mutex);
    }

    private MutexJsonSemaphore(Mutex mutex)
    {
        _mutex = mutex;
    }

    public void WaitOne()
    {
        _mutex.WaitOne();
    }

    public void ReleaseOne()
    {
        _mutex.ReleaseMutex();
    }
}