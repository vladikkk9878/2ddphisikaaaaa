using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private Image HealHealth;
    
    private int _currentHealth;
    
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        DrowHealth();
        if (_currentHealth <= 0) ;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void DrowHealth()
    {
        HealHealth.fillAmount = _currentHealth / maxHealth;
        
    }
}
