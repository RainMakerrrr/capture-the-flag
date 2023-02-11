using UnityEngine;

namespace Code.UI
{
    public class MiniSlider : MonoBehaviour
    {
        [SerializeField] private RectTransform _area;
        [SerializeField] private RectTransform _rect;

        private float _maximumX;
        private float _minimumX;

        private float _moveSpeed;
        private float _startMoveSpeed;

        public void Construct(float startMoveSpeed)
        {
            _startMoveSpeed = startMoveSpeed;
            _moveSpeed = _startMoveSpeed;
        }

        private void Start()
        {
            _maximumX = _area.rect.max.x;
            _minimumX = _area.rect.min.x;
        }

        private void Update()
        {
            _rect.anchoredPosition += new Vector2(_moveSpeed * Time.deltaTime, 0f);

            if (_rect.anchoredPosition.x > _maximumX)
            {
                _moveSpeed *= -1f;
                _rect.anchoredPosition = new Vector2(_maximumX, _rect.anchoredPosition.y);
            }

            if (_rect.anchoredPosition.x < _minimumX)
            {
                _moveSpeed = Mathf.Abs(_moveSpeed);
                _rect.anchoredPosition = new Vector2(_minimumX, _rect.anchoredPosition.y);
            }
        }
    }
}