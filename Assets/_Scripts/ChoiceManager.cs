using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
	public PlayerController player;
	public string firstTimeCombatInfo = "";
	public DialogPanel dialogPanel;
	public static ChoiceManager instance;
	private bool hasShownFirstTimeCombatDialog = false;
	// Start is called before the first frame update
	void Start() {
		if (instance == null) {
			instance = this;
		}
		
	}

	public void ShowFirstTimeCombatDialog() {
		if (!hasShownFirstTimeCombatDialog) {
			Time.timeScale = 0;
			instance.dialogPanel.infoText.text = firstTimeCombatInfo;
			instance.dialogPanel.ShowDialog();
			instance.dialogPanel.option1Text.text = "Run";
			instance.dialogPanel.option2Text.text = "Attack";
			instance.dialogPanel.SetOptionsButtonActive(true);
			instance.dialogPanel.option1Button.onClick.AddListener(() => {

				instance.dialogPanel.HideDialog();
				instance.dialogPanel.option1Button.onClick.RemoveAllListeners();
				Time.timeScale = 1;
				instance.player.RunForFiveSeconds();
			});
			instance.dialogPanel.option2Button.onClick.AddListener(() => {

				instance.dialogPanel.HideDialog();
				instance.dialogPanel.option1Button.onClick.RemoveAllListeners();
				Time.timeScale = 1;
				instance.player.EquipWeapon();
			});
			hasShownFirstTimeCombatDialog = true;
		}
	}
}
