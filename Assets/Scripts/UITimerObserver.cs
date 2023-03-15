using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;

public class UITimerObserver : MonoBehaviour, ITimerObserver
{
    const string DefaultTimeLeft = "000";
    TMP_Text timeLeftText;

    void Start()
    {
        timeLeftText = GetComponent<TMP_Text>();
        timeLeftText.text = DefaultTimeLeft;

        var timerSubjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ITimerSubject>();
        foreach (ITimerSubject s in timerSubjects)
        {
            s.Attach(this);
        }
    }

    public void Revise(ISubject subject)
    {
        timeLeftText.text = Mathf.CeilToInt((subject as ITimerSubject).TimeLeft).ToString("000", new CultureInfo("es-ES"));
    }
}
