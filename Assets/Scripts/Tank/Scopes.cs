using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scopes : MonoBehaviour {

	public Texture2D closeScope;
	public Texture2D farScope;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	Transform m_transform;
	// Use this for initialization
	void Start () {
		m_transform = this.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = -(m_transform.position.x - Camera.main.transform.position.x);
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
		float distance = Mathf.Sqrt (Mathf.Pow (worldPos.x - m_transform.position.x, 2) + Mathf.Pow (worldPos.y - m_transform.position.y, 2));

		if (distance < BarrelShooting.directMaxDistance) {
			Cursor.SetCursor (closeScope, hotSpot, cursorMode);
		} else {
			Cursor.SetCursor (farScope, hotSpot, cursorMode);
		}
	}
}
