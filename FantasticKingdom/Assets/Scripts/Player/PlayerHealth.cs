using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    
    public float startingHealth = 100f;
    public float currentHealth;
    public Image damageImage;
    public float healthRegenDelay;
    public float healthRegenStep = 1;

    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public Text HealthText;
    public RawImage HPBar;

    public Transform spawnPoint;

    Animator anim;
    AudioSource playerDamagedAudio;
    PlayerMovement playerMovement;

    float healthTimer;
    bool regenHealth;
    bool isDead;
    bool damaged; //Is the player being damaged

    void Awake () {
        anim = GetComponent<Animator>();
        playerDamagedAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
        regenHealth = false;
    }
	
	void Update () {
        if (damaged)
        {
            if (damageImage != null)
                damageImage.color = flashColour;
        }
        else
        {
            if (damageImage != null)
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        if (HealthText != null)
            HealthText.text = "Health: " + currentHealth;
        if (HPBar != null)
            UpdateHealthBar();
        
    }

    void FixedUpdate()
    {
        healthTimer += Time.fixedDeltaTime;

        if (healthTimer >= healthRegenDelay)
        {
            regenHealth = true;
        }
        if (regenHealth && currentHealth < startingHealth)
            RegenHealth();
    }

    //Carry out actions related to taking damage
    public void TakeDamage(int amount)
    {
        damaged = true;
        regenHealth = false;
        playerDamagedAudio.Play();
        currentHealth -= amount;
        healthTimer = 0;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void RegenHealth()
    {
        if (currentHealth > 0 && currentHealth < startingHealth)
        {
            currentHealth += healthRegenStep;
        }
        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
    }

    //Controls what happens when the player dies
    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");
        playerMovement.enabled = false;
        StartCoroutine(RespawnTimer(3));
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void Respawn()
    {       
        isDead = false;
        currentHealth = startingHealth;
        playerMovement.enabled = true;
        anim.SetTrigger("Spawn");
        transform.position = spawnPoint.position;
        transform.rotation = Quaternion.Euler(new Vector3(0, 58, 0));
    }

    //Wait for the provided number of seconds
    IEnumerator RespawnTimer(float time)
    {
        yield return new WaitForSeconds(3);
        //RestartLevel();
        Respawn();
    }

    //Updates the UI component health bar
    public void UpdateHealthBar()
    {
        float HPRatio = 224 * currentHealth / startingHealth;
        HPBar.rectTransform.sizeDelta = new Vector2(HPRatio, 26);
        //HPBar.rectTransform.localPosition = new Vector3(HPRatio / 2 - 81, 0.5f, 0);
        //HPBar.rectTransform.localPosition = new Vector3(HPRatio / 2 + 2, -13, 0);
		HPBar.rectTransform.localPosition = new Vector3(HPRatio / 2 + 174.5f, -92.5f, 0);
    }

    //----------------------------------------------------------------------------||
    // Currently Useless code. kept here just in case
    //----------------------------------------------------------------------------||

    /** Checks for changes in currentHP field and applies them to the UI based on maxHP */
    /*
	void Health(){
		// Checks
		if (currentHP < 0)
			currentHP = 0;
		if (currentHP > maxHP)
			currentHP = maxHP;
		// Update HPBar
		float HPRatio = 200 * currentHP / maxHP;
        if (HPBar != null)
        {
            HPBar.rectTransform.sizeDelta = new Vector2(HPRatio, 22);
            HPBar.rectTransform.localPosition = new Vector3(HPRatio / 2 - 100, 0, 0);
        }
	}
    */
}
