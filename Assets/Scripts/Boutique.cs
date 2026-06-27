using UnityEngine;

public class Boutique : MonoBehaviour
{
    public void ExecuterEchange()
    {
        Debug.Log("DEBUG BOUTIQUE : Tentative d'exécution de l'échange...");
        
        GameManager manager = FindObjectOfType<GameManager>();
        
        if (manager != null)
        {
            Debug.Log("DEBUG BOUTIQUE : GameManager trouvé. Score déchets actuel = " + manager.scoreDéchets);
            
            if (manager.scoreDéchets > 0)
            {
                manager.AjouterGraines(manager.scoreDéchets);
                manager.scoreDéchets = 0;
                manager.MettreAJourUI(); 
                
                Debug.Log("DEBUG BOUTIQUE : Succès ! Déchets échangés contre des graines.");
            }
            else
            {
                Debug.Log("DEBUG BOUTIQUE : Échec, pas de déchets à échanger (score = 0).");
            }
        }
        else
        {
            Debug.LogError("DEBUG BOUTIQUE ERREUR : GameManager introuvable dans la scène !");
        }
    }
}