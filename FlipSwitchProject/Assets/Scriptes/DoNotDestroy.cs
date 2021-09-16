using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{

	void Awake()
	{
		GameObject[] obj = GameObject.FindGameObjectsWithTag("MSMusic");
		GameObject[] obj2 = GameObject.FindGameObjectsWithTag("Ad");
		if (obj.Length > 1)
			Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);
	}
}