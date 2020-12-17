using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerStats : MonoBehaviour
{
    #region  Singleton
    public static PlayerStats instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    [Header("Значения характеристик персонажа.")]
    [Tooltip("Текущий запас здоровья персонажа.")]
    [SerializeField] private float health;
    [Tooltip("Максимальный запас здоровья персонажа.")]
    [SerializeField] private float maxHelath;
    [Tooltip("Набранные очки.")] 
    [SerializeField] private int scores;
    [Tooltip("Время бессмертия персонажа.")]
    [SerializeField] private float immortalTime;
    
    [Header("Настройки перезагрузки уровня.")]
    [Tooltip("Время задержки перед перезапуском уровня.")]
    [SerializeField] private float reloadSceneTime = 2;
    
    [Header("Настройки тряски камеры при получении урона.")]
    [Tooltip("Продлжительность тряски камеры при получении урона.")]
    [SerializeField] private float durationCameraShake = 0.5f;
    [Tooltip("Сила тряски камеры при получении урона.")]
    [SerializeField] private float powerCameraShake = 0.5f;

    [Header("Отображение в интерфейсе.")] 
    [Tooltip("Полоска здоровья персонажа.")]
    [SerializeField] private Image healthBar;
    [Tooltip("Счётчик очков.")]
    [SerializeField] private Text scoresCounter;
    
    private PlayerInput _input;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private bool _isImmortal = false;
    private float _immortalTimer = 0;

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        scoresCounter.text = "" + scores;
        healthBar.fillAmount = health / maxHelath;
    }
    
    private void FixedUpdate()
    {
        if (_isImmortal)
        {
            _immortalTimer += Time.fixedDeltaTime;
            
            if (_immortalTimer >= immortalTime)
            {
                _isImmortal = false;
            }
        }
    }

    public void GetDamage(float value,bool isMortalDamage)
    {
        if (_isImmortal)
        {
            return;
        }

        health -= value;

        if (!isMortalDamage)
        {
            _animator.SetTrigger("Hit");
        }

        CameraShaker.Shake(durationCameraShake, powerCameraShake, CameraShaker.ShakeMode.XY);
        
        if (health <= 0)
        {
            _animator.SetBool("Dead",true);
            _input.enabled = false;
            _rigidbody2D.simulated = false;

            StartCoroutine(ReloadScene(reloadSceneTime));
        }

        healthBar.fillAmount = health / maxHelath;
    }

    public void GetImmortalBuff()
    {
        _immortalTimer = 0;
        _isImmortal = true;
    }

    public bool GetHealing(float value)
    {
        if (health == maxHelath)
        {
            return false;
        }

        health += value;

        if (health > maxHelath)
        {
            health = maxHelath;
        }

        healthBar.fillAmount = health / maxHelath;

        return true;
    }

    public void GetScores(int value)
    {
        scores += value;
        
        scoresCounter.text = "" + scores;
    }

    private IEnumerator ReloadScene(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
