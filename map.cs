using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using

public class map : MonoBehaviour {
    public GameObject myPrefab;
    // Put your URL here
    public string _WebsiteURL = "http://xxu629.azurewebsites.net/tables/Volcano?zumo-api-version=2.0.0"; //http://{Your Site Name}.azurewebsites.net/tables/{Your Table Name}?zumo-api-version=2.0.0
    public string jsonResponse;

    void Start() {

        string jsonResponse = Request.GET(_WebsiteURL);

        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

     
        Volcano[] location = JsonReader.Deserialize<Volcano[]>(jsonResponse);

        int aaaaaa = 0;
        foreach (Volcano volcano in location) {

            int x = volcano.LocationX;
            int y = volcano.LocationY;
            int z = volcano.LocationZ;

      
            GameObject newcube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
            newcube.name = "newcube" + aaaaaa;
            aaaaaa = aaaaaa + 1;
        }


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
                if (hitInfo.collider.tag == "location")
                {
                    GameObject newCube = Instantiate(myPrefab, new Vector3(hitInfo.point.x, hitInfo.point.y * 1.5f, hitInfo.point.z), Quaternion.identity) as GameObject;                    Volcano[] Rock = JsonReader.Deserialize<Volcano[]>(jsonResponse);
                    myPrefab.GetComponentInChildren<TextMesh>().text = "Name: " + Rock[1].VolcanoFrom + "\n" + "City Located: " + Rock[1].City + "\n" + "Last eruption: " + Rock[1].LastErruption;
                }
            }
        }
    }
}
