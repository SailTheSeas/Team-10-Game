using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerBars : MonoBehaviour
{
    //[SerializeField] Canvas playerUICanvas;
    [SerializeField] PlayerCharacter playerCharacerScript;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider mpSlider;
    // Start is called before the first frame update
    void Start()
    {
        playerCharacerScript = GetComponent<PlayerCharacter>();
        hpSlider = this.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Slider>();
        mpSlider = this.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Slider>();

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
