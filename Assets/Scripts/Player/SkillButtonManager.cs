using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillButtonManager : MonoBehaviour
{
    [Header("Skill Buttons")]
    [SerializeField] private List<Button> skillButtons;

    [Header("Skill Names")]
    [SerializeField] private List<string> skillNames;

    private HeroController heroController;
    private SkillCooldowns cooldownManager;

    private void Start()
    {
        heroController = FindObjectOfType<HeroController>();
        cooldownManager = heroController.GetComponent<SkillCooldowns>();

        for (int i = 0; i < skillButtons.Count; i++)
        {
            string skillName = skillNames[i];
            Button button = skillButtons[i];

            // Disable button interaction during cooldown
            button.onClick.AddListener(() => OnSkillButtonClick(skillName));
        }
    }

    private void Update()
    {
        // Update button interactability based on cooldown status
        for (int i = 0; i < skillButtons.Count; i++)
        {
            string skillName = skillNames[i];
            Button button = skillButtons[i];

            // Check if skill is on cooldown and disable button if needed
            button.interactable = !cooldownManager.IsSkillOnCooldown(skillName);
        }
    }

    private void OnSkillButtonClick(string skillName)
    {
        heroController.OnAttack(skillName);
    }
}
