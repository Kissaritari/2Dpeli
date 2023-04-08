using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public Slider healthbarSlider;
    private Player playerRef;
    public bool atMaxHp;
    // Start is called before the first frame update
    void Start()
    {
        atMaxHp = true;
        playerRef = GetComponent<Player>();
        // healthbarSlider = GetComponent<Slider>();
        healthbarSlider.maxValue = playerRef.GetComponent<Creature>().MaxHealth;
        healthbarSlider.value = playerRef.GetComponent<Creature>().MaxHealth;
    }
    private void Update()
    {
        if (healthbarSlider.value < healthbarSlider.maxValue)
            atMaxHp = false;
    }
}
