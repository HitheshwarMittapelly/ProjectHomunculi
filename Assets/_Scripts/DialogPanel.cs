using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : MonoBehaviour
{
	public Image backgroundImage;

	public Text infoText;

	public Button confirmButton;

	public Button option1Button;

	public Button option2Button;

	public Text option1Text;
	public Text option2Text;
    public void ShowDialog() {
		gameObject.SetActive(true);
		SetOptionsButtonActive(false);
		SetConfirmButtonActive(false);
	}

	public void HideDialog() {
		gameObject.SetActive(false);
		
	}

	public void SetOptionsButtonActive(bool val) {
		option1Button.gameObject.SetActive(val);
		option2Button.gameObject.SetActive(val);
	}

	public void SetConfirmButtonActive(bool val) {
		confirmButton.gameObject.SetActive(val);
	}
}
