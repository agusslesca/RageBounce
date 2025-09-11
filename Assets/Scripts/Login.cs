using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Login : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_Text errorMessageText; // <-- Nuevo campo para el texto de error


    [Header("Opciones")]
    public string savedUsername = "player"; // usuario de ejemplo
    public string savedPassword = "1234";    // contrase�a de ejemplo
    public string sceneToLoad = "Juego";    // escena al iniciar sesi�n

    public void OnLoginButton()
    {
        // Limpiamos el texto de error antes de cada intento de login
        errorMessageText.text = "";

        string usuario = usernameInput.text;
        string contrasena = passwordInput.text;

        Debug.Log("Usuario: " + usuario);
        Debug.Log("Contrase�a: " + contrasena);

        if (usuario == savedUsername && contrasena == savedPassword)
        {
            Debug.Log("Login exitoso!");
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.Log("Usuario o contrase�a incorrectos.");
            // Mostramos el mensaje de error en la UI
            errorMessageText.text = "Usuario o contrase�a incorrectos.";
            errorMessageText.gameObject.SetActive(true);

            StartCoroutine(HideErrorMessageDelayed(1f));
        }
    }

    private IEnumerator HideErrorMessageDelayed(float delay)
    {
        // Espera la cantidad de segundos especificada
        yield return new WaitForSeconds(delay);

        // Oculta el mensaje de error
        errorMessageText.gameObject.SetActive(false);
    }
}