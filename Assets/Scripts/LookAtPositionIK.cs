using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

[ExecuteInEditMode]
[RequireComponent(typeof(Animator))]
public class LookAtPositionIK : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform target;
    [SerializeField, Range(0, 1), Tooltip("LookAt のグローバルウェイト、他のパラメーターの係数")]
    private float weight;
    [SerializeField, Range(0, 1), Tooltip("ボディが LookAt にどれぐらい関係するか決定します")]
    private float bodyWeight;
    [SerializeField, Range(0, 1), Tooltip("頭 (head)が LookAt にどれぐらい関係するか決定します")]
    private float headWeight;
    [SerializeField, Range(0, 1), Tooltip("目が LookAt にどれぐらい関係するか決定します")]
    private float eyesWeight;
    [SerializeField, Range(0, 1), Tooltip("0.0 の場合、キャラクターはモーションの制限が 0 であり、1.0 の場合、キャラクターは完全に制限され（ LookAt が不可能）、0.5 の場合、使用可能な範囲の半分（ 180 度）まで移動できます")]
    private float clampWeight;

    void OnEnable()
    {
        SetUp();
    }

    private void SetUp()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();

            if (_animator == null)
            {
                enabled = false;
                return;
            }
        }

        InspectActiveIKPass(_animator);
    }

    [Conditional("UNITY_EDITOR")]
    private void InspectActiveIKPass(Animator animator)
    {
        if (!animator.HasActiveIKPass())
        {
            Debug.LogWarning("Animator Controller > Layers > Settings > IK Pass を有効にしてください。");
        }
    }

#if UNITY_EDITOR
    void Update()
    {
        if (!Application.isPlaying)
        {
            _animator.Update(0);
        }
    }
#endif

    void OnAnimatorIK(int layerIndex)
    {
        _animator.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
        _animator.SetLookAtPosition(target.position);
    }
}

#if UNITY_EDITOR
public static class AnimatorExtensions
{
    public static UnityEditor.Animations.AnimatorController AnimatorController(this Animator animator)
    {
        var runtimeAnimatorController = animator.runtimeAnimatorController;
        return runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
    }

    public static bool HasActiveIKPass(this Animator animator)
    {
        var animatorController = animator.AnimatorController();
        if (animatorController != null)
        {
            return animatorController.layers.HasActiveIKPass();
        }
        return false;
    }

    public static bool HasActiveIKPass(this UnityEditor.Animations.AnimatorControllerLayer[] layers)
    {
        foreach (var layer in layers)
        {
            if (layer.iKPass)
            {
                return true;
            }
        }
        return false;
    }
}
#endif