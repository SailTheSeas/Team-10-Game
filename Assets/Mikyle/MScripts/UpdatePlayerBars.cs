using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdatePlayerBars : MonoBehaviour
{
    //[SerializeField] Canvas playerUICanvas;
    [SerializeField] PlayerCharacter playerCharacerScript;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider mpSlider;
    [SerializeField] private TMP_Text nameText1, nameText2;
    // Start is called before the first frame update
    void Start()
    {
        playerCharacerScript = GetComponent<PlayerCharacter>();
        hpSlider = this.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Slider>();
        mpSlider = this.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Slider>();

        nameText1 = this.transform.GetChild(1).GetChild(0).GetChild(4).GetComponent<TMP_Text>();
        nameText2 = this.transform.GetChild(1).GetChild(0).GetChild(5).GetComponent<TMP_Text>();

        nameText1.text = playerCharacerScript.characterName;
        nameText2.text = playerCharacerScript.characterName;

        hpSlider.maxValue = playerCharacerScript.playerHealth;
        hpSlider.value = hpSlider.maxValue;
        mpSlider.maxValue = playerCharacerScript.playerMP;
        mpSlider.value = mpSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (hpSlider.value > playerCharacerScript.PlayerHealth)
            hpSlider.value -= 2;
        if (mpSlider.value > playerCharacerScript.playerMP)
            mpSlider.value -= 2;


        if (hpSlider.value < playerCharacerScript.PlayerHealth)
            hpSlider.value +=2;
        if (mpSlider.value < playerCharacerScript.playerMP)
            mpSlider.value += 2;
    }
}
