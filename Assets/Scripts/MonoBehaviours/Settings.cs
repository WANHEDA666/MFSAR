using UnityEngine;
using Vuforia;

public class Settings : MonoBehaviour
{
    private ImageTargetBehaviour imageTargetBehaviour;

    private void Awake()
    {
        imageTargetBehaviour = gameObject.GetComponent<ImageTargetBehaviour>();
#if UNITY_IOS
        imageTargetBehaviour.SetMotionHint(DataSetTrackableBehaviour.TargetMotionHint.DYNAMIC);
#elif UNITY_ANDROID
        imageTargetBehaviour.SetMotionHint(DataSetTrackableBehaviour.TargetMotionHint.ADAPTIVE);
#endif
    }
}