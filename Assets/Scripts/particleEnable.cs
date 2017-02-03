using UnityEngine;

public class particleEnable : MonoBehaviour
{

    public GameObject[] particles;
    public Transform _player;
	// Use this for initialization
	void Start () 
        {

        }
	
	// Update is called once per frame


    public void SpawnParticle(int index, float lifeTime)
    {
        GameObject particle = Instantiate(particles[index], _player.position, Quaternion.AngleAxis(-90, Vector3.right)) as GameObject;
        Destroy(particle, lifeTime);
    }
}
