using UnityEngine;
using UnityEngine.UI;

public class CarGearboxIndicator : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Text text;
    [SerializeField] private AudioSource GearChangedAudio;

    private void Start()
    {
        car.GearChanged += OnGearChanged;
    }

    private void OnDestroy()
    {
        car.GearChanged -= OnGearChanged;
    }
    private void OnGearChanged(string gearName)
    {
        text.text = gearName;
        GearChangedAudio.Play();
    }
}
