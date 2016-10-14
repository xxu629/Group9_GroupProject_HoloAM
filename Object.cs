using UnityEngine;
using System.Collections;
using Pathfinding.Serialization.JsonFx;
using UnityEngine.SceneManagement;

public class Object : MonoBehaviour
{
    public GameObject myPrefab;
    public GameObject the_cube;
    public GameObject model;
    public GameObject the_line;
    public string jsonResponse;

    public string _WebsiteURL = "http://xxu629.azurewebsites.net/tables/Volcano?zumo-api-version=2.0.0";
    // Use this for initialization
    void Start()
    {

        jsonResponse = Request.GET(_WebsiteURL);
        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }
        Volcano[] Rock = JsonReader.Deserialize<Volcano[]>(jsonResponse);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.tag == "object")
                {
                    GameObject newSphere = Instantiate(myPrefab, new Vector3(hitInfo.point.x, hitInfo.point.y * 2, hitInfo.point.z), Quaternion.identity) as GameObject;
                    //newSphere.name = "newSphere" + i;
                    //i += 1;
                }
                if (hitInfo.collider.tag == "info")
                {
                    hitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    GameObject newCube = Instantiate(the_cube, new Vector3(hitInfo.point.x, hitInfo.point.y * 1.5f, hitInfo.point.z), Quaternion.identity) as GameObject;
                    
                    Volcano[] Rock = JsonReader.Deserialize<Volcano[]>(jsonResponse);
                    newCube.GetComponentInChildren<TextMesh>().text = "Rock Type: " + Rock[1].RockType + "\n" + "From: " + Rock[1].VolcanoFrom + "\n" + "Discover: " + Rock[1].Discover + "\n" + "Year_Discovered: " + Rock[1].DicoveredDate;
                }
                if (hitInfo.collider.tag == "button")
                {
                    hitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    GameObject newCube = Instantiate(model, new Vector3(hitInfo.point.x, hitInfo.point.y * 1.5f, hitInfo.point.z), Quaternion.identity) as GameObject;
                    model.GetComponent<Renderer>().material.color = Color.grey;
                }
                if (hitInfo.collider.tag == "link")
                {
                    hitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    GameObject newCube = Instantiate(the_line, new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z), Quaternion.identity) as GameObject;
                }
                if (hitInfo.collider.tag == "Screen")
                {
                    hitInfo.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    SceneManager.LoadScene("the_map");
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.tag == "button" || hitInfo.collider.tag == "TheBoard" || hitInfo.collider.tag == "model" || hitInfo.collider.tag == "link")
                {
                    Destroy(hitInfo.collider.gameObject);
                    //newSphere.name = "newSphere" + i;
                    //i += 1;
                }
            }
        }
    }
}
