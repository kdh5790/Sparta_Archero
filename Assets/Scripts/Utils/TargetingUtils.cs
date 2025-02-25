using UnityEngine;

namespace Utils
{
    public static class TargetingUtils
    {
        // �� ��ü ������ �Ÿ��� ����ϴ� �Լ�
        public static float GetDistance(Transform from, Transform to)
        {
            if (from == null || to == null) return Mathf.Infinity;
            return Vector3.Distance(from.position, to.position);
        }

        // from���� to�� ���ϴ� ���� ���� ���͸� ��ȯ�ϴ� �Լ�
        public static Vector3 GetDirection(Transform from, Transform to)
        {
            if (from == null || to == null) return Vector3.zero;
            return (to.position - from.position).normalized;
        }
    }
}
