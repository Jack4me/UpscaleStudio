
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void OnLootCollected(LootPiece lootPiece) {
        // Обновляем данные о луте
        _worldData.LootData.Collected++;
        
        // Вызываем обновление интерфейса через LootCounter
        FindObjectOfType<LootCounter>().UpdateCounterText();
    }
}
