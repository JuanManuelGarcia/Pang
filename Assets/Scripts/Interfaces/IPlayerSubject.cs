public interface IPlayerSubject : ISubject
{
    bool IsDead { get; }
    string CurrentWeaponName { get; }
    int CurrentWeaponAmmo { get; }
}
