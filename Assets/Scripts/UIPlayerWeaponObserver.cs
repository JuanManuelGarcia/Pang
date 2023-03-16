using System.Linq;
using TMPro;
using UnityEngine;

public class UIPlayerWeaponObserver : MonoBehaviour, IPlayerObserver
{
    [SerializeField] TMP_Text WeaponNameText;
    [SerializeField] GameObject InfinityImage;
    [SerializeField] TMP_Text WeaponAmmoText;

    IPlayerSubject dyingPlayer;

    void Update()
    {
        // The player is a different instance each Level, so it needs to look for him to observe him again when it dies 
        if (dyingPlayer == null)
        {
            var playerSubjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<IPlayerSubject>();
            foreach (IPlayerSubject s in playerSubjects)
            {
                s.Attach(this);
                dyingPlayer = null;
                s.Notify();
            }
        }
    }

    public void Revise(ISubject subject)
    {
        IPlayerSubject ps = subject as IPlayerSubject;

        WeaponNameText.text = ps.CurrentWeaponName;

        if (ps.CurrentWeaponAmmo < int.MaxValue)
        {
            InfinityImage.SetActive(false);
            WeaponAmmoText.gameObject.SetActive(true);
            WeaponAmmoText.text = ps.CurrentWeaponAmmo.ToString("000");
        }
        else
        {
            InfinityImage.SetActive(true);
            WeaponAmmoText.gameObject.SetActive(false);
        }

        if (ps.IsDead)
        {
            dyingPlayer = ps;
        }
    }
}
