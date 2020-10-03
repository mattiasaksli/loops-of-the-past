using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{

    public int Lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Lives < 1) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Slime>())
        {
            Destroy(other.gameObject);
            HUD.Instance.RemoveLife();

            Lives -= 1;
        }

        if (other.gameObject.GetComponent<Fireball>())
        {
            Destroy(other.gameObject);
            HUD.Instance.RemoveLife();

            Lives -= 1;
        }
    }

}
