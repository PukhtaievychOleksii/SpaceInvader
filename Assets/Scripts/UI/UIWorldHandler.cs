using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWorldHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreTextPrefab;
    [SerializeField]
    private GameObject HPImagePrefab;
    [SerializeField]
    private GameObject powerSliderPrefab;
    [SerializeField]
    private Canvas Canvas;
    [SerializeField]
    private float SpaceBetweenHPImages;
    private Text scoreText;
    private Stack<Image> HPImages = new Stack<Image>();
    private Slider powerSlider;
    private float fromEdgePixelsDistance = 30f;
    private int scoreTextLength = 6;
    void Start()
    {
        DataHolder.SetUIWorldHandler(this);
        LocateUI();

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LocateUI()
    {
        LocateScoreText();
        LocateHPImages();
        LocatePowerSlider();
    }

    private void LocateScoreText()
    {
        Camera MainCamera = Canvas.worldCamera;
        Text scoreText = scoreTextPrefab.GetComponent<Text>();
        Vector3 scoreTextLocation =MainCamera.ScreenToWorldPoint(new Vector3(fromEdgePixelsDistance + scoreText.rectTransform.rect.width / 2, MainCamera.pixelHeight - scoreText.rectTransform.rect.height,0));
        scoreTextLocation = new Vector3(scoreTextLocation.x, scoreTextLocation.y, 0);
        this.scoreText = Instantiate(scoreTextPrefab, scoreTextLocation, Quaternion.identity).GetComponent<Text>();
        this.scoreText.gameObject.transform.SetParent(Canvas.transform);
        this.scoreText.rectTransform.localScale = Vector3.one;
    }

    private void LocateHPImages()
    {
        for (int i = 0; i < DataHolder.SpaceShip.GetHealthPoints(); i++) {
            Image HPImage = Instantiate(HPImagePrefab, GetHPImageLocation(i), Quaternion.identity).GetComponent<Image>();
            HPImages.Push(HPImage);
            HPImage.gameObject.transform.SetParent(Canvas.transform);
            HPImage.rectTransform.localScale = Vector3.one;
        }
    }

    private Vector3 GetHPImageLocation(int posIndex)
    {
        Image image = HPImagePrefab.GetComponent<Image>();
        Vector3 screenPos = new Vector3(0, DataHolder.MainCamera.pixelHeight, 0);
        screenPos.y -= (scoreText.rectTransform.rect.height + SpaceBetweenHPImages) * 2;
        screenPos.x += fromEdgePixelsDistance + 10 + (image.rectTransform.rect.width + SpaceBetweenHPImages) * posIndex;
        Vector3 worldPos = DataHolder.MainCamera.ScreenToWorldPoint(screenPos);
        worldPos = new Vector3(worldPos.x, worldPos.y, 0);
        return worldPos;
    }

    public void UpdateScoreTest()
    {
        string newText = "";
        int score = DataHolder.Game.Match.Player.Score;
        string scoreStr = score.ToString();
        for (int i = 0;i < scoreTextLength - scoreStr.Length; i++)
        {
            newText += " " + " 0";
        }
        for(int i = 0;i < scoreStr.Length; i++)
        {
            newText += " " + scoreStr[i];
        }
        scoreText.text = newText;
        
    }

    public void UpdateHPImages()
    {
        if (HPImages.Count > 0)
        {
            Image image = HPImages.Pop();
            Destroy(image.gameObject);
        }
    }

    private void LocatePowerSlider()
    {
        GameObject PowerSlider = Instantiate(powerSliderPrefab, GetPowerSliderLocation(), Quaternion.identity);
        PowerSlider.transform.SetParent(Canvas.transform);
        PowerSlider.transform.localScale = Vector3.one;
        powerSlider = PowerSlider.GetComponentInChildren<Slider>();
        powerSlider.maxValue = DataHolder.SpaceShip.MaxPower;
    }

    private Vector3 GetPowerSliderLocation()
    {
        Vector3 screenPos = new Vector3(DataHolder.MainCamera.pixelWidth, DataHolder.MainCamera.pixelHeight / 2, 0);
        screenPos.x -= fromEdgePixelsDistance * 2;
        Vector3 worldPos = DataHolder.MainCamera.ScreenToWorldPoint(screenPos);
        worldPos = new Vector3(worldPos.x, worldPos.y, 0);
        return worldPos;

    }

    public void UpdatePowerSlider(float value)
    {
        powerSlider.value = value;
    }



     
}
