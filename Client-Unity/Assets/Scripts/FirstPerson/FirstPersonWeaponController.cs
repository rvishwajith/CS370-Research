using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class FirstPersonWeaponController : MonoBehaviour
{
    [Header("Weapon Data")]
    public Transform weapon;
    [SerializeField] private WeaponType weaponType = WeaponType.SemiAuto;
    [ReadOnly] private WeaponState weaponState = WeaponState.Ready;
    [SerializeField] private float weaponCooldownTime = 0.15f;

    [Header("Muzzle Flash")]
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private Transform muzzleFlashPrefab;
    private ParticleSystem muzzleFlashPrefabParticle;

    [Header("Bullet Tracers")]
    [SerializeField] private LineRenderer bulletTracerPrefab;

    private void Start()
    {
        if (muzzleFlashPrefab)
        {
            muzzleFlashPrefabParticle = muzzleFlashPrefab.GetComponentInChildren<ParticleSystem>();
        }
    }

    private void Update()
    {
        bool UserDidTryFireWeapon()
        {
            var fireKey = KeyCode.Mouse0;
            switch (weaponType)
            {
                case WeaponType.SemiAuto:
                    return Input.GetKeyDown(fireKey);
                case WeaponType.FullAuto:
                    return Input.GetKey(fireKey);
                default:
                    return Input.GetKeyDown(fireKey);
            }
        }

        if (weaponState == WeaponState.Ready && UserDidTryFireWeapon())
            FireWeapon();
    }

    private void FireWeapon()
    {
        weaponState = WeaponState.Cooldown;
        FireWeaponVFX();
        DOVirtual.DelayedCall(weaponCooldownTime, () =>
        {
            weaponState = WeaponState.Ready;
        });
    }

    private void FireWeaponVFX()
    {
        void AddBulletTracer()
        {
            var tracer = Instantiate(bulletTracerPrefab, muzzlePoint);
            tracer.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            var lifetime = 0.05f;
            var noiseStrength = 0.05f;
            var length = Camera.main.farClipPlane;

            var noise = UnityEngine.Random.insideUnitCircle * noiseStrength;
            var dir = tracer.transform.TransformDirection(
                Vector3.Slerp(Vector3.Slerp(Vector3.forward, Vector3.right, noise.x),
                Vector3.up, noise.y));
            var positions = new Vector3[]
            {
                tracer.transform.position,
                tracer.transform.position + dir * length
            };
            tracer.SetPositions(positions);
            Destroy(tracer.gameObject, lifetime);
        }

        void AddMuzzleFlash()
        {
            var muzzleFlashInstance = Instantiate(muzzleFlashPrefabParticle, muzzlePoint);
            var lifetime = 0.05f;

            muzzleFlashInstance.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            muzzleFlashInstance.Pause();
            muzzleFlashInstance.Play();
            Destroy(muzzleFlashInstance.gameObject, lifetime);
        }

        if (weapon != null && muzzlePoint != null)
        {
            if (muzzleFlashPrefabParticle != null)
                AddMuzzleFlash();
            if (bulletTracerPrefab != null)
                AddBulletTracer();
        }
    }

    private void OnGUI()
    {

    }
}

/*
 * WeaponController class is better but it's not being used right now.
 */
/*
[System.Serializable]
class WeaponController
{
    public Transform weapon;

    public WeaponType type = WeaponType.SemiAuto;
    public WeaponState state;

    public int currMags;
    public int currBullets;

    [System.Serializable]
    class VFXSettings
    {
        [Header("VFX Settings")]
        public Transform muzzle;
        public ParticleSystem muzzleFlashPrefab;
        public LineRenderer bulletTracerPrefab;
    }
    private VFXSettings vfxSettings;

    [System.Serializable]
    class AmmoSettings
    {
        [Header("Firing Settings")]
        public int mags = 5;
        public int bulletsPerMag = 30;
    }
    private AmmoSettings ammoSettings;

    public WeaponController()
    {
        void ApplyAmmoSettings()
        {
            currMags = ammoSettings.mags;
            currBullets = ammoSettings.bulletsPerMag;
            currMags -= 1;
        }
        ApplyAmmoSettings();
        state = WeaponState.Ready;
    }
}
*/