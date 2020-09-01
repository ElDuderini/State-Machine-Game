using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public Camera camera;
    public float maxDist = 100;
    public GameObject bullet;
    public GameObject reloadCircle;
    public GameObject ammoUI;
    public GameObject ammoUIPrefab;
    public int maxAmmo = 20;
    private int ammo;
    public float timeToReload = 2;
    private bool reloading = false;
    private float time;

    void Start()
    {
        camera = transform.parent.GetComponent<Camera>();
        ammo = maxAmmo;

        for (int i = 1; i <= maxAmmo; i++)
        {
            Instantiate(ammoUIPrefab, ammoUI.transform);
        }
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(hit.point);
        }
        else
        {
            transform.LookAt(ray.GetPoint(maxDist));
        }

        if(Input.GetMouseButtonDown(0) && ammo > 0 && !reloading)
        {
            GameObject go = Instantiate(bullet);
            go.transform.rotation = transform.rotation;
            ammo--;
            Destroy(ammoUI.transform.GetChild(0).gameObject);
        }

        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            time = 0;
            reloading = true;
        }

        if(reloading)
        {
            time += Time.deltaTime;
            
            if(time < timeToReload)
            {
                reloadCircle.transform.position = Input.mousePosition;
                reloadCircle.GetComponent<Image>().fillAmount = time / timeToReload;
            }
            else
            {
                reloading = false;
                reloadCircle.GetComponent<Image>().fillAmount = 0;
                ammo = maxAmmo;

                int count = maxAmmo - ammoUI.transform.childCount;

                for (int i = 1; i <= count; i++)
                {
                    Instantiate(ammoUIPrefab, ammoUI.transform);
                }
            }
        }
    }
}
