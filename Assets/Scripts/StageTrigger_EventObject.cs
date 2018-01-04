﻿using UnityEngine;
using System.Collections;

public class StageTrigger_EventObject : MonoBehaviour {

	// === 外部パラメータ（インスペクタ表示） =====================
	[System.Serializable]
	public class Rigidbody2DParam {
		public bool						enabled 					= true;
		public float 					mass 						= 1.0f;
		public float	 				linearDrag 					= 0.0f;
		public float 					angularDrag 				= 0.05f;
		public float 					gravityScale 				= 1.0f;
		public bool  					fixedAngle 					= false;
		public bool  					isKinematic 				= false;
		public RigidbodyInterpolation2D interpolation				= RigidbodyInterpolation2D.None;
		public RigidbodySleepMode2D 	sleepingMode				= RigidbodySleepMode2D.StartAwake;
		public CollisionDetectionMode2D collisionDetection 			= CollisionDetectionMode2D.Discrete;
		
		[Header("-------------------")]
		public Vector2 					centerOfMass 				= new Vector2(0.0f,0.0f);
		public Vector2 					velocity					= new Vector2(0.0f,0.0f);
		public float 					angularVelocity 			= 0.0f;

		[Header("-------------------")]
		public bool 					addForceEnabled 			= false;
		public Vector2					addForcePower 				= new Vector2 (0.0f, 0.0f);
		public bool 					addForceAtPositionEnabled 	= false;
		public GameObject				addForceAtPositionObject;
		public Vector2					addForceAtPositionPower	 	= new Vector2 (0.0f, 0.0f);
		public bool 					addRelativeForceEnabled 	= false;
		public Vector2					addRelativeForcePower	 	= new Vector2 (0.0f, 0.0f);
		public bool 					addTorqueEnabled 			= false;
		public float					addTorquePower				= 0.0f;
		public bool 					movePositionEnabled 		= false;
		public Vector2					movePosition			 	= new Vector2 (0.0f, 0.0f);
		public bool 					moveRotationEnabled 		= false;
		public float					moveRotation				= 0.0f;
	}

	public float 					runTime 	= 0.0f;
	public float 					destoryTime = 0.0f;

	[Space(10)]
	public bool						sendMesseageObjectEnabled 		= false;
	public string					sendMesseageString 				= "OnTriggerEnter2D_PlayerEvent";
	public GameObject[] 			sendMesseageObjectList;
	public bool						instantiateGameObjectEnabled 	= false;
	public GameObject[] 			instantiateGameObjectList;

	[Space(10)]
	public Rigidbody2DParam 		rigidbody2DParam;

	// === 外部パラメータ ======================================
	[System.NonSerialized] public bool triggerOn = false;


	// === コード（Monobehaviour基本機能の実装） ================
	void OnTriggerEnter2D_PlayerEvent (GameObject go) {
		Invoke ("runTriggerWork",runTime);
	}

	void runTriggerWork() {

		if (rigidbody2DParam.enabled) {
			if (gameObject.GetComponent<Rigidbody2D> () == null) {
				gameObject.AddComponent<Rigidbody2D> ();
			}

			gameObject.GetComponent<Rigidbody2D>().mass 				= rigidbody2DParam.mass;
			gameObject.GetComponent<Rigidbody2D>().drag 				= rigidbody2DParam.linearDrag;
			gameObject.GetComponent<Rigidbody2D>().angularDrag 			= rigidbody2DParam.angularDrag;
			gameObject.GetComponent<Rigidbody2D>().gravityScale 		= rigidbody2DParam.gravityScale;
			gameObject.GetComponent<Rigidbody2D>().fixedAngle 			= rigidbody2DParam.fixedAngle;
			gameObject.GetComponent<Rigidbody2D>().isKinematic 			= rigidbody2DParam.isKinematic;
			gameObject.GetComponent<Rigidbody2D>().interpolation 		= rigidbody2DParam.interpolation;
			gameObject.GetComponent<Rigidbody2D>().sleepMode 			= rigidbody2DParam.sleepingMode;
			gameObject.GetComponent<Rigidbody2D>().collisionDetectionMode 	= rigidbody2DParam.collisionDetection;

			gameObject.GetComponent<Rigidbody2D>().centerOfMass 		= rigidbody2DParam.centerOfMass;
			gameObject.GetComponent<Rigidbody2D>().velocity				= rigidbody2DParam.velocity;
			gameObject.GetComponent<Rigidbody2D>().angularVelocity 		= rigidbody2DParam.angularVelocity;

			if (rigidbody2DParam.addForceEnabled) {
				gameObject.GetComponent<Rigidbody2D>().AddForce(rigidbody2DParam.addForcePower);
			}
			if (rigidbody2DParam.addForceAtPositionEnabled) {
				gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(
					rigidbody2DParam.addForceAtPositionPower,
					rigidbody2DParam.addForceAtPositionObject.transform.position);
			}
			if (rigidbody2DParam.addRelativeForceEnabled) {
				gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(rigidbody2DParam.addRelativeForcePower);
			}
			if (rigidbody2DParam.addTorqueEnabled) {
				gameObject.GetComponent<Rigidbody2D>().AddTorque(rigidbody2DParam.addTorquePower);
			}
			if (rigidbody2DParam.movePositionEnabled) {
				gameObject.GetComponent<Rigidbody2D>().MovePosition(rigidbody2DParam.movePosition);
			}
			if (rigidbody2DParam.moveRotationEnabled) {
				gameObject.GetComponent<Rigidbody2D>().MoveRotation(rigidbody2DParam.moveRotation);
			}
		}

		if (sendMesseageObjectEnabled && sendMesseageObjectList != null) {
			foreach(GameObject go in sendMesseageObjectList) {
				go.SendMessage(sendMesseageString,gameObject);
			}
		}
		if (instantiateGameObjectEnabled && instantiateGameObjectList != null) {
			foreach(GameObject go in instantiateGameObjectList) {
				Instantiate(go);
			}
		}

		if (destoryTime > 0.0f) {
			Destroy(gameObject,destoryTime);
		}
		
		triggerOn = true;
	}
}
