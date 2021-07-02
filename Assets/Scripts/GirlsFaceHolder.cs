using UnityEngine;

public class GirlsFaceHolder : MonoBehaviour
{
	[SerializeField] private Sprite RaelleFace;
	[SerializeField] private Sprite AbigailFace;
	[SerializeField] private Sprite TallyFace;

	public Sprite Raelle {
		get { return RaelleFace; }
	}

	public Sprite Abigail {
		get { return AbigailFace; }
	}

	public Sprite Tally {
		get { return TallyFace; }
	}
}