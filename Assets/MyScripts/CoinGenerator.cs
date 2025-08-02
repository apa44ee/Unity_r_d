using UnityEngine;
using Dreamteck.Forever;

public class CoinGenerator : MonoBehaviour
{
    public GameObject coinPrefab;
    public int coinCount = 5;
    public Vector3 areaSize = new Vector3(10f, 0f, 20f);
    public Vector3 areaOffset = new Vector3(0f, 1f, 0f);
    public float coinScale = 0.4f; // розмір монетки
    public LayerMask obstacleMask; // слой обєктів, на яких можна савнитись

    void Start()
    {
        GenerateCoins();
    }

    void GenerateCoins()
    {
        int spawned = 0;
        int maxAttempts = coinCount * 10;

        while (spawned < coinCount && maxAttempts > 0)
        {
            maxAttempts--;

            Vector3 randomPosition = new Vector3(
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                0f,
                Random.Range(-areaSize.z / 2f, areaSize.z / 2f)
            );

            Vector3 spawnPos = transform.position + areaOffset + randomPosition;

            // Перевірка, не на місці інших обєктів
            if (Physics.CheckSphere(spawnPos, 0.5f, obstacleMask))
            {
                continue; // Попробуем інше місце
            }

            GameObject coin = Instantiate(coinPrefab, spawnPos, Quaternion.Euler(90f, 0f, 0f), transform);

            // маштабування
            coin.transform.localScale = Vector3.one * coinScale;

            // Ротейшн монет
            if (coin.GetComponent<Rotator>() == null)
            {
                coin.AddComponent<Rotator>();
            }

            spawned++;
        }
    }
}