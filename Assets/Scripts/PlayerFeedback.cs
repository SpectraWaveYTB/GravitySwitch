using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeedback : MonoBehaviour
{
    [SerializeField] private Color switchColor = new Color(0.2f, 0.8f, 1f);
    [SerializeField] private float switchScalePulse =  1.3f;
    [SerializeField] private float switchFeedbackDuration = 0.08f;
    [SerializeField] private Color deathColor = new Color (1f, 0.2f, 0.2f);
    [SerializeField] private float deathFlashDuration = 0.05f;

    private MeshRenderer _renderer;
    private Vector3 _baseScale;
    private Color _baseColor;
    private static readonly int ColorProp = Shader.PropertyToID("_Color");

    public static PlayerFeedback Instance { get; private set;}

    void Awake()
    {
        Instance = this;
        _renderer = GetComponent<MeshRenderer>();
        _baseScale = transform.localScale;
        _baseColor = _renderer.material.color;
    }

    
    IEnumerator SwitchPulse()
    {
        _renderer.material.color = switchColor;
         transform.localScale = _baseScale * switchScalePulse;
         yield return new WaitForSeconds(switchFeedbackDuration);
         _renderer.material.color = _baseColor;
        transform.localScale = _baseScale;
    }

    IEnumerator DeathFlash()
    {
        _renderer.material.color = deathColor;
        yield return new WaitForSeconds(deathFlashDuration);
        GameManager.Instance.RestartGame();
    }

    public void PlaySwitchFeedback()
    {
        StartCoroutine(SwitchPulse());
    }

    public void PlayDeathFeedback()
    {
        StartCoroutine(DeathFlash());
    }
}
