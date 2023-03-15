using System.Globalization;
using TMPro;
using UnityEngine;

public class UIPointsObserver : MonoBehaviour, IPointsObserver
{
    private const string DefaultValue = "000.000.000";
    TMP_Text scoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = DefaultValue;
        PointsSingleton.Instance.Attach(this);
    }

    public void Revise(ISubject subject)
    {
        scoreText.text = (subject as IPointsSubject).Points.ToString("000,000,000", new CultureInfo("es-ES"));
    }

    private void OnDestroy()
    {
        PointsSingleton.Instance.Detach(this);
    }
}
