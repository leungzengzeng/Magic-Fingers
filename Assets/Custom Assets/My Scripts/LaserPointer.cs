using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//What if the laser is just always active?
public class LaserPointer : MonoBehaviour {
	//Vive script to be modified for Hi5 VR Glove. Attach as a component to aimin (trying to write it to attach to hands).
	[SerializeField]
	//Laser Components
	public GameObject aimer; 
	public GameObject laserPrefab;
	private GameObject laser;
	private Transform laserTransform;
	private Vector3 hitPoint; 
	//Hand Components
	//Hi5HandInteraction aimingInteraction;
	GameObject castingHand; //Hand which gestures to create spells
	 
	Hi5HandInteraction hi5HandInteraction;
	//Teleport Components
//	public Transform cameraRigTransform; 
//	public GameObject teleportReticlePrefab;
//	private GameObject reticle;
//	private Transform teleportReticleTransform;
//	public Transform headTransform; 
//	public Vector3 teleportReticleOffset; 
//	public LayerMask teleportMask; 
//	private bool shouldTeleport;

	void Awake()
	{
		aimer = GameObject.FindGameObjectWithTag ("aimingHand"); //want to create an object for which the aiming is controlled 
		castingHand = GameObject.FindGameObjectWithTag ("castingHand");
		hi5HandInteraction = castingHand.GetComponent <Hi5HandInteraction> ();
		//aimingInteraction = aimingHand.GetComponent <Hi5HandInteraction> ();

	}


	private void ShowLaser(RaycastHit hit)
	{
		
		laser.SetActive(true);
		laserTransform.position = Vector3.Lerp(aimer.transform.position, hitPoint, .5f);
		laserTransform.LookAt(hitPoint); 
		laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
	}

	void Start () {
		laser = Instantiate(laserPrefab);
		laserTransform = laser.transform;
	}
	

	void Update () {
		if (hi5HandInteraction.m_IsGrabbing == true) //If the object is the casting hand, and the casting hand is grabbing...
		{
			RaycastHit hit;


			if (Physics.Raycast(aimer.transform.position, transform.right, out hit, 100))
			{
				hitPoint = hit.point;
				ShowLaser(hit);
			}
		}
		else
		{
			laser.SetActive(false);
		}
	}
}
