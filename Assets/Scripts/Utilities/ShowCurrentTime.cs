using UnityEngine;
using TMPro;

public class ShowCurrentTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentTimeLabel;

    // Update is called once per frame
    void Update()
    {
        _currentTimeLabel.text = TimerUtility.CurrentTime.ToString("F");
    }
}
