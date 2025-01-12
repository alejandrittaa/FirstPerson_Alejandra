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
        [Tooltip("Damage inflicted on the enemy when hit.")]
        public int daño = 20; // Daño que inflige el arma
        public float rangoDisparo = 50f; // Rango del disparo

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
                Debug.Log("Tecla E presionada. Llamando a FireWeapon.");
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

            // --- Raycast para detectar impacto ---
            RaycastHit hit;
            if (Physics.Raycast(muzzlePosition.transform.position, muzzlePosition.transform.forward, out hit, rangoDisparo))
            {
                Debug.Log($"Impacto en: {hit.collider.name}");

                // Intentar obtener el componente Enemigo del objeto impactado o de sus padres
                Enemigo enemigo = hit.collider.GetComponentInParent<Enemigo>();
                if (enemigo != null)
                {
                    enemigo.RecibirDaño(daño);
                }
            }

            // --- Manejar audio ---
            // --- Manejar audio del disparo ---
            if (source != null)
            {
                source.clip = GunShotClip; // Asegúrate de que se use el clip correcto
                source.PlayOneShot(GunShotClip); // Reproduce el sonido del disparo
            }

            // --- Manejar audio del proyectil deshabilitado ---
            if (projectileToDisableOnFire != null)
            {
                projectileToDisableOnFire.SetActive(false);
                Invoke("ReEnableDisabledProjectile", 3);
            }
        }

        private void ReEnableDisabledProjectile()
        {
            // --- Manejar audio de recarga ---
            if (reloadSource != null)
            {
                reloadSource.clip = ReloadClip; // Asegúrate de que se use el clip correcto
                reloadSource.PlayOneShot(ReloadClip); // Reproduce el sonido de recarga
            }

            if (projectileToDisableOnFire != null)
            {
                projectileToDisableOnFire.SetActive(true);
            }
        }
    }
}