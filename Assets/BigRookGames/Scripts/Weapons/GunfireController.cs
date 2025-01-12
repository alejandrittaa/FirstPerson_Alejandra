using UnityEngine;

namespace BigRookGames.Weapons
{
    public class GunfireController : MonoBehaviour
    {
        // --- Audio ---
        public AudioClip GunShotClip;
        public AudioClip ReloadClip;
        public AudioSource source;
        public AudioSource reloadSource;
        public Vector2 audioPitch = new Vector2(.9f, 1.1f);

        // --- Muzzle ---
        public GameObject muzzlePrefab;
        public GameObject muzzlePosition;

        // --- Config ---
        public bool autoFire; // Este se desactivará para evitar disparos automáticos
        public float shotDelay = .5f;
        public bool rotate = true;
        public float rotationSpeed = .25f;

        // --- Options ---
        public GameObject scope;
        public bool scopeActive = true;
        private bool lastScopeState;

        // --- Projectile ---
        [Tooltip("The projectile gameobject to instantiate each time the weapon is fired.")]
        public GameObject projectilePrefab;
        [Tooltip("Sometimes a mesh will want to be disabled on fire. For example: when a rocket is fired, we instantiate a new rocket, and disable" +
            " the visible rocket attached to the rocket launcher")]
        public GameObject projectileToDisableOnFire;

        // --- Timing ---
        [SerializeField] private float timeLastFired;


        private void Start()
        {
            if (source != null) source.clip = GunShotClip;
            timeLastFired = 0;
            lastScopeState = scopeActive;

            // Desactivamos autoFire al inicio
            autoFire = false;
        }

        private void Update()
        {
            // --- Si rotate está activado, rota el arma en escena ---
            if (rotate)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y
                                                                        + rotationSpeed, transform.localEulerAngles.z);
            }

            // --- Disparar arma cuando se presiona la tecla E y se respeta el delay ---
            if (Input.GetKeyDown(KeyCode.E) && ((timeLastFired + shotDelay) <= Time.time))
            {
                FireWeapon();
            }

            // --- Alternar el scope basado en el estado público ---
            if (scope && lastScopeState != scopeActive)
            {
                lastScopeState = scopeActive;
                scope.SetActive(scopeActive);
            }
        }

        /// <summary>
        /// Lógica para disparar el arma.
        /// </summary>
        public void FireWeapon()
        {
            // --- Mantener registro de la última vez que se disparó ---
            timeLastFired = Time.time;

            // --- Generar el destello del disparo ---
            var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);

            // --- Generar proyectil ---
            if (projectilePrefab != null)
            {
                GameObject newProjectile = Instantiate(projectilePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation, transform);
            }

            // --- Deshabilitar objetos, si es necesario ---
            if (projectileToDisableOnFire != null)
            {
                projectileToDisableOnFire.SetActive(false);
                Invoke("ReEnableDisabledProjectile", 3);
            }

            // --- Manejar audio ---
            if (source != null)
            {
                if (source.transform.IsChildOf(transform))
                {
                    source.Play();
                }
                else
                {
                    AudioSource newAS = Instantiate(source);
                    if ((newAS = Instantiate(source)) != null && newAS.outputAudioMixerGroup != null && newAS.outputAudioMixerGroup.audioMixer != null)
                    {
                        newAS.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", Random.Range(audioPitch.x, audioPitch.y));
                        newAS.pitch = Random.Range(audioPitch.x, audioPitch.y);

                        newAS.PlayOneShot(GunShotClip);

                        Destroy(newAS.gameObject, 4);
                    }
                }
            }
        }

        private void ReEnableDisabledProjectile()
        {
            reloadSource.Play();
            projectileToDisableOnFire.SetActive(true);
        }
    }
}