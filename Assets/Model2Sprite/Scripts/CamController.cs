using UnityEngine;
using System.Collections;

/// <summary>
/// camera control manager
/// </summary>
public class CamController : MonoBehaviour {
	public Transform dummy;
	Vector3 direct = new Vector3(0f, 0f, 0f);
	float dist = 0f;

	void OnSliderChangeX(float val){
		direct = new Vector3((val+0.5f)*360f, direct.y, 0f);
		dummy.rotation = Quaternion.Euler(direct);
		transform.LookAt(dummy);
	}

	void OnSliderChangeY(float val){
		direct = new Vector3(direct.x, (val+0.5f)*360f, 0f);
		dummy.rotation = Quaternion.Euler(direct);
		transform.LookAt(dummy);
	}

	void OnSliderChangeH(float val){
		Vector3 pos = new Vector3(0f, (0.5f-val)*8f, 0f);
		dummy.position = pos;
	}

	void Start () {
		dist = Vector3.Distance(transform.position, dummy.position);
	}
}
