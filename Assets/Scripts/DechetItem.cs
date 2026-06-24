using UnityEngine;

public class DechetItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si c'est le joueur qui passe sur l'objet
        if (other.CompareTag("Player"))
        {
            GameManager manager = FindObjectOfType<GameManager>();
            if (manager != null)
            {
                manager.AjouterPoint(); // Ajoute le point et actualise le texte
            }

            Destroy(gameObject); // Supprime la sphère ramassée
        }
    }
}
