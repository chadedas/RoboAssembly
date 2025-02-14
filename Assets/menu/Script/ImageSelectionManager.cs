using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageSelectionManager : MonoBehaviour
{
    public Image[] images; // Array of Image objects representing your selectable pictures
    public Button next1Button; // Button object for "Next"
    public Button next2Button; // Button object for "Next"
    public Button backButton; // Button object for "Back"
    public Image[] displayImages; // Array of images to display when each image is selected
    public Color selectedColor = Color.yellow; // Color for the selected frame
    private int selectedImageIndex = -1; // Track which image is selected (none initially)

    void Start()
    {
        // Disable the "Next" button initially
        next1Button.interactable = false;
        next2Button.interactable = false;

        // Hide all display images initially
        foreach (Image displayImage in displayImages)
        {
            displayImage.gameObject.SetActive(false);
        }

        // Add event listeners for each image
        for (int i = 0; i < images.Length; i++)
        {
            int index = i; // Local copy for closure

            // Check if there is a Button component in the Image
            Button imageButton = images[i].GetComponent<Button>();
            if (imageButton != null)
            {
                imageButton.onClick.AddListener(() => OnImageSelected(index));
            }
            else
            {
                Debug.LogError($"No Button component found in Image at index {i}.");
            }
        }

        // Add event listeners for the "Next" and "Back" buttons
        next1Button.onClick.AddListener(OnNext1Pressed);
        next2Button.onClick.AddListener(OnNext2Pressed);
        backButton.onClick.AddListener(OnBackPressed);
    }

    void OnImageSelected(int index)
    {
        // Ensure index is valid
        if (index < 0 || index >= images.Length || images[index] == null)
        {
            Debug.LogError($"Image at index {index} is null or out of bounds.");
            return;
        }

        // Hide all display images before showing the selected one
        foreach (Image displayImage in displayImages)
        {
            displayImage.gameObject.SetActive(false);
        }

        // Check if the clicked image is already selected
        if (selectedImageIndex == index)
        {
            selectedImageIndex = -1; // Reset selection
            next1Button.interactable = false; // Disable "Next" button
            next2Button.interactable = false;
            return; // Exit the method
        }

        // Update the selected image index
        selectedImageIndex = index;

        // Enable the "Next" button
        next1Button.interactable = true;
        next2Button.interactable = true;

        // Show the corresponding display image
        if (displayImages.Length > index && displayImages[index] != null)
        {
            displayImages[index].gameObject.SetActive(true);
        }
    }

    void OnNext1Pressed()
    {
        // Load the corresponding page depending on the selected image
        if (selectedImageIndex != -1)
        {
            switch (selectedImageIndex)
            {
                case 0:
                    SceneManager.LoadScene("KR150_1"); // Load scene for Image 1
                    break;
                case 1:
                    SceneManager.LoadScene("KR210_1"); // Load scene for Image 2
                    break;
                //case 2:
                    //SceneManager.LoadScene(4); // Load scene for Image 3
                    //break;
                // Add more cases if you have more images
                default:
                    Debug.LogWarning("No corresponding page found!");
                    break;
            }
        }
    }
        void OnNext2Pressed()
    {
        // Load the corresponding page depending on the selected image
        if (selectedImageIndex != -1)
        {
            switch (selectedImageIndex)
            {
                case 0:
                    SceneManager.LoadScene("KR150_2"); // Load scene for Image 1
                    break;
                case 1:
                    SceneManager.LoadScene("KR210_2"); // Load scene for Image 2
                    break;
                //case 2:
                    //SceneManager.LoadScene(4); // Load scene for Image 3
                    //break;
                // Add more cases if you have more images
                default:
                    Debug.LogWarning("No corresponding page found!");
                    break;
            }
        }
    }

    void OnBackPressed()
    {
        // Load the "Menu" scene when Back is pressed
        SceneManager.LoadScene(0); // Replace "Menu" with your target scene name
    }
}
