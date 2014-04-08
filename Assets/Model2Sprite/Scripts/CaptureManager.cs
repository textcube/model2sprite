using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// capture manager for 2D-Animated Sequences.
/// </summary>
public class CaptureManager : MonoBehaviour {
    public Texture2D capturedImage;
	public UISlider slider;
	public PcController pcCtrl;
	int count = 0;
	public bool okCapture = true;
	float step = 0.1f;
	float total = 1f;
	
	string folder;
	public string fileName = "pc_attack";
	
	bool isDoing = false;

    void Start() {
		//folder = Application.dataPath + "/Captures";
		folder = "C:/Users/Administrator/Pictures/Captures";
		if (!Directory.Exists(folder)) {
			Directory.CreateDirectory(folder);
		}
		
		Object[] objects = GameObject.FindObjectsOfType(typeof(Animator));
		Animator animator = (Animator)objects[0];
		pcCtrl = animator.GetComponent<PcController>();
		
		if (okCapture) StartCoroutine( MakeSpriteFiles(3f) );
    }

	IEnumerator MakeSpriteFiles(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		total = pcCtrl.aniLength;
		StartCoroutine( DelayCapture(1f) );
	}

	IEnumerator DelayCapture(float delayTime) {
		yield return new WaitForSeconds(delayTime);
		if (count * step < total) {
			slider.sliderValue = count * step / total;
			StartCoroutine( Capture() );
			StartCoroutine( DelayCapture(delayTime) );
		}
	}
	
	void OnRecordButton(){
		StartCoroutine( MakeSpriteFiles(1f) );
	}

	IEnumerator Capture() {
		isDoing = true;
		yield return new WaitForEndOfFrame();
        Rect lRect = new Rect(1f, 1f, Screen.width - 2, Screen.height - 2);
        if (capturedImage) Destroy(capturedImage);
        capturedImage = zzTransparencyCapture.capture(lRect);
		SaveTextureToFile(capturedImage , "/"+fileName+"_"+(count++).ToString("00")+".png");
		isDoing = false;
    }
	
    public void Update() {
        if (Input.GetKeyDown(KeyCode.C)) StartCoroutine( Capture() );
        if (Input.GetKeyDown(KeyCode.S)) Destroy(capturedImage);

    }
	
	void SaveTextureToFile(Texture2D texture, string fileName){
		byte[] bytes = texture.EncodeToPNG();
		System.IO.File.WriteAllBytes(folder + fileName, bytes);
	}

    void OnGUI() {
		/*
        if (capturedImage) {
            GUI.DrawTexture(
                new Rect(Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.8f, Screen.height * 0.8f),
                capturedImage,
                ScaleMode.ScaleToFit,
                true);
            GUI.color = Color.green;
            GUILayout.Label("press S to clear");
        }
        */
    }
}