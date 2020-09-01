using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public Camera camera;
    [Tooltip("How far the gun will look when no object at cursor")]
    public float lookAtDist = 100;
    [Tooltip("UI object for the crosshair")]
    public GameObject crosshair;
    [Tooltip("Prefab for the bullet")]
    public GameObject bullet;
    [Tooltip("UI progress bar icon object for reloading")]
    public GameObject reloadCircle;
    [Tooltip("UI object holding the ammo icons")]
    public GameObject ammoUI;
    [Tooltip("Prefab for the ammo UI icon")]
    public GameObject ammoUIPrefab;
    [Tooltip("Maximum ammo the player can have at once")]
    public int maxAmmo = 20;
    [Tooltip("Time in seconds it takes to reload")]
    public float timeToReload = 2;

    private int ammo;
    private bool reloading = false;
    private float time;

    void Start()
    {
        //get the main camera
        camera = transform.parent.GetComponent<Camera>();

        //start the player with full ammo
        ammo = maxAmmo;

        //create a UI icon for each ammo
        for (int i = 1; i <= maxAmmo; i++)
        {
            Instantiate(ammoUIPrefab, ammoUI.transform);
        }
    }

    void Update()
    {
        crosshair.transform.position = Input.mousePosition;

        RaycastHit hit;
        //raycast from the mouse positon on the screen
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        //if the raycast hits something, point the gun at it
        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(hit.point);
        }
        //if no raycast hit, point the gun maxDist away from the mouse position
        else
        {
            transform.LookAt(ray.GetPoint(lookAtDist));
        }

        //if left click is pressed with ammo left and not reloading, create a bullet and remove ammo from the int and UI
        if(Input.GetMouseButtonDown(0) && ammo > 0 && !reloading)
        {
            GameObject go = Instantiate(bullet);
            go.transform.rotation = transform.rotation;
            ammo--;
            Destroy(ammoUI.transform.GetChild(0).gameObject);
        }

        if(reloading)
        {
            time += Time.deltaTime;

            //if reloading has not finished, display the reloading UI icon at the mouse with the appropriate amount of progress
            if (time < timeToReload)
            {
                reloadCircle.transform.position = Input.mousePosition;
                reloadCircle.GetComponent<Image>().fillAmount = time / timeToReload;
            }
            //if done reloading
            else
            {
                reloading = false;

                //hide reloading UI icon by setting progress back to 0
                reloadCircle.GetComponent<Image>().fillAmount = 0;

                //find how much ammo to reload by subtracting max ammo from how much ammo is left, then create an ammo icon for each
                int count = maxAmmo - ammo;

                for (int i = 1; i <= count; i++)
                {
                    Instantiate(ammoUIPrefab, ammoUI.transform);
                }

                //set ammo amount to maximum
                ammo = maxAmmo;
            }
        }
        else
        {
            //if not currently reloading and a key is pressed, start reloading
            if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space))
            {
                time = 0;
                reloading = true;
            }
        }
    }
}
