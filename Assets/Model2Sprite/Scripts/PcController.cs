using UnityEngine;
using System.Collections;

/// <summary>
/// manage character animation steps
/// </summary>
public class PcController : MonoBehaviour {
	Animator animator;
	bool playbackFlag = false;
	public float aniLength = 1f;
	Vector3 direct = new Vector3(0, 0, 0);

	void Start () {
		animator = GetComponent<Animator>();
		animator.speed = 0f;
		
		GameObject.Find("AniSlider").GetComponent<UISlider>().eventReceiver = gameObject;
		
		StartCoroutine(AnimationCapture(0.1f));
	}
	
	IEnumerator AnimationCapture(float delayTime) {
		animator.StartRecording(30);
		yield return new WaitForSeconds(delayTime);
		animator.StopRecording ();
		animator.StartPlayback ();
		Debug.Log ("StopRecording : " + animator.recorderStopTime);
		//playbackFlag = true;
		animator.playbackTime = 0f;
		AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
		Debug.Log( "state.length: " + state.length );
		aniLength = state.length;
	}
	
	void OnSliderChange(float val){
		if (playbackFlag) return;
		if (animator==null) return;
		animator.playbackTime = val * aniLength;
	}

	void OnSliderChangeH(float val){
		if (playbackFlag) return;
		if (animator==null) return;
		Vector3 pos = new Vector3(0, (val-0.5f)*8, 0);
		transform.position = pos;
	}

	void OnSliderChangeX(float val){
		if (playbackFlag) return;
		if (animator==null) return;
		direct = new Vector3((val+0.5f)*360, direct.y, 0);
		Quaternion rot = Quaternion.Euler(direct);
		transform.rotation = rot;
	}

	void OnSliderChangeY(float val){
		if (playbackFlag) return;
		if (animator==null) return;
		direct = new Vector3(direct.x, (val+0.5f)*360, 0);
		Quaternion rot = Quaternion.Euler(direct);
		transform.rotation = rot;
	}

	// Update is called once per frame
	void Update () {
		if (playbackFlag) {
			float newPlaybackTime = animator.playbackTime + Time.deltaTime;
			if (newPlaybackTime > animator.recorderStopTime) {
				newPlaybackTime = 0;
			}
			animator.playbackTime = newPlaybackTime;
		}
	}
}
