using System;
using UnityEngine;
using UnityEngine.UI;

namespace AdrianMunteanTest
{
    public class WheelOfFortuneView : MonoBehaviour, IView
    {
        private const float MAX_WHEEL_ROTATION_SPEED = 1500f;
        private const float MIN_ANGLE_TO_ROTATE_TOWARDS = 10f;
        private const float MIN_ROTATION_TO_ROTATE_TOWARDS = 100f;
        private const float ROTATION_TO_BEGIN_SLOW_DOWN = 300f;
        private const float VALUE_FOR_SLOW_DOWN = 2000f;

        public Action<int> ButtonSpin;
        public Action ButtonBack;
        public Action<bool> WheelSpinResult;

        [SerializeField] private SelectOptionButton[] _wheelOptionButtons;
        [SerializeField] private Button _buttonBack;
        [SerializeField] private Button _buttonSpin;
        [SerializeField] private Transform _rotatingWheel;

        private bool _canRotate;
        private float _rotationSpeed;
        private int _selectedWheelOption;
        private int _stopAtWheelValue;
        private Vector3 _targetRotationEuler;

        private GameDataModel _gameDataModel;

        public void UpdateView(GameDataModel gameDataModel)
        {
            _gameDataModel = gameDataModel;
            gameObject.SetActive(true);
        }

        public void CloseView()
        {
            gameObject.SetActive(false);
        }

        public void SpinWheelToResult(int result)
        {
            _canRotate = true;

            _rotationSpeed = MAX_WHEEL_ROTATION_SPEED;
            _stopAtWheelValue = result;
            float stopAngleForValue = 22.5f + (_stopAtWheelValue - 1) * 45f;
            _targetRotationEuler = new Vector3(0, 0, stopAngleForValue);
        }

        public void SetDefaultOption()
        {
            _selectedWheelOption = 1;
            _wheelOptionButtons[0].SetSelected(true);
        }

        public void SetActionOnButtons()
        {
            _buttonSpin.onClick.AddListener(OnButtonSpin);
            _buttonBack.onClick.AddListener(OnButtonBack);

            SetActionOnWheelOptionButtons();
        }

        private void SetActionOnWheelOptionButtons()
        {
            for (int i = 0; i < _wheelOptionButtons.Length; i++)
            {
                int optionIndex = i + 1;
                _wheelOptionButtons[i].onClick.AddListener(delegate { OnWheelOptionButtons(optionIndex); });
            }
        }

        private void OnWheelOptionButtons(int wheelOption)
        {
            _selectedWheelOption = wheelOption;
            SetWheelOptionSelectStatus(wheelOption);
        }

        private void SetWheelOptionSelectStatus(int wheelOption)
        {
            for (int i = 0; i < _wheelOptionButtons.Length; i++)
            {
                _wheelOptionButtons[i].SetSelected(i == wheelOption - 1);
            }
        }

        private void OnButtonSpin()
        {
            ButtonSpin.Invoke(_selectedWheelOption);
        }

        private void OnButtonBack()
        {
            ButtonBack.Invoke();
        }

        private void WheelSpinDone()
        {
            WheelSpinResult.Invoke(_selectedWheelOption == _stopAtWheelValue);
        }

        // Update is called once per frame
        void Update()
        {
            if (_canRotate)
            {
                float angleRemaining = Quaternion.Angle(_rotatingWheel.rotation, Quaternion.Euler(_targetRotationEuler));

                if (angleRemaining < MIN_ANGLE_TO_ROTATE_TOWARDS && _rotationSpeed <= MIN_ROTATION_TO_ROTATE_TOWARDS)
                {
                    _rotatingWheel.rotation = Quaternion.RotateTowards(_rotatingWheel.rotation, Quaternion.Euler(_targetRotationEuler), Time.deltaTime * _rotationSpeed);

                    if (angleRemaining == 0)
                    {
                        _canRotate = false;
                        Invoke(nameof(WheelSpinDone), 1);
                        //Debug.Log(Time.time + "     ###   rotate DONE -> " + _rotatingWheel.rotation.eulerAngles.z + "   target Z -> " + _targetRotationEuler.z);
                    }
                }
                else
                {
                    if (_rotationSpeed > ROTATION_TO_BEGIN_SLOW_DOWN)
                    {
                        _rotationSpeed -= Time.deltaTime * VALUE_FOR_SLOW_DOWN;
                    }
                    else
                    {
                        if (_rotationSpeed > MIN_ROTATION_TO_ROTATE_TOWARDS)
                        {
                            _rotationSpeed -= Time.deltaTime * angleRemaining * 3f;
                        }
                    }

                    _rotatingWheel.rotation *= Quaternion.Euler(new Vector3(0, 0, -Time.deltaTime * _rotationSpeed));
                }
            }
        }
    }
}