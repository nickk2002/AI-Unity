using System.Collections;
using UnityEngine;

public class Arma : MonoBehaviour
{
    // numele armei
    private Rigidbody rigidbody;

    public enum ModTragere
    {
        Automat,
        SemiAutomat,
    }

    [System.Serializable]
    public class SetariArma
    {
        [Header("Setarile Armei")]
        public string nume;
        public float damage;
        public float distanta = 100f;
        public float spread;
        public ModTragere modTragere = ModTragere.Automat;
        public float rataTragereAuto; // din cate in cate secunde trage arma automata
        public float rataTragereSemiAuto; // din cate in cate secunde trage arma semiautomata

        public bool reincarcareAuto = false;  // daca arma se reincarca automat sau nu, true -> se incarca , false -> nu se incarca automat

        public Transform ejectPosition; // de unde iese glontul
        [Header("Other")]

        [Header("Muzzle Flash")]

        public bool areMuzzleFlash = false;
        public GameObject muzzleFlash;
        public float timpMuzzleFlash = 1f; // cat timp dureaza efectul

        [Header("Reload")]
        public float timpReload;

        [Header("Pozitionarea armei")]
        public Transform armaEchipata;
        public Transform armaNeechipata;

        [Header("Effects")]
        public bool ejectShells;
        public GameObject shellPrefab;// prefab de glont
        public Transform shellTransform; // pozitia de unde iese



    }
    [System.Serializable]
    public class SetariGloante
    {
        [Header("Setari Gloante")]
        public GameObject glontPrefab; // prefabul care va fi pus pe perete atunci cand se trage;
        public int gloanteTotal; // cate gloante 'cara' player-ul momentan 
        public int maximCartus; // cate gloante incap maxim intr-un cartus
        public int cartusCurent; // cate gloante are momentan in cartus

    }

    [SerializeField] private SetariArma setariArma;
    [SerializeField] private SetariGloante setariGloante;


    private bool isReloading = false; // daca arma se reincarca
    [SerializeField] private bool gloanteInfinite;
    private float timpAuto = 0;

    private void InstatiazaGlont(Vector3 position)
    {
        GameObject glont = Instantiate(setariGloante.glontPrefab);
        glont.transform.position = position;
        //glont.transform.SetParent(this.gameObject.transform);
        Destroy(glont, Random.Range(10f, 30f));
    }
    private void IncarcaCartus()
    {

        int curent = setariGloante.cartusCurent;
        int maxim = setariGloante.maximCartus;
        int total = setariGloante.gloanteTotal;
        if (total > maxim - curent)
        {
            total -= maxim - curent;
            curent = maxim;
        }
        else
        {
            curent = total;
            total = 0;
        }
        setariGloante.cartusCurent = curent;
        setariGloante.gloanteTotal = total;

    }
    private void Reload()
    {
        if (isReloading)
            return;
        // daca numarul total de gloante totale este  mai mic decat 0 atunci nu e bine
        if (setariGloante.gloanteTotal <= 0)
        {
            return;
        }
        StartCoroutine(StopReloading());
    }
    IEnumerator StopReloading()
    {
        yield return new WaitForSeconds(setariArma.timpReload);
        isReloading = false;
        IncarcaCartus();
    }
    private void ScadeGloante()
    {
        if (!gloanteInfinite)
        {
            setariGloante.cartusCurent -= 1;
            setariGloante.gloanteTotal -= 1;
        }

    }
    public void TrageGlont()
    {

        RaycastHit hit;
        Transform eject = setariArma.ejectPosition;
        Vector3 dir = eject.InverseTransformDirection(eject.forward);
        Vector3 ejectPos = setariArma.ejectPosition.position;

        Debug.DrawRay(ejectPos, dir * 100);
        dir += new Vector3(Random.Range(0, 1), 0, Random.Range(0, 1)) * setariArma.spread;

        if (Physics.Raycast(ejectPos, dir, out hit, setariArma.distanta))
        {
            //if(hit.collider.gameObject.isStatic)
            #region trage glont
            Vector3 wallPosition = hit.point;
            Vector3 direction = (wallPosition - eject.position).normalized;

            Vector3 actualPosition = wallPosition - direction * 0.25f;

            Quaternion rotation = Quaternion.LookRotation(hit.normal);
            GameObject glont = Instantiate(setariGloante.glontPrefab, actualPosition, rotation);

            Destroy(glont, Random.Range(10f, 30f));
            ScadeGloante();
            #endregion

            #region muzzleFlash
            if (setariArma.areMuzzleFlash)
            {
                Transform transformGlont = setariArma.ejectPosition;
                GameObject muzzleFlash = Instantiate(setariArma.muzzleFlash, transformGlont.position, Quaternion.identity);
                muzzleFlash.transform.SetParent(transformGlont);
                Destroy(muzzleFlash, setariArma.timpMuzzleFlash);
            }
            #endregion

            #region shellEffect
            if (setariArma.ejectShells)
            {
                ShellEfect();
            }


            #endregion

        }
    }
    private bool CanShoot()
    {
        if (Input.GetButton("Fire1") && (setariGloante.cartusCurent > 0 || gloanteInfinite) && !isReloading)
        {

            if (setariArma.modTragere == ModTragere.Automat && timpAuto >= setariArma.rataTragereAuto)
            {
                timpAuto = 0;
                return true;
            }
        }
        return false;
    }
    private void ShellEfect()
    {
        GameObject shell = Instantiate(setariArma.shellPrefab);
        shell.transform.position = setariArma.shellTransform.position;

    }
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Assert(setariGloante.gloanteTotal >= setariGloante.maximCartus);
        IncarcaCartus();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("DAaa");
            Reload();
        }
        if (CanShoot())
            TrageGlont();
        if (setariArma.modTragere == ModTragere.Automat)
            timpAuto += Time.deltaTime;
    }
}
