using System.Collections.Generic;

public interface IBallSubject : ISubject
{
    List<IBallSubject> BallsSpawned { get; }
}
