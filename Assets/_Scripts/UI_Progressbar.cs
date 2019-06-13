using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Progressbar : MonoBehaviour
{
	
	public Image fillBarImage;

	public void SetFillValue(float newValue) {
		fillBarImage.fillAmount = newValue;
	}
}
