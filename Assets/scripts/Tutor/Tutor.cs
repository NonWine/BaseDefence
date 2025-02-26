using UnityEngine;
using Zenject;

public class Tutor : MonoBehaviour 
{
    [SerializeField] private GameObject _tutorPanel;
    [SerializeField] private GameObject _leftTutorHand, _rightTutorHand;
    [Inject] private ScreenTouchController _screenTouchController;
    private int _haTutor;
    private float _timer;
    
    
    
    
    private void Start()
    {
        _haTutor = PlayerPrefs.GetInt(nameof(_haTutor), _haTutor);
        if (_haTutor == 0)
        {
            StartTutor();
   
        }
        else
            EndTutor();
    }
    

    private void OnDestroy()
    {
        _screenTouchController.OnLeftSideTap -= LeftHold;
        _screenTouchController.OnRightSideTap -= RightHold;
    }

    private void StartTutor()
    {
        _screenTouchController.OnLeftSideTap += LeftHold;
        _screenTouchController.OnRightSideTap += RightHold;
        _tutorPanel.SetActive(true);
    }

    private void EndTutor()
    {
        _tutorPanel.SetActive(false);
         PlayerPrefs.SetInt(nameof(_haTutor), 1);

    }

    private void LeftHold()
    {
        _timer += Time.deltaTime;

        if (_timer >= 1f)
        {
            _timer = 0f;
            _screenTouchController.OnLeftSideTap -= LeftHold;

            _leftTutorHand.SetActive(false);
            
            if(_leftTutorHand.activeSelf == false && _rightTutorHand.activeSelf == false)
                EndTutor();
        }
    }

    private void RightHold()
    {
        _timer += Time.deltaTime;

        if (_timer >= 1f)
        {
            _timer = 0f;
            _screenTouchController.OnRightSideTap -= RightHold;

            _rightTutorHand.SetActive(false);
            
                       
            if(_leftTutorHand.activeSelf == false && _rightTutorHand.activeSelf == false)
                EndTutor();
        }
    }
}
