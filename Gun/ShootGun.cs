using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    private const float fireRate = 1f;
    private const int damage = 1;
    private const float hitDistance = 60f;
    private float timer;
    private Health health;
    private AudioSource audioSource;
    [SerializeField] private AudioClip aimGun;
    [SerializeField] private AudioClip shootGun;
    [SerializeField] private AudioClip reloadGun;
    [SerializeField] private AudioClip animalHitImpact;
    [SerializeField] private AudioClip equipGun;
    [SerializeField] private GunPickUp gunPickUpScript;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private GameObject spine;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private QuestGiver questGiver;
    [SerializeField] private GameObject bulletHitMarker;
    [SerializeField] private GameObject reticle;
    private bool equipSound;
    private Animator anim;
    private GameObject player;
    private bool ShootingWithRifle => Input.GetMouseButton(1) && Input.GetMouseButtonDown(0) && gunPickUpScript.rifleWasPickedUp;

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
        AimGunPosition();

        // Timer to delay between shots
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (gunPickUpScript != null)
            {
                if (ShootingWithRifle)
                {
                    timer = 0f;
                    Shoot();
                }
            }
        }
    }

    private void Shoot()
    {
        StartCoroutine(GunSounds());

        // One raycast coming from the camera
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, hitDistance))
        {   
            // Create bullet mark on any object the bullet hits, disappear after 3 seconds.
            GameObject bulletMark = Instantiate(bulletHitMarker, hitInfo.point, Quaternion.identity);
            bulletMark.transform.SetParent(hitInfo.transform);
            Destroy(bulletMark, 3f);

            // Check that they ray hit an animal, spawn blood, animal sound effect, and reduce health.
            if (hitInfo.collider.gameObject.CompareTag("Enemy") || hitInfo.collider.gameObject.CompareTag("Rat") || hitInfo.collider.gameObject.CompareTag("Wolf"))
            {
                GameObject blood = Instantiate(bloodEffect, hitInfo.point, Quaternion.identity);
                Destroy(blood, 3f);
                audioSource.PlayOneShot(animalHitImpact, 0.8f);
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

    private void AimGunPosition()
    {
        if (gunPickUpScript != null)
        {
            if (Input.GetMouseButton(1) && gunPickUpScript.rifleWasPickedUp)
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
            else if (gunPickUpScript.rifleWasPickedUp)
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
            gunPickUpScript.gun.SetActive(false);
        }
    }

    private IEnumerator GunSounds()
    {
        audioSource.PlayOneShot(shootGun, 0.5f);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(reloadGun, 0.7f);
    }
}
