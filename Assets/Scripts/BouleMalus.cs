using UnityEngine;

public class BouleMalus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // On déclenche le malus de caméra
            FindObjectOfType<GameManager>().DeclencherMalus();
            Destroy(gameObject); // Détruit la boule
        }
    }
}
