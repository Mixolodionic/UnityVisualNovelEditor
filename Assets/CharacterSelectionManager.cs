using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject spriteBoxPrefab;
    public GameObject buttonPrefab;
    public Transform container;

    private bool isFirstPress = true;
    private GameObject leftButton = null;
    private GameObject rightButton = null;
    private GameObject centerSpriteBox = null;

    void Start()
    {
        GenerateButtonInCenter();
    }

    private void GenerateButtonInCenter()
    {
        GameObject centerButton = Instantiate(buttonPrefab, container);
        centerButton.transform.localScale = Vector3.one;

        centerButton.GetComponent<Button>().onClick.AddListener(() => OnCenterButtonPress(centerButton));
    }

    private void OnCenterButtonPress(GameObject centerButton)
    {
        centerSpriteBox = ReplaceWithSpritePrefab(centerButton, "center");

        if (isFirstPress)
        {
            leftButton = GenerateButtonOnSide("left");
            rightButton = GenerateButtonOnSide("right");
            isFirstPress = false;
        }
    }

    private GameObject ReplaceWithSpritePrefab(GameObject button, string position)
    {
        GameObject spriteBox = Instantiate(spriteBoxPrefab, container);
        spriteBox.transform.SetSiblingIndex(button.transform.GetSiblingIndex());

        Button closeButton = spriteBox.transform.Find("XButton").GetComponent<Button>();
        closeButton.onClick.AddListener(() => RemoveSpriteBox(spriteBox, button, position));

        Destroy(button);
        return spriteBox;
    }

    private void RemoveSpriteBox(GameObject spriteBox, GameObject originalButton, string position)
    {
        Destroy(spriteBox);

        if (position == "center")
        {
            if (leftButton != null) Destroy(leftButton);
            if (rightButton != null) Destroy(rightButton);

            isFirstPress = true;
            leftButton = null;
            rightButton = null;
            GenerateButtonInCenter();
        }

        else
        {
            GameObject newButton = Instantiate(buttonPrefab, container);
            newButton.transform.SetSiblingIndex(originalButton.transform.GetSiblingIndex());

            newButton.GetComponent<Button>().onClick.AddListener(() => ReplaceWithSpritePrefab(newButton, position));

            if (position == "left")
            {
                newButton.transform.SetSiblingIndex(0);
                leftButton = newButton;
            }
            else if (position == "right")
            {
                newButton.transform.SetSiblingIndex(container.childCount);
                rightButton = newButton;
            }

            newButton.GetComponent<Button>().onClick.AddListener(() => ReplaceWithSpritePrefab(newButton, position));
        }
    }

    private GameObject GenerateButtonOnSide(string side)
    {
        GameObject newButton = Instantiate(buttonPrefab, container);
        newButton.transform.localScale = Vector3.one;

        if (side == "left")
        {
            newButton.transform.SetSiblingIndex(0);
        }
        else if (side == "right") {
            newButton.transform.SetSiblingIndex(container.childCount);
        }

        newButton.GetComponent<Button>().onClick.AddListener(() => ReplaceWithSpritePrefab(newButton, side));

        return newButton;
    }
}
