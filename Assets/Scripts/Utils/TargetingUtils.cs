using UnityEngine;

namespace Utils
{
    public static class TargetingUtils
    {
        // 두 객체 사이의 거리를 계산하는 함수
        public static float GetDistance(Transform from, Transform to)
        {
            if (from == null || to == null) return Mathf.Infinity;
            return Vector3.Distance(from.position, to.position);
        }

        // from에서 to로 향하는 단위 방향 벡터를 반환하는 함수
        public static Vector3 GetDirection(Transform from, Transform to)
        {
            if (from == null || to == null) return Vector3.zero;
            return (to.position - from.position).normalized;
        }
    }
}
