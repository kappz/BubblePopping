using UnityEngine;
using UnityEngine.UI;

public class Sliders_Horizontal: MonoBehaviour
{
	// Assign in the inspector
	public GameObject cannon1;
	public GameObject cannon2;
	public GameObject cannon3;
	public Slider slider;

	// Preserve the original and current orientation
	private float previousValue;

	void Awake ()
	{
		// Assign a callback for when this slider changes
		this.slider.onValueChanged.AddListener (this.OnSliderChanged);

		// And current value
		this.previousValue = this.slider.value;
	}

	void OnSliderChanged (float value)
	{
		// How much we've changed
		float delta = value - this.previousValue;
		this.cannon1.transform.Rotate (Vector3.up * delta * 45);
		this.cannon2.transform.Rotate (Vector3.up * delta * 45);
		this.cannon3.transform.Rotate (Vector3.up * delta * 45);

		// Set our previous value for the next change
		this.previousValue = value;
	}
}