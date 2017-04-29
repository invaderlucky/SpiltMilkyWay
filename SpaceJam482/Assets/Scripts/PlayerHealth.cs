using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	public float maxHealth = 100f;
    public float startingHealth = 100f;                            // The amount of health the player starts the game with.
    public float currentHealth;                                   // The current health the player has.
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    public static float laserDamage = 50f;
    public int missileCount = 0;
    public float energy = 100;
    public float energyRegen = 6;
    public float energyDrain = 10;

    public GameObject HealthBar;
    public GameObject EnergyBar;


    //Animator anim;                                              // Reference to the Animator component.
    public AudioSource playerAudio;                                    // Reference to the AudioSource component.
    //PlayerMovement playerMovement;                              // Reference to the player's movement.
    //PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.
    public bool isShooting = false;

    void Awake ()
    {
        // Setting up the references.
        //anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        //playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();

        // Set the initial health of the player.
        currentHealth = startingHealth;
    }


    void Update ()
    {
        // If the player has just been damaged...
        if(damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            //damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            //damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
        energy += energyRegen * Time.deltaTime;
        if (isShooting)
        {
            energy -= energyDrain * Time.deltaTime;
        }
        if (energy >= 100f)
        {
            energy = 100f;
        }
        float calcEnergy = energy / 100;
        if (calcEnergy < 0)
            calcEnergy = 0;
        setEnergyBar(calcEnergy);
    }


    public void TakeDamage (float amount)
    {
        float amt = amount;
        // Set the damaged flag so the screen will flash.
        damaged = true;
        //check energy/shield, subtract if possible
        if(energy > amt)
        {
            energy -= amt;
            amt = 0;
        }
        else
        {
            amt -= energy;
            energy = 0;
        }

        // Reduce the current health by the damage amount.
        currentHealth -= amt;

        // Set the health bar's value to the current health.
        //healthSlider.value = currentHealth;
        float calcHealth = currentHealth / maxHealth; // so like 80 / 100 = 0.8f
        if (calcHealth < 0)
        	calcHealth = 0;
        setHealthBar(calcHealth);

        // Play the hurt sound effect.
        //playerAudio.Play ();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if(currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death ();
        }
    }

    public void setHealthBar(float newHealth) {
    	// newHealth needs to be a value between 0 and 1, 
		HealthBar.transform.localScale = new Vector3(newHealth, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
    }

    public void setEnergyBar(float newEnergy)
    {
        EnergyBar.transform.localScale = new Vector3(newEnergy, EnergyBar.transform.localScale.y, EnergyBar.transform.localScale.z);
    }


    void Death ()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        PlayerPrefs.SetInt("Score", GameObject.Find("GameManager").GetComponent<GameManager>().score);
        SceneManager.LoadScene("GameOver");
/* 
        // Turn off any remaining shooting effects.
        playerShooting.DisableEffects ();

        // Tell the animator that the player is dead.
        anim.SetTrigger ("Die");
 */
        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        //playerAudio.clip = deathClip;
        //playerAudio.Play ();

        // Turn off the movement and shooting scripts.
        //playerMovement.enabled = false;
       // playerShooting.enabled = false;
    }

}
