using UnityEngine;
using UnityEngine.InputSystem;

public class PlantationLibre : MonoBehaviour
{
    public GameObject prefabArbre; // C'est ici que tu glisseras ton modèle d'arbre
    public float distanceDevant = 2.0f;

    public void OnPlantTree(InputValue value)
    {
        if (value.isPressed)
        {
            GameManager manager = FindObjectOfType<GameManager>();

            if (manager != null && manager.nombreGraines >= 5)
            {
                // Position devant le joueur
                Vector3 position = transform.position + (transform.forward * distanceDevant);
                
                // Consommer les graines
                manager.nombreGraines -= 3;
                manager.MettreAJourUI();
                
                // Créer l'arbre
                Instantiate(prefabArbre, position, Quaternion.identity);
                
                // Notifier le GameManager
                manager.ArbrePlante();
                
                Debug.Log("Arbre planté !");
            }
            else
            {
                Debug.Log("Pas assez de graines (il en faut 5) !");
            }
        }
    }
}