using _UpscaleStudio._Scripts.Player;
using UnityEngine;

namespace _UpscaleStudio._Scripts.Enemy {
    public class ZombieTrigger : MonoBehaviour {
        [SerializeField] private GameObject zombie;
        [SerializeField] private float moveDistance = 5f; 
        [SerializeField] private float speed = 2f;
        private static readonly int _run = Animator.StringToHash("Run");

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out PlayerHandle playerHandle)) {
                StartCoroutine(MoveZombieForward());
            }
        }

        private global::System.Collections.IEnumerator MoveZombieForward() {
            Vector3 startPosition = zombie.transform.position; 
            Vector3 targetPosition = startPosition + zombie.transform.forward * moveDistance; 

            float distanceTravelled = 0f;

        
            while (distanceTravelled < moveDistance) {
                Animator animator=   zombie.GetComponent<Animator>();
                animator.SetBool(_run, true);
                float moveStep = speed * Time.deltaTime; 
                zombie.transform.position += zombie.transform.forward * moveStep; 
                distanceTravelled += moveStep; 
                yield return null; 
            }

            // Убедимся, что зомби точно на целевой позиции (если нужно точное завершение)
            zombie.transform.position = targetPosition;
        }
    }
}