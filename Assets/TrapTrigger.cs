using System.Collections;
using _UpscaleStudio._Scripts.Data.SoundData;
using _UpscaleStudio._Scripts.Player;
using _UpscaleStudio._Scripts.System.Handlers;
using UnityEngine;

public class TrapTrigger : MonoBehaviour {
    [SerializeField] public ParticleSystem trapEffect;
    [SerializeField] public Die playerDie;
    [SerializeField] private SoundData trapSound;
    [SerializeField] private SoundData fireTrapSound;


    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out PlayerHandle playerHandle)) {
            StartCoroutine(TrapActivate());
        }
    }

    private IEnumerator TrapActivate() {
        SoundHandler.Instance.PlaySound(trapSound, transform.position, gameObject);

      
        yield return new WaitForSeconds(1f);
        trapEffect.Play();
        SoundHandler.Instance.PlaySound(fireTrapSound, transform.position, gameObject);
        yield return new WaitForSeconds(1f);
        playerDie.HandleDeath();
        Debug.Log("Trap Activated!");
    }
}