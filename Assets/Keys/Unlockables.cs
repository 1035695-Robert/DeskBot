using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Keys
{
    public class Unlockables : MonoBehaviour, IUnlockAbility
    {
        [Header("unlockSlot Variables")] [SerializeField]
        private bool isUnlocked;

        [SerializeField] AbilityType abilityType;
        private Collider2D keySlotCollider;
        private Image keySlotImage;
        BotAbilities ability;


        private void Awake()
        {
            ability = BotAbilities.Instance;
            keySlotCollider = GetComponent<Collider2D>();
            keySlotImage = GetComponent<Image>();
            OnUnlockAbility();
        }

        public void OnUnlockAbility()
        {
            isUnlocked = IsUnlocked();
            LockToggle();
        }

        private void LockToggle()
        {
            switch (isUnlocked)
            {
                case true:
                    Debug.Log("check");
                    keySlotCollider.enabled = true;
                    keySlotImage.color = Color.white;
                    break;
                case false:
                    Debug.Log("NOT check");
                    keySlotCollider.enabled = false;
                    keySlotImage.color = Color.gray;
                    break;
            }
        }

        bool IsUnlocked()
        {
            return ability.IsAbilityUnlocked(abilityType);
        }
    }
}