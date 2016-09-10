using UnityEngine;
using System.Collections;


public class CameraController : MonoBehaviour {

    public GameObject m_target;

	[SerializeField]
    public string m_playerName = "Player";
	private Vector3 m_relativePosition;

    void Start () {
        m_relativePosition = gameObject.transform.position - m_target.transform.position;
    }

    void LateUpdate () {
		transform.position = new Vector3 ( m_target.gameObject.transform.position.x, transform.position.y, transform.position.z );
    }
}
