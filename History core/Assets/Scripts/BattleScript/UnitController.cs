using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Transform))]
public class UnitController : MonoBehaviour
{
    public float attackDuration = 0.2f;


    public float attackDistanceFactor = 0.9f;


    private Vector3 startPosition;

    void Awake()
    {
        startPosition = transform.position;
    }


    public void Attack(UnitController target)
    {
        Vector3 toTarget = target.transform.position - startPosition;
        Vector3 dir = toTarget.normalized;


        float fullDist = toTarget.magnitude;
        Vector3 attackPos = startPosition + dir * fullDist * attackDistanceFactor;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(attackPos, attackDuration).SetEase(Ease.OutQuad));
        seq.Append(transform.DOMove(startPosition, attackDuration).SetEase(Ease.InQuad));
        seq.OnComplete(() => { target.TakeDamage(); });
    }


    public void TakeDamage()
    {
        gameObject.SetActive(false);
    }
}