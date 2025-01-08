/*
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RB_HUDHealthBar : MonoBehaviour {
    
    public RB_Health Rb_health; // Reference to the health component

    [Header("UX")]
    [SerializeField] private float _chipSpeed = 2.0f; // Speed at which the health bar updates
    [SerializeField] private Image _frontHealthBar; // Front health bar image
    [SerializeField] private Image _backHealthBar; // Back health bar image
    
    private void Update()
    {
        UxUpdateXHealthBar();
    }
    
    private void UxStart() {
        _frontHealthBar.fillAmount = Rb_health.Hp / Rb_health.HpMax;
        _backHealthBar.fillAmount = Rb_health.Hp / Rb_health.HpMax;
    }
    
    private void UxUpdateXHealthBar()
    {
        float fillF = _frontHealthBar.fillAmount;
        float fillB = _backHealthBar.fillAmount;
        float hFraction = Rb_health.Hp / Rb_health.HpMax; // Decimal representation of health (0 to 1)

        // Update back health bar (damage taken)
        if (fillB > hFraction)
        {
            _frontHealthBar.fillAmount = hFraction;
            _backHealthBar.color = Color.red;
            Rb_health.LerpTimer += Time.deltaTime;

            float percentComplete = Rb_health.LerpTimer / _chipSpeed;
            percentComplete = percentComplete * percentComplete;

            _backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        // Update front health bar (healing)
        if (fillF < hFraction)
        {
            _backHealthBar.color = Color.green;
            _backHealthBar.fillAmount = hFraction;
            Rb_health.LerpTimer += Time.deltaTime;

            float percentComplete = Rb_health.LerpTimer / _chipSpeed;
            percentComplete = percentComplete * percentComplete;

            _frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }
}
*/
