using UnityEngine;

public class OnDestroyAddPoints : MonoBehaviour
{
    [SerializeField] int PointsToAdd;

    private void OnDestroy()
    {
        PointsSingleton.Instance.AddPoints(PointsToAdd);
    }
}
