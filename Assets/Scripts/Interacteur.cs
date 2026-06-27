using UnityEngine;
using UnityEngine.InputSystem;

public class Interacteur : MonoBehaviour
{
    public void OnInteractBoutique(InputValue value)
    {
        // Log 1 : On vérifie si Unity reçoit bien l'appui sur le bouton
        Debug.Log("DEBUG INTERACTEUR : Bouton Triangle détecté par le système !");

        if (value.isPressed)
        {
            GameObject boutiqueObj = GameObject.FindGameObjectWithTag("Boutique");
            
            if (boutiqueObj != null)
            {
                float dist = Vector3.Distance(transform.position, boutiqueObj.transform.position);
                Debug.Log("DEBUG INTERACTEUR : Boutique trouvée, distance = " + dist);

                if (dist < 20f) // Distance élargie pour le test
                {
                    Debug.Log("DEBUG INTERACTEUR : Distance OK, appel de ExecuterEchange()");
                    boutiqueObj.GetComponent<Boutique>().ExecuterEchange();
                }
                else
                {
                    Debug.Log("DEBUG INTERACTEUR : Trop loin de la boutique !");
                }
            }
            else
            {
                Debug.Log("DEBUG INTERACTEUR ERREUR : Aucun objet avec le tag 'Boutique' n'est présent dans la scène.");
            }
        }
    }
}