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
		transform.position = new Vector3 ( transform.position.x, transform.position.y, m_target.transform.position.z + m_relativePosition.z );
    }
}
