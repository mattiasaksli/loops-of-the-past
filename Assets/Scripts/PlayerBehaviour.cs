using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{

    public Animator anim;
    public int Lives = 3;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Lives < 1)
        {
            isDead = true;
            anim.SetBool("isDead", isDead);
            Invoke("PlayerDeath", 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.GetComponent<Slime>())
        {
            AudioManager.Instance.Play("Hit");
            
            Destroy(other.gameObject);
            HUD.Instance.RemoveLife();

            Lives -= 1;
        }

        if (other.gameObject.GetComponent<Fireball>())
        {
            AudioManager.Instance.Play("Hit");
            
            Destroy(other.gameObject);
            HUD.Instance.RemoveLife();

            Lives -= 1;
        }
    }

    private void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
