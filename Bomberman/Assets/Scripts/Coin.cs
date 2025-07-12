using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Optional: bisa tambahin suara, efek, nambah skor, dll
            ScoreManager.instance.AddScore(coinValue);
            Destroy(gameObject); // Hapus koin
        }
    }
}
