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
            if (!isUnlocked)
            {
                bool currentlyUnlocked = IsUnlocked();
                keySlotCollider.enabled = currentlyUnlocked;
                keySlotImage.color = currentlyUnlocked ? Color.white : Color.gray;
                isUnlocked = currentlyUnlocked;

                Debug.Log(currentlyUnlocked ? "locked" : "unlocked");
            }
        }

        bool IsUnlocked()
        {
            return ability.IsAbilityUnlocked(abilityType);
        }
    }
}
