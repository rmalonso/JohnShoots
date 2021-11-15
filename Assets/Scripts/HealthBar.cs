using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Health;
    
    public JohnScript John;
    
    
    // Escala el tama�o de la barra de vida en funci�n de la vida de John
    public void UploadHealthBar()
    {
        float amount = John.getHealth();
        Health.transform.localScale = new Vector2(amount, 1);
    }
}
