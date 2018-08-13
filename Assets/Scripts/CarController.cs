using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CarController : MonoBehaviour {

	 public float topSpeed = 50; // km per hour
 	private float currentSpeed = 0;
 	private float pitch = 0;
	
	bool move = false;
	bool isGrounded = false;

	public Rigidbody2D rb;
	public TextMeshProUGUI scoreText;
	public AudioClip click;
	public GameObject ui;
	public GameObject startPanelUI;
	public TextMeshProUGUI text;
	public TextMeshProUGUI STENtext;
	public GameObject backButton;
	string score;

	public float speed = 20f;
	public float rotationSpeed = 2f;	
	
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.P)) Toggle();
		if(transform.position.y <= -15) StartCoroutine(EndGame());
		if (Input.GetButtonDown("Fire1"))
		{
			move = true;
		}
		if (Input.GetButtonUp("Fire1"))
		{
			move = false;
		}
	}

	private void FixedUpdate()
	{
		if (move == true)
		{
			if (isGrounded)
			{
				rb.AddForce(transform.right * speed * Time.fixedDeltaTime * 100f, ForceMode2D.Force);
				if(transform.position.x > 0){
					score = string.Format("{0:0}",transform.position.x);
					scoreText.text =  score;
				}			

			} else
			{
				rb.AddTorque(rotationSpeed * Time.fixedDeltaTime * 100f, ForceMode2D.Force);
			}
		}
			currentSpeed = transform.GetComponent <Rigidbody2D> ().velocity.magnitude * 3.6f;
     		pitch = currentSpeed / topSpeed; 
     		transform.GetComponent<AudioSource>().pitch = pitch;
	}

	private void OnCollisionEnter2D()
	{
		isGrounded = true;
	}

	private void OnCollisionExit2D()
	{
		isGrounded = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Finish")
		{
			transform.GetComponent<AudioSource>().Stop();
			EndPanel();
		}
		else StartCoroutine(EndGame());
		
	}


	public void Restart()
	{
		Time.timeScale = 1f;	
		SoundManager.instance.PlaySingle(click);		
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Toggle(){
		SoundManager.instance.PlaySingle(click);
		text.text = "PAUSE";
		backButton.SetActive(true);	
		ui.SetActive(!ui.activeSelf);
		if(ui.activeSelf){
			Time.timeScale = 0f;
			transform.GetComponent<AudioSource>().Stop();
		} else
		{
			Time.timeScale = 1f;	
			transform.GetComponent<AudioSource>().Play();
		} 	
	}

	IEnumerator EndGame()
	{
		transform.GetComponent<AudioSource>().Stop();
		yield return new WaitForSeconds(2f);
		Time.timeScale = 0f;
		backButton.SetActive(false);
		text.text = "GAME OVER";
		ui.SetActive(true);
	}

	public void EndPanel(){		
		STENtext.text = "FINISH";				
		SoundManager.instance.PlaySingle(click);		
		startPanelUI.SetActive(!startPanelUI.activeSelf);
		if(startPanelUI.activeSelf){
			Time.timeScale = 0f;
		} else Time.timeScale = 1f;		
	}
}
