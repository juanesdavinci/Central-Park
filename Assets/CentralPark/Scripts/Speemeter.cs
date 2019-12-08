using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speemeter : MonoBehaviour
{
    private Rigidbody _rb;
    private CarController _carController;
    private SpeedMeterUI _speedMeterUi;

    private float _speed;
    
    // Start is called before the first frame update
    void Start()
    {
        _carController = GetComponent<CarController>();

        if (_carController.playerNumber == 1)
        {
            _rb = GetComponent<Rigidbody>();
            _speedMeterUi = FindObjectOfType<SpeedMeterUI>();
        }
        else
        {
            Destroy(this);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_rb == null) return;
        if(_speedMeterUi == null) return;
        
        _speed = _rb.velocity.magnitude;
        _speedMeterUi.setSpeed(_speed);
    }
}
