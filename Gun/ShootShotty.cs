using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ShootShotty : MonoBehaviour
{
    private const float fireRate = 1f;
    private const float hitDistance = 35f;
    private const int damage = 2;
    private float timer;
    private Health health;
    private List<Vector3> bulletSpread;
    [SerializeField] private GameObject spine;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private AudioClip equipGun;
    [SerializeField] private AudioClip shootGun;
    [SerializeField] private AudioClip reloadGun;
    [SerializeField] private AudioClip animalHitImpact;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private GameObject bulletHitMarker;
    [SerializeField] private GameObject reticle;
    [SerializeField] private GunPickUp gunPickUpScript;
    [SerializeField] private QuestGiver questGiver;
    private bool equipSound;
    private Animator anim;
    private GameObject player;
    private AudioSource audioSource;
    private bool ShootingWithShotgun => Input.GetMouseButton(1) && Input.GetMouseButtonDown(0) && gunPickUpScript.shottyWasPickedUp;

    private void Awake()
    {
        bulletSpread = new List<Vector3>()
        {
            new Vector3(0.500f, 0.50f, 0.0f), // center ray
            new Vector3(0.495f, 0.51f, 0.0f), // left ray
            new Vector3(0.505f, 0.51f, 0.0f), // right ray
            new Vector3(0.500f, 0.52f, 0.0f), // top ray
            new Vector3(0.493f, 0.53f, 0.0f), // far left & high ray
            new Vector3(0.507f, 0.53f, 0.0f), // far right & high ray
        };
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = player.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spine = GameObject.FindWithTag("Spine");
        rightHand = GameObject.FindWithTag("RightHand");
    }

    private void Update()
    {
        AimShottyPosition();

        // Timer to delay between shots
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (gunPickUpScript != null)
            {
                if (ShootingWithShotgun)
                {
                    timer = 0f;
                    ShootShotgun();
                }
            }
        }
    }

    private void ShootShotgun()
    {
        // Adds in all the rays and then calls the Fire function to shoot each ray
        List<Ray> rays = new List<Ray>();
        foreach (Vector3 bullet in bulletSpread)
        {
            rays.Add(Camera.main.ViewportPointToRay(bullet));
        }

        foreach (Ray ray in rays)
        {
            Fire(ray);
        }
    }

    private void Fire(Ray ray)
    {
        StartCoroutine(GunSounds());
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo, hitDistance))
        {
            // Create bullet mark on any object the bullet hits, disappear after 3 seconds.
            GameObject bulletMark = Instantiate(bulletHitMarker, hitInfo.point, Quaternion.identity);
            bulletMark.transform.SetParent(hitInfo.transform);
            Destroy(bulletMark, 3f);

            if (hitInfo.collider.gameObject.CompareTag("Enemy") || hitInfo.collider.gameObject.CompareTag("Rat") || hitInfo.collider.gameObject.CompareTag("Wolf"))
            {
                // Check that they ray hit an animal, spawn blood, animal sound effect, and reduce health.
                GameObject blood = Instantiate(bloodEffect, hitInfo.point, Quaternion.identity);
                Destroy(blood, 3f);
                audioSource.PlayOneShot(animalHitImpact, 1f);
                health = hitInfo.collider.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
            // If the ray hit a target and it's is on quest one or two, then update the UI for those two target quests.
            else if (hitInfo.collider.gameObject.CompareTag("Target") && questGiver.currentQuestIndex == 1 && questGiver.isQuestAccepted ||
                    hitInfo.collider.gameObject.CompareTag("Target") && questGiver.currentQuestIndex == 2 && questGiver.isQuestAccepted)
            {
                UiCounter.Instance.killCount++;
                UiCounter.Instance.UpdateUIKills();
            }
        }
    }

    private void AimShottyPosition()
    {
        if (gunPickUpScript != null)
        {
            if (Input.GetMouseButton(1) && gunPickUpScript.shottyWasPickedUp)
            {
                // Position the gun in the player's hands while aiming
                transform.SetParent(rightHand.transform);
                transform.localPosition = new Vector3(0f, 0f, 0f);
                transform.localRotation = Quaternion.Euler(-13.13f, -51.2f, -75.17f);

                if (!equipSound)
                {
                    audioSource.PlayOneShot(equipGun, 1f);
                    equipSound = true;
                }
                reticle.SetActive(true);
                anim.SetBool("isAiming", true);
            }
            else if (gunPickUpScript.shottyWasPickedUp)
            {
                equipSound = false;
                reticle.SetActive(false);
                anim.SetBool("isAiming", false);

                // Position the gun onto the player's back when not aiming
                transform.SetParent(spine.transform);
                transform.localPosition = new Vector3(0f, 0.0059f, -0.0070f);
                transform.localRotation = Quaternion.Euler(-33f, -86f, -99f);
            }
        }
        else
        {
            gunPickUpScript.shotty.SetActive(false);
        }
    }

    private IEnumerator GunSounds()
    {
        audioSource.PlayOneShot(shootGun, 0.5f);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(reloadGun, 0.2f);
    }
}